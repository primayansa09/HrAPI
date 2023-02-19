using HrAPI.Context;
using HrAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Create(Entity entity)
        {
            entities.Add(entity);
            return myContext.SaveChanges();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Update(Entity entity, Key key)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
        public int Delete(Key key)
        {
            var delete = entities.Find(key);
            myContext.Remove(delete);
            return myContext.SaveChanges();            
        }
    }
}
