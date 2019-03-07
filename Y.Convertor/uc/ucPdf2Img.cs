using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Y.Convertor.Common;
using Y.Convertor.Models;

namespace Y.Convertor.uc
{
    public partial class ucPdf2Img : UserControl
    {
        public ucPdf2Img()
        {
            InitializeComponent();
            SetConvertBtnVisiable(false);
        }

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
            var pafFiles = TransformPdfFiles(files, safeFiles, filePaths);
            //FillDataGrid(pdf2Jpgs);
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
            pdfFile.FileName = filePath.Substring(filePath.LastIndexOf("\\"),filePath.Length- filePath.LastIndexOf("\\")+1);
            pdfFile.FileSize = Utils.CountSize(Utils.GetFileSize(filePath));
            return pdfFile;
        }

        private void SetConvertBtnVisiable(bool isShow)
        {
            if (isShow)
            {
                this.btnContinueAdd.Show();
                this.btnStartConvert.Show();
            }
            else
            {
                this.btnContinueAdd.Hide();
                this.btnStartConvert.Hide();
            }
        }

    }
}
