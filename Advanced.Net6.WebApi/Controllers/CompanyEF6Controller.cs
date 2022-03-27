using Advanced.Net6.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityFormworkCore6;
using EntityFormworkCore6.Models;
using Advanced.Net6.Service;
using System.Linq.Expressions;
using Advanced.Net6.Interface;

namespace Advanced.Net6.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CompanyEF6Controller : ControllerBase
    {
        private readonly ICommpayService _commpayService;
        private readonly ILogger<CompanyEF6Controller> _logger;

        public CompanyEF6Controller(ILogger<CompanyEF6Controller> logger, ICommpayService commpayService)
        {
            this._logger = logger;
            _commpayService = commpayService;
        }

        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V4))]
        [HttpGet()]
        public string Get()
        {
            {
                using (CustomerDbContext customerDb = new CustomerDbContext())
                {
                    Company company = new Company
                    {
                        Name = "string>",
                        CreateTime = DateTime.Now,
                        CreatorId = 55

                    };

                    customerDb.Add<Company>(company);
                    customerDb.SaveChanges();

                    Company company1 = customerDb.Companies.OrderByDescending(c => c.Id).FirstOrDefault();
                    company1.Name = "tdk";

                    customerDb.Update(company);
                    customerDb.SaveChanges();


                    customerDb.Remove<Company>(company);
                    customerDb.SaveChanges();

                }
            }

            return "";
        }

        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V4))]
        [HttpGet()]
        public async Task<string> Get2() {
            {
                using (CustomerDbContext customerDb = new CustomerDbContext())
                {
                    Company company = new Company
                    {
                        Name = "string>",
                        CreateTime = DateTime.Now,
                        CreatorId = 55

                    };

                    await customerDb.AddAsync(company);
                    await customerDb.SaveChangesAsync();
                }
            }

            return "";
        }


        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V3))]
        [AllowAnonymous]
        [HttpGet()]
        public object QueryPage()
        {
            try
            {
                int pageSize = 10;
                int pageindex = 1;
                Expression<Func<Company, bool>> expression = c => true;

                expression = c => c.CreatorId == 1 && c.CreateTime.ToString().Contains("19");
                //expression = c => c.CreatorId == 1;
                var company = _commpayService.QueryPage<Company, int>(expression, pageSize, pageindex, c => c.Id);
                _logger.LogDebug("this is  LogDebug");
                _logger.LogInformation("this is  LogInformation");
                _logger.LogWarning("this is  LogWarning");
                _logger.LogError("this is  LogError");
                _logger.LogTrace("this is  LogTrace");
                _logger.LogCritical("this is  LogCritical");
                _logger.LogInformation("执行查询语句");
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
           
        }
    }
}
