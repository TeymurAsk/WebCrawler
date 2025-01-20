using Microsoft.EntityFrameworkCore;

namespace WebCrawler.Data
{
    public class WebCrawlerDBContext : DbContext
    {
        public WebCrawlerDBContext(DbContextOptions<WebCrawlerDBContext> options) : base(options) 
        {
                
        }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Page> Pages { get; set; }
    }
}
