using System.ComponentModel.DataAnnotations;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace WebCrawler.Data
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ICollection<Keyword> Keywords { get; set; }
    }
}
