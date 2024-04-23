using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums;

/// <summary>
/// This is and example of a smart enum, you can modify it however you see fit.
/// Note that the class is decorated with a JsonConverter attribute so that it is properly serialized as a JSON.
/// </summary>
[JsonConverter(typeof(SmartEnumNameConverter<FeedbackEnum, string>))]
public sealed class FeedbackEnum : SmartEnum<FeedbackEnum, string>
{
    public static readonly FeedbackEnum Unsatisfactory = new(nameof(Unsatisfactory), "Unsatisfactory");
    public static readonly FeedbackEnum Satisfactory = new(nameof(Satisfactory), "Satisfactory");
    public static readonly FeedbackEnum Decent = new(nameof(Decent), "Decent");
    public static readonly FeedbackEnum Near_Perfect = new(nameof(Near_Perfect), "Near Perfect");

    private FeedbackEnum(string name, string value) : base(name, value)
    {
    }
}
