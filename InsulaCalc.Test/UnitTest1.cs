using InsulaCalc.Services;

namespace InsulaCalc.Test;

public class PipeInsulationCalculatorTests
{
    [Fact]
    public void CalculateRequiredSections_WhenLengthIs13_ShouldReturn11()
    {
        // Arrange
        var calculator = new PipeInsulationCalculator();

        // Act
        var result = calculator.CalculateRequiredSections(13);

        // Assert
        Assert.Equal(11, result);

    }
    [Fact]
    public void CalculateFullPackages_When11SectionsAnd5PerPackage_ShouldReturn2()
    {
        // Arrange
        var calculator = new PipeInsulationCalculator();

        // Act
        var result = calculator.CalculateFullPackages(11, 5);

        // Assert
        Assert.Equal(2, result);
    }
    [Fact]
    public void CalculateLooseSections_When11SectionsAnd5PerPackage_ShouldReturn1()
    {
        // Arrange
        var calculator = new PipeInsulationCalculator();

        // Act
        var result = calculator.CalculateLooseSections(11, 5);

        // Assert
        Assert.Equal(1, result);
    }
}
