

namespace HrAPI.Repositories.Interface
{
    public interface IRepository<Entity, Key> where Entity:class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key key);
        int Create(Entity entity);
        int Update(Entity entity, Key key);
        int Delete(Key key);
    }
}
