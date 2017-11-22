using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Repository.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Transactions;
using Project.DAL;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VehicleDbContext Context;

        public UnitOfWork(VehicleDbContext c)
        {
            this.Context = c;
        }

        public Task<int> AddAsync<T>(T entity) where T : class
        {
            DbEntityEntry<T> dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                Context.Set<T>().Add(entity);
            }
            return Task.FromResult(1);
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await Context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public Task<int> DeleteAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Context.Set<T>().Attach(entity);
                Context.Set<T>().Remove(entity);
            }
            return Task.FromResult(1);
        }

        public Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            var entity = Context.Set<T>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteAsync<T>(entity);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public Task<int> UpdateAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            return Task.FromResult(1);
        }
    }
}
