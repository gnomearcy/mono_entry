using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ServiceSuccess : IAsyncServiceResult<object>
    {
        public ServiceStatusCode StatusCode
        {
            get => ServiceStatusCode.SUCCESS;
            set => StatusCode = ServiceStatusCode.SUCCESS;
        }

        public object ServiceResult
        {
            get => throw new InvalidOperationException("This type does not have result data");
            set => throw new InvalidOperationException("This type does not allow result data");
        }

        public async Task<IServiceResult<object>> ToTask()
        {
            return await Task.FromResult((IServiceResult<object>)new ServiceSuccess());
        }
    }
}
