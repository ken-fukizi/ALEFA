using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.EF.Core;
//using TrackableEntities.Common.Core;
//using TrackableEntities.EF.Core;

namespace Common.Infrastructure.Repositories.Generic
{
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity> where TEntity : AggregateRoot where TDbContext : DbContext
    {


        protected GenericDbContext<TDbContext> _context;
        internal DbSet<TEntity> _dbSet;
        protected ILogger<GenericRepository<TDbContext, TEntity>> _logger;

        public GenericRepository(GenericDbContext<TDbContext> context, ILogger<GenericRepository<TDbContext, TEntity>> logger)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _logger = logger;
        }

        protected TDbContext specificContext => (TDbContext)((DbContext)_context);

        public virtual IQueryable<TEntity> FullyLoadedQueryable { get; }

        public async Task<IEnumerable<TEntity>> AllFullyLoaded()
        {
            return await FullyLoadedQueryable.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> AllEagerLoaded()
        {
            var notEagerLoaded = await FullyLoadedQueryable.ToListAsync();
            return notEagerLoaded;
        }

        public async Task<IEnumerable<TEntity>> FindFullyLoaded
          (Expression<Func<TEntity, bool>> predicate,
          params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = FullyLoadedQueryable;
            IEnumerable<TEntity> results = await query.Where(predicate).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<TEntity>> FindFullyLoadedBy(Expression<Func<TEntity, bool>> predicate, int? recordLimit = null)
        {
            IEnumerable<TEntity> results;
            if (recordLimit != null)
            {
                results = await FullyLoadedQueryable
                    .Where(predicate).Take((int)recordLimit).ToListAsync();
            }
            else
            {
                results = await FullyLoadedQueryable
                    .Where(predicate).ToListAsync();
            }

            return results;
        }

        public async Task<ICollection<string>> ValidateKey(int? id, string fieldName, bool allowNull)
        {
            var errors = new List<string>();
            if (id == null)
            {
                if (!allowNull)
                {
                    errors.Add($"{fieldName} is null and should contain a key value");
                }
            }
            else
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    errors.Add($"{typeof(TEntity).Name} does not contain the primary key {id} held in {fieldName}");
                }
            }


            return errors;

        }

