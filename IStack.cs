namespace FunctionalDataStructures;

public interface IStack<T> : IEnumerable<T>
{
    IStack<T> Push(T item);
    T Peek();
    IStack<T> Pop();
    bool IsEmpty { get; }
}
