using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ServiceNoOp : IAsyncServiceResult<object>
    {
        public ServiceStatusCode StatusCode
        {
            get => ServiceStatusCode.NO_OP;
            set => StatusCode = ServiceStatusCode.NO_OP;
        }

        public object ServiceResult
        {
            get => throw new InvalidOperationException("This type has no result");
            set => throw new InvalidOperationException("This type has no result");
        }

        public Task<IServiceResult<object>> ToTask()
        {
            return Task.FromResult((IServiceResult<object>)new ServiceNoOp());
        }
    }
}
