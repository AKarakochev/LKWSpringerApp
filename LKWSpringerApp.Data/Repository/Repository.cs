using LKWSpringerApp.Data.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly LkwSpringerDbContext dbContext;
        private readonly DbSet<TType> dbSet;

        public Repository(LkwSpringerDbContext dbContext)
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
        //
        //Da vidq kak se pravi
        public bool SoftDelete(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(TId id)
        {
            throw new NotImplementedException();
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
