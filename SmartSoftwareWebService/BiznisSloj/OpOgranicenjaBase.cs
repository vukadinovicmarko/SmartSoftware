using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpOgranicenjaBase : Operation
    {
        public DbItemGrupeOgranicenja DataSelectOgranicenja { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            
            DbItemGrupeOgranicenja[] niz =
                (
                 from ogranicenje in entities.grupe_ogranicenja
                 join tipovi1 in entities.tip_opreme
                 on ogranicenje.id_tip_opreme1 equals tipovi1.id_tip_opreme
                 join tipovi2 in entities.tip_opreme
                 on ogranicenje.id_tip_opreme2 equals tipovi2.id_tip_opreme into joinovaniTipovi
                 from leftOuterJoinovaniTipovi in joinovaniTipovi.DefaultIfEmpty()
                 join parametri1 in entities.parametris
                 on ogranicenje.id_parametra1 equals parametri1.id_parametri
                 join parametri2 in entities.parametris
                 on ogranicenje.id_parametra2 equals parametri2.id_parametri  into joinovaniParametri
                 from leftOuterJoinovaniParametri in joinovaniParametri.DefaultIfEmpty()
                 where ogranicenje.id_tip_opreme_kolekcije == this.DataSelectOgranicenja.id_tip_opreme_kolekcije
                 select new DbItemGrupeOgranicenja()
                 {
                     Id_grupe_ogranicenja = ogranicenje.Id_grupe_ogranicenja,
                     id_parametra1 = ogranicenje.id_parametra1,
                     id_parametra2 = ogranicenje.id_parametra2,
                     id_tip_opreme_kolekcije = ogranicenje.id_tip_opreme_kolekcije,
                     id_tip_opreme1 = ogranicenje.id_tip_opreme1,
                     id_tip_opreme2 = ogranicenje.id_tip_opreme2,
                     tipProvere = ogranicenje.tipProvere,
                     nazivParametra1 = parametri1.naziv_parametra,
                     nazivParametra2 = leftOuterJoinovaniParametri.naziv_parametra,
                     nazivTipaOpreme1 = tipovi1.naziv_tipa,
                     nazivTipaOpreme2 = leftOuterJoinovaniTipovi.naziv_tipa
                 }).ToArray();

            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpOgranicenjeSelect : OpOgranicenjaBase
    {

    }

    public class OpOgranicenjaInsert : OpOgranicenjaBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            System.Data.Objects.ObjectParameter idOgranicenja = new System.Data.Objects.ObjectParameter("idOgranicenja", System.Type.GetType("System.Int32"));

            if (this.DataSelectOgranicenja != null)
            {
                entities.GrupeOgranicenjaInsert(DataSelectOgranicenja.id_tip_opreme1, DataSelectOgranicenja.id_tip_opreme2, DataSelectOgranicenja.id_parametra1, DataSelectOgranicenja.id_parametra2, DataSelectOgranicenja.id_tip_opreme_kolekcije, DataSelectOgranicenja.tipProvere);
            }
            return base.execute(entities);
        }
    }

    public class OpOgranicenjaUpdate : OpOgranicenjaBase
    {

    }

    public class OpOgrnicenjaDelete : OpOgranicenjaBase
    {
        
    }

    public class DbItemGrupeOgranicenja
    {
        public int Id_grupe_ogranicenja { get; set; }
        public int id_tip_opreme1 { get; set; }
        public int id_parametra1 { get; set; }
        public string nazivTipaOpreme1 { get; set; }
        public string nazivParametra1 { get; set; }
        public int? id_tip_opreme2 { get; set; }
        public int? id_parametra2 { get; set; }
        public string nazivTipaOpreme2 { get; set; }
        public string nazivParametra2 { get; set; }
        public string tipProvere { get; set; }
        public int id_tip_opreme_kolekcije { get; set; }
    }
}