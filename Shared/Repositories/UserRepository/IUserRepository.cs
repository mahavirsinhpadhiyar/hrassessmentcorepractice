using Shared.Entities;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<UserVM> GetAllUsers();
        UserVM GetUserDetail(Guid Id);
        void SaveUser(UserVM model);

        UserVM UpdateUser(UserVM model);
        Task<UserVM> Login(LoginVM loginVM);
    }
}
