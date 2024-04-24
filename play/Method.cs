namespace play.Method;

public class Method
{
    public static void Greet(string name, int age)
    {
        Console.WriteLine($"Hello {name}! You are {age} years old.");
    }
}

class Person
{
    // private string name;
    //
    // public string Name   // property
    // {
    //     get { return name; }
    //     set { name = value; }
    // }
    
    public string Name { get; set; }
}
