using Diplom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Diplom.AppDbContext;
using Diplom.Services.Interfaces;
using Diplom.ViewModels;
using System.Security.Claims;

namespace Diplom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IConsultationService _consultationService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            IUserService userService, IConsultationService consultationService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
            _consultationService = consultationService;
        }

        public async Task<IActionResult> GetUsers()
        {

            var response = await _userService.GetUsers();

            if (response.StatusCode == Models.Account.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _userService.DeleteUser(userId);

            if (response.StatusCode == Models.Account.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(_context.Consultations.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetConsultation(int id)
        {
            var consultation = await _consultationService.GetCons(id);

            var consViewModel = new ConsultationViewModel()
            {
                Id = consultation.Data.Id,
                Name = consultation.Data.Name,
                Date = consultation.Data.Date,
                Description = consultation.Data.Description,
                UserName = consultation.Data.UserName,
            };
            return View(consViewModel);
        }

        public async Task<IActionResult> Unsub(int consId)
        {
            var user = await _consultationService.Unsub(consId, User.Identity.Name);
            return RedirectToAction("Search", "Search");
        }

        public IActionResult AddCons() => View();

        public async Task<IActionResult> MyCons()
        {
            var userName = User.Identity.Name;

            var response = await _consultationService.GetMyCons(userName);

            if (response.StatusCode == Diplom.Models.Account.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddConsultation(ConsultationViewModel consultation)
        {
            string name = User.FindFirstValue(ClaimTypes.Name);
            consultation.UserName = name;

            var response = await _consultationService.AddCosultation(consultation);

            if (response.StatusCode == Diplom.Models.Account.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public async Task<IActionResult> Sub(int id)
        {
            string name = User.FindFirstValue(ClaimTypes.Name);

            var response = await _consultationService.Sub(id, name);

            if (response.StatusCode == Diplom.Models.Account.StatusCode.OK)
            {
                return View("Index", _context.Consultations.ToList());
            }
            return View("Index");

        }

        public async Task<IActionResult> DeleteCons(int consId)
        {
            var response = await _consultationService.DeleteConsultation(consId);

            if (response.StatusCode == Models.Account.StatusCode.OK)
            {
                return RedirectToAction("MyCons");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCons(int consId)
        {
            var response = await _consultationService.GetCons(consId);

            if (response.StatusCode == Models.Account.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateConsToDb(int consId, ConsultationViewModel consultation)
        {
            consultation.Id = consId;
            var response = await _consultationService.UpdateCons(consultation);
            if (response.StatusCode == Models.Account.StatusCode.OK)
            {
                return RedirectToAction("MyCons");
            }
            return RedirectToAction("View");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
