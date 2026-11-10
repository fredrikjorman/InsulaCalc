
using InsulaCalc.Models;

namespace InsulaCalc.Services;

public class DemoProjectService : IProjectService
{
    private readonly List<Project> _projects = new();

    public Task<List<Project>> GetAllProjectsAsync()
    {
        return Task.FromResult(_projects.ToList());
    }

    public Task<Project?> GetProjectByIdAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(project);
    }

    public Task SaveProjectAsync(Project project)
    {
        var existing = _projects.FirstOrDefault(p => p.Id == project.Id);

        if (existing == null)
        {
            _projects.Add(project);
        }
        else
        {
            var index = _projects.IndexOf(existing);
            _projects[index] = project;
            project.UpdatedAt = DateTime.UtcNow;
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteProjectAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return Task.FromResult(false);

        _projects.Remove(project);

        return Task.FromResult(true);
    }
}
