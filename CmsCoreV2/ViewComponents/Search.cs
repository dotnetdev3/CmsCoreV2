using CmsCoreV2.Data;
using CmsCoreV2.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Search(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int pageNumber = 1;
            var pageSize = 6;
            List<SearchDto> searchresult = null;
            var ww = Request.Query["query"].ToString();


            if (ww != null && ww != "")
            {
                if (!String.IsNullOrEmpty(Request.Query["page"]))
                {
                    pageNumber = Convert.ToInt32(Request.Query["page"]);
                }
                searchresult = await GetSearch(ww);
                var pagedData = searchresult.ToPagedList(pageSize, pageNumber);
                return View(pagedData);
            }
            return View("Default", new List<SearchDto>().ToPagedList(pageSize, 1));

        }
        private Task<List<SearchDto>> GetSearch(string word)
        {
            return Task.FromResult(SearchResults(word));
        }
        public List<SearchDto> SearchResults(string search)
        {
            List<SearchDto> searchDtoList = new List<SearchDto>();
            searchDtoList.AddRange(SearchPosts(search));
            searchDtoList.AddRange(SearchPages(search));
            searchDtoList.AsQueryable().OrderByDescending(v => v.ViewCount);
            return searchDtoList;
        }

        public List<SearchDto> SearchPosts(string words)
        {
            words = words.Trim();
            var searchWords = words.Split(' ');
            var query = _context.Posts.AsQueryable();
            List<SearchDto> dto = new List<SearchDto>();
            foreach (string word in searchWords)
            {
                if (word != null && word != "")
                {
                    dto = query.Where(w => w.IsPublished == true).Where(p => p.Body.Contains(word) || p.Title.Contains(word) || p.Description.Contains(word) || p.SeoDescription.Contains(word) || p.SeoKeywords.Contains(word) || p.SeoKeywords.Contains(word)).Select(s => new SearchDto { Title = s.Title, Slug = s.Slug, Description = s.Description, ViewCount = s.ViewCount }).ToList();
                }
            }
            return dto;
        }
        public List<SearchDto> SearchPages(string words)
        {
            words = words.Trim();
            var searchWords = words.Split(' ');
            var query = _context.Pages.AsQueryable();
            List<SearchDto> dto = new List<SearchDto>();
            foreach (string word in searchWords)
            {
                if (word != null && word != "")
                {
                    dto = query.Where(w => w.IsPublished == true).Where(p => p.Body.Contains(word) || p.Title.Contains(word) || p.SeoDescription.Contains(word) || p.SeoKeywords.Contains(word) || p.SeoKeywords.Contains(word)).Select(s => new SearchDto { Title = s.Title, Description=s.Body, Slug = s.Slug, ViewCount = s.ViewCount }).ToList();
                }
            }
            return dto;
        }
    }
}
