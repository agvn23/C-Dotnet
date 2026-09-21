# Classes and Objects

In this exercise, you will create a .NET console application that manages a small collection
of books. The program will use classes and objects to organise data and behaviour. The entry 
point (Program.cs) will run a while loop, allowing the user to choose actions from a simple menu.

## Objective
Practise designing a class (Book) with properties and methods.
Learn how to instantiate objects and manage them in a collection.
Reinforce using loops, conditionals, and method calls inside a console application.
## Instructions
Create a new console app:

```
dotnet new console -o BookLibrary
cd BookLibrary
code .
```

Add a Book.cs file with a Book class that contains:

Properties: Title, Author, and Year

A constructor to initialise a book

A method DisplayInfo() that prints book details

In Program.cs:

Create a List<Book> to store the collection.

Implement a while loop that keeps showing a menu:

Add Book → ask the user for title, author, and year, then add a new Book to the list.

Get Book → ask for a title, then search and display info if found.

Edit Book → ask for a title, then update its details.

Remove Book → ask for a title, then remove it from the list.

Exit → break the loop.

The methods listed above can be mapped to local functions in Program.cs like 
void AddBook(), void GetBook(), etc.
We recommend that some repetitive tasks like reading the inputs or searching for a 
book in the list are also abstracted to local functions in Program.cs, e.g: string 
ReadNonEmpty(string prompt) or Book? FindByTitle(string title)
## Hints
We haven’t talked about lists just yet but if you have a class of Book you can 
create a list like:
` var books = new List<Book>; `

A list can be queried to find something:
```
books.Find(
  item => string.Equals(item.NameOfProperty, searchTerm.Trim(),
  StringComparison.OrdinalIgnoreCase)
  )
```

The Console class has a method to read an input
For validating your inputs int and string have methods for this
