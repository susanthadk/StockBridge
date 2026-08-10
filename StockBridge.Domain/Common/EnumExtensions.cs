using System.Reflection;

namespace StockBridge.Domain.Common;

public static class EnumExtensions
{
    public static string GetEnumValue<T>(this T value)
        where T : struct, Enum
    {
        return value
            .GetType()
            .GetField(value.ToString())!
            .GetCustomAttribute<EnumValueAttribute>()?.Value
            ?? value.ToString();
    }

    public static T? GetEnumByValue<T>(this string value)
        where T : struct, Enum
    {
        foreach (var item in Enum.GetValues<T>())
        {
            if (string.Equals(item.GetEnumValue(), value, StringComparison.OrdinalIgnoreCase))
                return item;
        }

        return null;
    }
}