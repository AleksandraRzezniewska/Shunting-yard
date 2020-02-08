using Calculator.Library;

namespace Calculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Equations equation1 = new Equations("3*4+6-7");
            Equations equation2 = new Equations("3/3");
            System.Console.WriteLine(equation1.Result);
            System.Console.WriteLine(equation2.Result);
            System.Console.ReadKey();
        }
    }
}
