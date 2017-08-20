using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    [DataContract]
    public class DbItemKorisnici
    {
        [DataMember]
        public int id_korisnici { get; set; }
        [DataMember]
        public string ime { get; set; }
        [DataMember]
        public string prezime { get; set; }
        [DataMember]
        public string mejl { get; set; }
        [DataMember]
        public string broj_telefona { get; set; }
        [DataMember]
        public int? brojOstvarenihPoena { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string lozinka { get; set; }
        [DataMember]
        public int id_uloge { get; set; }
        [DataMember]
        public string naziv_uloge { get; set; }
        [DataMember]
        public string zaPretragu { get; set; }
        [DataMember]
        public bool deletedField { get; set; }
        [DataMember]
        public bool? polKorisnika { get; set; }
        [DataMember]
        public string slikaKorisnika { get; set; }
        [DataMember]
        public DateTime? datumKreiranja { get; set; }
        [DataMember]
        public DateTime? datumAzuriranja { get; set; }
        [DataMember]
        public int? brojKupovina { get; set; }


        public override string ToString()
        {
            return username + "," + id_uloge;
        }
    }
    

    public class DbItemKupci : DbItemKorisnici
    {
        

        public double? ukupnoPotrosio { get; set; }
        
        public DbItemIstorijaKupovine[] ListaKupovina { get; set; }
    }

}