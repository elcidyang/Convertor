using MetroSet_UI.Forms;

namespace Y.Convertor.Common
{
    public class YMessageBox
    {
        public static void ShowMsgBox(int height, string msg)
        {
            MetroSetMessageBox.Show(new FrmMessageBox(height), msg);
        }
    }
}
