using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public abstract class OpOpremaBase:Operation
    {
        public DbItemOpremaSaParametrima DataSelectOprema { get; set; } 

        public static List<DbItemParametri> VratiParametreZaOpremu(int idOpreme, DataSloj.SmartSoftwareBazaEntities entities)
        {
            List<DbItemParametri> lista =
                (
                    from parametar in entities.parametris
                    join parametarOprema in entities.parametarOpremas
                    on parametar.id_parametri equals parametarOprema.id_parametri
                    where parametarOprema.id_oprema == idOpreme
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        vrednost_parametra = parametarOprema.vrednost_parametra
                    }
                 ).ToList();
            return lista;
        }

       public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
                (
                    from oprema in entities.opremas
                    select new DbItemOpremaSaParametrima()
                    {
                        cena = oprema.cena,
                        id_oprema = oprema.id_oprema,
                        id_tip_opreme = oprema.id_tip_opreme,
                        kolicina_na_lageru = oprema.kolicina_na_lageru,
                        kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                        lager = oprema.lager,
                        model = oprema.model,
                        naslov = oprema.naslov,
                        opis = oprema.opis,
                        oprema_na_popustu = oprema.oprema_na_popustu,
                        proizvodjac = oprema.proizvodjac,
                        slikaOriginalPutanja = oprema.slika,
                        slika = oprema.slika,
                        DeletedField = oprema.deletedField
                    }
                /*
                 * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                 * 
                 * treba da se za svaki posebno laptop pozove metod koji vraca
                 * njegove parametre (kao kolerisan upit) valjda
                 */
                ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public abstract class OpOpremaBaseZaRezervacijuInsert : Operation
    {
        public DbItemOprema DataSelectOprema { get; set; }

        public static List<DbItemParametri> VratiParametreZaOpremu(int idOpreme, DataSloj.SmartSoftwareBazaEntities entities)
        {
            List<DbItemParametri> lista =
                (
                    from parametar in entities.parametris
                    join parametarOprema in entities.parametarOpremas
                    on parametar.id_parametri equals parametarOprema.id_parametri
                    where parametarOprema.id_oprema == idOpreme
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        vrednost_parametra = parametarOprema.vrednost_parametra
                    }
                 ).ToList();
            return lista;
        }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
                (
                    from oprema in entities.opremas
                    select new DbItemOpremaSaParametrima()
                    {
                        cena = oprema.cena,
                        id_oprema = oprema.id_oprema,
                        id_tip_opreme = oprema.id_tip_opreme,
                        kolicina_na_lageru = oprema.kolicina_na_lageru,
                        kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                        lager = oprema.lager,
                        model = oprema.model,
                        naslov = oprema.naslov,
                        opis = oprema.opis,
                        oprema_na_popustu = oprema.oprema_na_popustu,
                        proizvodjac = oprema.proizvodjac,
                        slikaOriginalPutanja = oprema.slika,
                        slika = oprema.slika,
                        DeletedField = oprema.deletedField
                    }
                /*
                 * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                 * 
                 * treba da se za svaki posebno laptop pozove metod koji vraca
                 * njegove parametre (kao kolerisan upit) valjda
                 */
                ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                 niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpOpremaSelect : OpOpremaBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {

            if(this.DataSelectOprema == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   where oprema.id_tip_opreme == DataSelectOprema.id_tip_opreme
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika,
                       DeletedField = oprema.deletedField
                   }
                    /*
                     * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                     * 
                     * treba da se za svaki posebno laptop pozove metod koji vraca
                     * njegove parametre (kao kolerisan upit) valjda
                     */
               ).ToArray();

                kolekcija_opreme[] svaOpremaIzKolekcijeOpreme =
               (from kolekcijaOpreme in entities.kolekcija_opreme
                select kolekcijaOpreme).ToArray();


                for (int i = 0; i < niz.Length; i++)
                {
                    niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                    niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
                    DbItemKolekcijaOpreme kolekcijaOpreme = (niz[i] as DbItemKolekcijaOpreme);
                    if (kolekcijaOpreme != null)
                    {
                        kolekcijaOpreme.KolekcijaOpreme = OpKolekcijaOpremeBase.kolekcijaOpremeZaIdKolekcije(niz[i].id_oprema, svaOpremaIzKolekcijeOpreme, entities);
                    }
                }



                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }
 
        }
    }


    public abstract class OpOpremaGlavniProzorBase : Operation
    {
        public DbItemOpremaSaParametrima DataSelectOprema { get; set; }

        public static List<DbItemParametri> VratiParametreZaOpremuGlavniProzor(int idOpreme, DataSloj.SmartSoftwareBazaEntities entities)
        {
            List<DbItemParametri> lista =
                (
                    from parametar in entities.parametris
                    join parametarOprema in entities.parametarOpremas
                    on parametar.id_parametri equals parametarOprema.id_parametri
                    where parametarOprema.id_oprema == idOpreme && parametarOprema.deletedField == false
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        vrednost_parametra = parametarOprema.vrednost_parametra
                    }
                 ).ToList();
            return lista;
        }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
                (
                    from oprema in entities.opremas
                    where oprema.deletedField == false
                    select new DbItemOpremaSaParametrima()
                    {
                        cena = oprema.cena,
                        id_oprema = oprema.id_oprema,
                        id_tip_opreme = oprema.id_tip_opreme,
                        kolicina_na_lageru = oprema.kolicina_na_lageru,
                        kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                        lager = oprema.lager,
                        model = oprema.model,
                        naslov = oprema.naslov,
                        opis = oprema.opis,
                        oprema_na_popustu = oprema.oprema_na_popustu,
                        proizvodjac = oprema.proizvodjac,
                        slikaOriginalPutanja = oprema.slika,
                        slika = oprema.slika,
                        DeletedField = oprema.deletedField
                    }
                /*
                 * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                 * 
                 * treba da se za svaki posebno laptop pozove metod koji vraca
                 * njegove parametre (kao kolerisan upit) valjda
                 */
                ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpOpremaGlavniProzorSelect : OpOpremaGlavniProzorBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {

            if (this.DataSelectOprema == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   where oprema.id_tip_opreme == DataSelectOprema.id_tip_opreme && oprema.deletedField == false
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika,
                       DeletedField = oprema.deletedField
                   }
                    /*
                     * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                     * 
                     * treba da se za svaki posebno laptop pozove metod koji vraca
                     * njegove parametre (kao kolerisan upit) valjda
                     */
               ).ToArray();

                kolekcija_opreme[] svaOpremaIzKolekcijeOpreme =
               (from kolekcijaOpreme in entities.kolekcija_opreme
                select kolekcijaOpreme).ToArray();


                for (int i = 0; i < niz.Length; i++)
                {
                    niz[i].ListaParametara = OpOpremaGlavniProzorBase.VratiParametreZaOpremuGlavniProzor(niz[i].id_oprema, entities);
                    niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
                    DbItemKolekcijaOpreme kolekcijaOpreme = (niz[i] as DbItemKolekcijaOpreme);
                    if (kolekcijaOpreme != null)
                    {
                        kolekcijaOpreme.KolekcijaOpreme = OpKolekcijaOpremeBase.kolekcijaOpremeZaIdKolekcije(niz[i].id_oprema, svaOpremaIzKolekcijeOpreme, entities);
                    }
                }



                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }

        }
    }

    public class OpOpremaSaParametrimaAdminPanel : OpOpremaBase
    {
        protected List<DbItemParametri> VratiSveParametreZaTipOpremeIPopuniOnoStoIma(int idOpreme, int idTipOpreme, DataSloj.SmartSoftwareBazaEntities entities)
        {
            List<DbItemParametri> lista = VratiParametreZaOpremu(idOpreme, entities);


            List<DbItemParametri> listaSvihParametara =
                (
                    from parametar in entities.parametris
                    where parametar.id_tip_opreme == idTipOpreme
                    select new DbItemParametri()
                    {
                        default_vrednost = parametar.default_vrednost,
                        id_parametri = parametar.id_parametri,
                        id_tip_opreme = parametar.id_tip_opreme,
                        naziv_parametra = parametar.naziv_parametra,
                        tipParametra = parametar.tipParametra,
                        deletedField = parametar.deletedField
                    }
                 ).ToList();

            foreach (var parametar in listaSvihParametara)
            {
                foreach (var parOprema in lista)
	            {	 
                    if(parametar.id_parametri == parOprema.id_parametri)
                    {
                        parametar.vrednost_parametra = parOprema.vrednost_parametra;
                    }
	            }
            }

            return listaSvihParametara;
        }

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
                DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   //where oprema.deletedField == false
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika,
                       DeletedField = oprema.deletedField
                   }
                    /*
                     * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                     * 
                     * treba da se za svaki posebno laptop pozove metod koji vraca
                     * njegove parametre (kao kolerisan upit) valjda
                     */
               ).ToArray();
                for (int i = 0; i < niz.Length; i++)
                {
                    niz[i].ListaParametara = this.VratiSveParametreZaTipOpremeIPopuniOnoStoIma(niz[i].id_oprema, niz[i].id_tip_opreme, entities);
                    niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            
        }
    }

    public class opOpremaSaParametrimaAdminPanelDelete : OpOpremaSaParametrimaAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
           if(this.DataSelectOprema != null)
           {
               entities.OpremaDelete(this.DataSelectOprema.id_oprema);
           }
           OperationObject opObj = base.execute(entities);
           return opObj;
        }
    }

    public class opOpremaSaParametrimaAdminPanelRestore : OpOpremaSaParametrimaAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOprema != null)
            {
                entities.RestoreIzbrisanaOprema(this.DataSelectOprema.id_oprema);
            }
            OperationObject opObj = base.execute(entities);
            return opObj;
        }
    }

    public class OpOpremaSaParametrimaAdminPanelUpdate : OpOpremaSaParametrimaAdminPanel
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOprema != null)
            {
                entities.OpremaUpdate
                (
                    this.DataSelectOprema.proizvodjac,
                    this.DataSelectOprema.naslov,
                    this.DataSelectOprema.cena,
                    this.DataSelectOprema.opis,
                    this.DataSelectOprema.model,
                    this.DataSelectOprema.lager,
                    this.DataSelectOprema.kolicina_u_rezervi,
                    this.DataSelectOprema.kolicina_na_lageru,
                    this.DataSelectOprema.oprema_na_popustu,
                    this.DataSelectOprema.slika,
                    this.DataSelectOprema.id_oprema
                );
                foreach (var item in this.DataSelectOprema.ListaParametara)
                {
                    entities.UpdateVrednostParametra(item.vrednost_parametra, item.id_parametri, this.DataSelectOprema.id_oprema);
                }
            }
            OperationObject opObj = base.execute(entities);
            return opObj;
        }
    }

    public class OpOpremaSaParametrimaAdminPanelInsert : OpOpremaSaParametrimaAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {

            if (this.DataSelectOprema != null)
            {
                System.Data.Objects.ObjectParameter idRezervacijePar = new System.Data.Objects.ObjectParameter("idOpreme", System.Type.GetType("System.Int32"));


                entities.OpremaInsert
                (
                    idRezervacijePar,
                    this.DataSelectOprema.proizvodjac,
                    this.DataSelectOprema.naslov,
                    this.DataSelectOprema.cena,
                    this.DataSelectOprema.opis,
                    this.DataSelectOprema.model,
                    this.DataSelectOprema.lager,
                    this.DataSelectOprema.kolicina_u_rezervi,
                    this.DataSelectOprema.kolicina_na_lageru,
                    this.DataSelectOprema.oprema_na_popustu,
                    this.DataSelectOprema.slika,
                    this.DataSelectOprema.id_tip_opreme
                );

                int id = Convert.ToInt32(idRezervacijePar.Value);
                this.DataSelectOprema.id_oprema = id;

                foreach (var item in this.DataSelectOprema.ListaParametara)
                {
                    entities.UpdateVrednostParametra(item.vrednost_parametra, item.id_parametri, this.DataSelectOprema.id_oprema);
                }
            }
            OperationObject opObj = base.execute(entities);
            return opObj;
        }
    }


    public class OpOpremaUpdate : OpOpremaBase 
    {

        
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelectOprema != null)
            {
                entities.OpremaUpdate
                (
                    this.DataSelectOprema.proizvodjac,       
                    this.DataSelectOprema.naslov,            
                    this.DataSelectOprema.cena,            
                    this.DataSelectOprema.opis,              
                    this.DataSelectOprema.model,             
                    this.DataSelectOprema.lager,            
                    this.DataSelectOprema.kolicina_u_rezervi,
                    this.DataSelectOprema.kolicina_na_lageru,
                    this.DataSelectOprema.oprema_na_popustu,
                    this.DataSelectOprema.slika,             
                    this.DataSelectOprema.id_oprema 
                );
                foreach (var item in this.DataSelectOprema.ListaParametara)
                {
                    entities.UpdateVrednostParametra(item.vrednost_parametra, item.id_parametri, this.DataSelectOprema.id_oprema);
                }
            }
            OperationObject opObj = base.execute(entities);
            return opObj;
        }
    }

    public class OpOpremaSkiniSaLagera : OpOpremaBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOprema != null)
            {
                entities.OpremaSkiniSaLagera(this.DataSelectOprema.id_oprema);
            }
            return base.execute(entities);
        }
    }
    
    public class OpOpremaPretraga : OpOpremaBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelectOprema == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   where oprema.naslov.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.model.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.opis.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.proizvodjac.Contains(DataSelectOprema.zaPretragu)
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika,
                       DeletedField = oprema.deletedField,
                   }
                    /*
                     * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                     * 
                     * treba da se za svaki posebno laptop pozove metod koji vraca
                     * njegove parametre (kao kolerisan upit) valjda
                     */
               ).ToArray();
                for (int i = 0; i < niz.Length; i++)
                {
                    niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                    niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }
        }
    }

    public class OpOpremaPretragaAdminPanel : OpOpremaSaParametrimaAdminPanel
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
           (
               from oprema in entities.opremas
               where oprema.naslov.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.model.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.opis.Contains(DataSelectOprema.zaPretragu) ||
                   oprema.proizvodjac.Contains(DataSelectOprema.zaPretragu)
               select new DbItemOpremaSaParametrima()
               {
                   cena = oprema.cena,
                   id_oprema = oprema.id_oprema,
                   id_tip_opreme = oprema.id_tip_opreme,
                   kolicina_na_lageru = oprema.kolicina_na_lageru,
                   kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                   lager = oprema.lager,
                   model = oprema.model,
                   naslov = oprema.naslov,
                   opis = oprema.opis,
                   oprema_na_popustu = oprema.oprema_na_popustu,
                   proizvodjac = oprema.proizvodjac,
                   slikaOriginalPutanja = oprema.slika,
                   slika = oprema.slika,
                   DeletedField = oprema.deletedField
               }
                /*
                 * http://stackoverflow.com/questions/9970100/calling-a-method-inside-a-linq-query
                 * 
                 * treba da se za svaki posebno laptop pozove metod koji vraca
                 * njegove parametre (kao kolerisan upit) valjda
                 */
           ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = this.VratiSveParametreZaTipOpremeIPopuniOnoStoIma(niz[i].id_oprema, niz[i].id_tip_opreme, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;

        }
    }


    public class OpOpremaSelectFilteri : OpOpremaGlavniProzorSelect
    {
        public static DbItemOpremaSaParametrima vratiOpremuZaIdOpreme(int idOpreme, SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   where oprema.id_oprema == idOpreme && oprema.deletedField == false
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika
                   }
               ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            if (niz.Length > 0)
                return niz[0];
            else
                return null;
        }

        protected int[] VratiIdjeveZaFiltere(int idTipOpreme, int[] nizIdijeva,DbItemParametri filter,SmartSoftwareBazaEntities entities)
        {
            List<int> l = null;
            if(nizIdijeva == null)
            {
                l =
                    (
                from oprema in entities.opremas
                where oprema.id_tip_opreme == idTipOpreme 
                select oprema.id_oprema
                ).ToList();
            }
            else
            {
               l =      (
                  from   opreema in entities.opremas
                  join opremaParametar in entities.parametarOpremas
                  on opreema.id_oprema equals opremaParametar.id_oprema
                  join parametar in entities.parametris
                  on opremaParametar.id_parametri equals parametar.id_parametri
                  where nizIdijeva.Contains(opreema.id_oprema) && 
                  ( filter.naziv_parametra == parametar.naziv_parametra && filter.ListaVrednostiZaFiltere.Contains(opremaParametar.vrednost_parametra))
                  select opreema.id_oprema
                    ).ToList();
            }


            return l.ToArray();
            
        }


        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            int n = this.DataSelectOprema.ListaParametara.Count;

            int [] niz = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, null, null, entities);

            //int[] niz1 = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, 0, niz, this.DataSelectOprema.ListaParametara[0], entities);
            int[] nizTmp = niz;
            for (int i = 0; i < n; i++)
            {

                nizTmp = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, nizTmp, this.DataSelectOprema.ListaParametara[i], entities);
            }

            List<DbItemOpremaSaParametrima> l = new List<DbItemOpremaSaParametrima>();

            foreach (var item in nizTmp)
            {
                this.DataSelectOprema.id_oprema = item;
                var result = vratiOpremuZaIdOpreme(item, entities);
                if(result != null)
                l.Add(result);
            }
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            opObj.Niz = l.ToArray();
            return opObj;
        }
    }

    public class OpOpremaSelectFilteriAdminPanel : OpOpremaSaParametrimaAdminPanel
    {
        public static DbItemOpremaSaParametrima vratiOpremuZaIdOpreme(int idOpreme, SmartSoftwareBazaEntities entities)
        {
            DbItemOpremaSaParametrima[] niz =
               (
                   from oprema in entities.opremas
                   where oprema.id_oprema == idOpreme
                   select new DbItemOpremaSaParametrima()
                   {
                       cena = oprema.cena,
                       id_oprema = oprema.id_oprema,
                       id_tip_opreme = oprema.id_tip_opreme,
                       kolicina_na_lageru = oprema.kolicina_na_lageru,
                       kolicina_u_rezervi = oprema.kolicina_u_rezervi,
                       lager = oprema.lager,
                       model = oprema.model,
                       naslov = oprema.naslov,
                       opis = oprema.opis,
                       oprema_na_popustu = oprema.oprema_na_popustu,
                       proizvodjac = oprema.proizvodjac,
                       slikaOriginalPutanja = oprema.slika,
                       slika = oprema.slika,
                       DeletedField = oprema.deletedField
                   }
               ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }
            return niz[0];
        }

        protected int[] VratiIdjeveZaFiltere(int idTipOpreme, int[] nizIdijeva, DbItemParametri filter, SmartSoftwareBazaEntities entities)
        {
            List<int> l = null;
            if (nizIdijeva == null)
            {
                l =
                    (
                from oprema in entities.opremas
                where oprema.id_tip_opreme == idTipOpreme
                select oprema.id_oprema
                ).ToList();
            }
            else
            {
                l = (
                   from opreema in entities.opremas
                   join opremaParametar in entities.parametarOpremas
                   on opreema.id_oprema equals opremaParametar.id_oprema
                   join parametar in entities.parametris
                   on opremaParametar.id_parametri equals parametar.id_parametri
                   where nizIdijeva.Contains(opreema.id_oprema) &&
                   (filter.naziv_parametra == parametar.naziv_parametra && filter.ListaVrednostiZaFiltere.Contains(opremaParametar.vrednost_parametra))
                   select opreema.id_oprema
                     ).ToList();
            }


            return l.ToArray();

        }


        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            int n = this.DataSelectOprema.ListaParametara.Count;

            int[] niz = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, null, null, entities);

            //int[] niz1 = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, 0, niz, this.DataSelectOprema.ListaParametara[0], entities);
            int[] nizTmp = niz;
            for (int i = 0; i < n; i++)
            {

                nizTmp = this.VratiIdjeveZaFiltere(this.DataSelectOprema.id_tip_opreme, nizTmp, this.DataSelectOprema.ListaParametara[i], entities);
            }

            List<DbItemOpremaSaParametrima> l = new List<DbItemOpremaSaParametrima>();

            foreach (var item in nizTmp)
            {
                this.DataSelectOprema.id_oprema = item;
                l.Add(vratiOpremuZaIdOpreme(item, entities));
            }
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            opObj.Niz = l.ToArray();
            return opObj;
        }
    }


    public class OpKolekcijaOpremeBase : OpOpremaBase
    {
        public DbItemKolekcijaOpreme DataSelectKolekcijaOpreme { get; set; }
        public static DbItemOpremaSaParametrima[] kolekcijaOpremeZaIdKolekcije(int idKolekcijeOpreme, kolekcija_opreme[] svaOpremaIzKolekcijeOpreme, SmartSoftwareBazaEntities entities)
        {
            int [] nizIdjeva =
                (from kolekcijaOpreme in svaOpremaIzKolekcijeOpreme
                 where kolekcijaOpreme.id_opreme == idKolekcijeOpreme
                 select kolekcijaOpreme.id_deo
                 ).ToArray();

            List<DbItemOpremaSaParametrima> opremeZaKolekciju = new List<DbItemOpremaSaParametrima>();
            foreach (var item in nizIdjeva)
            {
                var result = OpOpremaSelectFilteri.vratiOpremuZaIdOpreme(item, entities);
                if(result != null)
                {
                    opremeZaKolekciju.Add(result);
                    int index = opremeZaKolekciju.Count - 1;
                    opremeZaKolekciju[index].ListaParametara = VratiParametreZaOpremu(opremeZaKolekciju[index].id_oprema, entities);
                }
            }

            return opremeZaKolekciju.ToArray();
        }

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            var query = from p in entities.kolekcija_opreme
                        group p by p.id_opreme into g
                        select new
                        {
                            IdOpreme = g.Key
                        };
            
            DbItemKolekcijaOpreme[] niz =
            (
                from kolekcijaOpreme in query
                join glavnaOprema in entities.opremas
                on kolekcijaOpreme.IdOpreme equals glavnaOprema.id_oprema
                select new DbItemKolekcijaOpreme()
                {
                    cena = glavnaOprema.cena,
                    id_oprema = glavnaOprema.id_oprema,
                    id_tip_opreme = glavnaOprema.id_tip_opreme,
                    kolicina_na_lageru = glavnaOprema.kolicina_na_lageru,
                    kolicina_u_rezervi = glavnaOprema.kolicina_u_rezervi,
                    lager = glavnaOprema.lager,
                    model = glavnaOprema.model,
                    naslov = glavnaOprema.naslov,
                    opis = glavnaOprema.opis,
                    oprema_na_popustu = glavnaOprema.oprema_na_popustu,
                    proizvodjac = glavnaOprema.proizvodjac,
                    slikaOriginalPutanja = glavnaOprema.slika,
                    slika = glavnaOprema.slika
                }
            ).ToArray();

            kolekcija_opreme [] svaOpremaIzKolekcijeOpreme =
                (from kolekcijaOpreme in entities.kolekcija_opreme
                 select kolekcijaOpreme).ToArray();


            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                niz[i].KolekcijaOpreme = kolekcijaOpremeZaIdKolekcije(niz[i].id_oprema, svaOpremaIzKolekcijeOpreme, entities);
                niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
            }

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpKolekcijaOpremeSelect : OpKolekcijaOpremeBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelectKolekcijaOpreme != null)
            {
         
                var query = from p in entities.kolekcija_opreme
                            group p by p.id_opreme into g
                            select new
                            {
                                IdOpreme = g.Key
                            };

                DbItemKolekcijaOpreme[] niz =
                (
                    from kolekcijaOpreme in query
                    join glavnaOprema in entities.opremas
                    on kolekcijaOpreme.IdOpreme equals glavnaOprema.id_oprema
                    select new DbItemKolekcijaOpreme()
                    {
                        cena = glavnaOprema.cena,
                        id_oprema = glavnaOprema.id_oprema,
                        id_tip_opreme = glavnaOprema.id_tip_opreme,
                        kolicina_na_lageru = glavnaOprema.kolicina_na_lageru,
                        kolicina_u_rezervi = glavnaOprema.kolicina_u_rezervi,
                        lager = glavnaOprema.lager,
                        model = glavnaOprema.model,
                        naslov = glavnaOprema.naslov,
                        opis = glavnaOprema.opis,
                        oprema_na_popustu = glavnaOprema.oprema_na_popustu,
                        proizvodjac = glavnaOprema.proizvodjac,
                        slikaOriginalPutanja = glavnaOprema.slika,
                        slika = glavnaOprema.slika
                    }
                ).ToArray();



                kolekcija_opreme[] svaOpremaIzKolekcijeOpreme =
                    (from kolekcijaOpreme in entities.kolekcija_opreme
                     select kolekcijaOpreme).ToArray();



                for (int i = 0; i < niz.Length; i++)
                {
                    niz[i].ListaParametara = OpOpremaBase.VratiParametreZaOpremu(niz[i].id_oprema, entities);
                    niz[i].KolekcijaOpreme = kolekcijaOpremeZaIdKolekcije(niz[i].id_oprema, svaOpremaIzKolekcijeOpreme, entities);
                    niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();
                }

                OperationObject opObj = new OperationObject();
                opObj.Niz = niz;
                opObj.Success = true;
                return opObj;
            }
            else
            {
                return base.execute(entities);
            }

        }
    }

    public class OpKolekcijaOpremeInsert : OpKolekcijaOpremeBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelectKolekcijaOpreme != null)
            {
                OpOpremaSaParametrimaAdminPanelInsert op = new OpOpremaSaParametrimaAdminPanelInsert();
                op.DataSelectOprema = this.DataSelectKolekcijaOpreme;
                OperationObject rez = OperationManager.Singleton.executeOp(op);
                
                this.DataSelectKolekcijaOpreme.id_oprema = op.DataSelectOprema.id_oprema;

                foreach (var item in this.DataSelectKolekcijaOpreme.kolekcijaOpremeIdjevi)
                {
                    entities.KolekcijaOpremeInsert(this.DataSelectKolekcijaOpreme.id_oprema, item);
                }
                return base.execute(entities);
            }
            else
            {
                return base.execute(entities);
            }
        }
    }

    //public class OpKonfiguracijaKolekcijeSelectWhereId : OpOpremaBase
    //{
    //    public DbItemKolekcijaOpreme DataSelectKonfiguracijaKolekcije { get; set; }
    //    public override OperationObject execute(SmartSoftwareBazaEntities entities)
    //    {
    //        DbItemTipoviZaKonfiguraciju[] niz = 
    //            (from konf in entities.konfiguracija_kolekcije
    //                 where konf.Id_tip_opreme_kolekcije == this.DataSelectKonfiguracijaKolekcije.id_tip_opreme
    //                     select 
    //                 ).ToArray();
    //    }
    //}

    
    [DataContract]
    public class DbItemKolekcijaOpreme : DbItemOpremaSaParametrima
    {
        [DataMember]
        public DbItemOpremaSaParametrima[] KolekcijaOpreme { get; set; }
        [DataMember]
        public int[] kolekcijaOpremeIdjevi { get; set; }
    }

    [DataContract]
    public class DbItemParametri
    {
        [DataMember]
        public int id_parametri { get; set; }
        [DataMember]
        public string naziv_parametra { get; set; }
        [DataMember]
        public string default_vrednost { get; set; }
        [DataMember]
        public int id_tip_opreme { get; set; }
        [DataMember]
        public string vrednost_parametra { get; set; }
        [DataMember]
        public bool za_filter { get; set; }
        [DataMember]
        public string tipParametra { get; set; }
        [DataMember]
        public bool deletedField{ get; set; }
        [DataMember]
        public List<String> ListaVrednostiZaFiltere { get; set; }

        public string zaPretragu { get; set; }
    }

    [DataContract]
    public class DbItemOpremaSaParametrima
    {

        public DbItemOpremaSaParametrima()
        {

        }

        

        [DataMember]
        public int id_oprema { get; set; }
        [DataMember]
        public string proizvodjac { get; set; }
        [DataMember]
        public string naslov { get; set; }
        [DataMember]
        public double cena { get; set; }
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public int id_tip_opreme { get; set; }
        [DataMember]
        public bool?  lager { get; set; }
        [DataMember]
        public int? kolicina_u_rezervi { get; set; }
        [DataMember]
        public int? kolicina_na_lageru { get; set; }
        [DataMember]
        public int? oprema_na_popustu { get; set; }
        [DataMember]
        public string slika { get; set; }
        [DataMember]
        public string slikaOriginalPutanja { get; set; }
        [DataMember]
        public int kolicinaUKorpi { get; set; }
        [DataMember]
        public int kolicinaURezervacijama { get; set; }
        [DataMember]
        public bool DeletedField{ get; set; }
        [DataMember]
        public string zaPretragu { get; set; }
        [DataMember]
        public int kolicina_rezervisane_opreme { get; set; }
        [DataMember]
        public List<DbItemParametri> ListaParametara { get; set; }
        [DataMember]
        public int idProdavca { get; set; }
        
        
    }

    [DataContract]
    public class DbItemOpremaSaParametrimaIProdavac
    {

        public DbItemOpremaSaParametrimaIProdavac()
        {

        }



        [DataMember]
        public int id_oprema { get; set; }
        [DataMember]
        public string proizvodjac { get; set; }
        [DataMember]
        public string naslov { get; set; }
        [DataMember]
        public double cena { get; set; }
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public int id_tip_opreme { get; set; }
        [DataMember]
        public bool? lager { get; set; }
        [DataMember]
        public int? kolicina_u_rezervi { get; set; }
        [DataMember]
        public int? kolicina_na_lageru { get; set; }
        [DataMember]
        public int? oprema_na_popustu { get; set; }
        [DataMember]
        public string slika { get; set; }
        [DataMember]
        public string slikaOriginalPutanja { get; set; }
        [DataMember]
        public int kolicinaUKorpi { get; set; }
        [DataMember]
        public int kolicinaURezervacijama { get; set; }
        [DataMember]
        public bool DeletedField { get; set; }
        [DataMember]
        public string zaPretragu { get; set; }
        [DataMember]
        public int kolicina_rezervisane_opreme { get; set; }
        [DataMember]
        public List<DbItemParametri> ListaParametara { get; set; }
        [DataMember]
        public int idProdavca { get; set; }
    }


    [DataContract]
    public class DbItemOprema
    {

       



        [DataMember]
        public int id_oprema { get; set; }
        [DataMember]
        public string proizvodjac { get; set; }
        [DataMember]
        public string naslov { get; set; }
        [DataMember]
        public double cena { get; set; }
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public int id_tip_opreme { get; set; }
        [DataMember]
        public bool? lager { get; set; }
        [DataMember]
        public int? kolicina_u_rezervi { get; set; }
        [DataMember]
        public int? kolicina_na_lageru { get; set; }
        [DataMember]
        public int? oprema_na_popustu { get; set; }
        [DataMember]
        public string slika { get; set; }
        [DataMember]
        public string slikaOriginalPutanja { get; set; }
        [DataMember]
        public int kolicinaUKorpi { get; set; }
        [DataMember]
        public int kolicinaURezervacijama { get; set; }
        [DataMember]
        public bool DeletedField { get; set; }
        [DataMember]
        public string zaPretragu { get; set; }
        [DataMember]
        public int kolicina_rezervisane_opreme { get; set; }


    }
}