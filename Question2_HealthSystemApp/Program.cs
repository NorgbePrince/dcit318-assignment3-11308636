using System;
using System.Collections.Generic;

public class Patient
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Condition { get; set; }

    public Patient(string name, int age, string condition)
    {
        Name = name;
        Age = age;
        Condition = condition;
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Condition: {Condition}");
    }
}

public class PatientRegistry<T> where T : Patient
{
    private readonly List<T> patients = new();

    public void Add(T patient)
    {
        patients.Add(patient);
    }

    public void ListAll()
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
            return;
        }

        Console.WriteLine("\n--- Patient Records ---");
        foreach (var patient in patients)
        {
            patient.Display();
        }
    }
}

class Program
{
    static void Main()
    {
        PatientRegistry<Patient> registry = new();

        while (true)
        {
            Console.WriteLine("\n1. Register Patient\n2. View Patients\n3. Exit");
            Console.Write("Choose option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine() ?? "Unknown";
                    Console.Write("Enter Age: ");
                    if (!int.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.WriteLine("Invalid age.");
                        break;
                    }
                    Console.Write("Enter Condition: ");
                    string condition = Console.ReadLine() ?? "N/A";

                    registry.Add(new Patient(name, age, condition));
                    Console.WriteLine("Patient added.");
                    break;

                case "2":
                    registry.ListAll();
                    break;

                case "3":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
