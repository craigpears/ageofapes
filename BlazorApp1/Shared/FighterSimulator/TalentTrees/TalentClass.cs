namespace BlazorApp1.Shared.FighterSimulator.TalentTrees;

public abstract class TalentClass
{
    protected static List<double> HalfPercentSteps = new List<double> { 0.5, 1, 1.5};
    protected static List<double> SinglePercentSteps = new List<double> { 1, 2, 3};
    protected static List<double> OneAndHalfPercentSteps = new List<double> { 1.5, 3.0, 4.5};
    
    protected static List<double> OnePercentSteps = new List<double> { 1.0, 2.0, 3.0 };
    protected static List<double> TwoPercentSteps = new List<double> { 2.0, 4.0, 6.0 };
    protected static List<double> ThreePercentSteps = new List<Double> { 3.0, 6.0, 9.0 };
    protected static List<double> FivePercentSteps = new List<Double> { 5.0, 10.0, 15.0 };
    
    protected static List<double> TwoHalfPercentSteps = new List<double> { 0.5, 1};
    protected static List<double> TwoSinglePercentSteps = new List<double> { 1.0, 2.0};
    protected static List<double> TwoOneAndHalfPercentSteps = new List<double> { 1.5, 3.0};

    protected static List<double> HalfPercent = new List<double> { 0.5 };
    protected static List<double> OnePercent = new List<double> { 1 };
    protected static List<double> OneAndAHalfPercent = new List<double> { 1.5 };
    
    protected static List<double> MinusOnePercentSteps = new List<double> { -1, -2, -3 };
    protected static List<double> MinusHalfPercentSteps = new List<double> { -0.5, -0.1, -1.5 };
}