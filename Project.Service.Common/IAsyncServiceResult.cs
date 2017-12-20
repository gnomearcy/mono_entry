using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IAsyncServiceResult<T> : IServiceResult<T> where T: class
    {
        /// <summary>
        /// Provides the async wrapping for all types that act as <see cref="IServiceResult{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type that should be wrapped into <see cref="Task"/></typeparam>
        Task<IServiceResult<T>> ToTask();
    }
}
