namespace FunctionalDataStructures;

public interface IQueue<T> : IEnumerable<T>
{
    IQueue<T> Enqueue(T item);
    IQueue<T> Dequeue();
    T Peek();
    bool IsEmpty { get; }
}