using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockBridge.Domain.Common;

namespace StockBridge.Infrastructure.Persistence.ValueConverters;

public class EnumValueConverter<TEnum> : ValueConverter<TEnum, string>
    where TEnum : struct, Enum
{
    public EnumValueConverter()
        : base(
            value => value.GetEnumValue(),
            value => FromDbValue(value))
    {
    }

    private static TEnum FromDbValue(string value)
    {
        return value.GetEnumByValue<TEnum>()
            ?? throw new InvalidOperationException($"Unknown value '{value}' for enum '{nameof(TEnum)}'.");
    }
}