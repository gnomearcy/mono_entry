using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class ServiceValue<T> : IAsyncServiceResult<T> where T : class
    {
        public ServiceStatusCode StatusCode
        {
            get => ServiceStatusCode.SUCCESS;
        }

        private T Data;
        public T ServiceResult
        {
            get => Data ?? throw new NullReferenceException("Cannot read value that is not set");
            set => Data = value ?? throw new ArgumentNullException("This type does not accept null value");
        }

        public Task<IServiceResult<T>> ToTask()
        {
            return Task.FromResult((IServiceResult<T>)new ServiceValue<T>
            {
                ServiceResult = this.ServiceResult
            });
        }
    }
}
