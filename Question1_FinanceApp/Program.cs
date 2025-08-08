using System;
using System.Collections.Generic;

public interface ITransaction
{
    void Display();
}

public sealed record Transaction(string Id, string Description, decimal Amount, DateTime Date) : ITransaction
{
    public void Display()
    {
        Console.WriteLine($"ID: {Id}, Description: {Description}, Amount: GHC{Amount}, Date: {Date.ToShortDateString()}");
    }
}

class FinanceApp
{
    private List<Transaction> transactions = new();

    public void AddTransaction(string id, string description, decimal amount)
    {
        var transaction = new Transaction(id, description, amount, DateTime.Now);
        transactions.Add(transaction);
    }

    public void ShowAllTransactions()
    {
        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions recorded.");
            return;
        }

        Console.WriteLine("\n--- Transaction History ---");
        foreach (var tx in transactions)
        {
            tx.Display();
        }
    }
}

class Program
{
    static void Main()
    {
        FinanceApp app = new();

        while (true)
        {
            Console.WriteLine("\n1. Add Transaction\n2. View Transactions\n3. Exit");
            Console.Write("Choose option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter ID: ");
                    string id = Console.ReadLine() ?? "TX";
                    Console.Write("Enter Description: ");
                    string desc = Console.ReadLine() ?? "No description";
                    Console.Write("Enter Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        app.AddTransaction(id, desc, amount);
                        Console.WriteLine("Transaction added.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;

                case "2":
                    app.ShowAllTransactions();
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
