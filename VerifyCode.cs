using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace second
{
    public static class VerifyCode
    {
        static VerifyCode()
        {
            Init();
        }
        public static void Init(int w=100,int h=42,int p=100,int l=25,int c=6)
        {
            Width = w;
            Height = h;
            PixelNumber = p;
            LineNumber = l;
            Count = c;
        }
        public static string CreateCodeStr()
        {
            StringBuilder code = new StringBuilder();
            Random random = new Random(DateTime.Now.Second);
            for (int i = 0; i < Count; i++)
            {
                int number = random.Next();
                if (number % 2 == 0)
                    code.Append((char)('0' + number % 9));
                else
                    code.Append((char)('A' + number % 26));
            }
            return code.ToString();
        }
        public static byte[] CreateCode(string codeStr)
        {
            Random random = new Random(DateTime.Now.Second);
            Bitmap bitmap = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bitmap);
            try
            {
                g.Clear(Color.White);
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, Width - 1, Height - 1);
                for (int i = 0; i < LineNumber; i++)
                {
                    int x1 = random.Next(Width);
                    int x2 = random.Next(Width);
                    int y1 = random.Next(Height);
                    int y2 = random.Next(Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                for (int j = 0; j < PixelNumber; j++)
                {
                    int x = random.Next(Width);
                    int y = random.Next(Height);
                    bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                Font font = new Font(FontFamily.GenericSerif, 16, FontStyle.Italic | FontStyle.Bold | FontStyle.Strikeout);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(codeStr, font, brush, 4, 2);
                MemoryStream m = new MemoryStream();
                bitmap.Save(m, ImageFormat.Jpeg);
                return m.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                bitmap.Dispose();
                g.Dispose();
            }
        }
        public static int Width
        {
            get;
            set;
        }
        public static int Height
        {
            get;
            set;
        }
        public static int PixelNumber
        {
            get;
            set;
        }
        public static int LineNumber
        {
            get;
            set;
        }
        public static int Count
        {
            get;
            set;
        }
    }
}