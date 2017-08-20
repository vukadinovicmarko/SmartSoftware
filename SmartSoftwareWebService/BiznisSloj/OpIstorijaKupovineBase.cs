using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpIstorijaKupovineBase: Operation
    {
        public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
        public DbItemKupljenaOpremaSaParametrima[] ListaKupljeneOpremaDataSelect { get; set; }


        protected List<DbItemKupljenaOpremaSaParametrima> VratiKupljenuOpremuZaIstorijuKupovine(int idIstorijaKupovine, SmartSoftwareBazaEntities entities)
        {
            List<DbItemKupljenaOpremaSaParametrima> Lista =
               (
                   from prodataOprema in entities.opremas
                   join istorijaOprema in entities.istorija_kupovine_oprema
                   on prodataOprema.id_oprema equals istorijaOprema.id_oprema
                   where istorijaOprema.id_istorija_kupovine == idIstorijaKupovine
                   select new DbItemKupljenaOpremaSaParametrima()
                   {
                       cena = prodataOprema.cena,
                       id_oprema = prodataOprema.id_oprema,
                       id_tip_opreme = prodataOprema.id_tip_opreme,
                       kolicina_na_lageru = prodataOprema.kolicina_na_lageru,
                       model = prodataOprema.model,
                       naslov = prodataOprema.naslov,
                       proizvodjac = prodataOprema.proizvodjac,
                       slika = prodataOprema.slika,
                       DeletedField = prodataOprema.deletedField,
                       kolicina_u_rezervi = prodataOprema.kolicina_u_rezervi,
                       lager = prodataOprema.lager,
                       opis = prodataOprema.opis,
                       oprema_na_popustu = prodataOprema.oprema_na_popustu,
                       slikaOriginalPutanja = prodataOprema.slika,
                       cena_opreme_kad_je_prodata = istorijaOprema.cena_opreme_kad_je_prodata,
                       id_istorija_kupovine = istorijaOprema.id_istorija_kupovine,
                       prodataKolicina = istorijaOprema.kolicina,
                       popust_na_cenu = istorijaOprema.popust_na_cenu,
                       ukupna_cena_artikla = istorijaOprema.ukupna_cena_artikla
                   }).ToList();

            for (int i = 0; i < Lista.Count; i++)
            {
                Lista[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(Lista[i].id_oprema, entities);
                Lista[i].slika = HttpContext.Current.Server.MapPath("." + Lista[i].slika).ToString();
            }

            return Lista;
        }

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     orderby istorijaKupovine.datum_prodaje descending
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca,
                         idProdavac = istorijaKupovine.id_prodavca,
                         ukupna_cena_kupovine = istorijaKupovine.ukupna_cena_kupovine,
                        broj_iskoriscenih_popust_poena = istorijaKupovine.broj_iskoriscenih_popust_poena
                     }
                ).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupljeneOpreme = VratiKupljenuOpremuZaIstorijuKupovine(item.id_istorija_kupovine, entities);
                item.Kupac = VratiKupcaZaIdKupca(item.idKupac,entities);
                item.prodavac = VratiProdavcaZaIdProdavca(item.idProdavac, entities);

            }
            

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;

            return opObj;
        }

        public static DbItemKorisnici VratiProdavcaZaIdProdavca(int? idProdavca, SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                (from korisnik in entities.korisnicis
                 where korisnik.id_korisnici == idProdavca
                 select new DbItemKorisnici()
                 {
                     id_korisnici = korisnik.id_korisnici,
                     broj_telefona = korisnik.broj_telefona,
                     brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                     ime = korisnik.ime,
                     mejl = korisnik.mejl,
                     slikaKorisnika = korisnik.slika_korisnika,
                     prezime = korisnik.prezime
                 }
                     ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            return niz == null || niz.Length == 0 || niz[0] == null ? null: niz[0];
        }

        public static DbItemKorisnici VratiKupcaZaIdKupca(int? idKupca, SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                   (from korisnik in entities.korisnicis
                    where korisnik.id_korisnici == idKupca
                    select new DbItemKorisnici()
                    {
                        id_korisnici = korisnik.id_korisnici,
                        broj_telefona = korisnik.broj_telefona,
                        brojOstvarenihPoena = korisnik.broj_ostvareni_poena,
                        ime = korisnik.ime,
                        mejl = korisnik.mejl,
                        prezime = korisnik.prezime,
                        slikaKorisnika = korisnik.slika_korisnika
                    }
                        ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            return niz == null || niz.Length == 0 || niz[0] == null ? null : niz[0];
        }

    }

    //public abstract class OpIstorijaKupovineZaListuKupacaBase : Operation
    //{
    //    public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
    //    public override OperationObject execute(SmartSoftwareBazaEntities entities)
    //    {

    //        DbItemIstorijaKupovine[] niz =
    //            (
    //                 from istorijaKupovine in entities.istorija_kupovine
    //                 select new DbItemIstorijaKupovine()
    //                 {
    //                     datum_prodaje = istorijaKupovine.datum_prodaje,
    //                     id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
    //                     idKupac = istorijaKupovine.id_kupca
    //                 }

    //            ).GroupBy(k => k.idKupac).Select(g => g.OrderByDescending(l => l.datum_prodaje).FirstOrDefault()).ToArray();
            
    //        foreach (var item in niz)
    //        {
    //            item.Kupac = VratiKupcaZaIdKupca(item.idKupac, entities);
    //        }

           

    //        OperationObject opObj = new OperationObject();
    //        opObj.Niz = niz;
    //        opObj.Success = true;

    //        return opObj;
    //    }

        

    //    public static DbItemKorisnici VratiKupcaZaIdKupca(int? idKupca, SmartSoftwareBazaEntities entities)
    //    {
    //        DbItemKorisnici[] niz =
    //               (from korisnik in entities.korisnicis
    //                where korisnik.id_korisnici == idKupca
    //                select new DbItemKorisnici()
    //                {
    //                    id_korisnici = korisnik.id_korisnici,
    //                    ime = korisnik.ime,
    //                    prezime = korisnik.prezime,
    //                    slikaKorisnika = korisnik.slika_korisnika
    //                }
    //                    ).ToArray();
    //        for (int i = 0; i < niz.Length; i++)
    //        {
    //            niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
    //        }
    //        return niz == null || niz.Length == 0 || niz[0] == null ? null : niz[0];
    //    }

    //}

    public class OpIstorijaKupovineSelect : OpIstorijaKupovineBase
    {

    }

    public class OpIstorijaKupovineZaListuKupacaDatumSelect : Operation
    {
        public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     where istorijaKupovine.id_kupca != null
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca
                     }

                ).GroupBy(k => k.idKupac).Select(g => g.OrderByDescending(l => l.datum_prodaje).FirstOrDefault()).ToArray();

            foreach (var item in niz)
            {
                item.Kupac = VratiKupcaZaIdKupca(item.idKupac, entities);
            }

            DbItemIstorijaKupovine[] niz2 = niz.OrderByDescending(k => k.datum_prodaje).ToArray();


            OperationObject opObj = new OperationObject();
            opObj.Niz = niz2;
            opObj.Success = true;

            return opObj;
        }



        public static DbItemKorisnici VratiKupcaZaIdKupca(int? idKupca, SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                   (from korisnik in entities.korisnicis
                    where korisnik.id_korisnici == idKupca
                    select new DbItemKorisnici()
                    {
                        id_korisnici = korisnik.id_korisnici,
                        ime = korisnik.ime,
                        prezime = korisnik.prezime,
                        slikaKorisnika = korisnik.slika_korisnika
                    }
                        ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            return niz == null || niz.Length == 0 || niz[0] == null ? null : niz[0];
        }
    }


    public class OpIstorijaKupovineZaListuKupacaDatumSelectPretraga : Operation
    {
        public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.IstorijaKupovineDataSelect == null)
                return new OperationObject()
                {
                    Niz = new DbItemIstorijaKupovine[0]
                };

            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     join kupac in entities.korisnicis 
                     on istorijaKupovine.id_kupca equals kupac.id_korisnici
                     where istorijaKupovine.id_kupca != null && kupac.ime.Contains(this.IstorijaKupovineDataSelect.searchKupacIme)
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca
                     }
                ).GroupBy(k => k.idKupac).Select(g => g.OrderByDescending(l => l.datum_prodaje).FirstOrDefault()).ToArray();

            foreach (var item in niz)
            {
                item.Kupac = OpIstorijaKupovineZaListuKupacaDatumSelect.VratiKupcaZaIdKupca(item.idKupac, entities);
            }

            DbItemIstorijaKupovine[] niz2 = niz.OrderByDescending(k => k.datum_prodaje).ToArray();


            OperationObject opObj = new OperationObject();
            opObj.Niz = niz2;
            opObj.Success = true;

            return opObj;
        }



       
    }


    public class OpIstorijaKupovineZaListuKupacaImeSelect : Operation
    {
        public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     where istorijaKupovine.id_kupca != null
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca
                     }

                ).GroupBy(k => k.idKupac).Select(g => g.FirstOrDefault()).ToArray();

            foreach (var item in niz)
            {
                item.Kupac = VratiKupcaZaIdKupca(item.idKupac, entities);
            }



            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;

            return opObj;
        }



        public static DbItemKorisnici VratiKupcaZaIdKupca(int? idKupca, SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] niz =
                   (from korisnik in entities.korisnicis
                    where korisnik.id_korisnici == idKupca
                    select new DbItemKorisnici()
                    {
                        id_korisnici = korisnik.id_korisnici,
                        ime = korisnik.ime,
                        prezime = korisnik.prezime,
                        slikaKorisnika = korisnik.slika_korisnika
                    }
                        ).OrderBy(k => k.ime).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            return niz == null || niz.Length == 0 || niz[0] == null ? null : niz[0];
        }
    }

    public class OpIstorijaKupovineZaListuKupacaBrojKupovinaSelect : Operation
    {
        public DbItemIstorijaKupovine IstorijaKupovineDataSelect { get; set; }
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            ObjectResult<IstorijaKupovineNajcesciKupci_Result> rez = null;
            if (IstorijaKupovineDataSelect != null)
            {
                rez = entities.IstorijaKupovineNajcesciKupci(IstorijaKupovineDataSelect.asc);
            }

            IstorijaKupovineNajcesciKupci_Result[] tmpNiz = rez.ToArray();
            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     where istorijaKupovine.id_kupca != null
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca
                     }

                ).GroupBy(k => k.idKupac).Select(g => g.FirstOrDefault()).ToArray();

            foreach (var item in niz)
            {
                item.Kupac = VratiKupcaZaIdKupca(item.idKupac, entities, tmpNiz);
                
            }
            DbItemIstorijaKupovine[] niz2 = niz.OrderByDescending(o => o.Kupac.brojKupovina).ToArray();


            OperationObject opObj = new OperationObject();
            opObj.Niz = niz2;
            opObj.Success = true;

            return opObj;
        }



        public static DbItemKorisnici VratiKupcaZaIdKupca(int? idKupca, SmartSoftwareBazaEntities entities, IstorijaKupovineNajcesciKupci_Result[] tmpNiz)
        {

            DbItemKorisnici[] niz =
                    (from grupisanKupac in tmpNiz
                     join kupac in entities.korisnicis
                     on grupisanKupac.id_kupca equals kupac.id_korisnici
                     where grupisanKupac.id_kupca == idKupca 
                     select new DbItemKorisnici()
                     {
                         id_korisnici = kupac.id_korisnici,
                         ime = kupac.ime,
                         prezime = kupac.prezime,
                         slikaKorisnika = kupac.slika_korisnika,
                         brojKupovina = grupisanKupac.brojKupovina
                    }
                        ).OrderByDescending(k => k.brojKupovina).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slikaKorisnika = HttpContext.Current.Server.MapPath("." + niz[i].slikaKorisnika).ToString();
            }
            return niz == null || niz.Length == 0 || niz[0] == null ? null : niz[0];
        }
    }

    public class OpIstorijaKupovineInsert : OpIstorijaKupovineBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            System.Data.Objects.ObjectParameter idIstorijaKupovinePar = new System.Data.Objects.ObjectParameter("id_istorija_kupovine", System.Type.GetType("System.Int32"));

            if(this.IstorijaKupovineDataSelect != null && this.ListaKupljeneOpremaDataSelect != null)
            {

                int? idKupca = (this.IstorijaKupovineDataSelect.Kupac == null ? 0 : this.IstorijaKupovineDataSelect.Kupac.id_korisnici);

                entities.DodajUIstorijuKupovine(idIstorijaKupovinePar, this.IstorijaKupovineDataSelect.datum_prodaje, this.IstorijaKupovineDataSelect.prodavac.id_korisnici, idKupca == 0 ? null : idKupca, this.IstorijaKupovineDataSelect.ukupna_cena_kupovine, this.IstorijaKupovineDataSelect.broj_iskoriscenih_popust_poena);

                int idIstorijeKupovine = Convert.ToInt32(idIstorijaKupovinePar.Value);

                foreach (var item in this.ListaKupljeneOpremaDataSelect)
                {
                    entities.DodajUIstorijuKupovineOprema(item.id_oprema, item.prodataKolicina, item.cena, item.popust_na_cenu, item.ukupna_cena_artikla, idIstorijeKupovine);
                }
            
            }
            //ovde dole treba da stavimo return base.execute();
            //ali za to mora da napravmo gore select u base klasi
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            opObj.Niz = new DbItemKupljenaOpremaSaParametrima[5];
            return opObj;
        }
    }


    public class OpIstorijaKupovineNajprodavanijaOprema : OpIstorijaKupovineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.IstorijaKupovineDataSelect != null)
            {
                ObjectResult<IstorijaKupovineNajprodavanijaOprema_Result> rez = entities.IstorijaKupovineNajprodavanijaOprema(this.IstorijaKupovineDataSelect.asc);
                IstorijaKupovineNajprodavanijaOprema_Result[] tmpNiz = rez.ToArray();
                DbItemOpremaSaParametrimaStatistika[] oprema =
                    (from najprodavanijaOprema in tmpNiz
                     join opremaIzBaze in entities.opremas
                     on najprodavanijaOprema.id_oprema equals opremaIzBaze.id_oprema
                     select new DbItemOpremaSaParametrimaStatistika()
                     {
                         cena = opremaIzBaze.cena,
                         DeletedField = opremaIzBaze.deletedField,
                         id_oprema = opremaIzBaze.id_oprema,
                         id_tip_opreme = opremaIzBaze.id_tip_opreme,
                         kolicina_na_lageru = opremaIzBaze.kolicina_na_lageru,
                         kolicina_u_rezervi = opremaIzBaze.kolicina_u_rezervi,
                         lager = opremaIzBaze.lager,
                         model = opremaIzBaze.model,
                         naslov = opremaIzBaze.naslov,
                         opis = opremaIzBaze.opis,
                         proizvodjac = opremaIzBaze.proizvodjac,
                         slika = opremaIzBaze.slika,
                         oprema_na_popustu = opremaIzBaze.oprema_na_popustu,
                         kolkoPutaJeProdavata = najprodavanijaOprema.prodataKolicinaOpreme,
                         slikaOriginalPutanja = opremaIzBaze.slika
                     }).ToArray();

                for (int i = 0; i < oprema.Length; i++)
                {
                    oprema[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(oprema[i].id_oprema, entities);
                    oprema[i].slika = HttpContext.Current.Server.MapPath("." + oprema[i].slika).ToString();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = oprema;
                opObj.Success = true;
                return opObj;
            }
            else
            {
                return base.execute(entities);
            }
        }
    }


    public class OpIstorijaKupovineUkupnaKolicinaProdateOpreme : OpIstorijaKupovineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            var items = (from istorijaKupovineOprema in entities.istorija_kupovine_oprema
                        select new { istorijaKupovineOprema.kolicina }).ToList();
            var sum = items.Select(c => c.kolicina).Sum();
            Object[] niz = new Object[1];
            niz[0] = sum as int?;
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpIstorijaKupovineKolicinaProdateOpremeZaDanas : OpIstorijaKupovineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            System.Data.Objects.ObjectParameter rez = new System.Data.Objects.ObjectParameter("brojProdateOpremeDanas", System.Type.GetType("System.Int32"));
            

            if (this.IstorijaKupovineDataSelect != null)
            {
                entities.IstorijaKupovineUkupnoProdanoOpremeDanas(this.IstorijaKupovineDataSelect.datumDanas, this.IstorijaKupovineDataSelect.datumSutra, rez);
            }

            int prodatoOpremeDanas = rez.Value == null ?  0 : Convert.ToInt32(rez.Value);
            Object[] niz = new Object[1];
            niz[0] = prodatoOpremeDanas;
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpIstorijaKupovineZaradjenoDanas : OpIstorijaKupovineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            System.Data.Objects.ObjectParameter rez = new System.Data.Objects.ObjectParameter("ukupnoZaradjeno", System.Type.GetType("System.Double"));
            
            if (this.IstorijaKupovineDataSelect != null)
            {
                entities.IstorijaKupovineZaradjenoDanas(this.IstorijaKupovineDataSelect.datumDanas, this.IstorijaKupovineDataSelect.datumSutra, rez);
            }

            double ukupnoZaradjeno = Convert.ToDouble(rez.Value);
            Object[] niz = new Object[1];
            niz[0] = ukupnoZaradjeno;
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpIstorijaKupovineUkupnoZaradjeno : OpIstorijaKupovineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            System.Data.Objects.ObjectParameter rez = new System.Data.Objects.ObjectParameter("ukupnoZaradjeno", System.Type.GetType("System.Double"));

            entities.IstorijaKupovineUkupnoZaradjeno(rez);
            
            double ukupnoZaradjeno = Convert.ToDouble(rez.Value);
            Object[] niz = new Object[1];
            niz[0] = ukupnoZaradjeno;
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }


    public abstract class OpIstorijaKupovineKupci : OpIstorijaKupovineBase
    {
        protected DbItemIstorijaKupovine[] ListaKupovinaZaKorisnika(int idKupca, SmartSoftwareBazaEntities entities)
        {
            DbItemIstorijaKupovine[] niz =
                (
                     from istorijaKupovine in entities.istorija_kupovine
                     where istorijaKupovine.id_kupca == idKupca
                     orderby istorijaKupovine.datum_prodaje descending
                     select new DbItemIstorijaKupovine()
                     {
                         datum_prodaje = istorijaKupovine.datum_prodaje,
                         id_istorija_kupovine = istorijaKupovine.id_istorija_kupovine,
                         idKupac = istorijaKupovine.id_kupca,
                         idProdavac = istorijaKupovine.id_prodavca,
                         ukupna_cena_kupovine = istorijaKupovine.ukupna_cena_kupovine,
                         broj_iskoriscenih_popust_poena = istorijaKupovine.broj_iskoriscenih_popust_poena
                     }
                ).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupljeneOpreme = VratiKupljenuOpremuZaIstorijuKupovine(item.id_istorija_kupovine, entities);
                //item.Kupac = VratiKupcaZaIstorijuKupovine(item.idKupac, entities);
                item.prodavac = VratiProdavcaZaIdProdavca(item.idProdavac, entities);
            }
            

            return niz;
        }
    }

    public class OpIstorijaKupovineKupciInitPrikaz : OpIstorijaKupovineKupci
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            ObjectResult<int?> rez = null;
            rez = entities.IstorijaKupovineKupciInitPrikaz();
            int?[] tmpNiz = rez.ToArray();
            DbItemKupci[] niz =
                    (from grupisanKupac in tmpNiz
                     join kupac in entities.korisnicis
                     on grupisanKupac.Value equals kupac.id_korisnici
                     select new DbItemKupci()
                     {
                         id_korisnici = kupac.id_korisnici,
                         brojOstvarenihPoena = kupac.broj_ostvareni_poena,
                         broj_telefona = kupac.broj_telefona,
                         deletedField = kupac.deletedField,
                         id_uloge = kupac.id_uloge,
                         ime = kupac.ime,
                         lozinka = kupac.lozinka,
                         mejl = kupac.mejl,
                         prezime = kupac.prezime,
                         username = kupac.username,
                         
                         slikaKorisnika = kupac.slika_korisnika
                     }).OrderBy(p => p.ime).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupovina = ListaKupovinaZaKorisnika(item.id_korisnici, entities);
                item.slikaKorisnika = HttpContext.Current.Server.MapPath("." + item.slikaKorisnika).ToString();
                
            }
            //if(niz != null && niz.Length > 0)
            //{
            //    for (int i = 0; i < niz.Length; i++)
            //    {
            //        if(niz[i].ListaKupovina != null && niz[i].ListaKupovina.Length > 0)
            //        {
            //            niz.OrderBy(p => p.ListaKupovina[i].datum_prodaje);
            //        }
            //    }
            //}
           

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }


    public class OpIstorijaKupovineNajcesciKupci : OpIstorijaKupovineKupci
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            ObjectResult<IstorijaKupovineNajcesciKupci_Result> rez = null;
            if (this.IstorijaKupovineDataSelect != null)
            {
               rez = entities.IstorijaKupovineNajcesciKupci(this.IstorijaKupovineDataSelect.asc);
            }

            IstorijaKupovineNajcesciKupci_Result[] tmpNiz = rez.ToArray();

            DbItemKupci[] niz = 
                    (from grupisanKupac in tmpNiz
                    join kupac in entities.korisnicis
                    on grupisanKupac.id_kupca equals kupac.id_korisnici
                    select new DbItemKupci()
                    {
                        id_korisnici = kupac.id_korisnici,
                        brojKupovina = grupisanKupac.brojKupovina,
                        brojOstvarenihPoena = kupac.broj_ostvareni_poena,
                        broj_telefona = kupac.broj_telefona,
                        deletedField = kupac.deletedField,
                        id_uloge = kupac.id_uloge,
                        ime = kupac.ime,
                        lozinka = kupac.lozinka,
                        mejl = kupac.mejl,
                        prezime = kupac.prezime,
                        username = kupac.username,
                        slikaKorisnika = kupac.slika_korisnika
                    }).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupovina = ListaKupovinaZaKorisnika(item.id_korisnici, entities);
                item.slikaKorisnika = HttpContext.Current.Server.MapPath("." + item.slikaKorisnika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
           
        }
    }

    public class OpIstorijaKupovinekupciKojiNajviseKupuju : OpIstorijaKupovineKupci
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            ObjectResult<IstorijaKupovineKupciKojiNajviseKupuju_Result> rez = null;
            if (this.IstorijaKupovineDataSelect != null)
            {
                rez = entities.IstorijaKupovineKupciKojiNajviseKupuju(this.IstorijaKupovineDataSelect.asc);
            }

            IstorijaKupovineKupciKojiNajviseKupuju_Result[] tmpNiz = rez.ToArray();

            DbItemKupci[] niz =
                    (from grupisanKupac in tmpNiz
                     join kupac in entities.korisnicis
                     on grupisanKupac.id_kupca equals kupac.id_korisnici
                     select new DbItemKupci()
                     {
                         id_korisnici = kupac.id_korisnici,
                         brojKupovina = grupisanKupac.kupljenoOpreme,
                         brojOstvarenihPoena = kupac.broj_ostvareni_poena,
                         broj_telefona = kupac.broj_telefona,
                         deletedField = kupac.deletedField,
                         id_uloge = kupac.id_uloge,
                         ime = kupac.ime,
                         lozinka = kupac.lozinka,
                         mejl = kupac.mejl,
                         prezime = kupac.prezime,
                         username = kupac.username,
                         slikaKorisnika = kupac.slika_korisnika
                     }).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupovina = ListaKupovinaZaKorisnika(item.id_korisnici, entities);
                item.slikaKorisnika = HttpContext.Current.Server.MapPath("." + item.slikaKorisnika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;

        }
    }

    public class OpIstorijaKupovinePotrosnjaKupaca : OpIstorijaKupovineKupci
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            ObjectResult<IstorijaKupovinePotrosnjaKupaca_Result> rez = null;
            if (this.IstorijaKupovineDataSelect != null)
            {
                rez = entities.IstorijaKupovinePotrosnjaKupaca(this.IstorijaKupovineDataSelect.asc);
            }

            IstorijaKupovinePotrosnjaKupaca_Result[] tmpNiz = rez.ToArray();

            DbItemKupci[] niz =
                    (from grupisanKupac in tmpNiz
                     join kupac in entities.korisnicis
                     on grupisanKupac.id_kupca equals kupac.id_korisnici
                     select new DbItemKupci()
                     {
                         id_korisnici = kupac.id_korisnici,
                         ukupnoPotrosio = grupisanKupac.ukupnoPotrosio,
                         brojOstvarenihPoena = kupac.broj_ostvareni_poena,
                         broj_telefona = kupac.broj_telefona,
                         deletedField = kupac.deletedField,
                         id_uloge = kupac.id_uloge,
                         ime = kupac.ime,
                         lozinka = kupac.lozinka,
                         mejl = kupac.mejl,
                         prezime = kupac.prezime,
                         username = kupac.username,
                         slikaKorisnika = kupac.slika_korisnika
                     }).ToArray();

            foreach (var item in niz)
            {
                item.ListaKupovina = ListaKupovinaZaKorisnika(item.id_korisnici, entities);
                item.slikaKorisnika = HttpContext.Current.Server.MapPath("." + item.slikaKorisnika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;

        }
    }
    [DataContract]
    public class DbItemIstorijaKupovine
    {

        [DataMember]
        public int id_istorija_kupovine { get; set; }

        [DataMember]
        public System.DateTime datum_prodaje { get; set; }
        [DataMember]
        public DbItemKorisnici prodavac { get; set; }
        [DataMember]
        public DbItemKorisnici Kupac { get; set; }

        [DataMember]
        public string searchKupacIme { get; set; }

        [DataMember]
        public int? idProdavac { get; set; }
        [DataMember]
        public int? idKupac { get; set; }

        [DataMember]
        public double? broj_iskoriscenih_popust_poena { get; set; }

        [DataMember]
        public double ukupna_cena_kupovine { get; set; }
        [DataMember]
        public List<DbItemKupljenaOpremaSaParametrima> ListaKupljeneOpreme { get; set; }


        [DataMember]
        public bool asc { get; set; }


        [DataMember]
        public DateTime datumDanas { get; set; }

        [DataMember]
        public DateTime datumSutra { get; set; }
    }

    [DataContract]
    public class DbItemKupljenaOpremaSaParametrima : DbItemOpremaSaParametrima
    {
        [DataMember]
        public int? prodataKolicina { get; set; }
        [DataMember]
        public double? cena_opreme_kad_je_prodata { get; set; }
        [DataMember]
        public double? popust_na_cenu { get; set; }
        [DataMember]
        public int? ukupna_cena_artikla { get; set; }
        [DataMember]
        public int id_istorija_kupovine { get; set; }
    }

    [DataContract]
    public class DbItemOpremaSaParametrimaStatistika : DbItemOpremaSaParametrima
    {
        [DataMember]
        public int? kolkoPutaJeProdavata { get; set; }
    }
}