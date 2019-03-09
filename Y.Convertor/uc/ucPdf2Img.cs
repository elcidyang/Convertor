using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using MetroSet_UI.Forms;
using XPTable.Models;
using Y.Convertor.Common;
using Y.Convertor.Models;
using Cell = XPTable.Models.Cell;
using Row = XPTable.Models.Row;
using RowStyle = XPTable.Models.RowStyle;

namespace Y.Convertor.uc
{
    public partial class ucPdf2Img : UserControl
    {
        private MainForm _mainForm;
        public delegate void ProgressNotice(NoticeResult result);
        public delegate void ConvertComplete();
        private Thread convertThread = null;
        /// <summary>
        /// 转换中的表行索引
        /// </summary>
        private int _convertingtablerowIndex;
        public ucPdf2Img(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            SetConvertBtnVisiable(false);
            InitTable();
            this.cboImgType.SelectedIndex = 0;
        }


        #region table

        private const int FileNameColIndex = 0;
        private const int FileSizeColIndex = 1;
        private const int PageStartColIndex = 2;
        private const int PageEndColIndex = 3;
        private const int ProgressColIndex = 4;
        private const int DeleteColIndex = 5;
        private const int FilePathColIndex = 6;
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitTable()
        {
            this.tableFiles.ColumnModel = this.columnModel;
            this.tableFiles.TableModel = this.tableModel;
            this.tableFiles.CellMouseDown += TableFiles_CellMouseDown;
            this.tableFiles.CellMouseEnter += TableFiles_CellMouseEnter;
            this.tableFiles.CellMouseLeave += TableFiles_CellMouseLeave;
            ImageColumn fileNameColumn = new ImageColumn("文件名", 240)
            {
                DrawText = true
            };
            TextColumn fileSizeColumn = new TextColumn("文件大小", 90)
            {
                Editable = false
            };
            NumberColumn pageStartColumn = new NumberColumn("起始页", 60)
            {
                Editable = true,
                Minimum = 1,
                Maximum = 2000,
                ShowUpDownButtons = true
            };
            NumberColumn pageEndColumn = new NumberColumn("结束页", 60)
            {
                Editable = true,
                Minimum = 1,
                Maximum = 2000,
                ShowUpDownButtons = true
            };
            ProgressBarColumn progressColumn = new ProgressBarColumn("状态", 100);
            ImageColumn deleteColumn = new ImageColumn("移除", 35)
            {
                DrawText = false
            };
            TextColumn filePathColumn = new TextColumn("文件路径", null, 0, false);
            this.columnModel.Columns.AddRange(new Column[] {fileNameColumn,
                fileSizeColumn,
                pageStartColumn,
                pageEndColumn,
                progressColumn,
                deleteColumn,filePathColumn });
        }

        private void TableFiles_CellMouseLeave(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Column == DeleteColIndex)
            {
                var cell = this.tableFiles.TableModel.Rows[e.Row].Cells[e.Column];
                cell.Image = Properties.Resources.trash_empty;
            }
        }

