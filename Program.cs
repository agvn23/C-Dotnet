using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("1. Doc Hierarchy")
        Document doc = new Report { Title = "Q3 Financials", Author = "Alice" };
        doc.PrintInfo(); // Calls Reprts PrintInfo due to polymorphism

        //2. Shape Hierarchy (Hiding vs Overriding)
        Console.WriteLine("2. Shape Hierarchy")
        //Direct references
        Circle circleObj = new Circle();
        circleObj.Draw(); // Output: Drawing Circle
        Square squareObj = new Square();
        squareObj.Draw(); // Output: Drawing Square

        //Base class references (Polymorphism vs Hiding test)
        Shape shapeRefCircle = new Circle();
        shapeRefCircle.Draw(); // Output: Drawing generic shape (because 'new' hides it based on reference type)
        Shape shapeRefSquare= new Square();
        shapeRefSquare.Draw(); // Output: Drawing Square (because 'override' uses the actual object type)

        //3. Membership Hierarchy
        Console.WriteLine("3. Membership Hierarchy")
        Membership member = new LifetimeMembership { MemberName = "John Doe" };
        Console.WriteLine($"{member.MemberName}'s Benefits: {member.GetBenefits()}");
    }
}
