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
