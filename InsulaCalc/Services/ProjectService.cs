
using System.Text.Json;
using InsulaCalc.Models;

namespace InsulaCalc.Services;

public class ProjectService
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _fileLock = new(1, 1);

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public ProjectService()
    {
        _filePath = Path.Combine(
            AppContext.BaseDirectory,
            "Data",
            "projects.json"
        );
    }

    private async Task<List<Project>> ReadProjectsAsync()
    {
        if (!File.Exists(_filePath))
            return new List<Project>();

        string json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Project>();

        return JsonSerializer.Deserialize<List<Project>>(json)
               ?? new List<Project>();
    }

    private async Task WriteProjectsAsync(List<Project> projects)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
            Directory.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(projects, _jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        await _fileLock.WaitAsync();

        try
        {
            return await ReadProjectsAsync();
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<Project?> GetProjectByIdAsync(Guid id)
    {
        var projects = await GetAllProjectsAsync();

        return projects.FirstOrDefault(p => p.Id == id);
    }


    public async Task SaveProjectAsync(Project project)
    {
        await _fileLock.WaitAsync();

        try
        {
            var projects = await ReadProjectsAsync();

            var existing = projects.FirstOrDefault(p => p.Id == project.Id);

            if (existing == null)
            {
                projects.Add(project);
            }
            else
            {
                existing.Name = project.Name;
                existing.Description = project.Description;
                existing.Materials = project.Materials;
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await WriteProjectsAsync(projects);
        }
        finally
        {
            _fileLock.Release();
        }
    }


    public async Task<bool> DeleteProjectAsync(Guid id)
    {
        await _fileLock.WaitAsync();

        try
        {
            var projects = await ReadProjectsAsync();

            var project = projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
                return false;

            projects.Remove(project);

            await WriteProjectsAsync(projects);

            return true;
        }
        finally
        {
            _fileLock.Release();
        }
    }
}
