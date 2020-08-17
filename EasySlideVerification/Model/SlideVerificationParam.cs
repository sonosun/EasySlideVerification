using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationParam
    {
        /// <summary>
        /// 背景图片原图
        /// </summary>
        public byte[] BackgroundImage { get; set; }

        /// <summary>
        /// 滑块图
        /// </summary>
        public byte[] SlideImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Edge { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MixedCount { get; set; }
    }
}
