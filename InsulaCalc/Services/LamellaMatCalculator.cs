
namespace InsulaCalc.Services;

public class LamellaMatCalculator
{
    public decimal CalculateRequiredArea(
        int pipeDiameter,
        int insulationThickness,
        decimal pipeLength)
    {
        if (pipeDiameter <= 0)
            throw new ArgumentException("Pipe diameter must be greater than zero.");

        if (insulationThickness <= 0)
            throw new ArgumentException("Insulation thickness must be greater than zero.");

        if (pipeLength <= 0)
            throw new ArgumentException("Pipe length must be greater than zero.");

        decimal outerDiameter = pipeDiameter + (2 * insulationThickness);

        decimal circumference = (decimal)Math.PI * outerDiameter / 1000m;

        decimal requiredArea = circumference * pipeLength;

        return Math.Round(requiredArea, 2);
    }

    public int CalculateRequiredRolls(decimal requiredArea, decimal rollArea)
    {
        if (requiredArea <= 0)
            throw new ArgumentException("Required area must be greater than zero.");

        if (rollArea <= 0)
            throw new ArgumentException("Roll area must be greater than zero.");

        return (int)Math.Ceiling(requiredArea / rollArea);
    }
}
