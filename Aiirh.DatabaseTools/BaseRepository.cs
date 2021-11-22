using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiirh.DatabaseTools
{
    public abstract class BaseRepository<TContext> where TContext : DbContext, IHasCurrentUserId
    {
        private DbContextOptions Options { get; }
        protected const int Timeout = 3600;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseRepository(DbContextOptions options)
        {
            Options = options;
            var serviceScope = ServiceActivator.GetScope();
            _httpContextAccessor = (IHttpContextAccessor)serviceScope.ServiceProvider.GetService(typeof(IHttpContextAccessor));
        }

        protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var context = CreateContext();
            var added = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        protected static async Task<TEntity> AddAsync<TEntity>(TEntity entity, TContext context) where TEntity : class
        {
            var added = context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        protected static async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, TContext context) where TEntity : class
        {
            var updated = context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return updated.Entity;
        }

        protected virtual async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            await using var context = CreateContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        protected TContext CreateContext()
        {
            var context = (TContext)Activator.CreateInstance(typeof(TContext), Options) ?? throw new SimpleException("DbContext can't be open");
            context.CurrentUserId = _httpContextAccessor.HttpContext?.User.Identity.Name;
            context.Database.SetCommandTimeout(Timeout);
            return context;
        }
    }
}
