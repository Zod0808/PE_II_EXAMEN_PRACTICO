using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace proyecto_peti.Models
{
    public partial class Modelo : DbContext
    {
        public Modelo()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<AnalisisFODA> AnalisisFODA { get; set; }
        public virtual DbSet<AnalisisPEST> AnalisisPEST { get; set; }
        public virtual DbSet<CadenaValor> CadenaValor { get; set; }
        public virtual DbSet<FuerzasPorter> FuerzasPorter { get; set; }
        public virtual DbSet<InformacionEmpresa> InformacionEmpresa { get; set; }
        public virtual DbSet<IniciativasEstrategicas> IniciativasEstrategicas { get; set; }
        public virtual DbSet<MatrizCAME> MatrizCAME { get; set; }
        public virtual DbSet<MatrizRACI> MatrizRACI { get; set; }
        public virtual DbSet<Mision> Mision { get; set; }
        public virtual DbSet<ObjetivosEspecificos> ObjetivosEspecificos { get; set; }
        public virtual DbSet<ObjetivosEstrategicos> ObjetivosEstrategicos { get; set; }
        public virtual DbSet<PlanEstrategico> PlanEstrategico { get; set; }
        public virtual DbSet<ResumenEjecutivo> ResumenEjecutivo { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Valores> Valores { get; set; }
        public virtual DbSet<Vision> Vision { get; set; }
        public DbSet<ObservacionesCadenaValor> ObservacionesCadenaValor { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjetivosEstrategicos>()
                .HasMany(e => e.ObjetivosEspecificos)
                .WithRequired(e => e.ObjetivosEstrategicos)
                .HasForeignKey(e => e.ObjetivoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.AnalisisFODA)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.AnalisisPEST)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.CadenaValor)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.FuerzasPorter)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.InformacionEmpresa)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.IniciativasEstrategicas)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.MatrizCAME)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanEstrategico>()
                .HasMany(e => e.MatrizRACI)
                .WithRequired(e => e.PlanEstrategico)
                .HasForeignKey(e => e.PlanId)
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
                .HasMany(e => e.ResumenEjecutivo)
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
