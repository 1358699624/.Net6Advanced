using Advanced.Net6.Interface;
using Advanced.Net6.Interface.IOC;
using AutofacAttribue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.Service.IOC
{
    public class Phone : IPhone
    {
        [CusotmSelectAttribute]
        public IMircPhone Microphone { get; set; }

        public IHeadphone Headphone { get; set; }
        public IPower Power { get; set; }

        public Phone(IHeadphone headphone)
        {
            this.Headphone = headphone;

            Console.WriteLine($"{this.GetType().Name}被构造了{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void Queue()
        {
            Console.WriteLine($"方法");
        }

        public object Out(string name)
        {
            return new
            {
                name = name,
                datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                getType = this.GetType().Name
            };
        }
    }
}
