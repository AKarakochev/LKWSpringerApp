using LKWSpringerApp.Web.ViewModels.PinBoard;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IPinBoardService
    {
        Task<PinBoardNewsModel> GetNewsAsync();
        Task AddNewsAsync(PinBoardNewsModel model);
        Task EditNewsAsync(PinBoardNewsModel model);
        Task<PinBoardDetailsModel?> GetPinBoardDataForDriverAsync(Guid driverId);
    }
}
