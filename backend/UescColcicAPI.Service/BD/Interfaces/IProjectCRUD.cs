using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;

namespace UescColcicAPI.Services.BD.Interfaces;

public interface IProjectsCRUD : IBaseCRUD<ProjectViewModel, ProjectViewModel>
{
        int Create(ProjectViewModel project);
        void Update(int id, ProjectViewModel project);
        void Delete(int id);
        Project ReadById(int id);
        List<Project> ReadAll();
        List<Project> GetProjectsByProfessorId(int professorId);
}