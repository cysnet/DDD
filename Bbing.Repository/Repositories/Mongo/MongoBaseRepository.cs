using Bbing.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.BaseModel;
using Bbing.Infrastructure;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Bbing.Repository.Repositories.Mongo
{
    public class MongoBaseRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        public MongoBaseRepository()
        {

        }
        static MongoBaseRepository()
        {
            Environment = "Environment".GetAppSetting();
            _connStr = $"MongoUrl.{Environment}".GetAppSetting();
            _dbName = $"MongoDb.{Environment}".GetAppSetting();
        }

        public static string Environment ;

        private static string _connStr ;

        private static string _dbName ;
        private static MongoUrl url => new MongoUrl(_connStr);
        private static MongoClient client => new MongoClient(url);
        public static IMongoDatabase db => client.GetDatabase(_dbName);
        public string Name => typeof(TEntity).Name;
        public IMongoCollection<TEntity> Collection => db.GetCollection<TEntity>(Name);
        public IQueryable<TEntity> Entities => Collection.AsQueryable();

        public FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;

        public UpdateDefinitionBuilder<TEntity> UpdateBuilder => Builders<TEntity>.Update;

        public ProjectionDefinitionBuilder<TEntity> Projection => Builders<TEntity>.Projection;


        public virtual int DeleteById(string id)
        {
            return Convert.ToInt32(Collection.DeleteOne(Filter.Eq(e => e.ObjId, id)).DeletedCount);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            entity.ObjId = ObjectId.GenerateNewId().ToString();
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = false;
            entity.LastModifyTime= DateTime.Now;
            Collection.InsertOne(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ObjId = ObjectId.GenerateNewId().ToString();
                entity.CreateTime = DateTime.Now;
                entity.LastModifyTime = DateTime.Now;
                entity.IsDelete = false;
            }
            Collection.InsertMany(entities);
            return entities;
        }

        public virtual int Update(Expression<Func<TEntity, bool>> expression, params KV[] param)
        {
            var list = new List<UpdateDefinition<TEntity>>();
            param.ToList().ForEach(e => {
                list.Add(UpdateBuilder.Set(e.key, e.value));
            });
            list.Add(UpdateBuilder.Set(e=>e.LastModifyTime, DateTime.Now));
            var result = Collection.UpdateOne(expression, UpdateBuilder.Combine(list));
            return Convert.ToInt32(result.ModifiedCount);
        }

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="MongoId"></param>
        /// <returns></returns>
        public virtual TEntity GetById(string id)
        {
            return Entities.FirstOrDefault(e => e.ObjId == id);
        }

        /// <summary>
        /// 查询多个
        /// </summary>
        /// <param name="MongoId"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetMany(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression)?.ToList();
        }

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="MongoId"></param>
        /// <returns></returns>
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).FirstOrDefault();
        }


        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual TResult GetOne<TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector)
        {
            return Entities.Where(expression).Select(selector).FirstOrDefault();
        }

        /// <summary>
        /// 物理删除多个
        /// </summary>
        public virtual int DeleteMany(params KV[] param)
        {
            if (param == null || param.Count() == 0)
            {
                throw new Exception("删除条件不可为null");
            }
            var list = new List<FilterDefinition<TEntity>>();
            param.ToList().ForEach(e => {
                list.Add(Filter.Eq(e.key, e.value));
            });
            var result = Collection.DeleteMany(Filter.And(list));
            return Convert.ToInt32(result.DeletedCount);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            return Collection.AsQueryable().Any(expression);
        }

        /// <summary>
        /// 正序分页获取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <param name="selector"></param>
        /// <param name="sort"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<TResult> GetPageList<TResult, TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector, Func<TEntity, TKey>sort, int PageIndex, int PageSize)
        {
            return Collection.AsQueryable().Where(expression).OrderBy(sort).AsQueryable().Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(selector)
                .ToList();
        }

        /// <summary>
        /// 倒序分页获取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <param name="selector"></param>
        /// <param name="sortDesc"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<TResult> GetPageListDesc<TResult, TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector, Func<TEntity, TKey> sortDesc, int PageIndex,
            int PageSize)
        {
            return Collection.AsQueryable().Where(expression).OrderByDescending(sortDesc).AsQueryable().Skip((PageIndex - 1) * PageSize).Take(PageSize).Select(selector)
                .ToList();
        }
    }
}
