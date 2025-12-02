using System;


public class ComputerStateP
{
    public bool InternetOK { get; }
    public bool MemoryOK { get; }
    public bool CpuLoadOK { get; }

    public ComputerStateP(bool internetOK, int memory, int cpuLoad)
    {
        InternetOK = internetOK;
        MemoryOK = memory >= 500;
        CpuLoadOK = cpuLoad <= 80;
    }
}
public class ComputerSystemP
{
    public bool InternetAvailable;
    public int MemoryAvailable;
    public int CpuLoad;
    public ComputerStateP GetState()
    {
        return new ComputerStateP(InternetAvailable, MemoryAvailable, CpuLoad);

    }
}
public class ComputerSystem
{
    public bool InternetAvailable;
    public int MemoryAvailable;
    public int CpuLoad;

    public void CheckState()
    {
        PrintStatus("Internet", InternetAvailable, "Internet OK", "Without connection");
        PrintStatus("Memory", MemoryAvailable >= 256, "Memory OK", "Low memory");
        PrintStatus("CPU Load", CpuLoad < 75, "CPU Load OK", "High CPU Load");
    }

    public void PrintStatus(string name, bool condition, string okMessage, string badMessage)
    {
        Console.WriteLine(condition ? okMessage : badMessage);
    }


}

class Program
{
    static void Main()
    {
        ComputerSystem system = new ComputerSystem();
        system.InternetAvailable = true;
        system.MemoryAvailable = 300;
        system.CpuLoad = 63;

        system.CheckState();
        system.PrintStatus("Custom Check",
            system.MemoryAvailable > 512,
            "Sufficient Memory",
            "Insufficient Memory");



        var sys2 = new ComputerSystemP
        {
            InternetAvailable = false,
            MemoryAvailable = 1024,
            CpuLoad = 45
        };

        var state = sys2.GetState();
        Console.WriteLine($"Internet OK: {state.InternetOK}");
        Console.WriteLine($"Memory OK: {state.MemoryOK}");
        Console.WriteLine($"CPU Load OK: {state.CpuLoadOK}");
    }
}