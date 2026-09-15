using System.Collections;

namespace FunctionalDataStructures;

public class StateHistoryQueue<T> : IEnumerable<T>
{
    private readonly StateHistory<IQueue<T>> _stateHistory = new StateHistory<IQueue<T>>(Queue<T>.Empty);
    public bool CanUndo => _stateHistory.CanUndo;
    public bool CanRedo => _stateHistory.CanRedo;
    public bool IsEmpty => _stateHistory.State.IsEmpty;

    public void Execute(IQueue<T> newState) => _stateHistory.Execute(newState);
    public IQueue<T> Undo() => _stateHistory.Undo();
    public IQueue<T> Redo() => _stateHistory.Redo();
    public void Enqueue(T item) => Execute(_stateHistory.State.Enqueue(item));
    public T Dequeue() 
    {
        T result = _stateHistory.State.Peek();
        Execute(_stateHistory.State.Dequeue());
        return result;
    }

    public IEnumerator<T> GetEnumerator() => _stateHistory.State.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}