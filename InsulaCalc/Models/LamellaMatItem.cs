
namespace InsulaCalc.Models;

public class LamellaMatItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int PipeDiameter { get; set; }

    public int InsulationThickness { get; set; }

    public decimal PipeLength { get; set; }

    public decimal RollArea { get; set; }

    public decimal RequiredArea { get; set; }

    public int RequiredRolls { get; set; }
}
