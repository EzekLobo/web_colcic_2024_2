using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UescColcicAPI.Services.BD
{
    public class ProjectsCRUD : IProjectsCRUD
    {
        private static readonly List<Project> Projects = new()
        {
            new Project { ProjectId = 1, Title = "AI Research", Description = "Research on AI applications", Type = "A", StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6), ProfessorId = 1 },
            new Project { ProjectId = 2, Title = "Blockchain Initiative", Description = "Exploring blockchain for decentralized systems", Type = "B", StartDate = DateTime.Now.AddMonths(-3), EndDate = DateTime.Now.AddMonths(9), ProfessorId = 2 }
        };

        // Lista de professores simulada (pode ser substituída por uma chamada ao banco de dados)
        private static readonly List<Professor> Professors = new()
        {
            new Professor { ProfessorId = 1, Name = "Dr. John Doe", Email = "john.doe@university.com", Department = "Computer Science", Bio = "Expert in AI and machine learning" },
            new Professor { ProfessorId = 2, Name = "Dr. Jane Smith", Email = "jane.smith@university.com", Department = "Mathematics", Bio = "Specialist in algebra and number theory" }
        };

        public int Create(ProjectViewModel projectViewModel)
        {
            // Verificar se o professor existe
            var professorExists = Professors.Any(p => p.ProfessorId == projectViewModel.ProfessorId);
            if (!professorExists)
            {
                throw new InvalidOperationException($"Professor with ID {projectViewModel.ProfessorId} does not exist.");
            }

            var project = new Project
            {
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                Type = projectViewModel.Type,
                StartDate = projectViewModel.StartDate,
                EndDate = projectViewModel.EndDate,
                ProfessorId = projectViewModel.ProfessorId // Associando projeto ao Professor
            };

            if (Projects.Any(p => p.Title == project.Title))
            {
                throw new InvalidOperationException($"A project with the title '{project.Title}' already exists.");
            }

            project.ProjectId = Projects.Any() ? Projects.Max(p => p.ProjectId) + 1 : 1;
            Projects.Add(project);
            return project.ProjectId;
        }

        public void Update(int id, ProjectViewModel projectViewModel)
        {
            var project = Find(id);
            if (project != null)
            {
                if (Projects.Any(p => p.Title == projectViewModel.Title && p.ProjectId != id))
                {
                    throw new InvalidOperationException($"Another project with the title '{projectViewModel.Title}' already exists.");
                }

                // Verificar se o professor existe ao atualizar
                var professorExists = Professors.Any(p => p.ProfessorId == projectViewModel.ProfessorId);
                if (!professorExists)
                {
                    throw new InvalidOperationException($"Professor with ID {projectViewModel.ProfessorId} does not exist.");
                }

                project.Title = projectViewModel.Title;
                project.Description = projectViewModel.Description;
                project.Type = projectViewModel.Type;
                project.StartDate = projectViewModel.StartDate;
                project.EndDate = projectViewModel.EndDate;
                project.ProfessorId = projectViewModel.ProfessorId;
            }
        }

        public void Delete(int id)
        {
            var project = Find(id);
            if (project != null)
            {
                Projects.Remove(project);
            }
        }

        public Project ReadById(int id)
        {
            return Find(id);
        }

        public IEnumerable<Project> ReadAll()
        {
            return Projects.Select(project => new Project
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                Type = project.Type,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ProfessorId = project.ProfessorId
            }).ToList();
        }

        private Project? Find(int id)
        {
            return Projects.FirstOrDefault(p => p.ProjectId == id);
        }

        public IEnumerable<Project> GetProjectsByProfessorId(int professorId)
        {
            // Retorna todos os projetos associados ao Professor
            return Projects.Where(p => p.ProfessorId == professorId).ToList();
        }
    }
}
