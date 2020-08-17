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
        /// 创建图片滑动数据
        /// </summary>
        SlideVerificationInfo Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool Validate(string id, int x, int y);
    }
}