        private void TableFiles_CellMouseEnter(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Column == DeleteColIndex)
            {
                var cell = this.tableFiles.TableModel.Rows[e.Row].Cells[e.Column];
                cell.Image = Properties.Resources.trash_empty_alt;
            }
        }

        private void TableFiles_CellMouseDown(object sender, XPTable.Events.CellMouseEventArgs e)
        {
            if (e.Column == DeleteColIndex)
            {
                var currentRow = this.tableFiles.TableModel.Rows[e.Row];
                this.tableFiles.TableModel.Rows.RemoveAt(e.Row);
                if (this.tableFiles.TableModel.Rows.Count <= 0)
                {
                    if (convertThread != null && convertThread.IsAlive)
                    {
                        convertThread.Abort();
                    }
                    SetConvertBtnVisiable(false);
                    ConvertCompleteAction();
                    AddDragTip();
                    return;
                }
                //判断移除行转换的状态
                //重启线程转换
                ProgressNotice notice = ProgressAction;//创建一个委托对象
                ConvertComplete convertCompleted = ConvertCompleteAction;
                if (convertThread != null && convertThread.IsAlive)
                {
                    convertThread.Abort();
                }
                convertThread = new Thread(StartConvert);
                var imgType = GetConvertedImgType();
                if ((ConvertState)currentRow.Tag == ConvertState.Converted)
                {
                    _convertingtablerowIndex--;
                }
                convertThread.Start(new List<object> { _convertingtablerowIndex, notice, convertCompleted, imgType });
            }
        }
        #endregion

        private void ucPdf2Imgcs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ?
                DragDropEffects.Copy : DragDropEffects.None;
        }

        private void ucPdf2Img_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var pdfFiles = new List<Pdf2JpgFileInfo>();
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (file.EndsWith(".pdf"))
                    {
                        var pdfFile = TransformPdfFile(file);
                        pdfFiles.Add(pdfFile);
                    }
                }

                if (pdfFiles.Count > 0)
                {
                    ClearDragTip();
                    SetConvertBtnVisiable(true);
                    FillDataGrid(pdfFiles);
                }
            }
        }

        private void ucPdf2Img_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void lblTip_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void ShowOpenFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "*.pdf|*.pdf"
            };
            fileDialog.ShowDialog();
            var files = fileDialog.FileNames;
            if (files.Length <= 0)
            {
                return;
            }

            ClearDragTip();
            SetConvertBtnVisiable(true);
            var safeFiles = fileDialog.SafeFileNames;
            var filePaths = fileDialog.FileNames;
            var pdfFiles = TransformPdfFiles(files, safeFiles, filePaths);
            FillDataGrid(pdfFiles);
        }

        private void FillDataGrid(List<Pdf2JpgFileInfo> pdfFiles)
        {
            var rowStyle = new RowStyle
            {
                Font = new Font("微软雅黑", 9f, FontStyle.Regular)
            };
            var index = this.tableFiles.TableModel.Rows.Count;
            var sameFilesName = new List<string>();
            foreach (var pdfFile in pdfFiles)
            {
                if (IsExistSameFileName(pdfFile.FileName))
                {
                    sameFilesName.Add(pdfFile.FileName);
                    continue;
                }
                var row = new Row();
                row.Cells.AddRange(new Cell[]
                {
                    new Cell(pdfFile.FileName, Properties.Resources.pdf_ico),
                    new Cell(Utils.CountSize(Utils.GetFileSize(pdfFile.FilePath))),
                    new Cell(1),
                    new Cell(100),
                    new Cell(0),
                    new Cell("删除", Properties.Resources.trash_empty),
                    new Cell(pdfFile.FilePath)
                });
                row.Tag = ConvertState.NotConvert;
                this.tableFiles.TableModel.Rows.Add(row);
                this.tableFiles.TableModel.Rows[index].RowStyle = rowStyle;
                index++;
            }

            if (sameFilesName.Count > 0)
            {
                if (sameFilesName.Count > 5)
                {
                    sameFilesName = sameFilesName.Take(5).ToList();
                    sameFilesName.Add("... ...");
                }
                var height = sameFilesName.Count * 20 + 150;
                var strNames = string.Join("\n", sameFilesName);
                YMessageBox.ShowMsgBox(height, $"下列文件：\n{strNames}\n已存在，将忽略添加！");
            }
        }


        private void btnContinueAdd_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void btnClearTable_Click(object sender, EventArgs e)
        {
            this.tableFiles.TableModel.Rows.Clear();
        }

        private void btnStartConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_mainForm.LbllblSavePath.Text.Trim()))
            {
                YMessageBox.ShowMsgBox(100, "请先选择要保存的路径！");
                return;
            }

            this.btnStartConvert.Text = "转换中...";
            SetConvertBtnEnable(false);
            ProgressNotice notice = ProgressAction;//创建一个委托对象
            ConvertComplete convertCompleted = ConvertCompleteAction;
            if (convertThread != null)
            {
                convertThread = null;
            }
            convertThread = new Thread(StartConvert);
            var imgType = GetConvertedImgType();
            convertThread.Start(new List<object> { 0, notice, convertCompleted, imgType });
        }

        /// <summary>
        /// 开始转换
        /// </summary>
        public void StartConvert(object p)
        {
            var savePath = _mainForm.LbllblSavePath.Text;
            var parameters = p as List<object>;
            _convertingtablerowIndex = Convert.ToInt32(parameters[0]);
            var noticeAction = parameters[1];
            var convertCompleteAction = parameters[2];
            ConvertComplete completedCalback = convertCompleteAction as ConvertComplete;
            ProgressNotice noticeCallback = noticeAction as ProgressNotice;
            var imgType = parameters[3].ToString();
            foreach (Row row in this.tableFiles.TableModel.Rows)
            {
                var filePath = row.Cells[FilePathColIndex].Text;
                var fileName = row.Cells[FileNameColIndex].Text;
                var fullFileName = Path.Combine(savePath, fileName.Contains(".") ?
                    fileName.Remove(fileName.LastIndexOf(".")) : fileName);
                if (!Directory.Exists(fullFileName))
                {
                    Directory.CreateDirectory(fullFileName);
                }

                try
                {
                    row.Tag = ConvertState.Converting;
                    HandleDocumentToImg(filePath, fullFileName, imgType, noticeCallback, _convertingtablerowIndex);
                }
                catch (Exception ex)
                {
                    row.Tag = ConvertState.NotConvert;
                    if (ex is InvalidPasswordException)
                    {
                        var noticeResult = new NoticeResult
                        {
                            Success = false,
                            ErrMsg = $"文件\n{fileName}\n是加密文件，请先解密！"
                        };
                        this.Invoke(noticeCallback, noticeResult);
                    }
                    else
                    {
                        throw;
                    }
                }
                //标志为转换完成
                if ((ConvertState)row.Tag == ConvertState.Converting)
                {
                    row.Tag = ConvertState.Converted;
                }
                _convertingtablerowIndex++;
            }
            this.Invoke(completedCalback);
            convertThread = null;
        }

        private void HandleDocumentToImg(string filePath, string fullFileName, string imgType, ProgressNotice noticeCallback, int rowIndex)
        {
            Document pdfDocument = new Document(filePath);
            var pageCount = pdfDocument.Pages.Count;
            var realPercent = 0.0;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                realPercent += (double)100 / pageCount;
                using (FileStream imageStream = new FileStream(fullFileName + "\\" + pageIndex + imgType,
                    FileMode.Create))
                {
                    Resolution resolution = new Resolution(300);
                    JpegDevice jpegDevice = new JpegDevice(resolution, 100);
                    jpegDevice.Process(pdfDocument.Pages[pageIndex], imageStream);
                    imageStream.Close();
                }
                if (pageIndex == pdfDocument.Pages.Count)
                {
                    realPercent = 100;
                }
                var noticeResult = new NoticeResult
                {
                    Success = true,
                    rowIndex = rowIndex,
                    pageIndex = pageIndex,
                    per = (int)realPercent
                };
                this.Invoke(noticeCallback, noticeResult);
            }
        }


        private void ConvertCompleteAction()
        {
            this.btnStartConvert.Text = "开始转换";
            SetConvertBtnEnable(true);
        }
        public void ProgressAction(NoticeResult result)
        {
            if (result.Success)
            {
                try
                {
                    this.tableFiles.TableModel.Rows[result.rowIndex].Cells[ProgressColIndex].Data = result.per;
                }
                catch (Exception)
                {
                    convertThread.Abort();
                }
            }
            else
            {
                YMessageBox.ShowMsgBox(150, result.ErrMsg);
            }
        }


        /// <summary>
        /// 表格中是否已存在相同文件名文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsExistSameFileName(string fileName)
        {
            foreach (Row row in this.tableFiles.TableModel.Rows)
            {
                if (row.Cells[FileNameColIndex].Text == fileName)
                {
                    return true;
                }
            }
            return false;
        }

        private List<Pdf2JpgFileInfo> TransformPdfFiles(string[] files, string[] safeFiles, string[] filePaths)
        {
            var pdf2Jpgs = new List<Pdf2JpgFileInfo>();
            for (var i = 0; i < files.Length; i++)
            {
                var pdf2Jpg = new Pdf2JpgFileInfo
                {
                    FileName = safeFiles[i],
                    FilePath = filePaths[i],
                    FileSize = Utils.CountSize(Utils.GetFileSize(filePaths[i]))
                };
                pdf2Jpgs.Add(pdf2Jpg);
            }
            return pdf2Jpgs;
        }

        private Pdf2JpgFileInfo TransformPdfFile(string filePath)
        {
            var pdfFile = new Pdf2JpgFileInfo();
            pdfFile.FilePath = filePath;
            pdfFile.FileName = filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
            pdfFile.FileSize = Utils.CountSize(Utils.GetFileSize(filePath));
            return pdfFile;
        }

        private void SetConvertBtnVisiable(bool isShow)
        {
            if (isShow)
            {
                tableFiles.Show();
                panelBottom.Show();
            }
            else
            {
                tableFiles.Hide();
                panelBottom.Hide();
            }
        }

        private void SetConvertBtnEnable(bool enable)
        {
            this.btnStartConvert.Enabled = enable;
            this.btnClearTable.Enabled = enable;
            this.btnContinueAdd.Enabled = enable;
        }

        private void ClearDragTip()
        {
            this.lblTip.Hide();
            this.lblTip.Click -= lblTip_Click;
            this.Click -= ucPdf2Img_Click;
        }

        private void AddDragTip()
        {
            this.lblTip.Click -= lblTip_Click;
            this.Click -= ucPdf2Img_Click;
            this.lblTip.Show();
            this.lblTip.Click += lblTip_Click;
            this.Click += ucPdf2Img_Click;
        }

        /// <summary>
        /// 获取转换后的图片后缀
        /// </summary>
        /// <returns></returns>
        private string GetConvertedImgType()
        {
            var suffix = string.Empty;
            var imgType = this.cboImgType.SelectedItem.ToString();
            switch (imgType)
            {
                case "JPG":
                    suffix = ".jpg";
                    break;
                case "PNG":
                    suffix = ".png";
                    break;
                case "GIF":
                    suffix = ".gif";
                    break;
                case "BMP":
                    suffix = ".bmp";
                    break;
                case "TIF":
                    suffix = ".tif";
                    break;
            }
            return suffix;
        }

        /// <summary>
        /// 文件转换状态
        /// </summary>
        public enum ConvertState
        {
            NotConvert = 0,
            Converting = 1,
            Converted = 2
        }

        /// <summary>
        /// 转换消息返回结构
        /// </summary>
        public class NoticeResult
        {
            public bool Success { get; set; }

            public string ErrMsg { get; set; }
            /// <summary>
            /// 行索引
            /// </summary>
            public int rowIndex { get; set; }
            /// <summary>
            /// 页索引
            /// </summary>
            public int pageIndex { get; set; }
            /// <summary>
            /// 进度百分比
            /// </summary>
            public int per { get; set; }
        }
    }
}
