using LKWSpringerApp.Data;
using LKWSpringerApp.Services.Data.Interfaces;
using LKWSpringerApp.Web.ViewModels.PinBoard;
using LKWSpringerApp.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Services.Data
{
    public class PinBoardService : IPinBoardService
    {
        private readonly LkwSpringerDbContext dbContext;

        public PinBoardService(LkwSpringerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PinBoardNewsModel> GetNewsAsync()
        {
            return await dbContext.PinBoards
                .Select(pb => new PinBoardNewsModel
                {
                    News = pb.News,
                    ImportantNews = pb.ImportantNews
                })
                .FirstOrDefaultAsync() ?? new PinBoardNewsModel { News = "No news available", ImportantNews = "No important news available" };
        }

        public async Task EditNewsAsync(PinBoardNewsModel model)
        {
            var pinBoard = await dbContext.PinBoards.FirstOrDefaultAsync();

            if (pinBoard == null)
                throw new InvalidOperationException("News not found.");

            pinBoard.News = model.News;
            pinBoard.ImportantNews = model.ImportantNews;

            dbContext.PinBoards.Update(pinBoard);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PinBoardDetailsModel?> GetPinBoardDataForDriverAsync(Guid driverId)
        {
            var pinBoard = await dbContext.PinBoards.FirstOrDefaultAsync(pb => pb.DriverId == driverId);

            if (pinBoard == null)
            {
                pinBoard = new PinBoard
                {
                    DriverId = driverId,
                    DrivingLicenseExpDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    DrivingCardExpDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    DrivingLicenseRenewalDate = null,
                    DrivingCardRenewalDate = null,
                    UpcomingCourse = null,
                    UpcomingCourseDate = null,
                    News = "No news available",
                    ImportantNews = "No important news available"
                };

                await dbContext.PinBoards.AddAsync(pinBoard);
                await dbContext.SaveChangesAsync();
            }

            return new PinBoardDetailsModel
            {
                DriverId = pinBoard.DriverId ?? Guid.Empty,
                DrivingLicenseExpDate = pinBoard.DrivingLicenseExpDate.ToString("MM/yyyy"),
                DrivingCardExpDate = pinBoard.DrivingCardExpDate.ToString("MM/yyyy"),
                DrivingLicenseRenewalDate = pinBoard.DrivingLicenseRenewalDate?.ToString("MM/yyyy"),
                DrivingCardRenewalDate = pinBoard.DrivingCardRenewalDate?.ToString("MM/yyyy"),
                UpcomingCourse = pinBoard.UpcomingCourse,
                UpcomingCourseDate = pinBoard.UpcomingCourseDate?.ToString("dd/MM/yyyy"),
                News = pinBoard.News,
                ImportantNews = pinBoard.ImportantNews
            };
        }

        public async Task<PinBoardEditDriverModel?> GetPinBoardDataForEditAsync(Guid driverId)
        {
            var driverExists = await dbContext.Drivers.AnyAsync(d => d.Id == driverId && !d.IsDeleted);

            if (!driverExists)
            {
                throw new InvalidOperationException("Driver does not exist.");
            }

            var pinBoard = await dbContext.PinBoards.FirstOrDefaultAsync(pb => pb.DriverId == driverId);

            if (pinBoard == null)
            {
                pinBoard = new PinBoard
                {
                    DriverId = driverId,
                    DrivingLicenseExpDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    DrivingCardExpDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    DrivingLicenseRenewalDate = null,
                    DrivingCardRenewalDate = null,
                    UpcomingCourse = null,
                    UpcomingCourseDate = null
                };

                await dbContext.PinBoards.AddAsync(pinBoard);
                await dbContext.SaveChangesAsync();
            }

            return new PinBoardEditDriverModel
            {
                DriverId = pinBoard.DriverId ?? Guid.Empty,
                DrivingLicenseExpDate = pinBoard.DrivingLicenseExpDate.ToString("MM/yyyy"),
                DrivingCardExpDate = pinBoard.DrivingCardExpDate.ToString("MM/yyyy"),
                DrivingLicenseRenewalDate = pinBoard.DrivingLicenseRenewalDate?.ToString("MM/yyyy"),
                DrivingCardRenewalDate = pinBoard.DrivingCardRenewalDate?.ToString("MM/yyyy"),
                UpcomingCourse = pinBoard.UpcomingCourse,
                UpcomingCourseDate = pinBoard.UpcomingCourseDate?.ToString("dd/MM/yyyy")
            };
        }

        public async Task UpdatePinBoardAsync(PinBoardEditDriverModel model)
        {
            var pinBoard = await dbContext.PinBoards.FirstOrDefaultAsync(pb => pb.DriverId == model.DriverId);

            if (pinBoard == null)
            {
                throw new InvalidOperationException($"PinBoard record not found for DriverId: {model.DriverId}");
            }

            pinBoard.DrivingLicenseExpDate = DateTime.ParseExact(model.DrivingLicenseExpDate, "MM/yyyy", null);
            pinBoard.DrivingCardExpDate = DateTime.ParseExact(model.DrivingCardExpDate, "MM/yyyy", null);
            pinBoard.DrivingLicenseRenewalDate = string.IsNullOrEmpty(model.DrivingLicenseRenewalDate)
                ? (DateTime?)null
                : DateTime.ParseExact(model.DrivingLicenseRenewalDate, "MM/yyyy", null);
            pinBoard.DrivingCardRenewalDate = string.IsNullOrEmpty(model.DrivingCardRenewalDate)
                ? (DateTime?)null
                : DateTime.ParseExact(model.DrivingCardRenewalDate, "MM/yyyy", null);
            pinBoard.UpcomingCourse = model.UpcomingCourse;
            pinBoard.UpcomingCourseDate = string.IsNullOrEmpty(model.UpcomingCourseDate)
                ? (DateTime?)null
                : DateTime.ParseExact(model.UpcomingCourseDate, "dd/MM/yyyy", null);

            dbContext.PinBoards.Update(pinBoard);
            await dbContext.SaveChangesAsync();
        }
    }
}
