using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Domain.Repositories.EntityFrameworkCore;

public class PolpAbpEfCoreRepository<TDbContext, TEntity> : EfCoreRepository<TDbContext, TEntity, Guid>
    where TDbContext : IEfCoreDbContext
    where TEntity : class, IEntity<Guid>
{
    public PolpAbpEfCoreRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// Performs an operation for adding/removing the child items into/from the parent entity.
    /// </summary>
    /// <typeparam name="TProperty">Typeof the the child item</typeparam>
    /// <param name="id">Parent Id</param>
    /// <param name="navPath">Expression for defining the child items</param>
    /// <param name="func">A lambda function for adding or removing the child items</param>
    /// <param name="autoSave">Auto save flag</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Task</returns>
    /// <exception cref="ArgumentNullException">If the given entity is not found</exception>
    protected async Task AddOrRemoveChildItemsAsync<TProperty>(Guid id,
        Expression<Func<TEntity, TProperty>> navPath,
        Action<TEntity> func,
        bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var context = await GetDbContextAsync();
        var entity = context.Set<TEntity>().Include(navPath).FirstOrDefault(b => b.Id == id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        func(entity);
        if (autoSave)
        {
            await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }
    }

    /// <summary>
    /// Performs an operation for updating the child item
    /// </summary>
    /// <typeparam name="TProperty">Typeof the the child item</typeparam>
    /// <param name="id">Parent Id</param>
    /// <param name="navPath">Expression for defining the child items</param>
    /// <param name="func">A lambda function for adding or removing the child items</param>
    /// <param name="autoSave">Auto save flag</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Task</returns>
    /// <exception cref="ArgumentNullException">If the given entity is not found</exception>
    protected async Task UpdateChildItemAsync<TProperty>(Guid id,
        Expression<Func<TEntity, TProperty>> navPath,
        Action<TEntity> func,
        bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var context = await GetDbContextAsync();
        var entity = context.Set<TEntity>().Include(navPath).FirstOrDefault(b => b.Id == id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        func(entity);
        context.Update(entity);
        if (autoSave)
        {
            await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }
    }

    /// <summary>
    /// Performs an operation for updating the child item and the children of the child items.
    /// </summary>
    /// <typeparam name="TProperty">Typeof the the child item</typeparam>
    /// <typeparam name="TNestedProperty"></typeparam>
    /// <param name="id">Parent Id</param>
    /// <param name="navPath">Expression for defining the child items</param>
    /// <param name="childNavPath"></param>
    /// <param name="func">A lambda function for adding or removing the child items</param>
    /// <param name="autoSave">Auto save flag</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Task</returns>
    /// <exception cref="ArgumentNullException"></exception>
    protected async Task UpdateChildItemExtAsync<TProperty, TNestedProperty>(Guid id,
         Expression<Func<TEntity, List<TProperty>>> navPath,
         Expression<Func<TProperty, List<TNestedProperty>>> childNavPath,
         Action<TEntity> func,
         bool autoSave = false,
         CancellationToken cancellationToken = default)
    {
        var context = await GetDbContextAsync();
        var entity = context.Set<TEntity>().Include(navPath).ThenInclude(childNavPath).FirstOrDefault(b => b.Id == id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        func(entity);
        context.Update(entity);
        if (autoSave)
        {
            await context.SaveChangesAsync(GetCancellationToken(cancellationToken));
        }
    }
}

