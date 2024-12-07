using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.PinBoard;
using LKWSpringerApp.Services.Data.Helpers;
using LKWSpringerApp.Web.ViewModels.Driver;
using static LKWSpringerApp.Common.ErrorMessagesConstants.PinBoard;
using static LKWSpringerApp.Common.SuccessMessagesConstants.PinBoard;

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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var drivers = await driverService.GetAllDriversForPinBoardAsync(pageIndex, pageSize);
            var news = await pinBoardService.GetNewsAsync();

            var model = new Tuple<PaginatedList<DriverPinBoardModel>, PinBoardNewsModel>(drivers, news);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await pinBoardService.GetPinBoardDataForDriverAsync(id);

            if (details == null)
            {
                TempData["ErrorMessage"] = PinBoardDetailsNotAvailable;
                return RedirectToAction(nameof(Index));
            }

            return View(details);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNews(PinBoardNewsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await pinBoardService.EditNewsAsync(model);
            TempData["SuccessMessage"] = PinBoardNewsUpdated;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData["ErrorMessage"] = PinBoardDriverInvalidId;
                return RedirectToAction(nameof(Index));
            }

            var pinBoardData = await pinBoardService.GetPinBoardDataForEditAsync(id);

            if (pinBoardData == null)
            {
                TempData["ErrorMessage"] = PinBoardDriverDataNotFound;
                return RedirectToAction(nameof(Index));
            }

            return View(pinBoardData);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PinBoardEditDriverModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = PinBoardInvalidData;
                return View(model);
            }

            try
            {
                await pinBoardService.UpdatePinBoardAsync(model);
                TempData["SuccessMessage"] = PinBoardDetailsUpdate;
                return RedirectToAction(nameof(Details), new { id = model.DriverId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View(model);
            }
        }
    }
}
