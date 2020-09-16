using System.Data.Entity;
using System.Linq;

namespace Receiver161
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Binary> Binaries { get; set; }
        public DbSet<Bin_extended> Bins_extended { get; set; }
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Join tables Messages and Contents and return records from Contents
        /// where id equals input value
        /// </summary>
        /// <param name="id_message"></param>
        /// <returns></returns>
        public IQueryable<Content> GetContentsForId(int id_message)
        {
            return from m in this.Messages
                   join c in this.Contents on m.Id equals c.Id_messages
                   where m.Id.Equals(id_message)
                   select c;
        }

        /// <summary>
        /// Join tables Contents and Binaries and return records from Binaries
        /// where id equals input value
        /// </summary>
        /// <param name="id_content"></param>
        /// <returns></returns>
        public IQueryable<Binary> GetBinariesForId(int id_content)
        {
            return from c in this.Contents
                   join b in this.Binaries on c.Id equals b.Id_contents
                   where c.Id.Equals(id_content)
                   select b;
        }

        /// <summary>
        /// Join tables Binaries and Bin_extended and return records from Bin_extended
        /// where id equals input value
        /// </summary>
        /// <param name="id_binaries"></param>
        /// <returns></returns>
        public IQueryable<Bin_extended> GetBinExtensionForId(int id_binaries)
        {
            return from b in this.Binaries
                   join b_ex in this.Bins_extended on b.Id equals b_ex.Id_binaries
                   where b.Id.Equals(id_binaries)
                   select b_ex;
        }
    }
}
