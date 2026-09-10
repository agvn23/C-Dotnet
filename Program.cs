GenericRepository<Employee> employees = new();

employees.Add(new Employee(1, "Alice"));
employees.Add(new Employee(2, "Bob"));
employees.Add(new Employee(3, "Charlie"));

//testing
Employee? employee = employees.GetById(2);
Console.WriteLine(employee?.Name);

bool updated = employees.Update(
    new Employee(2, "Robert")
);
Console.WriteLine($"Updated: {updated}");

bool deleted = employees.Delete(3);
Console.WriteLine($"Deleted: {deleted}");

Console.WriteLine("All employees:");
foreach (Employee emp in employees.GetAll())
{
    Console.WriteLine($"{emp.Id}: {emp.Name}");
}

// BlogPost

Console.WriteLine();
Console.WriteLine("Testing BlogPosts:");

GenericRepository<BlogPost> posts = new();

posts.Add(new BlogPost(1, "My First Blog Post"));
posts.Add(new BlogPost(2, "Learning C#"));
posts.Add(new BlogPost(3, "Understanding Generics"));

//testing
BlogPost? post = posts.GetById(2);
Console.WriteLine($"Found: {post?.Title}");

bool postUpdated = posts.Update(
    new BlogPost(2, "Learning C# Generics")
);
Console.WriteLine($"Updated: {postUpdated}");

bool postDeleted = posts.Delete(3);
Console.WriteLine($"Deleted: {postDeleted}");

Console.WriteLine("All blog posts:");
foreach (BlogPost blogPost in posts.GetAll())
{
    Console.WriteLine($"{blogPost.Id}: {blogPost.Title}");
}
