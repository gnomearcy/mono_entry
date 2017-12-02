using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Utils
    {
        public static void ThrowIfNull(object obj)
        {
            if(obj == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
