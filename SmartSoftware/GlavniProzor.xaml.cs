using Microsoft.Win32;
using SmartSoftware;
using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using SmartSoftware.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using Xceed.Wpf.Toolkit;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window, INotifyPropertyChanged
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        bool daLiJePosecenTimer = false;

        DispatcherTimer timerOtvoriAdminPanel = new DispatcherTimer();
        DispatcherTimer timerZatvoriAdminPanel = new DispatcherTimer();

        DispatcherTimer timerOtvoriRezervacije = new DispatcherTimer();
        DispatcherTimer timerZatvoriRezervacije = new DispatcherTimer();

        DispatcherTimer timerOtvoriKorpu = new DispatcherTimer();
        DispatcherTimer timerZatvoriKorpu = new DispatcherTimer();


        private bool prviPutUcitanTriVjuTipoviOpreme = false;
        private bool prviPutUcitanaPretraga = false;
        private bool prviPutUcitanoResetovanje = false;
        //private bool prviPutUcitanTriVjuTipoviOpreme = false;

        #region Animacije

        

        //Thickness standardnaMargina = new Thickness(0);
        //Thickness marginaZaBrdBorderi = new Thickness(0, 0, -1, 0);

        //Thickness marginaZaBrdBorderiUvuceno = new Thickness(35, 0, 0, 0);
        DoubleAnimation daAnimacijaPrikaziPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
        DoubleAnimation daAnimacijaSakrijPolaSekunde = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));

        DoubleAnimation daAnimacijaPrikaziSekund = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 1));
        DoubleAnimation daAnimacijaSakrijSekund = new DoubleAnimation(0, new TimeSpan(0, 0, 1));


        DoubleAnimation daSakrijGlavniProzor = new DoubleAnimation(0, 0.5, new TimeSpan(0, 0, 0, 0, 500));
        DoubleAnimation daPrikaziGlavniProzor = new DoubleAnimation(0.5,0, new TimeSpan(0, 0, 0, 0, 500));


        ThicknessAnimation taNazivKategorijeMargina = new ThicknessAnimation(new Thickness(-5, 0, 0, 0), new Thickness(0), new TimeSpan(0, 0, 0, 0, 200));
        DoubleAnimation daBrdTriVjuHierarchicalNazivSirina = new DoubleAnimation(0, 187, new TimeSpan(0, 0, 0, 0, 200));
        DoubleAnimation daBrdTriVjuTipoviOpremeNazivSirina = new DoubleAnimation(0, 157, new TimeSpan(0, 0, 0, 0, 200));

        DoubleAnimation daPrikaziFiltere = new DoubleAnimation(120, new TimeSpan(0, 0, 1));
        DoubleAnimation daSakrijFiltere = new DoubleAnimation(0, new TimeSpan(0, 0, 1));

        DoubleAnimation daTriTackeZaDetalje = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));


        ThicknessAnimation taMarginaZaBrdBorderi = new ThicknessAnimation(new Thickness(35, 0, 0, 0), new Thickness(0, 0, -1, 0), new TimeSpan(0, 0, 1));


        //DoubleAnimation daPrikaziPraznuOpremuZbogOdabranihFiltera = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
        //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
        //DoubleAnimation daPrikaziPraznuOpremu = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));

        #endregion





        private string slikaProdavca = "";

        public string SlikaProdavca
        {
            get { return slikaProdavca; }
            set { SetAndNotify(ref slikaProdavca, value); }
        }


        ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }






        private Narudzbina narudzbina = new Narudzbina();

        public Narudzbina Narudzbina
        {
            get { return narudzbina; }
            set { SetAndNotify(ref narudzbina, value); }
        }





        public int brojacZaTextBlockLoaded = 0;
        private List<OblastiOpreme> listaOblastiOpreme = new List<OblastiOpreme>();

        public List<OblastiOpreme> ListaOblastiOpreme
        {
            get { return listaOblastiOpreme; }
            set { SetAndNotify(ref listaOblastiOpreme, value); }
        }

        private OblastiOpreme trenutnoKliknutaOblastOpreme;

        public OblastiOpreme TrenutnoKliknutaOblastOpreme
        {
            get { return trenutnoKliknutaOblastOpreme; }
            set { SetAndNotify(ref trenutnoKliknutaOblastOpreme, value); }
        }

        int selektovanRed = 0;
        double visinaSkrolaZaOpremu = 0;

        bool dalisepozivabaza = false;
        private SmartSoftwareDocumentManagerVM documentManagerVM = new SmartSoftwareDocumentManagerVM();
        //private List<Oprema> lista = new List<Oprema>();

        //public List<Oprema> Lista
        //{
        //    get { return lista; }
        //    set { SetAndNotify(ref lista, value); }
        //}

        //private SmartSoftwareGlavnaOblast tmpEditObj;

        //public SmartSoftwareGlavnaOblast TmpEditObj
        //{
        //    get { return tmpEditObj; }
        //    set { SetAndNotify(ref tmpEditObj, value); }
        //}
        bool nemaDetaljnogPrikaza = false;
        bool izmenjenaOprema = false;
        private Oprema currentOprema;

        public Oprema CurrentOprema
        {
            get { return currentOprema; }
            set { SetAndNotify(ref currentOprema, value); }
        }

        //private Oprema documentManagerVM.TrenutnaOpremaZaMenjanje;

        //public Oprema documentManagerVM.TrenutnaOpremaZaMenjanje
        //{
        //    get { return documentManagerVM.TrenutnaOpremaZaMenjanje; }
        //    set { SetAndNotify(ref documentManagerVM.TrenutnaOpremaZaMenjanje, value); }
        //}


        public static ObservableCollection<Oprema> listaTrenutnihRezervacija = new ObservableCollection<Oprema>();

        //public static Korisnici trenutniProdavac = new Korisnici();





        private bool aktivnoRezervisanje = false;

        public bool AktivnoRezervisanje
        {
            get { return aktivnoRezervisanje; }
            set { SetAndNotify(ref aktivnoRezervisanje, value); }
        }

        private TrenutnaListaRezervacija rezDodavanje = new TrenutnaListaRezervacija();

        public TrenutnaListaRezervacija RezDodavanje
        {
            get { return rezDodavanje; }
            set { SetAndNotify(ref rezDodavanje, value); }
        }

        private int idUloge;


        //private int currentTipOpreme = 0;

        //public int CurrentTipOpreme
        //{
        //    get { return currentTipOpreme; }
        //    set { SetAndNotify(ref currentTipOpreme, value); }
        //}

        private TipoviOpreme currentTipOpreme = new TipoviOpreme(null);

        public TipoviOpreme CurrentTipOpreme
        {
            get { return currentTipOpreme; }
            set { SetAndNotify(ref currentTipOpreme, value); }
        }

        private TipoviOpreme stariTipOpreme;

        public TipoviOpreme StariTipOpreme
        {
            get { return stariTipOpreme; }
            set { SetAndNotify(ref stariTipOpreme, value); }
        }



        private string putanjaDoSlikeExpand = App.rootPath + "\\slike\\arrowDole-128-Blue.png";

        public string PutanjaDoSlikeExpand
        {
            get { return putanjaDoSlikeExpand; }
            set { SetAndNotify(ref putanjaDoSlikeExpand, value); }
        }

        public GlavniProzor(Korisnici trenutniProdavac)
        {
            InitializeComponent();
            this.DataContext = documentManagerVM;
            
            this.documentManagerVM.PosebnaKolona = new GridLength(0);

            if (trenutniProdavac.IdUloge == 1) AdminPanelKlik.Visibility = Visibility.Visible;
            else AdminPanelKlik.Visibility = Visibility.Hidden;

            this.documentManagerVM.TrenutniProdavac = trenutniProdavac;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = service.PrikaziZaposleneKorisnike(null);
            this.popuniListuKorisnici(korisnici);


            if (!File.Exists(this.documentManagerVM.TrenutniProdavac.SlikaKorisnika) && ListaKorisnika != null && ListaKorisnika.Count > 0)
            {
                for (int i = 0; i < ListaKorisnika.Count; i++)
			    {
			        if(ListaKorisnika[i].IdKorisnici == this.documentManagerVM.TrenutniProdavac.IdKorisnici)
                    {
                        this.documentManagerVM.TrenutniProdavac.SlikaKorisnika = ListaKorisnika[i].SlikaKorisnika;
                    }
                
			    }
            }


            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(this.documentManagerVM.TrenutniProdavac.IdKorisnici);
            if (oprema.Length > 0)
            {
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(null, this.documentManagerVM.TrenutniProdavac.IdKorisnici);
            }
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaSelect(this.documentManagerVM.TrenutniProdavac.IdKorisnici);
            this.popuniKorpu(opremaPonovo);

            if (this.documentManagerVM.Korpa.Count > 0)
            {
                korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpaIma");
            }
            else
            {
                korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpa");
            }

            if (GlavniProzor.listaTrenutnihRezervacija.Count > 0)
            {
                RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacijeIma");
            }
            else
            {
                RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacije");
            }

            


            //SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpreme();
            //this.popuniListuOblastiOpreme(nizOblasti);

            //SmartSoftwareServiceReference.[] nizOblasti = service.OblastiOpreme();
            //this.popuniListuOblastiOpreme(nizOblasti);
            //SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovi = service.TipOpreme();



            //if(TipoviOpreme.Id = 1)
            //{
            //    slikaZaTipove.Source = 
            //}




            //OVDJE TESTIRANJE
            /*
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrimaStatistika[] nizFilteri = service.IstorijaKupovineNajprodavanijaOprema(true);


            int rezProdataOprema = service.IstorijaKupovineUkupanBrojProdateOpreme();

            DateTime danas = DateTime.Now;
            DateTime sutra = danas.AddDays(1);

            //ovo dole ne radi
            int rezProdataOpremaDanas = service.IstorijaKupovineUkupanBrojProdateOpremeZaDanas(danas, sutra);

            double rezZaradjeno = service.IstorijaKupovineUkupnoZaradjeno();

            double rezZaradjenoDanas = service.IstorijaKupovineZaradjenoDanas(danas, sutra);

            SmartSoftwareServiceReference.DbItemKupci[] nizNajcesciKupci = service.IstorijaKupovineNajcesciKupci(true);

            SmartSoftwareServiceReference.DbItemKupci[] nizVelikiPotrosaci = service.IstorijaKupovineKupciKojiNajviseKupuju(true);

            SmartSoftwareServiceReference.DbItemKupci[] nizKupciPotrosnja = service.IstorijaKupovineKupciKojiNajviseTrose(true);


            DbItemNarudzbine narudzbina1 = new DbItemNarudzbine()
            {
                datum_narudzbine = DateTime.Now,
                id_narudzbine = 8,
                id_oprema = 1,
                id_prodavca = 14
                kolicina = 1
            };

            bool rezultatNarudzbineInsert = service.OpNarudzbineInsert(narudzbina1);


            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine = service.OpNarudzbineSelect();

            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine1 = service.OpNarudzbinePrihvatiNarudzbinu(narudzbina1);

            DbItemNarudzbine narudzbina2 = new DbItemNarudzbine()
            {
                datum_narudzbine = DateTime.Now,
                id_narudzbine = 9,
                id_oprema = 6,
                id_prodavca = 14,
                kolicina = 3
            };
            bool rezultatNarudzbineInsert2 = service.OpNarudzbineInsert(narudzbina2);
            
            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbine2 = service.OpNarudzbineOdbijNarudzinu(narudzbina2);

            //kolekcija opreme
            SmartSoftwareServiceReference.DbItemKolekcijaOpreme[] nizKolekcijeOpreme = service.OpKolekcijaOpremeSelect();
            DbItemKolekcijaOpreme kolekcijaOpreme = new DbItemKolekcijaOpreme()
            {  
                cena = 10000,
                DeletedField = false,
                id_oprema = 0,
                id_tip_opreme = 13,
                kolicinaUKorpi = 0,
                kolicinaURezervacijama = 0,
                lager = true,
                kolicina_na_lageru = 10,
                kolicina_u_rezervi = 10,
                model = "nk3000",
                ListaParametara = new DbItemParametri[0],
                kolekcijaOpremeIdjevi = new int[2] { 1, 6 },
                KolekcijaOpreme = null,
                naslov = "nova kolekcija opreme",
                oprema_na_popustu = 0,
                opis = "znaci najnoviji komp mi ga sklapali najaci je master Marko i padawan Mihailo su sklapali tako da ono zna se sta valja",
                proizvodjac = "SmartSoftware prodavniica",
                slika = null,
                slikaOriginalPutanja = null,
                zaPretragu = ""
            };
            SmartSoftwareServiceReference.DbItemKolekcijaOpreme[] nizKolekcijeOpreme1 = service.OpKolekcijaOpremeInsert(kolekcijaOpreme);

            */
        }


        ObservableCollection<Korisnici> ListaKorisnikaTrenutno = null;
        private void popuniListuKorisnici(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnikaTrenutno = null;
            ListaKorisnikaTrenutno = new ObservableCollection<Korisnici>();
            for (int i = 0; i < nizKorisnika.Length; i++)
            {

                ListaKorisnikaTrenutno.Add(new Korisnici()
                {
                    IdKorisnici = nizKorisnika[i].id_korisnici,
                    IdUloge = nizKorisnika[i].id_uloge,
                    IzabranaUloga = nizKorisnika[i].id_uloge - 1,
                    ImeKorisnika = nizKorisnika[i].ime,
                    PrezimeKorisnika = nizKorisnika[i].prezime,
                    MejlKorisnika = nizKorisnika[i].mejl,
                    BrojTelefonaKorisnika = nizKorisnika[i].broj_telefona,
                    BrojOstvarenihPoena = Convert.ToDouble(nizKorisnika[i].brojOstvarenihPoena),
                    Username = nizKorisnika[i].username,
                    Lozinka = nizKorisnika[i].lozinka,
                    NazivUloge = nizKorisnika[i].naziv_uloge,
                    ImeIPrezimeKorisnika = nizKorisnika[i].ime + " " + nizKorisnika[i].prezime,
                    DeletedItem = nizKorisnika[i].deletedField,
                    PolKorisnika = nizKorisnika[i].polKorisnika,
                    SlikaKorisnika = nizKorisnika[i].slikaKorisnika == null ? nizKorisnika[i].id_uloge == 1 ? App.PutanjaDoSlikeAdministratorKorisnici : App.PutanjaDoSlikeProdavacKorisnici : nizKorisnika[i].slikaKorisnika,
                    DatumKreiranjaKorisnika = nizKorisnika[i].datumKreiranja,
                    DatumAzuriranjaKorisnika = nizKorisnika[i].datumAzuriranja,
                });
            }
            this.ListaKorisnika = ListaKorisnikaTrenutno;
        }



        #region PropertyChangedImpl
        protected void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        public void popuniListuOblastiOpreme(DbItemOblastiOpreme[] nizOblasti)
        {
            this.ListaOblastiOpreme = new List<OblastiOpreme>();

            for (int i = 0; i < nizOblasti.Count(); i++)
            {
                OblastiOpreme oblast = new OblastiOpreme(null)
                {
                    IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme

                };
                this.ListaOblastiOpreme.Add(oblast);

            }
        }


        
        
        //private void trivju_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    this.documentManagerVM.CurrentDocumentVM.CurrentSmartSoftwareGlavnaOblastVM = e.NewValue as SmartSoftwareGlavnaOblastVM;


        //    //ControlTemplate ctpOdabirOpreme = ctlOdabirOpreme.Template;


            

        //    //ItemsControl pera = ctpOdabirOpreme.FindName("pera", ctlOdabirOpreme) as ItemsControl;
        //    //ItemsControl kolona2 = ctpOdabirOpreme.FindName("pera2", ctlOdabirOpreme) as ItemsControl;
        //    //ItemsControl kolona3 = ctpOdabirOpreme.FindName("pera3", ctlOdabirOpreme) as ItemsControl;
        //    //ItemsControl kolona4 = ctpOdabirOpreme.FindName("pera4", ctlOdabirOpreme) as ItemsControl;
        //    ////pera.ItemsSource = null;
        //    //if (kolona2 != null)
        //    //    kolona2.ItemsSource = null;
        //    //if (kolona3 != null)
        //    //    kolona3.ItemsSource = null;
        //    //if (kolona4 != null)
        //    //    kolona4.ItemsSource = null;
        //    pera2.Content = null;
        //    skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
        //    skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;

        //    TreeView t = sender as TreeView;
        //    TipoviOpremeVM t1 = t.SelectedItem as TipoviOpremeVM;
        //    ListaFiltera = null;
        //    if (t1 != null)
        //    {

        //        TipoviOpreme t3 = t1.Model as TipoviOpreme;
        //        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
        //        SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(t3.IdTipOpreme);
        //        this.popuniListuOprema(niz);
        //        CurrentTipOpreme = t3.IdTipOpreme;




        //        this.popuniListuFiltera(t3.IdTipOpreme);
        //        this.popuniListuFiltera(t3.IdTipOpreme);

        //    }
        //    if (ListaFiltera != null)
        //    {
        //        if (ListaFiltera.Count > 0)
        //        {
        //            //for (int i = 0; i < ListaFiltera.Count; i++)
        //            //{
        //            //    if (ListaFiltera[i].KolekcijaVrednostiZaFilter.Count > 0)
        //            //    {

        //            //    }
        //            //}
        //            grdFilteri.Visibility = Visibility.Visible;
        //            grdFilteri.Height = 120;
        //        }
        //        else
        //        {
        //            grdFilteri.Visibility = Visibility.Hidden;
        //            grdFilteri.Height = 0;
        //        }
        //    }
        //    else
        //    {
        //        grdFilteri.Visibility = Visibility.Hidden;
        //        grdFilteri.Height = 0;
        //    }
        //}

        private ObservableCollection<Parametri> listaFiltera = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaFiltera
        {
            get { return listaFiltera; }
            set { SetAndNotify(ref listaFiltera, value); }
        }


        private void popuniListuFiltera(int idTipOpreme)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizFilteri = service.PrikaziFiltereGlavniProzor(idTipOpreme);
            this.ListaFiltera = new ObservableCollection<Parametri>();
            foreach (var item in nizFilteri)
            {
                if (item.ListaVrednostiZaFiltere.Length > 0)
                {
                    Parametri p = new Parametri(null)
                    {
                        DefaultVrednost = item.default_vrednost,
                        IdParametri = item.id_parametri,
                        IdTipOpreme = item.id_tip_opreme,
                        Name = item.naziv_parametra,
                        VrednostParametra = item.vrednost_parametra
                    };
                    for (int i = 0; i < item.ListaVrednostiZaFiltere.Length; i++)
                    {
                        if (item.ListaVrednostiZaFiltere[i] != null)
                        {

                            p.KolekcijaVrednostiZaFilter.Add(new VrednostiFiltera()
                            {
                                VrednostFiltera = item.ListaVrednostiZaFiltere[i],
                                idVrednostiFiltera = i + 1,
                                OdabranaVrednostZaFiltere = false,
                                idParametra = item.id_parametri
                            });

                        }
                    }

                    if (p.KolekcijaVrednostiZaFilter.Count > 0)
                    {
                        listaFiltera.Add(p);
                    }
                }

            }
            filteriPrikaz.ItemsSource = listaFiltera;
        }

        private void popuniListuOprema(DbItemOpremaSaParametrima[] nizOpremaSaParametrima)
        {
            List<Oprema> Listaa = new List<Oprema>();
            for (int i = 0; i < nizOpremaSaParametrima.Length; i++)
            {

                Oprema opremaZaListu = new Oprema(null)
                {

                    Cena = nizOpremaSaParametrima[i].cena,
                    IdOprema = nizOpremaSaParametrima[i].id_oprema,
                    IdTipOpreme = nizOpremaSaParametrima[i].id_tip_opreme,
                    KolicinaNaLageru = nizOpremaSaParametrima[i].kolicina_na_lageru,
                    KolicinaNaLageruZaRezervisanje = nizOpremaSaParametrima[i].kolicina_na_lageru,
                    KolicinaURezervi = nizOpremaSaParametrima[i].kolicina_u_rezervi,
                    Lager = nizOpremaSaParametrima[i].lager,
                    Model = nizOpremaSaParametrima[i].model,
                    Name = nizOpremaSaParametrima[i].naslov,
                    Opis = nizOpremaSaParametrima[i].opis,
                    OpremaNaPopustu = nizOpremaSaParametrima[i].oprema_na_popustu,
                    Proizvodjac = nizOpremaSaParametrima[i].proizvodjac,
                    Picture = nizOpremaSaParametrima[i].slika,
                    Slika = nizOpremaSaParametrima[i].slika,
                    SlikaOriginalPutanja = nizOpremaSaParametrima[i].slikaOriginalPutanja,
                    IzabranaKolicina = 1,
                    IzabranaKolicinaZaRezervisanje = 1,
                    IzabranaKolicinaZaRezervisanjeGlavniProzor = 1,
                    DaliMozeJosDaseDoda = nizOpremaSaParametrima[i].kolicina_na_lageru > 0,


                };

                if (!File.Exists(nizOpremaSaParametrima[i].slika))
                {
                    opremaZaListu.Picture = App.PutanjaDoSlikeNoImage;
                    opremaZaListu.Slika = App.PutanjaDoSlikeNoImage;
                }



                if (GlavniProzor.listaTrenutnihRezervacija != null && GlavniProzor.listaTrenutnihRezervacija.Count > 0)
                {
                    for (int j = 0; j < GlavniProzor.listaTrenutnihRezervacija.Count; j++)
                    {


                        if (nizOpremaSaParametrima[i].id_oprema == GlavniProzor.listaTrenutnihRezervacija[j].IdOprema)
                        {
                            if (nizOpremaSaParametrima[i].kolicina_na_lageru > 0)
                            {
                                opremaZaListu.DaliMozeJosDaseDodaURezervacije = GlavniProzor.listaTrenutnihRezervacija[j].KolicinaNaLageruZaRezervisanje > 0;
                                if (this.CurrentOprema != null)
                                {
                                    //if ((TmpEditObj as Oprema).IdOprema == nizOpremaSaParametrima[i].id_oprema)
                                    //{
                                    //    (TmpEditObj as Oprema).DaliMozeJosDaseDodaURezervacije = GlavniProzor.listaTrenutnihRezervacija[j].KolicinaNaLageruZaRezervisanje > 0;
                                    //    if (!(TmpEditObj as Oprema).DaliMozeJosDaseDodaURezervacije) (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                                    //    else (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
                                    //}
                                }


                                break;
                            }
                            else
                            {
                                opremaZaListu.DaliMozeJosDaseDodaURezervacije = false;
                                if (CurrentOprema != null)
                                {
                                    //if ((TmpEditObj as Oprema).IdOprema == nizOpremaSaParametrima[i].id_oprema)
                                    //{
                                    //    (TmpEditObj as Oprema).DaliMozeJosDaseDodaURezervacije = nizOpremaSaParametrima[i].kolicina_na_lageru > 0;
                                    //    (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                                    //}
                                }

                                break;
                            }
                        }


                        else
                        {
                            if (j == (GlavniProzor.listaTrenutnihRezervacija.Count - 1))
                            {
                                opremaZaListu.DaliMozeJosDaseDodaURezervacije = nizOpremaSaParametrima[i].kolicina_na_lageru > 0;
                                break;
                            }
                            else continue;


                            //if (TmpEditObj != null)
                            //{
                            //if ((TmpEditObj as Oprema).IdOprema == nizOpremaSaParametrima[i].id_oprema)
                            //{
                            //    (TmpEditObj as Oprema).DaliMozeJosDaseDodaURezervacije = nizOpremaSaParametrima[i].kolicina_na_lageru > 0;
                            //    (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                            //}
                            //}


                        }
                    }
                }
                else
                {
                    opremaZaListu.DaliMozeJosDaseDodaURezervacije = nizOpremaSaParametrima[i].kolicina_na_lageru > 0;
                    if (CurrentOprema != null)
                    {
                        //if ((TmpEditObj as Oprema).IdOprema == nizOpremaSaParametrima[i].id_oprema)
                        //{
                        //    (TmpEditObj as Oprema).DaliMozeJosDaseDodaURezervacije = nizOpremaSaParametrima[i].kolicina_na_lageru > 0;
                        //    (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                        //}
                    }


                }
                Listaa.Add(opremaZaListu);
                for (int j = 0; j < nizOpremaSaParametrima[i].ListaParametara.Length; j++)
                {
                    Parametri p = new Parametri(null)
                    {
                        DefaultVrednost = nizOpremaSaParametrima[i].ListaParametara[j].default_vrednost,
                        IdParametri = nizOpremaSaParametrima[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = nizOpremaSaParametrima[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra == null
                        || nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra == "" ?
                        KlasaKonstante.NEMA :
                        nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra,
                        Name = nizOpremaSaParametrima[i].ListaParametara[j].naziv_parametra
                    };

                    if (p.VrednostParametra.ToUpper() == "TRUE")
                    {
                        p.VrednostParametra = KlasaKonstante.IMA;
                    }
                    else if (p.VrednostParametra.ToUpper() == "FALSE")
                    {
                        p.VrednostParametra = KlasaKonstante.NEMA;
                    }

                    Listaa[i].ListaParametara.Add(p);
                }

            }
            this.documentManagerVM.Lista = Listaa;
            for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
            {
                if (CurrentOprema != null)
                {
                    if ((CurrentOprema as Oprema).IdOprema == this.documentManagerVM.Lista[i].IdOprema)
                    {
                        (CurrentOprema as Oprema).DaliMozeJosDaseDodaURezervacije = this.documentManagerVM.Lista[i].DaliMozeJosDaseDodaURezervacije;
                        (CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
                        break;
                    }
                }
            }
            rasporediListuOpremePravilno();
            //if (this.CurrentOprema != null && this.trenutniBorderOprema != null)
            //{
            //    int trenutniId = Convert.ToInt32((trenutniBorderOprema.FindName("skrivenId") as TextBlock).Text);

                
    
  

                
            //    //ItemsPresenter ips = brd.Child as ItemsPresenter;
            //    //var proba = FindVisualChild<ItemsControl>(ips);
            //    if (trenutniId == this.CurrentOprema.IdOprema)
            //    {
                    
            //            //Rectangle rctSenkaZaBrdOprema = trenutniGridOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //            //rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

            //        Border brdOpremaNaziv = trenutniBorderOprema.FindName("brdOpremaNaziv") as Border;
            //            brdOpremaNaziv.Background = Brushes.Transparent;
            //            TextBlock tblckOpremaNaziv = brdOpremaNaziv.FindName("tblckOpremaNaziv") as TextBlock;
            //            tblckOpremaNaziv.Foreground = Brushes.Black;
                    

                    
            //    }
            //}

            if(this.documentManagerVM.Lista != null && this.documentManagerVM.Lista.Count == 0)
            {
                CurrentOprema = null;
                pera2.Content = null;
                skrolVjuverEdit.Visibility = Visibility.Hidden;
                //skrolVjuverPrikaz.Visibility = Visibility.Hidden;
                
            }

            if(GlavniProzor.listaTrenutnihRezervacija.Count > 0)
            {
                for (int i = 0; i < GlavniProzor.listaTrenutnihRezervacija.Count; i++)
                {
                    for (int j = 0; j < this.documentManagerVM.Lista.Count; j++)
                    {
                        if ((GlavniProzor.listaTrenutnihRezervacija[i] as Oprema).IdOprema == (this.documentManagerVM.Lista[j] as Oprema).IdOprema)
                        {
                            (this.documentManagerVM.Lista[j] as Oprema).KolicinaNaLageruZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija[i].KolicinaNaLageruZaRezervisanje;
                            this.documentManagerVM.Lista[j].IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
                        }
                    }


                    
                }
            }
        }

        public static DependencyObject FindChild(DependencyObject parent, string name)
        {
            // confirm parent and name are valid.
            if (parent == null || string.IsNullOrEmpty(name)) return null;

            if (parent is FrameworkElement && (parent as FrameworkElement).Name == name) return parent;

            DependencyObject result = null;

            if (parent is FrameworkElement) (parent as FrameworkElement).ApplyTemplate();

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                result = FindChild(child, name);
                if (result != null) break;
            }

            return result;
        }

        /// <summary>
        /// Looks for a child control within a parent by type
        /// </summary>
        public static T FindChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            // confirm parent is valid.
            if (parent == null) return null;
            if (parent is T) return parent as T;

            DependencyObject foundChild = null;

            if (parent is FrameworkElement) (parent as FrameworkElement).ApplyTemplate();

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                foundChild = FindChild<T>(child);
                if (foundChild != null) break;
            }

            return foundChild as T;
        }
        decimal staraSirinaDoka, novaSirinaDokaOprema = 0;
        int brojKolonaKojiMozeDaStaneUStariiDok, brojKolonaKojiMozeDaStaneUNoviDok  = 0;
        decimal sirinaJednogObjektaOpreme = 280;
        bool poslatoOdPromeneVelicineProzora = false;
        private void rasporediListuOpremePravilno()
        {

            bool daLiJeIstaLista = true;
            if (!poslatoOdPromeneVelicineProzora)
            {
                novaSirinaDokaOprema = Convert.ToDecimal(brdDockOprema.ActualWidth);
                decimal decimalniPrikazNoveSirineDokaOprema = 0;
                decimal decimalniPrikazStareSirineDokaOprema = 0;

                decimal daLiSeUvecavaDokIliSeSmanjuje = 0;
                if (novaSirinaDokaOprema > staraSirinaDoka)
                    daLiSeUvecavaDokIliSeSmanjuje = 1;
                else
                    daLiSeUvecavaDokIliSeSmanjuje = -1;

                if (daLiSeUvecavaDokIliSeSmanjuje != 0)
                {
                    if (staraSirinaDoka > sirinaJednogObjektaOpreme)
                    {
                        //if (brojKolonaKojiMozeDaStaneUStariiDok + daLiSeUvecavaDokIliSeSmanjuje == 0)
                        //{
                        //    decimalniPrikazStareSirineDokaOprema = 0;
                        //    decimalniPrikazStareSirineDokaOprema = Math.Round(staraSirinaDoka / sirinaJednogObjektaOpreme,2);
                        //    //brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor((staraSirinaDoka / (sirinaJednogObjektaOpreme + (2 * 15)))));
                        //}
                        //else
                        //{
                        decimalniPrikazStareSirineDokaOprema = 0;
                        decimalniPrikazStareSirineDokaOprema = Math.Round(staraSirinaDoka / sirinaJednogObjektaOpreme, 2);
                        //}
                        if (decimalniPrikazStareSirineDokaOprema != 0)
                        {
                            brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor((decimalniPrikazStareSirineDokaOprema)));
                        }
                        else
                            brojKolonaKojiMozeDaStaneUStariiDok = 1;
                    }
                    else
                    {
                        brojKolonaKojiMozeDaStaneUStariiDok = 1;
                    }
                    if (novaSirinaDokaOprema > sirinaJednogObjektaOpreme)
                    {
                        //if (brojKolonaKojiMozeDaStaneUNoviDok + daLiSeUvecavaDokIliSeSmanjuje == 0)
                        //{
                        //    decimalniPrikazNoveSirineDokaOprema = 0;
                        //    decimalniPrikazNoveSirineDokaOprema = Math.Round(novaSirinaDokaOprema / sirinaJednogObjektaOpreme, 2);
                        //    //brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor(((novaSirinaDokaOprema / (sirinaJednogObjektaOpreme + (2 * 15))))));
                        //}
                        //else
                        //{
                        decimalniPrikazNoveSirineDokaOprema = 0;
                        decimalniPrikazNoveSirineDokaOprema = Math.Round(novaSirinaDokaOprema / sirinaJednogObjektaOpreme, 2);
                        //}
                        if (decimalniPrikazNoveSirineDokaOprema != 0)
                        {
                            brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor((decimalniPrikazNoveSirineDokaOprema)));
                        }
                        else
                            brojKolonaKojiMozeDaStaneUNoviDok = 1;
                    }
                }

                poslatoOdPromeneVelicineProzora = false;
            }
             
            if (brojKolonaKojiMozeDaStaneUNoviDok == 0) brojKolonaKojiMozeDaStaneUNoviDok = 1;
            if ((brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok) || (this.StariTipOpreme != this.CurrentTipOpreme) || TrenutniParametarZaFilterChecked != null || TrenutniParametarZaFilterUnchecked != null || izmenjenaOprema || aktivnaPretraga)
            {

                //if ((novaSirinaDokaOprema > (sirinaJednogObjektaOpreme * brojKolonaKojiMozeDaStaneUNoviDok) + (15 * brojKolonaKojiMozeDaStaneUNoviDok)) || (novaSirinaDokaOprema < 270))
                //{
                

                    if (this.documentManagerVM.Lista != null)
                    {

                        
                        int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
                        double razlomak = brojProizvodaUListiOpreme / (double)brojKolonaKojiMozeDaStaneUNoviDok;
                        int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

                        int brojac = 0;
                        bool prekinuto = false;

                        List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();
                        

                        
                        for (int i = 0; i < brojNovihLista; i++)
                        {
                            List<Oprema> novaLista = new List<Oprema>();

                            for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
                            {
                                if (j % brojKolonaKojiMozeDaStaneUNoviDok == 0 && j != 0 && !prekinuto)
                                {
                                    prekinuto = true;
                                    break;
                                }
                                novaLista.Add(this.documentManagerVM.Lista[j]);
                                brojac++;
                                prekinuto = false;

                            }
                            NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
                            novaListaZaListuSvihLista.NovaLista = novaLista;
                            listaSvihLista.Add(novaListaZaListuSvihLista);
                        }
                        if (listaSvihLista.Count != this.documentManagerVM.ListaSvihLista.Count)
                            daLiJeIstaLista = false;
                        else
                        {
                            for (int i = 0; i < listaSvihLista.Count; i++)
                            {
                                if (listaSvihLista[i].NovaLista.Count != this.documentManagerVM.ListaSvihLista[i].NovaLista.Count)
                                {
                                    daLiJeIstaLista = false;
                                    break;
                                }
                                else
                                {
                                    for (int j = 0; j < listaSvihLista[i].NovaLista.Count; j++)
                                    {
                                        if (listaSvihLista[i].NovaLista[j].IdOprema != this.documentManagerVM.ListaSvihLista[i].NovaLista[j].IdOprema)
                                        {
                                            daLiJeIstaLista = false;
                                            break;
                                        }
                                    }
                                    if (!daLiJeIstaLista)
                                        break;
                                }
                            }
                        }
                        if (!daLiJeIstaLista || izmenjenaOprema || aktivnaPretraga)
                            this.documentManagerVM.ListaSvihLista = listaSvihLista;
                        
                    }
                    
                }
            aktivirajDetaljanPrikaz();
                //else
                //{
                //    brojKolonaKojiMozeDaStaneUNoviDok--;
                //    if (brojKolonaKojiMozeDaStaneUNoviDok == 0) brojKolonaKojiMozeDaStaneUNoviDok = 1;
                //    if ((brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok) || (this.StariTipOpreme != this.CurrentTipOpreme))
                //    {

                //        if ((novaSirinaDokaOprema > (sirinaJednogObjektaOpreme * brojKolonaKojiMozeDaStaneUNoviDok) + (15 * brojKolonaKojiMozeDaStaneUNoviDok) + 5) || (novaSirinaDokaOprema < 270))
                //        {
                //            if (this.documentManagerVM.Lista != null)
                //            {
                //                int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
                //                double razlomak = brojProizvodaUListiOpreme / (double)brojKolonaKojiMozeDaStaneUNoviDok;
                //                int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

                //                int brojac = 0;
                //                bool prekinuto = false;

                //                List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

                //                for (int i = 0; i < brojNovihLista; i++)
                //                {
                //                    List<Oprema> novaLista = new List<Oprema>();

                //                    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
                //                    {
                //                        if (j % brojKolonaKojiMozeDaStaneUNoviDok == 0 && j != 0 && !prekinuto)
                //                        {
                //                            prekinuto = true;
                //                            break;
                //                        }
                //                        novaLista.Add(this.documentManagerVM.Lista[j]);
                //                        brojac++;
                //                        prekinuto = false;

                //                    }
                //                    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
                //                    novaListaZaListuSvihLista.NovaLista = novaLista;
                //                    listaSvihLista.Add(novaListaZaListuSvihLista);
                //                }
                //                this.documentManagerVM.ListaSvihLista = listaSvihLista;
                //            }
                //        }
                //        staraSirinaDoka = novaSirinaDokaOprema;
                //    }
                //}


                staraSirinaDoka = novaSirinaDokaOprema;

            
            //}
           


            //if (brdDockOprema.ActualWidth < 575)
            //{
            //    int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //    double razlomak = brojProizvodaUListiOpreme / (double)1;
            //    int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //    int brojac = 0;
            //    bool prekinuto = false;

            //    List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //    for (int i = 0; i < brojNovihLista; i++)
            //    {
            //        List<Oprema> novaLista = new List<Oprema>();

            //        for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //        {
            //            if (j % 1 == 0 && j != 0 && !prekinuto)
            //            {
            //                prekinuto = true;
            //                break;
            //            }
            //            novaLista.Add(this.documentManagerVM.Lista[j]);
            //            brojac++;
            //            prekinuto = false;

            //        }
            //        NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //        novaListaZaListuSvihLista.NovaLista = novaLista;
            //        listaSvihLista.Add(novaListaZaListuSvihLista);
            //    }
            //    this.documentManagerVM.ListaSvihLista = listaSvihLista;
                
            //}
            //else if (brdDockOprema.ActualWidth > 575 && brdDockOprema.ActualWidth < 860)
            //{
            //    int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //    double razlomak = brojProizvodaUListiOpreme / (double)2;
            //    int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //    int brojac = 0;
            //    bool prekinuto = false;

            //    List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //    for (int i = 0; i < brojNovihLista; i++)
            //    {
            //        List<Oprema> novaLista = new List<Oprema>();

            //        for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //        {
            //            if (j % 2 == 0 && j != 0 && !prekinuto)
            //            {
            //                prekinuto = true;
            //                break;
            //            }
            //            novaLista.Add(this.documentManagerVM.Lista[j]);
            //            brojac++;
            //            prekinuto = false;

            //        }
            //        NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //        novaListaZaListuSvihLista.NovaLista = novaLista;
            //        listaSvihLista.Add(novaListaZaListuSvihLista);
            //    }
            //    this.documentManagerVM.ListaSvihLista = listaSvihLista;


            //}
            //else if (brdDockOprema.ActualWidth > 860 && brdDockOprema.ActualWidth < 1145)
            //{
            //    int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //    double razlomak = brojProizvodaUListiOpreme / (double)3;
            //    int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //    int brojac = 0;
            //    bool prekinuto = false;

            //    List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //    for (int i = 0; i < brojNovihLista; i++)
            //    {
            //        List<Oprema> novaLista = new List<Oprema>();

            //        for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //        {
            //            if (j % 3 == 0 && j != 0 && !prekinuto)
            //            {
            //                prekinuto = true;
            //                break;
            //            }
            //            novaLista.Add(this.documentManagerVM.Lista[j]);
            //            brojac++;
            //            prekinuto = false;

            //        }
            //        NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //        novaListaZaListuSvihLista.NovaLista = novaLista;
            //        listaSvihLista.Add(novaListaZaListuSvihLista);
            //    }
            //    this.documentManagerVM.ListaSvihLista = listaSvihLista;
            //}
            //else if (brdDockOprema.ActualWidth > 1145)
            //{
            //    int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //    double razlomak = brojProizvodaUListiOpreme / (double)4;
            //    int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //    int brojac = 0;
            //    bool prekinuto = false;

            //    List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //    for (int i = 0; i < brojNovihLista; i++)
            //    {
            //        List<Oprema> novaLista = new List<Oprema>();

            //        for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //        {
            //            if (j % 4 == 0 && j != 0 && !prekinuto)
            //            {
            //                prekinuto = true;
            //                break;
            //            }
            //            novaLista.Add(this.documentManagerVM.Lista[j]);
            //            brojac++;
            //            prekinuto = false;

            //        }
            //        NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //        novaListaZaListuSvihLista.NovaLista = novaLista;
            //        listaSvihLista.Add(novaListaZaListuSvihLista);
            //    }
            //    this.documentManagerVM.ListaSvihLista = listaSvihLista;
            //}


                if (ictOpremaRedovi.Items.Count > 0 && (!daLiJeIstaLista || izmenjenaOprema || aktivnaPretraga || TrenutniParametarZaFilterChecked != null || TrenutniParametarZaFilterUnchecked != null))
            {
                if (!timer2.IsEnabled)
                {
                    timer2 = new DispatcherTimer();
                    timer2.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    timer2.Tick += timer2_Tick;
                    //daLiJePosecenTimer = false;
                    timer2.Start();

                }
            }
                else if (aktivnaPretraga || TrenutniParametarZaFilterChecked != null || TrenutniParametarZaFilterUnchecked != null)
                {

                    if (aktivnaPretraga)
                    {

                        LejautDokumentOprema.Title = "Rezultati pretrage".ToUpper();
                        resetujSvaPoljaISvojstva();

                        //DoubleAnimation daPrikaziPraznuOpremu = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        //brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daPrikaziPraznuOpremu);

                        //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(1, 0, new TimeSpan(0, 0, 0, 0, 500));
                        //skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daSakrijIctRedove);
                        aktivnaPretraga = false;
                    }
                    else if (TrenutniParametarZaFilterChecked != null || TrenutniParametarZaFilterUnchecked != null)
                    {
                        this.trenutniBorderOprema = null;
                        this.CurrentOprema = null;
                        aktivirajDetaljanPrikaz();

                        this.documentManagerVM.TekstPretrage = tbPretraga.Text;
                        //DoubleAnimation daPrikaziPraznuOpremuZbogOdabranihFiltera = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);


                        //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                        //this.CurrentOprema = null;

                        //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                        TrenutniParametarZaFilterChecked = TrenutniParametarZaFilterUnchecked = null;
                        
                    }


                    
                }
                

               
            
        }

        //private Grid trenutniGridOprema;
        private Border trenutniBorderOprema;

        //private void element_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    pera2.Visibility = Visibility.Visible;
        //    Border b = sender as Border;
        //    //MessageBox.Show(s.Children[0].ToString());

        //    //MessageBox.Show(g.FindName("skrivenId").GetType().ToString());
        //    TextBlock t = b.FindName("skrivenId") as TextBlock;
        //    //MessageBox.Show(t.Text);

        //    List<Oprema> o = new List<Oprema>();
        //    for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
        //    {
        //        if (this.documentManagerVM.Lista[i].IdOprema == Int32.Parse(t.Text))
        //        {
        //            o.Add(this.documentManagerVM.Lista[i]);
        //            this.TmpEditObj = this.documentManagerVM.Lista[i];

                    
        //            int trenutniId = Convert.ToInt32((trenutniBorderOprema.FindName("skrivenId") as TextBlock).Text);
        //            if (this.CurrentOprema == null)
        //                this.CurrentOprema = new Oprema(null);
        //                if (this.documentManagerVM.Lista[i].IdOprema != this.CurrentOprema.IdOprema)
        //                {
        //                    this.CurrentOprema.DaLiJeKliknutoNaGrid = false;
        //                    if (this.trenutniBorderOprema != null)
        //                    {
        //                        //Rectangle rctSenkaZaBrdOprema = trenutniGridOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
        //                        //rctSenkaZaBrdOprema.Visibility = Visibility.Hidden;


        //                        Border brdOpremaNaziv = trenutniBorderOprema.FindName("brdOpremaNaziv") as Border;
        //                        brdOpremaNaziv.Background = Brushes.Gainsboro;
        //                        Label tblckOpremaNaziv = brdOpremaNaziv.FindName("tblckOpremaNaziv") as Label;
        //                        tblckOpremaNaziv.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
        //                    }

        //                    this.trenutniBorderOprema = b;
        //                }
                    
        //            this.CurrentOprema = this.documentManagerVM.Lista[i];
        //            this.CurrentOprema.DaLiJeKliknutoNaGrid = true;
        //            break;
        //        }
        //    }

        //    Oprema o1 = TmpEditObj as Oprema;
        //    o1.TmpKolicinaNaLageru = o1.KolicinaNaLageru;
        //    o1.Tmp2KolicinaNaLageru = o1.KolicinaNaLageru;
        //    if (GlavniProzor.listaTrenutnihRezervacija != null)
        //    {
        //        for (int i = 0; i < GlavniProzor.listaTrenutnihRezervacija.Count; i++)
        //        {
        //            if (GlavniProzor.listaTrenutnihRezervacija[i].IdOprema == (TmpEditObj as Oprema).IdOprema)
        //            {
        //                (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
        //                (TmpEditObj as Oprema).KolicinaNaLageruZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija[i].KolicinaNaLageruZaRezervisanje;

        //            }
        //        }
        //    }

        //    ////pera2Edit.Content = null;
        //    skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
        //    pera2.Content = null;

        //    if (dalisepozivabaza == true)
        //    {
        //        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
        //        SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.TmpEditObj as Oprema).IdTipOpreme);
        //        popuniListuOprema(rez);
        //        this.dalisepozivabaza = false;
        //    }

        //    pera2.Content = o;
        //    //pera2.Visibility = System.Windows.Visibility.Visible;
        //    //pera.Visibility = System.Windows.Visibility.Hidden;
        //    skrolVjuverPrikaz.Visibility = Visibility.Visible;

        //}

        private void btnIzmeniArtikal_Click(object sender, RoutedEventArgs e)
        {
            skrolVjuverEdit.Visibility = Visibility.Visible;
            skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;

            //documentManagerVM.TrenutnaOpremaZaMenjanje = new Oprema(null);
            //List<SmartSoftwareGlavnaOblast> t = new List<SmartSoftwareGlavnaOblast>();

            if (this.CurrentOprema != null)
            {

                for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                {
                    if (this.documentManagerVM.Lista[i].IdOprema == this.CurrentOprema.IdOprema)
                    {
                        this.CurrentOprema = null;
                        documentManagerVM.TrenutnaOpremaZaMenjanje = new Oprema(null)
                        {
                            Cena = this.documentManagerVM.Lista[i].Cena,
                            IdOprema = this.documentManagerVM.Lista[i].IdOprema,
                            Opis = this.documentManagerVM.Lista[i].Opis,
                            IdTipOpreme = this.documentManagerVM.Lista[i].IdTipOpreme,
                            KolicinaNaLageru = this.documentManagerVM.Lista[i].KolicinaNaLageru,
                            KolicinaURezervi = this.documentManagerVM.Lista[i].KolicinaURezervi,
                            Lager = this.documentManagerVM.Lista[i].Lager,
                            Picture = this.documentManagerVM.Lista[i].Slika,
                            Name = this.documentManagerVM.Lista[i].Name,
                            OpremaNaPopustu = this.documentManagerVM.Lista[i].OpremaNaPopustu,
                            Proizvodjac = this.documentManagerVM.Lista[i].Proizvodjac,
                            Slika = this.documentManagerVM.Lista[i].Slika,
                            SlikaOriginalPutanja = this.documentManagerVM.Lista[i].SlikaOriginalPutanja,
                            Model = this.documentManagerVM.Lista[i].Model

                        };
                        //documentManagerVM.TrenutnaOpremaZaMenjanje.ListaParametara = new ObservableCollection<Parametri>();
                        for (int j = 0; j < this.documentManagerVM.Lista[i].ListaParametara.Count; j++)
                        {
                            Parametri p = new Parametri(null)
                            {
                                DefaultVrednost = this.documentManagerVM.Lista[i].ListaParametara[j].DefaultVrednost,
                                IdParametri = this.documentManagerVM.Lista[i].ListaParametara[j].IdParametri,
                                IdTipOpreme = this.documentManagerVM.Lista[i].ListaParametara[j].IdTipOpreme,
                                VrednostParametra = this.documentManagerVM.Lista[i].ListaParametara[j].VrednostParametra == null
                                || this.documentManagerVM.Lista[i].ListaParametara[j].VrednostParametra == "" ?
                                KlasaKonstante.NEMA :
                                this.documentManagerVM.Lista[i].ListaParametara[j].VrednostParametra,
                                Name = this.documentManagerVM.Lista[i].ListaParametara[j].Name
                            };

                            if (p.VrednostParametra.ToUpper() == "TRUE")
                            {
                                p.VrednostParametra = KlasaKonstante.IMA;
                            }
                            else if (p.VrednostParametra.ToUpper() == "FALSE")
                            {
                                p.VrednostParametra = KlasaKonstante.NEMA;
                            }
                            documentManagerVM.TrenutnaOpremaZaMenjanje.ListaParametara.Add(p);
                           
                        }
                        

                        //foreach (var item in this.documentManagerVM.Lista[i].ListaParametara)
                        //{
                        //    parametri.Add(new DbItemParametri() { default_vrednost = item.DefaultVrednost, id_parametri = item.IdParametri, id_tip_opreme = item.IdTipOpreme, naziv_parametra = item.Name, vrednost_parametra = item.VrednostParametra });
                        //}
                        break;
                    }
                }

                pera2Edit.Content = null;

                pera2Edit.Content = documentManagerVM.TrenutnaOpremaZaMenjanje;
                
                //dalisepozivabaza = true;
            }
            //documentManagerVM.TrenutnaOpremaZaMenjanje = null;


        }
        
        private void btnIzmeniISacuvaj_Click(object sender, RoutedEventArgs e)
        {

            // Korisnik dijalogom zadaje fajl iz koga se čita dokument
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Presentation file (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            //if (ofd.ShowDialog() ?? false == true)
            //{
            //    MessageBox.Show(ofd.FileName);
            //    MessageBox.Show(ofd.SafeFileName);

            //    string a = (tmpEditObj as Oprema).SlikaOriginalPutanja;
            //    int pozicija = a.LastIndexOf("/");
            //    string putanjaPrefix = a.Substring(0, pozicija + 1);
            //    System.IO.FileInfo fileInfo =
            //           new System.IO.FileInfo(ofd.FileName);
            //    SmartSoftwareServiceInterfaceClient clientUpload =
            //            new SmartSoftwareServiceInterfaceClient();
            //    SmartSoftwareServiceReference.RemoteFileInfo
            //           uploadRequestInfo = new RemoteFileInfo();

            //    using (System.IO.FileStream stream =
            //           new System.IO.FileStream(ofd.FileName,
            //           System.IO.FileMode.Open, System.IO.FileAccess.Read))
            //    {
            //        uploadRequestInfo.FileName = ofd.SafeFileName;
            //        uploadRequestInfo.Length = fileInfo.Length;
            //        uploadRequestInfo.FileByteStream = stream;
            //        clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);

            //        //clientUpload.UploadFile(stream);

            //        string zika = putanjaPrefix + ofd.SafeFileName;
            //        (tmpEditObj as Oprema).SlikaOriginalPutanja = zika;

            //    }
            //}

            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.documentManagerVM.TrenutnaOpremaZaMenjanje != null && this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika != null)
            {
                pozicija = this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika.LastIndexOf("\\");
                nazivSlike = this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\oprema\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika = putanjaPrefix + nazivSlike;
                }


            
                Oprema o = this.documentManagerVM.TrenutnaOpremaZaMenjanje as Oprema;
                DbItemOpremaSaParametrima oprema = new DbItemOpremaSaParametrima()
                {
                    cena = o.Cena,
                    id_oprema = o.IdOprema,
                    opis = o.Opis,
                    id_tip_opreme = o.IdTipOpreme,
                    kolicina_na_lageru = o.KolicinaNaLageru,
                    kolicina_u_rezervi = o.KolicinaURezervi,
                    lager = o.Lager,

                    naslov = o.Name,
                    oprema_na_popustu = o.OpremaNaPopustu,
                    proizvodjac = o.Proizvodjac,
                    slika = o.Slika,
                    slikaOriginalPutanja = o.SlikaOriginalPutanja,
                    model = o.Model

                };

                List<DbItemParametri> parametri = new List<DbItemParametri>();


                foreach (var item in o.ListaParametara)
                {
                    parametri.Add(new DbItemParametri() { default_vrednost = item.DefaultVrednost, id_parametri = item.IdParametri, id_tip_opreme = item.IdTipOpreme, naziv_parametra = item.Name, vrednost_parametra = item.VrednostParametra });
                }

                oprema.ListaParametara = parametri.ToArray();
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                DbItemOpremaSaParametrima[] nizOpremaSaParametrima = service.UpdateOpreme(oprema);
                izmenjenaOprema = true;
                //this.popuniListuOprema(nizOpremaSaParametrima);


                skrolVjuverEdit.Visibility = System.Windows.Visibility.Hidden;
                skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Visible;
                // pera2.ItemsSource = null;
                //pera
                //List<Oprema> ponovniPrikazObjekta = new List<Oprema>();
                //int idOprema = (this.CurrentOprema as Oprema).IdOprema;
                //Oprema tmpO = null;
                //foreach (var item in this.documentManagerVM.Lista)
                //{
                //    if (item.IdOprema == idOprema)
                //    {
                //        tmpO = item;
                //        break;
                //    }
                //}
                this.CurrentOprema = documentManagerVM.TrenutnaOpremaZaMenjanje;
                if (this.CurrentOprema != null)
                {
                    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(this.CurrentOprema.IdTipOpreme);
                    if (niz != null && niz.Length > 0)
                    {
                        this.popuniListuOprema(niz);
                        for (int i = 0; i < niz.Length; i++)
                        {
                            if(this.CurrentOprema.IdOprema == niz[i].id_oprema)
                            {
                                if (!File.Exists(niz[i].slika))
                                    this.CurrentOprema.Slika = App.PutanjaDoSlikeNoImage;
                                else
                                    this.CurrentOprema.Slika = niz[i].slika;
                            }
                        }
                    }
                }
                //ponovniPrikazObjekta.Add(tmpO);
                pera2.Content = this.CurrentOprema;


                //if (dalisepozivabaza == true)
                //{
                //    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.CurrentOprema as Oprema).IdTipOpreme);
                //    popuniListuOprema(rez);
                //    this.dalisepozivabaza = false;
                //}
                //ControlTemplate ctpOdabirOpreme = ctlOdabirOpreme.Template;

                //ItemsControl pera = ctpOdabirOpreme.FindName("pera", ctlOdabirOpreme) as ItemsControl;
                //pera.ItemsSource = null;
                //pera.ItemsSource = this.documentManagerVM.Lista;
            }
        }

        private void btnOtkaziIzmene_Click(object sender, RoutedEventArgs e)
        {
            skrolVjuverEdit.Visibility = Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = Visibility.Visible;


            Grid grdProba = FindChild<Grid>(pera2);
            TextBlock tbProba = grdProba.FindName("skrivenId") as TextBlock;
            int id = Convert.ToInt32((tbProba).Text);

            for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
            {
                if (this.documentManagerVM.Lista[i].IdOprema == id)
                {
                    this.CurrentOprema = this.documentManagerVM.Lista[i];
                    break;
                }
            }

            

            pera2Edit.Content = null;
            pera2.Content = null;

            pera2.Content = this.CurrentOprema;
            //// pera2.ItemsSource = null;
            ////if (dalisepozivabaza == true)
            ////{
            ////    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            ////    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((this.TmpEditObj as Oprema).IdTipOpreme);
            ////    popuniListuOprema(niz);
            ////    this.dalisepozivabaza = false;
            ////}

            ////List<Oprema> ponovniPrikazObjekta = new List<Oprema>();
            ////int idOprema = (this.TmpEditObj as Oprema).IdOprema;
            ////Oprema tmpO = null;
            ////foreach (var item in this.documentManagerVM.Lista)
            ////{
            ////    if (item.IdOprema == idOprema)
            ////    {
            ////        tmpO = item;
            ////        break;
            ////    }
            ////}
            //////ovo si prosli put izmenio da bi radilo za text a za sliku ne radi i dalje
            ////this.TmpEditObj = tmpO;
            //////mozda je neka fora sto se slika menja za stari objekat
            ////ponovniPrikazObjekta.Add(tmpO);
            ////pera2.Content = ponovniPrikazObjekta;
            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //if (CurrentTipOpreme != null && CurrentTipOpreme.IdTipOpreme > 0)
            //{
            //    DbItemOpremaSaParametrima[] oprema = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme.IdTipOpreme);



            //    this.popuniListuOprema(oprema);
            //    if (GlavniProzor.listaTrenutnihRezervacija != null)
            //    {
            //        int duzinaTrenutneListeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;
            //        if (TmpEditObj != null && duzinaTrenutneListeZaRezervisanje > 0)
            //        {
            //            for (int i = 0; i < duzinaTrenutneListeZaRezervisanje; i++)
            //            {
            //                if ((TmpEditObj as Oprema).IdOprema == GlavniProzor.listaTrenutnihRezervacija[i].IdOprema)
            //                {
            //                    (TmpEditObj as Oprema).KolicinaNaLageruZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija[i].KolicinaNaLageru - GlavniProzor.listaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje;
            //                    break;
            //                }
            //            }



            //        }
            //        else if (duzinaTrenutneListeZaRezervisanje == 0)
            //        {
            //            if (TmpEditObj != null)
            //            {
            //                (TmpEditObj as Oprema).KolicinaNaLageruZaRezervisanje = (TmpEditObj as Oprema).KolicinaNaLageru;
            //                (TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
            //            }
            //        }

            //        for (int i = 0; i < duzinaTrenutneListeZaRezervisanje; i++)
            //        {
            //            for (int j = 0; j < this.documentManagerVM.Lista.Count; j++)
            //            {

            //                if (this.documentManagerVM.Lista[j].IdOprema == GlavniProzor.listaTrenutnihRezervacija[i].IdOprema)
            //                {
            //                    this.documentManagerVM.Lista[j].IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
            //                    this.documentManagerVM.Lista[j].KolicinaNaLageruZaRezervisanje = this.documentManagerVM.Lista[j].KolicinaNaLageru - GlavniProzor.listaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje;
            //                    break;
            //                }
            //            }
            //        }



            //    }
            //}
            //pera2.Content = null;
            ////skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;
            ////skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Visible;
            //if (this.documentManagerVM.Lista != null)
            //{
            //    for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
            //    {
            //        if (TmpEditObj != null)
            //        {
            //            if ((TmpEditObj as Oprema).IdOprema == this.documentManagerVM.Lista[i].IdOprema)
            //            {
            //                trenutno = this.documentManagerVM.Lista[i];
            //                TmpEditObj = this.documentManagerVM.Lista[i];
            //                break;
            //            }
            //        }
            //    }
            //    pera2.Content = trenutno as Oprema;

            //    this.documentManagerVM.BrojOpremeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;

            //    if (GlavniProzor.listaTrenutnihRezervacija.Count > 0)
            //    {
            //        RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacijeIma");
            //    }
            //    else
            //    {
            //        RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacije");
            //    }
            //}
        }

        private void popuniKorpu(DbItemOpremaSaParametrima[] oprema)
        {
            this.documentManagerVM.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

            for (int i = 0; i < oprema.Length; i++)
            {
                this.documentManagerVM.Korpa.Add(new Oprema(null)
                {
                    Cena = oprema[i].cena,
                    IdOprema = oprema[i].id_oprema,
                    IdTipOpreme = oprema[i].id_tip_opreme,
                    KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
                    KolicinaURezervi = oprema[i].kolicina_u_rezervi,
                    Lager = oprema[i].lager,
                    Model = oprema[i].model,
                    Name = oprema[i].naslov,
                    Opis = oprema[i].opis,
                    OpremaNaPopustu = oprema[i].oprema_na_popustu,
                    Proizvodjac = oprema[i].proizvodjac,
                    Slika = oprema[i].slika,
                    SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
                    IzabranaKolicina = oprema[i].kolicinaUKorpi

                });



                for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
                {
                    (this.documentManagerVM.Korpa[i] as Oprema).ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        Name = oprema[i].ListaParametara[j].naziv_parametra
                    });

                }
            }
        }


        private void btnProdajArtikal_Click(object sender, RoutedEventArgs e)
        {

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(this.documentManagerVM.TrenutniProdavac.IdKorisnici);
            this.popuniKorpu(oprema);
            Oprema op = this.CurrentOprema as Oprema;






            int brojac = 0;
            int duzina = this.documentManagerVM.Korpa.Count;
            for (brojac = 0; brojac < duzina; brojac++)
            {
                if ((this.documentManagerVM.Korpa[brojac] as Oprema).IdOprema == (this.CurrentOprema as Oprema).IdOprema)
                {
                    break;
                }
            }
            if (brojac == duzina)
            {
                //this.documentManagerVM.Korpa.Add(tmpEditObj);
                if ((this.CurrentOprema as Oprema).IzabranaKolicina <= (this.CurrentOprema as Oprema).KolicinaNaLageru)
                {

                    SmartSoftwareServiceReference.OperationObject rez = service.KorpaInsert((this.CurrentOprema as Oprema).IdOprema, (this.CurrentOprema as Oprema).IzabranaKolicina, this.documentManagerVM.TrenutniProdavac.IdKorisnici);
                }
            }
            else
            {
                //onda samo povecaj ili smanji broj 
                if ((this.CurrentOprema as Oprema).IzabranaKolicina + (documentManagerVM.Korpa[brojac] as Oprema).IzabranaKolicina <= (documentManagerVM.Korpa[brojac] as Oprema).KolicinaNaLageru)
                {
                    SmartSoftwareServiceReference.OperationObject rez = service.KorpaUpdate((this.CurrentOprema as Oprema).IdOprema, (this.CurrentOprema as Oprema).IzabranaKolicina + ((documentManagerVM.Korpa[brojac] as Oprema).IzabranaKolicina), this.documentManagerVM.TrenutniProdavac.IdKorisnici);
                }
            }



            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaSelect(this.documentManagerVM.TrenutniProdavac.IdKorisnici);
            this.popuniKorpu(opremaPonovo);




            //this.documentManagerVM.Rezervacije = this.documentManagerVM.Korpa;


            int id = (this.CurrentOprema as Oprema).IdOprema;

            if (this.CurrentOprema != null)
            {
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((this.CurrentOprema as Oprema).IdTipOpreme);
                this.popuniListuOprema(niz);
                List<Oprema> o = new List<Oprema>();
                for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                {
                    if (this.documentManagerVM.Lista[i].IdOprema == id)
                    {
                        o.Add(this.documentManagerVM.Lista[i]);
                    }
                }
                //ControlTemplate ctpOdabirOpreme = ctlOdabirOpreme.Template;

                //ItemsControl pera = ctpOdabirOpreme.FindName("pera", ctlOdabirOpreme) as ItemsControl;
                //pera.ItemsSource = null;
                //pera.ItemsSource = this.documentManagerVM.Lista;

                pera2.Content = null;
                pera2.Content = o;
                this.CurrentOprema = o[0];
            }

            if (this.documentManagerVM.Korpa.Count > 0)
            {
                korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpaIma");
            }
            else
            {
                korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpa");
            }


        }

        private void btnSkiniSaLageraArtikal_Click(object sender, RoutedEventArgs e)
        {
            string poruka = "Da li zaista želite da uklonite sa lagera ovu opremu?";
            MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
            MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
            MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

            switch (rezultatBoxa)
            {
                case MessageBoxResult.Yes:
                   SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                   if (this.CurrentOprema != null)
                   {
                       this.popuniListuOprema(service.UpdateOpremeSkiniOpremuSaLagera((this.CurrentOprema as Oprema).IdOprema));
                       (this.CurrentOprema as Oprema).KolicinaNaLageru = 0;
                       (this.CurrentOprema as Oprema).DaliMozeJosDaseDoda = false;
                   }
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }

        private void btnRezervisiArtikal_Click(object sender, RoutedEventArgs e)
        {
            int brojac = 0;
            int duzina = GlavniProzor.listaTrenutnihRezervacija.Count;
            for (brojac = 0; brojac < duzina; brojac++)
            {
                if ((GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IdOprema == (this.CurrentOprema as Oprema).IdOprema)
                {
                    break;
                }
            }
            if (brojac == duzina)
            {
                //this.documentManagerVM.Korpa.Add(tmpEditObj);
                if ((CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor <= (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje)
                {

                    GlavniProzor.listaTrenutnihRezervacija.Add(CurrentOprema as Oprema);

                    (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje = (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor;

                    (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje = (CurrentOprema as Oprema).KolicinaNaLageru - (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje;
                    (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).KolicinaNaLageruZaRezervisanje = (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje;

                    //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                    //DbItemOpremaSaParametrima[] nizOpreme = service.OpremeSaParametrimaGlavniProzor(0);
                    //this.popuniListuOprema(nizOpreme);
                    //pera2.Content = null;
                    //pera2.Content = TmpEditObj;

                }
                else if ((CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor == (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje)
                {
                    if (GlavniProzor.listaTrenutnihRezervacija != null)
                    {

                        if ((CurrentOprema as Oprema).IdOprema == GlavniProzor.listaTrenutnihRezervacija[brojac].IdOprema)
                        {
                            if ((CurrentOprema as Oprema).KolicinaNaLageru > 0)
                            {
                                (CurrentOprema as Oprema).DaliMozeJosDaseDodaURezervacije = GlavniProzor.listaTrenutnihRezervacija[brojac].KolicinaNaLageruZaRezervisanje > 0;
                            }
                            else
                            {
                                //(TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                                (CurrentOprema as Oprema).DaliMozeJosDaseDodaURezervacije = false;

                            }
                        }

                    }
                    else
                    {
                        //(TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                        (CurrentOprema as Oprema).DaliMozeJosDaseDodaURezervacije = false;

                    }
                }

            }
            else
            {
                //onda samo povecaj ili smanji broj 
                if ((CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor + (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje <= (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).KolicinaNaLageru)
                {
                    (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje = (CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor + (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje;

                    (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje = (CurrentOprema as Oprema).KolicinaNaLageru - (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).IzabranaKolicinaZaRezervisanje;
                    (GlavniProzor.listaTrenutnihRezervacija[brojac] as Oprema).KolicinaNaLageruZaRezervisanje = (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje;

                    if ((CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje == 0)
                    {
                        (CurrentOprema as Oprema).DaliMozeJosDaseDodaURezervacije = false;
                        //(TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 0;
                    }

                    (CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;

                }



            }
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DbItemOpremaSaParametrima[] nizOpreme = service.OpremeSaParametrimaGlavniProzor((CurrentOprema as Oprema).IdTipOpreme);
            this.popuniListuOprema(nizOpreme);
            pera2.Content = null;
            pera2.Content = CurrentOprema;
            this.documentManagerVM.BrojOpremeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;

            if (GlavniProzor.listaTrenutnihRezervacija.Count > 0)
            {
                RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacijeIma");
            }
            else
            {
                RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacije");
            }
            //if ((TmpEditObj as Oprema).IzabranaKolicinaZaRezervisanje <= (TmpEditObj as Oprema).KolicinaNaLageru)
            //{

            //    GlavniProzor.listaTrenutnihRezervacija.Add(TmpEditObj);
            //   (TmpEditObj as Oprema).TmpIzabranaKolicina = 1;


            //}



            //this.RezDodavanje = new TrenutnaListaRezervacija();
            //RezDodavanje.ShowDialog();


            //int brojac = 0;
            //int duzina = RezDodavanje.Rezervacije.Count;
            //for (brojac = 0; brojac < duzina; brojac++)
            //{
            //    if ((rezDodavanje.Rezervacije[brojac] as Oprema).IdOprema == (this.tmpEditObj as Oprema).IdOprema)
            //    {
            //        break;
            //    }
            //}
            //if (brojac == duzina)
            //{
            //    //this.documentManagerVM.Korpa.Add(tmpEditObj);
            //    if ((tmpEditObj as Oprema).IzabranaKolicina <= (tmpEditObj as Oprema).KolicinaNaLageru)
            //    {

            //        rezDodavanje.Rezervacije.Add(tmpEditObj);
            //        (tmpEditObj as Oprema).TmpIzabranaKolicina = 1;
            //    }
            //}
            //else
            //{
            //    (rezDodavanje.Rezervacije[brojac] as Oprema).TmpIzabranaKolicina += 1;
            //}


            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            //int brojac = 0;
            //int duzina = this.documentManagerVM.Rezervacije.Count;
            //for (brojac = 0; brojac < duzina; brojac++)
            //{
            //    if ((this.documentManagerVM.Rezervacije[brojac] as Oprema).IdOprema == (this.tmpEditObj as Oprema).IdOprema)
            //    {
            //        break;
            //    }
            //}
            //if (brojac == duzina)
            //{
            //    //this.documentManagerVM.Korpa.Add(tmpEditObj);
            //    if ((tmpEditObj as Oprema).IzabranaKolicina <= (tmpEditObj as Oprema).KolicinaNaLageru)
            //    {

            //        SmartSoftwareServiceReference.OperationObject rez = service.RezervacijeInsert((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina,"ime");
            //    }
            //}
            //else
            //{
            //    //onda samo povecaj ili smanji broj 
            //    if ((tmpEditObj as Oprema).IzabranaKolicina + (documentManagerVM.Rezervacije[brojac] as Oprema).IzabranaKolicina <= (documentManagerVM.Rezervacije[brojac] as Oprema).KolicinaNaLageru)
            //    {
            //        SmartSoftwareServiceReference.OperationObject rez = service.RezervacijeUpdate((tmpEditObj as Oprema).IdOprema, (tmpEditObj as Oprema).IzabranaKolicina + ((documentManagerVM.Rezervacije[brojac] as Oprema).IzabranaKolicina));
            //    }
            //}
            //Oprema op = TmpEditObj as Oprema;

            ////  op.IzabranaKolicina += op.TmpIzabranaKolicina;
            ////op.Tmp2KolicinaNaLageru = op.KolicinaNaLageru - op.IzabranaKolicina;
            ////op.TmpIzabranaKolicina = 1;
            ////op.DaliMozeJosDaseDoda = op.IzabranaKolicina != op.KolicinaNaLageru;
            ////if (!op.DaliMozeJosDaseDoda)
            ////{
            ////    op.IzabranaKolicina = 1;
            ////}

            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.RezervacijeSelect();



            //this.documentManagerVM.Rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

            //for (int i = 0; i < oprema.Length; i++)
            //{
            //    this.documentManagerVM.Rezervacije.Add(new Oprema(null)
            //    {
            //        Cena = oprema[i].cena,
            //        IdOprema = oprema[i].id_oprema,
            //        IdTipOpreme = oprema[i].id_tip_opreme,
            //        KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
            //        KolicinaURezervi = oprema[i].kolicina_u_rezervi,
            //        Lager = oprema[i].lager,
            //        Model = oprema[i].model,
            //        Name = oprema[i].naslov,
            //        Opis = oprema[i].opis,
            //        OpremaNaPopustu = oprema[i].oprema_na_popustu,
            //        Proizvodjac = oprema[i].proizvodjac,
            //        Slika = oprema[i].slika,
            //        SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
            //        IzabranaKolicina = oprema[i].kolicinaUKorpi

            //    });



            //    for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
            //    {
            //        (this.documentManagerVM.Rezervacije[i] as Oprema).ListaParametara.Add(new Parametri(null)

            //        {
            //            DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
            //            IdParametri = oprema[i].ListaParametara[j].id_parametri,
            //            IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
            //            VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
            //            Name = oprema[i].ListaParametara[j].naziv_parametra
            //        });

            //    }
            //}


            //this.documentManagerVM.Rezervacije = this.documentManagerVM.Rezervacije;


            //int id = (tmpEditObj as Oprema).IdOprema;

            //if (tmpEditObj != null)
            //{
            //    this.popuniListuOprema((tmpEditObj as Oprema).IdTipOpreme, null);
            //    List<Oprema> o = new List<Oprema>();
            //    for (int i = 0; i < this.Lista.Count; i++)
            //    {
            //        if (this.Lista[i].IdOprema == id)
            //        {
            //            o.Add(this.Lista[i]);
            //        }
            //    }

            //    pera.ItemsSource = null;
            //    pera.ItemsSource = this.Lista;

            //    pera2.ItemsSource = null;
            //    pera2.ItemsSource = o;
            //    tmpEditObj = o[0];
            //}
        }

        private void btnPopustArtikal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNaruciArtikal_Click(object sender, RoutedEventArgs e)
        {
            bool daLiJeNaruceno = false;

            IntegerUpDown intUpDownNarudzbina = (((sender as Button).Parent as Grid).FindName("intUpDownNarudzbina") as IntegerUpDown);

            DbItemNarudzbine narudzbina = new DbItemNarudzbine()
            {
                datum_narudzbine = DateTime.Now,
                id_oprema = this.CurrentOprema.IdOprema,
                id_prodavca = this.documentManagerVM.TrenutniProdavac.IdKorisnici,
                kolicina = Convert.ToInt32(intUpDownNarudzbina.Text),
            };


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            daLiJeNaruceno  = service.OpNarudzbineInsert(narudzbina);
            
        }

        private void korpaklik_MouseDown(object sender, RoutedEventArgs e)
        {

            DoubleAnimation daKorpaOtvoriRecNijeAktivanProzor = new DoubleAnimation(0, 0.5, new TimeSpan(0, 0, 0, 0, 500));
            recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daKorpaOtvoriRecNijeAktivanProzor);
            if (!timerOtvoriKorpu.IsEnabled)
            {
                timerOtvoriKorpu = new DispatcherTimer();
                timerOtvoriKorpu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                timerOtvoriKorpu.Tick += timerOtvoriKorpu_Tick;
                timerOtvoriKorpu.Start();

                recNijeAktivanProzor.Visibility = Visibility.Visible;
            }


            

        }

        void timerOtvoriKorpu_Tick(object sender, EventArgs e)
        {
            if (timerOtvoriKorpu.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();


                KorpaProzor prozor = new KorpaProzor(this.documentManagerVM.TrenutniProdavac);
                
                prozor.ShowDialog();
                
                this.documentManagerVM.Korpa = prozor.Korpa;
                this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());



                if (!timerZatvoriKorpu.IsEnabled)
                {
                    timerZatvoriKorpu = new DispatcherTimer();
                    timerZatvoriKorpu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    timerZatvoriKorpu.Tick += timerZatvoriKorpu_Tick;
                    timerZatvoriKorpu.Start();
                }
                DoubleAnimation daKorpaZatvoriRecNijeAktivanProzor = new DoubleAnimation(0.5, 0, new TimeSpan(0, 0, 0, 0, 500));
                recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daKorpaZatvoriRecNijeAktivanProzor);
                //ControlTemplate ctpOdabirOpreme = ctlOdabirOpreme.Template;

                //ItemsControl pera = ctpOdabirOpreme.FindName("pera", ctlOdabirOpreme) as ItemsControl;
                //pera.Items.Clear();
                

                
                
                

            }


        }

        void resetujSvaPoljaISvojstva()
        {
            skrolVjuverEdit.Visibility = Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;
            this.documentManagerVM.Lista = null;
            this.StariTipOpreme = null;
            this.CurrentTipOpreme = null;
            izmenjenaOprema = false;

            if (!prviPutUcitanoResetovanje)
            {
                brdListaSaOpremomJePraznaZaOdabraniTipOpreme.Visibility = Visibility.Visible;
                brdListaSaOpremomJePraznaZbogOdabranihFiltera.Visibility = Visibility.Visible;
                brdListaSaOpremomJePraznaZbogPretrage.Visibility = Visibility.Visible;
                skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;
                prviPutUcitanoResetovanje = true;
            }


            if (aktivnaPretraga)
            {
                this.documentManagerVM.TekstPretrage = tbPretraga.Text;

                //DoubleAnimation daPrikaziPraznuOpremuZbogOdabranihFiltera = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                //DoubleAnimation daPrikaziPraznuOpremuZbogPretrage = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                //DoubleAnimation daPrikaziPraznuOpremu = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

                brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
                brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                aktivnaPretraga = false;
            }
            else
            {
                brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
            }
            //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
            skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaSakrijPolaSekunde);
            //brdListaSaOpremomJePrazna.Visibility = Visibility.Visible;
            //ictOpremaRedovi.Visibility = Visibility.Hidden;
            

            trenutniBorderOprema = null;
            this.CurrentOprema = null;
            this.CurrentTipOpreme = null;
            this.selektovanRed = 1;

            if (TrenutniGridTriVjuTipoviOpreme != null)
            {
                Border brdTriVjuTipoviOpremeNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;
                TextBlock textblok = TrenutniGridTriVjuTipoviOpreme.FindName("textblok") as TextBlock;
                textblok.Foreground = Brushes.Black;
            }
            //skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Hidden;
            //skrolVjuverPrikaz.Visibility = Visibility.Hidden;
            //grdFilteri.Visibility = Visibility.Visible;
//DoubleAnimation daZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
            grdFilteri.BeginAnimation(Border.HeightProperty, daAnimacijaSakrijSekund);

            DoubleAnimation dabrdPrikazDetaljaOpremeSirinaNula = new DoubleAnimation(staraSirinabrdPrikazDetaljaOpreme, 0, new TimeSpan(0, 0, 0, 0, 500));
            brdPrikazDetaljaOpreme.BeginAnimation(Border.WidthProperty, dabrdPrikazDetaljaOpremeSirinaNula);
            staraSirinabrdPrikazDetaljaOpreme = 0;
            aktivanDetaljanPrikaz = false; 
        }

        void timerZatvoriKorpu_Tick(object sender, EventArgs e)
        {
            if (timerZatvoriKorpu.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();

                recNijeAktivanProzor.Visibility = Visibility.Hidden;
                pera2.Content = null;

                resetujSvaPoljaISvojstva();

                if (this.CurrentOprema != null)
                {
                    int id = (this.CurrentOprema as Oprema).IdOprema;

                    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrima((this.CurrentOprema as Oprema).IdTipOpreme);
                    this.popuniListuOprema(niz);
                    List<Oprema> o = new List<Oprema>();
                    for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                    {
                        if (this.documentManagerVM.Lista[i].IdOprema == id)
                        {
                            o.Add(this.documentManagerVM.Lista[i]);
                        }
                    }

                    //pera.ItemsSource = null;
                    //pera.ItemsSource = this.documentManagerVM.Lista;

                    pera2.Content = null;
                    pera2.Content = o;
                    this.CurrentOprema = o[0];
                }

                if (this.documentManagerVM.Korpa.Count > 0)
                {
                    korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpaIma");
                }
                else
                {
                    korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpa");
                }



            }

            timerOtvoriKorpu.Stop();
            timerZatvoriKorpu.Stop();
        }

        private void perakloz(object sender, EventArgs e)
        {
            this.documentManagerVM.Korpa = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(null, this.documentManagerVM.TrenutniProdavac.IdKorisnici);


            Application.Current.Shutdown();

        }
        SmartSoftwareGlavnaOblast trenutno;
        
        private bool slucajnoUProlazuOtvoriRezervaciju = false;
        private bool slucajnoUProlazuZatvoriRezervaciju = false;
        private void RezervacijeKlik_MouseDown(object sender, RoutedEventArgs e)
        {


            recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daSakrijGlavniProzor);
            if (!timerOtvoriRezervacije.IsEnabled)
            {
                timerOtvoriRezervacije = new DispatcherTimer();
                timerOtvoriRezervacije.Interval = new TimeSpan(0, 0, 0, 0, 500);
                timerOtvoriRezervacije.Tick += timerOtvoriRezervacije_Tick;
                timerOtvoriRezervacije.Start();
                slucajnoUProlazuOtvoriRezervaciju = true;
                recNijeAktivanProzor.Visibility = Visibility.Visible;
            }
            
            
            

            

        }
        
        
        void timerOtvoriRezervacije_Tick(object sender, EventArgs e)
        {
            if (slucajnoUProlazuOtvoriRezervaciju && timerOtvoriRezervacije.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();
                
                RezervacijePregledProzor rezPregledProzor = new RezervacijePregledProzor(this.documentManagerVM.TrenutniProdavac);

                rezPregledProzor.ShowDialog();
                GlavniProzor.listaTrenutnihRezervacija = rezPregledProzor.ListaTrenutnihRezervacija;
                daLiJeNestoPrebacenoUKorpu = rezPregledProzor.DaLiJeNestoPrebacenoUKorpu;
                if (!timerZatvoriRezervacije.IsEnabled)
                {
                    timerZatvoriRezervacije = new DispatcherTimer();
                    timerZatvoriRezervacije.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    timerZatvoriRezervacije.Tick += timerZatvoriRezervaciju_Tick;
                    timerZatvoriRezervacije.Start();
                    slucajnoUProlazuZatvoriRezervaciju = true;
                }
                //DoubleAnimation daRezervacijeZatvoriRecNijeAktivanProzor = new DoubleAnimation(0.5, 0, new TimeSpan(0, 0, 0, 0, 500));
                recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daPrikaziGlavniProzor);
                slucajnoUProlazuOtvoriRezervaciju = false;
                
            }
            else return;
        }

        private bool daLiJeNestoPrebacenoUKorpu = false;

        
        void timerZatvoriRezervaciju_Tick(object sender, EventArgs e)
        {
            if (slucajnoUProlazuZatvoriRezervaciju && timerZatvoriRezervacije.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();
                slucajnoUProlazuZatvoriRezervaciju = false;
                recNijeAktivanProzor.Visibility = Visibility.Hidden;

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                


                if (CurrentTipOpreme != null && CurrentTipOpreme.IdTipOpreme > 0)
                {
                    DbItemOpremaSaParametrima[] oprema = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme.IdTipOpreme);



                    this.popuniListuOprema(oprema);
                    if (GlavniProzor.listaTrenutnihRezervacija != null)
                    {
                        int duzinaTrenutneListeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;
                        if (CurrentOprema != null && duzinaTrenutneListeZaRezervisanje > 0)
                        {
                            for (int i = 0; i < duzinaTrenutneListeZaRezervisanje; i++)
                            {
                                if ((CurrentOprema as Oprema).IdOprema == GlavniProzor.listaTrenutnihRezervacija[i].IdOprema)
                                {
                                    (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija[i].KolicinaNaLageru - GlavniProzor.listaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje;
                                    break;
                                }
                            }



                        }
                        else if (duzinaTrenutneListeZaRezervisanje == 0)
                        {
                            if (CurrentOprema != null)
                            {
                                (CurrentOprema as Oprema).KolicinaNaLageruZaRezervisanje = (CurrentOprema as Oprema).KolicinaNaLageru;
                                (CurrentOprema as Oprema).IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
                            }
                        }

                        for (int i = 0; i < duzinaTrenutneListeZaRezervisanje; i++)
                        {
                            for (int j = 0; j < this.documentManagerVM.Lista.Count; j++)
                            {

                                if (this.documentManagerVM.Lista[j].IdOprema == GlavniProzor.listaTrenutnihRezervacija[i].IdOprema)
                                {
                                    this.documentManagerVM.Lista[j].IzabranaKolicinaZaRezervisanjeGlavniProzor = 1;
                                    this.documentManagerVM.Lista[j].KolicinaNaLageruZaRezervisanje = this.documentManagerVM.Lista[j].KolicinaNaLageru - GlavniProzor.listaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje;
                                    break;
                                }
                            }
                        }



                    }
                }
                pera2.Content = null;
                //skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Hidden;
                //skrolVjuverPrikaz.Visibility = System.Windows.Visibility.Visible;
                if (this.documentManagerVM.Lista != null)
                {
                    for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                    {
                        if (CurrentOprema != null)
                        {
                            if ((CurrentOprema as Oprema).IdOprema == this.documentManagerVM.Lista[i].IdOprema)
                            {
                                trenutno = this.documentManagerVM.Lista[i];
                                CurrentOprema = this.documentManagerVM.Lista[i];
                                break;
                            }
                        }
                    }
                    pera2.Content = trenutno as Oprema;

                    


                }

                this.documentManagerVM.BrojOpremeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;

                if (GlavniProzor.listaTrenutnihRezervacija.Count > 0)
                {
                    RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacijeIma");
                }
                else
                {
                    RezervacijeKlik.SetResourceReference(StyleProperty, "stilDugmiciRezervacije");
                }

                if (daLiJeNestoPrebacenoUKorpu)
                {
                    this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
                    resetujSvaPoljaISvojstva();
                    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaSelect(this.documentManagerVM.TrenutniProdavac.IdKorisnici);
                    this.popuniKorpu(opremaPonovo);

                    if (this.documentManagerVM.Korpa.Count > 0)
                    {
                        korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpaIma");
                    }
                    else
                    {
                        korpaklik.SetResourceReference(StyleProperty, "stilDugmiciKorpa");
                    }

                }

                timerZatvoriRezervacije.Stop();
                timerOtvoriRezervacije.Stop();
            }
            else
            {
                //this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
                timerZatvoriRezervacije.Stop();
                timerOtvoriRezervacije.Stop();
                return;
            }
        }

        private void btnRezervisiArtikal_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private bool aktivnaPretraga = false;
        
             DispatcherTimer timer = new DispatcherTimer();
             string zaPretragu = "";
        private void tbPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox t = sender as TextBox;
            zaPretragu = t.Text;
            if (timer.IsEnabled)
            {
                timer.Stop();
                timer = new DispatcherTimer();
            }
            if(!timer.IsEnabled)
            {
                aktivnaPretraga = true;
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.PretragaOpreme(zaPretragu, false);
                if (niz.Length > 0)
                    this.popuniListuOprema(niz);
                else
                {
                    LejautDokumentOprema.Title = "Rezultati pretrage".ToUpper();
                    resetujSvaPoljaISvojstva();
                    aktivnaPretraga = false;
                }
                //skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;
                //aktivnaPretraga = true;
            }
            timer.Stop();
        }
        Parametri TrenutniParametarZaFilterChecked;
        Parametri TrenutniParametarZaFilterUnchecked;
        private void chbFilteri_Checked(object sender, RoutedEventArgs e)
        {
            
            CheckBox ch = sender as CheckBox;
            TextBlock textZaIdParametra = (ch.Parent as Grid).FindName("idParametra") as TextBlock;
            int skrivenIdParametra = Convert.ToInt32(textZaIdParametra.Text);
            TextBlock textZaIdVrednosti = (ch.Parent as Grid).FindName("idVrednostiFiltera") as TextBlock;
            int skrivenIdVrednosti = Convert.ToInt32(textZaIdVrednosti.Text);


            //ICollectionView checkboxovi = CollectionViewSource.GetDefaultView(this.ListaFiltera);
            //Parametri p = checkboxovi.CurrentItem as Parametri;

            for (int i = 0; i < this.ListaFiltera.Count; i++)
            {
                if (this.ListaFiltera[i].IdParametri == skrivenIdParametra)
                {
                    TrenutniParametarZaFilterChecked = ListaFiltera[i];
                    break;
                }
            }



            TrenutniParametarZaFilterChecked.OdabranParametarZaFiltere = true;
            foreach (var item in TrenutniParametarZaFilterChecked.KolekcijaVrednostiZaFilter)
            {
                if (item.idVrednostiFiltera == skrivenIdVrednosti)
                {
                    item.OdabranaVrednostZaFiltere = true;
                    break;
                }
            }



            //ICollectionView vrednostiCheckboxova = CollectionViewSource.GetDefaultView(p.KolekcijaVrednostiZaFilter);
            //VrednostiFiltera stikliranaVrednost = vrednostiCheckboxova.CurrentItem as VrednostiFiltera;

            this.PrikaziOpremuNaOsnovuIzabranihFiltera();

        }


        private void PrikaziOpremuNaOsnovuIzabranihFiltera()
        {
            int tipOpreme = 1;
            if (CurrentTipOpreme != null && CurrentTipOpreme.IdTipOpreme != 0)
                tipOpreme = CurrentTipOpreme.IdTipOpreme;

            //sastavljanje uslova upita
            //rade stavke grane ALI NE RADE TJ NE KUPI
            //SVE GRANE proverite ovo dole sto ne kupi sve lepo 
            // ana vveb servisu rai lepo onaj upit marko car !!! :D 
            List<DbItemParametri> listaZaUpit = new List<DbItemParametri>();
            foreach (var grana in this.ListaFiltera)
            {
                if (grana.OdabranParametarZaFiltere)
                {
                    DbItemParametri v = new DbItemParametri();
                    v.naziv_parametra = grana.Name;
                    List<string> s = new List<string>();
                    foreach (var stavka in grana.KolekcijaVrednostiZaFilter)
                    {
                        if (stavka.OdabranaVrednostZaFiltere)
                        {
                            s.Add(stavka.VrednostFiltera);

                        }
                    }
                    v.ListaVrednostiZaFiltere = s.ToArray();
                    listaZaUpit.Add(v);

                }
            }

            //servis da se pozove

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.PrikaziOpremuPoFilterima(tipOpreme, listaZaUpit.ToArray());
            this.popuniListuOprema(niz);
            //ControlTemplate ctpOdabirOpreme = ctlOdabirOpreme.Template;

            //ItemsControl pera = ctpOdabirOpreme.FindName("pera", ctlOdabirOpreme) as ItemsControl;
            //pera.ItemsSource = null;
            //pera.ItemsSource = this.documentManagerVM.Lista;
        }

        private void chbFilteri_Unchecked(object sender, RoutedEventArgs e)
        {
            //Parametri pa = new Parametri(null);
            CheckBox ch = sender as CheckBox;
            TextBlock textZaIdParametra = (ch.Parent as Grid).FindName("idParametra") as TextBlock;
            int skrivenIdParametra = Convert.ToInt32(textZaIdParametra.Text);
            TextBlock textZaIdVrednosti = (ch.Parent as Grid).FindName("idVrednostiFiltera") as TextBlock;
            int skrivenIdVrednosti = Convert.ToInt32(textZaIdVrednosti.Text);




            for (int i = 0; i < this.ListaFiltera.Count; i++)
            {
                if (this.ListaFiltera[i].IdParametri == skrivenIdParametra)
                {
                    TrenutniParametarZaFilterUnchecked = ListaFiltera[i];
                    break;
                }
            }



            //pa.OdabranParametarZaFiltere = true;
            //foreach (var item in pa.KolekcijaVrednostiZaFilter)
            //{
            //    if (item.idVrednostiFiltera == skrivenIdVrednosti)
            //    {
            //        item.OdabranaVrednostZaFiltere = true;
            //        break;
            //    }
            //}





            int brojac = 0;

            foreach (var item in TrenutniParametarZaFilterUnchecked.KolekcijaVrednostiZaFilter)
            {
                if (item.idVrednostiFiltera == skrivenIdVrednosti && item.OdabranaVrednostZaFiltere)
                {
                    item.OdabranaVrednostZaFiltere = false;
                    break;
                }
            }

            foreach (var item in TrenutniParametarZaFilterUnchecked.KolekcijaVrednostiZaFilter)
            {
                if (item.OdabranaVrednostZaFiltere)
                {
                    brojac++;
                }
            }

            if (brojac == 0)
            {
                TrenutniParametarZaFilterUnchecked.OdabranParametarZaFiltere = false;
            }



            //ICollectionView vrednostiCheckboxova = CollectionViewSource.GetDefaultView(p.KolekcijaVrednostiZaFilter);
            //VrednostiFiltera stikliranaVrednost = vrednostiCheckboxova.CurrentItem as VrednostiFiltera;

            this.PrikaziOpremuNaOsnovuIzabranihFiltera();

        }


        private void AdminPanelKlik_Click(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation daAdminPanelOtvoriRecNijeAktivanProzor = new DoubleAnimation(0, 0.5, new TimeSpan(0, 0, 0, 0, 500));
            recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daSakrijGlavniProzor);
            if (!timerOtvoriAdminPanel.IsEnabled)
            {
                timerOtvoriAdminPanel = new DispatcherTimer();
                timerOtvoriAdminPanel.Interval = new TimeSpan(0, 0, 0, 0, 500);
                timerOtvoriAdminPanel.Tick += timerOtvoriAdminPanel_Tick;
                timerOtvoriAdminPanel.Start();
                
                recNijeAktivanProzor.Visibility = Visibility.Visible;
            }




            
        }

        void timerOtvoriAdminPanel_Tick(object sender, EventArgs e)
        {
            if (timerOtvoriAdminPanel.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();

                AdminPanelProzor adminPanel = new AdminPanelProzor(this.documentManagerVM.TrenutniProdavac);

                adminPanel.ShowDialog();

                if (!timerZatvoriAdminPanel.IsEnabled)
                {
                    timerZatvoriAdminPanel = new DispatcherTimer();
                    timerZatvoriAdminPanel.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    timerZatvoriAdminPanel.Tick += timerZatvoriAdminPanel_Tick;
                    timerZatvoriAdminPanel.Start();
                }
                //DoubleAnimation daAdminPanelZatvoriRecNijeAktivanProzor = new DoubleAnimation(0.5, 0, new TimeSpan(0, 0, 0, 0, 500));
                //DoubleAnimation daAdminPanelZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));

                recNijeAktivanProzor.BeginAnimation(Rectangle.OpacityProperty, daPrikaziGlavniProzor);
                grdFilteri.BeginAnimation(Border.HeightProperty, daAnimacijaSakrijSekund);
            }
        }

        void timerZatvoriAdminPanel_Tick(object sender, EventArgs e)
        {
            if (timerZatvoriAdminPanel.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();

                pera2.Content = null;

                resetujSvaPoljaISvojstva();


                recNijeAktivanProzor.Visibility = Visibility.Hidden;
                

                this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());

            }

            timerOtvoriAdminPanel.Stop();
            timerZatvoriAdminPanel.Stop();
            
        }

        private void probaDugme_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("aaaa");
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            if (recNijeAktivanProzor.Visibility == Visibility.Visible)
                SystemSounds.Beep.Play();
            if (kliknutoNaProzorJednom)
                kliknutoNaProzorDvaput = true;
            kliknutoNaProzorJednom = true;
            if (!timer3.IsEnabled)
            {
                timer3.Interval = new TimeSpan(0, 0, 0, 0, 200);
                timer3.Tick += timer3_Tick;
                timer3.Start();
            }
        }

        private void dugmeZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dugmeMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                //grdCeoSadrzaj.Margin = new Thickness(8);
            }

            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                //grdCeoSadrzaj.Margin = new Thickness(0);
            }
        }

        private void dugmeMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnUcitajPonovoIzBaze_Click(object sender, RoutedEventArgs e)
        {
            this.documentManagerVM.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());
            resetujSvaPoljaISvojstva();
        }

        private void grdTriVjuHierarchical_MouseEnter(object sender, MouseEventArgs e)
        {


            Grid grdHierarchical = (Grid)sender;
            
            Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
            Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
            Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
            //Border brdProbaBorderiSirina = grdHierarchical.FindName("brdProbaBorderiSirina") as Border;
            Border brdProbaBorderi = grdHierarchical.FindName("brdProbaBorderi") as Border;
            
            Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;
            int idOblastiOpreme = Convert.ToInt32((grdHierarchical.FindName("skrivenId") as TextBlock).Text);
            int HoverovanaOblast = -1;
            for (int i = 0; i < this.documentManagerVM.CurrentDocumentVM.RootVM.Items.Count; i++)
            {
                if (((this.documentManagerVM.CurrentDocumentVM.RootVM.Items[i] as OblastiOpremeVM).Model as OblastiOpreme).IdOblastiOpreme == idOblastiOpreme)
                {
                    HoverovanaOblast = i;
                    break;
                }
            }
            if ((HoverovanaOblast != -1) &&((this.documentManagerVM.CurrentDocumentVM.RootVM.Items[HoverovanaOblast] as OblastiOpremeVM).Items.Count > 0))
            {
                ToggleButton tgb = grdHierarchical.FindName("Expander") as ToggleButton;
                Thickness margina = grdHierarchical.Margin;
                tgb.Width = 30;

                margina.Left = -30;
                grdHierarchical.Margin = margina;

                if (tgb.IsChecked == true)
                {

                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleExpandovanoHoverovano");
                }
                else
                {
                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleHoverovano");

                }
            }
            


            //brdTriVjuHierarchicalNaziv.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
            //brdAktivno.Background = Brushes.White;

            //ColorAnimation caBrdTriVjuHierarchicalNazivBorder = new ColorAnimation((SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778")), new TimeSpan(0, 0, 0, 0, 300));
            //ThicknessAnimation taBrdTriVjuHierarchicalNazivBorder = new ThicknessAnimation(new Thickness(0, 1, 0, 1), new TimeSpan(0, 0, 0, 0, 300));
            //brdTriVjuHierarchicalNaziv.BeginAnimation(Border.BackgroundProperty, caBrdTriVjuHierarchicalNazivBorder);
            //brdTriVjuHierarchicalNaziv.BorderBrush = txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
            brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;



            brdTriVjuHierarchicalNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
            brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalNazivSirina);
            //brdProbaBorderi.BorderThickness = new Thickness(1, 0, 0, 0);
            //brdProbaBorderi.Width = 187;
            //brdProbaBorderi.Visibility = Visibility.Visible;
            txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //txtBoxNazivOblastiOpreme.Foreground = Brushes.White;
            //txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;




            

        }

        private void grdTriVjuHierarchical_MouseLeave(object sender, MouseEventArgs e)
        {
            //Grid grdHierarchical = (Grid)sender;
            //Border brdHierarchical = grdHierarchical.Children[0] as Border;
            ////Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
            //Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
            //brdAktivno.Background = Brushes.Gray;

            //brdHierarchical.Background = Brushes.Transparent;
            //Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;
            //brdTriVjuHierarchicalNaziv.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
            //txtBoxNazivOblastiOpreme.Foreground = Brushes.White;
            //txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {

                Grid grdHierarchical = (Grid)sender;
                Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
                Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
                Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
                Border brdProbaBorderi = grdHierarchical.FindName("brdProbaBorderi") as Border;
                Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;
                ToggleButton tgb = grdHierarchical.FindName("Expander") as ToggleButton;
                if (tgb.IsChecked == true)
                {
                    tgb.Width = 30;
                    Thickness margina = grdHierarchical.Margin;
                    margina.Left = -30;
                    grdHierarchical.Margin = margina;
                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleHoverovano");
                }
                else
                {
                    Thickness margina = grdHierarchical.Margin;
                    margina.Left = 0;
                    grdHierarchical.Margin = margina;
                    tgb.Width = 0;
                    //tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyle");
                }
                //brdTriVjuHierarchicalNaziv.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                //Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
                //brdAktivno.Background = Brushes.White;
                if (treeViewItem.IsExpanded)
                {

                    
                }
                else
                {
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                    //DoubleAnimation daBrdTriVjuHierarchicalNazivSirina = new DoubleAnimation(0, 187, new TimeSpan(0, 0, 0, 0, 500));
                    //brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalNazivSirina);
                    //brdTriVjuHierarchicalNaziv.BorderBrush = Brushes.Gainsboro;
                    //brdProbaBorderi.Visibility = Visibility.Hidden;
                    txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;

                }
            }
        }



        private void grdTriVjuHierarchical_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timer2.IsEnabled)
            {
                timer2.Stop();
                timer2 = new DispatcherTimer();
            }
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                Grid grdHierarchical = (Grid)sender;



                Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
                Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
                Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
                Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;

                int idOblastiOpreme = Convert.ToInt32((grdHierarchical.FindName("skrivenId") as TextBlock).Text);
                int HoverovanaOblast = -1;
                for (int i = 0; i < this.documentManagerVM.CurrentDocumentVM.RootVM.Items.Count; i++)
                {
                    if (((this.documentManagerVM.CurrentDocumentVM.RootVM.Items[i] as OblastiOpremeVM).Model as OblastiOpreme).IdOblastiOpreme == idOblastiOpreme)
                    {
                        HoverovanaOblast = i;
                        break;
                    }
                }
                if ((HoverovanaOblast != -1) && ((this.documentManagerVM.CurrentDocumentVM.RootVM.Items[HoverovanaOblast] as OblastiOpremeVM).Items.Count > 0))
                {
                    ToggleButton tgb = grdHierarchical.FindName("Expander") as ToggleButton;
                    Thickness margina = grdHierarchical.Margin;
                    tgb.Width = 30;

                    margina.Left = -30;
                    grdHierarchical.Margin = margina;

                    if (tgb.IsChecked == true)
                    {

                        tgb.IsChecked = false;
                    }
                    else
                    {
                        tgb.IsChecked = true;


                    }
                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleHoverovano");
                }







                if (treeViewItem.IsExpanded)
                {
                    
                    if (TrenutniGridTriVjuTipoviOpreme != null && HoverovanaOblast != -1)
                    {
                        int idOblasti = Convert.ToInt32((TrenutniGridTriVjuTipoviOpreme.FindName("skrivenIdOblasti") as TextBlock).Text);
                        if (((this.documentManagerVM.CurrentDocumentVM.RootVM.Items[HoverovanaOblast] as OblastiOpremeVM).Model as OblastiOpreme).IdOblastiOpreme == idOblasti)
                        {

                            int HoverovanTip = -1;

                            int idTipaOpreme = Convert.ToInt32((TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock).Text);

                            foreach (var item in this.documentManagerVM.CurrentDocumentVM.RootVM.Items)
                            {
                                for (int i = 0; i < (item.Model as OblastiOpreme).Items.Count; i++)
                                {
                                    if (((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).IdTipOpreme == idTipaOpreme)
                                    {
                                        HoverovanTip = i;
                                        
                                        ((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).DaLiJeKliknutoNaGrid = false;
                                        LejautDokumentOprema.Title = "OPREMA";
                                        resetujSvaPoljaISvojstva();
                                       
                                        //this.documentManagerVM.Lista = null;
                                        //this.StariTipOpreme = null;
                                        //this.CurrentTipOpreme = null;
                                        ////this.CurrentTipOpreme = 0;
                                        
                                        //Border brdTriVjuTipoviOpremeNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                                        //brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;

                                        //trenutniBorderOprema = null;
                                        //this.CurrentOprema = null;
                                        //this.selektovanRed = 1;

                                        //TextBlock textblok = TrenutniGridTriVjuTipoviOpreme.FindName("textblok") as TextBlock;
                                        //textblok.Foreground = Brushes.Black;
                                        //skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Hidden;
                                        ////skrolVjuverPrikaz.Visibility = Visibility.Hidden;
                                        //grdFilteri.Visibility = Visibility.Hidden;
                                        //DoubleAnimation daZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                                        //grdFilteri.BeginAnimation(Border.HeightProperty, daZatvoriGrdFilteri);

                                        //DoubleAnimation dabrdPrikazDetaljaOpremeSirinaNula = new DoubleAnimation(staraSirinabrdPrikazDetaljaOpreme,0,new TimeSpan(0,0,0,0,500));
                                        //brdPrikazDetaljaOpreme.BeginAnimation(Border.WidthProperty, dabrdPrikazDetaljaOpremeSirinaNula);
                                        //staraSirinabrdPrikazDetaljaOpreme = 0;
                                        //aktivanDetaljanPrikaz = false; 
                                        break;
                                    }
                                }
                                if (HoverovanTip != -1)
                                    break;

                            }
                            
                        }

                        
                    }
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                    //brdTriVjuHierarchicalNaziv.BorderBrush = Brushes.Gainsboro;

                    txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
                    

                    treeViewItem.IsExpanded = false;
                }
                else
                {
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
                   // brdTriVjuHierarchicalNaziv.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    treeViewItem.IsExpanded = true;

                }


            }
            


        }

        
        


        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        Grid TrenutniGridTriVjuTipoviOpreme;
        //public static string srediSlovaPrvoSlovoPocetnoZaSvakuRec(string s)
        //{
        //    StringBuilder sb = new StringBuilder(s.Length);
        //    bool capitalize = true;
        //    foreach (char c in s)
        //    {
        //        sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
        //        capitalize = !Char.IsLetter(c);
        //    }
        //    return sb.ToString();
        //}


        private void grdTriVjuTipoviOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            brojacZaTextBlockLoaded = 0;

            //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
            brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
            

            
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                Grid grdTriVjuTipoviOpreme = (Grid)sender;
                int idTipaOpreme = Convert.ToInt32((grdTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock).Text);
                if (idTipaOpreme != 0)
                {
                    
                    if ((this.StariTipOpreme != null && this.StariTipOpreme.IdTipOpreme != idTipaOpreme) || aktivnaPretraga || idTipaOpreme != 0)
                    {
                        int HoverovanTip = -1;
                        foreach (var item in this.documentManagerVM.CurrentDocumentVM.RootVM.Items)
                        {



                            for (int i = 0; i < (item.Model as OblastiOpreme).Items.Count; i++)
                            {
                                if (((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).IdTipOpreme == idTipaOpreme)
                                {
                                    HoverovanTip = i;

                                    ((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).DaLiJeKliknutoNaGrid = true;
                                    this.CurrentTipOpreme = (item.Model as OblastiOpreme).Items[i] as TipoviOpreme;
                                   
                                    break;
                                }
                            }
                            if (HoverovanTip != -1)
                                break;

                        }

                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(idTipaOpreme);
                        if (niz.Length > 0)
                            this.popuniListuOprema(niz);
                        else
                        {

                            this.documentManagerVM.TekstPretrage = this.CurrentTipOpreme.Name;
                            //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                            brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
                            this.CurrentOprema = null;
                            if (this.documentManagerVM.Lista != null)
                                this.documentManagerVM.Lista.Clear();
                            
                            //aktivirajDetaljanPrikaz();

                        }


                        //CurrentTipOpreme.IdTipOpreme = idTipaOpreme;
                         HoverovanTip = -1;
                         if (this.StariTipOpreme != null)
                         {
                             if (this.CurrentTipOpreme.IdTipOpreme != this.StariTipOpreme.IdTipOpreme)
                             {
                                 if (this.StariTipOpreme.DaLiJeKliknutoNaGrid)
                                 {
                                     foreach (var item in this.documentManagerVM.CurrentDocumentVM.RootVM.Items)
                                     {
                                         for (int i = 0; i < (item.Model as OblastiOpreme).Items.Count; i++)
                                         {
                                             if (((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).IdTipOpreme == this.StariTipOpreme.IdTipOpreme)
                                             {
                                                 HoverovanTip = i;

                                                 ((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).DaLiJeKliknutoNaGrid = false;
                                                 if (TrenutniGridTriVjuTipoviOpreme != null)
                                                 {
                                                     Border brdTriVjuTipoviOpremeNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                                                     brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;

                                                     TextBlock textblok = TrenutniGridTriVjuTipoviOpreme.FindName("textblok") as TextBlock;
                                                     textblok.Foreground = Brushes.Black;
                                                 }
                                                 this.CurrentOprema = null;
                                                 trenutniBorderOprema = null;
                                                 selektovanRed = 1;
                                                 aktivirajDetaljanPrikaz();
                                                 break;
                                             }
                                         }
                                         if (HoverovanTip != -1)
                                             break;

                                     }
                                 }


                             }
                         }

                       
                        

                        //skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;

                        this.popuniListuFiltera(idTipaOpreme);
                        TrenutniGridTriVjuTipoviOpreme = grdTriVjuTipoviOpreme;

                        if (this.CurrentTipOpreme != null)
                            LejautDokumentOprema.Title = this.CurrentTipOpreme.Name.ToUpper();

                        //DoubleAnimation daSakrijPraznuOpremuZbogPretrage = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        //DoubleAnimation daSakrijPraznuOpremu = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        //DoubleAnimation daPrikaziIctRedove = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        if(!prviPutUcitanTriVjuTipoviOpreme)
                        {
                            brdListaSaOpremomJePraznaZaOdabraniTipOpreme.Visibility = Visibility.Visible;
                            brdListaSaOpremomJePraznaZbogOdabranihFiltera.Visibility = Visibility.Visible;
                            brdListaSaOpremomJePraznaZbogPretrage.Visibility = Visibility.Visible;
                            skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;
                            prviPutUcitanTriVjuTipoviOpreme = true;
                        }
                        
                        //brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        
                        //brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        
                        //brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        
                        //skrolvjuverPrikazTipoviOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                        if (niz.Length > 0)
                        {
                            brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                            skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaPrikaziSekund);
                            
                        }
                        
                    }
                    

                }
                this.StariTipOpreme = this.CurrentTipOpreme;
                if (ListaFiltera != null)
                {
                    if (ListaFiltera.Count > 0)
                    {
                        //for (int i = 0; i < ListaFiltera.Count; i++)
                        //{
                        //    if (ListaFiltera[i].KolekcijaVrednostiZaFilter.Count > 0)
                        //    {

                        //    }
                        //}
                        
                        //grdFilteri.Visibility = Visibility.Visible;

                        //ThicknessAnimation taOtvoriGrdFilteri = new ThicknessAnimation(new Thickness(0, 120, 0, 0), new Thickness(0), new TimeSpan(0, 0, 1));
                        //grdFilteri.BeginAnimation(Border.MarginProperty, taOtvoriGrdFilteri);

                        //DoubleAnimation daOtvoriGrdFilteri = new DoubleAnimation(120, new TimeSpan(0, 0, 1));
                        grdFilteri.BeginAnimation(Border.HeightProperty, daPrikaziFiltere);
                    }
                    else
                    {
                        //grdFilteri.Visibility = Visibility.Hidden;

                        //ThicknessAnimation taZatvoriGrdFilteri = new ThicknessAnimation(new Thickness(0, 120, 0, 0), new TimeSpan(0, 0, 1));
                        //grdFilteri.BeginAnimation(Border.MarginProperty, taZatvoriGrdFilteri);

                        //DoubleAnimation daZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                        grdFilteri.BeginAnimation(Border.HeightProperty, daSakrijFiltere);
                    }
                }
                else
                {
                    //grdFilteri.Visibility = Visibility.Hidden;

                    //ThicknessAnimation taZatvoriGrdFilteri = new ThicknessAnimation(new Thickness(0, 120, 0, 0), new TimeSpan(0, 0, 1));
                    //grdFilteri.BeginAnimation(Border.MarginProperty, taZatvoriGrdFilteri);
                    //DoubleAnimation daZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                    grdFilteri.BeginAnimation(Border.HeightProperty, daSakrijFiltere);
                }
            }



        }

        private void grdTriVjuTipoviOpreme_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grdTriVjuTipoviOpreme = (Grid)sender;
            //Border brdTriVjuTipoviOpremeAktivno = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeAktivno") as Border;
            //brdTriVjuTipoviOpremeAktivno.Background = Brushes.White;


            Border brdTriVjuTipoviOpremeNaziv = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
            brdTriVjuTipoviOpremeNaziv.Background = Brushes.Gainsboro;
            //Border brdProba = brdTriVjuTipoviOpremeNaziv.Child as Border;
            //brdProba.Background = new BrushConverter().ConvertFrom("#3FFFFFFF") as SolidColorBrush;


            TextBlock textblok = brdTriVjuTipoviOpremeNaziv.FindName("textblok") as TextBlock;
            //ThicknessAnimation taBrdTriVjuTipoviOpremelNazivMargina = new ThicknessAnimation(new Thickness(-5, 0, 0, 0), new Thickness(0), new TimeSpan(0, 0, 0, 0, 200));
            

            


            brdTriVjuTipoviOpremeNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
            brdTriVjuTipoviOpremeNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuTipoviOpremeNazivSirina);
            //textblok.Foreground = Brushes.White;
            textblok.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;




        }

        private void grdTriVjuTipoviOpreme_MouseLeave(object sender, MouseEventArgs e)
        {
            
            //Border brdTriVjuTipoviOpremeAktivno = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeAktivno") as Border;
            //brdTriVjuTipoviOpremeAktivno.Background = Brushes.Gray;
            Grid grdTriVjuTipoviOpreme = (Grid)sender;

            
            int idTipaOpreme = Convert.ToInt32((grdTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock).Text);
            //CurrentTipOpreme = idTipaOpreme;
            //int HoverovanTip = -1;

            if(this.CurrentTipOpreme != null)
            {
                if(this.CurrentTipOpreme.IdTipOpreme != idTipaOpreme)
                {
                    Border brdTriVjuTipoviOpremeNaziv = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                    brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;

                    TextBlock textblok = brdTriVjuTipoviOpremeNaziv.FindName("textblok") as TextBlock;
                    textblok.Foreground = Brushes.Black;
                }
            }
            else 
            {
                Border brdTriVjuTipoviOpremeNaziv = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;

                TextBlock textblok = brdTriVjuTipoviOpremeNaziv.FindName("textblok") as TextBlock;
                textblok.Foreground = Brushes.Black;
            }

            //foreach (var item in this.documentManagerVM.CurrentDocumentVM.RootVM.Items)
            //{
            //    for (int i = 0; i < (item.Model as OblastiOpreme).Items.Count; i++)
            //    {
            //        if (((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).IdTipOpreme == idTipaOpreme)
            //        {
            //            HoverovanTip = i;
            //            if (((item.Model as OblastiOpreme).Items[i] as TipoviOpreme).DaLiJeKliknutoNaGrid)
            //            {

            //            }
            //            else
            //            {
            //                Border brdTriVjuTipoviOpremeNaziv = grdTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
            //                brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;

            //                TextBlock textblok = brdTriVjuTipoviOpremeNaziv.FindName("textblok") as TextBlock;
            //                textblok.Foreground = Brushes.Black;
            //            }

            //            break;
            //        }
            //    }
            //    if (HoverovanTip != -1)
            //        break;
            //}
        }

        

        private void grdOprema_MouseEnter(object sender, MouseEventArgs e)
        {
            //Grid grdOprema = (Grid)sender;
            //////Border brdOprema = grdOprema.Parent as Border;
            //////brdOprema.BorderThickness = new Thickness(0);
            //////Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //////rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

            //int idOpreme = Convert.ToInt32((grdOprema.FindName("skrivenId") as TextBlock).Text);

            //Border brdOpremaNaziv = grdOprema.FindName("brdOpremaNaziv") as Border;
            //brdOpremaNaziv.Background = Brushes.Gainsboro;
            //Label lblOpremaNaziv = grdOprema.FindName("lblOpremaNaziv") as Label;
            //lblOpremaNaziv.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;

            

            //Label lblProizvodjac = grdOprema.FindName("lblProizvodjac") as Label;
            //lblProizvodjac.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;

            //Label lblModel = grdOprema.FindName("lblModel") as Label;
            //lblModel.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;

            ////Label lblOpis = grdOprema.FindName("lblOpis") as Label;
            ////lblOpis.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;



            //int brojRedova = ictOpremaRedovi.Items.Count;
            //if (brojRedova > 0)
            //{
            //    for (int i = 0; i < brojRedova; i++)
            //    {
            //        ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
            //        Grid grd1 = FindChild<Grid>(cp);
            //        ItemsControl ictKoloneCena = grd1.FindName("ictOpremaKoloneCena") as ItemsControl;
            //        //ItemsControl ictKolone = FindChild<ItemsControl>(cp);
            //        if (ictKoloneCena.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            //        {
            //            for (int j = 0; j < ictKoloneCena.Items.Count; j++)
            //            {
            //                ContentPresenter cpa = ictKoloneCena.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;

            //                Grid grdGlavniOpremaCena = FindChild<Grid>(cpa);
            //                //ScrollViewer scvOprema = grdGlavniOprema.Children[0] as ScrollViewer;
            //                Grid grdOpremaCena = grdGlavniOpremaCena.FindName("grdOpremaCena") as Grid;
            //                //Grid grdOprema = brdOprema.Child as Grid;
            //                int idCena = Convert.ToInt32((grdOpremaCena.FindName("skrivenId") as TextBlock).Text);

            //                if (idCena == idOpreme)
            //                {
            //                    //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //                    //rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

            //                    //Border brdOpremaNaziv = grdOpremaCena.FindName("brdOpremaNaziv") as Border;
            //                    //brdOpremaNaziv.Background = Brushes.Transparent;
            //                    Label lblCena = grdOpremaCena.FindName("lblCena") as Label;
            //                    lblCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
            //                    TextBlock tblckCena = grdOpremaCena.FindName("tblckCena") as TextBlock;
            //                    tblckCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
            //                    i = brojRedova;

            //                    break;
            //                }

            //            }

            //        }
            //    }


            //}




            //Label lblCena = grdOprema.FindName("lblCena") as Label;
            //lblCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
        }

        private void grdOprema_MouseLeave(object sender, MouseEventArgs e)
        {
            //Grid grdOprema = (Grid)sender;
            //////Border brdOprema = grdOprema.Parent as Border;
            //////brdOprema.BorderThickness = new Thickness(1);
            //int idOpreme = Convert.ToInt32((grdOprema.FindName("skrivenId") as TextBlock).Text);
            //if (this.CurrentOprema != null && this.CurrentOprema.IdOprema != 0)
            //{

            //    int id = Convert.ToInt32((grdOprema.FindName("skrivenId") as TextBlock).Text);
            //    if (this.CurrentOprema.IdOprema == id && this.CurrentOprema.DaLiJeKliknutoNaGrid)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //        //rctSenkaZaBrdOprema.Visibility = Visibility.Hidden;


            //        Border brdOpremaNaziv = grdOprema.FindName("brdOpremaNaziv") as Border;
            //        brdOpremaNaziv.Background = Brushes.White;
            //        Label lblOpremaNaziv = grdOprema.FindName("lblOpremaNaziv") as Label;
            //        lblOpremaNaziv.Foreground = Brushes.Black;

            //        Label lblProizvodjac = grdOprema.FindName("lblProizvodjac") as Label;
            //        lblProizvodjac.Foreground = Brushes.Gray;

            //        Label lblModel = grdOprema.FindName("lblModel") as Label;
            //        lblModel.Foreground = Brushes.Gray;

            //        //Label lblOpis = grdOprema.FindName("lblOpis") as Label;
            //        //lblOpis.Foreground = Brushes.Gray;

            //        int brojRedova = ictOpremaRedovi.Items.Count;
            //        if (brojRedova > 0)
            //        {
            //            for (int i = 0; i < brojRedova; i++)
            //            {
            //                ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
            //                Grid grd1 = FindChild<Grid>(cp);
            //                ItemsControl ictKoloneCena = grd1.FindName("ictOpremaKoloneCena") as ItemsControl;
            //                //ItemsControl ictKolone = FindChild<ItemsControl>(cp);
            //                if (ictKoloneCena.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            //                {
            //                    for (int j = 0; j < ictKoloneCena.Items.Count; j++)
            //                    {
            //                        ContentPresenter cpa = ictKoloneCena.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;

            //                        Grid grdGlavniOpremaCena = FindChild<Grid>(cpa);
            //                        //ScrollViewer scvOprema = grdGlavniOprema.Children[0] as ScrollViewer;
            //                        Grid grdOpremaCena = grdGlavniOpremaCena.FindName("grdOpremaCena") as Grid;
            //                        //Grid grdOprema = brdOprema.Child as Grid;
            //                        int idCena = Convert.ToInt32((grdOpremaCena.FindName("skrivenId") as TextBlock).Text);

            //                        if (idCena == idOpreme)
            //                        {
            //                            //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //                            //rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

            //                            //Border brdOpremaNaziv = grdOpremaCena.FindName("brdOpremaNaziv") as Border;
            //                            //brdOpremaNaziv.Background = Brushes.Transparent;
            //                            Label lblCena = grdOpremaCena.FindName("lblCena") as Label;
            //                            lblCena.Foreground = Brushes.Black;
            //                            TextBlock tblckCena = grdOpremaCena.FindName("tblckCena") as TextBlock;
            //                            tblckCena.Foreground = Brushes.Black;
            //                            i = brojRedova;

            //                            break;
            //                        }

            //                    }

            //                }
            //            }


            //        }
            //    }


            //}
            //else
            //{
            //    //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //    //rctSenkaZaBrdOprema.Visibility = Visibility.Hidden;


            //    Border brdOpremaNaziv = grdOprema.FindName("brdOpremaNaziv") as Border;
            //    brdOpremaNaziv.Background = Brushes.White;
            //    Label lblOpremaNaziv = grdOprema.FindName("lblOpremaNaziv") as Label;
            //    lblOpremaNaziv.Foreground = Brushes.Black;

            //    Label lblProizvodjac = grdOprema.FindName("lblProizvodjac") as Label;
            //    lblProizvodjac.Foreground = Brushes.Gray;

            //    Label lblModel = grdOprema.FindName("lblModel") as Label;
            //    lblModel.Foreground = Brushes.Gray;

            //    //Label lblOpis = grdOprema.FindName("lblOpis") as Label;
            //    //lblOpis.Foreground = Brushes.Gray;

            //    int brojRedova = ictOpremaRedovi.Items.Count;
            //    if (brojRedova > 0)
            //    {
            //        for (int i = 0; i < brojRedova; i++)
            //        {
            //            ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
            //            Grid grd1 = FindChild<Grid>(cp);
            //            ItemsControl ictKoloneCena = grd1.FindName("ictOpremaKoloneCena") as ItemsControl;
            //            //ItemsControl ictKolone = FindChild<ItemsControl>(cp);
            //            if (ictKoloneCena.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            //            {
            //                for (int j = 0; j < ictKoloneCena.Items.Count; j++)
            //                {
            //                    ContentPresenter cpa = ictKoloneCena.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;

            //                    Grid grdGlavniOpremaCena = FindChild<Grid>(cpa);
            //                    //ScrollViewer scvOprema = grdGlavniOprema.Children[0] as ScrollViewer;
            //                    Grid grdOpremaCena = grdGlavniOpremaCena.FindName("grdOpremaCena") as Grid;
            //                    //Grid grdOprema = brdOprema.Child as Grid;
            //                    int idCena = Convert.ToInt32((grdOpremaCena.FindName("skrivenId") as TextBlock).Text);

            //                    if (idCena == idOpreme)
            //                    {
            //                        //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
            //                        //rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

            //                        //Border brdOpremaNaziv = grdOpremaCena.FindName("brdOpremaNaziv") as Border;
            //                        //brdOpremaNaziv.Background = Brushes.Transparent;
            //                        Label lblCena = grdOpremaCena.FindName("lblCena") as Label;
            //                        lblCena.Foreground = Brushes.Black;
            //                        TextBlock tblckCena = grdOpremaCena.FindName("tblckCena") as TextBlock;
            //                        tblckCena.Foreground = Brushes.Black;
            //                        i = brojRedova;

            //                        break;
            //                    }

            //                }

            //            }
            //        }
            //    }
            //}
            
            
        }



        //bool prviPutKolona1 = false;
        //bool prviPutKolona2 = false;
        //bool prviPutKolona3 = false;
        //bool prviPutKolona4 = false;

        //bool drugiPutKolona1 = false;
        //bool drugiPutKolona2 = false;
        //bool drugiPutKolona3 = false;
        //bool drugiPutKolona4 = false;
        DispatcherTimer timer2 = new DispatcherTimer();
       
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Window proba = sender as Window;
            //aktivirajDetaljanPrikaz();
            //aktivirajDetaljanPrikaz();
            aktivirajDetaljanPrikaz();
            novaSirinaDokaOprema = Convert.ToDecimal(brdDockOprema.ActualWidth);
            decimal decimalniPrikazNoveSirineDokaOprema = 0;
            decimal decimalniPrikazStareSirineDokaOprema = 0;

            decimal daLiSeUvecavaDokIliSeSmanjuje = 0;
            if (novaSirinaDokaOprema > staraSirinaDoka)
                daLiSeUvecavaDokIliSeSmanjuje = 1;
            else
                daLiSeUvecavaDokIliSeSmanjuje = -1;

            if (daLiSeUvecavaDokIliSeSmanjuje != 0)
            {
                if (staraSirinaDoka > sirinaJednogObjektaOpreme)
                {
                    //if (brojKolonaKojiMozeDaStaneUStariiDok + daLiSeUvecavaDokIliSeSmanjuje == 0)
                    //{
                    //    decimalniPrikazStareSirineDokaOprema = 0;
                    //    decimalniPrikazStareSirineDokaOprema = Math.Round(staraSirinaDoka / sirinaJednogObjektaOpreme,2);
                    //    //brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor((staraSirinaDoka / (sirinaJednogObjektaOpreme + (2 * 15)))));
                    //}
                    //else
                    //{
                        decimalniPrikazStareSirineDokaOprema = 0;
                        decimalniPrikazStareSirineDokaOprema = Math.Round(staraSirinaDoka / sirinaJednogObjektaOpreme,2);
                    //}
                    if (decimalniPrikazStareSirineDokaOprema != 0)
                    {
                        brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor((decimalniPrikazStareSirineDokaOprema)));
                    }
                    else
                        brojKolonaKojiMozeDaStaneUStariiDok = 1;
                }
                else
                {
                    brojKolonaKojiMozeDaStaneUStariiDok = 1;
                }
                if (novaSirinaDokaOprema > sirinaJednogObjektaOpreme)
                {
                    //if (brojKolonaKojiMozeDaStaneUNoviDok + daLiSeUvecavaDokIliSeSmanjuje == 0)
                    //{
                    //    decimalniPrikazNoveSirineDokaOprema = 0;
                    //    decimalniPrikazNoveSirineDokaOprema = Math.Round(novaSirinaDokaOprema / sirinaJednogObjektaOpreme, 2);
                    //    //brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor(((novaSirinaDokaOprema / (sirinaJednogObjektaOpreme + (2 * 15))))));
                    //}
                    //else
                    //{
                        decimalniPrikazNoveSirineDokaOprema = 0;
                        decimalniPrikazNoveSirineDokaOprema = Math.Round(novaSirinaDokaOprema / sirinaJednogObjektaOpreme, 2);
                    //}
                    if (decimalniPrikazNoveSirineDokaOprema != 0)
                    {
                        brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor((decimalniPrikazNoveSirineDokaOprema)));
                    }
                    else
                        brojKolonaKojiMozeDaStaneUNoviDok = 1;
                }
                else
                    brojKolonaKojiMozeDaStaneUNoviDok = 1;
            }
            //doublePrikazNoveSirineDokaOprema = doublePrikazStareSirineDokaOprema = 0;
            if (brojKolonaKojiMozeDaStaneUNoviDok == 0) brojKolonaKojiMozeDaStaneUNoviDok = 1;
            if ((brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok) || (this.StariTipOpreme != this.CurrentTipOpreme))
            {
                //doublePrikazNoveSirineDokaOprema = (novaSirinaDokaOprema / (sirinaJednogObjektaOpreme + ((brojKolonaKojiMozeDaStaneUNoviDok + daLiSeUvecavaDokIliSeSmanjuje) * 15)));
                //doublePrikazNoveSirineDokaOprema = (novaSirinaDokaOprema / (sirinaJednogObjektaOpreme + ((brojKolonaKojiMozeDaStaneUNoviDok + daLiSeUvecavaDokIliSeSmanjuje) * 15)));
                //staraSirinaDoka = novaSirinaDokaOprema;
                poslatoOdPromeneVelicineProzora = true;
                rasporediListuOpremePravilno();
                
            }
            //if (staraSirinaDoka > 0)
            //{
            //    novaSirinaDokaOprema = brdDockOprema.ActualWidth;
            //    brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(staraSirinaDoka / sirinaJednogObjektaOpreme)));
            //    brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(novaSirinaDokaOprema / sirinaJednogObjektaOpreme)));

            //    if (brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok)
            //    {
            //        rasporediListuOpremePravilno();
            //        if (novaSirinaDokaOprema > (sirinaJednogObjektaOpreme * brojKolonaKojiMozeDaStaneUNoviDok) + (15 * brojKolonaKojiMozeDaStaneUNoviDok) + 5)
            //        {
            //            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme.IdTipOpreme);
            //            this.popuniListuOprema(niz);

            //            if (this.documentManagerVM.Lista != null)
            //            {
            //                int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //                double razlomak = brojProizvodaUListiOpreme / (double)brojKolonaKojiMozeDaStaneUNoviDok;
            //                int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //                int brojac = 0;
            //                bool prekinuto = false;

            //                List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //                for (int i = 0; i < brojNovihLista; i++)
            //                {
            //                    List<Oprema> novaLista = new List<Oprema>();

            //                    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //                    {
            //                        if (j % brojKolonaKojiMozeDaStaneUNoviDok == 0 && j != 0 && !prekinuto)
            //                        {
            //                            prekinuto = true;
            //                            break;
            //                        }
            //                        novaLista.Add(this.documentManagerVM.Lista[j]);
            //                        brojac++;
            //                        prekinuto = false;

            //                    }
            //                    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //                    novaListaZaListuSvihLista.NovaLista = novaLista;
            //                    listaSvihLista.Add(novaListaZaListuSvihLista);
            //                }
            //                this.documentManagerVM.ListaSvihLista = listaSvihLista;
            //            }
            //        }
            //        staraSirinaDoka = novaSirinaDokaOprema;

            //        if (ictOpremaRedovi.Items.Count > 0)
            //        {
            //            if (timer2.IsEnabled)
            //                timer2.Stop();
            //            timer2.Interval = new TimeSpan(0, 0, 1);
            //            timer2.Tick += timer2_Tick;
            //            daLiJePosecenTimer = false;
            //            timer2.Start();

            //        }
            //    }
            //}

            
           
            

            //if (brdDockOprema.ActualWidth < 575)
            //{
                
            //    if (!prviPutKolona1)
            //    {
            //        if (CurrentTipOpreme != 0)
            //        {
            //            rasporediListuOpremePravilno();
                        
            //            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme);
            //            //this.popuniListuOprema(niz);


            //            //int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //            //double razlomak = brojProizvodaUListiOpreme / (double)1;
            //            //int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //            //int brojac = 0;
            //            //bool prekinuto = false;

            //            //List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //            //for (int i = 0; i < brojNovihLista; i++)
            //            //{
            //            //    List<Oprema> novaLista = new List<Oprema>();

            //            //    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //            //    {
            //            //        if (j % 1 == 0 && j != 0 && !prekinuto)
            //            //        {
            //            //            prekinuto = true;
            //            //            break;
            //            //        }
            //            //        novaLista.Add(this.documentManagerVM.Lista[j]);
            //            //        brojac++;
            //            //        prekinuto = false;

            //            //    }
            //            //    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //            //    novaListaZaListuSvihLista.NovaLista = novaLista;
            //            //    listaSvihLista.Add(novaListaZaListuSvihLista);
            //            //}
            //            //this.documentManagerVM.ListaSvihLista = listaSvihLista;


            //        }
            //        //if (ictOpremaRedovi.Items.Count > 0)
            //        //{
            //        //    if (timer2.IsEnabled)
            //        //        timer2.Stop();
            //        //    timer2.Interval = new TimeSpan(0, 0, 1);
            //        //    timer2.Tick += timer2_Tick;
            //        //    daLiJePosecenTimer = false;
            //        //    timer2.Start();

            //        //}


            //        //drugiPutKolona1 = true;

            //        //drugiPutKolona2 = false;
            //        //drugiPutKolona3 = false;
            //        //drugiPutKolona4 = false;



            //        prviPutKolona1 = true;

            //        prviPutKolona2 = false;
            //        prviPutKolona3 = false;
            //        prviPutKolona4 = false;
            //    }
                
                
            //}
            //else if (brdDockOprema.ActualWidth > 575 && brdDockOprema.ActualWidth < 860)
            //{
            //    aktivirajDetaljanPrikaz();
            //    if (!prviPutKolona2)
            //    {
            //        if (CurrentTipOpreme != 0)
            //        {
                        
            //            rasporediListuOpremePravilno();
                        
            //            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme);
            //            //this.popuniListuOprema(niz);

            //            //int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //            //double razlomak = brojProizvodaUListiOpreme / (double)2;
            //            //int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //            //int brojac = 0;
            //            //bool prekinuto = false;

            //            //List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //            //for (int i = 0; i < brojNovihLista; i++)
            //            //{
            //            //    List<Oprema> novaLista = new List<Oprema>();

            //            //    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //            //    {
            //            //        if (j % 2 == 0 && j != 0 && !prekinuto)
            //            //        {
            //            //            prekinuto = true;
            //            //            break;
            //            //        }
            //            //        novaLista.Add(this.documentManagerVM.Lista[j]);
            //            //        brojac++;
            //            //        prekinuto = false;

            //            //    }
            //            //    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //            //    novaListaZaListuSvihLista.NovaLista = novaLista;
            //            //    listaSvihLista.Add(novaListaZaListuSvihLista);
            //            //}
            //            //this.documentManagerVM.ListaSvihLista = listaSvihLista;
            //        }

            //        //if (ictOpremaRedovi.Items.Count > 0)
            //        //{
            //        //    if (timer2.IsEnabled)
            //        //        timer2.Stop();
            //        //    timer2.Interval = new TimeSpan(0, 0, 1);
            //        //    timer2.Tick += timer2_Tick;
            //        //    daLiJePosecenTimer = false;
            //        //    timer2.Start();
            //        //}
            //        prviPutKolona1 = false;

            //        prviPutKolona2 = true;

            //        prviPutKolona3 = false;
            //        prviPutKolona4 = false;
            //    }
               

            //}
            //else if (brdDockOprema.ActualWidth > 860 && brdDockOprema.ActualWidth < 1145)
            //{
            //    aktivirajDetaljanPrikaz();
            //    if (!prviPutKolona3)
            //    {
            //        if (CurrentTipOpreme != 0)
            //        {
                        
            //            rasporediListuOpremePravilno();
                        
            //            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme);
            //            //this.popuniListuOprema(niz);

            //            //int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //            //double razlomak = brojProizvodaUListiOpreme / (double)3;
            //            //int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //            //int brojac = 0;
            //            //bool prekinuto = false;

            //            //List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //            //for (int i = 0; i < brojNovihLista; i++)
            //            //{
            //            //    List<Oprema> novaLista = new List<Oprema>();

            //            //    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //            //    {
            //            //        if (j % 3 == 0 && j != 0 && !prekinuto)
            //            //        {
            //            //            prekinuto = true;
            //            //            break;
            //            //        }
            //            //        novaLista.Add(this.documentManagerVM.Lista[j]);
            //            //        brojac++;
            //            //        prekinuto = false;

            //            //    }
            //            //    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //            //    novaListaZaListuSvihLista.NovaLista = novaLista;
            //            //    listaSvihLista.Add(novaListaZaListuSvihLista);
            //            //}
            //            //this.documentManagerVM.ListaSvihLista = listaSvihLista;

            //        }
            //        //if (ictOpremaRedovi.Items.Count > 0)
            //        //{
            //        //    if (timer2.IsEnabled)
            //        //        timer2.Stop();
            //        //    timer2.Interval = new TimeSpan(0, 0, 1);
            //        //    timer2.Tick += timer2_Tick;
            //        //    daLiJePosecenTimer = false;
            //        //    timer2.Start();
            //        //}
            //        prviPutKolona1 = false;
            //        prviPutKolona2 = false;

            //        prviPutKolona3 = true;

            //        prviPutKolona4 = false;
            //    }
            //}
            //else if (brdDockOprema.ActualWidth > 1145)
            //{
            //    aktivirajDetaljanPrikaz();
            //    if (!prviPutKolona4)
            //    {

            //        if (CurrentTipOpreme != 0)
            //        {
                        
            //            rasporediListuOpremePravilno();
            //            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //            //SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] niz = service.OpremeSaParametrimaGlavniProzor(CurrentTipOpreme);
            //            //this.popuniListuOprema(niz);

            //            //int brojProizvodaUListiOpreme = this.documentManagerVM.Lista.Count;
            //            //double razlomak = brojProizvodaUListiOpreme / (double)4;
            //            //int brojNovihLista = Convert.ToInt32(Math.Ceiling(razlomak));

            //            //int brojac = 0;
            //            //bool prekinuto = false;

            //            //List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

            //            //for (int i = 0; i < brojNovihLista; i++)
            //            //{
            //            //    List<Oprema> novaLista = new List<Oprema>();

            //            //    for (int j = brojac; j < brojProizvodaUListiOpreme; j++)
            //            //    {
            //            //        if (j % 4 == 0 && j != 0 && !prekinuto)
            //            //        {
            //            //            prekinuto = true;
            //            //            break;
            //            //        }
            //            //        novaLista.Add(this.documentManagerVM.Lista[j]);
            //            //        brojac++;
            //            //        prekinuto = false;

            //            //    }
            //            //    NovaListaZaListuSvihLista novaListaZaListuSvihLista = new NovaListaZaListuSvihLista();
            //            //    novaListaZaListuSvihLista.NovaLista = novaLista;
            //            //    listaSvihLista.Add(novaListaZaListuSvihLista);
            //            //}
            //            //this.documentManagerVM.ListaSvihLista = listaSvihLista;
            //        }
            //        //if (ictOpremaRedovi.Items.Count > 0)
            //        //{
            //        //    if (timer2.IsEnabled)
            //        //        timer2.Stop();
            //        //    timer2.Interval = new TimeSpan(0, 0, 1);
            //        //    timer2.Tick += timer2_Tick;
            //        //    daLiJePosecenTimer = false;
            //        //    timer2.Start();
            //        //}
            //        prviPutKolona1 = false;
            //        prviPutKolona2 = false;
            //        prviPutKolona3 = false;

            //        prviPutKolona4 = true;
            //    }
            //}
        }
       
        

        void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2.IsEnabled)
            {
                DispatcherTimer senderTimer = (DispatcherTimer)sender;
                senderTimer.Stop();

                
                int brojRedova = ictOpremaRedovi.Items.Count;
                if (brojRedova > 0)
                {
                    this.brojacZaTextBlockLoaded = 0;
                    for (int i = 0; i < brojRedova; i++)
                    {
                        
                        ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                        ItemsControl ictKolone = FindChild<ItemsControl>(cp);
                        if (ictKolone.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                        {
                            int brojKolona = ictKolone.Items.Count;

                            for (int j = 0; j < brojKolona; j++)
                            {
                                
                                ContentPresenter cpa = ictKolone.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;
                                Border brdOprema = FindChild<Border>(cpa);

                                if (brojKolona > 1)
                                {
                                    //DoubleAnimation daOpacityZaBrdBorderi = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));


                                    brdOprema.BeginAnimation(Border.MarginProperty, taMarginaZaBrdBorderi);
                                    brdOprema.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
                                }
                                else
                                {
                                    //ThicknessAnimation taMarginaZaBrdBorderi = new ThicknessAnimation(new Thickness(35, 0, 0, 0), standardnaMargina, new TimeSpan(0, 0, 1));
                                    //DoubleAnimation daOpacityZaBrdBorderi = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

                                    brdOprema.BeginAnimation(Border.MarginProperty, taMarginaZaBrdBorderi);
                                    brdOprema.BeginAnimation(Border.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
                                }
                                TextBlock tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                                TextBlock tblckOpremaProizvodjac = brdOprema.FindName("tblckOpremaProizvodjac") as TextBlock;
                                TextBlock tblckOpremaModel = brdOprema.FindName("tblckOpremaModel") as TextBlock;
                                TextBlock tblckOpremaOpis = brdOprema.FindName("tblckOpremaOpis") as TextBlock;
                                TextBlock tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;
                                int id = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

                                if (this.documentManagerVM.Lista != null && this.documentManagerVM.Lista.Count > 0)
                                {
                                    if (tblckOpremaNaziv.ActualWidth > 240)
                                    {
                                        
                                            if (id == this.documentManagerVM.Lista[brojacZaTextBlockLoaded].IdOprema)
                                            {
                                                this.documentManagerVM.Lista[brojacZaTextBlockLoaded].DaLiTekstNaslovaZauzimaViseRedova = true;
                                                TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;

                                                tblckOpremaNazivDetaljnije.Visibility = Visibility.Visible;
                                                tblckOpremaNazivDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                            }
                                        


                                    }
                                    if (tblckOpremaProizvodjac.ActualWidth > 125)
                                    {
                                        

                                            if (id == this.documentManagerVM.Lista[brojacZaTextBlockLoaded].IdOprema)
                                            {
                                                this.documentManagerVM.Lista[brojacZaTextBlockLoaded].DaLiTekstProizvodjacaZauzimaViseRedova = true;
                                                TextBlock tblckOpremaProizvodjacDetaljnije = brdOprema.FindName("tblckOpremaProizvodjacDetaljnije") as TextBlock;
                                                //DoubleAnimation daTblckOpremaProizvodjacDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));
                                                tblckOpremaProizvodjacDetaljnije.Visibility = Visibility.Visible;
                                                tblckOpremaProizvodjacDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                            }
                                        

                                    }
                                    if (tblckOpremaModel.ActualWidth > 125)
                                    {

                                       
                                            if (id == this.documentManagerVM.Lista[brojacZaTextBlockLoaded].IdOprema)
                                            {
                                                this.documentManagerVM.Lista[brojacZaTextBlockLoaded].DaLiTekstModelaZauzimaViseRedova = true;
                                                TextBlock tblckOpremaModelDetaljnije = brdOprema.FindName("tblckOpremaModelDetaljnije") as TextBlock;
                                                //DoubleAnimation daTblckOpremaModelDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));
                                                tblckOpremaModelDetaljnije.Visibility = Visibility.Visible;
                                                tblckOpremaModelDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                            }
                                        

                                    }
                                    if (tblckOpremaOpis.ActualWidth > 125)
                                    {

                                        
                                            if (id == this.documentManagerVM.Lista[brojacZaTextBlockLoaded].IdOprema)
                                            {
                                                this.documentManagerVM.Lista[brojacZaTextBlockLoaded].DaLiTekstOpisaZauzimaViseRedova = true;
                                                TextBlock tblckOpremaOpisDetaljnije = brdOprema.FindName("tblckOpremaOpisDetaljnije") as TextBlock;
                                                //DoubleAnimation daTblckOpremaOpisDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));
                                                tblckOpremaOpisDetaljnije.Visibility = Visibility.Visible;
                                                tblckOpremaOpisDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                            }
                                        

                                    }

                                    if (tblckOpremaCena.ActualWidth > 125)
                                    {

                                        
                                            if (id == this.documentManagerVM.Lista[brojacZaTextBlockLoaded].IdOprema)
                                            {
                                                this.documentManagerVM.Lista[brojacZaTextBlockLoaded].DaLiTekstCeneZauzimaViseRedova = true;
                                                TextBlock tblckOpremaCenaDetaljnije = brdOprema.FindName("tblckOpremaCenaDetaljnije") as TextBlock;
                                                //DoubleAnimation daTblckOpremaCenaDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));
                                                tblckOpremaCenaDetaljnije.Visibility = Visibility.Visible;
                                                tblckOpremaCenaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                            }
                                        
                                    }

                                    if (this.trenutniBorderOprema != null)
                                    {


                                        int IdOprema = Convert.ToInt32((trenutniBorderOprema.FindName("skrivenId") as TextBlock).Text);
                                        if (IdOprema == id)
                                        {
                                            tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                                            TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
                                            Border brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

                                            Label lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
                                            Label lblModel = brdOprema.FindName("lblModel") as Label;
                                            Label lblOpis = brdOprema.FindName("lblOpis") as Label;
                                            Label lblCena = brdOprema.FindName("lblCena") as Label;
                                            tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;

                                            brdOpremaNaziv.Background = Brushes.Gainsboro;

                                            tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;

                                            selektovanRed = i + 1;



                                        }
                                    }
                                    this.brojacZaTextBlockLoaded++;
                                }
                                
                                //if (CurrentOprema != null)
                                //{
                                //    if (id == CurrentOprema.IdOprema)
                                //    {
                                //        //Rectangle rctSenkaZaBrdOprema = grdOprema.FindName("rctSenkaZaBrdOprema") as Rectangle;
                                //        //rctSenkaZaBrdOprema.Visibility = Visibility.Visible;

                                //        Border brdOpremaNaziv = grdOprema.FindName("brdOpremaNaziv") as Border;
                                //        brdOpremaNaziv.Background = Brushes.Transparent;
                                //        Label lblOpremaNaziv = brdOpremaNaziv.FindName("lblOpremaNaziv") as Label;
                                //        lblOpremaNaziv.Foreground = Brushes.Black;

                                //        this.trenutniGridOprema = grdOprema;
                                //        i = brojRedova;
                                //        daLiJePosecenTimer = true;
                                //        break;
                                //    }
                                //}
                                
                            }

                        }
                    }

                    if (aktivnaPretraga)
                    {
                        this.documentManagerVM.TekstPretrage = tbPretraga.Text;
                        this.StariTipOpreme = null;
                        this.CurrentOprema = null;
                        this.CurrentTipOpreme = null;
                        if (TrenutniGridTriVjuTipoviOpreme != null)
                        {
                            Border brdTriVjuTipoviOpremeNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdTriVjuTipoviOpremeNaziv") as Border;
                            brdTriVjuTipoviOpremeNaziv.Background = Brushes.Transparent;
                            TextBlock textblok = TrenutniGridTriVjuTipoviOpreme.FindName("textblok") as TextBlock;
                            textblok.Foreground = Brushes.Black;
                        }
                        this.TrenutniGridTriVjuTipoviOpreme = null;
                        
                        //DoubleAnimation daPrikaziPraznuOpremuZbogPretrage = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        //DoubleAnimation daPrikaziPraznuOpremu = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(0,1, new TimeSpan(0, 0, 0, 0, 500));
                        //DoubleAnimation daZatvoriGrdFilteri = new DoubleAnimation(0, new TimeSpan(0, 0, 1));

                        LejautDokumentOprema.Title = "Rezultati pretrage".ToUpper();

                        if(!prviPutUcitanaPretraga)
                        {
                            brdListaSaOpremomJePraznaZaOdabraniTipOpreme.Visibility = Visibility.Visible;
                            brdListaSaOpremomJePraznaZbogOdabranihFiltera.Visibility = Visibility.Visible;
                            brdListaSaOpremomJePraznaZbogPretrage.Visibility = Visibility.Visible;
                            skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;
                            prviPutUcitanaPretraga = true;
                        }
                        
                        brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaPrikaziSekund);
                        grdFilteri.BeginAnimation(Border.HeightProperty, daSakrijFiltere);

                        if (this.CurrentOprema != null && this.documentManagerVM.Lista != null)
                        {
                            bool skloniDetaljanPrikaz = true;
                            for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                            {
                                if (this.CurrentOprema.IdOprema == this.documentManagerVM.Lista[i].IdOprema)
                                {
                                    skloniDetaljanPrikaz = false;

                                    break;
                                }
                            }

                            if (skloniDetaljanPrikaz)
                            {
                                this.CurrentOprema = null;
                                this.trenutniBorderOprema = null;
                                aktivirajDetaljanPrikaz();
                            }

                        }
                        aktivnaPretraga = false;
                    }
                    else if (TrenutniParametarZaFilterChecked != null || TrenutniParametarZaFilterUnchecked != null)
                    {
                        //DoubleAnimation daPrikaziPraznuOpremuZbogFiltera = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        

                        if (this.CurrentOprema != null && this.documentManagerVM.Lista != null)
                        {
                            bool skloniDetaljanPrikaz = true;
                            for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                            {
                                if (this.CurrentOprema.IdOprema == this.documentManagerVM.Lista[i].IdOprema)
                                {
                                    skloniDetaljanPrikaz = false;

                                    break;
                                }
                            }

                            if (skloniDetaljanPrikaz)
                            {
                                this.CurrentOprema = null;
                                this.trenutniBorderOprema = null;
                                aktivirajDetaljanPrikaz();
                            }

                        }

                        //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaPrikaziPolaSekunde);

                        //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                        brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
                    }
                    else{

                    
                    //DoubleAnimation daPrikaziPraznuOpremuZbogFiltera = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                    //DoubleAnimation daPrikaziPraznuOpremu = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePrazna.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                    //DoubleAnimation daBrdListaSaOpremomJePraznaZaOdabraniTipOpreme = new DoubleAnimation(0, new TimeSpan(0, 0, 0, 0, 500));
                        brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

                    //DoubleAnimation daSakrijIctRedove = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        skrolvjuverPrikazTipoviOpreme.BeginAnimation(ScrollViewer.OpacityProperty, daAnimacijaPrikaziPolaSekunde);
                    }

                }
                
                if (selektovanRed > 0)
                {
                    visinaSkrolaZaOpremu = (selektovanRed * 180) - 180;
                    skrolvjuverPrikazTipoviOpreme.ScrollToVerticalOffset(visinaSkrolaZaOpremu);
                }
                //aktivirajDetaljanPrikaz();
                izmenjenaOprema = false;
                TrenutniParametarZaFilterChecked = null;
                TrenutniParametarZaFilterUnchecked = null;
                timer2.Stop();
            }
            
        }

        private void Expander_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tgb = (ToggleButton) sender;
            if (tgb.IsMouseOver)
            {
                var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
                if (treeViewItem != null)
                {

                    Grid grdHierarchical = ((tgb.Parent) as Grid).Parent as Grid;
                    Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
                    Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
                    Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
                    Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;
                    //ToggleButton tgb = grdHierarchical.FindName("Expander") as ToggleButton;

                    tgb.Width = 30;

                    Thickness margina = grdHierarchical.Margin;
                    margina.Left = -30;
                    grdHierarchical.Margin = margina;

                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleHoverovano");

                    if (treeViewItem.IsExpanded)
                    {

                        brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                        txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;


                        treeViewItem.IsExpanded = false;
                    }
                    else
                    {
                        brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
                        txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                        treeViewItem.IsExpanded = true;

                    }
                }
            }
        }

        private void Expander_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton tgb = (ToggleButton) sender;
            if (tgb.IsMouseOver)
            {
                var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
                if (treeViewItem != null)
                {

                    Grid grdHierarchical = ((tgb.Parent) as Grid).Parent as Grid;
                    Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
                    Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;
                    Border brdAktivno = grdHierarchical.FindName("brdAktivno") as Border;
                    Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;
                    //ToggleButton tgb = grdHierarchical.FindName("Expander") as ToggleButton;
                    tgb.Width = 0;
                    Thickness margina = grdHierarchical.Margin;
                    margina.Left = 0;
                    grdHierarchical.Margin = margina;

                    tgb.SetResourceReference(StyleProperty, "ExpandCollapseToggleStyleHoverovano");

                    if (treeViewItem.IsExpanded)
                    {

                        brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                        txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;


                        treeViewItem.IsExpanded = false;
                    }
                    else
                    {
                        brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
                        
                        txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                        treeViewItem.IsExpanded = true;

                    }
                }
            }
        }

        private void trivju_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
                e.Handled = true;
        }

        DispatcherTimer timer3 = new DispatcherTimer();
        bool kliknutoNaProzorJednom = false;
        bool kliknutoNaProzorDvaput = false;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if(recNijeAktivanProzor.Visibility == Visibility.Visible)
            //    SystemSounds.Beep.Play();
            //if (kliknutoNaProzorJednom)
            //    kliknutoNaProzorDvaput = true;
            //kliknutoNaProzorJednom = true;
            //if (!timer3.IsEnabled)
            //{
            //    timer3.Interval = new TimeSpan(0, 0, 0, 0, 200);
            //    timer3.Tick += timer3_Tick;
            //    timer3.Start();
            //}
        }

        void timer3_Tick(object sender, EventArgs e)
        {
            if (kliknutoNaProzorJednom && kliknutoNaProzorDvaput)
            {
                if (this.WindowState == System.Windows.WindowState.Maximized)
                    this.WindowState = System.Windows.WindowState.Normal;
                else
                    this.WindowState = System.Windows.WindowState.Maximized;
            }
            kliknutoNaProzorJednom = false;
            kliknutoNaProzorDvaput = false;
            timer3.Stop();
        }

        

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                grdCeoSadrzaj.Margin = new Thickness(0);
                dugmeMaximize.SetResourceReference(StyleProperty, "dugmeMaximize");

            }

            else
            {
                grdCeoSadrzaj.Margin = new Thickness(8);
                dugmeMaximize.SetResourceReference(StyleProperty, "dugmeRestore");
            }
        }


        DispatcherTimer timerproba;
        double novaSirinabrdPrikazDetaljaOpreme = 0;
        double staraSirinabrdPrikazDetaljaOpreme = 0;
        bool aktivanDetaljanPrikaz = false;
        private void brdOprema_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            skrolVjuverEdit.Visibility = Visibility.Hidden;
            skrolVjuverPrikaz.Visibility = Visibility.Visible;

            Border brdOprema, brdOpremaNaziv;
            int IdOprema; 
            //int trenutniId = Convert.ToInt32(((sender as Border).FindName("skrivenId") as TextBlock).Text);
            TextBlock tblckOpremaNaziv, tblckOpremaNazivDetaljnije, tblckOpremaCena;
            Label lblProizvodjac, lblModel, lblOpis, lblCena;
            
            if (this.trenutniBorderOprema != null)
            {
                int brojRedova = ictOpremaRedovi.Items.Count;
                if (brojRedova > 0)
                {
                    this.brojacZaTextBlockLoaded = 0;
                    for (int i = 0; i < brojRedova; i++)
                    {

                        ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                        ItemsControl ictKolone = FindChild<ItemsControl>(cp);
                        if (ictKolone.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                        {
                            int brojKolona = ictKolone.Items.Count;

                            for (int j = 0; j < brojKolona; j++)
                            {

                                ContentPresenter cpa = ictKolone.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;
                                brdOprema = FindChild<Border>(cpa);
                                IdOprema = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);
                                int trenutniIdOprema = Convert.ToInt32((trenutniBorderOprema.FindName("skrivenId") as TextBlock).Text);
                                if (IdOprema == trenutniIdOprema)
                                {
                                    IdOprema = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

                                    tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                                    tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
                                    brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

                                    lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
                                    lblModel = brdOprema.FindName("lblModel") as Label;
                                    lblOpis = brdOprema.FindName("lblOpis") as Label;
                                    lblCena = brdOprema.FindName("lblCena") as Label;
                                    tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;



                                    brdOpremaNaziv.Background = Brushes.White;
                                    tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = Brushes.Black;

                                    lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = Brushes.Gray;
                                    break;
                                }

                            }
                        }
                    }
                }
                
                
                
            }

            brdOprema = sender as Border;
            IdOprema = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

            tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
            tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
            brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

            lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
            lblModel = brdOprema.FindName("lblModel") as Label;
            lblOpis = brdOprema.FindName("lblOpis") as Label;
            lblCena = brdOprema.FindName("lblCena") as Label;
            tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;

            brdOpremaNaziv.Background = Brushes.Gainsboro;

            tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;

            trenutniBorderOprema = brdOprema;
            if (this.documentManagerVM.Lista != null)
            {
                for (int i = 0; i < this.documentManagerVM.Lista.Count; i++)
                {
                    if (IdOprema == this.documentManagerVM.Lista[i].IdOprema)
                    {
                        this.CurrentOprema = this.documentManagerVM.Lista[i];
                        break;
                    }
                }
            }
            if(this.CurrentOprema != null)
                pera2.Content = this.CurrentOprema;
            pera2.Visibility = Visibility.Visible;
            
            skrolVjuverPrikaz.Visibility = Visibility.Visible;
            //if ((brdCentralniSadrzaj.ActualWidth / 3) > 450)
            //    novaSirinabrdPrikazDetaljaOpreme = 450;
            //else if ((brdCentralniSadrzaj.ActualWidth / 3) < 350)
            //    novaSirinabrdPrikazDetaljaOpreme = 350;
            //else novaSirinabrdPrikazDetaljaOpreme = brdCentralniSadrzaj.ActualWidth / 3;
            //this.brdPrikazDetaljaOpreme.Width = novaSirinabrdPrikazDetaljaOpreme;
            aktivirajDetaljanPrikaz();

            
            //int brojRedova = ictOpremaRedovi.Items.Count;
            //    if (brojRedova > 0)
            //    {
            //        this.brojacZaTextBlockLoaded = 0;
            //        for (int i = 0; i < brojRedova; i++)
            //        {
            //            ContentPresenter cp = ictOpremaRedovi.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
            //            ItemsControl ictKolone = FindChild<ItemsControl>(cp);
            //            if (ictKolone.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            //            {
            //                int brojKolona = ictKolone.Items.Count;
            //                for (int j = 0; j < brojKolona; j++)
            //                {
            //                    ContentPresenter cpa = ictKolone.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;
            //                    Border brdOpremaTest = FindChild<Border>(cpa);
            //                    int idTest  = Convert.ToInt32((brdOpremaTest.FindName("skrivenId") as TextBlock).Text);
            //                    if(IdOprema == idTest)
            //                    {
            //                        selektovanRed = i;
            //                        break;
            //                    }
                                
            //                }
            //            }
            //            if (selektovanRed > 0) break;
            //        }
            //    }


            //if(selektovanRed > 0)
            //{
            //    visinaSkrolaZaOpremu = (selektovanRed * 200) - 200;
            //    skrolvjuverPrikazTipoviOpreme.ScrollToVerticalOffset(visinaSkrolaZaOpremu);
            //}

        }
        double tempStaraSirinaDetaljnogPrikaza = 0;
        DispatcherTimer timerZaBrdPrikazDetaljaOpreme = new DispatcherTimer(); 
        DispatcherTimer timerPrikaziTooltipZaPrikazDetaljaOpreme = new DispatcherTimer();
        public void aktivirajDetaljanPrikaz()
        {

            bool pomerenBorder = false;
                
           
            

           
            if (this.CurrentOprema != null)
            {
                if ((brdCentralniSadrzaj.ActualWidth / 3) > 450)
                    novaSirinabrdPrikazDetaljaOpreme = 450;
                else if ((brdCentralniSadrzaj.ActualWidth / 3) < 350)
                    novaSirinabrdPrikazDetaljaOpreme = 350;
                else novaSirinabrdPrikazDetaljaOpreme = brdCentralniSadrzaj.ActualWidth / 3;


                if (novaSirinabrdPrikazDetaljaOpreme != staraSirinabrdPrikazDetaljaOpreme)
                {
                    DoubleAnimation daProba = new DoubleAnimation(staraSirinabrdPrikazDetaljaOpreme, novaSirinabrdPrikazDetaljaOpreme, new TimeSpan(0, 0, 0, 0, 300));
                    brdPrikazDetaljaOpreme.BeginAnimation(Border.WidthProperty, daProba);
                    staraSirinabrdPrikazDetaljaOpreme = novaSirinabrdPrikazDetaljaOpreme;
                    novaSirinabrdPrikazDetaljaOpreme = 0;
                    pomerenBorder = true;
                    //rasporediListuOpremePravilno();

                    
                    
                }

                if(!timerPrikaziTooltipZaPrikazDetaljaOpreme.IsEnabled)
                {
                    timerPrikaziTooltipZaPrikazDetaljaOpreme = new DispatcherTimer();
                    timerPrikaziTooltipZaPrikazDetaljaOpreme.Interval = new TimeSpan(0,0,0,0,200);
                    timerPrikaziTooltipZaPrikazDetaljaOpreme.Tick += timerPrikaziTooltipZaPrikazDetaljaOpreme_Tick;
                    timerPrikaziTooltipZaPrikazDetaljaOpreme.Start();
                }


                

            }
            else
            {
                novaSirinabrdPrikazDetaljaOpreme = 0;
                if (novaSirinabrdPrikazDetaljaOpreme != staraSirinabrdPrikazDetaljaOpreme)
                {
                    DoubleAnimation daProba = new DoubleAnimation(staraSirinabrdPrikazDetaljaOpreme, novaSirinabrdPrikazDetaljaOpreme, new TimeSpan(0, 0, 0, 0, 300));

                    brdPrikazDetaljaOpreme.BeginAnimation(Border.WidthProperty, daProba);

                    if (novaSirinabrdPrikazDetaljaOpreme == 0 && this.CurrentOprema == null)
                    {
                        tempStaraSirinaDetaljnogPrikaza = staraSirinabrdPrikazDetaljaOpreme;
                        nemaDetaljnogPrikaza = true;

                    }
                    pomerenBorder = true;
                    staraSirinabrdPrikazDetaljaOpreme = novaSirinabrdPrikazDetaljaOpreme;
                    novaSirinabrdPrikazDetaljaOpreme = 0;
                    
                }

            }
            //if (timerZaBrdPrikazDetaljaOpreme.IsEnabled && pomerenBorder)
            //{
            //    timerZaBrdPrikazDetaljaOpreme.Stop();
            //}
            //if (!timerZaBrdPrikazDetaljaOpreme.IsEnabled && pomerenBorder)
            //{
            //    timerZaBrdPrikazDetaljaOpreme = new DispatcherTimer();
            //    timerZaBrdPrikazDetaljaOpreme.Interval = new TimeSpan(0, 0, 0, 0, 40);
            //    timerZaBrdPrikazDetaljaOpreme.Tick += timerZaBrdPrikazDetaljaOpreme_Tick;
            //    timerZaBrdPrikazDetaljaOpreme.Start();
            //}

            // if (staraSirinaDoka > 0 && !nemaDetaljnogPrikaza && !timerZaBrdPrikazDetaljaOpreme.IsEnabled)
            //{
             
            //    brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(staraSirinaDoka / sirinaJednogObjektaOpreme)));
            //    brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(novaSirinaDokaOprema / sirinaJednogObjektaOpreme)));
              

            //    if (brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok)
            //    {
            //        rasporediListuOpremePravilno();
            //    }
            
            //staraSirinabrdPrikazDetaljaOpreme = novaSirinabrdPrikazDetaljaOpreme;
            


            //else novaSirinabrdPrikazDetaljaOpreme = 0;

                
            //        if (staraSirinabrdPrikazDetaljaOpreme != novaSirinabrdPrikazDetaljaOpreme)
            //        {
                        

                        
            //            if (novaSirinabrdPrikazDetaljaOpreme == 0 && this.CurrentOprema == null)
            //            {
            //                nemaDetaljnogPrikaza = true;
                            
            //            }
            //            staraSirinabrdPrikazDetaljaOpreme = novaSirinabrdPrikazDetaljaOpreme;
            //            novaSirinabrdPrikazDetaljaOpreme = 0;
            //        }
                    
            //        if (nemaDetaljnogPrikaza&& !timerZaBrdPrikazDetaljaOpreme.IsEnabled)
            //        {
            //            timerZaBrdPrikazDetaljaOpreme = new DispatcherTimer();
            //            timerZaBrdPrikazDetaljaOpreme.Interval = new TimeSpan(0, 0, 0, 0, 600);
            //            timerZaBrdPrikazDetaljaOpreme.Tick += timerZaBrdPrikazDetaljaOpreme_Tick;
            //            timerZaBrdPrikazDetaljaOpreme.Start();
            //        }


            //        if (staraSirinaDoka > 0 && !nemaDetaljnogPrikaza && !timerZaBrdPrikazDetaljaOpreme.IsEnabled)
            //    {
                   
            //        brojKolonaKojiMozeDaStaneUStariiDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(staraSirinaDoka / sirinaJednogObjektaOpreme)));
            //        brojKolonaKojiMozeDaStaneUNoviDok = Convert.ToInt32(Math.Floor(Convert.ToDecimal(novaSirinaDokaOprema / sirinaJednogObjektaOpreme)));
                    

            //        if (brojKolonaKojiMozeDaStaneUNoviDok != brojKolonaKojiMozeDaStaneUStariiDok)
            //        {
            //            rasporediListuOpremePravilno();
            //        }
            //    }
                //if (this.CurrentOprema == null)
                //{
                //    rasporediListuOpremePravilno();
                //}
                
                //if (aktivanDetaljanPrikaz && !timerZaBrdPrikazDetaljaOpreme.IsEnabled)
                //{
                //    timerZaBrdPrikazDetaljaOpreme = new DispatcherTimer();
                //    timerZaBrdPrikazDetaljaOpreme.Interval = new TimeSpan(0, 0, 2);
                //    timerZaBrdPrikazDetaljaOpreme.Tick += timerZaBrdPrikazDetaljaOpreme_Tick;
                //    timerZaBrdPrikazDetaljaOpreme.Start();
                //}
                //if (dablProba > 0 && this.CurrentOprema != null && aktivanDetaljanPrikaz)
                //{
                    
                //    DoubleAnimation daProba = new DoubleAnimation(staraSirinaBrdPrikazDetaljaOpreme, dablProba, new TimeSpan(0, 0, 1));


                //    brdPrikazDetaljaOpreme.BeginAnimation(Border.WidthProperty, daProba);
                //    //brdPrikazDetaljaOpreme.Width = dablProba;
                //}




                
            
            
        }

        void timerPrikaziTooltipZaPrikazDetaljaOpreme_Tick(object sender, EventArgs e)
        {
            if(timerPrikaziTooltipZaPrikazDetaljaOpreme.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();

                Grid grdPera2 = FindChild<Grid>(pera2) as Grid;
                if (grdPera2 != null)
                {
                    TextBlock tblckPera2Naslov = grdPera2.FindName("tblckPera2Naslov") as TextBlock;
                    if (tblckPera2Naslov.ActualWidth > brdPrikazDetaljaOpreme.ActualWidth)
                    {
                        if (this.CurrentOprema != null)
                        {
                            this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme = true;
                            TextBlock tblckPera2OpremaNazivDetaljnije = grdPera2.FindName("tblckPera2OpremaNazivDetaljnije") as TextBlock;
                            //DoubleAnimation daTblckPera2OpremaNazivDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));
                            tblckPera2OpremaNazivDetaljnije.Visibility = Visibility.Visible;
                            tblckPera2OpremaNazivDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);
                        }

                    }
                    else
                    {
                        if (this.CurrentOprema != null)
                        {
                            this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme = false;
                            TextBlock tblckPera2OpremaNazivDetaljnije = grdPera2.FindName("tblckPera2OpremaNazivDetaljnije") as TextBlock;
                            //DoubleAnimation daTblckPera2OpremaNazivDetaljnijeSirina = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                            tblckPera2OpremaNazivDetaljnije.Visibility = Visibility.Hidden;
                            tblckPera2OpremaNazivDetaljnije.Width = 0;
                        }
                        //tblckPera2OpremaNazivDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTblckPera2OpremaNazivDetaljnijeSirina);
                    }
                }


            }
            timerPrikaziTooltipZaPrikazDetaljaOpreme.Stop();
        }

        //void timerZaBrdPrikazDetaljaOpreme_Tick(object sender, EventArgs e)
        //{
        //    if (timerZaBrdPrikazDetaljaOpreme.IsEnabled)
        //    {
        //        //double tempStaraSirinaDetaljnogPrikaza = staraSirinabrdPrikazDetaljaOpreme;
        //        //novaSirinaDokaOprema = brdDockOprema.ActualWidth;
        //        //if (nemaDetaljnogPrikaza && this.CurrentOprema == null)
        //        //{
        //        //    novaSirinaDokaOprema += tempStaraSirinaDetaljnogPrikaza;

        //        //}
        //        rasporediListuOpremePravilno();
        //    }
        //    DispatcherTimer timerSender = (DispatcherTimer)sender;
        //    timerSender.Stop();
        //    timerZaBrdPrikazDetaljaOpreme.Stop();
        //}

        //void timerZaBrdPrikazDetaljaOpreme_Tick(object sender, EventArgs e)
        //{
        //    if (timerZaBrdPrikazDetaljaOpreme.IsEnabled)
        //    {

                
        //        aktivanDetaljanPrikaz = false;
        //        timerZaBrdPrikazDetaljaOpreme.Stop();
        //        DispatcherTimer timerSender = (DispatcherTimer)sender;
        //        timerSender.Stop();
        //    }
            
        //    //timerZaBrdPrikazDetaljaOpreme = new DispatcherTimer();
            
        //}

        
        private void brdOprema_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brdOprema = sender as Border;
            int IdOprema = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

            TextBlock tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
            TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
            
            Border brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

            Label lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
            Label lblModel = brdOprema.FindName("lblModel") as Label;
            Label lblOpis = brdOprema.FindName("lblOpis") as Label;
            Label lblCena = brdOprema.FindName("lblCena") as Label;
            TextBlock tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;


            
            brdOpremaNaziv.Background = Brushes.Gainsboro;
            //brdOpremaNaziv.BorderBrush = Brushes.Gainsboro;

            tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
            
        }

        private void brdOprema_MouseLeave(object sender, MouseEventArgs e)
        {
            Border brdOprema = sender as Border;
            int trenutniId = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

            if (this.trenutniBorderOprema != null)
            {

                
                int IdOprema = Convert.ToInt32((trenutniBorderOprema.FindName("skrivenId") as TextBlock).Text);
                if (IdOprema != trenutniId)
                {
                    TextBlock tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                    TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
                    Border brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

                    Label lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
                    Label lblModel = brdOprema.FindName("lblModel") as Label;
                    Label lblOpis = brdOprema.FindName("lblOpis") as Label;
                    Label lblCena = brdOprema.FindName("lblCena") as Label;
                    TextBlock tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;



                    brdOpremaNaziv.Background = Brushes.White;
                    tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = Brushes.Black;

                    lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = Brushes.Gray;
                }
            }
            else
            {
                TextBlock tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;
                Border brdOpremaNaziv = brdOprema.FindName("brdOpremaNaziv") as Border;

                Label lblProizvodjac = brdOprema.FindName("lblProizvodjac") as Label;
                Label lblModel = brdOprema.FindName("lblModel") as Label;
                Label lblOpis = brdOprema.FindName("lblOpis") as Label;
                Label lblCena = brdOprema.FindName("lblCena") as Label;
                TextBlock tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;



                brdOpremaNaziv.Background = Brushes.White;
                tblckOpremaNaziv.Foreground = tblckOpremaNazivDetaljnije.Foreground = lblCena.Foreground = tblckOpremaCena.Foreground = Brushes.Black;

                lblProizvodjac.Foreground = lblModel.Foreground = lblOpis.Foreground = Brushes.Gray;
            }


            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //brdListaSaOpremomJePraznaZaOdabraniTipOpreme.Visibility = Visibility.Visible;
            //brdListaSaOpremomJePraznaZaOdabraniTipOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
            //brdListaSaOpremomJePraznaZbogOdabranihFiltera.Visibility = Visibility.Visible;
            //brdListaSaOpremomJePraznaZbogOdabranihFiltera.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
            //brdListaSaOpremomJePraznaZbogPretrage.Visibility = Visibility.Visible;
            //brdListaSaOpremomJePraznaZbogPretrage.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);
            //skrolvjuverPrikazTipoviOpreme.Visibility = Visibility.Visible;
            //skrolvjuverPrikazTipoviOpreme.BeginAnimation(Border.OpacityProperty, daAnimacijaSakrijPolaSekunde);

        }

        private void filteriPrikaz_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void brdFilteriZaPrikaz_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brdFilteriZaPrikaz = (Border)sender;
            Label lblNazivFiltera = brdFilteriZaPrikaz.FindName("lblNazivFiltera") as Label;
            lblNazivFiltera.Foreground = new BrushConverter().ConvertFrom("#FF2F5778") as SolidColorBrush;
        }

        private void brdFilteriZaPrikaz_MouseLeave(object sender, MouseEventArgs e)
        {
            Border brdFilteriZaPrikaz = (Border)sender;
            Label lblNazivFiltera = brdFilteriZaPrikaz.FindName("lblNazivFiltera") as Label;
            lblNazivFiltera.Foreground = Brushes.Black;
        }

        private void btnIzmeniSliku_Click(object sender, RoutedEventArgs e)
        {
            Button senderIzmenaSlike = sender as Button;
            Image imgSlikaZaIzmenuSlike = ((senderIzmenaSlike.Parent) as Grid).FindName("imgSlikaZaIzmenuSlike") as Image;
            OpenFileDialog ofdOprema = new OpenFileDialog();
            ofdOprema.Filter = "Image file (*.jpg)|*.jpg";
            if (ofdOprema.ShowDialog() ?? false)
            {
                if (this.documentManagerVM.TrenutnaOpremaZaMenjanje != null)
                {
                    this.documentManagerVM.TrenutnaOpremaZaMenjanje.Slika = ofdOprema.FileName;
                }
            }
        }

        

        

        

        

    }
}
