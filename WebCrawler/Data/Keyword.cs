using System.ComponentModel.DataAnnotations;

namespace WebCrawler.Data
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        [Required]
        public int PageId { get; set; }
        [Required]
        public Page Page { get; set; }
    }
}
