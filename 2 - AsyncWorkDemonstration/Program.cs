using System.Diagnostics;

List<string> DataToProcess = new() { "FirstData", "SecondData", "ThirdData" };

Console.WriteLine("Синхронная обработка:");
TimeSpan Time = RunProcessing(DataToProcess);

Console.WriteLine("\nАссинхронная обработка:");
TimeSpan AsyncTime = await RunAsyncProcessing(DataToProcess);

double TineDifference = Math.Abs(Time.TotalSeconds - AsyncTime.TotalSeconds);
Console.WriteLine($"\nРазница: {TineDifference:F3} сек.");

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

    Console.WriteLine($"Обработка заняла {sw.Elapsed.TotalSeconds:F3} сек.");
    return sw.Elapsed;
}

static TimeSpan RunProcessing(List<string> data)
{
    var sw = Stopwatch.StartNew();
    foreach (var d in data) Console.WriteLine(ProcessData(d));

    sw.Stop();

    Console.WriteLine($"Обработка заняла {sw.Elapsed.TotalSeconds:F3} сек.");
    return sw.Elapsed;
}

static async Task<string> ProcessDataAsync(string dataName)
{
    int AsyncDelaySec = 3;
    await Task.Delay(AsyncDelaySec * 1000);

    string result = $"Обработка {dataName} завершена за {AsyncDelaySec} сек.";

    return result;
}
static string ProcessData(string dataName)
{
    int ThreadDelaySec = 3;
    Thread.Sleep(ThreadDelaySec * 1000);

    string result = $"Обработка {dataName} завершена за {ThreadDelaySec} сек.";
   
    return result;
}