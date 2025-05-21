using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Dto;
using SenaiNotes.Models;

namespace SenaiNotes.Context;

public partial class SenaiNotesContext : DbContext
{
  
    private IConfiguration _configuration;
    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options, IConfiguration config)
        : base(options)
    {
        _configuration = config;
    }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagNota> TagNotas { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var con = _configuration.GetConnectionString("DefaultConecction");
            optionsBuilder.UseSqlServer(con);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotasId).HasName("PK__Notas__494AC75BCEC1CBBB");

            entity.Property(e => e.NotasId).HasColumnName("NotasID");
            entity.Property(e => e.ConteudoNotas).HasColumnType("text");
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Notas__UsuarioId__797309D9");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tags__D4316BFC02F94603");

            entity.Property(e => e.NomeTag)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TagNota>(entity =>
        {
            entity.HasKey(e => e.TagNotasId).HasName("PK__TagNotas__F3BA09B4293F8285");

            entity.HasOne(d => d.Notas).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.NotasId)
                .HasConstraintName("FK__TagNotas__NotasI__7E37BEF6");

            entity.HasOne(d => d.Tags).WithMany(p => p.TagNota)
                .HasForeignKey(d => d.NotasId)
                .HasConstraintName("FK__TagNotas__TagsId__7F2BE32F");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__TipoUsua__7F22C72253394714");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B805CB0410");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105345D8983BB").IsUnique();

            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCadastro).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha).IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .HasConstraintName("FK__Usuarios__TipoUs__76969D2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    internal void TagNotaDto(TagNotaDto tagN)
    {
        throw new NotImplementedException();
    }
}
