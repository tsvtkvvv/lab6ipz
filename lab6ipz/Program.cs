using System;

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

    private void PrintStatus(string name, bool condition, string okMessage, string badMessage)
    {
        Console.WriteLine(condition ? okMessage : badMessage);
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
            system.PrintStatus("Custom Check", system.MemoryAvailable > 512, "Sufficient Memory", "Insufficient Memory");
        }
    }
}
