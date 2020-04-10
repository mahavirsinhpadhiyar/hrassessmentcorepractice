using System;
using System.Collections.Generic;
using System.Text;
using Shared.Context;
using Shared.Repositories.ConsultantRepository;
using Shared.Repositories.UserRepository;

namespace Shared.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //private bool disposed = false;
        public HRADbContext _context { get; }
        public UnitOfWork(HRADbContext context)
        {
            _context = context;
        }
        public async void Commit()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        //private readonly IUserRepository _userRepository;
        //private readonly IConsultantRepository _consultantRepository;
        //public UnitOfWork(IUserRepository userRepository, IConsultantRepository consultantRepository)
        //{
        //    _userRepository = userRepository;
        //    _consultantRepository = consultantRepository;
        //}
        //public IUserRepository UserRepository => throw new NotImplementedException();

        //public IConsultantRepository ConsultantRepository => throw new NotImplementedException();

        //public void Complete()
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
