namespace WpfMvvmEf6Example.Interfaces
{
    public interface IDbContextFactory
    {
        ForumDbContext GetDbContext();
    }
}
