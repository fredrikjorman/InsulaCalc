
using InsulaCalc.Services;
using Xunit;

namespace InsulaCalc.Test;

public class LamellaMatCalculatorTests
{
    [Fact]
    public void CalculateRequiredArea_ShouldReturnCorrectArea()
    {
        // Arrange
        var calculator = new LamellaMatCalculator();

        // Act
        var result = calculator.CalculateRequiredArea(200, 50, 10m);

        // Assert
        Assert.Equal(9.42m, result);
    }

    [Fact]
    public void CalculateRequiredRolls_ShouldRoundUp()
    {
        // Arrange
        var calculator = new LamellaMatCalculator();

        // Act
        var result = calculator.CalculateRequiredRolls(9.42m, 6m);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculateRequiredArea_ShouldThrowException_WhenDiameterIsInvalid()
    {
        // Arrange
        var calculator = new LamellaMatCalculator();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            calculator.CalculateRequiredArea(0, 50, 10m));
    }

    [Fact]
    public void CalculateRequiredRolls_ShouldThrowException_WhenRollAreaIsInvalid()
    {
        // Arrange
        var calculator = new LamellaMatCalculator();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            calculator.CalculateRequiredRolls(9.42m, 0m));
    }
}
