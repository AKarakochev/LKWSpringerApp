using LKWSpringerApp.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.PinBoard;
using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Web.ViewModels.Driver;

namespace LKWSpringerApp.Web.Controllers
{
    [Authorize]
    public class PinBoardController : Controller
    {
        private readonly IPinBoardService pinBoardService;
        private readonly IDriverService driverService;

        public PinBoardController(IPinBoardService pinBoardService, IDriverService driverService)
        {
            this.pinBoardService = pinBoardService;
            this.driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var drivers = await driverService.GetAllDriversForPinBoardAsync(pageIndex, pageSize);
            var news = await pinBoardService.GetNewsAsync();

            var model = new Tuple<PaginatedList<DriverPinBoardModel>, PinBoardNewsModel>(drivers, news);

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var loggedInDriverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && loggedInDriverId != id.ToString())
            {
                TempData["ErrorMessage"] = "You do not have permission to view this information.";
                return RedirectToAction(nameof(Index));
            }

            var details = await pinBoardService.GetPinBoardDataForDriverAsync(id);

            if (details == null)
            {
                TempData["ErrorMessage"] = "Details not available.";
                return RedirectToAction(nameof(Index));
            }

            return View(details);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNews()
        {
            return View(new PinBoardNewsModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNews(PinBoardNewsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await pinBoardService.AddNewsAsync(model);
            TempData["SuccessMessage"] = "News added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNews()
        {
            var news = await pinBoardService.GetNewsAsync();
            return View(news);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNews(PinBoardNewsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await pinBoardService.EditNewsAsync(model);
            TempData["SuccessMessage"] = "News updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
