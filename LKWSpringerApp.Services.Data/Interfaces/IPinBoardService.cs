using LKWSpringerApp.Web.ViewModels.PinBoard;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IPinBoardService
    {
        Task<PinBoardNewsModel> GetNewsAsync();
        Task EditNewsAsync(PinBoardNewsModel model);
        Task<PinBoardDetailsModel?> GetPinBoardDataForDriverAsync(Guid driverId);
        Task<PinBoardEditDriverModel?> GetPinBoardDataForEditAsync(Guid driverId);
        Task UpdatePinBoardAsync(PinBoardEditDriverModel model);
    }
}
