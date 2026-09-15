namespace FunctionalDataStructures;

public static partial class Extensions
{
    public static string Bracket<T>(this IStack<T> stack)
    {
        var items = stack.ToList();
        return "[" + string.Join(", ", items) + "]";
    }
    public static string Bracket<T>(this IQueue<T> queue)
    {
        var items = queue.ToList();
        return "[" + string.Join(", ", items) + "]";
    }
}
