using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartSoftwareWebService.DataSloj;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpNarudzbineBase : Operation
    {
        public DbItemNarudzbine NaruzbineDataSelect { get; set; }

        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
        
            DbItemNarudzbine[] niz =
                (from narudzbina in entities.narudzbines
                 select new DbItemNarudzbine()
                 {
                     datum_narudzbine = narudzbina.datum_narudzbine,
                     kolicina = narudzbina.kolicina,
                     id_prodavca = narudzbina.id_prodavca,
                     id_oprema = narudzbina.id_oprema,
                     id_narudzbine = narudzbina.id_narudzbine
                 }).OrderByDescending(n => n.datum_narudzbine).ToArray();

            foreach (var item in niz)
            {
                item.prodavac = OpIstorijaKupovineBase.VratiProdavcaZaIdProdavca(item.id_prodavca,entities);
                item.narucenaOprema = OpOpremaSelectFilteriAdminPanel.vratiOpremuZaIdOpreme(item.id_oprema, entities);
            }
            OperationObject opObj = new OperationObject();
            opObj.Niz = niz;
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpNaruzbineSelect : OpNarudzbineBase
    {
       
    }

    public class OpNaruzbineInsert : OpNarudzbineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if(this.NaruzbineDataSelect != null)
            {
                entities.NarudzbineInsert(this.NaruzbineDataSelect.id_oprema, this.NaruzbineDataSelect.kolicina, this.NaruzbineDataSelect.id_prodavca, this.NaruzbineDataSelect.datum_narudzbine);
            }
            OperationObject opObj = new OperationObject();
            opObj.Success = true;
            return opObj;
        }
    }

    public class OpNaruzbinePrihvati : OpNarudzbineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.NaruzbineDataSelect != null)
            {
                entities.NarudzbinePrihvati(this.NaruzbineDataSelect.id_narudzbine, this.NaruzbineDataSelect.id_oprema, this.NaruzbineDataSelect.kolicina);
            }
            return base.execute(entities);
        }
    }

    public class OpNaruzbineDelete : OpNarudzbineBase
    {
        public override OperationObject execute(SmartSoftwareBazaEntities entities)
        {
            if (this.NaruzbineDataSelect != null)
            {
                entities.NarudzbineDelete(this.NaruzbineDataSelect.id_narudzbine);
            }
            return base.execute(entities);
        }
    }
    
    public class DbItemNarudzbine
    {
        public int id_narudzbine { get; set; }
        public int kolicina { get; set; }
        public int id_oprema { get; set; }
        public DbItemOpremaSaParametrima narucenaOprema { get; set; }
        public int id_prodavca { get; set; }
        public DbItemKorisnici prodavac { get; set; }
        public System.DateTime? datum_narudzbine { get; set; }
        
    }
}