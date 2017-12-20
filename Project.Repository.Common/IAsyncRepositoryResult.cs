using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    /// <summary>
    /// Provides the async wrapping for all types that act as <see cref="IRepositoryResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type that should be wrapped into <see cref="Task"/></typeparam>
    public interface IAsyncRepositoryResult<T>: IRepositoryResult<T> where T: class
    {
        Task<IRepositoryResult<T>> ToTask();
    }
}
