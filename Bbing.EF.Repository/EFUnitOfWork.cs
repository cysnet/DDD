using Bbing.Domain.BaseModel;
using Bbing.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bbing.EF.Repository
{
    public class EFUnitOfWork : IUnitOfWorkRepositoryContext
    {
        public bool IsCommitted { get; set; }

        // public BbingContext orderdbcontext = new BbingContext();
        public ThreadLocal<BbingContext> orderdbcontext = new ThreadLocal<BbingContext>(() => new BbingContext());
        public int Commit()
        {
            var result = 0;
            if (!IsCommitted)
                result = orderdbcontext.Value.SaveChanges();
            IsCommitted = true;
            return result;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            orderdbcontext.Dispose();
        }

        public void RegisterDeleted<TEntity>(TEntity obj) where TEntity : AggregateRoot
        {
            orderdbcontext.Value.Set<TEntity>().Remove(obj);
            IsCommitted = false;
        }

        public void RegisterModified<TEntity>(TEntity obj) where TEntity : AggregateRoot
        {
            orderdbcontext.Value.Entry<TEntity>(obj).State = EntityState.Modified;
            IsCommitted = false;
        }

        public void RegisterNew<TEntity>(TEntity obj) where TEntity : AggregateRoot
        {
            orderdbcontext.Value.Set<TEntity>().Add(obj);
            IsCommitted = false;
        }

        public void Rollback()
        {
            IsCommitted = false;
        }
    }
}
