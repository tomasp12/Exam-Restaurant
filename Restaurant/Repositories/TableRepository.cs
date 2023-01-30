using Restaurant.Models;
namespace Restaurant.Repositories
{
    public class TableRepository
    {
        public List<Table> GetTableList()
        {
            DbRepository<Table> data = new();
            return data.GetAll();
        }
        public int GetTableSeats(int tableNumber)
        {
            DbRepository<Table> data = new();
            return data.GetAll().First(x => x.TableNumber == tableNumber).Seats;
        }
        public bool GetTableStatus(int tableNumber)
        {
            DbRepository<Table> data = new();
            return data.GetAll().First(x => x.TableNumber == tableNumber).IsOccupied;
        }

    }
}
