using EasySlideVerification.Store;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification
{
    /// <summary>
    /// 
    /// </summary>
    public static class SlideVerificationExtension
    {
        /// <summary>
        /// 注册校验模块
        /// </summary>
        /// <param name="services"></param>
        public static void AddSlideVerification(this IServiceCollection services, Action<SlideVerificationOptions> options = null)
        {
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddSingleton(typeof(ISlideVerifyService), typeof(SlideVerifyService));
            services.AddSingleton(typeof(ISlideVerificationStore), typeof(VerificationInMemoryStore));

            if (options != null)
            {
                options.Invoke(SlideVerificationOptions.Default);
            }
        }

        /// <summary>
        /// 注册校验模块,使用Redis存储
        /// </summary>
        /// <param name="services"></param>
        public static void AddSlideVerification(this IServiceCollection services, Action<SlideVerificationRedisOptions> redisOptions, Action<SlideVerificationOptions> options = null)
        {
            services.AddHttpClient();
            services.AddSingleton(typeof(ISlideVerifyService), typeof(SlideVerifyService));
            services.AddSingleton(typeof(ISlideVerificationStore), typeof(VerificationInRedisStore));

            if (redisOptions != null)
            {
                redisOptions.Invoke(SlideVerificationRedisOptions.Default);
            }

            if (options != null)
            {
                options.Invoke(SlideVerificationOptions.Default);
            }
        }
    }
}
