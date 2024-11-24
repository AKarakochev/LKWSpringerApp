using LKWSpringerApp.Data.Models.Repository.Interfaces;
using LKWSpringerApp.Data.Models;

using Microsoft.EntityFrameworkCore;


namespace LKWSpringerApp.Data.Repository
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly LkwSpringerDbContext dbContext;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(LkwSpringerDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TType>();
        }
        public TType GetById(TId id)
        {
            TType entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet
                .FindAsync(id);

            return entity;
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public async Task AddAsync(TType item)
        {
            await dbSet.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public void Delete(TType entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDeleteAsync(Guid driverId, Guid tourId)
        {
            // Ensure that this logic is only applied to composite-key entities
            if (typeof(TType) == typeof(DriverTour))
            {
                // Retrieve the entity using the composite key
                var entity = await dbSet
                    .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "DriverId") == driverId &&
                                              EF.Property<Guid>(e, "TourId") == tourId);

                if (entity == null)
                {
                    return false; // Entity not found
                }

                // Check if the entity implements ISoftDeletable
                if (entity is ISoftDeletable softDeletableEntity)
                {
                    softDeletableEntity.IsDeleted = true;
                    await dbContext.SaveChangesAsync();
                    return true;
                }

                throw new InvalidOperationException($"Entity {typeof(TType)} does not implement ISoftDeletable");
            }

            throw new InvalidOperationException($"SoftDeleteAsync is not supported for type {typeof(TType)}");
        }

        public async Task<bool> SoftDeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                await dbContext.SaveChangesAsync();
                return true;
            }

            throw new InvalidOperationException($"Entity {typeof(TType)} does not implement ISoftDeletable");
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                dbSet.Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
