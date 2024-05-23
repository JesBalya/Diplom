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

        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
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

        public IActionResult GetConsultation(int id) => View(_context.Consultations.FirstOrDefault(x => x.Id == id));

        public IActionResult Cons() => View();

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

            var response = await _consultationService.Sub(id,name);

            if (response.StatusCode == Diplom.Models.Account.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
