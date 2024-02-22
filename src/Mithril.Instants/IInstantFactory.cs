namespace Mithril.Instants;

public interface IInstantFactory
{
    public Instant Create(string dateTime);

    public Instant Create(DateTimeOffset dateTime);

    public Instant Create(DateTime dateTime);
}