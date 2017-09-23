using System;

public class SurveyCalculations
{
    private static bool weAreInDebugMode = true;
    public Tuple<bool,bool> ShowGenderCounterResult(double totalVotes, int globalPercent, int yourChoicePercent, int alternateChoicePercent )
    {
        var currentShowStatus = false;
        currentShowStatus = 
            //if there is more than 20 votes in total 
            (totalVotes >= 20) 
            //AND if (
            //  ABS(yourChoice percent - otherchoice percent) >= 8%
            && (Math.Abs(alternateChoicePercent-yourChoicePercent) >= 8 
            //  OR yourChoicePercent >= 25% of the alternate choice percent 
                || ((Math.Abs(yourChoicePercent-alternateChoicePercent)/yourChoicePercent*100)>25));
            // then show the result

        return Tuple.Create(currentShowStatus,weAreInDebugMode);
    } 
}
