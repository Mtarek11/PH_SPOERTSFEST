using Microsoft.EntityFrameworkCore;
using Models;

namespace Reposatiory
{
    public class MainManager<T> where T : class
    {
        private readonly RamadanOlympicsContext MydB;
        private readonly DbSet<T> set;
        public MainManager(RamadanOlympicsContext _mydB)
        {
            MydB = _mydB;
            set = MydB.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return set.AsQueryable();
        }
        public async Task<T> FindByIdAsync(int id)
        {
            return await set.FindAsync(id);
        }
        public async Task<T> FindByStringIdAsync(string id)
        {
            return await set.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await set.AddAsync(entity);
        }
        public void Update(T entity)
        {
            set.Update(entity);
        }
        public void PartialUpdate(T entity)
        {
            set.Attach(entity);
        }
        public void Remove(T entity)
        {
            set.Remove(entity);
        }
    }
}
