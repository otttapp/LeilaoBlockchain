using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Interfaces;
using TesteAplicacao.Infraestructure.Procedure;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MainDBContext db;

        private static readonly string _nomeEntidade = typeof(TEntity).GetCustomAttribute<TableAttribute>()?.Name.Replace('_', ' ') ?? typeof(TEntity).Name;
        private static readonly string _nomeEntidadeUpper = (typeof(TEntity).GetCustomAttribute<TableAttribute>()?.ToString() ?? typeof(TEntity).Name).ToUpper();

        public RepositoryBase(MainDBContext _db)
        {
            db = _db;
        }
        public virtual async Task<TEntity> GetByIdThrowsIfNull(uint id) => await GetByIdThrowsIfNull<uint>(id);

        private async Task<TEntity> GetByIdThrowsIfNull<T>(uint id, string? mensagem = null)
        {
            mensagem = String.IsNullOrWhiteSpace(mensagem) ? $"É necessário informar {_nomeEntidade}." : mensagem;

            return await GetById(id) ?? throw new BusinessException($"{_nomeEntidadeUpper}_INEXISTENTE", mensagem, $"ID = {id}");
        }

        public virtual async Task Add(TEntity obj)
        {
            db.Add(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task AddRange(List<TEntity> obj)
        {
            db.AddRange(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task RemoveRange(List<TEntity> obj)
        {
            db.RemoveRange(obj);
            await db.SaveChangesAsync();
        }

        public virtual TEntity AddTest(TEntity obj)
        {
            db.Add(obj);
            db.SaveChanges();
            return obj;
        }

        //public virtual async Task<IEnumerable<TEntity>> SQL(string sql)
        //{
        //    return await db.Set<TEntity>().FromSqlRaw(sql).ToListAsync();
        //}

        //public virtual async void ExecuteQuery(string sql)
        //{
        //    db.Database.ExecuteSqlRaw(sql);
        //}

        //public async Task ExecuteQueryAsync(string sql)
        //{
        //    await db.Database.ExecuteSqlRawAsync(sql);
        //}

        public async Task<int> CountAsync()
        {
            return await db.Set<TEntity>().CountAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> expression)
        {
            var query = db.Set<TEntity>().Where(expression);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            var query = db.Set<TEntity>().Where(filterExpression);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TEntity>> select)
        {
            var query = db.Set<TEntity>().Where(filterExpression).Select(select);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TEntity>> select,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            var query = db.Set<TEntity>().Where(filterExpression).Select(select);
            return await orderBy(query).ToListAsync();
        }

        public virtual async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression)
        {
            var query = db.Set<TEntity>().Where(expression);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, TEntity>> select)
        {
            return await db.Set<TEntity>().Select(select).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, TEntity>> select, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await orderBy(db.Set<TEntity>().Select(select)).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(uint id)
        {
            var entity = await db.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new BusinessException("ENTIDADE_NAO_ENCONTRADA", "Entidade não encontrada.");
            }

            return entity;
        }

        public virtual async Task Remove(TEntity obj)
        {
            db.Set<TEntity>().Remove(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task Remove(List<TEntity> obj)
        {
            db.Set<TEntity>().RemoveRange(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public virtual async Task Update(List<TEntity> obj)
        {
            db.UpdateRange(obj);
            await db.SaveChangesAsync();
        }

        public virtual async Task UpdateFields(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate)
        {
            db.Attach(obj);
            foreach (var property in propertiesToUpdate)
            {
                db.Entry(obj).Property(property).IsModified = true;
            }
            await db.SaveChangesAsync();
        }

        //public virtual async Task<IEnumerable<TStoredProcedureResult>> ExecuteStoredProcedure<TStoredProcedureResult>(string sqlCommand, params object[] parameters) where TStoredProcedureResult : class, IProcedureResult
        //{
        //    return await db.Set<TStoredProcedureResult>().FromSqlRaw(sqlCommand, parameters).ToListAsync();
        //}

        public IQueryable<TEntity> GetQuery()
        {
            return db.Set<TEntity>().AsQueryable();
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<IEnumerable<TEntity>> SQL(string sql)
        {
            throw new NotImplementedException();
        }

        public void ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteQueryAsync(string sql)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TStoredProcedureResult>> IRepositoryBase<TEntity>.ExecuteStoredProcedure<TStoredProcedureResult>(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}


