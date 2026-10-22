namespace InsulaCalc.Services;

public class PipeInsulationCalculator
{
    public int CalculateRequiredSections(decimal requiredLength)
    {
        if (requiredLength <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requiredLength),
                "Required length must be greater than zero.");
        }

        return (int)Math.Ceiling(requiredLength / 1.2m);
    }
    public int CalculateFullPackages(int requiredSections, int sectionsPerPackage)
    {
        if (sectionsPerPackage <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sectionsPerPackage),
                "Package size must be greater than zero.");
        }

        return requiredSections / sectionsPerPackage;
    }
    public int CalculateLooseSections(int requiredSections, int sectionsPerPackage)
    {
        if (sectionsPerPackage <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sectionsPerPackage),
                "Package size must be greater than zero.");
        }

        return requiredSections % sectionsPerPackage;
    }

}
