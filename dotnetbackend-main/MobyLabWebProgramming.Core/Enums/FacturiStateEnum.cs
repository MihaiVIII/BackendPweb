using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

/// <summary>
/// This is and example of a smart enum, you can modify it however you see fit.
/// Note that the class is decorated with a JsonConverter attribute so that it is properly serialized as a JSON.
/// </summary>
[JsonConverter(typeof(SmartEnumNameConverter<FacturiStateEnum, string>))]
public sealed class FacturiStateEnum : SmartEnum<FacturiStateEnum, string>
{
    public static readonly FacturiStateEnum Processing = new(nameof(Processing), "Processing");
    public static readonly FacturiStateEnum InTranzit = new(nameof(InTranzit), "In Tranzit");
    public static readonly FacturiStateEnum Arrived = new(nameof(Arrived), "Products Arrived");
    public static readonly FacturiStateEnum Cancelled = new(nameof(Cancelled), "Cancelled");

    private FacturiStateEnum(string name, string value) : base(name, value)
    {
    }
}
