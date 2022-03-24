using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.Interface.IOC
{
    public interface IPhone
    {
        IMircPhone Microphone { get; set; }
        IHeadphone Headphone { get; set; }
        IPower Power { get; set; }

        void Queue();

        object Out(string name);

    }
}
