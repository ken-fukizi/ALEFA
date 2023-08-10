using SharedKernel.Interfaces;
using UI.Domain.Models.PatientAggregate;

namespace UI.Domain.Contracts
{
    public interface IPatientRepository : IGenericRepository<PatientModel>
    {
    }
}
