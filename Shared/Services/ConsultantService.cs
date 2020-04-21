using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Helpers;
using Shared.Repositories.GenericRepository;
using Shared.UnitOfWork;
using Shared.ViewModels.Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IConsultantService
    {
        Task<List<ConsultantVM>> GetAllConsultants();
        Task<ConsultantVM> GetConsultantDetail(Guid Id);
        Task<DbStatusCode> SaveConsultant(ConsultantVM model);
        Task<DbStatusCode> UpdateConsultant(ConsultantVM model);
        Task<DbStatusCode> DeleteConsultant(Guid Id);
        Task<int> GetTotalConsultantCount();
    }
    public class ConsultantService: IConsultantService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Repositories.GenericRepository.IGenericRepository<ConsultantModel> genericRepository;

        public ConsultantService(IUnitOfWork unitOfWork, IGenericRepository<ConsultantModel> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            this.genericRepository = genericRepository;
        }

        public async Task<List<ConsultantVM>> GetAllConsultants()
        {
            try
            {
                return await genericRepository.GetAll().Select(m => new ConsultantVM()
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    IsActive = m.IsActive,
                    IsAdmin = m.IsAdmin
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                //logging an exception
                return null;
            }
        }

        public async Task<ConsultantVM> GetConsultantDetail(Guid Id)
        {
            ConsultantVM consultantVM = new ConsultantVM();

            var consultantModel = await genericRepository.Get(e => e.Id == Id).FirstOrDefaultAsync();
            if (consultantModel != null)
            {
                consultantVM.Id = consultantModel.Id;
                consultantVM.FirstName = consultantModel.FirstName;
                consultantVM.LastName = consultantModel.LastName;
                consultantVM.Email = consultantModel.Email;
                consultantVM.IsActive = consultantModel.IsActive;
                consultantVM.IsAdmin = consultantModel.IsAdmin;
            }
            else
            {
                return null;
            }

            return consultantVM;
        }

        public async Task<DbStatusCode> SaveConsultant(ConsultantVM model)
        {
            try
            {
                genericRepository.Add(new ConsultantModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsAdmin = model.IsAdmin
                });
                var changedVal = await unitOfWork.Commit();

                if (changedVal > 0)
                    return DbStatusCode.Created;
                else
                {
                    return DbStatusCode.DbError;
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return DbStatusCode.Exception;
            }
        }

        public async Task<DbStatusCode> UpdateConsultant(ConsultantVM model)
        {
            try
            {
                //var consultantModel = await GetConsultantDetail(model.Id);
                var consultantModel = await genericRepository.Get(e => e.Id == model.Id).FirstOrDefaultAsync();

                if (consultantModel != null)
                {
                    consultantModel.FirstName = model.FirstName;
                    consultantModel.LastName = model.LastName;
                    consultantModel.Email = model.Email;
                    consultantModel.IsActive = model.IsActive;
                    consultantModel.IsAdmin = model.IsAdmin;

                    genericRepository.Update(consultantModel);
                    var changedVal = await unitOfWork.Commit();

                    if (changedVal > 0)
                        return DbStatusCode.Updated;
                    else
                    {
                        //Set the response status
                        return DbStatusCode.DbError;
                    }
                }
                else
                {
                    return DbStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return DbStatusCode.Exception;
            }
        }
        public async Task<DbStatusCode> DeleteConsultant(Guid Id)
        {
            try
            {
                var consultantModel = await genericRepository.Get(e => e.Id == Id).FirstOrDefaultAsync();

                if (consultantModel == null)
                {
                    return DbStatusCode.NotFound;
                }
                else
                {
                    genericRepository.Delete(consultantModel);
                    var changedVal = await unitOfWork.Commit();

                    if (changedVal > 0)
                        return DbStatusCode.Deleted;
                    else
                    {
                        //Set the response status
                        return DbStatusCode.DbError;
                    }
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return DbStatusCode.Exception;
            }
        }

        public async Task<int> GetTotalConsultantCount()
        {
            try
            {
                return await genericRepository.GetTotalCount();
            }
            catch (Exception ex)
            {
                //logging an exception
                return 0;
            }
        }
    }
}
