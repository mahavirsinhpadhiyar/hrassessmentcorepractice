using Shared.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        HRADbContext _context { get; }
        Task<int> Commit();
    }
}
