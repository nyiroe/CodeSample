using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
            : base("name=ForumDbContext")
        {
            Database.Log = Console.Write;

            // Pl. AutoMapper behúzná az adatbázist
            // lazy loading miatt egyesével kérdezné le a rekordokat
            // inkább célzottan kérdezünk le az optimális teljesítményhez
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Topic)
                .WillCascadeOnDelete(true);
        }
    }
}
