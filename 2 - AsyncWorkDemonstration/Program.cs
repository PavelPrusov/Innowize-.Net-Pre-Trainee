using System.Diagnostics;

List<string> dataToProcess = new() { "FirstData", "SecondData", "ThirdData" };

Console.WriteLine("Синхронная обработка:");
TimeSpan time = RunProcessing(dataToProcess);
Console.WriteLine($"Обработка заняла {time.TotalSeconds:F3} сек.");

Console.WriteLine("\nАссинхронная обработка:");
TimeSpan asyncTime = await RunAsyncProcessing(dataToProcess);
Console.WriteLine($"Обработка заняла {asyncTime.TotalSeconds:F3} сек.");

double timeDifference = Math.Abs(time.TotalSeconds - asyncTime.TotalSeconds);
Console.WriteLine($"\nРазница: {timeDifference:F3} сек.");

static async Task<TimeSpan> RunAsyncProcessing(List<string> data)
{
    var sw = Stopwatch.StartNew();

    List<Task<string>> tasks = data.Select(d => ProcessDataAsync(d)).ToList();

    while (tasks.Count > 0)
    {
        Task<string> finished = await Task.WhenAny(tasks);
        Console.WriteLine(await finished);
        tasks.Remove(finished);
    }

    sw.Stop();

    return sw.Elapsed;
}

static TimeSpan RunProcessing(List<string> data)
{
    var sw = Stopwatch.StartNew();
    foreach (var d in data) Console.WriteLine(ProcessData(d));

    sw.Stop();

    return sw.Elapsed;
}

static async Task<string> ProcessDataAsync(string dataName)
{
    int asyncDelaySec = 3;
    await Task.Delay(asyncDelaySec * 1000);

    string result = $"Обработка {dataName} завершена за {asyncDelaySec} сек.";

    return result;
}
static string ProcessData(string dataName)
{
    int threadDelaySec = 3;
    Thread.Sleep(threadDelaySec * 1000);

    string result = $"Обработка {dataName} завершена за {threadDelaySec} сек.";
   
    return result;
}