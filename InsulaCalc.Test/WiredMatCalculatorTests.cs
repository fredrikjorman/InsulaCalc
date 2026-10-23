
using InsulaCalc.Services;

namespace InsulaCalc.Test;

public class WiredMatCalculatorTests
{
    [Fact]
    public void CalculateRequiredArea_ShouldReturnCorrectArea()
    {
        // Arrange
        var calculator = new WiredMatCalculator();

        // Act
        var result = calculator.CalculateRequiredArea(
            100, 50, 10m);

        // Assert
        Assert.Equal(6.28m, result);
    }

    [Fact]
    public void CalculateRequiredRolls_ShouldRoundUp()
    {
        // Arrange
        var calculator = new WiredMatCalculator();

        // Act
        var result = calculator.CalculateRequiredRolls(6.28m, 5m);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculateRequiredArea_ShouldThrow_WhenPipeDiameterIsZero()
    {
        // Arrange
        var calculator = new WiredMatCalculator();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            calculator.CalculateRequiredArea(0, 50, 10m));
    }

    [Fact]
    public void CalculateRequiredRolls_ShouldThrow_WhenRollAreaIsZero()
    {
        // Arrange
        var calculator = new WiredMatCalculator();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            calculator.CalculateRequiredRolls(6.28m, 0m));
    }

}
