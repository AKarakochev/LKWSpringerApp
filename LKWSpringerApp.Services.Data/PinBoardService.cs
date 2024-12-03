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

        public async Task AddNewsAsync(PinBoardNewsModel model)
        {
            var existingNews = await dbContext.PinBoards.FirstOrDefaultAsync(pb => pb.DriverId == null);

            if (existingNews != null)
            {
                existingNews.News = model.News;
                existingNews.ImportantNews = model.ImportantNews;
                dbContext.PinBoards.Update(existingNews);
            }
            else
            {
                var pinBoard = new PinBoard
                {
                    News = model.News,
                    ImportantNews = model.ImportantNews
                };
                await dbContext.PinBoards.AddAsync(pinBoard);
            }

            await dbContext.SaveChangesAsync();
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
            return await dbContext.PinBoards
                .Where(pb => pb.DriverId == driverId || pb.DriverId == null)
                .Select(pb => new PinBoardDetailsModel
                {
                    DrivingLicenseExpDate = pb.DrivingLicenseExpDate.ToString("MM/yyyy"),
                    DrivingCardExpDate = pb.DrivingCardExpDate.ToString("MM/yyyy"),
                    DrivingLicenseRenewalDate = pb.DrivingLicenseRenewalDate.HasValue
                        ? pb.DrivingLicenseRenewalDate.Value.ToString("MM/yyyy")
                        : null,
                    DrivingCardRenewalDate = pb.DrivingCardRenewalDate.HasValue
                        ? pb.DrivingCardRenewalDate.Value.ToString("MM/yyyy")
                        : null,
                    News = pb.News,
                    ImportantNews = pb.ImportantNews
                })
                .FirstOrDefaultAsync();
        }
    }
}
