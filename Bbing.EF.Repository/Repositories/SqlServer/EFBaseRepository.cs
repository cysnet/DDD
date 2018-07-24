using Bbing.Domain.BaseModel;
using Bbing.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.EF.Repository.Repositories.SqlServer
{
    public class EFBaseRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        public EFUnitOfWork UnitOfWork { get; set; }

        public IQueryable<TEntity> Entities => UnitOfWork.orderdbcontext.Value.Set<TEntity>();

        public int DeleteById(string id)
        {
            var obj = UnitOfWork.orderdbcontext.Value.Set<TEntity>().Find(id);
            if (obj == null)
            {
                return 0;
            }
            UnitOfWork.RegisterDeleted(obj);
            return 1;
        }

        public bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Any(expression);
        }

        public TEntity GetById(string id)
        {
            var ID = Convert.ToInt32(id);
            return Entities.Where(e => e.ID == ID).FirstOrDefault();
        }

        public List<TEntity> GetMany(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).ToList();
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).FirstOrDefault();
        }

        public TResult GetOne<TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector)
        {
            return Entities.Where(expression).Select(selector).FirstOrDefault();
        }

        public List<TResult> GetPageList<TResult, TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, TKey>> sort, int PageIndex, int PageSize)
        {
            return Entities.Where(expression).OrderBy(sort).AsQueryable().Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(selector)
                .ToList();
        }

        public List<TResult> GetPageListDesc<TResult, TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, TKey>> sortDesc, int PageIndex, int PageSize)
        {
            return Entities.Where(expression).OrderByDescending(sortDesc).AsQueryable().Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(selector)
           .ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = false;
            entity.LastModifyTime = DateTime.Now;
            UnitOfWork.RegisterNew<TEntity>(entity);
            return entity;
        }

        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateTime = DateTime.Now;
                entity.LastModifyTime = DateTime.Now;
                entity.IsDelete = false;
            }
            entities.ToList().ForEach(e => { UnitOfWork.RegisterNew<TEntity>(e); });
            return entities;
        }

        public int Update(Expression<Func<TEntity, bool>> expression, params KV[] param)
        {
            throw new NotImplementedException();
        }

        public int DeleteMany(params KV[] param)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity)
        {
            entity.LastModifyTime = DateTime.Now;
            UnitOfWork.RegisterModified(entity);
            return 1;
        }

        public int Commit()
        {
            return UnitOfWork.Commit();
        }
    }
}
