using Advanced.Net6.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityFormworkCore6;
using EntityFormworkCore6.Models;

namespace Advanced.Net6.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CompanyEF6Controller : ControllerBase
    {

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

                    Company  company1  = customerDb.Companies.OrderByDescending(c => c.Id).FirstOrDefault();
                    company1.Name = "tdk";

                    customerDb.Update(company);
                    customerDb.SaveChanges();


                    customerDb.Remove<Company>(company);
                    customerDb.SaveChanges();

                }
            }

            return "";
        }

        [ApiExplorerSettings(GroupName= nameof(ApiVersionInfo.V4))]
        [HttpGet()]
        public async  Task<string> Get2() {
            {
                using (CustomerDbContext customerDb = new CustomerDbContext ())
                {
                    Company company = new Company
                    {
                        Name = "string>",
                         CreateTime = DateTime.Now,
                             CreatorId =   55
                                
                    };    

                     await  customerDb.AddAsync(company);
                     await  customerDb.SaveChangesAsync();              
                }
            }
            
            return "";
        }
    }
}
