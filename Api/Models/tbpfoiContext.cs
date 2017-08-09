using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class tbpfoiContext : DbContext
    {
        public virtual DbSet<Dokument> Dokument { get; set; }
        public virtual DbSet<JedinicaMjere> JedinicaMjere { get; set; }
        public virtual DbSet<Kontakt> Kontakt { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<PartnerKontakt> PartnerKontakt { get; set; }
        public virtual DbSet<Proizvod> Proizvod { get; set; }
        public virtual DbSet<StanjeProizvoda> StanjeProizvoda { get; set; }
        public virtual DbSet<StatusDokumenta> StatusDokumenta { get; set; }
        public virtual DbSet<StavkaDokumenta> StavkaDokumenta { get; set; }
        public virtual DbSet<VrstaDokumenta> VrstaDokumenta { get; set; }
        public virtual DbSet<VrstaPartnera> VrstaPartnera { get; set; }
        public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }

        public tbpfoiContext(DbContextOptions<tbpfoiContext> options)
    : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dokument>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DatumAzuriranja).HasDefaultValueSql("now()");

                entity.Property(e => e.DatumKreiranja).HasDefaultValueSql("now()");

                entity.Property(e => e.Godina).HasDefaultValueSql("2017");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.VrstaId).HasColumnName("VrstaID");

                entity.Property(e => e.ZaposlenikId).HasColumnName("ZaposlenikID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Dokument)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("Dokument_StatusID_fkey");

                entity.HasOne(d => d.Vrsta)
                    .WithMany(p => p.Dokument)
                    .HasForeignKey(d => d.VrstaId)
                    .HasConstraintName("Dokument_VrstaID_fkey");

                entity.HasOne(d => d.Zaposlenik)
                    .WithMany(p => p.Dokument)
                    .HasForeignKey(d => d.ZaposlenikId)
                    .HasConstraintName("Dokument_ZaposlenikID_fkey");
            });

            modelBuilder.Entity<JedinicaMjere>(entity =>
            {
                entity.ToTable("Jedinica_Mjere");

                entity.HasIndex(e => e.Naziv)
                    .HasName("JedinicaMjere_Naziv");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"JedinicaMjere_ID_seq\"'::regclass)");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Kontakt>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"kontakt_ID_seq\"'::regclass)");

                entity.Property(e => e.Adresa)
                    .HasColumnType("varchar")
                    .HasMaxLength(128);

                entity.Property(e => e.Email)
                    .HasColumnType("varchar")
                    .HasMaxLength(64);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(32);

                entity.Property(e => e.Telefon)
                    .HasColumnType("varchar")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"dobavljac_ID_seq\"'::regclass)");

                entity.Property(e => e.DatumKreiranja).HasColumnType("time");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(64);

                entity.Property(e => e.VrstaId).HasColumnName("VrstaID");

                entity.HasOne(d => d.Vrsta)
                    .WithMany(p => p.Partner)
                    .HasForeignKey(d => d.VrstaId)
                    .HasConstraintName("Dobavljac_Vrsta");
            });

            modelBuilder.Entity<PartnerKontakt>(entity =>
            {
                entity.HasKey(e => new { e.PartnerId, e.KontaktId })
                    .HasName("PK_Partner_Kontakt");

                entity.ToTable("Partner_Kontakt");

                entity.Property(e => e.PartnerId).HasColumnName("PartnerID");

                entity.Property(e => e.KontaktId).HasColumnName("KontaktID");

                entity.HasOne(d => d.Kontakt)
                    .WithMany(p => p.PartnerKontakt)
                    .HasForeignKey(d => d.KontaktId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Kontakt");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerKontakt)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Partner");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.HasIndex(e => e.Naziv)
                    .HasName("Proizvod_Naziv");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<StanjeProizvoda>(entity =>
            {
                entity.ToTable("Stanje_Proizvoda");

                entity.HasIndex(e => e.ProizvodId)
                    .HasName("StanjeProizvoda_ProizvodID_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"StanjeProizvoda_ID_seq\"'::regclass)");

                entity.Property(e => e.JedinicaMjereId).HasColumnName("JedinicaMjereID");

                entity.Property(e => e.ProizvodId)
                    .IsRequired()
                    .HasColumnName("ProizvodID");

                entity.Property(e => e.Stanje).HasDefaultValueSql("0");

                entity.HasOne(d => d.JedinicaMjere)
                    .WithMany(p => p.StanjeProizvoda)
                    .HasForeignKey(d => d.JedinicaMjereId)
                    .HasConstraintName("StanjeProizvoda_JedinicaMjereID_fkey");

                entity.HasOne(d => d.Proizvod)
                    .WithOne(p => p.StanjeProizvoda)
                    .HasForeignKey<StanjeProizvoda>(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("StanjeProizvoda_ProizvodID_fkey");
            });

            modelBuilder.Entity<StatusDokumenta>(entity =>
            {
                entity.ToTable("Status_Dokumenta");

                entity.HasIndex(e => e.Naziv)
                    .HasName("StatusDokumenta_Naziv");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"StatusDokumenta_ID_seq\"'::regclass)");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StavkaDokumenta>(entity =>
            {
                entity.ToTable("Stavka_Dokumenta");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"StavkaDokumenta_ID_seq\"'::regclass)");

                entity.Property(e => e.DokumentId).HasColumnName("DokumentID");

                entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

                entity.HasOne(d => d.Dokument)
                    .WithMany(p => p.StavkaDokumenta)
                    .HasForeignKey(d => d.DokumentId)
                    .HasConstraintName("StavkaDokumenta_DokumentID_fkey");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.StavkaDokumenta)
                    .HasForeignKey(d => d.ProizvodId)
                    .HasConstraintName("StavkaDokumenta_ProizvodID_fkey");
            });

            modelBuilder.Entity<VrstaDokumenta>(entity =>
            {
                entity.ToTable("Vrsta_Dokumenta");

                entity.HasIndex(e => e.Naziv)
                    .HasName("VrstaDokumenta_Naziv");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"VrstaDokumenta_ID_seq\"'::regclass)");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<VrstaPartnera>(entity =>
            {
                entity.ToTable("Vrsta_Partnera");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("nextval('\"vrsta_dobavljaca_ID_seq\"'::regclass)");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.HasIndex(e => e.Prezime)
                    .HasName("Zaposlenik_Prezime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DatumRodjenja).HasColumnType("date");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(32);

                entity.Property(e => e.KontaktId).HasColumnName("KontaktID");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(32);

                entity.HasOne(d => d.Kontakt)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.KontaktId)
                    .HasConstraintName("Zaposlenik_Kontakt");
            });

            modelBuilder.HasSequence("dobavljac_ID_seq");

            modelBuilder.HasSequence("JedinicaMjere_ID_seq");

            modelBuilder.HasSequence("kontakt_ID_seq");

            modelBuilder.HasSequence("StanjeProizvoda_ID_seq");

            modelBuilder.HasSequence("StatusDokumenta_ID_seq");

            modelBuilder.HasSequence("StavkaDokumenta_ID_seq");

            modelBuilder.HasSequence("vrsta_dobavljaca_ID_seq");

            modelBuilder.HasSequence("VrstaDokumenta_ID_seq");
        }
    }
}