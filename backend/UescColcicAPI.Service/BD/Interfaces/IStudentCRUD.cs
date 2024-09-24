using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using System.Collections.Generic;

namespace UescColcicAPI.Services.BD.Interfaces
{
    public interface IStudentsCRUD : IBaseCRUD<StudentViewModel, StudentViewModel>
    {
        int Create(StudentViewModel student);
        void Update(int id, StudentViewModel student);
        void Delete(int id);
        Student ReadById(int id);
        List<Student> ReadAll();
    }
}