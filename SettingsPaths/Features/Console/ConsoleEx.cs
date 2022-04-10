namespace Features;
internal static class ConsoleEx
{
    public static T ReadLine<T>(string message)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine() ?? String.Empty;
        return (T)Convert.ChangeType(input, typeof(T));
    }
}
