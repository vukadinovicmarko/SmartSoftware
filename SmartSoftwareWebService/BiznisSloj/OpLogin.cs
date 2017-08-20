using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSoftwareWebService.BiznisSloj
{
    public class OpLogin:Operation
    {
        public DbItemKorisnici DataSelectKorisnici { get; set; }

        public override OperationObject execute(DataSloj.SmartSoftwareBazaEntities entities)
        {
            DbItemKorisnici[] korisniciNiz =
               (
               from korisnik in entities.korisnicis
               where korisnik.username == DataSelectKorisnici.username && korisnik.lozinka == DataSelectKorisnici.lozinka
               select new DbItemKorisnici()
               {
                  id_korisnici = korisnik.id_korisnici,
                  id_uloge = korisnik.id_uloge,
                  username = korisnik.username
               }).ToArray();
            OperationObject opObj = new OperationObject();
            opObj.Niz = korisniciNiz;
            opObj.Success = true;
            return opObj;
        }
    }
}