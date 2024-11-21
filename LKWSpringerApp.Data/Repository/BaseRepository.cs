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

        public IEnumerable<TType> GetAll()
        {
            return dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await dbSet.ToArrayAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            dbSet.Add(item);
            dbContext.SaveChanges();
        }

        public async Task AddAsync(TType item)
        {
            await dbSet.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public bool Delete(TId id)
        {
            TType entity = GetById(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            dbContext.SaveChanges();

            return true;
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

        public bool SoftDelete(TId id)
        {
            var entity = GetById(id);
            if (entity == null) return false;

            if (entity is ISoftDeletable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }

            throw new InvalidOperationException($"Entity {typeof(TType)} does not implement ISoftDeletable");
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

        public bool Update(TType item)
        {
            try
            {
                dbSet.Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

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
    }
}
