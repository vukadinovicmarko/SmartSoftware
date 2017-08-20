using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpKorpaSelect : OpOpremaBase
    {
       public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {

            DbItemOpremaSaParametrima[] niz =
                (
                    from oprema in entities.opremas
                    join korpa in entities.Korpas
                    on oprema.id_oprema equals korpa.idOprema
                    where korpa.idProdavca == DataSelectOprema.idProdavca
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
                        kolicinaUKorpi = korpa.kolicina,
                        DeletedField = oprema.deletedField,
                    }

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

    public class OpKorpaInsert : OpKorpaSelect
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            entities.DodajUKorpu(DataSelectOprema.id_oprema, DataSelectOprema.kolicinaUKorpi, DataSelectOprema.idProdavca);
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            return opObj;
        }
    }


    public class OpKorpaUpdate : OpKorpaSelect
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            entities.AzurirajKorpu(DataSelectOprema.id_oprema, DataSelectOprema.kolicinaUKorpi, DataSelectOprema.idProdavca);
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            return opObj;
        }
    }


    public class OpKorpaDelete : OpKorpaSelect
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            OperationObject rez = null;
            if(this.DataSelectOprema.id_oprema > 0)
            {
                entities.ObrisiOpremuIzKorpe(this.DataSelectOprema.id_oprema, DataSelectOprema.idProdavca);
                rez = base.execute(entities);
            }
            else
            {
                entities.ObrisiCeluKorpu(DataSelectOprema.idProdavca);
                rez = new OperationObject() { Success = true };
            }
            return rez;
        }
    }

    public class OpRezervacijaUKorpu : OpRezervacijeBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            OperationObject rez = null;
            if(this.DataSelectDbItemRezervacijaSelect != null)
            {
                DbItemOpremaSaParametrimaIProdavac[] niz =
                    (from opremaIzRezervacija in entities.RezervacijeOpremas
                     where opremaIzRezervacija.id_rezervacije == this.DataSelectDbItemRezervacijaSelect.id_rezervacije
                     select new DbItemOpremaSaParametrimaIProdavac()
                     {
                         id_oprema = opremaIzRezervacija.id_oprema,
                         kolicina_rezervisane_opreme = opremaIzRezervacija.kolicina_rezervisane_opreme,
                         idProdavca = DataSelectDbItemRezervacijaSelect.id_Prodavca
                     }).ToArray();

                foreach (var item in niz)
                {
                    entities.DodajUKorpuIzRezervacije(item.id_oprema, item.kolicina_rezervisane_opreme, item.idProdavca);
                }

                entities.IzbrisiRezervacijuZaStavljanjeUkorpu(this.DataSelectDbItemRezervacijaSelect.id_rezervacije);
            }
            return base.execute(entities);
        }
    }
}