namespace Calculator_App
{
    public class Calculator
    {
        public double Sum(double firstNum, double secondNum) => firstNum + secondNum;
        public double Sub(double firstNum, double secondNum) => firstNum - secondNum;
        public double Mult(double firstNum, double secondNum) => firstNum * secondNum;
        public double Div(double firstNum, double secondNum) => secondNum != 0 ? firstNum / secondNum : throw new DivideByZeroException();
        public enum Operation
        {
            Add = 1,
            Subtract = 2,
            Multiply = 3,
            Divide = 4
        }
    }
}
