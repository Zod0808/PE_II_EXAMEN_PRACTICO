using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace proyecto_peti.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Mision> Mision { get; set; }
        public virtual DbSet<ObjetivosEspecificos> ObjetivosEspecificos { get; set; }
        public virtual DbSet<ObjetivosEstrategicos> ObjetivosEstrategicos { get; set; }
        public virtual DbSet<PlanEstrategico> PlanEstrategico { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Valores> Valores { get; set; }
        public virtual DbSet<Vision> Vision { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjetivosEstrategicos>()
                .HasMany(e => e.ObjetivosEspecificos)
                .WithRequired(e => e.ObjetivosEstrategicos)
                .HasForeignKey(e => e.ObjetivoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.Mision)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.ObjetivosEstrategicos)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.Valores)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.Vision)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.PlanEstrategico)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
