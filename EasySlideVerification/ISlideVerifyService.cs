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
        /// 验证结果
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Validate(VerifyParam param);
    }
}
