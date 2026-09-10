using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Test Thermometer
        Console.WriteLine("--- Thermometer Test ---");
        Thermometer thermo = new Thermometer();
        thermo.SetTemperature(25);
        Console.WriteLine($"Celsius: {thermo.TemperatureCelsius}°C");
        Console.WriteLine($"Fahrenheit: {thermo.GetFahrenheit()}°F");

        // 2. Test Student
        Console.WriteLine("\n--- Student Test ---");
        Student student = new Student("Alice", 85);
        student.Introduce();
        student.UpdateGrade(95); // Valid update
        student.Introduce();

        // 3. Test Door
        Console.WriteLine("\n--- Door Test ---");
        Door door = new Door();
        door.Status();
        door.Open();
        door.Status();
        door.Close();
    }
}
