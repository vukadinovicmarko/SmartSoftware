using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    
    public abstract class OpOblastiOpremeBase:Operation
    {
        public DbItemOblastiOpreme DataSelectOblastiOpreme { get; set; }
       

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOblastiOpreme[] oblastiOpremeNiz =
                (
                    from oblasti in entities.oblasti_opreme
                    where oblasti.deletedField == false
                    select new DbItemOblastiOpreme()
                    {
                        id_oblasti_opreme = oblasti.id_oblasti_opreme,
                        naziv_oblasti_opreme = oblasti.naziv_oblasti_opreme,
                        picture = oblasti.picture,
                        SlikaOriginalPutanja = oblasti.picture,
                        DeletedField = oblasti.deletedField
                    }
                ).ToArray();
            for (int i = 0; i < oblastiOpremeNiz.Length; i++)
            {
                oblastiOpremeNiz[i].picture = HttpContext.Current.Server.MapPath("." + oblastiOpremeNiz[i].picture).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = oblastiOpremeNiz;
            opObj.Success = true;
            return opObj;
        }
    }

    

    public class OpOblastiOpremeSelect : OpOblastiOpremeBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOblastiOpreme == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemTipOpreme[] tipoviOpremeNiz =
                  (
                      from tipovi in entities.tip_opreme
                      join oblasti in entities.oblasti_opreme
                      on tipovi.id_oblasti_opreme equals oblasti.id_oblasti_opreme
                      where tipovi.id_oblasti_opreme == DataSelectOblastiOpreme.id_oblasti_opreme
                      select new DbItemTipOpreme()
                      {
                          id_oblasti_opreme = tipovi.id_oblasti_opreme,
                          id_tip_opreme = tipovi.id_tip_opreme,
                          naziv_tipa = tipovi.naziv_tipa
                      }
                  ).ToArray();

                OperationObject opObj = new OperationObject();
                opObj.Niz = tipoviOpremeNiz;
                opObj.Success = true;
                return opObj;
            }
        }        
    }

    public abstract class OpOblastiOpremeAdminPanelBase : Operation
    {
        public DbItemOblastiOpreme DataSelectOblastiOpreme { get; set; }


        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOblastiOpreme[] oblastiOpremeNiz =
                (
                    from oblasti in entities.oblasti_opreme
                    select new DbItemOblastiOpreme()
                    {
                        id_oblasti_opreme = oblasti.id_oblasti_opreme,
                        naziv_oblasti_opreme = oblasti.naziv_oblasti_opreme,
                        picture = oblasti.picture,
                        SlikaOriginalPutanja = oblasti.picture,
                        DeletedField = oblasti.deletedField
                    }
                ).ToArray();
            for (int i = 0; i < oblastiOpremeNiz.Length; i++)
            {
                oblastiOpremeNiz[i].picture = HttpContext.Current.Server.MapPath("." + oblastiOpremeNiz[i].picture).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = oblastiOpremeNiz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpOblastiOpremeAdminPanelSelect : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOblastiOpreme == null)
            {
                return base.execute(entities);
            }
            else
            {
                DbItemTipOpreme[] tipoviOpremeNiz =
                  (
                      from tipovi in entities.tip_opreme
                      join oblasti in entities.oblasti_opreme
                      on tipovi.id_oblasti_opreme equals oblasti.id_oblasti_opreme
                      where tipovi.id_oblasti_opreme == DataSelectOblastiOpreme.id_oblasti_opreme
                      select new DbItemTipOpreme()
                      {
                          id_oblasti_opreme = tipovi.id_oblasti_opreme,
                          id_tip_opreme = tipovi.id_tip_opreme,
                          naziv_tipa = tipovi.naziv_tipa
                      }
                  ).ToArray();

                OperationObject opObj = new OperationObject();
                opObj.Niz = tipoviOpremeNiz;
                opObj.Success = true;
                return opObj;
            }
        }
    }

    public class OpOblastiOpremeInsert : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.DataSelectOblastiOpreme != null)
            {
                entities.OblastiOpremeInsert(this.DataSelectOblastiOpreme.naziv_oblasti_opreme, this.DataSelectOblastiOpreme.picture);
            }
            return base.execute(entities);
        }
    }

    public class OpOblastiOpremeUpdate : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOblastiOpreme != null)
            {
                entities.OblastiOpremeUpdate(this.DataSelectOblastiOpreme.id_oblasti_opreme,this.DataSelectOblastiOpreme.naziv_oblasti_opreme, this.DataSelectOblastiOpreme.picture);
            }
            return base.execute(entities);
        }
    }

    public class OpOblastiOpremeDelete : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOblastiOpreme != null)
            {
                entities.ObrisiOblastOpreme(this.DataSelectOblastiOpreme.id_oblasti_opreme);
            }
            return base.execute(entities);
        }
    }

    public class OpOblastiOpremeRestore : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.DataSelectOblastiOpreme != null)
            {
                entities.RestoreIzbrisanaOblastOpreme(this.DataSelectOblastiOpreme.id_oblasti_opreme);
            }
            return base.execute(entities);
        }
    }

    public class OpOblastiOpremePretraga : OpOblastiOpremeAdminPanelBase
    {
        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemOblastiOpreme[] niz =
             (
                from oblastiOpreme in entities.oblasti_opreme
                where oblastiOpreme.naziv_oblasti_opreme.Contains(DataSelectOblastiOpreme.ZaPretragu)
                select new DbItemOblastiOpreme()
                {
                    id_oblasti_opreme = oblastiOpreme.id_oblasti_opreme,
                    naziv_oblasti_opreme = oblastiOpreme.naziv_oblasti_opreme,
                    picture = oblastiOpreme.picture,
                    SlikaOriginalPutanja = oblastiOpreme.picture
                }
              ).ToArray();
            for (int i = 0; i < niz.Length; i++)
            {
                niz[i].picture = HttpContext.Current.Server.MapPath("." + niz[i].picture).ToString();
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }


    [DataContract]
    public class DbItemOblastiOpreme
    {
        public DbItemOblastiOpreme()
        {

        }
        [DataMember]
        public int id_oblasti_opreme { get; set; }
        [DataMember]
        public string naziv_oblasti_opreme { get; set; }
        [DataMember]
        public string picture { get; set; }
        [DataMember]
        public string SlikaOriginalPutanja { get; set; }
        [DataMember]
        public string ZaPretragu { get; set; }
        [DataMember]
        public bool DeletedField { get; set; }
        
    }

    [DataContract]
    public class DbItemTipOpreme
    {
        [DataMember]
        public int id_tip_opreme { get; set; }
        [DataMember]
        public string naziv_tipa { get; set; }
        [DataMember]
        public int? id_oblasti_opreme { get; set; }
        [DataMember]
        public string slika_tipa { get; set; }
        [DataMember]
        public string SlikaOriginalPutanja { get; set; }
        [DataMember]
        public string ZaPretragu { get; set; }
        [DataMember]
        public string naziv_oblasti_opreme { get; set; }
        [DataMember]
        public bool DeletedField { get; set; }
    }

    [DataContract]
    public class DbItemTipoviZaKonfiguraciju : DbItemTipOpreme
    {
        [DataMember]
        public bool? izabranZaKonfiguraciju { get; set; }
        [DataMember]
        public int? idTipOpremeKolekcije { get; set; }
        [DataMember]
        public int? idTipOpremeDeoKolekcije { get; set; }
        [DataMember]
        public int? redosledPrikazivanja { get; set; }
        [DataMember]
        public int moguca_kolicina_unosa { get; set; }


        [DataMember]
        public DbItemOpremaSaParametrima[] KolekcijaOpreme { get; set; }
        [DataMember]
        public int[] kolekcijaOpremeIdjevi { get; set; }

    }

}