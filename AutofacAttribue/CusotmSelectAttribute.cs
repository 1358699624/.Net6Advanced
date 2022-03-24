using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAttribue
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CusotmSelectAttribute: Attribute
    {
    }
}
