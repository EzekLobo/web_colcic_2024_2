using Microsoft.EntityFrameworkCore;
using UescColcicAPI.Core;

namespace UescColcicAPI.Infra
{
    public class ClUescColcicAPIDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=colcicdb;User=root;Password=251417@BAV;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }

        public DbSet<Professor> Professors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Modelagem da entidade Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId); 
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200); 
                entity.Property(e => e.Description).HasMaxLength(1000); 
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50); 
                
                // Adiciona a FK ProfessorId
                entity.HasOne(e => e.Professor)
                      .WithMany(p => p.Projects) // Um professor tem muitos projetos
                      .HasForeignKey(e => e.ProfessorId)
                      .OnDelete(DeleteBehavior.Cascade); // Se o professor for deletado, deletar os projetos relacionados
            });

            // Modelagem da entidade Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId); 
                entity.Property(e => e.Registration).IsRequired().HasMaxLength(50); 
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Course).HasMaxLength(100); 
                entity.Property(e => e.Bio).HasMaxLength(1000); 
            });

            // Modelagem da entidade Professor
            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.ProfessorId); 
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Department).HasMaxLength(100); 
                entity.Property(e => e.Bio).HasMaxLength(1000); 
            });
        }
    }
}
