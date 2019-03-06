using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Controls;
using MetroSet_UI.Forms;

namespace Y.Convertor
{
    public partial class MainForm : MetroSetForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPDF2JPG_Click(object sender, System.EventArgs e)
        {
            ClearBtnsColor();
            SetBtnClick(this.btnPDF2JPG);
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

    }
}
