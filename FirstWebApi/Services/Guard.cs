using System.Runtime.CompilerServices;

namespace FirstWebApi.Services;

public static class Guard
{
    public static void IsNotNullOrString(string value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        if (string.IsNullOrWhiteSpace(value) && value != "string")
        {
            throw new ArgumentException($"Please enter valid value in {name}!");
        }
    }
    public static void IsNotNull(string value,[CallerArgumentExpression(nameof(value))] string name = "")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Please enter valid value in {name}!");
        }
    }
}
