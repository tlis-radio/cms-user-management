using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Entities.Base;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected readonly UserManagementDbContext Context;

    public GenericRepository(UserManagementDbContext context)
    {
        DbSet = context.Set<TEntity>();
        Context = context;
    }

    public Task<TEntity?> GetByIdAsync(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return await query.Where(predicate).ToListAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async ValueTask<bool> DeleteByIdAsync(Guid id)
    {
        var toDelete = await DbSet.FindAsync(id);
        if (toDelete is null)
            return false;

        DbSet.Remove(toDelete);
        return true;
    }

    public bool Delete(TEntity toDelete)
    {
        DbSet.Remove(toDelete);
        return true;
    }

    public bool Delete(IEnumerable<TEntity>? toDelete)
    {
        if (toDelete is null)
            return true;

        DbSet.RemoveRange(toDelete);
        return true;
    }

    protected IQueryable<TEntity> ConfigureTracking(IQueryable<TEntity> query, bool asTracking)
    {
        if (asTracking)
        {
            return query.AsTracking();
        }

        return query.AsNoTracking();
    }
}