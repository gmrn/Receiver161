using System.Data.Entity;

namespace Receiver161
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Message> Messages { get; set; }
    }
}
