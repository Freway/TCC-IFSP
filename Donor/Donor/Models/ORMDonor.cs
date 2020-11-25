using System.Data.Entity;

namespace Donor.Models
{
    public class OrmDonor : DbContext
    {
        public OrmDonor()
            : base("name=ORMDonor")
        {
        }

        public virtual DbSet<ComprimentoCabelo> ComprimentoCabelo { get; set; }
        public virtual DbSet<DivulgacaoDoacao> DivulgacaoDoacao { get; set; }
        public virtual DbSet<Doacao> Doacao { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<NivelNotificacao> NivelNotificacao { get; set; }
        public virtual DbSet<NivelNotificacaoUsuario> NivelNotificacaoUsuario { get; set; }
        public virtual DbSet<Notificacao> Notificacao { get; set; }
        public virtual DbSet<PontoDeDoacao> PontoDeDoacao { get; set; }
        public virtual DbSet<RequisitoDoacao> RequisitoDoacao { get; set; }
        public virtual DbSet<TipoDoacao> TipoDoacao { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DivulgacaoDoacao>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<DivulgacaoDoacao>()
                .HasMany(e => e.Notificacao)
                .WithRequired(e => e.DivulgacaoDoacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .Property(e => e.Municipio1)
                .IsUnicode(false);

            modelBuilder.Entity<Municipio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Municipio>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Municipio)
                .HasForeignKey(e => e.IdMunicipio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .HasMany(e => e.PontoDeDoacao)
                .WithRequired(e => e.Municipio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .HasMany(e => e.Usuario1)
                .WithMany(e => e.Municipios)
                .Map(m => m.ToTable("MunicipioUsuario").MapLeftKey("IdMunicipio").MapRightKey("IdUsuario"));

            modelBuilder.Entity<NivelNotificacao>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<NivelNotificacao>()
                .HasMany(e => e.NivelNotificacaoUsuario)
                .WithRequired(e => e.NivelNotificacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NivelNotificacaoUsuario>()
                .HasMany(e => e.TipoDoacao)
                .WithMany(e => e.NivelNotificacaoUsuario)
                .Map(m => m.ToTable("NivelNotificacaoUsuarioTipoDoacao").MapLeftKey("IdNivelNotificacaoUsuario").MapRightKey("IdTipoDoacao"));

            modelBuilder.Entity<PontoDeDoacao>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<PontoDeDoacao>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<PontoDeDoacao>()
                .Property(e => e.NomeResponsavel)
                .IsUnicode(false);

            modelBuilder.Entity<PontoDeDoacao>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<PontoDeDoacao>()
                .HasMany(e => e.RequisitoDoacao)
                .WithRequired(e => e.PontoDeDoacao)
                .HasForeignKey(e => e.IdPontoDoacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PontoDeDoacao>()
                .HasMany(e => e.TipoDoacao)
                .WithMany(e => e.PontoDeDoacao)
                .Map(m => m.ToTable("TipoDoacaoAceita").MapLeftKey("IdPontoDeDoacao").MapRightKey("IdTipoDoacao"));

            modelBuilder.Entity<RequisitoDoacao>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDoacao>()
                .Property(e => e.TipoDoacao1)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDoacao>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDoacao>()
                .HasMany(e => e.DivulgacaoDoacao)
                .WithRequired(e => e.TipoDoacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoDoacao>()
                .HasMany(e => e.Doacao)
                .WithRequired(e => e.TipoDoacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.TipoUsuario1)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.TipoUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Sexo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.TipoSanguineo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.ComprimentoCabelo)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Doacao)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdPontoDoacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.NivelNotificacaoUsuario)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Notificacao)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
