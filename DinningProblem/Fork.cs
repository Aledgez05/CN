using System.Threading;

public class Fork
{
private readonly object forkLock = new object();

public int Id { get; }

public Fork(int id)
{
Id = id;
}

public bool PickUp(int timeout)
{
return Monitor.TryEnter(forkLock, timeout);
}

public void PutDown()
{
Monitor.Exit(forkLock);
}
}
