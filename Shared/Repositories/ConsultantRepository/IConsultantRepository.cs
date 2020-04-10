using Shared.ViewModels.Consultant;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Repositories.ConsultantRepository
{
    public interface IConsultantRepository
    {
        Task<List<ConsultantVM>> GetAllConsultants();
        Task<ConsultantVM> GetConsultantDetail(Guid Id);
        Task<bool> SaveConsultant(ConsultantVM model);
        Task<bool> UpdateConsultant(ConsultantVM model);
        Task<bool> DeleteConsultant(Guid Id);
    }
}
