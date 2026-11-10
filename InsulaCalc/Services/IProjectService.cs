
using InsulaCalc.Models;

namespace InsulaCalc.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();

    Task<Project?> GetProjectByIdAsync(Guid id);

    Task SaveProjectAsync(Project project);

    Task<bool> DeleteProjectAsync(Guid id);
}
