using System;

public class ComputerSystem
{
    public bool InternetAvailable;
    public int MemoryAvailable;
    public int CpuLoad;

    public void CheckState()
    {
        Console.WriteLine("Checking system...");

        if (InternetAvailable == true)
        {
            Console.WriteLine("Internet OK");
        }
        else
        {
            Console.WriteLine("No Internet");
        }

        if (MemoryAvailable < 500)
        {
            Console.WriteLine("Low RAM!");
        }
        else
        {
            Console.WriteLine("RAM normal");
        }

        if (CpuLoad > 80)
        {
            Console.WriteLine("High CPU load!");
        }
        else
        {
            Console.WriteLine("CPU OK");
        }
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
    }
}
