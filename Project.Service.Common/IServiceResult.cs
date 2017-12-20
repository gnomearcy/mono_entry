using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    /// <summary>
    /// Generic interface that has to be implemented by concrete types which will be returned from <see cref="IVehicleService"/> methods.
    /// This type serves the same semantic function as <see cref="Tuple"/>, but gives the ability to provide concrete types and better semantic 
    /// code readability with descriptive property names.
    /// </summary>
    /// <typeparam name="T">Type of a return value returned from a service method</typeparam>
    public interface IServiceResult<T> where T : class
    {
        ServiceStatusCode StatusCode { get; }

        T ServiceResult { get; set; }
    }
}
