using ConsoleApp.MiniInventoryManagementSystem.Models;

namespace ConsoleApp.MiniInventoryManagementSystem.Services;

public class InventoryService
{
    private readonly List<Item> _items = new List<Item>();

    public void AddToInventory(Item item)
    {
        _items.Add(item);
    }

    public void RemoveFromInventory(int id)
    {
        var item = _items.FirstOrDefault(i => i.ID == id);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void UpdateInventoryItem(int id, Item updatedItem)
    {
        var item = _items.FirstOrDefault(i => i.ID == id);
        if (item != null)
        {
            item.Name = updatedItem.Name;
            item.Quantity = updatedItem.Quantity;
            item.Price = updatedItem.Price;
        }
    }

    public Item GetItem(int id)
    {
        return _items.FirstOrDefault(i => i.ID == id);
    }

    public List<Item> ListInventory()
    {
        return _items;
    }
}