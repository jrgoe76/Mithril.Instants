namespace Mithril.Instants;

internal sealed class InstantFactory(
    ITimeZoneProvider timeZoneProvider) : IInstantFactory
{
    private readonly string _timeZone = timeZoneProvider.Get();

    public Instant Create(DateTimeOffset dateTime)
        => new (dateTime, _timeZone);

    public Instant Create(string dateTime)
        => Create(DateTimeOffset.Parse(dateTime));

    public Instant Create(DateTime dateTime)
        => Instant.FromLocal(dateTime, _timeZone);
}
