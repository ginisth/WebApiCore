using App.Repository;
using Core.Models;

namespace ApplicationLogic
{
    public class ProjectsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        public async Task<IEnumerable<Project>> ViewProjects()
        {
            return await projectRepository.GetAsync();
        }
    }
}