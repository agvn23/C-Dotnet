public class Thermometer
{
    // Private field holding the actual temperature
    private double _temperatureCelsius;

    // Property with a public getter and a private setter
    public double TemperatureCelsius
    {
        get { return _temperatureCelsius; }
        private set { _temperatureCelsius = value; }
    }

    // Controlled method to set the temperature with validation logic
    public void SetTemperature(double value)
    {
        if (value >= -50 && value <= 100)
        {
            _temperatureCelsius = value;
        }
        else
        {
            Console.WriteLine("Invalid temperature! Must be between -50 and 100.");
        }
    }

    // Method to calculate and return Fahrenheit
    public double GetFahrenheit()
    {
        return (_temperatureCelsius * 9.0 / 5.0) + 32;
    }
}