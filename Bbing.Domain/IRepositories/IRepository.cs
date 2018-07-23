using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.BaseModel;

namespace Bbing.Domain.IRepositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        #region 属性
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region 公共方法

        /// <summary>
        /// 插入一个
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(string id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> expression, params KV[] param);

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(string id);

        /// <summary>
        /// 查询多个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        List<TEntity> GetMany(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        TResult GetOne<TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// 物理删除多个
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int DeleteMany(params KV[] param);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<TEntity, bool>> expression);

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
        List<TResult> GetPageList<TResult, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> selector, Func<TEntity, TKey> sort, int PageIndex, int PageSize);

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
        List<TResult> GetPageListDesc<TResult, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> selector, Func<TEntity, TKey> sortDesc, int PageIndex, int PageSize);

        #endregion
    }
}
