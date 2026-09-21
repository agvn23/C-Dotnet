# Encapsulation and Abstraction

In this exercise you will create a new console app and scaffold three different classes to practise encapsulation. Each class should demonstrate how to hide internal state and control access using properties and methods.

## Objective
Learn how to design classes that keep fields private, expose properties responsibly, and provide controlled methods for changing state.

## Instructions
Scaffold a new console application:

```
dotnet new console -o EncapsulationExercise
cd EncapsulationExercise
code .
```

Add three classes in separate files:

## 1. Thermometer.cs

Private field to hold temperature in Celsius.
Property TemperatureCelsius with a public getter and a private setter.
Method SetTemperature(double value) that only allows values between -50 and 100.
Method GetFahrenheit() that returns the Fahrenheit equivalent.
## 2. Student.cs

Auto-properties Name and Grade.
Ensure Grade can only be set inside the class (private set).
Method UpdateGrade(int newGrade) that validates the grade is between 0 and 100.
Method Introduce() that prints a simple message with name and grade.
## 3. Door.cs

Property IsOpen with a private setter.
Method Open() that sets IsOpen = true.
Method Close() that sets IsOpen = false.
Method Status() that prints whether the door is open or closed.
## Program.cs

Instantiate each of the three classes.
Demonstrate accessing values with getters and methods, and mutating them using controlled methods.
# Reflection question
What's the difference between:

`public int MyProperty { get; private set; }`

and

`public int MyProperty { get; }`

