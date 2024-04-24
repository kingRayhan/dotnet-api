public class DataTypes
{
    public static void init()
    {
        Console.WriteLine("-------------------   Data Types   -------------------");
        // Multiple variables on one line
        int a = 1, b = 2, c = 3;
        Console.WriteLine($"a = {a}, b = {b}, c = {c}");

        // Multiple variables on multiple lines
        int d = 4;
        int e = 5;
        int f = 6;
        Console.WriteLine($"d = {d}, e = {e}, f = {f}");

        // String interpolation
        string greeting = "Hello";
        string name = "Rayhan";
        Console.WriteLine($"{greeting} {name}!");

        // Numeric data types
        int myNum = 5; // Integer (whole number)
        double myDoubleNum = 5.99D; // Floating point number
        char myLetter = 'D'; // Character
        bool myBool = true; // Boolean
        string myText = "Hello"; // String

        Console.WriteLine(myNum);
        Console.WriteLine(myDoubleNum);
        Console.WriteLine(myLetter);
        Console.WriteLine(myBool);
        Console.WriteLine(myText);

        // Boolean data types
        bool isTrue = true;
        bool isFalse = false;

        if (isTrue)
        {
            Console.WriteLine("This is true");
        }
        else
        {
            Console.WriteLine("This is false");
        }

        if (isFalse)
        {
            Console.WriteLine("This is false");
        }
        else
        {
            Console.WriteLine("This is true");
        }

        bool isPositive = true;
        bool isNegative = false;
        bool isZero = false;

        if (isPositive)
        {
            Console.WriteLine("This is positive");
        }
        else if (isNegative)
        {
            Console.WriteLine("This is negative");
        }
        else if (isZero)
        {
            Console.WriteLine("This is zero");
        }
        else
        {
            Console.WriteLine("This is neither positive nor negative");
        }
    }
}