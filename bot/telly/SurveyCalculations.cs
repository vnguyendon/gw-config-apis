using System;
using Gw.Bot.Sequences.Actions;

public class SurveyShowConditions : ISurveyShowConditions
{
    private int deltaPercent = 3;
    private int minVotes = 20;
    public string DebugVisualInfo { get; } = " *";
    public bool IsDebugModeActivated { get; } = true;
    public bool ShowPercentAgeRange(CounterValues ageRangeCounter, CounterValues globalCounter)
    {
        return (Math.Abs(globalCounter.YourPercentage - ageRangeCounter.YourPercentage) >= deltaPercent)
                && (ageRangeCounter.Total >= minVotes);
    }

    public bool ShowPercentCountry(CounterValues countryCounter, CounterValues globalCounter)
    {
        return (Math.Abs(globalCounter.YourPercentage - countryCounter.YourPercentage) >= deltaPercent)
                && (countryCounter.Total >= minVotes);
    }

    public bool ShowPercentGender(CounterValues genderCounter, CounterValues globalCounter)
    {
        return (Math.Abs(globalCounter.YourPercentage - genderCounter.YourPercentage) >= deltaPercent)
                && (genderCounter.Total >= minVotes);
    }

    public bool ShowPercentVoters(CounterValues globalCounter)
    {
        return true;
    }

    public bool ShowTotalVoters(CounterValues globalCounter)
    {
        return globalCounter.Total >= minVotes;
    }
}
