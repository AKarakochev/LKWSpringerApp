namespace LKWSpringerApp.Data.Models.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        IQueryable<TType> GetAllAttached();

        // Addition
        Task AddAsync(TType item);

        // Deletion
        void Delete(TType entity);
        Task<bool> DeleteAsync(TId id);

        // Soft Deletion
        Task<bool> SoftDeleteAsync(TId id);
        Task<bool> SoftDeleteAsync(Guid driverId, Guid tourId);

        // Update
        Task<bool> UpdateAsync(TType item);

        //Save
        Task SaveChangesAsync();
    }
}
