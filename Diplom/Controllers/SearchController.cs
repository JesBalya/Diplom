using Diplom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diplom.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService ss) 
        { 
            searchService = ss;
        }

        public async Task<IActionResult> Search()
        {
            string name = User.FindFirstValue(ClaimTypes.Name);
      
            var response = await searchService.getConsultations(name);

            if (response.StatusCode == Diplom.Models.Account.StatusCode.OK)
            {
               return View(response.Data);
            }

            return View();
        }

    }
}
