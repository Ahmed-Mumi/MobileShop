using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RS1_P120_MobitelShop.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RS1_P120_MobitelShop.DAL
{
    public class MojContext : DbContext
    {
        public MojContext():
            base("Name=MojConnectionString")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Administrator).WithRequired(x => x.Korisnik);
            modelBuilder.Entity<Korisnik>().HasOptional(x => x.Klijent).WithRequired(x => x.Korisnik);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<DetaljiNarudzbe> DetaljiNarudzbi { get; set; }
        public DbSet<Dobavljac> Dobavljaci { get; set; }
        public DbSet<Filijala> Filijale { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Isporuka> Isporuke { get; set; }
        public DbSet<Kanton> Kantoni { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Korpa> Korpe { get; set; }
        public DbSet<Login> Logini { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<OcjenaMobitela> OcjeneMobitela { get; set; }
        public DbSet<Popust> Popusti { get; set; }
        public DbSet<Servis> Servisi { get; set; }
        public DbSet<Skladiste> Skladista { get; set; }
        public DbSet<SkladisteArtikal> SkladisteArtikli { get; set; }
        public DbSet<Specifikacije> Specifikacija { get; set; }
        public DbSet<TipServisa> TipoviServisa { get; set; }
        public DbSet<UlazRobeDetalji> UlaziRobeDetalji { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }
        public DbSet<Galerija> Galerije { get; set; }


    }
}