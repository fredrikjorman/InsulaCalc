using System.Text.Json;
using InsulaCalc.Models;

namespace InsulaCalc.Services;

public class ProjectService
{
    private readonly string _filePath;

    public ProjectService()
    {
        _filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Data",
            "projects.json"
        );
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Project>();
        }

        string json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Project>();
        }

        return JsonSerializer.Deserialize<List<Project>>(json)
               ?? new List<Project>();
    }

    public async Task SaveProjectAsync(Project project)
    {
        var projects = await GetAllProjectsAsync();

        projects.Add(project);

        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(projects, options);

        await File.WriteAllTextAsync(_filePath, json);
    }


}
