public class Student
{
    // Auto-properties for Name and Grade
    public string Name { get; set; }
    
    // Grade can only be set from within the Student class
    public int Grade { get; private set; }

    // Constructor to easily initialize Name and Grade
    public Student(string name, int initialGrade)
    {
        Name = name;
        UpdateGrade(initialGrade);
    }

    // Method to safely update the grade with validation
    public void UpdateGrade(int newGrade)
    {
        if (newGrade >= 0 && newGrade <= 100)
        {
            Grade = newGrade;
        }
        else
        {
            Console.WriteLine("Invalid grade! Must be between 0 and 100.");
        }
    }

    // Method to print student info
    public void Introduce()
    {
        Console.WriteLine($"Hi, my name is {Name} and my grade is {Grade}.");
    }
}