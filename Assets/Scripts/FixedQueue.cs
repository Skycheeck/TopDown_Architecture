using System.Collections.Generic;

public class FixedQueue<T>
{
    private readonly Queue<T> _queue;
    private readonly int _limit;
    public int Count => _queue.Count;
    
    public FixedQueue(int capacity)
    {
        _queue = new Queue<T>(capacity);
        _limit = capacity;
    }

    public FixedQueue(IEnumerable<T> collection, int limit)
    {
        _queue = new Queue<T>(collection);
        _limit = limit;
    }

    public bool Enqueue(T item)
    {
        if (_queue.Count >= _limit) return false;
        _queue.Enqueue(item);
        return true;
    }

    public T Dequeue() => _queue.Dequeue();

    public IEnumerable<T> GetImmutable() => _queue;
}