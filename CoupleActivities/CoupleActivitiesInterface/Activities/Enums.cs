namespace CoupleActivities.Activities
{
    public enum DaysOpen
    {
        None,
        Always,
        Week,
        Weekend,
        AlwaysButSunday
    }

    public enum Price
    {
        None,
        Cheap,
        Medium,
        Expensive
    }

    public enum EnergyNeeded
    {
        None,
        NoEnergy,
        LotOfEnergy
    }

    public enum Transport
    {
        None,
        WalkBike,
        Subway,
        Car
    }

    public enum ActivityDuration
    {
        None,
        Short,
        Medium,
        Long
    }

    public enum Temperature
    {
        None,
        DoesntMatter,
        Hot,
        Cold
    }

    public enum Category
    {
        None,
        Food,
        CulturalActivity,
        Sport,
        Alcohol,
        Outside,
        Inside,
        Travel
    }
}
