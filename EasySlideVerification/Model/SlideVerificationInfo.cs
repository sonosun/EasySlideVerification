using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 背景图片（base64图片数据）
        /// </summary>
        public byte[] BackgroudImage { get; set; }

        /// <summary>
        /// 滑块图片（base64图片数据）
        /// </summary>
        public byte[] SlideImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OffsetY { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationPlainInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 背景图片（base64图片数据）
        /// </summary>
        public string BackgroudImage { get; set; }

        /// <summary>
        /// 滑块图片（base64图片数据）
        /// </summary>
        public string SlideImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// 背景图片高度
        /// </summary>
        public int BgHeight { get; set; }

        /// <summary>
        /// 背景图片宽度
        /// </summary>
        public int BgWidth { get; set; }
    }
}
