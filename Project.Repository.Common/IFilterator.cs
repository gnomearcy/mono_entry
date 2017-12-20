using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IFilterator<FilterParams, FilterResult> where FilterResult : class
    {
        Task<IRepositoryResult<FilterResult>> FilterFor(FilterParams payload);
    }
}
