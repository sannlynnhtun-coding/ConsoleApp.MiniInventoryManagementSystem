using ConsoleApp.MiniInventoryManagementSystem.Models;
using ConsoleApp.MiniInventoryManagementSystem.Services;

namespace ConsoleApp.MiniInventoryManagementSystem;

class Program
{
    private static readonly InventoryService inventory = new InventoryService();
    private static readonly OrderService orderService = new OrderService();

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. List Products");
            Console.WriteLine("3. Edit Product");
            Console.WriteLine("4. Remove Product");
            Console.WriteLine("5. Search Products");
            Console.WriteLine("6. Create Order");
            Console.WriteLine("7. List Orders");
            Console.WriteLine("8. Add Item to Order");
            Console.WriteLine("9. Exit");
            Console.WriteLine("Enter your choice: ");
            choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ListProducts();
                    break;
                case 3:
                    EditProduct();
                    break;
                case 4:
                    RemoveProduct();
                    break;
                case 5:
                    SearchProducts();
                    break;
                case 6:
                    CreateOrder();
                    break;
                case 7:
                    ListOrders();
                    break;
                case 8:
                    AddItemToOrder();
                    break;
                case 9:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        } while (choice != 9);
    }

    static void AddProduct()
    {
        Console.WriteLine("Enter product details:");
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine()!);
        Console.Write("Name: ");
        string name = Console.ReadLine()!;
        Console.Write("Quantity: ");
        int quantity = int.Parse(Console.ReadLine()!);
        Console.Write("Price: ");
        double price = double.Parse(Console.ReadLine()!);

        inventory.AddToInventory(new Item { ID = id, Name = name, Quantity = quantity, Price = price });
        Console.WriteLine("Product added.");
    }

    static void ListProducts()
    {
        Console.WriteLine("Listing all products...");
        var items = inventory.ListInventory();
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
        }
    }

    static void EditProduct()
    {
        Console.WriteLine("Enter the ID of the product to edit:");
        int id = int.Parse(Console.ReadLine()!);
        var item = inventory.GetItem(id);
        if (item != null)
        {
            Console.Write("New Name: ");
            item.Name = Console.ReadLine()!;
            Console.Write("New Quantity: ");
            item.Quantity = int.Parse(Console.ReadLine()!);
            Console.Write("New Price: ");
            item.Price = double.Parse(Console.ReadLine()!);

            inventory.UpdateInventoryItem(id, item);
            Console.WriteLine("Product updated.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void RemoveProduct()
    {
        Console.WriteLine("Enter the ID of the product to remove:");
        int id = int.Parse(Console.ReadLine()!);
        inventory.RemoveFromInventory(id);
        Console.WriteLine("Product removed.");
    }

    static void SearchProducts()
    {
        Console.WriteLine("Enter the name of the product to search:");
        string name = Console.ReadLine()!;
        var items = inventory.ListInventory();
        var foundItems = items.FindAll(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (foundItems.Count > 0)
        {
            foreach (var item in foundItems)
            {
                Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
            }
        }
        else
        {
            Console.WriteLine("No products found.");
        }
    }

    static void CreateOrder()
    {
        Console.WriteLine("Creating a new order...");
        var items = new List<Item>();

        char addMore;
        do
        {
            Console.WriteLine("Enter the ID of the item to add to the order:");
            int itemId = int.Parse(Console.ReadLine()!);
            var item = inventory.GetItem(itemId);
            if (item != null)
            {
                items.Add(item);
                Console.WriteLine("Item added to order.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }

            Console.WriteLine("Do you want to add more items? (y/n)");
            addMore = char.Parse(Console.ReadLine()!);
        } while (addMore == 'y');

        var order = orderService.CreateOrder(items);
        Console.WriteLine($"Order {order.OrderID} created with total price: {order.TotalPrice}");
    }

    static void ListOrders()
    {
        Console.WriteLine("Listing all orders...");
        var orders = orderService.ListOrders();
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderID}, Total Price: {order.TotalPrice}");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  Item ID: {item.ID}, Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
            }
        }
    }

    static void AddItemToOrder()
    {
        Console.WriteLine("Enter the Order ID to add items:");
        int orderId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter the ID of the item to add:");
        int itemId = int.Parse(Console.ReadLine()!);
        var item = inventory.GetItem(itemId);
        if (item != null)
        {
            orderService.AddItemToOrder(orderId, item);
            Console.WriteLine("Item added to order.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
}