namespace FunctionalDataStructures;

public static partial class Extensions
{
    public static string Bracket<T>(this IEnumerable<T> items) =>
        "[" + string.Join(", ", items) + "]";
}
