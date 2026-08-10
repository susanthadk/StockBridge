namespace StockBridge.Domain.Common;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}