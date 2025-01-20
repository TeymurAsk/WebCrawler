using Microsoft.AspNetCore.Mvc;
using WebCrawler.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCrawler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        private readonly CrawlerService _crawlerService;
        public CrawlerController(CrawlerService crawlerService)
        {
            _crawlerService = crawlerService;
        }

        [HttpPost("crawl")]
        public async Task<IActionResult> StartCrawl([FromBody] string url)
        {
            await _crawlerService.CrawlAsync(url);
            return Ok("Crawling started.");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            // Search pages by keyword
            var pages = await _crawlerService.SearchPagesByKeyword(keyword);
            return Ok(pages);
        }
    }
}
