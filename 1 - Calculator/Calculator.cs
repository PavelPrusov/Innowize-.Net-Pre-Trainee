namespace _1___Calculator
{
    public class Calculator
    {
        public double Sum(double x, double y) => x + y;
        public double Sub(double x, double y) => x - y;
        public double Mult(double x, double y) => x * y;
        public double Div(double x, double y) => y != 0 ? x / y : throw new DivideByZeroException();

    }
}
