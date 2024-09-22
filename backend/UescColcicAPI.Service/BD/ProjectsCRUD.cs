using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class ProjectsCRUD : IProjectsCRUD
    {
        private static readonly List<Project> Projects = new()
        {
            new Project { ProjectId = 1, Title = "AI Research", Description = "Exploring new AI techniques", StartDate = new DateTime(2023, 01, 01), EndDate = new DateTime(2023, 12, 31) },
            new Project { ProjectId = 2, Title = "Data Analysis Tool", Description = "Building a tool for big data analysis", StartDate = new DateTime(2023, 02, 01), EndDate = new DateTime(2023, 11, 30) },
            new Project { ProjectId = 3, Title = "Web Development Framework", Description = "Creating a modern web framework", StartDate = new DateTime(2023, 03, 15), EndDate = new DateTime(2024, 03, 14) }
        };

        public void Create(Project entity)
        {
            // Verifica se o título do projeto já existe
            if (Projects.Any(p => p.Title == entity.Title))
            {
                throw new InvalidOperationException($"A project with title {entity.Title} already exists.");
            }

            // Gera automaticamente um ID para o novo projeto
            entity.ProjectId = Projects.Any() ? Projects.Max(p => p.ProjectId) + 1 : 1;
            Projects.Add(entity);
        }

        public void Delete(Project entity)
        {
            var project = this.Find(entity.ProjectId);
            if (project is not null)
            {
                Projects.Remove(project);
            }
        }

        public IEnumerable<Project> ReadAll()
        {
            return Projects;
        }

        public Project? ReadById(int id)
        {
            return this.Find(id);
        }

        public void Update(Project entity)
        {
            var project = this.Find(entity.ProjectId);
            if (project is not null)
            {
                // Verifica se outro projeto já usa o mesmo título
                if (Projects.Any(p => p.Title == entity.Title && p.ProjectId != entity.ProjectId))
                {
                    throw new InvalidOperationException($"Another project with title {entity.Title} already exists.");
                }

                // Atualiza os campos
                project.Title = entity.Title;
                project.Description = entity.Description;
                project.StartDate = entity.StartDate;
                project.EndDate = entity.EndDate;
            }
        }

        // Busca um projeto por ID
        private Project? Find(int id)
        {
            return Projects.FirstOrDefault(p => p.ProjectId == id);
        }
    }
}
