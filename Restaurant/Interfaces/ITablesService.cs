namespace Restaurant.Interfaces

{
    public interface ITablesService
    {
        void SetTableStatus(int tableNumber);
        public int ChooseFreeTable(int customersNumber);
        public int ChooseOccupierTable();
    }
}
