using Advanced.Net6.Interface;
using Advanced.Net6.Interface.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.Service.IOC
{
    public class Headphone:IHeadphone
    {

        public Headphone(IPower  headphone) 
        {
            Console.WriteLine($"{this.GetType().Name} 被构造了");
        }
    }
}
