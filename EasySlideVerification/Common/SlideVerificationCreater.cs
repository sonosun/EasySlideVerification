using EasySlideVerification.Common;
using EasySlideVerification.Model;
using EasySlideVerification.Store;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;

namespace EasySlideVerification.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationCreater
    {
        /// <summary>
        /// 
        /// </summary>
        private SlideVerificationCreater() { }

        public static readonly SlideVerificationCreater Instance = new SlideVerificationCreater();

        Random random = new Random();

        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationInfo Create(SlideVerificationParam param)
        {
            SlideVerificationInfo result = new SlideVerificationInfo();
            result.Key = Guid.NewGuid().ToString("N");

            using (Bitmap coverImage = ImageUtil.GetImage(param.SlideImage))
            using (Bitmap sourceImage = ImageUtil.GetImage(param.BackgroundImage))
            {
                result.BgWidth = sourceImage.Width;
                result.BgHeight = sourceImage.Height;
                result.SlideWidth = coverImage.Width;
                result.SlideHeight = coverImage.Height;
                result.PositionX = random.Next(coverImage.Width, sourceImage.Width - coverImage.Width - param.Edge);
                result.PositionY = random.Next(coverImage.Height, sourceImage.Height - coverImage.Height);

                //滑块图片
                result.SlideImg = CaptureImage(sourceImage, coverImage, result.PositionX, result.PositionY);
                //背景图片
                result.BackgroundImg = DrawBackground(sourceImage, coverImage, result.PositionX, result.PositionY, param.MixedCount);
            }

            return result;
        }

        /// <summary>
        /// 画背景图
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="coverImage"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="mixedCount"></param>
        /// <returns></returns>
        private byte[] DrawBackground(Bitmap sourceImage, Bitmap coverImage, int positionX, int positionY, int mixedCount)
        {
            //背景图片
            using (Graphics graphics = Graphics.FromImage(sourceImage))
            {
                graphics.DrawImage(coverImage, positionX, positionY, coverImage.Width, coverImage.Height);
                graphics.Save();
            }

            //画混淆图
            DrawMix(sourceImage, coverImage, mixedCount, positionX, positionY);

            return ImageUtil.ImageToByteArr(sourceImage, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 画混淆图
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="mixImage"></param>
        /// <param name="count"></param>
        /// <param name="slidePositionX"></param>
        /// <param name="slidePositionY"></param>
        private void DrawMix(Image sourceImage, Image mixImage, int count, int slidePositionX, int slidePositionY)
        {
            using (Graphics graphics = Graphics.FromImage(sourceImage))
            {
                int coverWidth = mixImage.Width;
                int coverHeight = mixImage.Height;
                for (int i = 0; i < count; i++)
                {
                    int offsetX = random.Next(mixImage.Width, sourceImage.Width - mixImage.Width);
                    while (Math.Abs(offsetX - slidePositionX) < coverWidth * 2)
                    {
                        offsetX = random.Next(mixImage.Width, sourceImage.Width - mixImage.Width);
                    }

                    //int offsetY = random.Next(mixImage.Height, sourceImage.Height - mixImage.Height);
                    int offsetY = slidePositionY;

                    graphics.DrawImage(mixImage, offsetX, offsetY, coverWidth, coverHeight);
                    graphics.Save();
                }
            }
        }

        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="fromImage">来源图片</param>        
        /// <param name="offsetX">从偏移X坐标位置开始截取</param>
        /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
        /// <param name="width">保存图片的宽度</param>
        /// <param name="height">保存图片的高度</param>
        /// <returns></returns>
        private byte[] CaptureImage(Image fromImage, int offsetX, int offsetY, int width, int height)
        {
            //创建新图位图
            using (Bitmap bitmap = new Bitmap(width, height))
            //创建作图区域
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                //截取原图相应区域写入作图区
                graphic.DrawImage(fromImage, 0, 0, new Rectangle(offsetX, offsetY, width + 6, height + 6), GraphicsUnit.Pixel);
                graphic.Save();

                return ImageUtil.ImageToByteArr(bitmap, ImageFormat.Png);
            }
        }

        /// <summary>
        /// 从大图中根据像素点，生成图片，作为拼图块
        /// </summary>
        /// <param name="backgroudImage"></param>
        /// <param name="coverImage"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        private byte[] CaptureImage(Bitmap backgroudImage, Bitmap coverImage, int offsetX, int offsetY)
        {
            //创建新图位图
            using (Bitmap bitmap = new Bitmap(coverImage.Width, coverImage.Height))
            {
                for (int y = 0; y < coverImage.Height; y++)
                {
                    for (int x = 0; x < coverImage.Width; x++)
                    {
                        var pointColor = coverImage.GetPixel(x, y);
                        if (pointColor.A != 0)
                        {
                            bitmap.SetPixel(x, y, backgroudImage.GetPixel(offsetX + x, offsetY + y));
                        }
                    }
                }

                return ImageUtil.ImageToByteArr(bitmap, ImageFormat.Png);
            }
        }
    }
}
