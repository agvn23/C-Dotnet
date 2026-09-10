//Base class
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a generic shape.");
    }
}
//Derived class Circle (uses 'new' to hide the base method)
public class Circle : Shape
{
    public new void Draw()
    {
        Console.WriteLine("Drawing a Circle.");
    }
}
//Derived class Square (uses 'override' for polymorphism)
public class Square : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a Square.");
    }
}