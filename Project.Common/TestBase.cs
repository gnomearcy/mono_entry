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
                AutoMapperConfig.Configure();
            }
            catch(System.InvalidOperationException e)
            {
                // Already initialized
            }
        }
    }
}
