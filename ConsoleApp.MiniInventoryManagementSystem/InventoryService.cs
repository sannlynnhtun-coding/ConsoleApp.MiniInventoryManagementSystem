namespace ConsoleApp.MiniInventoryManagementSystem;

public abstract class InventoryService
{
    private static readonly List<ProductDto> Inventory = [];
    private static int _nextProductId = 1;

    public static void Run()
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
            Console.WriteLine("6. Exit");
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
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        } while (choice != 6);
    }

    private static void AddProduct()
    {
        // Get product details from user input
        Console.WriteLine("Enter product name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Enter quantity: ");
        var quantity = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter unit price: ");
        var price = decimal.Parse(Console.ReadLine()!);

        // Create a new product object
        var product = new ProductDto
            { ProductId = _nextProductId++, Name = name!, Quantity = quantity, UnitPrice = price };

        // Add product to the inventory list
        Inventory.Add(product);
        Console.WriteLine("Product added successfully!");
    }

    private static void ListProducts()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("** Inventory List **");
        Console.WriteLine("{0,-10} {1,-20} {2,10} {3,10:C2}", "ID", "Name", "Quantity", "Unit Price");
        Console.WriteLine("---------------------------------------------------------------------------");
        foreach (var product in Inventory)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,10} {3,10:C2}", product.ProductId, product.Name, product.Quantity,
                product.UnitPrice);
        }
    }

    private static void EditProduct()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("Enter product ID to edit: ");
        var productId = int.Parse(Console.ReadLine()!);

        var productIndex = Inventory.FindIndex(p => p.ProductId == productId);
        if (productIndex == -1)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        Console.WriteLine("Edit Product Details:");
        Console.WriteLine($"Current Name : {Inventory[productIndex].Name}");
        var newName =
            Console.ReadLine() ?? Inventory[productIndex].Name; // Use nullish coalescing for optional name change
        Console.WriteLine($"Current Quantity: {Inventory[productIndex].Quantity}");
        int newQuantity = int.TryParse(Console.ReadLine(), out newQuantity)
            ? newQuantity
            : Inventory[productIndex].Quantity; // Use TryParse for optional quantity change

        Console.WriteLine($"Current Unit Price: {Inventory[productIndex].UnitPrice:C2}");
        decimal newPrice = decimal.TryParse(Console.ReadLine(), out newPrice)
            ? newPrice
            : Inventory[productIndex].UnitPrice; // Use TryParse for optional price change

        Inventory[productIndex] = new ProductDto
            { ProductId = productId, Name = newName, Quantity = newQuantity, UnitPrice = newPrice };
        Console.WriteLine("Product edited successfully!");
    }

    private static void RemoveProduct()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("Enter product ID to remove: ");
        var productId = int.Parse(Console.ReadLine()!);

        var productIndex = Inventory.FindIndex(p => p.ProductId == productId);
        if (productIndex == -1)
        {
            Console.WriteLine("Product not found!");
            return;
        }

        Console.WriteLine($"Are you sure you want to remove product '{Inventory[productIndex].Name}' (y/n)?");
        var confirmation = Console.ReadKey().KeyChar;

        if (confirmation == 'y' || confirmation == 'Y')
        {
            Inventory.RemoveAt(productIndex);
            Console.WriteLine("Product removed successfully!");
        }
        else
        {
            Console.WriteLine("Product removal cancelled.");
        }
    }

    private static void SearchProducts()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("Enter product name to search: ");
        var searchTerm = Console.ReadLine()!.ToLower();

        List<ProductDto> searchResults = Inventory.FindAll(p => p.Name.ToLower().Contains(searchTerm));

        if (searchResults.Count == 0)
        {
            Console.WriteLine("No products found matching that name.");
            return;
        }

        Console.WriteLine("** Search Results **");
        Console.WriteLine("{0,-10} {1,-20} {2,10} {3,10:C2}", "ID", "Name", "Quantity", "Unit Price");
        Console.WriteLine("--------------------------------------------------");
        foreach (var product in searchResults)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,10} {3,10:C2}", product.ProductId, product.Name, product.Quantity,
                product.UnitPrice);
        }
    }
}