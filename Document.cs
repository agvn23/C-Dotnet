//Base class
public class Document
{
    public string Title { get; set; }
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Document Title: {Title}");
    }
}
//Derived class Report
public class Report : Document
{
    public string Author { get; set; }
    public override void PrintInfo()
    {
        Console.WriteLine($"Report: '{Title}' by {Author}");
    }
}
//Derived class Invoice
public class Invoice : Document
{
    public decimal Amount { get, set, }
    public override void PrintInfo()
    {
        Console.WriteLine($"Invoice: '{Title}' for Amount: ${Amount}");
    }
}