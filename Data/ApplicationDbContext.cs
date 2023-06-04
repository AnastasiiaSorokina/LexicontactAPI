using System.Data.Entity;

namespace LexicontactAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("LexicontactConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        // Entities
        public DbSet<Language> Language { get; set; }
    }
}