// Simualted I/O method

//using System.Diagnostics;

//async Task SimulateDownloadAsync(string fileName, int ms)
//{
//    Console.WriteLine($"Starting {fileName}");
//    await Task.Delay(ms);   // pretend we're doing I/O
//    Console.WriteLine($"Finished {fileName}");
//}

// Sequential                   Sequential: 4011 ms
//var sw = Stopwatch.StartNew();
//await SimulateDownloadAsync("file1.txt", 2000);
//await SimulateDownloadAsync("file2.txt", 2000);
//sw.Stop();
//Console.WriteLine($"Sequential: {sw.ElapsedMilliseconds} ms");

// Parallel with Task.WhenAll.  Parallel: 2001 ms

//sw.Restart();
//var t1 = SimulateDownloadAsync("file1.txt", 2000);
//var t2 = SimulateDownloadAsync("file2.txt", 2000);
//await Task.WhenAll(t1, t2);
//sw.Stop();
//Console.WriteLine($"Parallel: {sw.ElapsedMilliseconds} ms");

using System.Diagnostics;

async Task<string> SimulateDownloadAsync(string fileName, int ms)
{
    Console.WriteLine($"Starting {fileName}");
    await Task.Delay(ms);
    if (fileName.Contains("fail"))
        throw new InvalidOperationException($"Download failed: {fileName}");
    Console.WriteLine($"Finished {fileName}");
    return $"Content of {fileName}";
}

async Task SpinnerUntilCompleted(Task task)
{
    char[] frames = { '|', '/', '-', '\\' };
    int i = 0;
    while (!task.IsCompleted)
    {
        Console.Write($"\r{frames[i++ % frames.Length]} working...");
        await Task.Delay(100);
    }
    Console.Write("\rDone!            \n");
}

var sw = Stopwatch.StartNew();

// Sequential
await SimulateDownloadAsync("s1.txt", 1500);
await SimulateDownloadAsync("s2.txt", 1500);
sw.Stop();
Console.WriteLine($"Sequential: {sw.ElapsedMilliseconds} ms\n");

// Parallel
sw.Restart();
var p1 = SimulateDownloadAsync("p1.txt", 1500);
var p2 = SimulateDownloadAsync("p2.txt", 1500);
await Task.WhenAll(p1, p2);
sw.Stop();
Console.WriteLine($"Parallel: {sw.ElapsedMilliseconds} ms\n");

// Error handling
try
{
    await SimulateDownloadAsync("fail.txt", 500);
}
catch (Exception ex)
{
    Console.WriteLine($"Caught: {ex.Message}\n");
}

// Returning values
var tasks = new[]
{
    SimulateDownloadAsync("a.txt", 1000),
    SimulateDownloadAsync("b.txt", 1200),
    SimulateDownloadAsync("c.txt", 800),
};
string[] results = await Task.WhenAll(tasks);
foreach (var r in results) Console.WriteLine(r);
Console.WriteLine();

// Bonus: spinner
var big = SimulateDownloadAsync("big.bin", 3000);
await SpinnerUntilCompleted(big);
Console.WriteLine(await big);