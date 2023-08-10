using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;

namespace Common.Infrastructure.Repositories.Generic
{
    public class GenericReferenceEntityRepository<TDbContext, TEntity> : GenericRepository<TDbContext, TEntity>, IGenericReferenceEntityRepository<TEntity> where TEntity : ReferenceEntity where TDbContext : DbContext
    {
        public GenericReferenceEntityRepository(GenericDbContext<TDbContext> context, ILogger<GenericReferenceEntityRepository<TDbContext, TEntity>> logger) : base((GenericDbContext<TDbContext>)context, logger)
        {
        }

        async Task<TEntity> IGenericReferenceEntityRepository<TEntity>.GetMatchingStaticEntityFromDb(TEntity entity)
        {
            var matchingEntity = await specificContext.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(re => re.DisplayName.Equals(entity.DisplayName));
            if (matchingEntity == null)
            {
                entity.TrackingState = TrackingState.Added;
                await InsertAsync(entity);
                return entity;
            }
            else
            {
                return matchingEntity;
            }
        }
    }

}
