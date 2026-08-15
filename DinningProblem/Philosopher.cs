using System;
using System.Threading;

public class Philosopher
{
private int id;
private Fork leftFork;
private Fork rightFork;

private int maxMeals;
private int thinkingTime;
private int eatingTime;

private Semaphore tableSemaphore;

public Philosopher(
int id,
Fork leftFork,
Fork rightFork,
int maxMeals,
int thinkingTime,
int eatingTime,
Semaphore tableSemaphore)
{
this.id = id;
this.leftFork = leftFork;
this.rightFork = rightFork;
this.maxMeals = maxMeals;
this.thinkingTime = thinkingTime;
this.eatingTime = eatingTime;
this.tableSemaphore = tableSemaphore;
}

public void Dine()
{
for (int meal = 1; meal <= maxMeals; meal++)
{
Think();

Console.WriteLine(
$"Filosofo {id} tiene hambre (Comida {meal})");

tableSemaphore.WaitOne();

Fork firstFork;
Fork secondFork;

// Asimetria para evitar deadlock
if (id % 2 == 0)
{
firstFork = rightFork;
secondFork = leftFork;
}
else
{
firstFork = leftFork;
secondFork = rightFork;
}

bool firstForkTaken = false;
bool secondForkTaken = false;

try
{
firstForkTaken = firstFork.PickUp(500);

if (!firstForkTaken)
{
meal--;
continue;
}

Console.WriteLine(
$"Filosofo {id} tomo tenedor {firstFork.Id}");

secondForkTaken = secondFork.PickUp(500);

if (!secondForkTaken)
{
Console.WriteLine(
$"Filosofo {id} no pudo tomar el segundo tenedor");

meal--;
continue;
}

Console.WriteLine(
$"Filosofo {id} tomo tenedor {secondFork.Id}");

Eat(meal);
}
finally
{
if (secondForkTaken)
{
secondFork.PutDown();

Console.WriteLine(
$"Filosofo {id} solto tenedor {secondFork.Id}");
}

if (firstForkTaken)
{
firstFork.PutDown();

Console.WriteLine(
$"Filosofo {id} solto tenedor {firstFork.Id}");
}

tableSemaphore.Release();
}

Console.WriteLine(
$"Filosofo {id} termino comida {meal}");

Console.WriteLine();
}

Console.WriteLine(
$"Filosofo {id} termino todas sus comidas.");
}

private void Think()
{
Console.WriteLine(
$"Filosofo {id} esta pensando...");

Thread.Sleep(thinkingTime);
}

private void Eat(int meal)
{
Console.WriteLine(
$"Filosofo {id} esta comiendo ({meal})");

Thread.Sleep(eatingTime);
}
}
