using MetroSet_UI.Forms;

namespace Y.Convertor
{
    public partial class FrmMessageBox : MetroSetForm
    {
        public FrmMessageBox(int height)
        {
            InitializeComponent();
            this.Height = height * 3;
        }
    }
}
