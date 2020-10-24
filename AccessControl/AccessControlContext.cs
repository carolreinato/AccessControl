using System;
using AccessControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccessControl
{
    public partial class AccessControlContext : DbContext
    {
        public AccessControlContext()
        {
        }

        public AccessControlContext(DbContextOptions<AccessControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CodigoAcesso> CodigoAcesso { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<PessoaTipoAcesso> PessoaTipoAcesso { get; set; }
        public virtual DbSet<TipoAcesso> TipoAcesso { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.\\;Initial Catalog=AccessControl;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodigoAcesso>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PessoaTipoAcesso>(entity =>
            {
                entity.Property(e => e.Entrada).HasColumnType("datetime");

                entity.Property(e => e.Saida).HasColumnType("datetime");

                entity.HasOne(d => d.IdCodigoAcessoNavigation)
                    .WithMany(p => p.PessoaTipoAcesso)
                    .HasForeignKey(d => d.IdCodigoAcesso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaTipoAcesso_CodigoAcesso");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithMany(p => p.PessoaTipoAcesso)
                    .HasForeignKey(d => d.IdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaTipoAcesso_Pessoa");

                entity.HasOne(d => d.IdTipoAcessoNavigation)
                    .WithMany(p => p.PessoaTipoAcesso)
                    .HasForeignKey(d => d.IdTipoAcesso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PessoaTipoAcesso_TipoAcesso");
            });

            modelBuilder.Entity<TipoAcesso>(entity =>
            {
                entity.Property(e => e.Nome)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
