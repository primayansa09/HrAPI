using HrAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : DbContext
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<Entity>();
        }

        public int Create(Entity entity)
        {
            entities.Add(entity);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Update(Entity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(Key key)
        {
            dbContext.Remove(entities.Find(key));
            return dbContext.SaveChanges();
        }
    }
}
