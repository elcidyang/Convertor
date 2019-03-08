using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XPTable.Models;
using Y.Convertor.Common;
using Y.Convertor.Models;
using RowStyle = XPTable.Models.RowStyle;

namespace Y.Convertor.uc
{
    public partial class ucPdf2Img : UserControl
    {
        public ucPdf2Img()
        {
            InitializeComponent();
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
            ImageColumn deleteColumn = new ImageColumn("删除", 35)
            {
                DrawText = false
            };
            TextColumn filePathColumn = new TextColumn("文件路径",null,0,false);
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
                this.tableFiles.TableModel.Rows.RemoveAt(e.Row);
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
            this.lblTip.Visible = false;
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
            SetConvertBtnVisiable(true);
            ClearDragTip();
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
            foreach (var pdfFile in pdfFiles)
            {
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
                this.tableFiles.TableModel.Rows.Add(row);
                this.tableFiles.TableModel.Rows[index].RowStyle = rowStyle;
                index++;
            }

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
            pdfFile.FileName = filePath.Substring(filePath.LastIndexOf("\\"), filePath.Length - filePath.LastIndexOf("\\") + 1);
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

        private void btnContinueAdd_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }
    }
}
