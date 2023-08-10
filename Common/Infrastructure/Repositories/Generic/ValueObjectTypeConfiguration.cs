using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repositories.Generic
{
    public class ValueObjectTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : ValueObject
    {
        public ValueObjectTypeConfiguration()
        {
        }

        public virtual void Configure(EntityTypeBuilder<T> entityTypeConfiguration)
        {
            // Trackingentities related //////////////////////////////////////////////////////////////////////////
            entityTypeConfiguration.Ignore(p => p.EntityIdentifier);
            entityTypeConfiguration.Ignore(p => p.TrackingState);
            entityTypeConfiguration.Ignore(p => p.ModifiedProperties);
        }
    }

}
