namespace Snippet.Micro.Common.Models
{
    public static class CommonResultExtension
    {
        #region 生成CommonResult

        /// <summary>
        /// 生成成功消息
        /// </summary>
        public static CommonResult Success(string message = "", string code = "")
        {
            return new CommonResult
            {
                IsSuccess = true,
                Code = code,
                Message = message
            };
        }

        /// <summary>
        /// 生成失败消息
        /// </summary>
        public static CommonResult Fail(string message, string code = "")
        {
            return new CommonResult
            {
                IsSuccess = false,
                Code = code,
                Message = message
            };
        }

        #endregion 生成CommonResult

        #region 生成泛型CommonResult

        /// <summary>
        /// 生成成功消息(不带消息内容)
        /// </summary>
        public static CommonResult<TData> SuccessResult<TData>(TData data, string message = "",
            string code = "")
            where TData : class
        {
            return new CommonResult<TData>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                Code = code
            };
        }

        #endregion 生成泛型CommonResult
    }
}
