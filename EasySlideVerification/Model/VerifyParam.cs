using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Model
{
    /// <summary>
    /// 校验参数
    /// </summary>
    public class VerifyParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// 是否校验成功后移除缓存
        /// </summary>
        public bool RemoveIfSuccess { get; set; }
    }
}
