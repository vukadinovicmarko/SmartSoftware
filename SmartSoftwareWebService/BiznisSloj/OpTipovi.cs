using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpTipovi:Operation
    {
        public DbItemTipOpreme DataSelectTipoviOpreme { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemTipOpreme[] tipoviOpremeNiz =
                   (
                       from tipovi in entities.tip_opreme
                       join oblastiOpreme in entities.oblasti_opreme
                       on tipovi.id_oblasti_opreme equals oblastiOpreme.id_oblasti_opreme into joinovani
                       from leftOuterTipoviOblasti in joinovani.DefaultIfEmpty()
                       where tipovi.deletedField == false
                       select new DbItemTipOpreme()
                       {
                           naziv_tipa = tipovi.naziv_tipa,
                           id_tip_opreme = tipovi.id_tip_opreme,
                           slika_tipa = tipovi.slika_tipa,
                           id_oblasti_opreme = tipovi.id_oblasti_opreme,
                           SlikaOriginalPutanja = tipovi.slika_tipa,
                           DeletedField = tipovi.deletedField,
                           naziv_oblasti_opreme = leftOuterTipoviOblasti.naziv_oblasti_opreme
                       }
                   ).ToArray();

            for (int i = 0; i < tipoviOpremeNiz.Length; i++)
            {
                tipoviOpremeNiz[i].slika_tipa = HttpContext.Current.Server.MapPath("." + tipoviOpremeNiz[i].slika_tipa).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = tipoviOpremeNiz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpTipoviOpremeSelect : OpTipovi
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme == null)
            {
                return base.execute(entities);
            }
            else
            {
                oprema[] opremaZaTipNiz =
                  (
                      from tipovi in entities.tip_opreme
                      join opreme in entities.opremas
                      on tipovi.id_tip_opreme equals opreme.id_tip_opreme
                      where tipovi.id_tip_opreme == DataSelectTipoviOpreme.id_tip_opreme
                      select opreme
                  ).ToArray();

                OperationObject opObj = new OperationObject();
                opObj.Niz = opremaZaTipNiz;
                opObj.Success = true;
                return opObj;
            }
        }
    }

    public abstract class OpTipoviAdminPanel : Operation
    {
        public DbItemTipOpreme DataSelectTipoviOpreme { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemTipOpreme[] tipoviOpremeNiz =
                   (
                       from tipovi in entities.tip_opreme
                       join oblastiOpreme in entities.oblasti_opreme
                       on tipovi.id_oblasti_opreme equals oblastiOpreme.id_oblasti_opreme into joinovani
                       from leftOuterTipoviOblasti in joinovani.DefaultIfEmpty()
                       select new DbItemTipOpreme()
                       {
                           naziv_tipa = tipovi.naziv_tipa,
                           id_tip_opreme = tipovi.id_tip_opreme,
                           slika_tipa = tipovi.slika_tipa,
                           id_oblasti_opreme = tipovi.id_oblasti_opreme,
                           SlikaOriginalPutanja = tipovi.slika_tipa,
                           DeletedField = tipovi.deletedField,
                           naziv_oblasti_opreme = leftOuterTipoviOblasti.naziv_oblasti_opreme
                       }
                   ).ToArray();

            for (int i = 0; i < tipoviOpremeNiz.Length; i++)
            {
                tipoviOpremeNiz[i].slika_tipa = HttpContext.Current.Server.MapPath("." + tipoviOpremeNiz[i].slika_tipa).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = tipoviOpremeNiz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpTipoviOpremeAdminPanelSelect : OpTipoviAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme == null)
            {
                return base.execute(entities);
            }
            else
            {
                oprema[] opremaZaTipNiz =
                  (
                      from tipovi in entities.tip_opreme
                      join opreme in entities.opremas
                      on tipovi.id_tip_opreme equals opreme.id_tip_opreme
                      where tipovi.id_tip_opreme == DataSelectTipoviOpreme.id_tip_opreme
                      select opreme
                  ).ToArray();

                OperationObject opObj = new OperationObject();
                opObj.Niz = opremaZaTipNiz;
                opObj.Success = true;
                return opObj;
            }
        }
    }





    public class OpTipoviOpremeInsert : OpTipoviAdminPanel
    {
        public override OperationObject execute( SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme != null)
            {
                entities.TipoviOpremeInsert(this.DataSelectTipoviOpreme.naziv_tipa, this.DataSelectTipoviOpreme.id_oblasti_opreme, this.DataSelectTipoviOpreme.slika_tipa);
            }
            return base.execute(entities);
        }
    }

    public class OpTipoviOpremeUpdate : OpTipoviAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme != null)
            {
                entities.TipoviOpremeUpdate
                (
                    this.DataSelectTipoviOpreme.id_tip_opreme,
                    this.DataSelectTipoviOpreme.naziv_tipa,
                    this.DataSelectTipoviOpreme.id_oblasti_opreme,
                    this.DataSelectTipoviOpreme.slika_tipa
                );
            }

            return base.execute(entities);
        }
    }

    public class OpTipoviOpremeDelete : OpTipoviAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme != null)
            {
                entities.TipoviOpremeDelete(this.DataSelectTipoviOpreme.id_tip_opreme);
            }
            return base.execute(entities);
        }
    }


    public class OpTipoviOpremeRestore : OpTipoviAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectTipoviOpreme != null)
            {
                entities.RestoreIzbrisanTipOpreme(this.DataSelectTipoviOpreme.id_tip_opreme);
            }
            return base.execute(entities);
        }
    }

    public class OpTipoviOpremePretraga : OpTipoviAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            DbItemTipOpreme[] niz =
             (
                from tipoviOpreme in entities.tip_opreme
                join oblastiOpreme in entities.oblasti_opreme
                on tipoviOpreme.id_oblasti_opreme equals oblastiOpreme.id_oblasti_opreme
                where tipoviOpreme.naziv_tipa.Contains(DataSelectTipoviOpreme.ZaPretragu)
                select new DbItemTipOpreme()
                {
                    id_oblasti_opreme = tipoviOpreme.id_oblasti_opreme,
                    id_tip_opreme = tipoviOpreme.id_tip_opreme,
                    naziv_tipa = tipoviOpreme.naziv_tipa,
                    slika_tipa = tipoviOpreme.slika_tipa,
                    SlikaOriginalPutanja = tipoviOpreme.slika_tipa,
                    naziv_oblasti_opreme = oblastiOpreme.naziv_oblasti_opreme
                }
              ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].slika_tipa = HttpContext.Current.Server.MapPath("." + niz[i].slika_tipa).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }




    public class OpTipoviOpremeZaKonfiguraciju : OpTipovi
    {
        public DbItemTipoviZaKonfiguraciju[] DataSelectListaTipovaZaKonfiguraciju { get; set; }

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            DbItemTipoviZaKonfiguraciju dataselect = (this.DataSelectTipoviOpreme as DbItemTipoviZaKonfiguraciju);

            DbItemTipoviZaKonfiguraciju[] niz =
                  (
                      from tipovi in entities.tip_opreme
                      join konfiguracija in entities.konfiguracija_kolekcije
                      on tipovi.id_tip_opreme equals konfiguracija.id_tip_opreme_deo_kolekcije into joinovani
                      from leftOuterTipoviZaKonfiguraciju in joinovani.DefaultIfEmpty()
                      where leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == dataselect.idTipOpremeKolekcije
                      select new DbItemTipoviZaKonfiguraciju()
                      {
                          naziv_tipa = tipovi.naziv_tipa,
                          id_tip_opreme = tipovi.id_tip_opreme,
                          slika_tipa = tipovi.slika_tipa,
                          id_oblasti_opreme = tipovi.id_oblasti_opreme,
                          SlikaOriginalPutanja = tipovi.slika_tipa,
                          DeletedField = tipovi.deletedField,
                          idTipOpremeKolekcije = leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije,
                          idTipOpremeDeoKolekcije = leftOuterTipoviZaKonfiguraciju.id_tip_opreme_deo_kolekcije,
                          redosledPrikazivanja = leftOuterTipoviZaKonfiguraciju.redosled_prikazivanja,
                          izabranZaKonfiguraciju = leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == null || leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == null ? false : true,
                          moguca_kolicina_unosa = leftOuterTipoviZaKonfiguraciju.moguca_kolicina_unosa
                      }).ToArray();

            //foreach (var item in niz)
            //{
            //    item.izabranZaKonfiguraciju = item.idTipOpremeDeoKolekcije == null || item.idTipOpremeDeoKolekcije == 0 ? false : true;
            //}

            //DbItemTipoviZaKonfiguraciju[] niz1 = niz.Where(p => p.idTipOpremeKolekcije == dataselect.idTipOpremeKolekcije).ToArray();





            //ovo dole treba zavrsiti

            OpTipoviOpremeSelect op = new OpTipoviOpremeSelect();
            OperationObject rez = OperationManager.Singleton.executeOp(op);

            List<DbItemTipoviZaKonfiguraciju> lista = new List<DbItemTipoviZaKonfiguraciju>();

            DbItemTipOpreme[] niz1 = rez.Niz as DbItemTipOpreme[];

            foreach (var item in niz1)
            {
                bool pronadjenItem = false;
                foreach (var item1 in niz)
                {
                    if(item.id_tip_opreme == item1.id_tip_opreme)
                    {
                        lista.Add(new DbItemTipoviZaKonfiguraciju()
                        {
                            id_tip_opreme = item1.id_tip_opreme,
                            izabranZaKonfiguraciju = true,
                            id_oblasti_opreme = item1.id_oblasti_opreme,
                            idTipOpremeDeoKolekcije = item1.idTipOpremeDeoKolekcije,
                            idTipOpremeKolekcije = item1.idTipOpremeKolekcije,
                            redosledPrikazivanja = item1.redosledPrikazivanja,
                            DeletedField = item1.DeletedField,
                            naziv_oblasti_opreme = item1.naziv_oblasti_opreme,
                            naziv_tipa = item1.naziv_tipa,
                            slika_tipa = item1.slika_tipa,
                            SlikaOriginalPutanja = item1.SlikaOriginalPutanja,
                            ZaPretragu = item1.ZaPretragu
                        });
                        pronadjenItem = true;
                    }
                }
                if(!pronadjenItem)
                {
                    lista.Add(new DbItemTipoviZaKonfiguraciju()
                    {
                        id_tip_opreme = item.id_tip_opreme,
                        izabranZaKonfiguraciju = false,
                        id_oblasti_opreme = item.id_oblasti_opreme,
                        DeletedField = item.DeletedField,
                        naziv_oblasti_opreme = item.naziv_oblasti_opreme,
                        naziv_tipa = item.naziv_tipa,
                        slika_tipa = item.slika_tipa,
                        SlikaOriginalPutanja = item.SlikaOriginalPutanja,
                        ZaPretragu = item.ZaPretragu
                    });
                }
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = lista.ToArray();
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpTipoviOpremeZaKonfiguracijuInsert : OpTipoviOpremeZaKonfiguraciju
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            DbItemTipoviZaKonfiguraciju dataselect = (this.DataSelectTipoviOpreme as DbItemTipoviZaKonfiguraciju);
            foreach (var item in this.DataSelectListaTipovaZaKonfiguraciju)
            {
                entities.KonfigurcijaKolekcijeInsertUpdate(dataselect.idTipOpremeKolekcije, item.id_tip_opreme, item.redosledPrikazivanja, item.izabranZaKonfiguraciju);
            }
            OpTipoviOpremeZaKonfiguracijuZaIdKonfigurcije op = new OpTipoviOpremeZaKonfiguracijuZaIdKonfigurcije();
            op.DataSelectTipoviOpreme = dataselect;
            return OperationManager.Singleton.executeOp(op);
        }
    }

    public class OpTipoviOpremeZaKonfiguracijuZaIdKonfigurcije : OpTipoviOpremeZaKonfiguraciju
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            DbItemTipoviZaKonfiguraciju dataselect = (this.DataSelectTipoviOpreme as DbItemTipoviZaKonfiguraciju);

            DbItemTipoviZaKonfiguraciju[] niz =
                  (
                      from tipovi in entities.tip_opreme
                      join konfiguracija in entities.konfiguracija_kolekcije
                      on tipovi.id_tip_opreme equals konfiguracija.id_tip_opreme_deo_kolekcije into joinovani
                      from leftOuterTipoviZaKonfiguraciju in joinovani.DefaultIfEmpty()
                      where leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == dataselect.idTipOpremeKolekcije
                      select new DbItemTipoviZaKonfiguraciju()
                      {
                          naziv_tipa = tipovi.naziv_tipa,
                          id_tip_opreme = tipovi.id_tip_opreme,
                          slika_tipa = tipovi.slika_tipa,
                          id_oblasti_opreme = tipovi.id_oblasti_opreme,
                          SlikaOriginalPutanja = tipovi.slika_tipa,
                          DeletedField = tipovi.deletedField,
                          idTipOpremeKolekcije = leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije,
                          idTipOpremeDeoKolekcije = leftOuterTipoviZaKonfiguraciju.id_tip_opreme_deo_kolekcije,
                          redosledPrikazivanja = leftOuterTipoviZaKonfiguraciju.redosled_prikazivanja,
                          izabranZaKonfiguraciju = leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == null || leftOuterTipoviZaKonfiguraciju.Id_tip_opreme_kolekcije == null ? false : true,
                          moguca_kolicina_unosa = leftOuterTipoviZaKonfiguraciju.moguca_kolicina_unosa
                      }).ToArray();            

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }


}