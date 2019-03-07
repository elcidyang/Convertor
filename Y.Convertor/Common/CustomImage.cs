using System.Drawing;
using System.Drawing.Drawing2D;

namespace Y.Convertor.Common
{
    public class CustomImage
    {
        public int Press { get; set; }
        //进度条图片属性
        public Image PressImg
        {
            get
            {
                Bitmap bmp = new Bitmap(104, 30); //这里给104是为了左边和右边空出2个像素，剩余的100就是百分比的值
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White); //背景填白色
                //g.FillRectangle(Brushes.Red, 2, 2, this.Press, 26);  //普通效果
                //填充渐变效果
                g.FillRectangle(new LinearGradientBrush(new Point(30, 2), new Point(30, 30), Color.CornflowerBlue, Color.DarkTurquoise),
                    0, 0,
                    this.Press, 25);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.DrawString(this.Press+"%",new Font(FontFamily.GenericSerif, 12,FontStyle.Regular), drawBrush,new PointF(2,5));
                return bmp;
            }
        }
    }
}
