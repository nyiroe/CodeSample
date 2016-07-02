using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;

namespace WpfMvvmEf6Example.Services
{
    public class DbContextFactory : IDbContextFactory
    {
        public ForumDbContext GetDbContext()
        {
            return new ForumDbContext();
        }
    }
}
