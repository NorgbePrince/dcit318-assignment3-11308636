using System;
using System.IO;
using System.Collections.Generic;

// Custom exception for invalid grade
public class InvalidGradeException : Exception
{
    public InvalidGradeException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        string inputFile = "grades.txt";
        string outputFile = "results.txt";

        try
        {
            // Create grades.txt if missing
            if (!File.Exists(inputFile))
            {
                string[] sampleData = {
                    "John Doe,95",
                    "Jane Smith,82",
                    "Mike Lee,77",
                    "Invalid Data,hello",
                    "Bad Score,120"
                };
                File.WriteAllLines(inputFile, sampleData);
                Console.WriteLine($"'{inputFile}' not found. Sample file created.");
            }

            List<string> results = new List<string>();

            foreach (var line in File.ReadAllLines(inputFile))
            {
                try
                {
                    string[] parts = line.Split(',');
                    if (parts.Length != 2)
                        throw new FormatException($"Invalid line format: {line}");

                    string name = parts[0].Trim();
                    if (!int.TryParse(parts[1], out int score))
                        throw new InvalidGradeException($"Invalid grade for {name}: {parts[1]}");

                    if (score < 0 || score > 100)
                        throw new InvalidGradeException($"Grade out of range for {name}: {score}");

                    string letter = GetLetterGrade(score);
                    results.Add($"{name}: {letter}");
                }
                catch (Exception ex)
                {
                    results.Add($"Error processing line: {line} ({ex.Message})");
                }
            }

            File.WriteAllLines(outputFile, results);
            Console.WriteLine($"Results written to {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error: {ex.Message}");
        }
    }

    static string GetLetterGrade(int score)
    {
        if (score >= 90) return "A";
        else if (score >= 80) return "B";
        else if (score >= 70) return "C";
        else if (score >= 60) return "D";
        else return "F";
    }
}
