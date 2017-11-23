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
        Task<ICollection<T>> GetAll();

        Task<T> GetById(Key id);

        Task<int> Insert(T model);

        Task<int> Update(T model);

        Task<int> Delete(T model);
    }
}
