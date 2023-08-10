using SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Service
{
    public interface IResponse
    {
        void AddException(DomainException ex);

        bool HasExceptions();

        IEnumerable<DomainException> Exceptions { get; }
    }
}