        public async Task<TEntity> FindFullyLoadedByKey(int id, bool throwErrorIfNotFound = false)
        {
            _logger.LogTrace($"{nameof(FindFullyLoadedByKey)} {typeof(TEntity).Name} ::: Start");
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            var entity = await FullyLoadedQueryable.SingleOrDefaultAsync(lambda);
            if (throwErrorIfNotFound && entity is null)
            {
                //throw new ItemNotFoundByKeyException(id: id.ToString(), itemType: typeof(TEntity).Name);
                throw new Exception("ItemNotFoundByKey");
            }
            else
            {
                return entity;
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            _logger.LogTrace($"{nameof(InsertAsync)} {typeof(TEntity).Name} ::: Start");
            _dbSet.Add(entity);
            await _context.SaveEntitiesAsync();
            _logger.LogTrace($"{nameof(InsertAsync)} {typeof(TEntity).Name} ::: End");
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _logger.LogTrace($"UpdateAsync Entity {typeof(TEntity).Name} ::: Start");
            try
            {
                _context.ApplyChanges(entity);

                await _context.SaveEntitiesAsync();

                // Reset tracking state to unchanged
                _context.AcceptChanges(entity);
                _context.DetachEntities(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //((IGenericRepository<TEntity>)this).DetachFromChangeTracking(entity.Id);
            _logger.LogTrace($"UpdateAsync Entity {typeof(TEntity).Name} ::: End");
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            _logger.LogTrace($"{nameof(UpdateAsync)} {typeof(TEntity).Name} ::: Start");
            try
            {
                _context.ApplyChanges(entities);

                await _context.SaveEntitiesAsync();

                // Reset tracking state to unchanged
                _context.AcceptChanges(entities);
                _context.DetachEntities(entities);
            }
            catch (Exception e)
            {
                throw;
            }

            _logger.LogTrace($"{nameof(UpdateAsync)} {typeof(TEntity).Name} ::: End");
        }

        public void PrintChanges()
        {
            foreach (var entityEntry in _context.ChangeTracker.Entries())
            {
                if (entityEntry.State != EntityState.Unchanged)
                {
                    Console.WriteLine("ENTITY: " + entityEntry.Entity.GetType().Name + "  " + entityEntry.State.ToString());
                    foreach (var entityEntryProperty in entityEntry.Properties)
                    {
                        if (entityEntryProperty.IsModified)
                        {
                            Console.WriteLine("    " + entityEntryProperty.Metadata.Name + " = " + entityEntryProperty.OriginalValue?.ToString() + " ||| " + entityEntryProperty.CurrentValue?.ToString());
                        }
                        else
                        {
                            if (entityEntryProperty.Metadata.Name == "Id")
                            {
                                Console.WriteLine("    " + entityEntryProperty.Metadata.Name + " = " + entityEntryProperty.OriginalValue?.ToString() + " ||| " + entityEntryProperty.CurrentValue?.ToString());
                            }
                        }
                    }
                }
            }
        }

        //void IGenericRepository<TEntity>.DetachFromChangeTracking(TEntity entity)
        //{
        //    _context.DetachEntities(entity);
        //}

        //void IGenericRepository<TEntity>.DetachFromChangeTracking(IEnumerable<TEntity> entities)
        //{
        //    _context.DetachEntities(entities);
        //}


        //public async Task DeleteAsync(int id)
        //{
        //    _logger.LogTrace($"{nameof(DeleteAsync)} {typeof(TEntity).Name} ::: Start" );
        //    var entity = await FindByKey(id);
        //    _dbSet.Remove(entity);
        //    await  _context.SaveEntitiesAsync();
        //    _logger.LogTrace($"{nameof(DeleteAsync)} {typeof(TEntity).Name} ::: End" );
        //}

        //public async Task LoadRelated(Entity entity)
        //{
        //    _logger.LogTrace($"{nameof(LoadRelated)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadRelatedEntitiesAsync(entity);
        //    _logger.LogTrace($"{nameof(LoadRelated)} {typeof(TEntity).Name} ::: End" );
        //}

        //public async Task LoadRelatedAsync(IEnumerable<Entity> entities)
        //{
        //    _logger.LogTrace($"{nameof(LoadRelatedAsync)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadRelatedEntitiesAsync(entities);
        //    _logger.LogTrace($"{nameof(LoadRelatedAsync)} {typeof(TEntity).Name} ::: Start" );
        //}

        //public async Task LoadAggregateAsync(TEntity entity)
        //{
        //    _logger.LogTrace($"{nameof(LoadAggregateAsync)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadAggregate(entity);
        //    _logger.LogTrace($"{nameof(LoadAggregateAsync)} {typeof(TEntity).Name} ::: End" );
        //}

        //public async Task LoadAggregateAsync(IEnumerable<TEntity> entities)
        //{
        //    _logger.LogTrace($"{nameof(LoadAggregateAsync)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadAggregate(entities);
        //    _logger.LogTrace($"{nameof(LoadAggregateAsync)} {typeof(TEntity).Name} ::: End" );
        //}

        //public async Task LoadChildAggregateAsync(Entity entity)
        //{
        //    _logger.LogTrace($"{nameof(LoadChildAggregateAsync)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadAggregate(entity);
        //    _logger.LogTrace($"{nameof(LoadChildAggregateAsync)} {typeof(TEntity).Name} ::: End" );
        //}

        //public async Task LoadChildAggregateAsync(IEnumerable<Entity> entities)
        //{
        //    _logger.LogTrace($"{nameof(LoadChildAggregateAsync)} {typeof(TEntity).Name} ::: Start" );
        //    await _context.LoadAggregate(entities);
        //    _logger.LogTrace($"{nameof(LoadChildAggregateAsync)} {typeof(TEntity).Name} ::: End" );
        //}
    }

}
