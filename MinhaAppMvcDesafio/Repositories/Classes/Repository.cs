using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MinhaAppMvcDesafio.Models;
using Microsoft.EntityFrameworkCore;
using MinhaAppMvcDesafio.Data;
using MinhaAppMvcDesafio.Repositories.Interfaces;

namespace MinhaAppMvcDesafio.Repositories.Classes
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly Contexto contexto;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly int qtdPaginas;
        protected int pageSize;

        protected Repository(Contexto db)
        {
            contexto   = db;
            DbSet      = db.Set<TEntity>();
            pageSize   = 1;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> ObterPaginados(int pageIndex)
        {
            return await DbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await contexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            contexto?.Dispose();
        }
    }
}