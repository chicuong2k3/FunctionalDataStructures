using System.Collections;

namespace FunctionalDataStructures;

// the idea is to implement a queue using two stacks
// one stack is used for enqueue operations and the other for dequeue operations
// if the queue is empty, both stacks are empty
// if the queue has elements, the dequeue stack must have elements
// there is no situation where the enqueue stack has elements but the dequeue stack is empty

public record Queue<T> : IQueue<T>
{
    private sealed record EmptyQueue() : IQueue<T>
    {
        public bool IsEmpty => true;

        public IQueue<T> Dequeue()
        {
            return Queue<T>.Empty;
        }

        public IQueue<T> Enqueue(T item)
        {
            return new Queue<T>(Stack<T>.Empty, Stack<T>.Empty.Push(item));
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        public T Peek() => throw new InvalidOperationException("Cannot peek into an empty queue.");

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public static IQueue<T> Empty { get; } = new EmptyQueue();

    private readonly IStack<T> _enqueueStack = Stack<T>.Empty;
    private readonly IStack<T> _dequeueStack = Stack<T>.Empty;

    public bool IsEmpty => _dequeueStack.IsEmpty;

    private Queue(IStack<T> enqueueStack, IStack<T> dequeueStack)
    {
        _enqueueStack = enqueueStack;
        _dequeueStack = dequeueStack;
    }

    public IQueue<T> Dequeue()
    {
        var newDequeueStack = _dequeueStack.Pop();
        // the dequeue is not empty after popping, meaning that the previous dequeue stack had 2 or more elements
        // so the new queue is the same as old one with the dequeue stack popped
        // this is the best case scenario with O(1) complexity
        if (!newDequeueStack.IsEmpty)
            return new Queue<T>(_enqueueStack, newDequeueStack);
        // the queue contains exactly one element, then dequeueing it will result in an empty queue
        if (_enqueueStack.IsEmpty)
            return Queue<T>.Empty;
        // otherwise, the dequeue stack is has exactly one element so after popping it will be empty
        // we need to reverse the enqueue stack to become the new dequeue stack
        // reverse a stack takes O(n) time and O(n) space
        // where n is the number of elements in the stack
        return new Queue<T>(Stack<T>.Empty, _enqueueStack.Reverse());

        // the amortized complexity of dequeue operation is O(1)
    }

    public IQueue<T> Enqueue(T item) => 
        IsEmpty ? new Queue<T>(_enqueueStack, _dequeueStack.Push(item)) 
            : new Queue<T>(_enqueueStack.Push(item), _dequeueStack);

    public T Peek() => _dequeueStack.Peek();

    public IEnumerator<T> GetEnumerator()
    {
        for (IQueue<T> current = this; !current.IsEmpty; current = current.Dequeue())
            yield return current.Peek();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static partial class Extensions
{
    public static IQueue<T> Reverse<T>(this IQueue<T> queue)
    {
        var reversed = Queue<T>.Empty;
        for (var current = queue; !current.IsEmpty; current = current.Dequeue())
            reversed = reversed.Enqueue(current.Peek());
        return reversed;
    }
}