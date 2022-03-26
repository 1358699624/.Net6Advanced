using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advanced.Net6.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    public class FirstController : ControllerBase
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        // GET: api/<FirstController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<FirstController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="value"></param>
        // POST api/<FirstController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<FirstController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<FirstController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
