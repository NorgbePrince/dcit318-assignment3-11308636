using System;
using System.Collections.Generic;

// Generic inventory class
public class Inventory<T>
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
        items.Add(item);
        Console.WriteLine($"{item} added to inventory.");
    }

    public void RemoveItem(T item)
    {
        if (items.Remove(item))
            Console.WriteLine($"{item} removed from inventory.");
        else
            Console.WriteLine($"{item} not found in inventory.");
    }

    public void DisplayItems()
    {
        Console.WriteLine("\nCurrent Inventory:");
        if (items.Count == 0)
            Console.WriteLine("Inventory is empty.");
        else
            foreach (var item in items)
                Console.WriteLine(item);
    }
}

class Program
{
    static void Main()
    {
        Inventory<string> stringInventory = new Inventory<string>();

        stringInventory.AddItem("Laptop");
        stringInventory.AddItem("Mouse");
        stringInventory.DisplayItems();

        stringInventory.RemoveItem("Mouse");
        stringInventory.DisplayItems();

        Console.WriteLine("\nUsing inventory with integers:");
        Inventory<int> intInventory = new Inventory<int>();
        intInventory.AddItem(1001);
        intInventory.AddItem(1002);
        intInventory.DisplayItems();
    }
}
