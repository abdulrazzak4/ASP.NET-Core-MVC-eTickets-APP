using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace webApp.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<T>> IEntityBaseRepository<T>.GetAllAsync() => await _context.Set<T>().ToListAsync();
        async Task<T> IEntityBaseRepository<T>.GeytByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id );
        async Task IEntityBaseRepository<T>.AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        async Task IEntityBaseRepository<T>.DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id ); 
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        async Task IEntityBaseRepository<T>.UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query,(current ,includeProperties) => current.Include(includeProperties));
            return await query.ToListAsync();
        }
    }
}