using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class ProfessorsCRUD : IProfessorsCRUD
    {
        private static readonly List<Professor> Professors = new()
        {
            new Professor { ProfessorId = 1, Name = "Dr. John Doe", Email = "john.doe@university.com", Department = "Computer Science", Bio = "Expert in AI and machine learning" },
            new Professor { ProfessorId = 2, Name = "Dr. Jane Smith", Email = "jane.smith@university.com", Department = "Mathematics", Bio = "Specialist in algebra and number theory" }
        };

        public void Create(Professor entity)
        {
            entity.ProfessorId = Professors.Max(p => p.ProfessorId) + 1;
            Professors.Add(entity);
        }

        public void Delete(Professor entity)
        {
            var professor = ReadById(entity.ProfessorId);
            if (professor != null)
            {
                Professors.Remove(professor);
            }
        }

        public IEnumerable<Professor> ReadAll()
        {
            return Professors;
        }

        public Professor? ReadById(int id)
        {
            return Professors.FirstOrDefault(p => p.ProfessorId == id);
        }

        public void Update(Professor entity)
        {
            var professor = ReadById(entity.ProfessorId);
            if (professor != null)
            {
                professor.Name = entity.Name;
                professor.Email = entity.Email;
                professor.Department = entity.Department;
                professor.Bio = entity.Bio;
            }
        }
    }
}
