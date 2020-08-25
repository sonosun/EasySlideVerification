using EasySlideVerification.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification
{
    /// <summary>
    /// 滑块校验
    /// </summary>
    public interface ISlideVerifyService
    {
        /// <summary>
        /// 创建图片滑动数据(图片以byte数组格式返回)
        /// </summary>
        SlideVerificationInfo Create();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool Validate(string key, int x, int y);
    }
}
