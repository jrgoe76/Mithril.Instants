using System.Diagnostics.CodeAnalysis;

namespace Mithril.Instants;

[ExcludeFromCodeCoverage]
public sealed class DefaultTimeZoneProvider : ITimeZoneProvider
{
    public static string TIME_ZONE = "America/New_York";

    public string Get()
        => TIME_ZONE;
}
