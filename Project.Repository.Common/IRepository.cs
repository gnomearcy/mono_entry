using Project.DAL;
using Project.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T model);

        void Update(T model);

        void Delete(T id);
    }
}
