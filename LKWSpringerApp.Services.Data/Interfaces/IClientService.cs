﻿using LKWSpringerApp.Web.ViewModels.Client;

namespace LKWSpringerApp.Services.Data.Interfaces
{
    public interface IClientService
    {
        Task<ICollection<AllClientModel>> IndexGetAllOrderedByNameAsync();

        Task AddClientAsync(AddClientModel model);

        //Dali trqbva pri men da e Guid?
        Task<DetailsClientModel> GetClientDetailsByIdAsync(Guid id);

        Task<bool> UpdateClientAsync(EditClientModel model);

        Task<bool> SoftDeleteClientAsync(Guid id);
    }
}
