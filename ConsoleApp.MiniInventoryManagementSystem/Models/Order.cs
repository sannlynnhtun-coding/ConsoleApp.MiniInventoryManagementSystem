namespace ConsoleApp.MiniInventoryManagementSystem.Models;

public class Order
{
    public int OrderID { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public double TotalPrice { get; set; }
}