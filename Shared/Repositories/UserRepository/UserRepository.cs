using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Context;
using Shared.Entities;
using Shared.ViewModels;

namespace Shared.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly HRADbContext _context;
        public UserRepository(HRADbContext context)
        {
            this._context = context;
        }
        public IEnumerable<UserVM> GetAllUsers()
        {
            return _context.UserModels.Select(m => new UserVM()
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                DOB = m.DOB,
                Id = m.Id
            }).ToList();
        }

        public UserVM GetUserDetail(Guid Id)
        {
            UserVM userVM = new UserVM();

            var userModel = _context.UserModels.FirstOrDefault(m => m.Id == Id);
            if (userModel != null)
            {
                userVM.Id = userModel.Id;
                userVM.FirstName = userModel.FirstName;
                userVM.LastName = userModel.LastName;
                userVM.Email = userModel.Email;
                userVM.Password = userModel.Password;
                userVM.DOB = userModel.DOB;
                userVM.ConfirmPassword = userModel.Password;
            }
            else
            {
                return null;
            }

            return userVM;
        }

        public async Task<UserVM> Login(LoginVM loginVM)
        {
            UserVM userVM = new UserVM();

            var userModel = await _context.UserModels.FirstOrDefaultAsync(m => m.Email == loginVM.UserName && m.Password == loginVM.Password);
            if (userModel != null)
            {
                userVM.Id = userModel.Id;
                userVM.FirstName = userModel.FirstName;
                userVM.LastName = userModel.LastName;
                userVM.Email = userModel.Email;
                userVM.DOB = userModel.DOB;
            }
            else
            {
                return null;
            }

            return userVM;
        }

        public void SaveUser(UserVM model)
        {
            _context.UserModels.Add(new UserModel() {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                DOB = model.DOB
            });
            _context.SaveChanges();
        }

        public UserVM UpdateUser(UserVM model)
        {
            var userModel = _context.UserModels.FirstOrDefault(m => m.Id == model.Id);

            if (userModel != null)
            {
                userModel.FirstName = model.FirstName;
                userModel.LastName = model.LastName;
                userModel.Email = model.Email;
                userModel.Password = model.Password;
                userModel.DOB = model.DOB;

                var user = _context.UserModels.Attach(userModel);
                user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return model;
        }
    }
}
