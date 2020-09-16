using Receiver161.Models;
using System.Data.Entity;
using System.Linq;

namespace Receiver161
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public DbSet<Binary> Binaries { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<SubContent> SubContents { get; set; }

        /// <summary>
        /// Search records from a table Content by id_message
        /// </summary>
        /// <param name="id_message"></param>
        /// <returns></returns>
        public IQueryable<Content> GetContentsForId(int id_message) => this.Contents.Where(c => c.Id_message == id_message);

        /// <summary>
        /// Search records from a table SubContent by id_content
        /// </summary>
        /// <param name="id_content"></param>
        /// <returns></returns>
        public IQueryable<SubContent> GetSubContentsById(int id_content) => this.SubContents.Where(sc => sc.Id_content == id_content);

        /// <summary>
        /// Search records from a table Request by id_message
        /// </summary>
        /// <param name="id_message"></param>
        /// <returns></returns>
        public IQueryable<Request> GetRequestsById(int id_message) => this.Requests.Where(req => req.Id_message == id_message);

        /// <summary>
        /// Search records from a table Response by id_message
        /// </summary>
        /// <param name="id_message"></param>
        /// <returns></returns>
        public IQueryable<Response> GetResponsesById(int id_message) => this.Responses.Where(res => res.Id_message == id_message);

        /// <summary>
        /// Search records from a table Binary by id_request
        /// </summary>
        /// <param name="id_request"></param>
        /// <returns></returns>
        public IQueryable<Binary> GetBinariesByRequestId(int id_request) => this.Binaries.Where(b => b.Id_request == id_request);

        /// <summary>
        /// Search records from a table Binary by id_response
        /// </summary>
        /// <param name="id_response"></param>
        /// <returns></returns>
        public IQueryable<Binary> GetBinariesByResponseId(int id_response) => this.Binaries.Where(b => b.Id_response == id_response);

    }
}
