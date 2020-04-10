using Shared.Context;
using Shared.Repositories.ConsultantRepository;
using Shared.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.UnitOfWork
{
    public interface IUnitOfWork
    {
        //void Complete();
        HRADbContext _context { get; }
        void Commit();
        //IUserRepository UserRepository { get; }
        //IConsultantRepository ConsultantRepository { get; }
    }
}
