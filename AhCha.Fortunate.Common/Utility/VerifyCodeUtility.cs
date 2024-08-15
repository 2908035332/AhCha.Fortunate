using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace AhCha.Fortunate.Common.Utility
{
    public class VerifyCodeUtility
    {

        /// <summary>
        /// 生成指定位数的随机数  
        /// </summary>
        /// <param name="CodeNum">返回验证码位数，默认 4 位</param>
        /// <returns></returns>
        public static string GetCode(int CodeNum = 4)
        {
            //验证码可以显示的字符集合  
            string Vchar = "2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,M,N,P,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组   
            string code = "";//产生的随机数  
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  
            Random rand = new Random();
            for (int i = 1; i < CodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
                }
                int t = rand.Next(61);//获取随机数  
                if (temp != -1 && temp == t)
                {
                    return GetCode(CodeNum);//如果获取的随机数重复，则递归调用  
                }
                temp = t;//把本次产生的随机数记录起来  
                code += VcArray[t];//随机数的位数加一  
            }
            return code;

        }

        /// <summary>
        /// 根据随机数生成图片
        /// </summary>
        /// <param name="code">图片中的文字</param>
        /// <returns></returns>

        public static MemoryStream Create(string code)
        {
            MemoryStream ms = null;
            Random random = new Random();
            //验证码颜色集合  
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

            using (var img = new Bitmap((int)code.Length * 18, 32))
            {
                using (var g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);//背景设为白色

                    //在随机位置画背景点  
                    for (int i = 0; i < 100; i++)
                    {
                        int x = random.Next(img.Width);
                        int y = random.Next(img.Height);
                        g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 3, 6);
                    }
                    //验证码绘制在g中  
                    for (int i = 0; i < code.Length; i++)
                    {
                        int cindex = random.Next(7);//随机颜色索引值  
                        int findex = random.Next(5);//随机字体索引值  
                        Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
                        Brush b = new SolidBrush(c[cindex]);//颜色  
                        int ii = 4;
                        if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                        {
                            ii = 2;
                        }
                        g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符  
                    }
                    ms = new MemoryStream();//生成内存流对象  
                    img.Save(ms, ImageFormat.Png);//将此图像以Png图像文件的格式保存到流中  
                }
            }

            return ms;
        }

        /// <summary>
        /// 根据随机数生成图片
        /// </summary>
        /// <param name="validateCode">图片中的文字</param>
        /// <returns></returns>
        public static MemoryStream CreateValidGraphic(string validateCode)
        {
            Bitmap img = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(img);
            try
            {
                Random random = new Random();//生成随机数
                g.Clear(Color.White);//清空图片背景色
                for (int i = 0; i < 25; i++)//画图片的干扰线
                {
                    int x1 = random.Next(img.Width);
                    int x2 = random.Next(img.Width);
                    int y1 = random.Next(img.Height);
                    int y2 = random.Next(img.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                }
                Font font = new Font("Arial", 13, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                for (int i = 0; i < 100; i++)//画图片的前景干扰点
                {
                    int x = random.Next(img.Width);
                    int y = random.Next(img.Height);
                    img.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, img.Width - 1, img.Height - 1);//画图片的边框线
                MemoryStream stream = new MemoryStream();
                img.Save(stream, ImageFormat.Png);
                return stream;//输入图片
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
