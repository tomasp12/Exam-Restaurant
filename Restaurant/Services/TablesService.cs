using Restaurant.Enum;
using Restaurant.Interfaces;
using Restaurant.Models;
using Restaurant.Repositories;

namespace Restaurant.Services
{
    public class TablesService : ITablesService
    {
        private readonly UiService _ui;
        private readonly TableRepository _tableRepository;
        public TablesService(UiService ui)
        {
            _ui = ui;
            _tableRepository = new TableRepository();
        }
        public void SetTableStatus(int tableNumber)
        {
            if (tableNumber == 0) return;
            var row = _tableRepository.GetTableList().First(x => x.TableNumber == tableNumber);
            row.IsOccupied ^= true;
            _tableRepository.UpdateTableStatus(row);
        }
        public int ChooseFreeTable(int customersNumber)
        {
            _ui.ShowPersonNumber(customersNumber);
            var tablesList = _tableRepository.GetTableList();
            _ui.ShowTableList(tablesList);
            var offeredTableNumber = FindTable(customersNumber);
            if (offeredTableNumber == 0)
            {
                _ui.OutputMessage(ErrorMessage.Table);
                return 0;
            }

            var tableNumber = _ui.ShowTableChoose(customersNumber, offeredTableNumber);
            var freeTableList = new List<Table>(tablesList.Where(x => x.IsOccupied == false));
            if (CheckIsTableInList(tableNumber, freeTableList))
            {
                return tableNumber;
            }
            else
            {
                _ui.OutputMessage(ErrorMessage.BusyTable);
                return 0;
            }
        }

        private int FindTable(int customersNumber)
        {
            var tablesList = _tableRepository.GetTableList().Where(x => x.IsOccupied == false).OrderBy(x => x.Seats);
            foreach (var table in tablesList)
            {
                if (table.Seats >= customersNumber)
                {
                    return table.TableNumber;
                }
            }
            return 0;

        }

        public int ChooseOccupierTable()
        {

            var tableList = new List<Table>(_tableRepository.GetTableList().Where(x => x.IsOccupied == true));
            _ui.ShowTableList(tableList);
            var tableNumber = _ui.AskChoose("Choose table (0 exit):");
            if (CheckIsTableInList(tableNumber, tableList))
            {
                return tableNumber;
            }
            return 0;
        }

        private bool CheckIsTableInList(int tableNumber, List<Table> list)
        {
            var count = list.Count(x => x.TableNumber == tableNumber);
            return count != 0;
        }
    }
}
