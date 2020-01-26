using Microsoft.EntityFrameworkCore;
using WebPrueba.Models;

namespace WebPrueba.Models
{
    public class SincoABRContext : DbContext
    {
        public SincoABRContext(DbContextOptions<SincoABRContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(bc => new { bc.StudentId, bc.SubjectId });

            modelBuilder.Entity<StudentSubject>()
                .HasOne(bc => bc.Student)
                .WithMany(b => b.StudentSubjects)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.StudentSubjects)
                .HasForeignKey(bc => bc.SubjectId);

            modelBuilder.Entity<SubjectDetail>()
            .Property<int>("TeacherId");

            modelBuilder.Entity<SubjectDetail>()
                .HasOne(p => p.Teacher)
                .WithMany()
                .HasForeignKey("TeacherId");
        }

        public DbSet<StudentDetail> Students { get; set; }
        public DbSet<SubjectDetail> Subjects { get; set; }
        public DbSet<TeacherDetail> Teachers { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }

    }
}
