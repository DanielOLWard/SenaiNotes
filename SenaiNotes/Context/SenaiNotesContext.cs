using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Models;

namespace SenaiNotes.Context;

public partial class SenaiNotesContext : DbContext
{
    public SenaiNotesContext()
    {
    }

    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lixeira> Lixeiras { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagNota> TagNota { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NOTE12-S28\\SQLEXPRESS;Initial Catalog=SenaiNotes;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lixeira>(entity =>
        {
            entity.HasKey(e => e.LixeiraId).HasName("PK__Lixeira__B61667F3C4C44AC8");

            entity.ToTable("Lixeira");

            entity.HasOne(d => d.Notas).WithMany(p => p.Lixeiras)
                .HasForeignKey(d => d.NotasId)
                .HasConstraintName("FK__Lixeira__NotasId__5165187F");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotasId).HasName("PK__Notas__494AC75BD89EC2FB");

            entity.Property(e => e.NotasId).HasColumnName("NotasID");
            entity.Property(e => e.ConteudoNotas).HasColumnType("text");
            entity.Property(e => e.Subtitulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Notas__UsuarioId__4E88ABD4");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagsId).HasName("PK__Tags__D4316BFC0123737F");

            entity.Property(e => e.NomeTag)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TagNota>(entity =>
        {
            entity.HasKey(e => e.TagNotasId).HasName("PK__TagNotas__F3BA09B431CFE34C");

            entity.HasOne(d => d.Notas).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.NotasId)
                .HasConstraintName("FK__TagNotas__NotasI__59FA5E80");

            entity.HasOne(d => d.Tags).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.TagsId)
                .HasConstraintName("FK__TagNotas__TagsId__5AEE82B9");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__TipoUsua__7F22C722315A05CA");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.Descricao)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B82DD446CB");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .HasConstraintName("FK__Usuarios__TipoUs__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
