
namespace InsulaCalc.Models;

public class ProjectMaterial
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string MaterialType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Length { get; set; }

    public int Diameter { get; set; }

    public int Thickness { get; set; }

    public int Packages { get; set; }

    public int LooseSections { get; set; }

    public int Rolls { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.Now;
}
