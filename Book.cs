public class Book
{
    public string Title { get; set; } 
    public string Author { get; set; }
    public int Year { get; set; }

    // Constructor to initialize the book
        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        // Method to print book details
        public void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {Year}");
        }
}


