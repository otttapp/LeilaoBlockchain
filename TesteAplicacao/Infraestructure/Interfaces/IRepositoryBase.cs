using Cuca_Api.Infraestructure.Procedure;
using System.Linq.Expressions;

namespace Cuca_Api.Infraestructure.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);

        Task AddRange(List<TEntity> obj);

        Task<TEntity> GetById(uint id);

              //Task<IEnumerable<TEntity> > GetAll(); //falar com gabriel

              //Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, TEntity>> fields); //falar com gabriel

              //Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, TEntity>> fields, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);    //falar com gabriel

        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> SQL(string sql);

        void ExecuteQuery(string sql);

        Task ExecuteQueryAsync(string sql);

        Task<int> CountAsync();

        Task Update(TEntity obj);

        Task Update(List<TEntity> obj);

        Task UpdateFields(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate);

        Task Remove(TEntity obj);

        Task Remove(List<TEntity> obj);

        Task<IEnumerable<TStoredProcedureResult>> ExecuteStoredProcedure<TStoredProcedureResult>(string sqlCommand, params object[] parameters) where TStoredProcedureResult : class, IProcedureResult;

        IQueryable<TEntity> GetQuery();

    }
}
