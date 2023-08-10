using Common.Infrastructure.DomainEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repositories.Generic
{
    public class GenericDbContext<T> : DbContext, IUnitOfWork where T : DbContext
    {
        private readonly IMediator _mediator;

        public GenericDbContext(DbContextOptions<T> options, IMediator mediator) : base((DbContextOptions)options)
        {
            // CHANGE TRACKING ==============================================================================
            base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            base.ChangeTracker.AutoDetectChangesEnabled = false;

            // MEDIATR ====================================================================================
            Guard.ArgumentNotNull(nameof(mediator), mediator);
            _mediator = mediator;
            System.Diagnostics.Debug.WriteLine("GenericContext::ctor ->" + this.GetHashCode());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be commuted
            var result = await SaveChangesAsync(cancellationToken: cancellationToken);

            return true;
        }


        public override int SaveChanges()
        {
            var rowAffecteds = base.SaveChanges();
            return rowAffecteds;

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var rowAffecteds = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return rowAffecteds;
        }


    }

}
