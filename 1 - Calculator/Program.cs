using Calculator_App;

Calculator calculator = new Calculator();

while (true)
{
    double firstNumber = 0;
    double secondNumber = 0;
    double result = 0;

    Console.WriteLine("Введите 1-ое число: ");

    while(!double.TryParse(Console.ReadLine(), out firstNumber))
    {
        Console.WriteLine("Неправильный формат данных");
    }

    Calculator.Operation operation = ReadOperation();

    Console.WriteLine("Введите 2-ое число: ");

    while(!double.TryParse(Console.ReadLine(), out secondNumber))
    {
        Console.WriteLine("Неправильный формат данных");
    }

    try
    {
        switch (operation)
        {
            case Calculator.Operation.Add: result = calculator.Sum(firstNumber, secondNumber); break;
            case Calculator.Operation.Subtract: result = calculator.Sub(firstNumber, secondNumber); break;
            case Calculator.Operation.Multiply: result = calculator.Mult(firstNumber, secondNumber); break;
            case Calculator.Operation.Divide: result = calculator.Div(firstNumber, secondNumber); break;
            default: throw new InvalidOperationException("неизвестная операция");
        }

        Console.WriteLine($"Результат: {Math.Round(result,3)}\n");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
   
    Console.WriteLine("Хотите продолжить? (y)");
    if (Console.ReadLine()?.ToLower() != "y")
        break;
}

static Calculator.Operation ReadOperation()
{
    Console.WriteLine("Выберите операцию:");
    Console.WriteLine($"{(int)Calculator.Operation.Add}. + (сложение)");
    Console.WriteLine($"{(int)Calculator.Operation.Subtract}. - (вычитание)");
    Console.WriteLine($"{(int)Calculator.Operation.Multiply}. * (умножение)");
    Console.WriteLine($"{(int)Calculator.Operation.Divide}. / (деление)");

    int operationNumber;
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out operationNumber) && Enum.IsDefined(typeof(Calculator.Operation), operationNumber))
            return (Calculator.Operation)operationNumber;
        
        Console.WriteLine("Неверная операция");
    }
}