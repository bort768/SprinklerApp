using Microsoft.EntityFrameworkCore;
using Model.Helpers;

namespace DataBaseService.Infrastructure
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal SprinklerAppDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SprinklerAppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual IEnumerable<TEntity> Specify(ISpecification<TEntity> specification = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            foreach (var includeExpression in specification.Includes)
            {
                query = query.Include(includeExpression);
            }

            if (specification.OrderBy != null)
            {
                //query = specification.OrderBy(query);
                query = query.OrderBy(specification.OrderBy);
            }

            return query.ToList();
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null) 
                Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
