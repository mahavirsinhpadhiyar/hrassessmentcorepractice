using Shared.Entities;
using Shared.Helpers;
using Shared.Repositories.GenericRepository;
using Shared.UnitOfWork;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface ICompanyService
    {
        List<CompanyVM> GetAllCompanys();
        Task<CompanyVM> GetCompanyDetail(Guid Id);
        Task<DbStatusCode> SaveCompany(CompanyVM model);
        Task<DbStatusCode> UpdateCompany(CompanyVM model);
        Task<DbStatusCode> DeleteCompany(Guid Id);
        Task<int> GetTotalCompanyCount();
    }
    public class CompanyService: ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Repositories.GenericRepository.IGenericRepository<CompanyModel> genericRepository;

        public CompanyService(IUnitOfWork unitOfWork, IGenericRepository<CompanyModel> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            this.genericRepository = genericRepository;
        }

        public List<CompanyVM> GetAllCompanys()
        {
            try
            {
                return genericRepository.GetAll().Select(m => new CompanyVM()
                {
                    CompanyDescription = m.CompanyDescription,
                    CompanyName = m.CompanyName,
                    Id = m.Id
                }).ToList();
            }
            catch (Exception ex)
            {
                //logging an exception
                return null;
            }
        }

        public async Task<CompanyVM> GetCompanyDetail(Guid Id)
        {
            CompanyVM companyVM = new CompanyVM();

            var companyModel = await genericRepository.GetSingle(Id);
            if (companyModel != null)
            {
                companyVM.Id = companyModel.Id;
                companyVM.CompanyName = companyModel.CompanyName;
                companyVM.CompanyDescription = companyModel.CompanyDescription;
            }
            else
            {
                return null;
            }

            return companyVM;
        }

        public async Task<DbStatusCode> SaveCompany(CompanyVM model)
        {
            try
            {
                genericRepository.Add(new CompanyModel()
                {
                    Id = Guid.NewGuid(),
                    CompanyName = model.CompanyName,
                    CompanyDescription = model.CompanyDescription
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

        public async Task<DbStatusCode> UpdateCompany(CompanyVM model)
        {
            try
            {
                //var consultantModel = await GetConsultantDetail(model.Id);
                var companyModel = await genericRepository.GetSingle(model.Id);

                if (companyModel != null)
                {
                    companyModel.CompanyName = model.CompanyName;
                    companyModel.CompanyDescription = model.CompanyDescription;

                    genericRepository.Update(companyModel);
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
        public async Task<DbStatusCode> DeleteCompany(Guid Id)
        {
            try
            {
                var companyModel = await genericRepository.GetSingle(Id);

                if (companyModel == null)
                {
                    return DbStatusCode.NotFound;
                }
                else
                {
                    genericRepository.Delete(companyModel);
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

        public async Task<int> GetTotalCompanyCount()
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
