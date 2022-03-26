using Advanced.NET6.JWTApi.Utity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Advanced.NET6.JWTApi.Controllers
{
    /// <summary>
    /// jwt
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class JWTController : ControllerBase
    {  
        /// <summary>
        /// aaa
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet()]
        public string  GetTokenJWT(string Name,string pwd) 
        {
            try
            {
                if (Name.Equals("mlj") && pwd.Equals("1"))
                {

                    var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, Name),
                    new Claim("Name", pwd) //不能放敏感信息 
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));//密钥16位
                    //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                    var token = new JwtSecurityToken(
                        issuer: "http://localhost:5020",
                        audience: "http://localhost:5029",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddHours(1),//1小时过期
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                    #region .NET6返回ApiResult数据获取不到问题

                    //{
                    //    //返回 ApiResult
                    //    return ResultHelper.Success(jwtToken);
                    //}
                    #endregion

                    return jwtToken;
                }
                else
                {
                    return "失败";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 222
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<string> GetTokenJWT2(string Name, string pwd)
        {
            try
            {

                return new string[] { "value1", "value2" }; ;
               
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message }; ;
            }
        }
    }
}
