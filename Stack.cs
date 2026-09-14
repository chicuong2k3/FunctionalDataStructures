using System.Collections;

namespace FunctionalDataStructures;

record Stack<T> : IStack<T>
{
    private sealed record EmptyStack : IStack<T>
    {
        public IStack<T> Push(T item) => new Stack<T>(item, this);

        public T Peek() => throw new InvalidOperationException("Stack is empty.");

        public IStack<T> Pop() => throw new InvalidOperationException("Stack is empty.");

        public bool IsEmpty => true;

        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static readonly IStack<T> Empty = new EmptyStack();

    public Stack(T item, IStack<T> tail)
    {
        _item = item;
        _tail = tail;
    }

    private readonly T _item;
    private readonly IStack<T> _tail;
    public IStack<T> Push(T item) => new Stack<T>(item, this);

    public T Peek() => _item;

    public IStack<T> Pop() => _tail;

    public IEnumerator<T> GetEnumerator()
    {
        for (IStack<T> current = this; !current.IsEmpty; current = current.Pop())
            yield return current.Peek();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool IsEmpty => false;
}