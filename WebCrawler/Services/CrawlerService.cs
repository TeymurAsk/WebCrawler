﻿using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Data;

namespace WebCrawler.Services
{
    public class CrawlerService
    {
        private readonly WebCrawlerDBContext _context;

        public CrawlerService(WebCrawlerDBContext context)
        {
            _context = context;
        }

        public async Task CrawlAsync(string url)
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);

            var content = doc.DocumentNode.InnerText;

            var keywords = ExtractKeywords(content);

            var page = new Page
            {
                Url = url,
                Content = content,
                Keywords = keywords.Select(k => new Keyword { Word = k }).ToList()
            };
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            var links = doc.DocumentNode.SelectNodes("//a[@href]")
                        ?.Select(node => node.Attributes["href"].Value)
                        ?.Where(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                        ?.Distinct();

            if (links != null)
            {
                foreach (var link in links)
                {
                    if (!_context.Pages.Any(p => p.Url == link))
                    {
                        await CrawlAsync(link);
                    }
                }
            }
        }

        private IEnumerable<string> ExtractKeywords(string content)
        {
            return content.Split(new[] { ' ', '\n', '\r', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                          .Where(word => word.Length > 3)
                          .Select(word => word.ToLowerInvariant())
                          .Distinct();
        }
        public async Task<List<Page>> SearchPagesByKeyword(string keyword)
        {
            return await _context.Pages
                .Where(p => p.Keywords.Any(k => k.Word.Contains(keyword)))
                .ToListAsync();
        }
    }
}
