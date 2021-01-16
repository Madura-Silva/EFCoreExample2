using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess
{
    public interface ICoreSampleDBContext
    {
        DbSet<T> DbSet<T>() where T : class;
        Task SaveChangesAsync();
    }
}
