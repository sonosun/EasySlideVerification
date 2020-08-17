using EasySlideVerification.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Store
{
    /// <summary>
    /// 校验数据保存接口
    /// </summary>
    public interface ISlideVerificationStore
    {
        /// <summary>
        /// 保存
        /// </summary>
        void Add(SlideVerificationInfo data, TimeSpan expire);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        SlideVerificationInfo Get(string key);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void Remove(string key);
    }
}
