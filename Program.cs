Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Book> books = new List<Book>();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Book Management Menu ---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Get Book");
            Console.WriteLine("3. Edit Book");
            Console.WriteLine("4. Remove Book");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Add Book
                    Console.Write("Enter title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter year: ");
                    if (int.TryParse(Console.ReadLine(), out int year))
                    {
                        books.Add(new Book(title, author, year));
                        Console.WriteLine("Book added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid year format. Book not added.");
                    }
                    break;

                case "2":
                    // Get Book
                    Console.Write("Enter the title of the book to search: ");
                    string searchTitle = Console.ReadLine();
                    Book foundBook = books.Find(b => b.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase));
                    
                    if (foundBook != null)
                    {
                        Console.WriteLine("Book found:");
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "3":
                    // Edit Book
                    Console.Write("Enter the title of the book you want to edit: ");
                    string editTitle = Console.ReadLine();
                    Book bookToEdit = books.Find(b => b.Title.Equals(editTitle, StringComparison.OrdinalIgnoreCase));

                    if (bookToEdit != null)
                    {
                        Console.Write("Enter new title (or press Enter to keep current): ");
                        string newTitle = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newTitle)) bookToEdit.Title = newTitle;

                        Console.Write("Enter new author (or press Enter to keep current): ");
                        string newAuthor = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newAuthor)) bookToEdit.Author = newAuthor;

                        Console.Write("Enter new year (or press Enter to keep current): ");
                        string yearInput = Console.ReadLine();
                        if (int.TryParse(yearInput, out int newYear))
                        {
                            bookToEdit.Year = newYear;
                        }

                        Console.WriteLine("Book updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "4":
                    // Remove Book
                    Console.Write("Enter the title of the book to remove: ");
                    string removeTitle = Console.ReadLine();
                    Book bookToRemove = books.Find(b => b.Title.Equals(removeTitle, StringComparison.OrdinalIgnoreCase));

                    if (bookToRemove != null)
                    {
                        books.Remove(bookToRemove);
                        Console.WriteLine("Book removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "5":
                    // Exit
                    running = false;
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }
}
