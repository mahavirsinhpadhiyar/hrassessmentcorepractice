using Shared.Helpers;
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
        Task<DbStatusCode> SaveConsultant(ConsultantVM model);
        Task<DbStatusCode> UpdateConsultant(ConsultantVM model);
        Task<DbStatusCode> DeleteConsultant(Guid Id);
        Task<int> GetTotalConsultantCount();
    }
}
