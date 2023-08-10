using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
    public interface IGenericReferenceEntityRepository<T> : IGenericRepository<T> where T : ReferenceEntity
    {
        Task<T> GetMatchingStaticEntityFromDb(T entity);
    }
}
