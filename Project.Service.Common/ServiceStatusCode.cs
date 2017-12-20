using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public enum ServiceStatusCode
    {
        /// <summary>
        /// Service successfully performed the action.
        /// </summary>
        SUCCESS = 1,
        
        /// <summary>
        /// Service method produced an error.
        /// </summary>
        ERROR = 2,

        /// <summary>
        /// Service did nothing.
        /// </summary>
        NO_OP = 3
    }
}
