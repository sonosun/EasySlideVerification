using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationRedisOptions
    {
        public static readonly SlideVerificationRedisOptions Default = new SlideVerificationRedisOptions();

        /// <summary>
        /// redis链接，如：127.0.0.1:6379
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// redis库索引(默认为：0)
        /// </summary>
        public int DatabaseIndex { get; set; }

        /// <summary>
        /// redis缓存key前缀，起到对缓存项分组的作用。可以为空，但建议使用 slide:
        /// </summary>
        public string KeyPrefix { get; set; }
    }
}
