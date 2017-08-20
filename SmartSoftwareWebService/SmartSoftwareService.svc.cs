using SmartSoftwareWebService.BiznisSloj;
using SmartSoftwareWebService.DataSloj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;


namespace SmartSoftwareWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SmartSoftwareService : SmartSoftwareServiceInterface
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string Login(string username,string password)
        {
            OpLogin op = new OpLogin();
            op.DataSelectKorisnici = new DbItemKorisnici()
            {
                username = username,
                lozinka = password
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici [] pera =  rezultat.Niz as DbItemKorisnici[];

            return pera.Length > 0 ? pera[0].ToString() : "UNESTIE user i pass";

        }

        
        public DbItemOblastiOpreme[] OblastiOpreme()
        {
            OpOblastiOpremeSelect op = new OpOblastiOpremeSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] oblastiOpremeNiz = rezultat.Niz as DbItemOblastiOpreme[];
            return oblastiOpremeNiz;
        }
        public DbItemOblastiOpreme[] OblastiOpremeAdminPanel()
        {
            OpOblastiOpremeAdminPanelSelect op = new OpOblastiOpremeAdminPanelSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] oblastiOpremeNiz = rezultat.Niz as DbItemOblastiOpreme[];
            return oblastiOpremeNiz;
        }

        public DbItemOblastiOpreme[] PretragaOblastiOpreme(string zaPretragu)
        {
            OpOblastiOpremePretraga op = new OpOblastiOpremePretraga();
            op.DataSelectOblastiOpreme = new DbItemOblastiOpreme()
            {
                ZaPretragu = zaPretragu
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] niz = rezultat.Niz as DbItemOblastiOpreme[];
            return niz;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public DbItemTipOpreme[] TipOpreme()
        {
            OpTipoviOpremeSelect op = new OpTipoviOpremeSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] tipoviNiz = rezultat.Niz as DbItemTipOpreme[];
            return tipoviNiz;
        }

        public DbItemTipOpreme[] TipOpremeAdminPanel()
        {
            OpTipoviOpremeAdminPanelSelect op = new OpTipoviOpremeAdminPanelSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] tipoviNiz = rezultat.Niz as DbItemTipOpreme[];
            return tipoviNiz;
        }

        public DbItemTipOpreme[] PretragaTipovaOpreme(string zaPretragu)
        {
            OpTipoviOpremePretraga op = new OpTipoviOpremePretraga();
            op.DataSelectTipoviOpreme = new DbItemTipOpreme()
            {
                ZaPretragu = zaPretragu
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] niz = rezultat.Niz as DbItemTipOpreme[];
            return niz;
        }

        public DbItemTipOpreme[] TipoviOpremeInsert(DbItemTipOpreme TipOpreme)
        {
            OpTipoviOpremeInsert op = new OpTipoviOpremeInsert();
            if (TipOpreme != null)
            {
                op.DataSelectTipoviOpreme = new DbItemTipOpreme()
                {
                    naziv_tipa = TipOpreme.naziv_tipa,
                    slika_tipa = TipOpreme.slika_tipa,
                    id_oblasti_opreme = TipOpreme.id_oblasti_opreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] niz = rezultat.Niz as DbItemTipOpreme[];
            return niz;
        }

        public DbItemTipOpreme[] TipoviOpremeUpdate(DbItemTipOpreme TipOpreme)
        {
            OpTipoviOpremeUpdate op = new OpTipoviOpremeUpdate();
            if (TipOpreme != null)
            {
                op.DataSelectTipoviOpreme = new DbItemTipOpreme()
                {
                    id_oblasti_opreme = TipOpreme.id_oblasti_opreme,
                    naziv_tipa = TipOpreme.naziv_tipa,
                    slika_tipa = TipOpreme.slika_tipa,
                    id_tip_opreme = TipOpreme.id_tip_opreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] niz = rezultat.Niz as DbItemTipOpreme[];
            return niz;
        }

        public DbItemTipOpreme[] TipoviOpremeDelete(DbItemTipOpreme TipOpreme)
        {
            OpTipoviOpremeDelete op = new OpTipoviOpremeDelete();
            if (TipOpreme != null)
            {
                op.DataSelectTipoviOpreme = new DbItemTipOpreme()
                {
                    id_tip_opreme = TipOpreme.id_tip_opreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] niz = rezultat.Niz as DbItemTipOpreme[];
            return niz;
        }


        public DbItemOpremaSaParametrima[] OpremeSaParametrima(int? idTipOpreme)
        {
            OpOpremaSelect op = new OpOpremaSelect();
            if (idTipOpreme != null && idTipOpreme != 0)
            {
                op.DataSelectOprema = new DbItemOpremaSaParametrima()
                {
                    id_tip_opreme = (int)idTipOpreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] opremaSaParametrima = rezultat.Niz as DbItemOpremaSaParametrima[];
            return opremaSaParametrima;
        }

        public DbItemOpremaSaParametrima[] OpremeSaParametrimaGlavniProzor(int? idTipOpreme)
        {
            OpOpremaGlavniProzorSelect op = new OpOpremaGlavniProzorSelect();
            if (idTipOpreme != null && idTipOpreme != 0)
            {
                op.DataSelectOprema = new DbItemOpremaSaParametrima()
                {
                    id_tip_opreme = (int)idTipOpreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] opremaSaParametrima = rezultat.Niz as DbItemOpremaSaParametrima[];
            return opremaSaParametrima;
        }




        public DbItemOpremaSaParametrima[] UpdateOpreme(DbItemOpremaSaParametrima opremaZaUpdate)
        {
            OpOpremaUpdate op = new OpOpremaUpdate();
            op.DataSelectOprema = opremaZaUpdate;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] opremaSaParametrima = rezultat.Niz as DbItemOpremaSaParametrima[];
            return opremaSaParametrima;
        }


        public DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanel()
        {
            OpOpremaSaParametrimaAdminPanel op = new OpOpremaSaParametrimaAdminPanel();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] opremaSaParametrima = rezultat.Niz as DbItemOpremaSaParametrima[];
            return opremaSaParametrima;
        }

        public DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanelUpdate(DbItemOpremaSaParametrima opremaZaAzuriranje)
        {
            OpOpremaSaParametrimaAdminPanelUpdate op = new OpOpremaSaParametrimaAdminPanelUpdate();
            op.DataSelectOprema = opremaZaAzuriranje;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemOpremaSaParametrima[];
        }

        public DbItemParametri[] ParametriZaIdTipaOpreme(int idTipOpreme)
        {
            OpParametriSelectWhereIdTipOpreme op = new OpParametriSelectWhereIdTipOpreme();
            op.DataSelect = new DbItemParametri()
            {
                id_tip_opreme = idTipOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemParametri[];

        }

        public DbItemOpremaSaParametrima[] OpremaSaParametrimaAdminPanelInsert(DbItemOpremaSaParametrima opremaZaInsert)
        {
            OpOpremaSaParametrimaAdminPanelInsert op = new OpOpremaSaParametrimaAdminPanelInsert();
            op.DataSelectOprema = opremaZaInsert;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemOpremaSaParametrima[];
        }



        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                string filePath = System.IO.Path.Combine(@"c:\Uploadfiles", request.FileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                if (!fileInfo.Exists)
                    throw new System.IO.FileNotFoundException("File not found",
                                                              request.FileName);

                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath,
                          System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result 
                result.FileName = request.FileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public PutanjaDoSlike UploadFile(RemoteFileInfo request)
        {
            FileStream targetStream = null;
            Stream sourceStream = request.FileByteStream;



            //  niz[i].slika = HttpContext.Current.Server.MapPath("." + niz[i].slika).ToString();

            string uploadFolder = @"" + HttpContext.Current.Server.MapPath("." + request.prefixPutanja).ToString();
            //string uploadFolder = @"C:\upload\";

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("." + request.prefixPutanja));

            string filePath = Path.Combine(uploadFolder, request.FileName);
            bool imaFajla = true;
            int brojac = 0;
            while(imaFajla)
            {
                if (File.Exists(filePath))
                {




                    int extenzijaPozicija = filePath.IndexOf(".");
                    string imeBezExtenzije = filePath.Substring(0,extenzijaPozicija);
                    Random nextRandomNumber = new Random();
                    imeBezExtenzije += nextRandomNumber.Next(10000).ToString();
                    filePath = imeBezExtenzije + filePath.Substring(extenzijaPozicija);

                    //ako je vec 1000 puta probao da unese fajl a vec postoji takva ista slika onda dodaj trenutno vreme na naziv slike
                    //
                    if (brojac >= 5)
                    {
                        extenzijaPozicija = filePath.IndexOf(".");
                        imeBezExtenzije = filePath.Substring(0, extenzijaPozicija);
                        imeBezExtenzije += DateTime.Now;
                        filePath = imeBezExtenzije + filePath.Substring(extenzijaPozicija);
                    }

                    request.FileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                }
                else
                {
                    imaFajla = false;
                }

            }

           
            using (targetStream = new FileStream(filePath, FileMode.Create,
                                  FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 65000 byte chunks

                const int bufferLen = 65000;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    // save to output stream
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
            //string putanjaZaBazu = uploadFolder + request.FileName;
            //request.FileName = putanjaZaBazu;

            return new PutanjaDoSlike() { novaPutanja = request.FileName };
        }

        public DbItemParametri[] PrikaziFiltere(int idTipOpreme)
        {
            OpParametriSelect op = new OpParametriSelect();
            op.DataSelect = new DbItemParametri()
            {
                id_tip_opreme = idTipOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] parametriSaListomVrednosti = rezultat.Niz as DbItemParametri[];
            return parametriSaListomVrednosti;
        }

        public DbItemParametri[] PrikaziFiltereGlavniProzor(int idTipOpreme)
        {
            OpParametriGlavniProzorSelect op = new OpParametriGlavniProzorSelect();
            op.DataSelect = new DbItemParametri()
            {
                id_tip_opreme = idTipOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] parametriSaListomVrednosti = rezultat.Niz as DbItemParametri[];
            return parametriSaListomVrednosti;
        }

        public DbItemOpremaSaParametrima[] KorpaSelect(int idProdavca)
        {
            OpKorpaSelect op = new OpKorpaSelect();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                idProdavca = idProdavca
            };

            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] oprema = rezultat.Niz as DbItemOpremaSaParametrima[];
            return oprema;
        }

        public OperationObject KorpaInsert(int idOprema, int kolicina, int idProdavca)
        {
            OpKorpaInsert op = new OpKorpaInsert();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                id_oprema = idOprema,
                kolicinaUKorpi = kolicina,
                idProdavca = idProdavca
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat;
        }

        public OperationObject KorpaUpdate(int idOprema, int kolicina, int idProdavca)
        {
            OpKorpaUpdate op = new OpKorpaUpdate();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                id_oprema = idOprema,
                kolicinaUKorpi = kolicina,
                idProdavca = idProdavca
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat;
        }

        public DbItemOpremaSaParametrima[] KorpaDelete(int? idOprema, int idProdavca)
        {
            OpKorpaDelete op = new OpKorpaDelete();
            
            if(idOprema != null)
            {
                op.DataSelectOprema = new DbItemOpremaSaParametrima()
                {
                    id_oprema = (int)idOprema,
                    idProdavca = idProdavca
                };
            }
            else
            {
                op.DataSelectOprema = new DbItemOpremaSaParametrima()
                {
                   idProdavca = idProdavca
                };

            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] niz = rezultat.Niz as DbItemOpremaSaParametrima[];
            return niz;
        }

        public DbItemKorisnici[] PrikaziKorisnike(int? idUloge)
        {
            OpKorisniciSelect op = new OpKorisniciSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemKorisnici[] PretragaKorisnika(string pretraga)
        {
            OpKorisniciPretraga op = new OpKorisniciPretraga();
            op.KorisniciDataSelect = new DbItemKorisnici()
            {
                zaPretragu = pretraga
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemKorisnici[] OpKorisniciInsert(DbItemKorisnici korisnik)
        {
            OpKorisniciInsert op = new OpKorisniciInsert();
            op.KorisniciDataSelect = new DbItemKorisnici()
            {
                ime = korisnik.ime,
                prezime = korisnik.prezime,
                mejl = korisnik.mejl,
                broj_telefona = korisnik.broj_telefona,
                id_uloge = korisnik.id_uloge,
                datumKreiranja = DateTime.Now
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemKorisnici[];
        }


        public DbItemKorisnici[] AzurirajBrojPoenaKorisnika(DbItemKorisnici korisnik)
        {

            OpKorisniciAzurirajBrojPoena op = new OpKorisniciAzurirajBrojPoena();
            op.KorisniciDataSelect = new DbItemKorisnici()
            {
                id_korisnici = korisnik.id_korisnici,
                brojOstvarenihPoena = korisnik.brojOstvarenihPoena,
                datumAzuriranja = korisnik.datumAzuriranja
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemKorisnici[];
        }

        public DbItemKorisnici[] PrikaziZaposleneKorisnike(DbItemKorisnici korisnik)
        {
            OpZaposleniKorisniciSelect op = new OpZaposleniKorisniciSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemKorisnici[] ZaposleniKorisniciInsert(DbItemKorisnici korisnik)
        {
            OpZaposleniKorisniciInsert op = new OpZaposleniKorisniciInsert();
            op.ZaposleniKorisniciDataSelect = new DbItemKorisnici()
            {
                ime = korisnik.ime,
                prezime = korisnik.prezime,
                mejl = korisnik.mejl,
                broj_telefona = korisnik.broj_telefona,
                brojOstvarenihPoena = korisnik.brojOstvarenihPoena,
                username = korisnik.username,
                lozinka = korisnik.lozinka,
                polKorisnika = korisnik.polKorisnika,
                slikaKorisnika = korisnik.slikaKorisnika,
                id_uloge = korisnik.id_uloge,
                datumKreiranja = DateTime.Now
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemKorisnici[] ZaposleniKorisniciUpdate(DbItemKorisnici korisnik)
        {
            OpZaposleniKorisniciUpdate op = new OpZaposleniKorisniciUpdate();
            op.ZaposleniKorisniciDataSelect = new DbItemKorisnici()
            {
                ime = korisnik.ime,
                id_korisnici = korisnik.id_korisnici,
                prezime = korisnik.prezime,
                mejl = korisnik.mejl,
                broj_telefona = korisnik.broj_telefona,
                brojOstvarenihPoena = korisnik.brojOstvarenihPoena,
                username = korisnik.username,
                lozinka = korisnik.lozinka,
                polKorisnika = korisnik.polKorisnika,
                slikaKorisnika = korisnik.slikaKorisnika,
                id_uloge = korisnik.id_uloge,
                datumAzuriranja = DateTime.Now
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemKorisnici[] KorisniciDelete(int idKorisnika)
        {
            OpKorisniciDelete op = new OpKorisniciDelete();
            op.ZaposleniKorisniciDataSelect = new DbItemKorisnici()
            {
                id_korisnici = idKorisnika
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemRezervacijeSelect[] RezervacijeSelect()
        {
            OpRezervacijeBase op = new OpRezervacijeBase();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemRezervacijeSelect[] rezervacije = rezultat.Niz as DbItemRezervacijeSelect[];
            return rezervacije;
        }

        public DbItemRezervacijeSelect[] PrebaciRezerevacijuUKorpu(int idRezervacije, int idProdavca)
        {
            OpRezervacijaUKorpu op = new OpRezervacijaUKorpu();
            op.DataSelectDbItemRezervacijaSelect = new DbItemRezervacijeSelect()
            {
                id_rezervacije = idRezervacije,
                id_Prodavca = idProdavca
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemRezervacijeSelect[] rezervacije = rezultat.Niz as DbItemRezervacijeSelect[];
            return rezervacije;
        }

        public bool RezervacijeInsert(DbItemRezervacijeInsert rezervacija)
        {
            OpRezervacijeInsert op = new OpRezervacijeInsert();
            op.DataSelectDbItemRezervacijaInsert = new DbItemRezervacijeInsert()
            {
                imeNaRezervacija = rezervacija.imeNaRezervacija,
                datum_rezervacije = rezervacija.datum_rezervacije,
                datum_azuriranja_rezervacije = rezervacija.datum_azuriranja_rezervacije,
                datum_isteka_rezervacije = rezervacija.datum_isteka_rezervacije,
                brojTelefona = rezervacija.brojTelefona,
                ListaOpremeZaRezervaciju = rezervacija.ListaOpremeZaRezervaciju
                
            };

            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Success;
        }

        public OperationObject RezervacijeUpdate(DbItemRezervacijeDeleteIUpdate[] rezervacija)
        {
            //SmartSoftwareWebService.BiznisSloj.OpRezervacijeUpdate op = new OpRezervacijeUpdate();
            //op.DataSelectOpremaRezervacija = new DbItemOpremaSaParametrimaRezervacija()
            //{
            //    id_oprema = rezervacija[0].id_oprema,
            //    kolicina_rezervisane_opreme = rezervacija[0].kolicina_rezervisane_opreme,
            //    datum_rezervacije = rezervacija[0].datum_rezervacije,
            //    datum_azuriranja_rezervacije = rezervacija[0].datum_azuriranja_rezervacije,
            //    datum_isteka_rezervacije = rezervacija[0].datum_isteka_rezervacije,
            //    brojTelefona = rezervacija[0].brojTelefona
            //};
            //OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return null;
        }

        //public DbItemRezervacijaSaOpremomSaParametrima[] RezervacijeOpremaDelete(int? idRezervacije, int? idOprema)
        //{
            //SmartSoftwareWebService.BiznisSloj.OpRezervacijeOpremaDelete op = new OpRezervacijeOpremaDelete();

            //if (idOprema != null)
            //{
            //    op.DataSelectOpremaSaParametrimaRezervacija = new DbItemOpremaSaParametrimaRezervacija()
            //    {
            //        id_oprema = (int)idOprema
            //    };
            //}
            //OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            //DbItemRezervacijaSaOpremomSaParametrima[] niz = rezultat.Niz as DbItemRezervacijaSaOpremomSaParametrima[];
        //    return null;
        //}

        public DbItemRezervacijeSelect[] RezervacijeDelete(DbItemRezervacijeDeleteIUpdate rezervacija)
        {
            SmartSoftwareWebService.BiznisSloj.OpRezervacijeDelete op = new OpRezervacijeDelete();

            if (rezervacija  != null)
            {
                op.DataSelectRezervacijeDelete = new DbItemRezervacijeDeleteIUpdate() { id_rezervacije = rezervacija.id_rezervacije, ListaOpremeZaRezervaciju = rezervacija.ListaOpremeZaRezervaciju };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemRezervacijeSelect[] niz = rezultat.Niz as DbItemRezervacijeSelect[];
            return niz;
        }
        

        public DbItemOpremaSaParametrima[] PretragaOpreme(string zaPretragu, bool zaAdminPanel)
        {
            Operation op = null;
            if(zaAdminPanel)
            {
                op = new OpOpremaPretragaAdminPanel();
                if (zaPretragu != "")
                {
                    (op as OpOpremaPretragaAdminPanel).DataSelectOprema = new DbItemOpremaSaParametrima()
                    {
                        zaPretragu = zaPretragu
                    };
                }
            }
            else
            {
                op = new OpOpremaPretraga();
                if (zaPretragu != "")
                {
                    (op as OpOpremaPretraga).DataSelectOprema = new DbItemOpremaSaParametrima()
                    {
                        zaPretragu = zaPretragu
                    };
                }
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] niz = rezultat.Niz as DbItemOpremaSaParametrima[];
            return niz;
        }


        public DbItemOpremaSaParametrima[] PrikaziOpremuPoFilterima(int idTipOpreme, DbItemParametri[] filteri)
        {
            OpOpremaSelectFilteri op = new OpOpremaSelectFilteri();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                id_tip_opreme = idTipOpreme, ListaParametara = filteri.ToList()
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] niz = rezultat.Niz as DbItemOpremaSaParametrima[];
            return niz;

        }


        public DbItemOblastiOpreme[] OblastiOpremeInsert(DbItemOblastiOpreme o)
        {
            OpOblastiOpremeInsert op = new OpOblastiOpremeInsert();
            if (o != null)
            {
                op.DataSelectOblastiOpreme = new DbItemOblastiOpreme()
                {
                    naziv_oblasti_opreme = o.naziv_oblasti_opreme,
                    picture = o.picture
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] niz = rezultat.Niz as DbItemOblastiOpreme[];
            return niz;
        }


        public DbItemOblastiOpreme[] OblastiOpremeUpdate(DbItemOblastiOpreme o)
        {
            OpOblastiOpremeUpdate op = new OpOblastiOpremeUpdate();
            if (o != null)
            {
                op.DataSelectOblastiOpreme = new DbItemOblastiOpreme()
                {
                    id_oblasti_opreme = o.id_oblasti_opreme,
                    naziv_oblasti_opreme = o.naziv_oblasti_opreme,
                    picture = o.picture
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] niz = rezultat.Niz as DbItemOblastiOpreme[];
            return niz;

        }

        public DbItemOblastiOpreme[] OpOblastiOpremeDelete(DbItemOblastiOpreme o)
        {
            OpOblastiOpremeDelete op = new OpOblastiOpremeDelete();
            if (o != null)
            {
                op.DataSelectOblastiOpreme = new DbItemOblastiOpreme()
                {
                    id_oblasti_opreme = o.id_oblasti_opreme
                }; 
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] niz = rezultat.Niz as DbItemOblastiOpreme[];
            return niz;

        }



        public DbItemKupljenaOpremaSaParametrima[] ProdajaArtikla(DbItemIstorijaKupovine prodaja, DbItemKupljenaOpremaSaParametrima[] prodataOprema)
        {
            OpIstorijaKupovineInsert op = new OpIstorijaKupovineInsert();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                datum_prodaje = prodaja.datum_prodaje,
                Kupac = prodaja.Kupac,
                prodavac = prodaja.prodavac,
                ukupna_cena_kupovine = prodaja.ukupna_cena_kupovine
            };
            op.ListaKupljeneOpremaDataSelect = prodataOprema;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKupljenaOpremaSaParametrima[] niz = rezultat.Niz as DbItemKupljenaOpremaSaParametrima[];
            return niz;
        }

        public DbItemIstorijaKupovine[] IstorijaKupovineSelect()
        {
            OpIstorijaKupovineSelect op = new OpIstorijaKupovineSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemIstorijaKupovine[] niz = rezultat.Niz as DbItemIstorijaKupovine[];
            return niz;
        }

        public DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaDatumSelect()
        {
            OpIstorijaKupovineZaListuKupacaDatumSelect op = new OpIstorijaKupovineZaListuKupacaDatumSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemIstorijaKupovine[] niz = rezultat.Niz as DbItemIstorijaKupovine[];
            return niz;
        }

        public DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaDatumPretraga(string searchIme)
        {
            OpIstorijaKupovineZaListuKupacaDatumSelectPretraga op = new OpIstorijaKupovineZaListuKupacaDatumSelectPretraga();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                searchKupacIme = searchIme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemIstorijaKupovine[] niz = rezultat.Niz as DbItemIstorijaKupovine[];
            return niz;
        }

        public DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaImeSelect()
        {
            OpIstorijaKupovineZaListuKupacaImeSelect op = new OpIstorijaKupovineZaListuKupacaImeSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemIstorijaKupovine[] niz = rezultat.Niz as DbItemIstorijaKupovine[];
            return niz;
        }

        public DbItemIstorijaKupovine[] IstorijaKupovineZaListuKupacaBrojKupovinaSelect(bool asc)
        {
            OpIstorijaKupovineZaListuKupacaBrojKupovinaSelect op = new OpIstorijaKupovineZaListuKupacaBrojKupovinaSelect();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                asc = asc
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemIstorijaKupovine[] niz = rezultat.Niz as DbItemIstorijaKupovine[];
            return niz;
        }
        
        


        public DbItemOpremaSaParametrima[] OpremaDelete(int idOpreme)
        {
            opOpremaSaParametrimaAdminPanelDelete op = new opOpremaSaParametrimaAdminPanelDelete();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                 id_oprema = idOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] niz = rezultat.Niz as DbItemOpremaSaParametrima[];
            return niz;
        }

        public DbItemOpremaSaParametrima[] OpremaRestore(int idOpreme)
        {
            opOpremaSaParametrimaAdminPanelRestore op = new opOpremaSaParametrimaAdminPanelRestore();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                id_oprema = idOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] niz = rezultat.Niz as DbItemOpremaSaParametrima[];
            return niz;
        }


        public DbItemTipOpreme[] TipoviOpremeRestore(DbItemTipOpreme TipOpreme)
        {
            OpTipoviOpremeRestore op = new OpTipoviOpremeRestore();
            if (TipOpreme != null)
            {
                op.DataSelectTipoviOpreme = new DbItemTipOpreme()
                {
                    id_tip_opreme = TipOpreme.id_tip_opreme
                };
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipOpreme[] niz = rezultat.Niz as DbItemTipOpreme[];
            return niz;
        }




        public DbItemOblastiOpreme[] OpOblastiOpremeRestore(DbItemOblastiOpreme o)
        {

            OpOblastiOpremeRestore op = new OpOblastiOpremeRestore();
            if (o != null)
            {
                op.DataSelectOblastiOpreme = new DbItemOblastiOpreme()
                {
                    id_oblasti_opreme = o.id_oblasti_opreme
                }; 
            }
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOblastiOpreme[] niz = rezultat.Niz as DbItemOblastiOpreme[];
            return niz;
        }

        public DbItemParametri[] SelectAdminPanelParametri()
        {
            OpSelectAdminPanelParametriSelect op = new OpSelectAdminPanelParametriSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Niz as DbItemParametri[];
        }


        public DbItemParametri[] ParametriInsert(DbItemParametri p)
        {
            OpParametriInsert op = new OpParametriInsert();
            op.DataSelect = new DbItemParametri()
                {
                    default_vrednost = p.default_vrednost,
                    id_parametri = p.id_parametri,
                    id_tip_opreme = p.id_tip_opreme,
                    naziv_parametra = p.naziv_parametra,
                    tipParametra = p.tipParametra,
                    vrednost_parametra = p.vrednost_parametra,
                    za_filter = p.za_filter
                };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] niz = rezultat.Niz as DbItemParametri[];
            return niz;
        }


        public DbItemParametri[] ParametriUpdate(DbItemParametri p)
        {
            OpParametriUpdate op = new OpParametriUpdate();
            op.DataSelect = new DbItemParametri()
            {
                default_vrednost = p.default_vrednost,
                id_parametri = p.id_parametri,
                id_tip_opreme = p.id_tip_opreme,
                naziv_parametra = p.naziv_parametra,
                tipParametra = p.tipParametra,
                vrednost_parametra = p.vrednost_parametra,
                za_filter = p.za_filter
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] niz = rezultat.Niz as DbItemParametri[];
            return niz;
        }


        public DbItemParametri[] ParametriDelete(int idParametra)
        {
            OpParametriDelete op = new OpParametriDelete();
            op.DataSelect = new DbItemParametri()
            {
                id_parametri = idParametra
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] niz = rezultat.Niz as DbItemParametri[];
            return niz;
        }

        public DbItemParametri[] ParametriRestore(int idParametra)
        {
            OpParametriRestore op = new OpParametriRestore();
            op.DataSelect = new DbItemParametri()
            {
                id_parametri = idParametra
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] niz = rezultat.Niz as DbItemParametri[];
            return niz;
        }


        public DbItemParametri[] ParametriPretraga(string zaPretragu)
        {
            OpParametriPretraga op = new OpParametriPretraga();
            op.DataSelect = new DbItemParametri()
            {
                zaPretragu = zaPretragu
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemParametri[] niz = rezultat.Niz as DbItemParametri[];
            return niz;
        }


        public DbItemKorisnici[] KorisniciRestore(int idKorisnika)
        {
            OpKorisniciRestore op = new OpKorisniciRestore();
            op.ZaposleniKorisniciDataSelect = new DbItemKorisnici()
            {
                id_korisnici = idKorisnika
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKorisnici[] niz = rezultat.Niz as DbItemKorisnici[];
            return niz;
        }

        public DbItemOpremaSaParametrimaStatistika[] IstorijaKupovineNajprodavanijaOprema(bool asc)
        {
            OpIstorijaKupovineNajprodavanijaOprema op = new OpIstorijaKupovineNajprodavanijaOprema();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                asc = asc
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrimaStatistika[] niz = rezultat.Niz as DbItemOpremaSaParametrimaStatistika[];
            return niz;
        }

        public int IstorijaKupovineUkupanBrojProdateOpreme()
        {
            OpIstorijaKupovineUkupnaKolicinaProdateOpreme op = new OpIstorijaKupovineUkupnaKolicinaProdateOpreme();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            int? broj = rezultat.Niz[0] as int?;
            return (int)broj;
        }

        public int IstorijaKupovineUkupanBrojProdateOpremeZaDanas(DateTime danas, DateTime sutra)
        {
            OpIstorijaKupovineKolicinaProdateOpremeZaDanas op = new OpIstorijaKupovineKolicinaProdateOpremeZaDanas();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                datumDanas = danas,
                datumSutra = sutra
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            int? broj = rezultat.Niz[0] as int?;
            return (int)broj;
        }

        public double IstorijaKupovineUkupnoZaradjeno()
        {
            OpIstorijaKupovineUkupnoZaradjeno op = new OpIstorijaKupovineUkupnoZaradjeno();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            double? broj = rezultat.Niz[0] as double?;
            return (double) broj;
        }

        public double IstorijaKupovineZaradjenoDanas(DateTime danas, DateTime sutra)
        {
            OpIstorijaKupovineZaradjenoDanas op = new OpIstorijaKupovineZaradjenoDanas();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                datumDanas = danas,
                datumSutra = sutra
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            double? broj = rezultat.Niz[0] as double?;
            return (double) broj;
        }

        public DbItemKupci[] IstorijaKupovineInicijalniPrikazKupaca()
        {
            OpIstorijaKupovineKupciInitPrikaz op = new OpIstorijaKupovineKupciInitPrikaz();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKupci[] niz = rezultat.Niz as DbItemKupci[];
            return niz;
        }

        public DbItemKupci[] IstorijaKupovineNajcesciKupci(bool asc)
        {
            OpIstorijaKupovineNajcesciKupci op = new OpIstorijaKupovineNajcesciKupci();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                asc = asc
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKupci[] niz = rezultat.Niz as DbItemKupci[];
            return niz;
        }

        public DbItemKupci[] IstorijaKupovineKupciKojiNajviseKupuju(bool asc)
        {
            OpIstorijaKupovinekupciKojiNajviseKupuju op = new OpIstorijaKupovinekupciKojiNajviseKupuju();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                asc = asc
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKupci[] niz = rezultat.Niz as DbItemKupci[];
            return niz;
        }

        public DbItemKupci[] IstorijaKupovineKupciKojiNajviseTrose(bool asc)
        {
            OpIstorijaKupovinePotrosnjaKupaca op = new OpIstorijaKupovinePotrosnjaKupaca();
            op.IstorijaKupovineDataSelect = new DbItemIstorijaKupovine()
            {
                asc = asc
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKupci[] niz = rezultat.Niz as DbItemKupci[];
            return niz;
        }

        //narudzbine


        public DbItemNarudzbine[] OpNarudzbineSelect()
        {
            OpNaruzbineSelect op = new OpNaruzbineSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemNarudzbine[] niz = rezultat.Niz as DbItemNarudzbine[];
            return niz;
        }

        public DbItemNarudzbine[] OpNarudzbinePrihvatiNarudzbinu(DbItemNarudzbine narudzbina)
        {
            OpNaruzbinePrihvati op = new OpNaruzbinePrihvati();
            op.NaruzbineDataSelect = new DbItemNarudzbine()
            {
                id_narudzbine = narudzbina.id_narudzbine,
                datum_narudzbine = narudzbina.datum_narudzbine,
                id_oprema = narudzbina.id_oprema,
                id_prodavca = narudzbina.id_prodavca,
                kolicina = narudzbina.kolicina
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemNarudzbine[] niz = rezultat.Niz as DbItemNarudzbine[];
            return niz;
        }

        public DbItemNarudzbine[] OpNarudzbineOdbijNarudzinu(DbItemNarudzbine narudzbina)
        {
            OpNaruzbineDelete op = new OpNaruzbineDelete();
            op.NaruzbineDataSelect = new DbItemNarudzbine()
            {
                id_narudzbine = narudzbina.id_narudzbine,
                datum_narudzbine = narudzbina.datum_narudzbine,
                id_oprema = narudzbina.id_oprema,
                id_prodavca = narudzbina.id_prodavca,
                kolicina = narudzbina.kolicina
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemNarudzbine[] niz = rezultat.Niz as DbItemNarudzbine[];
            return niz;
        }


        public bool OpNarudzbineInsert(DbItemNarudzbine narudzbina)
        {
            OpNaruzbineInsert op = new OpNaruzbineInsert();
            op.NaruzbineDataSelect = new DbItemNarudzbine()
            {
                datum_narudzbine = narudzbina.datum_narudzbine,
                id_oprema = narudzbina.id_oprema,
                id_prodavca = narudzbina.id_prodavca,
                kolicina = narudzbina.kolicina
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            return rezultat.Success;
        }


        //kolekcija opreme

        public DbItemKolekcijaOpreme[] OpKolekcijaOpremeSelect()
        {
            OpKolekcijaOpremeSelect op = new OpKolekcijaOpremeSelect();
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKolekcijaOpreme[] niz = rezultat.Niz as DbItemKolekcijaOpreme[];
            return niz;
        }

        public DbItemKolekcijaOpreme[] OpKolekcijaOpremeInsert(DbItemKolekcijaOpreme oprema)
        {
            OpKolekcijaOpremeInsert op = new OpKolekcijaOpremeInsert();
            op.DataSelectKolekcijaOpreme = oprema;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemKolekcijaOpreme[] niz = rezultat.Niz as DbItemKolekcijaOpreme[];
            return niz;
        }

        public DbItemOpremaSaParametrima[] UpdateOpremeSkiniOpremuSaLagera(int idOprema)
        {
            OpOpremaSkiniSaLagera op = new OpOpremaSkiniSaLagera();
            op.DataSelectOprema = new DbItemOpremaSaParametrima()
            {
                id_oprema = idOprema
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemOpremaSaParametrima[] opremaSaParametrima = rezultat.Niz as DbItemOpremaSaParametrima[];
            return opremaSaParametrima;
        }

        public DbItemGrupeOgranicenja[] OgranicenjaSelect(int idTipaKolekcijeOgranicenja)
        {
            OpOgranicenjeSelect op = new OpOgranicenjeSelect();
            op.DataSelectOgranicenja = new DbItemGrupeOgranicenja()
            {
                id_tip_opreme_kolekcije = idTipaKolekcijeOgranicenja
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemGrupeOgranicenja[] nizOgranicenja = rezultat.Niz as DbItemGrupeOgranicenja[];
            return nizOgranicenja;
        }

        public DbItemGrupeOgranicenja[] OgranicenjaInsert(DbItemGrupeOgranicenja ogranicenje)
        {
            OpOgranicenjaInsert op = new OpOgranicenjaInsert();
            op.DataSelectOgranicenja = new DbItemGrupeOgranicenja()
            {
                id_tip_opreme_kolekcije = ogranicenje.id_tip_opreme_kolekcije,
                tipProvere = ogranicenje.tipProvere,
                id_tip_opreme2 = ogranicenje.id_tip_opreme2,
                id_tip_opreme1 = ogranicenje.id_tip_opreme1,
                id_parametra2 = ogranicenje.id_parametra2,
                id_parametra1 = ogranicenje.id_parametra1,
                Id_grupe_ogranicenja = ogranicenje.Id_grupe_ogranicenja
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemGrupeOgranicenja[] nizOgranicenja = rezultat.Niz as DbItemGrupeOgranicenja[];
            return nizOgranicenja;
        }

        public DbItemTipoviZaKonfiguraciju[] TipoviZaNovuKonfiguraciju(int idTipOpreme)
        {
            OpTipoviOpremeZaKonfiguraciju op = new OpTipoviOpremeZaKonfiguraciju();
            op.DataSelectTipoviOpreme = new DbItemTipoviZaKonfiguraciju()
            {
                 idTipOpremeKolekcije = idTipOpreme
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipoviZaKonfiguraciju[] nizOgranicenja = rezultat.Niz as DbItemTipoviZaKonfiguraciju[];
            return nizOgranicenja;
        }

        public DbItemTipoviZaKonfiguraciju[] TipoviZaNovuKonfiguracijuInsertUpdate(int idTipOpremeKolekcije, DbItemTipoviZaKonfiguraciju[] kolekcijaKonfiguracije)
        {
            OpTipoviOpremeZaKonfiguracijuInsert op = new OpTipoviOpremeZaKonfiguracijuInsert();
            op.DataSelectTipoviOpreme = new DbItemTipoviZaKonfiguraciju()
            {
                idTipOpremeKolekcije = idTipOpremeKolekcije
            };
            op.DataSelectListaTipovaZaKonfiguraciju = kolekcijaKonfiguracije;
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipoviZaKonfiguraciju[] nizOgranicenja = rezultat.Niz as DbItemTipoviZaKonfiguraciju[];
            return nizOgranicenja;
        }




        //ogranicenja


        public DbItemTipoviZaKonfiguraciju[] OpPrikazKolekcijeZaUnos(int idTipOpremeKolekcije)
        {
            OpPrikazOnihKojiSuChekiraniInicijalno op = new OpPrikazOnihKojiSuChekiraniInicijalno();
            op.DataSelectTipoviOpreme = new DbItemTipoviZaKonfiguraciju()
            {
                idTipOpremeKolekcije = idTipOpremeKolekcije
            };
            OperationObject rezultat = OperationManager.Singleton.executeOp(op);
            DbItemTipoviZaKonfiguraciju[] nizOgranicenja = rezultat.Niz as DbItemTipoviZaKonfiguraciju[];
            return nizOgranicenja;
        }




       
    }
}