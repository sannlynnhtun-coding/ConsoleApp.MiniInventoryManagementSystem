# Mini Inventory Management System

## Overview
This console application is designed to manage a small inventory system. It supports operations such as adding, updating, and deleting items, as well as creating and managing orders.

## Domain Logic

### Item
Represents a product in the inventory.
- **Attributes**:
    - `ID`: Unique identifier for the item.
    - `Name`: Name of the item.
    - `Quantity`: Quantity in stock.
    - `Price`: Price per unit.
- **Methods**:
    - `AddItem()`: Adds a new item to the inventory.
    - `UpdateItem()`: Updates the details of an existing item.
    - `DeleteItem()`: Removes an item from the inventory.
    - `GetItem()`: Retrieves an item by its ID.

### Inventory
Manages a collection of items.
- **Attributes**: List of `Item` objects.
- **Methods**:
    - `AddToInventory(Item item)`: Adds an item to the inventory.
    - `RemoveFromInventory(int id)`: Removes an item by ID.
    - `UpdateInventoryItem(int id, Item item)`: Updates an existing item in the inventory.
    - `ListInventory()`: Lists all items in the inventory.

### Order
Handles the creation and management of customer orders.
- **Attributes**:
    - `OrderID`: Unique identifier for the order.
    - `Items`: List of `Item` objects in the order.
    - `TotalPrice`: Total price of all items in the order.
- **Methods**:
    - `CreateOrder(List<Item> items)`: Creates a new order.
    - `AddItemToOrder(int orderId, Item item)`: Adds an item to an existing order.
    - `RemoveItemFromOrder(int orderId, int itemId)`: Removes an item from an order.
    - `GetOrderDetails(int orderId)`: Retrieves details of a specific order.
