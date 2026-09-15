namespace FunctionalDataStructures;

// T is an immutable Queue<T> representing the state.
// For example, the sequence of states as follows:
// Q0 = []
// Q1 = [A]
// Q2 = [A, B]
// Q3 = [A, B, C]
// _undoStack = [Q0, Q1, Q2] (top to bottom)
// State: Q3
// _redoStack = [] (top to bottom)

// Call undo 
// _undoStack = [Q0, Q1] (top to bottom)
// State = Q2
// _redoStack = [Q3] (top to bottom)

// Call redo
// _undoStack = [Q0, Q1, Q2] (top to bottom)
// State = Q3
// _redoStack = [] (top to bottom)
public class StateHistory<T>
{
    private IStack<T> _undoStack = Stack<T>.Empty;
    private IStack<T> _redoStack = Stack<T>.Empty;
    public T State { get; private set; }

    public StateHistory(T initialState)
    {
        State = initialState;
    }

    public bool CanUndo => !_undoStack.IsEmpty;
    public bool CanRedo => !_redoStack.IsEmpty;

    public void Execute(T newState)
    {
        _undoStack = _undoStack.Push(State);
        State = newState;
        _redoStack = Stack<T>.Empty;
    }
    public T Undo()
    {
        if (!CanUndo) 
            throw new InvalidOperationException("Cannot undo");
        _redoStack = _redoStack.Push(State);
        State = _undoStack.Peek();
        _undoStack = _undoStack.Pop();
        return State;
    }

    public T Redo()
    {
        if (!CanRedo) 
            throw new InvalidOperationException("Cannot redo");
        _undoStack = _undoStack.Push(State);
        State = _redoStack.Peek();
        _redoStack = _redoStack.Pop();
        return State;
    }
}