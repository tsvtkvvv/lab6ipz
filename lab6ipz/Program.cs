using System;
using System.Collections.Generic;
using System.Net.Mail;

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
public abstract class ObserverBaseP : IObserver

{
    protected int updateCount = 0;
    public void Update(ComputerStateP state)
    {
        updateCount++;
        Console.WriteLine($"Update count: {updateCount}");
        LogUpdate(state);
    }
    protected abstract void LogUpdate(ComputerStateP state);
}

public class ConsoleLogger : ObserverBaseP
{
    private readonly string LogPrefix = "Console Logger: ";

    protected override void LogUpdate(ComputerStateP state)
    {
        Console.WriteLine($"{LogPrefix} Internet OK : {state.InternetOK}");
        Console.WriteLine($"{LogPrefix} Memory OK : {state.MemoryOK}");
        Console.WriteLine($"{LogPrefix} CPU Load OK : {state.CpuLoadOK}");
    }
}
public class CriticalAlertObserever : ObserverBaseP
{
    
    protected override void LogUpdate(ComputerStateP state)
    {
        if (!state.InternetOK || !state.MemoryOK || !state.CpuLoadOK)
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! [CRITICAL ALERT] ");
            Console.ResetColor();
        }
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





public class Program
{
    public static void Main(string[] args)
    {
        var systemMonitor = new ComputerSystemP();

        systemMonitor.Attach(new ConsoleLogger());
        systemMonitor.Attach(new CriticalAlertObserever());

        

        systemMonitor.SetSystemStatus(
            internet: true,
            memory: 1024,
            cpu: 45
        );

       

        systemMonitor.SetSystemStatus(
            internet: true,
            memory: 650,
            cpu: 95
        );
    }
}