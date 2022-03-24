using Advanced.Net6.Interface;
using Advanced.Net6.Interface.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.Service.IOC
{
    public class Power:IPower
    {
        IMircPhone imircPhone;


        public Power(IMircPhone mircPhone) 
        {
            Console.WriteLine($"{this.GetType().Name} 被构造了");
            this. imircPhone = mircPhone;
        }

        public Power(IMircPhone mircPhone, IMircPhone mircPhone2)
        {
            Console.WriteLine($"{this.GetType().Name} 被构造了");
            this.imircPhone = mircPhone2;
        }

    }
}
