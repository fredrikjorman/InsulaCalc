
using InsulaCalc.Models;
using InsulaCalc.Services;

namespace InsulaCalc.Test;

public class ProjectServiceTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        // Each test gets its own temporary JSON file.
        _testDirectory = Path.Combine(
            Path.GetTempPath(),
            "InsulaCalcTests",
            Guid.NewGuid().ToString());

        Directory.CreateDirectory(_testDirectory);

        string filePath = Path.Combine(
            _testDirectory,
            "projects.json");

        _service = new ProjectService(filePath);
    }

    [Fact]
    public async Task SaveProject_ShouldCreateNewProject()
    {
        // Arrange
        var project = new Project
        {
            Name = "Test Project",
            Description = "Test description"
        };

        // Act
        await _service.SaveProjectAsync(project);
        var projects = await _service.GetAllProjectsAsync();

        // Assert
        Assert.Single(projects);
        Assert.Equal("Test Project", projects[0].Name);
    }

    [Fact]
    public async Task GetProjectById_ShouldReturnCorrectProject()
    {
        // Arrange
        var project = new Project
        {
            Name = "Insulation Project"
        };

        await _service.SaveProjectAsync(project);

        // Act
        var result = await _service.GetProjectByIdAsync(project.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(project.Id, result.Id);
    }

    [Fact]
    public async Task SaveProject_ShouldSaveMaterials()
    {
        // Arrange
        var project = new Project
        {
            Name = "Material Test",
            Materials = new List<ProjectMaterial>
            {
                new ProjectMaterial
                {
                    MaterialType = "Pipe Sections",
                    Description = "Pipe 114/30",
                    Length = 12,
                    Packages = 3,
                    LooseSections = 1
                }
            }
        };

        // Act
        await _service.SaveProjectAsync(project);
        var saved = await _service.GetProjectByIdAsync(project.Id);

        // Assert
        Assert.NotNull(saved);
        Assert.Single(saved.Materials);
        Assert.Equal("Pipe Sections", saved.Materials[0].MaterialType);
        Assert.Equal(3, saved.Materials[0].Packages);
    }

    [Fact]
    public async Task SaveProject_ShouldUpdateExistingProject()
    {
        // Arrange
        var project = new Project
        {
            Name = "Original Name"
        };

        await _service.SaveProjectAsync(project);

        // Act
        project.Name = "Updated Name";
        await _service.SaveProjectAsync(project);

        var saved = await _service.GetProjectByIdAsync(project.Id);
        var allProjects = await _service.GetAllProjectsAsync();

        // Assert
        Assert.NotNull(saved);
        Assert.Equal("Updated Name", saved.Name);
        Assert.Single(allProjects);
    }

    [Fact]
    public async Task DeleteProject_ShouldRemoveProject()
    {
        // Arrange
        var project = new Project
        {
            Name = "Delete Test"
        };

        await _service.SaveProjectAsync(project);

        // Act
        bool deleted = await _service.DeleteProjectAsync(project.Id);
        var saved = await _service.GetProjectByIdAsync(project.Id);

        // Assert
        Assert.True(deleted);
        Assert.Null(saved);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, recursive: true);
        }
    }
}
