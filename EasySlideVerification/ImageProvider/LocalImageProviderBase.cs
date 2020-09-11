using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    public abstract class LocalImageProviderBase
    {
        //图片名称
        abstract protected string ImageNameFormat { get; }

        /// <summary>
        /// 随机数
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// 图片列表
        /// </summary>
        List<byte[]> Images = new List<byte[]>();

        /// <summary>
        /// 
        /// </summary>
        public LocalImageProviderBase()
        {
            this.Load();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetRandomOne()
        {
            int index = random.Next(this.Images.Count);
            return this.Images[index];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            string workDir = Directory.GetCurrentDirectory();
            string imageDir = $"{workDir}\\App_Data\\Images\\Slide";
            if (!Directory.Exists(imageDir))
            {
                throw new DirectoryNotFoundException($"图片路径：{imageDir} 不存在.");
            }

            string[] files = Directory.GetFiles(imageDir, ImageNameFormat);
            for (int i = 0; i < files.Length; i++)
            {
                using (Stream stream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                {
                    byte[] image = new byte[stream.Length];
                    stream.Read(image, 0, image.Length);
                    this.Images.Add(image);
                }
            }
        }
    }
}
