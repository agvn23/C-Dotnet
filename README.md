# Inheritance

In this exercise you will create a console application that demonstrates different inheritance chains. The goal is to practise using base classes, derived classes, method overriding, and method hiding.

## Objective
Learn how to define base and derived classes.
Practise calling base class constructors from derived classes.
Explore the difference between hiding (new) and overriding (override).
Create and use multiple inheritance chains.
## Instructions

Create a new console application:
```
dotnet new console -o InheritanceExercise
cd InheritanceExercise
code .
```

Scaffold the following classes:
## 1. Document hierarchy

Base class Document with a Title property and a virtual method PrintInfo().
Derived class Report that adds an Author property and overrides PrintInfo().
Derived class Invoice that adds an Amount property and overrides PrintInfo().
## 2. Shape hierarchy

Base class Shape with a method Draw().
Derived class Circle that hides the base Draw() using new.
Derived class Square that overrides a virtual Draw() to demonstrate polymorphism.
## 3. Membership hierarchy

Base class Membership with a MemberName property and virtual method GetBenefits().
Derived class StandardMembership overrides GetBenefits() to return a basic string.
Derived class PremiumMembership overrides GetBenefits() with more features.
Derived class LifetimeMembership sealed override of GetBenefits() to prevent further changes.
## Program.cs

Instantiate objects from each hierarchy.
Call their methods using both base class and derived class references.
Demonstrate the difference between hiding and overriding by assigning a derived object to a base reference and calling its methods.
## Reflection question
What happens when you call a hidden method (new) via a base class reference compared to an overridden method (override)?
