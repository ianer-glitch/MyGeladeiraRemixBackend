namespace Extensions;

public static class TypeExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? list)
    {
        return list == null || !list.Any(); 
    }
}