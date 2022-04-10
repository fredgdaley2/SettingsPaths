namespace Features;
internal static class ConsoleInteractions
{
    public static void DisplayDangerMessage(string msg)
    {
        var holdConsoleColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ForegroundColor = holdConsoleColor;
    }

    public static void DisplaySuccessMessage(string msg)
    {
        Console.WriteLine(msg);
    }

}
