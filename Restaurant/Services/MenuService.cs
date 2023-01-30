using Restaurant.Interfaces;
using Restaurant.Models;
using Restaurant.Repositories;

namespace Restaurant.Services
{
    public class MenuService<T> where T : class, IMenuItems
    {
        private readonly UiService _ui;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly List<T> _menuList;

        public MenuService(UiService ui)
        {
            _ui = ui;
            _orderItemRepository = new OrderItemRepository();
            var menuRepository = new MenuRepository();
            _menuList = menuRepository.GetMenuList<T>();
        }
        public void ShowMenu()
        {
            _ui.ShowMenuList(_menuList);
            _ui.OutputContinueText();
        }

        private T GetItemById(int itemId)
        {
            return _menuList.First(x => x.Id == itemId);
        }

        public void ChooseItem(int orderId)
        {
            if (orderId == 0)
            {
                return;
            }
            do
            {
                _ui.ShowMenuList(_menuList);
                var menuId = _ui.AskChoose(@"\nChoose item (0 exit):");
                if (menuId == 0)
                {
                    return;
                }
                if (CheckIsItemInList(menuId))
                {
                    SaveItem(orderId, menuId);
                }
            } while (true);
        }

        private void SaveItem(int orderId, int menuiD)
        {
            var menuItem = GetItemById(menuiD);
            var orderItem = new OrderItem()
            {
                Name = menuItem.Name,
                OrderId = orderId,
                Price = menuItem.Price
            };
            _orderItemRepository.AddOrderItem(orderItem);
        }

        private bool CheckIsItemInList(int menuId)
        {
            var count = _menuList.Count(x => x.Id == menuId);
            return count != 0;
        }
    }
}
