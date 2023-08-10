using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repositories.Generic
{
    public class EnumerationTypeConfiguration<T> : EntityTypeConfiguration<T> where T : Enumeration
    {
        public override void Configure(EntityTypeBuilder<T> entityTypeConfiguration)
        {
            base.Configure(entityTypeConfiguration);

            entityTypeConfiguration.Property(ct => ct.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entityTypeConfiguration.Property(ct => ct.DisplayName)
                .HasMaxLength(200)
                .IsRequired();

            //entityTypeConfiguration.Property<bool>("Persisted").HasDefaultValue(true);
        }
    }

}
