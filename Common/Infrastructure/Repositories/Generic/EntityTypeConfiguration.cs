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
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        private readonly string _toTable;
        public EntityTypeConfiguration()
        {
            _toTable = "";
        }

        public EntityTypeConfiguration(string toTable)
        {
            _toTable = toTable;
        }

        public virtual void Configure(EntityTypeBuilder<T> entityTypeConfiguration)
        {
            if (entityTypeConfiguration.Metadata.BaseType == null)
            {
                var tableName = _toTable == "" ? typeof(T).Name.ToLower() : _toTable;
                //entityTypeConfiguration.ToTable(tableName);                
                entityTypeConfiguration.HasKey(ct => ct.Id);

                // Trackingentities related //////////////////////////////////////////////////////////////////////////
                entityTypeConfiguration.Ignore(p => p.EntityIdentifier);
                entityTypeConfiguration.Ignore(p => p.TrackingState);

                //////////////////////////////////////////////////////////////////////////////////////////////////////

            }
            entityTypeConfiguration.Ignore(p => p.ModifiedProperties);
        }
    }

}
