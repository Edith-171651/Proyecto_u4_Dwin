using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WEB_API_ESCUELA.Models
{
    public partial class ESCUELA_PROYECTOContext : DbContext
    {
        public ESCUELA_PROYECTOContext()
        {
        }

        public ESCUELA_PROYECTOContext(DbContextOptions<ESCUELA_PROYECTOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Curso> Cursos { get; set; } = null!;
        public virtual DbSet<CursoAlumno> CursoAlumnos { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Profesor> Profesors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-5TOLRIG\\SQLEXPRESS; DataBase=ESCUELA_PROYECTO; user id=ESC_PROY.OWNER; password=Argentina1@; Encrypt=False;");
                //optionsBuilder.UseSqlServer("Server=CSPSPI069589L\\SQLEXPRESS; DataBase=ESCUELA_PROYECTO; user id=ESC_PROY.OWNER; password=Argentina1@; Encrypt=False;");


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("ALUMNO");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ApellidoM)
                    .HasMaxLength(100)
                    .HasColumnName("APELLIDO_M");

                entity.Property(e => e.ApellidoP)
                    .HasMaxLength(100)
                    .HasColumnName("APELLIDO_P");

                entity.Property(e => e.Grado)
                    .HasMaxLength(100)
                    .HasColumnName("GRADO");

                entity.Property(e => e.Grupo)
                    .HasMaxLength(100)
                    .HasColumnName("GRUPO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("CURSO");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.IdProfesor)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ID_PROFESOR");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey(d => d.IdProfesor)
                    .HasConstraintName("FK_CURSO_PROFESOR");
            });

            modelBuilder.Entity<CursoAlumno>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CURSO_ALUMNO");

                entity.Property(e => e.IdAlumno)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ID_ALUMNO");

                entity.Property(e => e.IdCurso)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ID_CURSO");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CURSO_ALUMNO_ALUMNO");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CURSO_ALUMNO_CURSO");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(100)
                    .HasColumnName("CONTRASENA");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .HasColumnName("TIPO");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO");
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("PROFESOR");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ApellidoM)
                    .HasMaxLength(100)
                    .HasColumnName("APELLIDO_M");

                entity.Property(e => e.ApellidoP)
                    .HasMaxLength(100)
                    .HasColumnName("APELLIDO_P");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("NOMBRE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
