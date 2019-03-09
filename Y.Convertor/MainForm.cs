using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Controls;
using MetroSet_UI.Forms;
using Y.Convertor.Common;
using Y.Convertor.uc;

namespace Y.Convertor
{
    public partial class MainForm : MetroSetForm
    { 
        /// <summary>
        /// 保存路径
        /// </summary>
        public MetroSetLabel LbllblSavePath;
        public MainForm()
        {
            InitializeComponent();
            LbllblSavePath = this.lblSavePath;
        }

        private void btnPDF2JPG_Click(object sender, System.EventArgs e)
        {
            ClearBtnsColor();
            SetBtnClick(this.btnPDF2JPG);
            this.panelContainer.Controls.Clear();
            var uc = new ucPdf2Img(this)
            {
                Dock = DockStyle.Fill,
                AllowDrop = true
            };
            this.panelContainer.Controls.Add(uc);
        }

        

        private void btnPDF2WORD_Click(object sender, System.EventArgs e)
        {
            ClearBtnsColor();
            SetBtnClick(this.btnPDF2WORD);

        }

        private void btnPDF2PPT_Click(object sender, System.EventArgs e)
        {
            ClearBtnsColor();
            SetBtnClick(this.btnPDF2PPT);
        }


        private void SetBtnClick(MetroDefaultSetButton btn)
        {
            btn.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            btn.NormalTextColor = Color.White;
        }


        private void ClearBtnsColor()
        {
            var controls = this.panelPDF1.Controls;
            foreach (Control control in controls)
            {
                if (control is MetroDefaultSetButton)
                {
                    var btn = control as MetroDefaultSetButton;
                    btn.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
                    btn.NormalTextColor = Color.Black;
                    btn.Refresh();
                }
            }
        }

        private void btnSelectSavePath_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            dialog.ShowDialog();
            var path = dialog.SelectedPath;
            if (!string.IsNullOrEmpty(path))
            {
                this.lblSavePath.Text = path;
            }
        }

        private void btnOpenDir_Click(object sender, System.EventArgs e)
        {
            if (!ValidConvertFilePath())
            {
                return;
            }
            System.Diagnostics.Process.Start("explorer.exe", this.lblSavePath.Text);
        }

        private bool ValidConvertFilePath()
        {
            if (string.IsNullOrEmpty(this.lblSavePath.Text.Trim()))
            {
                YMessageBox.ShowMsgBox(100, "请先选择要保存的路径！");
                return false;
            }
            return true;
        }
    }
}
