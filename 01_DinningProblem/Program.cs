using System;
using System.Threading;

public class MainApplication
{
private const int NumberOfPhilosophers = 5;
private const int MealsPerPhilosopher = 3;

private const int ThinkingTime = 1000;
private const int EatingTime = 1500;

private static readonly Semaphore TableSemaphore =
new Semaphore(4, 4);

public static void Main(string[] args)
{
Console.WriteLine("Dinning problem");
Console.WriteLine();

Fork[] forks = new Fork[NumberOfPhilosophers];
Philosopher[] philosophers = new Philosopher[NumberOfPhilosophers];
Thread[] threads = new Thread[NumberOfPhilosophers];

for (int i = 0; i < NumberOfPhilosophers; i++)
{
forks[i] = new Fork(i);
}

for (int i = 0; i < NumberOfPhilosophers; i++)
{
Fork leftFork = forks[i];
Fork rightFork = forks[(i + 1) % NumberOfPhilosophers];

philosophers[i] = new Philosopher(
i,
leftFork,
rightFork,
MealsPerPhilosopher,
ThinkingTime,
EatingTime,
TableSemaphore);

threads[i] = new Thread(philosophers[i].Dine);
}

foreach (Thread t in threads)
{
t.Start();
}

foreach (Thread t in threads)
{
t.Join();
}

Console.WriteLine();
Console.WriteLine("Todos los filosofos comieron.");
}
}
