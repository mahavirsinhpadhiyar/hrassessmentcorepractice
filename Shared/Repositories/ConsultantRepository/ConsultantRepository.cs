using Microsoft.EntityFrameworkCore;
using Shared.Context;
using Shared.Entities;
using Shared.ViewModels.Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Repositories.ConsultantRepository
{
    public class ConsultantRepository : IConsultantRepository
    {
        private readonly HRADbContext _context;
        //public ConsultantRepository()
        //{
        //    _context = new HRADbContext();
        //}
        public ConsultantRepository(HRADbContext context)
        {
            this._context = context;
        }
        public async Task<List<ConsultantVM>> GetAllConsultants()
        {
            try
            {
                return await _context.Consultants.Select(m => new ConsultantVM()
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

            var consultantModel = await _context.Consultants.FirstOrDefaultAsync(m => m.Id == Id);
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

        public async Task<bool> SaveConsultant(ConsultantVM model)
        {
            try
            {
                _context.Consultants.Add(new ConsultantModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsAdmin = model.IsAdmin
                });
                var changedVal = await _context.SaveChangesAsync();

                if (changedVal > 0)
                    return true;
                else
                {
                    //Set the response status
                    return false;
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return false;
            }
        }

        public async Task<bool> UpdateConsultant(ConsultantVM model)
        {
            try
            {
                var consultantModel = await _context.Consultants.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (consultantModel != null)
                {
                    consultantModel.FirstName = model.FirstName;
                    consultantModel.LastName = model.LastName;
                    consultantModel.Email = model.Email;
                    consultantModel.IsActive = model.IsActive;
                    consultantModel.IsAdmin = model.IsAdmin;

                    var consultant = _context.Consultants.Attach(consultantModel);
                    consultant.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    int changedVal = await _context.SaveChangesAsync();

                    if (changedVal > 0)
                        return true;
                    else
                    {
                        //Set the response status
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return false;
            }
        }
        public async Task<bool> DeleteConsultant(Guid Id)
        {
            try
            {
                var consultantModel = await _context.Consultants.FirstOrDefaultAsync(m => m.Id == Id);

                if (consultantModel == null)
                {
                    return false;
                }
                else
                {
                    _context.Consultants.Remove(consultantModel);
                    var deleteConsultantVal = await _context.SaveChangesAsync();

                    if (deleteConsultantVal > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                //logging an exception
                return false;
            }
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
