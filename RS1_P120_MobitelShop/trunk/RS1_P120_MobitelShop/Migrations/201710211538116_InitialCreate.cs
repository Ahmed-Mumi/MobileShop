namespace RS1_P120_MobitelShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisniks", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                        Email = c.String(),
                        Telefon = c.String(),
                        Adresa = c.String(),
                        KorisnickoIme = c.String(),
                        LozinkaHash = c.String(),
                        LozinkaSalt = c.String(),
                        KlijentId = c.Int(nullable: false),
                        AdministratorId = c.Int(nullable: false),
                        LoginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logins", t => t.LoginId)
                .Index(t => t.LoginId);
            
            CreateTable(
                "dbo.Klijents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisniks", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Artikals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sifra = c.String(),
                        Slika = c.Byte(nullable: false),
                        Model = c.String(),
                        Marka = c.String(),
                        Cijena = c.Double(nullable: false),
                        SpecifikacijeId = c.Int(nullable: false),
                        PopustId = c.Int(nullable: false),
                        TipServisaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Popusts", t => t.PopustId)
                .ForeignKey("dbo.Specifikacijes", t => t.SpecifikacijeId)
                .ForeignKey("dbo.TipServisas", t => t.TipServisaId)
                .Index(t => t.SpecifikacijeId)
                .Index(t => t.PopustId)
                .Index(t => t.TipServisaId);
            
            CreateTable(
                "dbo.Popusts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IznosPopusta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specifikacijes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperativniSistem = c.String(),
                        EksternaMemorija = c.String(),
                        Ekran = c.String(),
                        Rezolucija = c.String(),
                        VrstaEkrana = c.String(),
                        JezgreProcesora = c.String(),
                        Kamera = c.String(),
                        Povezivanje = c.String(),
                        RAM = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipServisas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
                        Cijena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetaljiNarudzbes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        NarudzbaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Narudzbas", t => t.NarudzbaId)
                .Index(t => t.NarudzbaId);
            
            CreateTable(
                "dbo.Narudzbas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojNarudzbe = c.Int(nullable: false),
                        DatumNarudzbe = c.DateTime(nullable: false),
                        StatusNarudzbe = c.String(),
                        Otkazano = c.Boolean(nullable: false),
                        IsporukaId = c.Int(nullable: false),
                        KlijentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Isporukas", t => t.IsporukaId)
                .ForeignKey("dbo.Klijents", t => t.KlijentId)
                .Index(t => t.IsporukaId)
                .Index(t => t.KlijentId);
            
            CreateTable(
                "dbo.Isporukas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        _Naziv = c.String(),
                        _Tip = c.String(),
                        _Kontakt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dobavljacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Email = c.String(),
                        Telefon = c.String(),
                        Adresa = c.String(),
                        SkladisteId = c.Int(nullable: false),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grads", t => t.GradId)
                .ForeignKey("dbo.Skladistes", t => t.SkladisteId)
                .Index(t => t.SkladisteId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Grads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        PostanskiBroj = c.String(),
                        KantonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kantons", t => t.KantonId)
                .Index(t => t.KantonId);
            
            CreateTable(
                "dbo.Kantons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skladistes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Kapacitet = c.Int(nullable: false),
                        Opis = c.String(),
                        Kolicina = c.Int(nullable: false),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grads", t => t.GradId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Filijalas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grads", t => t.GradId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Komentars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TekstKomentara = c.String(),
                        DatumKomentiranja = c.DateTime(nullable: false),
                        KlijentId = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikals", t => t.ArtikalId)
                .ForeignKey("dbo.Klijents", t => t.KlijentId)
                .Index(t => t.KlijentId)
                .Index(t => t.ArtikalId);
            
            CreateTable(
                "dbo.Korpas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                        KlijentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikals", t => t.ArtikalId)
                .ForeignKey("dbo.Klijents", t => t.KlijentId)
                .Index(t => t.ArtikalId)
                .Index(t => t.KlijentId);
            
            CreateTable(
                "dbo.OcjenaMobitelas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ocjena = c.Int(nullable: false),
                        KlijentId = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikals", t => t.ArtikalId)
                .ForeignKey("dbo.Klijents", t => t.KlijentId)
                .Index(t => t.KlijentId)
                .Index(t => t.ArtikalId);
            
            CreateTable(
                "dbo.Servis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumPrijave = c.DateTime(nullable: false),
                        DatumZavrsetka = c.DateTime(nullable: false),
                        Opis = c.String(),
                        Cijena = c.Double(nullable: false),
                        AdministratorId = c.Int(nullable: false),
                        TipServisaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrators", t => t.AdministratorId)
                .ForeignKey("dbo.TipServisas", t => t.TipServisaId)
                .Index(t => t.AdministratorId)
                .Index(t => t.TipServisaId);
            
            CreateTable(
                "dbo.SkladisteArtikals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        ArtikalId = c.Int(nullable: false),
                        SkladisteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikals", t => t.ArtikalId)
                .ForeignKey("dbo.Skladistes", t => t.SkladisteId)
                .Index(t => t.ArtikalId)
                .Index(t => t.SkladisteId);
            
            CreateTable(
                "dbo.UlazRobeDetaljis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        FilijalaId = c.Int(nullable: false),
                        DobavljacId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dobavljacs", t => t.DobavljacId)
                .ForeignKey("dbo.Filijalas", t => t.FilijalaId)
                .Index(t => t.FilijalaId)
                .Index(t => t.DobavljacId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UlazRobeDetaljis", "FilijalaId", "dbo.Filijalas");
            DropForeignKey("dbo.UlazRobeDetaljis", "DobavljacId", "dbo.Dobavljacs");
            DropForeignKey("dbo.SkladisteArtikals", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.SkladisteArtikals", "ArtikalId", "dbo.Artikals");
            DropForeignKey("dbo.Servis", "TipServisaId", "dbo.TipServisas");
            DropForeignKey("dbo.Servis", "AdministratorId", "dbo.Administrators");
            DropForeignKey("dbo.OcjenaMobitelas", "KlijentId", "dbo.Klijents");
            DropForeignKey("dbo.OcjenaMobitelas", "ArtikalId", "dbo.Artikals");
            DropForeignKey("dbo.Korpas", "KlijentId", "dbo.Klijents");
            DropForeignKey("dbo.Korpas", "ArtikalId", "dbo.Artikals");
            DropForeignKey("dbo.Komentars", "KlijentId", "dbo.Klijents");
            DropForeignKey("dbo.Komentars", "ArtikalId", "dbo.Artikals");
            DropForeignKey("dbo.Filijalas", "GradId", "dbo.Grads");
            DropForeignKey("dbo.Dobavljacs", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.Skladistes", "GradId", "dbo.Grads");
            DropForeignKey("dbo.Dobavljacs", "GradId", "dbo.Grads");
            DropForeignKey("dbo.Grads", "KantonId", "dbo.Kantons");
            DropForeignKey("dbo.DetaljiNarudzbes", "NarudzbaId", "dbo.Narudzbas");
            DropForeignKey("dbo.Narudzbas", "KlijentId", "dbo.Klijents");
            DropForeignKey("dbo.Narudzbas", "IsporukaId", "dbo.Isporukas");
            DropForeignKey("dbo.Artikals", "TipServisaId", "dbo.TipServisas");
            DropForeignKey("dbo.Artikals", "SpecifikacijeId", "dbo.Specifikacijes");
            DropForeignKey("dbo.Artikals", "PopustId", "dbo.Popusts");
            DropForeignKey("dbo.Korisniks", "LoginId", "dbo.Logins");
            DropForeignKey("dbo.Klijents", "Id", "dbo.Korisniks");
            DropForeignKey("dbo.Administrators", "Id", "dbo.Korisniks");
            DropIndex("dbo.UlazRobeDetaljis", new[] { "DobavljacId" });
            DropIndex("dbo.UlazRobeDetaljis", new[] { "FilijalaId" });
            DropIndex("dbo.SkladisteArtikals", new[] { "SkladisteId" });
            DropIndex("dbo.SkladisteArtikals", new[] { "ArtikalId" });
            DropIndex("dbo.Servis", new[] { "TipServisaId" });
            DropIndex("dbo.Servis", new[] { "AdministratorId" });
            DropIndex("dbo.OcjenaMobitelas", new[] { "ArtikalId" });
            DropIndex("dbo.OcjenaMobitelas", new[] { "KlijentId" });
            DropIndex("dbo.Korpas", new[] { "KlijentId" });
            DropIndex("dbo.Korpas", new[] { "ArtikalId" });
            DropIndex("dbo.Komentars", new[] { "ArtikalId" });
            DropIndex("dbo.Komentars", new[] { "KlijentId" });
            DropIndex("dbo.Filijalas", new[] { "GradId" });
            DropIndex("dbo.Skladistes", new[] { "GradId" });
            DropIndex("dbo.Grads", new[] { "KantonId" });
            DropIndex("dbo.Dobavljacs", new[] { "GradId" });
            DropIndex("dbo.Dobavljacs", new[] { "SkladisteId" });
            DropIndex("dbo.Narudzbas", new[] { "KlijentId" });
            DropIndex("dbo.Narudzbas", new[] { "IsporukaId" });
            DropIndex("dbo.DetaljiNarudzbes", new[] { "NarudzbaId" });
            DropIndex("dbo.Artikals", new[] { "TipServisaId" });
            DropIndex("dbo.Artikals", new[] { "PopustId" });
            DropIndex("dbo.Artikals", new[] { "SpecifikacijeId" });
            DropIndex("dbo.Klijents", new[] { "Id" });
            DropIndex("dbo.Korisniks", new[] { "LoginId" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropTable("dbo.UlazRobeDetaljis");
            DropTable("dbo.SkladisteArtikals");
            DropTable("dbo.Servis");
            DropTable("dbo.OcjenaMobitelas");
            DropTable("dbo.Korpas");
            DropTable("dbo.Komentars");
            DropTable("dbo.Filijalas");
            DropTable("dbo.Skladistes");
            DropTable("dbo.Kantons");
            DropTable("dbo.Grads");
            DropTable("dbo.Dobavljacs");
            DropTable("dbo.Isporukas");
            DropTable("dbo.Narudzbas");
            DropTable("dbo.DetaljiNarudzbes");
            DropTable("dbo.TipServisas");
            DropTable("dbo.Specifikacijes");
            DropTable("dbo.Popusts");
            DropTable("dbo.Artikals");
            DropTable("dbo.Logins");
            DropTable("dbo.Klijents");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Administrators");
        }
    }
}
