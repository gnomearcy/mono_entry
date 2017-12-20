using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IPaginator<Params, PageResult> where PageResult: class
    {
        Task<IRepositoryResult<PageResult>> GetPageFor(Params p);
    }
}
