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
        /// 由于一个验证可能需要进行两次校验，所以，第一次验证成功不能移除缓存数据，第二次验证成功后移除缓存数据，防止前端反复使用一个验证数据
        /// </summary>
        public bool RemoveIfSuccess { get; set; }
    }
}
