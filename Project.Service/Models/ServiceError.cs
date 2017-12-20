using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ServiceError: IAsyncServiceResult<object>
    {
        public ServiceStatusCode StatusCode
        {
            get => ServiceStatusCode.ERROR;
            set => StatusCode = ServiceStatusCode.ERROR;
        }

        public object ServiceResult
        {
            get => ServiceResult;
            set => ServiceResult = value;
        }

        public Task<IServiceResult<object>> ToTask()
        {
            return Task.FromResult((IServiceResult<object>) new ServiceError());
        }
    }
}
