using System;

public class SurveyCalculations
{
    private static bool weAreInDebugMode = true;
    public Tuple<bool,bool> ShowGenderCounterResult(double totalVotes, int globalPercent, int yourChoicePercent )
    {
        var currentShowStatus = false;
        currentShowStatus = 
            //if there is more than 20 votes in total 
            (totalVotes >= 20) 
            //AND if (
            //  ABS(yourChoice percent - otherchoice percent) >= 8%
            && (Math.Abs(50-yourChoicePercent) >= 4 
            //  OR yourChoicePercent >= 25% of global percent 
                || ((Math.Abs(globalPercent-yourChoicePercent)/globalPercent*100)>25));
            // then show the result

        return Tuple.Create(currentShowStatus,weAreInDebugMode);
    } 
}
