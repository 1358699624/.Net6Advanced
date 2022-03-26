namespace Advanced.NET6.JWTApi.Utity
{
    public static class ResultHelper
    {

        public static ApiResult Success(dynamic data) {
            return new ApiResult
            {
                  code = 200,
                  Data  =data,
                  res = "成功"
             };
        }

        public static ApiResult Error(dynamic data)
        {
            return new ApiResult
            {
                code = 500,
                Data = data,
                res = "失败"
            };
        }
    }
}
