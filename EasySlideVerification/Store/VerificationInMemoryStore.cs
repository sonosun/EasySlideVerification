using EasySlideVerification.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Store
{
    /// <summary>
    /// 内存存储校验数据
    /// </summary>
    public class VerificationInMemoryStore : ISlideVerificationStore
    {
        IMemoryCache store;

        public VerificationInMemoryStore(IMemoryCache store)
        {
            this.store = store;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expire"></param>
        public void Add(SlideVerificationInfo data, TimeSpan expire)
        {
            this.store.Set(data.Key, data, expire);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SlideVerificationInfo Get(string key)
        {
            return this.store.Get<SlideVerificationInfo>(key);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(string key)
        {
            this.store.Remove(key);
        }
    }
}
