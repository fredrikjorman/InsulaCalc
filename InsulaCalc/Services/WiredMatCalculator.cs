
namespace InsulaCalc.Services;

public class WiredMatCalculator
{
    public decimal CalculateRequiredArea(
        int pipeDiameter,
        int insulationThickness,
        decimal pipeLength)
    {
        if (pipeDiameter <= 0 ||
            insulationThickness <= 0 ||
            pipeLength <= 0)
        {
            throw new ArgumentException(
                "All measurements must be greater than zero.");
        }

        decimal outerDiameter =
            pipeDiameter + (2m * insulationThickness);

        decimal circumference =
            (decimal)Math.PI * (outerDiameter / 1000m);

        decimal requiredArea = circumference * pipeLength;

        return Math.Round(requiredArea, 2);
    }

    public int CalculateRequiredRolls(
        decimal requiredArea,
        decimal rollArea)
    {
        if (requiredArea <= 0 || rollArea <= 0)
        {
            throw new ArgumentException(
                "Area values must be greater than zero.");
        }

        return (int)Math.Ceiling(requiredArea / rollArea);
    }

}
