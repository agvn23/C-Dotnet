Generics in C#
What Are Generics?
Generics let you write classes, methods, interfaces, and delegates with a placeholder for the type they operate on. The actual type is supplied by the caller at compile time.
Instead of writing one method per type, you write one method that works for any type — while keeping full type safety and avoiding boxing.
Copy// Without generics — you'd need a version per type
public int MaxInt(int a, int b) => a > b ? a : b;
public double MaxDouble(double a, double b) => a > b ? a : b;
// ...and so on forever

// With generics — one method, all types
public T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) > 0 ? a : b;

The <T> is a type parameter. T is a convention (T, TKey, TValue, TResult, etc.), not a keyword.

The Problem Generics Solve
Before generics (C# 1.0), you had two bad options:
Option 1: object-based collections
CopyArrayList list = new ArrayList();
list.Add(42);
list.Add("hello");      // no type checking!
int x = (int)list[0];   // cast required, fails at runtime if wrong
string s = (string)list[1];

Problems:

No compile-time type safety — bugs surface at runtime
Boxing/unboxing for value types (performance cost)
Casting everywhere (ugly, error-prone)

Option 2: Duplicate code per type
Write IntList, StringList, CustomerList, etc. — massive duplication.
Generics fix both: one implementation, full type safety, no boxing.

Basic Syntax
Generic method
Copypublic T FirstOrDefault<T>(IEnumerable<T> source)
{
    foreach (var item in source)
        return item;
    return default!;   // default(T): null for ref types, 0/false for value types
}

Generic class
Copypublic class Repository<T>
{
    private readonly List<T> _items = new();

    public void Add(T item) => _items.Add(item);
    public T? GetById(Func<T, bool> predicate) => _items.FirstOrDefault(predicate);
    public int Count => _items.Count;
}

var repo = new Repository<Customer>();
repo.Add(new Customer { Name = "Aisha" });

Generic interface
Copypublic interface IRepository<T>
{
    void Add(T item);
    T? GetById(int id);
    IEnumerable<T> GetAll();
}

Generic delegate
Copypublic delegate TResult Func<in T, out TResult>(T arg);
// (built into .NET — shown for illustration)


Type Constraints (where clauses)
By default, T can be anything. Constraints restrict what T can be, which unlocks members you can call on it.
Copypublic T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

Without where T : IComparable<T>, the compiler wouldn't let you call .CompareTo() on a.
Constraint types



Constraint
Meaning




where T : class
Reference type only


where T : struct
Value type only (non-nullable)


where T : new()
Must have a public parameterless constructor


where T : BaseClass
Must inherit from BaseClass


where T : IInterface
Must implement IInterface


where T : U
Must be U or derive from U (another type param)


where T : notnull
Non-nullable (C# 8+)


where T : unmanaged
Unmanaged value type (no refs)



You can combine them:
Copypublic class Cache<TKey, TValue>
    where TKey : notnull
    where TValue : class, new()
{
    public TValue Create() => new TValue();
}

Why new() matters
Copypublic T Create<T>() where T : new() => new T();  // only legal because of new()

Why default(T) / default!
For an unconstrained T, you can't assume it's non-null or has a value:
Copypublic T GetOrDefault<T>(int id) => default!;  // null for ref types, 0 for int, etc.

In modern C#, T? and default! handle nullability more cleanly.

Where Generics Are Used in .NET
Almost everywhere in modern .NET:
Copy// Collections
List<T>, Dictionary<TKey, TValue>, HashSet<T>, Queue<T>, Stack<T>

// LINQ
IEnumerable<T>, IQueryable<T>, Func<T, TResult>, Action<T>

// Async
Task<T>, ValueTask<T>

// Nullable & memory
Nullable<T>, Span<T>, Memory<T>

// Delegates
Func<...>, Action<...>, Predicate<T>, Comparison<T>

// DI & options
IServiceCollection, IOptions<T>, ILogger<T>

// Events
EventHandler<TEventArgs>


Benefits of Generics
1. Type safety at compile time
Copyvar list = new List<int>();
list.Add("hello");   // ❌ compile error — caught immediately

With ArrayList this would compile and blow up at runtime.
2. No boxing/unboxing for value types
Copy// ArrayList (non-generic) — value types get boxed
ArrayList al = new ArrayList();
al.Add(42);                 // boxes int → object (heap allocation)
int x = (int)al[0];         // unboxes

// List<int> (generic) — no boxing
List<int> list = new List<int>();
list.Add(42);               // stored directly, no boxing
int y = list[0];            // no unboxing

This is a real performance win in hot paths. Boxing allocates on the heap and adds GC pressure.
3. Code reuse without duplication
One List<T> serves List<int>, List<string>, List<Customer> — no copy-paste per type.
4. No casting noise
Copy// Non-generic
Customer c = (Customer)list[0];

// Generic
Customer c = list[0];   // clean, no cast

5. Performance via runtime specialization
The CLR generates specialized code per value type. List<int> uses actual int storage — not object[]. For reference types, it reuses one instantiation (all refs are the same size).
6. Better IntelliSense & tooling
The compiler knows the exact type, so autocomplete, refactoring, and analysis all work precisely.
7. Enables powerful abstractions
LINQ, DI containers, ORMs (EF Core), and functional patterns all lean heavily on generics. Without them, these libraries would be far uglier or slower.

Drawbacks / Gotchas

Can't use arithmetic operators on unconstrained T (+, -, *). Workarounds: INumber<T> (C# 11+), or interfaces.Copypublic T Add<T>(T a, T b) where T : INumber<T> => a + b;  // .NET 7+


No static members per type param in a clean way (still awkward).
Overuse hurts readability — deep generic chains (Func<Task<IEnumerable<KeyValuePair<TKey, TValue>>>>) get unreadable.
Variance rules (in/out) confuse people at first.
Reflection over generics is fiddly.
Constraint combinations can get hairy — sometimes a plain interface is clearer.


Variance (brief but important)
Variance lets generic types be assigned covariantly/contravariantly:
Copy// Covariant (out) — IEnumerable<Derived> is IEnumerable<Base>
IEnumerable<string> strings = new List<string>();
IEnumerable<object> objects = strings;   // OK because IEnumerable<out T>

// Contravariant (in) — Action<Base> is Action<Derived>
Action<object> actObj = o => Console.WriteLine(o);
Action<string> actStr = actObj;          // OK because Action<in T>


out T — T only appears in outputs (return types) → covariant
in T — T only appears in inputs (parameters) → contravariant
T (invariant) — appears in both


Generic Constraints in Practice — Example
Copypublic class Repository<T> where T : class, IEntity, new()
{
    private readonly List<T> _items = new();

    public T Create() => new T();                       // new() allows this
    public T? Find(int id) => _items.FirstOrDefault(e => e.Id == id);  // IEntity gives .Id
    public void Add(T item) => _items.Add(item);        // class allows null checks
}

public interface IEntity { int Id { get; } }

Each constraint unlocks something:

class → can be null, can compare to null
IEntity → can access .Id
new() → can call new T()


Generic vs Non-Generic — Side-by-Side
Copy// ❌ Non-generic
ArrayList list = new ArrayList();
list.Add(1);
list.Add("two");
int n = (int)list[0];    // runtime cast; wrong cast = exception

// ✅ Generic
List<object> list2 = new();
list2.Add(1);
list2.Add("two");
// still weak typing because T = object

// ✅✅ Properly generic
List<int> list3 = new();
list3.Add(1);
// list3.Add("two");     // compile error — type safe
int m = list3[0];        // no cast, no boxing


When to Use Generics
Good fit:

Collections, containers, repositories
Utility methods that work on many types (Max, Swap, Map)
Abstractions that shouldn't care about the concrete type (LINQ, DI, async)
Avoiding boxing in performance-sensitive code
Type-safe APIs where the caller picks the type

Consider alternatives:

If only one or two types are ever used → just write the concrete version
If you need arithmetic on T and can't use INumber<T> → consider a non-generic overload
If the type parameter appears only once and adds no value → it's noise, drop it


Quick Reference — Minimal Example
Copypublic class Stack<T>
{
    private readonly List<T> _items = new();

    public void Push(T item) => _items.Add(item);

    public T Pop()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Empty");
        var last = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return last;
    }




Source: https://minitoolai.com/Claude/
