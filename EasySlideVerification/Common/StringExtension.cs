using System;
using System.Linq;
using System.Text;

namespace EasySlideVerification.Common
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        /// <summary>
        /// 是否非空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        /// <summary>
        /// 转换为decimal类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string val)
        {
            decimal result = 0;
            if (!string.IsNullOrEmpty(val))
            {
                decimal.TryParse(val, out result);
            }

            return result;
        }

        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this string val)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(val))
            {
                int.TryParse(val, out result);
            }

            return result;
        }

        /// <summary>
        /// 转换为long类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long ToLong(this string val)
        {
            long result = 0;
            if (!string.IsNullOrEmpty(val))
            {
                long.TryParse(val, out result);
            }

            return result;
        }

        /// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string val)
        {
            DateTime result = DateTime.MinValue;
            if (!string.IsNullOrEmpty(val))
            {
                DateTime.TryParse(val, out result);
            }

            return result;
        }

        /// <summary>
        /// 转换为Base64编码字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToBase64Str(this string val)
        {
            byte[] urlByteArr = Encoding.UTF8.GetBytes(val);
            return Convert.ToBase64String(urlByteArr);
        }

        /// <summary>
        /// Base64解码字符串
        /// </summary>
        /// <param name="val"></param>
        public static string FromBase64Str(this string val)
        {
            var urlByteArr = Convert.FromBase64String(val);
            return Encoding.UTF8.GetString(urlByteArr);
        }

        /// <summary>
        /// 字符串模板填充
        /// </summary>
        /// <param name="format"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, object param)
        {
            return string.Format(format, param);
        }

        /// <summary>
        /// 字符串模板填充
        /// </summary>
        /// <param name="format"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] param)
        {
            return string.Format(format, param);
        }

        /// <summary>
        /// 混淆替换字符串,如银行账户
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Mix(this string val)
        {
            string mixedWord = "*";
            string result = string.Empty;
            if (val.IsNotEmpty())
            {
                if (val.Length == 1)
                {
                    result = mixedWord;
                }
                else if (val.Length == 2)
                {
                    char[] arr = val.ToArray();
                    result = arr[0] + mixedWord;
                }
                else if (val.Length <= 5)
                {
                    result = MixWord(val, 1, mixedWord);
                }
                else if (val.Length <= 10)
                {
                    result = MixWord(val, 2, mixedWord);
                }
                else
                {
                    result = MixWord(val, 4, mixedWord);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val">待处理字符串</param>
        /// <param name="showCount">在字符串两端，可见的字符数.</param>
        /// <param name="mixedWord">用于隐藏替代的字符串.例如：*</param>
        /// <returns></returns>
        private static string MixWord(string val, int showCount, string mixedWord)
        {
            char[] arr = val.ToArray();
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < showCount || i >= arr.Length - showCount)
                {
                    content.Append(arr[i]);
                }
                else
                {
                    //防止字符串过长
                    if (content.Length < 12)
                    {
                        content.Append(mixedWord);
                    }
                }
            }
            return content.ToString();
        }
    }
}
