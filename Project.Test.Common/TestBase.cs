using Project.WebAPI.App_Start.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class TestBase
    {
        public TestBase()
        {
            try {
                // All test classes run in the same process and all of them require AutoMapper to be configured
                // AutoMapper throws an exception if it's configured more than once per process.
                AutoMapperConfig.Configure();
            }
            catch(InvalidOperationException)
            {
                // Already initialized / configured
            }
        }
    }
}
