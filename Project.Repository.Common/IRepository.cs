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
        IEnumerable<T> GetAll();

        T GetById(Key id);

        void Insert(T model);

        void Update(T model);

        void Delete(T model);
    }
}
