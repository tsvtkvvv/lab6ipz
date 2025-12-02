using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(ComputerStateP state);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class ComputerStateP
{
    public bool InternetOK { get; set; }
    public bool MemoryOK { get; set; }
    public bool CpuLoadOK { get; set; }

    public ComputerStateP(bool internetOK, int memory, int cpuLoad)
    {
        InternetOK = internetOK;
        MemoryOK = memory >= 500;
        CpuLoadOK = cpuLoad <= 80;
    }
}

public class ComputerSystemP : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public bool InternetAvailable { get; private set; }
    public int MemoryAvailable { get; private set; }
    public int CpuLoad { get; private set; }

    public void Attach(IObserver observer) => observers.Add(observer);
    public void Detach(IObserver observer) => observers.Remove(observer);

    public void Notify()
    {
        var state = GetState();
        foreach (var observer in observers)
        {
            observer.Update(state);
        }
    }

    public ComputerStateP GetState()
    {
        return new ComputerStateP(InternetAvailable, MemoryAvailable, CpuLoad);
    }

    public void SetSystemStatus(bool internet, int memory, int cpu)
    {
        Console.WriteLine($"\n Internet: {internet}, RAM: {memory} MB, CPU: {cpu}% ");

        InternetAvailable = internet;
        MemoryAvailable = memory;
        CpuLoad = cpu;

        Notify();
    }
}

public class ConsoleLogger : IObserver
{
    public void Update(ComputerStateP state)
    {
        Console.WriteLine("Console Logger ");
        Console.WriteLine($"Internet OK : {state.InternetOK}");
        Console.WriteLine($"Memory OK : {state.MemoryOK}");
        Console.WriteLine($"CPU Load OK : {state.CpuLoadOK}");
       
    }
}

public class CriticalAlertObserver : IObserver
{
    public void Update(ComputerStateP state)
    {
        if (!state.InternetOK || !state.MemoryOK || !state.CpuLoadOK)
        {
           
            Console.WriteLine("!!! [CRITICAL ALERT] ");
            Console.ResetColor();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var systemMonitor = new ComputerSystemP();

        systemMonitor.Attach(new ConsoleLogger());
        systemMonitor.Attach(new CriticalAlertObserver());

        

        systemMonitor.SetSystemStatus(
            internet: true,
            memory: 1024,
            cpu: 45
        );

       

        systemMonitor.SetSystemStatus(
            internet: false,
            memory: 650,
            cpu: 95
        );
    }
}