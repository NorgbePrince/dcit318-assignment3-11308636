using System;
using System.Collections.Generic;

// Generic Inventory Item
public class InventoryItem<T>
{
    public T Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    public InventoryItem(T id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }

    public void Display()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}, Quantity: {Quantity}");
    }
}

// Custom Exception
public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message) { }
}

public class InventoryManager<T>
{
    private readonly Dictionary<T, InventoryItem<T>> inventory = new();

    public void AddItem(T id, string name, int quantity)
    {
        if (inventory.ContainsKey(id))
        {
            inventory[id].Quantity += quantity;
        }
        else
        {
            inventory[id] = new InventoryItem<T>(id, name, quantity);
        }
    }

    public void GetItem(T id)
    {
        if (inventory.TryGetValue(id, out var item))
        {
            item.Display();
        }
        else
        {
            throw new ItemNotFoundException($"Item with ID '{id}' not found.");
        }
    }

    public void ListItems()
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("\n--- Inventory List ---");
        foreach (var item in inventory.Values)
        {
            item.Display();
        }
    }
}

class Program
{
    static void Main()
    {
        InventoryManager<string> manager = new();

        while (true)
        {
            Console.WriteLine("\n1. Add Item\n2. View Item by ID\n3. View All Items\n4. Exit");
            Console.Write("Choose option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter Item ID: ");
                    string id = Console.ReadLine() ?? "0";
                    Console.Write("Enter Item Name: ");
                    string name = Console.ReadLine() ?? "Unnamed";
                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int qty))
                    {
                        manager.AddItem(id, name, qty);
                        Console.WriteLine("Item added.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity.");
                    }
                    break;

                case "2":
                    Console.Write("Enter Item ID: ");
                    string searchId = Console.ReadLine() ?? "";
                    try
                    {
                        manager.GetItem(searchId);
                    }
                    catch (ItemNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    manager.ListItems();
                    break;

                case "4":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
