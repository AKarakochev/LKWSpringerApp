namespace LKWSpringerApp.Data.Models.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        IQueryable<TType> GetAllAttached();
        Task AddAsync(TType item);
        void Delete(TType entity);
        Task<bool> DeleteAsync(TId id);
        Task<bool> SoftDeleteAsync(TId id);
        Task<bool> SoftDeleteAsync(Guid driverId, Guid tourId);
        Task<bool> UpdateAsync(TType item);
        Task SaveChangesAsync();
    }
}
