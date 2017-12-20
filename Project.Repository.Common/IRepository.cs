using Project.DAL;
using Project.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepository<T, in Key> where T: class
    {
        Task<IRepositoryResult<ICollection<T>>> GetAll();

        Task<IRepositoryResult<T>> GetById(Key id);

        Task<IRepositoryResult<object>> Insert(T model);

        Task<IRepositoryResult<object>> Update(T model);

        Task<IRepositoryResult<object>> Delete(T model);
    }
}
