using System;

namespace CustomDelegate
{
    static class Program
    {
        delegate int IntOperation(int a, int b);

        private static int Add(int a, int b)
        {
            Console.WriteLine("Add called");
            return a + b;
        }

        private static int Subtract(int a, int b)
        {
            Console.WriteLine("Subtract called");
            return a - b;
        }
        
        static void Main(string[] args)
        {
            var op = new IntOperation(Add);
            Console.WriteLine(op(2, 2));

            op = Subtract;
            Console.WriteLine(op(3, 2));
        }
    }
}