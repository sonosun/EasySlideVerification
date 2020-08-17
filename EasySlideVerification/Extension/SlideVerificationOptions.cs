using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly SlideVerificationOptions Default = new SlideVerificationOptions()
        {
            AcceptableDeviation = 5,
        };

        /// <summary>
        /// 背景图
        /// </summary>
        public List<string> BackgroundImages { get; set; }

        /// <summary>
        /// 可接受的误差范围
        /// </summary>
        public int AcceptableDeviation { get; set; }

        /// <summary>
        /// 右边框距离(防止由于太靠近右侧，滑动按钮无法到达）
        /// </summary>
        public int Edge { get; set; }

        /// <summary>
        /// 混淆点数量
        /// </summary>
        public int MixedCount { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expire { get; set; }
    }
}
