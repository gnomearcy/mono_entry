using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Models
{
    public class RepoSuccess : IAsyncRepositoryResult<object>
    {
        public RepositoryStatusCodes Code
        {
            get => RepositoryStatusCodes.SUCCESS;
        }

        public object RepositoryResult
        {
            get => throw new InvalidOperationException("This type has no result");
            set => throw new InvalidOperationException("This type has no result.");
        }

        public Task<IRepositoryResult<object>> ToTask()
        {
            return Task.FromResult((IRepositoryResult<object>) new RepoSuccess());
        }
    }
}
