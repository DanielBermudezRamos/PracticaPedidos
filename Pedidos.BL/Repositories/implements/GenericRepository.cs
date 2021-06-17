using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pedidos.BL.Data;

namespace Pedidos.BL.Repositories.implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PedidosDBContext pedidoContext;

        public GenericRepository(PedidosDBContext pedidoContext)
        {
            this.pedidoContext = pedidoContext;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new Exception("Esta Entidad es Nula");

            pedidoContext.Set<TEntity>().Remove(entity);
            await pedidoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await pedidoContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await pedidoContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            pedidoContext.Set<TEntity>().Add(entity);
            await pedidoContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            //pedidoContext.Entry(entity).State = EntityState.Modified;
            pedidoContext.Set<TEntity>().Update(entity);
            await pedidoContext.SaveChangesAsync();
            return entity;
        }
    }
}
