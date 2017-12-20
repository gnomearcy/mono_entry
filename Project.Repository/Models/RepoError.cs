using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Models
{
    public class RepoError<T> : IAsyncRepositoryResult<T> where T: class
    {
        public RepositoryStatusCodes Code
        {
            get => RepositoryStatusCodes.ERROR;
        }

        public T RepositoryResult
        {
            get => throw new NullReferenceException("Result is always null for erroneous result. Check Code property for code value.");
            set => throw new FieldAccessException("Result is set to null by default for erroneous result.");
        }

        public Task<IRepositoryResult<T>> ToTask()
        {
            return Task.FromResult((IRepositoryResult<T>) new RepoError<T>());
        }
    }
}
