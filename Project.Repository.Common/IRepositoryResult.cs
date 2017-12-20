using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepositoryResult<T> where T : class
    {
        RepositoryStatusCodes Code { get; }

        T RepositoryResult { get; set; }
    }
}
