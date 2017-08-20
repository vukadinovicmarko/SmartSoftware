using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    //public class OpNesto : OpKolekcijaOpremeBase
    //{
    //    public override OperationObject execute(SmartSoftwareBazaEntities entities)
    //    {
    //        konfiguracija_kolekcije[] niz =
    //        (from konfiguracija in entities.konfiguracija_kolekcije
    //         where konfiguracija.Id_tip_opreme_kolekcije == this.DataSelectOprema.id_tip_opreme
    //         select konfiguracija).ToArray();

    //        grupe_ogranicenja[] nizOgranicenja =
    //            (
    //            from ogranicenje in entities.grupe_ogranicenja
    //            where ogranicenje.id_tip_opreme_kolekcije == this.DataSelectOprema.id_oprema
    //            select ogranicenje
    //            ).ToArray();

    //        if (nizOgranicenja.Length == 0)
    //        {
    //            onda vrati samo to mora da vidimo 
    //        }
    //        else
    //        {
    //            foreach (var item in nizOgranicenja)
    //            {
    //                if (item.tipProvere == "nekiTipProvereZaPrikaz")
    //                {

    //                }
    //            }
    //        }

    //    }
    //}

    public class OpPrikazOnihKojiSuChekiraniInicijalno : OpTipoviOpremeZaKonfiguraciju
    {
      

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            OpTipoviOpremeZaKonfiguracijuZaIdKonfigurcije op = new OpTipoviOpremeZaKonfiguracijuZaIdKonfigurcije();
            DbItemTipoviZaKonfiguraciju dataselect = (this.DataSelectTipoviOpreme as DbItemTipoviZaKonfiguraciju);
            op.DataSelectTipoviOpreme = dataselect;
            OperationObject rez = OperationManager.Singleton.executeOp(op);
            DbItemTipoviZaKonfiguraciju[] niz = rez.Niz as DbItemTipoviZaKonfiguraciju[];

            foreach (var item in niz)
            {
                OpOpremaSelect opOprema = new OpOpremaSelect();
                opOprema.DataSelectOprema = new DbItemOpremaSaParametrima()
                {
                     id_tip_opreme  = (int) dataselect.idTipOpremeKolekcije
                };

                OperationObject rezultat = OperationManager.Singleton.executeOp(opOprema);
                item.KolekcijaOpreme = rez.Niz as DbItemOpremaSaParametrima[];
            }

            rez.Success = true;
            rez.Niz = niz;
            return rez;
        }
    }

    //public class OpPrikazOnihKojiSuChekiraniSaProverama : OpPrikazOnihKojiSuChekiraniInicijalno
    //{
    //    public override OperationObject execute(SmartSoftwareBazaEntities entities)
    //    {
            //ovde nekako treba da se ispita cela lista 
            //sa svim clanovima i da se vraca za svaki tipOpreme odredjena oprema na osnovu 
            //ispunjenih uslova
    //    }
    //}
}