using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Context;

namespace Shared.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public HRADbContext _context { get; }
        public UnitOfWork(HRADbContext context)
        {
            _context = context;
        }
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
