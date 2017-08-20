using SmartSoftwareWebService.BiznisSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSoftwareWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface SmartSoftwareServiceInterface
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        

        [OperationContract]
        DbItemOblastiOpreme[] OblastiOpreme();

        [OperationContract]
        DbItemOblastiOpreme[] PretragaOblastiOpreme(string zaPretragu);

        [OperationContract]
        DbItemOblastiOpreme[] OblastiOpremeAdminPanel();

        [OperationContract]
        DbItemTipOpreme[] TipOpreme();

        [OperationContract]
        DbItemTipOpreme[] TipOpremeAdminPanel();

        [OperationContract]
        DbItemTipOpreme[] PretragaTipovaOpreme(string zaPretragu);

        [OperationContract]
        DbItemTipOpreme[] TipoviOpremeInsert(DbItemTipOpreme TipOpreme);

        [OperationContract]
        DbItemTipOpreme[] TipoviOpremeUpdate(DbItemTipOpreme TipOpreme);

        [OperationContract]
        DbItemTipOpreme[] TipoviOpremeDelete(DbItemTipOpreme TipOpreme);

        [OperationContract]
        DbItemTipOpreme[] TipoviOpremeRestore(DbItemTipOpreme TipOpreme);


        [OperationContract]
        DbItemOpremaSaParametrima[] OpremeSaParametrima(int? idTipOpreme);

        [OperationContract]
        DbItemOpremaSaParametrima[] OpremeSaParametrimaGlavniProzor(int? idTipOpreme);


        [OperationContract]
        DbItemOpremaSaParametrima[] UpdateOpreme(DbItemOpremaSaParametrima opremaZaUpdate);

        [OperationContract]
        DbItemOpremaSaParametrima[] UpdateOpremeSkiniOpremuSaLagera(int idOprema);


        [OperationContract]
        DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanel();

        [OperationContract]
        DbItemOpremaSaParametrima[] OpremaDelete(int idOpreme);

        [OperationContract]
        DbItemOpremaSaParametrima[] OpremaRestore(int idOpreme);
        
        [OperationContract]
        DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanelUpdate(DbItemOpremaSaParametrima opremaZaAzuriranje);

        [OperationContract]
        DbItemParametri[] ParametriZaIdTipaOpreme(int idTipOpreme);

        [OperationContract]
        DbItemParametri[] SelectAdminPanelParametri();

        [OperationContract]
        DbItemParametri[] ParametriPretraga(string zaPretragu);

        [OperationContract]
        DbItemParametri[] ParametriInsert(DbItemParametri p);

        [OperationContract]
        DbItemParametri[] ParametriUpdate(DbItemParametri p);

        [OperationContract]
        DbItemParametri[] ParametriDelete(int idParametra);

        [OperationContract]
        DbItemParametri[] ParametriRestore(int idParametra);

        [OperationContract]
        DbItemOpremaSaParametrimaStatistika[] IstorijaKupovineNajprodavanijaOprema(bool asc);

        [OperationContract]
        DbItemKupci[] IstorijaKupovineInicijalniPrikazKupaca();

        [OperationContract]
        DbItemKupci[] IstorijaKupovineNajcesciKupci(bool asc);

        [OperationContract]
        DbItemKupci[] IstorijaKupovineKupciKojiNajviseKupuju(bool asc);

        [OperationContract]
        DbItemKupci[] IstorijaKupovineKupciKojiNajviseTrose(bool asc);

        [OperationContract]
        int IstorijaKupovineUkupanBrojProdateOpreme();

        [OperationContract]
        int IstorijaKupovineUkupanBrojProdateOpremeZaDanas(DateTime danas, DateTime sutra);

        [OperationContract]
        double IstorijaKupovineUkupnoZaradjeno();

        [OperationContract]
        double IstorijaKupovineZaradjenoDanas(DateTime danas, DateTime sutra);
        
        [OperationContract]
        DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanelInsert(DbItemOpremaSaParametrima opremaZaInsert);
        

        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        PutanjaDoSlike UploadFile(RemoteFileInfo request);

        [OperationContract]
        DbItemParametri[] PrikaziFiltere(int idTipOpreme);

        [OperationContract]
        DbItemParametri[] PrikaziFiltereGlavniProzor(int idTipOpreme);

        [OperationContract]
        DbItemOpremaSaParametrima[] KorpaSelect(int idProdavca);

        [OperationContract]
        OperationObject KorpaInsert(int idOprema, int kolicina, int idProdavca);

        [OperationContract]
        OperationObject KorpaUpdate(int idOprema, int kolicina, int idProdavca);

        [OperationContract]
        DbItemOpremaSaParametrima[] KorpaDelete(int? idOprema, int idProdavca);

        [OperationContract]
        DbItemKorisnici[] PrikaziKorisnike(int? idUloge);

        [OperationContract]
        DbItemKorisnici[] PretragaKorisnika(string pretraga);

        [OperationContract]
        DbItemKorisnici[] OpKorisniciInsert(DbItemKorisnici korisnik);

        [OperationContract]
        DbItemKorisnici[] AzurirajBrojPoenaKorisnika(DbItemKorisnici korisnik);

        [OperationContract]
        DbItemKorisnici[] PrikaziZaposleneKorisnike(DbItemKorisnici korisnik);

        [OperationContract]
        DbItemKorisnici[] ZaposleniKorisniciInsert(DbItemKorisnici korisnik);

        [OperationContract]
        DbItemKorisnici[] ZaposleniKorisniciUpdate(DbItemKorisnici korisnik);

        [OperationContract]
        DbItemKorisnici[] KorisniciDelete(int idKorisnika);

        [OperationContract]
        DbItemKorisnici[] KorisniciRestore(int idKorisnika);

        [OperationContract]
        DbItemKupljenaOpremaSaParametrima[] ProdajaArtikla(DbItemIstorijaKupovine prodaja, DbItemKupljenaOpremaSaParametrima[] prodataOprema);

        [OperationContract]
        DbItemIstorijaKupovine[] IstorijaKupovineSelect();

        [OperationContract]
        DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaDatumSelect();

        [OperationContract]
        DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaDatumPretraga(string searchIme);

        [OperationContract]
        DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaImeSelect();

        [OperationContract]
        DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaBrojKupovinaSelect(bool asc);

        //[OperationContract]
        //DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaPotroseniNovacSelect();


        [OperationContract]
        DbItemRezervacijeSelect[] RezervacijeSelect();

        [OperationContract]
        DbItemRezervacijeSelect[] PrebaciRezerevacijuUKorpu(int idRezervacije, int idProdavca);

        [OperationContract]
        bool RezervacijeInsert(DbItemRezervacijeInsert rezervacija);

        [OperationContract]
        OperationObject RezervacijeUpdate(DbItemRezervacijeDeleteIUpdate[] rezervacija);

        //[OperationContract]
        //DbItemOpremaSaParametrimaRezervacija[] RezervacijeOpremaDelete(int? idRezervacije, int? idOprema);

        [OperationContract]
        DbItemRezervacijeSelect[] RezervacijeDelete(DbItemRezervacijeDeleteIUpdate rezervacija);

        [OperationContract]
        DbItemOpremaSaParametrima[] PretragaOpreme(string zaPretragu, bool zaAdminPanel);
        
        
        // TODO: Add your service operations here

        [OperationContract]
        DbItemOpremaSaParametrima[] PrikaziOpremuPoFilterima(int idTipOpreme, DbItemParametri[] filteri);

        [OperationContract]
        DbItemOblastiOpreme[] OblastiOpremeInsert(DbItemOblastiOpreme o);

        [OperationContract]
        DbItemOblastiOpreme[] OblastiOpremeUpdate(DbItemOblastiOpreme o);

        [OperationContract]
        DbItemOblastiOpreme[] OpOblastiOpremeDelete(DbItemOblastiOpreme o);

        [OperationContract]
        DbItemOblastiOpreme[] OpOblastiOpremeRestore(DbItemOblastiOpreme o);
        
        //Narudzbine
        
        [OperationContract]
        DbItemNarudzbine[] OpNarudzbineSelect();

        [OperationContract]
        DbItemNarudzbine[] OpNarudzbinePrihvatiNarudzbinu(DbItemNarudzbine narduzbina);

        [OperationContract]
        DbItemNarudzbine[] OpNarudzbineOdbijNarudzinu(DbItemNarudzbine narduzbina);

        [OperationContract]
        bool OpNarudzbineInsert(DbItemNarudzbine narduzbina);

        
        //kolekcija opreme
        [OperationContract]
        DbItemKolekcijaOpreme[] OpKolekcijaOpremeSelect();

        [OperationContract]
        DbItemKolekcijaOpreme[] OpKolekcijaOpremeInsert(DbItemKolekcijaOpreme oprema);

        [OperationContract]
        DbItemGrupeOgranicenja[] OgranicenjaSelect(int idTipaKolekcijeOgranicenja);

        [OperationContract]
        DbItemGrupeOgranicenja[] OgranicenjaInsert(DbItemGrupeOgranicenja ogranicenje);


        [OperationContract]
        DbItemTipoviZaKonfiguraciju[] TipoviZaNovuKonfiguraciju(int idTipOpreme);

        [OperationContract]
        DbItemTipoviZaKonfiguraciju[] TipoviZaNovuKonfiguracijuInsertUpdate(int idTipOpremeKolekcije, DbItemTipoviZaKonfiguraciju[] kolekcijaKonfiguracije);



        //ogranicenja

        [OperationContract]
        DbItemTipoviZaKonfiguraciju[] OpPrikazKolekcijeZaUnos(int idTipOpremeKolekcije);
    }





    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class PutanjaDoSlike
    {
        [MessageBodyMember]
        public string novaPutanja;
    }


    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageHeader(MustUnderstand = true)]
        public string prefixPutanja;


        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }

    }














    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }


}
