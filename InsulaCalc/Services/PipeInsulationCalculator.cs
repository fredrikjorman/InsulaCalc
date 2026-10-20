namespace InsulaCalc.Services;

public class PipeInsulationCalculator
{
    public int CalculateRequiredSections(decimal requiredLength)
    {
        return (int)Math.Ceiling(requiredLength / 1.2m);
    }
    public int CalculateFullPackages(int requiredSections, int sectionsPerPackage)
    {
        return requiredSections / sectionsPerPackage;
    }
    public int CalculateLooseSections(int requiredSections, int sectionsPerPackage)
    {
        return requiredSections % sectionsPerPackage;
    }

}
