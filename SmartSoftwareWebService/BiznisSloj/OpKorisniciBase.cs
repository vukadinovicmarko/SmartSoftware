using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpKorisniciBase : Operation
    {
        public DbItemKorisnici KorisniciDataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
               (from korisnik in entities.korisnicis
                join uloga in entities.uloges
                on korisnik.id_uloge equals uloga.id_uloge
                select new DbItemKorisnici()
                {
                    broj_telefona = korisnik.broj_telefona,
                    id_korisnici = korisnik.id_korisnici,
                    ime = korisnik.ime,
                    id_uloge = uloga.id_uloge,
                    prezime = korisnik.prezime,
                    brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                    mejl = korisnik.mejl,
                    naziv_uloge = uloga.naziv_uloge,
                    slikaKorisnika = korisnik.slika_korisnika,
                    datumKreiranja = korisnik.datum_kreiranja,
                    datumAzuriranja = korisnik.datum_azuriranja
                }).OrderBy(k => k.id_uloge).ThenBy(p => p.ime).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].slikaKorisnika != null)
                    niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpKorisniciSelect : OpKorisniciBase
    {

    }

    public class OpKorisniciPretraga : OpKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
             (from korisnik in entities.korisnicis
              join uloga in entities.uloges
               on korisnik.id_uloge equals uloga.id_uloge
              where (korisnik.id_uloge == 1 || korisnik.id_uloge == 2) && ( korisnik.ime.Contains(KorisniciDataSelect.zaPretragu) 
              || korisnik.prezime.Contains(KorisniciDataSelect.zaPretragu)
              || korisnik.mejl.Contains(KorisniciDataSelect.zaPretragu) )
              select new DbItemKorisnici()
              {
                  broj_telefona = korisnik.broj_telefona,
                  id_korisnici = korisnik.id_korisnici,
                  ime = korisnik.ime,
                  prezime = korisnik.prezime,
                  id_uloge = uloga.id_uloge,
                  naziv_uloge = uloga.naziv_uloge,
                  brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                  mejl = korisnik.mejl,
                  slikaKorisnika = korisnik.slika_korisnika,
                  datumKreiranja = korisnik.datum_kreiranja,
                  datumAzuriranja = korisnik.datum_azuriranja
              }).OrderBy(k => k.id_uloge).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].slikaKorisnika != null)
                    niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }

    }

    public class OpKorisniciInsert : OpKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.DodajKorisnika(KorisniciDataSelect.ime, KorisniciDataSelect.prezime, KorisniciDataSelect.mejl, KorisniciDataSelect.broj_telefona, this.KorisniciDataSelect.id_uloge, this.KorisniciDataSelect.datumKreiranja);
            return base.execute(entities);
        }
    }

    public class OpKorisniciAzurirajBrojPoena : OpKorisniciSelect
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.KorisniciDataSelect != null)
            {
                entities.AzurirajBrojPoena(this.KorisniciDataSelect.id_korisnici, this.KorisniciDataSelect.brojOstvarenihPoena, this.KorisniciDataSelect.datumAzuriranja);
            }
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            return opObj;
        }
    }

    public abstract class OpZaposleniKorisniciBase : Operation
    {
        public DbItemKorisnici ZaposleniKorisniciDataSelect { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                (
                    from korisnik in entities.korisnicis
                    join uloga in entities.uloges
                    on korisnik.id_uloge equals uloga.id_uloge
                    where korisnik.id_uloge == 2 || korisnik.id_uloge == 1
                    select  new DbItemKorisnici()
                    {
                        id_korisnici = korisnik.id_korisnici,
                        ime = korisnik.ime,
                        prezime = korisnik.prezime,
                        broj_telefona = korisnik.broj_telefona,
                        mejl = korisnik.mejl,
                        brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                        lozinka = korisnik.lozinka,
                        polKorisnika = korisnik.pol_korisnika,
                        slikaKorisnika = korisnik.slika_korisnika,
                        username = korisnik.username,
                        naziv_uloge = uloga.naziv_uloge,
                        id_uloge = korisnik.id_uloge,
                        datumKreiranja = korisnik.datum_kreiranja,
                        datumAzuriranja = korisnik.datum_azuriranja,
                        deletedField = korisnik.deletedField
                    }
                ).OrderBy(p => p.id_uloge).ThenBy(k => k.ime).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].slikaKorisnika != null)
                    niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            //.GroupBy(p => p.id_korisnici).Select(g => g.First())
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpZaposleniKorisniciSelect : OpZaposleniKorisniciBase
    {
        
    }

    public class OpZaposleniKorisniciInsert : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            
            entities.ZaposleniKorisniciInsert(this.ZaposleniKorisniciDataSelect.ime, this.ZaposleniKorisniciDataSelect.prezime, this.ZaposleniKorisniciDataSelect.mejl, this.ZaposleniKorisniciDataSelect.broj_telefona, this.ZaposleniKorisniciDataSelect.username, this.ZaposleniKorisniciDataSelect.lozinka, this.ZaposleniKorisniciDataSelect.brojOstvarenihPoena, this.ZaposleniKorisniciDataSelect.polKorisnika, this.ZaposleniKorisniciDataSelect.slikaKorisnika, this.ZaposleniKorisniciDataSelect.id_uloge,this.ZaposleniKorisniciDataSelect.datumKreiranja);

            return base.execute(entities);
        }
    }
    public class OpZaposleniKorisniciUpdate : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.ZaposleniKorisniciUpdate(this.ZaposleniKorisniciDataSelect.id_korisnici, this.ZaposleniKorisniciDataSelect.ime, this.ZaposleniKorisniciDataSelect.prezime, this.ZaposleniKorisniciDataSelect.mejl, this.ZaposleniKorisniciDataSelect.broj_telefona, this.ZaposleniKorisniciDataSelect.username, this.ZaposleniKorisniciDataSelect.lozinka, this.ZaposleniKorisniciDataSelect.brojOstvarenihPoena, this.ZaposleniKorisniciDataSelect.polKorisnika,this.ZaposleniKorisniciDataSelect.slikaKorisnika, this.ZaposleniKorisniciDataSelect.id_uloge, this.ZaposleniKorisniciDataSelect.datumAzuriranja);

            return base.execute(entities);
        }

    }


    public class OpKorisniciDelete : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.KorisniciDelete(this.ZaposleniKorisniciDataSelect.id_korisnici);
            return base.execute(entities);
        }  
    }
    public class OpKorisniciRestore : OpZaposleniKorisniciBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            entities.RestoreIzbrisanKorisnik(this.ZaposleniKorisniciDataSelect.id_korisnici);
            return base.execute(entities);
        }  
    }
}