using Advanced.Net6.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.Service
{
    public class CommpayService : BaseService, ICommpayService
    {
        public CommpayService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
