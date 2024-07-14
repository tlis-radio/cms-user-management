using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Dtos;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(UserManagementDbContext context)
    : GenericRepository<User>(context), IUserRepository
{
    public override Task<User?> GetByIdAsync(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(x => x.RoleHistory).ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(u => u.RoleHistory)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetUserDetailsById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(u => u.RoleHistory)
                !.ThenInclude(rh => rh.Role)
            .Include(u => u.MembershipHistory)
                !.ThenInclude(mh => mh.Membership)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<PaginationDto<User>> PaginationAsync(int limit, int pageNumber)
    {
        var queryGetTotalCount = await ConfigureTracking(DbSet.AsQueryable(), false).CountAsync();
        
        var pageQuery = ConfigureTracking(DbSet.AsQueryable(), false);

        var page = await pageQuery
            .OrderBy(u => u.Nickname)
            .Include(x => x.MembershipHistory)
                .ThenInclude(x => x.Membership)
            .Include(x => x.RoleHistory)
                .ThenInclude(x => x.Role)
            .Skip(limit * (pageNumber - 1))
            .Take(limit)
            .ToListAsync();
        
        return new PaginationDto<User>
        {
            Total = queryGetTotalCount,
            Limit = limit,
            Page = pageNumber,
            Results = page
        };
    }

    public async Task<List<User>> FilterAsync(List<Guid> ids)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), false);

        return await query
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }
}