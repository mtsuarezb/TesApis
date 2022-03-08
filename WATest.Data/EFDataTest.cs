namespace WATest.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WATest.Entities;

    public partial class EFDataTest : DbContext
    {
        public EFDataTest()
            : base("name=EFDataTest")
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Libro> Libro { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .Property(e => e.Nacionalidad)
                .IsUnicode(false);

            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Libro)
                .WithMany(e => e.Autor)
                .Map(m => m.ToTable("LibAut").MapLeftKey("IdAutor").MapRightKey("IdLibro"));

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.CI)
                .IsUnicode(false);

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Estudiante>()
                .Property(e => e.Carrera)
                .IsUnicode(false);

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Estudiante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Editorial)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .HasMany(e => e.Prestamo)
                .WithRequired(e => e.Libro)
                .WillCascadeOnDelete(false);
        }
    }
}
