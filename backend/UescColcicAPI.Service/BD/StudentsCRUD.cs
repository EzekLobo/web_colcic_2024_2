using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class StudentsCRUD : IStudentsCRUD
    {
        private static readonly List<Student> Students = new()
        {
            new Student { StudentId = 1, Registration = "REG123", Name = "Douglas", Email = "douglas.cic@uesc.br", Course = "Computer Science", Bio = "A passionate computer science student." },
            new Student { StudentId = 2, Registration = "REG124", Name = "Estevão", Email = "estevao.cic@uesc.br", Course = "Software Engineering", Bio = "Software enthusiast and problem solver." },
            new Student { StudentId = 3, Registration = "REG125", Name = "Gabriel", Email = "gabriel.cic@uesc.br", Course = "Information Systems", Bio = "Interested in data and business systems." },
            new Student { StudentId = 4, Registration = "REG126", Name = "Gabriela", Email = "gabriela.cic@uesc.br", Course = "Artificial Intelligence", Bio = "Exploring AI and machine learning." }
        };

        public void Create(Student entity)
        {
            // Verifica se o registro já existe
            if (Students.Any(s => s.Registration == entity.Registration))
            {
                throw new InvalidOperationException($"A student with registration {entity.Registration} already exists.");
            }

            // Gera automaticamente um ID para o novo estudante
            entity.StudentId = Students.Any() ? Students.Max(s => s.StudentId) + 1 : 1;
            Students.Add(entity);
        }

        public void Delete(Student entity)
        {
            var student = this.Find(entity.StudentId);
            if (student is not null)
            {
                Students.Remove(student);
            }
        }

        public IEnumerable<Student> ReadAll()
        {
            return Students;
        }

        public Student? ReadById(int id)
        {
            return this.Find(id);
        }

        public void Update(Student entity)
        {
            var student = this.Find(entity.StudentId);
            if (student is not null)
            {
                // Verifica se outro estudante já usa o mesmo registration
                if (Students.Any(s => s.Registration == entity.Registration && s.StudentId != entity.StudentId))
                {
                    throw new InvalidOperationException($"Another student with registration {entity.Registration} already exists.");
                }

                // Atualiza os campos
                student.Name = entity.Name;
                student.Email = entity.Email;
                student.Registration = entity.Registration;
                student.Course = entity.Course; 
                student.Bio = entity.Bio; 
            }
        }

        // Busca um estudante por ID
        private Student? Find(int id)
        {
            return Students.FirstOrDefault(x => x.StudentId == id);
        }
    }
}
