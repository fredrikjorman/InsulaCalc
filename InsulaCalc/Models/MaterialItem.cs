namespace InsulaCalc.Models;

public class MaterialItem
{
    public Guid Id { get; set; }
    public int Diameter { get; set; }
    public int InsulationThickness { get; set; }
    public decimal RequiredLength { get; set; }
    public int SectionsPerPackage { get; set; }
    public int RequiredSections { get; set; }
    public int FullPackages { get; set; }
    public int LooseSections { get; set; }

}