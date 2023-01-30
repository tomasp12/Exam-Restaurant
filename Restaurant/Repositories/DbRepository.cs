using Microsoft.EntityFrameworkCore;


namespace Restaurant.Repositories
{
    public class DbRepository<T> where T : class
    {
        public List<T> GetAll()
        {
            using var context = new RestaurantDatabaseContext();
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            using var context = new RestaurantDatabaseContext();
            return context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            using var context = new RestaurantDatabaseContext();
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            using var context = new RestaurantDatabaseContext();
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            using var context = new RestaurantDatabaseContext();
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
