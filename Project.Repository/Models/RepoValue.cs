using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Models
{
    public class RepoValue<T> : IAsyncRepositoryResult<T> where T: class
    {
        public RepositoryStatusCodes Code
        {
            get => RepositoryStatusCodes.SUCCESS;
        }

        /// <summary>
        /// Including backing field to prevent StackOverflow exception when setting the value to itself
        /// </summary>
        private T Data;
        public T RepositoryResult
        {
            get => Data;
            set => Data = value;
        }

        /// <summary>
        /// Factory method that wraps this type into a <see cref="Task"/> to return from <code async/> marked methods.
        /// The purpose of this factory is to centralize the responsibility of creating a <see cref="ValueResult"/> type wrapped into a <see cref="Task"/>
        /// into a single place because a cast to the implementing interface is required to satisfy the compiler.
        /// Otherwise, the compiler reports an error that <see cref="ValueResult"/> cannot be converted to <see cref="IRepositoryResult{T}"/> when the
        /// <see cref="IRepositoryResult{T}"/> is the generic type parameter of a <see cref="Task"/>.
        /// </summary>
        public Task<IRepositoryResult<T>> ToTask()
        {
            return Task.FromResult((IRepositoryResult<T>)new RepoValue<T>
            {
                RepositoryResult = this.RepositoryResult
            });
        }
    }
}
