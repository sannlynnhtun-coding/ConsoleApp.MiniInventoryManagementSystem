using ConsoleApp.MiniInventoryManagementSystem.Models;

namespace ConsoleApp.MiniInventoryManagementSystem.Services;

public class OrderService
{
    private readonly Dictionary<int, Order> _orders = new();
    private int _nextOrderId = 1;

    public Order CreateOrder(List<Item> items)
    {
        var order = new Order
        {
            OrderID = _nextOrderId++,
            Items = new List<Item>(items),
            TotalPrice = CalculateTotalPrice(items)
        };
        _orders.Add(order.OrderID, order);
        return order;
    }

    public void AddItemToOrder(int orderId, Item item)
    {
        if (_orders.TryGetValue(orderId, out var order))
        {
            order.Items.Add(item);
            order.TotalPrice = CalculateTotalPrice(order.Items);
        }
        else
        {
            throw new ArgumentException("Order not found");
        }
    }

    public Order GetOrderDetails(int orderId)
    {
        if (_orders.TryGetValue(orderId, out var order))
        {
            return order;
        }
        else
        {
            throw new ArgumentException("Order not found");
        }
    }

    public IEnumerable<Order> ListOrders()
    {
        return _orders.Values;
    }

    private double CalculateTotalPrice(List<Item> items)
    {
        return items.Sum(item => item.Price * item.Quantity);
    }
}