using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Models
{
    public class RepoNoOp : IAsyncRepositoryResult<object>
    {
        public RepositoryStatusCodes Code
        {
            get => RepositoryStatusCodes.NO_OP;
        }

        public object RepositoryResult
        {
            get => throw new NullReferenceException("NoOp type contains null result by default.");
            set => throw new FieldAccessException("NoOp type contains no result data.");
        }

        public Task<IRepositoryResult<object>> ToTask()
        {
            return Task.FromResult((IRepositoryResult<object>) new RepoNoOp());
        }
    }
}
