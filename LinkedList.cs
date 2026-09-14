using System.Text;

namespace FunctionalDataStructures;

abstract record LinkedList<T>
{
    public static readonly LinkedList<T> Empty = new EmptyList();

    // Private constructor closes the hierarchy: Cons and EmptyList (declared
    // inside this type) are the only two cases that can ever exist — no null,
    // no third case sneaking in from another file.
    private LinkedList() { }

    public bool IsEmpty => this is EmptyList;

    /// <summary>
    /// Adds a new element to the front of the linked list.
    /// This operation has a time complexity of O(1).
    /// </summary>
    /// <param name="value">The value to add to the front of the linked list.</param>
    /// <returns>A new linked list with the specified value added to the front.</returns>
    public LinkedList<T> Push(T value) => new Cons(value, this);

    /// <summary>
    /// Reverses the linked list. The original list remains unchanged.
    /// This operation has a time complexity of O(n), where n is the number of elements in the list.
    /// </summary>
    /// <returns>A new linked list with the elements in reverse order.</returns>
    public LinkedList<T> Reverse()
    {
        var reversed = Empty;
        for (var list = this; list is Cons(var value, var tail); list = tail)
            reversed = reversed.Push(value);
        return reversed;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var list = this; list is Cons(var value, var tail); list = tail)
            sb.Append(value).Append("->");
        return sb.Length == 0 ? "" : sb.Remove(sb.Length - 2, 2).ToString();
    }

    // Iterative equality/hash. Records auto-generate per-case Equals/GetHashCode
    // that would recurse into Tail (StackOverflowException risk on a long
    // list), so Cons/EmptyList below each explicitly suppress theirs and
    // defer to this one.
    public virtual bool Equals(LinkedList<T>? other)
    {
        var a = this;
        var b = other;
        while (a is Cons(var av, var at) && b is Cons(var bv, var bt))
        {
            if (!EqualityComparer<T>.Default.Equals(av, bv)) return false;
            a = at;
            b = bt;
        }
        return a is EmptyList && b is EmptyList;
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        for (var list = this; list is Cons(var value, var tail); list = tail)
            hash.Add(value);
        return hash.ToHashCode();
    }

    public sealed record Cons(T Value, LinkedList<T> Tail) : LinkedList<T>
    {
        // base.Equals (not Equals) — a plain virtual call would dispatch back
        // into the compiler-generated Equals(LinkedList<T>?) forwarder on
        // this very type and loop forever.
        public bool Equals(Cons? other) => base.Equals(other);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }

    private sealed record EmptyList : LinkedList<T>
    {
        public bool Equals(EmptyList? other) => base.Equals(other);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }
}
