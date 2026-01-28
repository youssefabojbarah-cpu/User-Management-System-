using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace User_Management_System
{
    internal class Program
    {
        static List<string> names = new List<string>();
        static List<int> ages = new List<int>();
        static List<double> salaries = new List<double>();

        static void Main(string[] args)
        {
            LoadFromFile();

            while (true)
            {
                ShowMenu();
                string choice = ReadChoice();


                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;

                    case "2":
                        ShowUsers();
                        break;

                    case "3":
                        SearchUser();
                        break;

                    case "4":
                        AverageSalary();
                        break;

                    case "5":
                        SaveToFile();
                        break;

                    case "6":
                        SaveToFile();
                        return;
                    
                    case "7":
                        DeleteUser();
                        break;

                    case "8":
                        UpdatUser();
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n1 - Add User");
            Console.WriteLine("2 - Show Users");
            Console.WriteLine("3 - Search User");
            Console.WriteLine("4 - Average Salary");
            Console.WriteLine("5 - Save To File");
            Console.WriteLine("6 - Exit");
            Console.WriteLine("7 - Delete User");
            Console.WriteLine("8 - Update User");
            Console.Write("Choose: ");
        }

        static void AddUser()
        {
            try
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.Write("Enter your age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter your salary: ");
                double salary = double.Parse(Console.ReadLine());

                names.Add(name);
                ages.Add(age);
                salaries.Add(salary);

                Console.WriteLine("User added successfully.");
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }
        
        static string ReadChoice()
        {
            Console.Write("Choose: ");
            return Console.ReadLine()?.Trim();
        }

        static void ShowUsers()
        {
            if (names.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {names[i]} - Age: {ages[i]} - Salary: {salaries[i]}");
            }
        }

        static void SearchUser()
        {
            Console.Write("Enter name to search: ");
            string search = Console.ReadLine();

            bool found = false;

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{names[i]} - Age: {ages[i]} - Salary: {salaries[i]}");
                    found = true;
                    break;
                }
            }

            if (!found)
                Console.WriteLine("User not found.");
        }

        static void AverageSalary()
        {
            if (salaries.Count == 0)
            {
                Console.WriteLine("No data.");
                return;
            }

            double avg = salaries.Average();
            Console.WriteLine("Average Salary = " + avg);
        }

        static void SaveToFile()
        {
            using (StreamWriter sw = new StreamWriter("users.txt"))
            {
                for (int i = 0; i < names.Count; i++)
                {
                    sw.WriteLine($"{names[i]},{ages[i]},{salaries[i]}");
                }
            }

            Console.WriteLine("Data saved.");
        }

        static void LoadFromFile()
        {
            if (!File.Exists("users.txt"))
                return;

            string[] lines = File.ReadAllLines("users.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length != 3)
                    continue;

                names.Add(parts[0]);
                ages.Add(int.Parse(parts[1]));
                salaries.Add(double.Parse(parts[2]));
            }
        }

        static void DeleteUser()
        {
            Console.Write("Enter name to delete: ");
            string name = Console.ReadLine();

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    names.RemoveAt(i);
                    ages.RemoveAt(i);
                    salaries.RemoveAt(i);

                    Console.WriteLine("User deleted successfully.");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }
        static void UpdatUser()
        {
            Console.Write("Enter name to update: ");
            string search = Console.ReadLine();

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Leave empty to keep old value.");

                    Console.Write($"New name ({names[i]}): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                        names[i] = newName;

                    Console.Write($"New age ({ages[i]}): ");
                    string ageInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ageInput))
                        ages[i] = int.Parse(ageInput);

                    Console.Write($"New salary ({salaries[i]}): ");
                    string salaryInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(salaryInput))
                        salaries[i] = double.Parse(salaryInput);

                    Console.WriteLine("User updated successfully.");
                    return;
                }
            }

            Console.WriteLine("User not found.");
        }


    }
}
