using _1___Calculator;

Calculator calculator = new Calculator();

while (true)
{
    double firstNumber = 0;
    double secondNumber = 0;

    int operationNumber = 0;

    double result = 0;

    Console.WriteLine("Введите 1-ое число: ");

    while(!double.TryParse(Console.ReadLine(), out firstNumber))
    {
        Console.WriteLine("Неправильный формат данных");
    }

    Console.WriteLine("Выберите операцию:");
    Console.WriteLine("1.+ (сложение)");
    Console.WriteLine("2.- (вычитание)");
    Console.WriteLine("3.* (умножение)");
    Console.WriteLine("4./ (деление)");
    Console.WriteLine();

    while (!int.TryParse(Console.ReadLine(), out operationNumber)||(operationNumber>4||operationNumber<1))
    {
        Console.WriteLine("неверная операция");
    }

    Console.WriteLine("Введите 2-ое число: ");

    while(!double.TryParse(Console.ReadLine(), out secondNumber))
    {
        Console.WriteLine("Неправильный формат данных");
    }

    try
    {
        switch (operationNumber)
        {
            case 1: result = calculator.Sum(firstNumber, secondNumber); break;
            case 2: result = calculator.Sub(firstNumber, secondNumber); break;
            case 3: result = calculator.Mult(firstNumber, secondNumber); break;
            case 4: result = calculator.Div(firstNumber, secondNumber); break;
            default: throw new InvalidOperationException("неизвестная операция");
        }

        Console.WriteLine($"Результат: {result}\n");
    }
    catch(Exception e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
   
    Console.WriteLine("Хотите продолжить? (y)");
    if (Console.ReadLine()?.ToLower() != "y")
        break;
}