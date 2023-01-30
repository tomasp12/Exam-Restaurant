using Restaurant.Interfaces;

namespace Restaurant.Repositories
{
    public class MenuRepository
    {
        public List<T> GetMenuList<T>() where T : class, IMenuItems
        {
            DbRepository<T> data = new();
            return data.GetAll();
        }
    }
}
