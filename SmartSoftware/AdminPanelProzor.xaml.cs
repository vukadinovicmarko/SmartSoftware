using Microsoft.Win32;
using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using SmartSoftware.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace SmartSoftware
{
        
 

    public enum TipParametara
    {
        //comboBox1.DataSource = Enum.GetValues(typeof(BlahEnum));
        STRING,
        BIT,
        TEXT,
        INT,
    }

    public partial class AdminPanelProzor : Window, INotifyPropertyChanged
    {
        //MediaTimeline.DesiredFrameRateProperty.OverrideMetadata(typeof(System.Windows.Media.Animation.Timeline), new FrameworkPropertyMetadata(10));
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private bool ucitanProzorPoPrviPut = false;
        ControlTemplate ctpKorisnici = new ControlTemplate();

        ICollectionView oblasti;

        private Visibility vidljivostDugmetaUcitajJosNarudzbina = Visibility.Hidden;

        public Visibility VidljivostDugmetaUcitajJosNarudzbina
        {
            get { return vidljivostDugmetaUcitajJosNarudzbina; }
            set { SetAndNotify(ref vidljivostDugmetaUcitajJosNarudzbina, value); }
        }

        private Orientation orijentacijaPrikazaOpremeUIstorijiKupovine = Orientation.Vertical;

        public Orientation OrijentacijaPrikazaOpremeUIstorijiKupovine
        {
            get { return orijentacijaPrikazaOpremeUIstorijiKupovine; }
            set { SetAndNotify(ref orijentacijaPrikazaOpremeUIstorijiKupovine, value); }
        }


        private string vratiTextZaDugmeSearch;

        public string VratiTextZaDugmeSearch
        {
            get { return vratiTextZaDugmeSearch; }
            set { SetAndNotify(ref vratiTextZaDugmeSearch, value); }
        }

        private string vratiTextZaDugmeInsert;

        public string VratiTextZaDugmeInsert
        {
            get { return vratiTextZaDugmeInsert; }
            set { SetAndNotify(ref vratiTextZaDugmeInsert, value); }
        }



        private const double opasiti0 = 0;
        private const double opasiti1 = 1;
        private const double opasiti15 = 15;
        private const double opasiti187 = 187;
        private static double trenutnaVrednostSirine = 187;

        public double TrenutnaVrednostSirine
        {
            get { return trenutnaVrednostSirine; }
            set { SetAndNotify(ref trenutnaVrednostSirine, value); }
        }


        private static TimeSpan trajanjeDveSekunde = new TimeSpan(0, 0, 2);
        private static TimeSpan trajanjeJedanSekund = new TimeSpan(0, 0, 1);
        private static TimeSpan trajanjePolaSekunde = new TimeSpan(0, 0, 0, 0, 500);
        private static TimeSpan trajanje200 = new TimeSpan(0, 0, 0, 0, 200);




        private static Thickness marginaLevo250 = new Thickness(-250, 0, 0, 0);
        private static Thickness marginaLevo5 = new Thickness(-5, 0, 0, 0);
        private static Thickness marginaDesno250 = new Thickness(250, 0, 0, 0);
        private static Thickness marginaDesno15 = new Thickness(15, 0, 0, 0);
        private static Thickness marginaCentar = new Thickness(0);
        private static Thickness marginaGore500 = new Thickness(0, 500, 0, 0);
        private static Thickness thicknessProba = new Thickness(-20, 0, 0, 0);

        private static ThicknessAnimation taPomeriUlevoPolaSekunde = new ThicknessAnimation(marginaLevo250, trajanjePolaSekunde);
        private static ThicknessAnimation taPomeriSDesnaUNormaluPolaSekunde = new ThicknessAnimation(marginaDesno250, marginaCentar, trajanjePolaSekunde);
        private static ThicknessAnimation taPomeriSLevaUNormaluPolaSekunde = new ThicknessAnimation(marginaLevo250, marginaCentar, trajanjePolaSekunde);
        private static ThicknessAnimation taPomeriNaDolePolaSekunde = new ThicknessAnimation(marginaGore500, trajanjePolaSekunde);
        private static ThicknessAnimation taNazivKategorijeMargina = new ThicknessAnimation(marginaLevo5, marginaCentar, trajanje200);
        private static ThicknessAnimation taPomeniDesnoZa15 = new ThicknessAnimation(marginaDesno15, trajanje200);
        private static ThicknessAnimation taMarginaVratiUNormalu = new ThicknessAnimation(marginaCentar, trajanje200);
        private static ThicknessAnimation taProba = new ThicknessAnimation(marginaCentar, thicknessProba, trajanjePolaSekunde);
        private static ThicknessAnimation taProba2 = new ThicknessAnimation(thicknessProba, marginaCentar, trajanjePolaSekunde);

        private static DoubleAnimation daPrikaziPolaSekunde = new DoubleAnimation(opasiti1, trajanjePolaSekunde);
        private static DoubleAnimation daSakrijPolaSekunde = new DoubleAnimation(opasiti0, trajanjePolaSekunde);
        private static DoubleAnimation daPrikaziSekund = new DoubleAnimation(opasiti1, trajanjeJedanSekund);
        private static DoubleAnimation daSakrijSekund = new DoubleAnimation(opasiti0, trajanjeJedanSekund);
        private static DoubleAnimation daSakrijDveSekunde = new DoubleAnimation(opasiti0, trajanjeDveSekunde);

        private static DoubleAnimation daBrdTriVjuHierarchicalNazivSirina = new DoubleAnimation(opasiti0, opasiti187, trajanje200);
        private static DoubleAnimation daTriTackeZaDetalje = new DoubleAnimation(opasiti0, opasiti15, trajanjeJedanSekund);



        private bool daLiJeListaOblastiOpremeOsvezena = false;

        private Visibility vidljivostTitla = Visibility.Hidden;
        public Visibility VidljivostTitla
        {
            get { return vidljivostTitla; }
            set { SetAndNotify(ref vidljivostTitla, value); }
        }


        private string izabraniNazivUloge;

        public string IzabraniNazivUloge
        {
            get { return izabraniNazivUloge; }
            set { SetAndNotify(ref izabraniNazivUloge, value); }
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



        private int kojeDugmeJePoReduKliknuto = 1;
        private int kojeDugmeJePoReduKliknutoRanije = 1;





        private ObservableCollection<String> listaUloga = new ObservableCollection<String>() { "Administrator", "Prodavac" };

        public ObservableCollection<String> ListaUloga
        {
            get { return listaUloga; }
            set { listaUloga = value; }
        }

        private IstorijaKupovine currentIstorijaKupovineKlasicanPrikaz;

        public IstorijaKupovine CurrentIstorijaKupovineKlasicanPrikaz
        {
            get { return currentIstorijaKupovineKlasicanPrikaz; }
            set { SetAndNotify(ref currentIstorijaKupovineKlasicanPrikaz, value); }
        }


        private ObservableCollection<KupljenaOprema> currentKupljenaOprema;

        public ObservableCollection<KupljenaOprema> CurrentKupljenaOprema
        {
            get { return currentKupljenaOprema; }
            set { SetAndNotify(ref currentKupljenaOprema, value); }
        }

        private IstorijaKupovine tempIstorijaKupovine = new IstorijaKupovine();

        public IstorijaKupovine TempIstorijaKupovine
        {
            get { return tempIstorijaKupovine; }
            set { SetAndNotify(ref tempIstorijaKupovine, value); }
        }

        private ObservableCollection<KupljenaOprema> currentOpremaZaIstorijuKupovine = new ObservableCollection<KupljenaOprema>();

        public ObservableCollection<KupljenaOprema> CurrentOpremaZaIstorijuKupovine
        {
            get { return currentOpremaZaIstorijuKupovine; }
            set { SetAndNotify(ref currentOpremaZaIstorijuKupovine, value); }
        }


        private ObservableCollection<String> listaUlogaZaUnos = new ObservableCollection<String>() { "Odaberite ulogu...", "Administrator", "Prodavac" };

        public ObservableCollection<String> ListaUlogaZaUnos
        {
            get { return listaUlogaZaUnos; }
            set { SetAndNotify(ref listaUlogaZaUnos, value); }
        }


        OblastiOpreme oo;
        Oprema o;
        TipoviOpreme t;
        Korisnici k;
        IstorijaKupovine i;
        Kupci ku;


        private ObservableCollection<String> listaUpitaZaKupca = new ObservableCollection<string>() { "Datumu kupovine", "Imenu", "Broju kupovina", "Potrošenom novcu" };

        public ObservableCollection<String> ListaUpitaZaKupca
        {
            get { return listaUpitaZaKupca; }
            //set { listaUpitaZaKupca = value; }
        }



        private ObservableCollection<OblastiOpreme> listaZaSve = new ObservableCollection<OblastiOpreme>();

        public ObservableCollection<OblastiOpreme> ListaZaSve
        {
            get { return listaZaSve; }
            set { SetAndNotify(ref listaZaSve, value); }
        }

        private ObservableCollection<OblastiOpreme> listaOblastiOpremeZaTipoveOpreme = new ObservableCollection<OblastiOpreme>();

        public ObservableCollection<OblastiOpreme> ListaOblastiOpremeZaTipoveOpreme
        {
            get { return listaOblastiOpremeZaTipoveOpreme; }
            set { SetAndNotify(ref listaOblastiOpremeZaTipoveOpreme, value); }
        }
        



        private int? izabranaOblastOpreme;
        public int? IzabranaOblastOpreme
        {
            get { return izabranaOblastOpreme; }
            set { SetAndNotify(ref izabranaOblastOpreme, value); }
        }

        private int? izabranaOblastOpremeZaUnos;
        public int? IzabranaOblastOpremeZaUnos
        {
            get { return izabranaOblastOpremeZaUnos; }
            set { SetAndNotify(ref izabranaOblastOpremeZaUnos, value); }
        }


        public ObservableCollection<Grid> listaVrednostiParametaraTemp = new ObservableCollection<Grid>();

        public ObservableCollection<Grid> ListaVrednostiParametaraTemp
        {
            get { return listaVrednostiParametaraTemp; }
            set { SetAndNotify(ref listaVrednostiParametaraTemp, value); }
        }


        private Parametri currentParametri;

        public Parametri CurrentParametri
        {
            get { return currentParametri; }
            set { SetAndNotify(ref currentParametri, value); }
        }

        ObservableCollection<OblastiOpreme> listaOblastiOpreme = new ObservableCollection<OblastiOpreme>();

        public ObservableCollection<OblastiOpreme> ListaOblastiOpreme
        {
            get { return listaOblastiOpreme; }
            set { SetAndNotify(ref listaOblastiOpreme, value); }
        }

        ObservableCollection<OblastiOpreme> listaOblastiOpremeZaUnos = new ObservableCollection<OblastiOpreme>();

        public ObservableCollection<OblastiOpreme> ListaOblastiOpremeZaUnos
        {
            get { return listaOblastiOpremeZaUnos; }
            set { SetAndNotify(ref listaOblastiOpremeZaUnos, value); }
        }




        ObservableCollection<TipoviOpreme> listaTipovaOpreme = new ObservableCollection<TipoviOpreme>();

        public ObservableCollection<TipoviOpreme> ListaTipovaOpreme
        {
            get { return listaTipovaOpreme; }
            set { SetAndNotify(ref listaTipovaOpreme, value); }
        }
        ObservableCollection<TipoviOpreme> listaTipovaOpremeZaUnosOpreme = new ObservableCollection<TipoviOpreme>();

        public ObservableCollection<TipoviOpreme> ListaTipovaOpremeZaUnosOpreme
        {
            get { return listaTipovaOpremeZaUnosOpreme; }
            set { SetAndNotify(ref listaTipovaOpremeZaUnosOpreme, value); }
        }



        private ObservableCollection<TipoviKofiguracije> listaTipovaZaKonfiguraciju = new ObservableCollection<TipoviKofiguracije>();

        public ObservableCollection<TipoviKofiguracije> ListaTipovaZaKonfiguraciju
        {
            get { return listaTipovaZaKonfiguraciju; }
            set { SetAndNotify(ref listaTipovaZaKonfiguraciju, value); }
        }

        ObservableCollection<Parametri> listaParametara = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametara
        {
            get { return listaParametara; }
            set { SetAndNotify(ref listaParametara, value); }
        }


        ObservableCollection<String> listaTipovaProvere = new ObservableCollection<String>()
            {
                "Upoređivanje", "Prikaz" 
            };

        public ObservableCollection<String> ListaTipovaProvere
        {
            get { return listaTipovaProvere; }
            set { SetAndNotify(ref listaTipovaProvere, value); }
        }


        ObservableCollection<Parametri> listaParametaraPomocno = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametaraPomocno
        {
            get { return listaParametaraPomocno; }
            set { SetAndNotify(ref listaParametaraPomocno, value); }
        }

        ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }

        ObservableCollection<IstorijaKupovine> listaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();

        public ObservableCollection<IstorijaKupovine> ListaIstorijeKupovine
        {
            get { return listaIstorijeKupovine; }
            set { SetAndNotify(ref listaIstorijeKupovine, value); }
        }

        private ObservableCollection<IstorijaKupovine> listaIstorijeKupovineZaListuKupaca = new ObservableCollection<IstorijaKupovine>();

        public ObservableCollection<IstorijaKupovine> ListaIstorijeKupovineZaListuKupaca
        {
            get { return listaIstorijeKupovineZaListuKupaca; }
            set { SetAndNotify(ref listaIstorijeKupovineZaListuKupaca, value); }
        }

        private ObservableCollection<Narudzbina> listaNarudzbina = new ObservableCollection<Narudzbina>();

        public ObservableCollection<Narudzbina> ListaNarudzbina
        {
            get { return listaNarudzbina; }
            set { SetAndNotify(ref listaNarudzbina, value); }
        }

        private ObservableCollection<GrupaOgranicenja> listaOgranicenja = new ObservableCollection<GrupaOgranicenja>();

        public ObservableCollection<GrupaOgranicenja> ListaOgranicenja
        {
            get { return listaOgranicenja; }
            set { SetAndNotify(ref listaOgranicenja, value); }
        }

        private ObservableCollection<GrupaOgranicenja> listaTrenutnaGrupeOgranicenjaZaTipOpreme = new ObservableCollection<GrupaOgranicenja>();

        public ObservableCollection<GrupaOgranicenja> ListaTrenutnaGrupeOgranicenjaZaTipOpreme
        {
            get { return listaTrenutnaGrupeOgranicenjaZaTipOpreme; }
            set { SetAndNotify(ref listaTrenutnaGrupeOgranicenjaZaTipOpreme, value); }
        }

        private GrupaOgranicenja currentOgranicenjeKolekcije = new GrupaOgranicenja();

        public GrupaOgranicenja CurrentOgranicenjeKolekcije
        {
            get { return currentOgranicenjeKolekcije; }
            set { SetAndNotify(ref currentOgranicenjeKolekcije, value); }
        }


        ObservableCollection<Oprema> listaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaOpreme
        {
            get { return listaOpreme; }
            set { SetAndNotify(ref listaOpreme, value); }
        }

        ObservableCollection<Kupci> listaKupaca = new ObservableCollection<Kupci>();

        public ObservableCollection<Kupci> ListaKupaca
        {
            get { return listaKupaca; }
            set { SetAndNotify(ref listaKupaca, value); }
        }


        private OblastiOpreme currentOblastiOpreme;

        public OblastiOpreme CurrentOblastiOpreme
        {
            get { return currentOblastiOpreme; }
            set { SetAndNotify(ref currentOblastiOpreme, value); }
        }

        private OblastiOpreme novaOblastOpreme;

        public OblastiOpreme NovaOblastOpreme
        {
            get { return novaOblastOpreme; }
            set { SetAndNotify(ref novaOblastOpreme, value); }
        }



        private TipoviOpreme currentTipoviOpreme;

        public TipoviOpreme CurrentTipoviOpreme
        {
            get { return currentTipoviOpreme; }
            set { SetAndNotify(ref currentTipoviOpreme, value); }
        }

        private TipoviOpreme noviTipOpreme;

        public TipoviOpreme NoviTipOpreme
        {
            get { return noviTipOpreme; }
            set { SetAndNotify(ref noviTipOpreme, value); }
        }

        private Korisnici currentKorisnici;

        public Korisnici CurrentKorisnici
        {
            get { return currentKorisnici; }
            set { SetAndNotify(ref currentKorisnici, value); }
        }

        private Korisnici noviKorisnik;

        public Korisnici NoviKorisnik
        {
            get { return noviKorisnik; }
            set { SetAndNotify(ref noviKorisnik, value); }
        }

        private Oprema currentOprema;

        public Oprema CurrentOprema
        {
            get { return currentOprema; }
            set { SetAndNotify(ref currentOprema, value); }
        }

        private Kupci currentKupci;

        public Kupci CurrentKupci
        {
            get { return currentKupci; }
            set { SetAndNotify(ref currentKupci, value); }
        }

        private bool daLiJeSlikaPromenjena = false;

        public bool DaLiJeSlikaPromenjena
        {
            get { return daLiJeSlikaPromenjena; }
            set { daLiJeSlikaPromenjena = value; }
        }
        //private int vreme = 5;

        //private DispatcherTimer timer;
        private int odakleKrece = 0;
        private int dokleIde = 4;
        private Korisnici trenutniProdavac = new Korisnici();

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }


        public AdminPanelProzor(Korisnici prodavac)
        {

            InitializeComponent();
            this.DataContext = this;
            //OblastiOpremeSpecijalnaSirina = new GridLength(1, GridUnitType.Star);
            //TipoviOpremeSpecijalnaSirina = new GridLength(0);
            btnOblastiOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
            this.popuniListuOblasti(nizOblasti);

            this.TrenutniProdavac = prodavac;



            //Timeline.SetDesiredFrameRate(taPomeriSDesnaUNormaluPolaSekunde, 30);
            //Timeline.SetDesiredFrameRate(taPomeriSLevaUNormaluPolaSekunde, 30);
            //Timeline.SetDesiredFrameRate(taPomeriNaDolePolaSekunde, 30);
            //Timeline.SetDesiredFrameRate(taNazivKategorijeMargina, 30);
            //Timeline.SetDesiredFrameRate(daPrikaziPolaSekunde, 30);
            //Timeline.SetDesiredFrameRate(daSakrijPolaSekunde, 30);
            //Timeline.SetDesiredFrameRate(daPrikaziSekund, 30);
            //Timeline.SetDesiredFrameRate(daSakrijSekund, 30);
            //Timeline.SetDesiredFrameRate(daSakrijDveSekunde, 30);
            //Timeline.SetDesiredFrameRate(daBrdTriVjuHierarchicalNazivSirina, 30);














        }

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    if (vreme > 0) {
        //        vreme--;

        //    }
        //    else
        //    {
        //        timer.Stop();

        //    }
        //}


        private void btnIzaberiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GridPrikazRezultataOblastiOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosOblastiOpreme.Visibility = Visibility.Hidden;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaOblastiOpreme);
            oo = oblasti.CurrentItem as OblastiOpreme;
            this.CurrentOblastiOpreme = oo;
            this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = false;

            template = null;

        }

        DispatcherTimer timerSpustiCeluOblast;



        void timerSpustiCeluOblast_Tick(object sender, EventArgs e)
        {
            if (timerSpustiCeluOblast.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();


                switch (kojeDugmeJePoReduKliknuto)
                {
                    case 1:
                        brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        if (kojeDugmeJePoReduKliknuto != kojeDugmeJePoReduKliknutoRanije)
                        {
                            if (kojeDugmeJePoReduKliknuto > kojeDugmeJePoReduKliknutoRanije)
                                brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                            else if (kojeDugmeJePoReduKliknuto < kojeDugmeJePoReduKliknutoRanije)
                                brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSLevaUNormaluPolaSekunde);


                        }
                        Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 1);
                        //brdUnosOblastiOpreme.IsEnabled = false;
                        brdPrikazDetaljaOblastiOpreme.IsEnabled = false;
                        brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = true;
                        break;
                    case 2:


                        brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        if (kojeDugmeJePoReduKliknuto != kojeDugmeJePoReduKliknutoRanije)
                        {
                            if (kojeDugmeJePoReduKliknuto > kojeDugmeJePoReduKliknutoRanije)
                                brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                            else if (kojeDugmeJePoReduKliknuto < kojeDugmeJePoReduKliknutoRanije)
                                brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSLevaUNormaluPolaSekunde);
                        }

                        Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 1);
                        brdPrikazDetaljaTipoviOpreme.IsEnabled = false;
                        //brdUnosTipaOpreme.IsEnabled = false;
                        brdPrazanPrikazDetaljaTipoviOpreme.IsEnabled = true;
                        break;
                    case 3:
                        brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        if (kojeDugmeJePoReduKliknuto != kojeDugmeJePoReduKliknutoRanije)
                        {
                            if (kojeDugmeJePoReduKliknuto > kojeDugmeJePoReduKliknutoRanije)
                                brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                            else if (kojeDugmeJePoReduKliknuto < kojeDugmeJePoReduKliknutoRanije)
                                brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSLevaUNormaluPolaSekunde);
                        }

                        Panel.SetZIndex(brdOpremaCeoSadrzaj, 1);
                        break;
                    case 4:
                        brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        if (kojeDugmeJePoReduKliknuto != kojeDugmeJePoReduKliknutoRanije)
                        {
                            if (kojeDugmeJePoReduKliknuto > kojeDugmeJePoReduKliknutoRanije)
                                brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                            else if (kojeDugmeJePoReduKliknuto < kojeDugmeJePoReduKliknutoRanije)
                                brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSLevaUNormaluPolaSekunde);
                        }

                        Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 1);
                        break;
                    case 5:
                        brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        if (kojeDugmeJePoReduKliknuto != kojeDugmeJePoReduKliknutoRanije)
                        {
                            if (kojeDugmeJePoReduKliknuto > kojeDugmeJePoReduKliknutoRanije)
                                brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                            else if (kojeDugmeJePoReduKliknuto < kojeDugmeJePoReduKliknutoRanije)
                                brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSLevaUNormaluPolaSekunde);
                        }

                        Panel.SetZIndex(brdKorisniciCeoSadrzaj, 1);

                        brdPrikazDetaljaKorisnici.IsEnabled = false;
                        //brdUnosKorisnici.IsEnabled = false;
                        brdPrazanPrikazDetaljaKorisnici.IsEnabled = true;
                        //trivjuKorisnici.Width = 275;





                        break;
                    case 6:

                        grdIstorijaKupovine.Visibility = Visibility.Visible;
                        grdIstorijaKupovine.IsEnabled = true;
                        Panel.SetZIndex(grdIstorijaKupovine, 1);
                        break;
                    case 7:
                        DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        cclUpravljanjeNarudzbinama.BeginAnimation(ContentControl.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        cclUpravljanjeNarudzbinama.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                        grdNarudzbine.Visibility = Visibility.Visible;
                        grdNarudzbine.IsEnabled = true;
                        Panel.SetZIndex(grdNarudzbine, 1);
                        this.pravilnoRasporediListuNarudzbina();
                        break;

                }





            }
            kliknutoNaDugmiceUpravljanja = false;
            timerSpustiCeluOblast.Stop();
        }
        
        private void popuniListuOblasti(DbItemOblastiOpreme[] nizOblasti)
        {
            ListaZaSve = null;
            ListaZaSve = new ObservableCollection<OblastiOpreme>();
            for (int i = 0; i < nizOblasti.Length; i++)
            {



                OblastiOpreme o = new OblastiOpreme(null)
                {

                    Name = nizOblasti[i].naziv_oblasti_opreme,
                    IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme,
                    Picture = nizOblasti[i].picture,
                    SlikaOriginalPutanja = nizOblasti[i].SlikaOriginalPutanja,
                    NazivOblastiOpreme = nizOblasti[i].naziv_oblasti_opreme,
                    DeletedItem = nizOblasti[i].DeletedField
                };

                
                if (!File.Exists(nizOblasti[i].picture))
                {
                    o.Picture = App.PutanjaDoSlikeNoImage;
                }

                ListaZaSve.Add(o);
            }

            ListaOblastiOpreme = ListaZaSve;
            daLiJeListaOblastiOpremeOsvezena = true;
        }

        private void popuniListuOblastiZaTipoveOpreme(DbItemOblastiOpreme[] nizOblasti)
        {
            ListaZaSve = null;
            ListaZaSve = new ObservableCollection<OblastiOpreme>();
            for (int i = 0; i < nizOblasti.Length; i++)
            {



                OblastiOpreme o = new OblastiOpreme(null)
                {

                    Name = nizOblasti[i].naziv_oblasti_opreme,
                    IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme,
                   
                };

                
                if (!File.Exists(nizOblasti[i].picture))
                {
                    o.Picture = App.PutanjaDoSlikeNoImage;
                }

                ListaZaSve.Add(o);
            }

            ListaOblastiOpremeZaTipoveOpreme = ListaZaSve;
            daLiJeListaOblastiOpremeOsvezena = true;
        }
        

        private void popuniListuOblastiZaUnos(DbItemOblastiOpreme[] nizOblasti)
        {
            ListaZaSve = null;
            ListaZaSve = new ObservableCollection<OblastiOpreme>();
            if (nizOblasti.Length > 0)
            {
                ListaZaSve.Add(new OblastiOpreme(null)
                {
                    Name = "Odaberite Oblast opreme..."
                });
                for (int i = 0; i < nizOblasti.Length; i++)
                {
                    ListaZaSve.Add(new OblastiOpreme(null)
                    {
                        Name = nizOblasti[i].naziv_oblasti_opreme,
                        IdOblastiOpreme = nizOblasti[i].id_oblasti_opreme,
                        Picture = nizOblasti[i].picture,
                        SlikaOriginalPutanja = nizOblasti[i].SlikaOriginalPutanja,
                        NazivOblastiOpreme = nizOblasti[i].naziv_oblasti_opreme,
                        DeletedItem = nizOblasti[i].DeletedField
                    });
                }

                ListaOblastiOpremeZaUnos = ListaZaSve;
                daLiJeListaOblastiOpremeOsvezena = true;
            }
        }

        private void popuniListuTipovaOpreme(DbItemTipOpreme[] nizTipovaOpreme)
        {
            ListaTipovaOpreme = null;
            ListaTipovaOpreme = new ObservableCollection<TipoviOpreme>();
            for (int i = 0; i < nizTipovaOpreme.Length; i++)
            {
                TipoviOpreme t = new TipoviOpreme(null)
                {
                    Name = nizTipovaOpreme[i].naziv_tipa,
                    IdOblastiOpreme = nizTipovaOpreme[i].id_oblasti_opreme,
                    IdTipOpreme = nizTipovaOpreme[i].id_tip_opreme,
                    Picture = nizTipovaOpreme[i].slika_tipa,
                    SlikaOriginalPutanja = nizTipovaOpreme[i].SlikaOriginalPutanja,
                    StaroImeTipa = nizTipovaOpreme[i].naziv_tipa,
                    NazivOblastiOpreme = nizTipovaOpreme[i].naziv_oblasti_opreme,
                    DeletedItem = nizTipovaOpreme[i].DeletedField
                };

                if (!File.Exists(nizTipovaOpreme[i].slika_tipa))
                {
                    t.Picture = App.PutanjaDoSlikeNoImage;
                }

                ListaTipovaOpreme.Add(t);

            }
        }

        private void popuniListuTipovaOpremeZaUnosOpreme(DbItemTipOpreme[] nizTipovaOpreme)
        {
            ListaTipovaOpremeZaUnosOpreme = null;
            ListaTipovaOpremeZaUnosOpreme = new ObservableCollection<TipoviOpreme>();
            ListaTipovaOpremeZaUnosOpreme.Add(new TipoviOpreme(null)
                {
                    Name = "Odaberite tip opreme...",
                    IdTipOpreme = 0
                });
            for (int i = 0; i < nizTipovaOpreme.Length; i++)
            {
                ListaTipovaOpremeZaUnosOpreme.Add(new TipoviOpreme(null)
                {
                    Name = nizTipovaOpreme[i].naziv_tipa,
                    IdTipOpreme = nizTipovaOpreme[i].id_tip_opreme
                });


            }
        }


        ObservableCollection<Korisnici> ListaKorisnikaTrenutno = null;
        private void popuniListuKorisnici(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnikaTrenutno = null;
            ListaKorisnikaTrenutno = new ObservableCollection<Korisnici>();
            for (int i = 0; i < nizKorisnika.Length; i++)
            {
                
                Korisnici k = new Korisnici()
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
                    //SlikaKorisnika = nizKorisnika[i].slikaKorisnika == null ? nizKorisnika[i].id_uloge == 1 ? App.PutanjaDoSlikeAdministratorKorisnici:App.PutanjaDoSlikeProdavacKorisnici : nizKorisnika[i].slikaKorisnika,
                    DatumKreiranjaKorisnika = nizKorisnika[i].datumKreiranja,
                    DatumAzuriranjaKorisnika = nizKorisnika[i].datumAzuriranja,
                };

                if (!File.Exists(nizKorisnika[i].slikaKorisnika))
                {
                    k.SlikaKorisnika = nizKorisnika[i].id_uloge == 1 ? App.PutanjaDoSlikeAdministratorKorisnici : App.PutanjaDoSlikeProdavacKorisnici;
                }
                else
                    k.SlikaKorisnika = nizKorisnika[i].slikaKorisnika;

                ListaKorisnikaTrenutno.Add(k);
            }
            this.ListaKorisnika = ListaKorisnikaTrenutno;
        }

        private void popuniListuNarudzbina(DbItemNarudzbine[] nizNarudzbina)
        {

            if (nizNarudzbina.Length >= 4)
                VidljivostDugmetaUcitajJosNarudzbina = Visibility.Visible;
            else
                VidljivostDugmetaUcitajJosNarudzbina = Visibility.Hidden;

            ListaNarudzbina = new ObservableCollection<Narudzbina>();
            ObservableCollection<Narudzbina> TrenutnaListaNarudzbina = new ObservableCollection<Narudzbina>();
            odakleKrece = 0;
            dokleIde = 4;

            for (int i = odakleKrece; i < dokleIde; i++)
            {
                Narudzbina n = new Narudzbina()
                    {
                        RedniBrojNarudzbine = i + 1,
                        DatumNarudzbine = nizNarudzbina[i].datum_narudzbine,
                        NarucenaKolicina = nizNarudzbina[i].kolicina,
                        IdNarudzbine = nizNarudzbina[i].id_narudzbine,
                        Prodavac = new Korisnici()
                        {
                            IdKorisnici = nizNarudzbina[i].prodavac.id_korisnici,
                            IdUloge = nizNarudzbina[i].prodavac.id_uloge,
                            IzabranaUloga = nizNarudzbina[i].prodavac.id_uloge - 1,
                            ImeKorisnika = nizNarudzbina[i].prodavac.ime,
                            PrezimeKorisnika = nizNarudzbina[i].prodavac.prezime,
                            MejlKorisnika = nizNarudzbina[i].prodavac.mejl,
                            SlikaKorisnika = nizNarudzbina[i].prodavac.slikaKorisnika,
                            BrojTelefonaKorisnika = nizNarudzbina[i].prodavac.broj_telefona,
                            BrojOstvarenihPoena = Convert.ToDouble(nizNarudzbina[i].prodavac.brojOstvarenihPoena),
                            Username = nizNarudzbina[i].prodavac.username,
                            Lozinka = nizNarudzbina[i].prodavac.lozinka,
                            NazivUloge = nizNarudzbina[i].prodavac.naziv_uloge,
                            DeletedItem = nizNarudzbina[i].prodavac.deletedField
                        },
                        Oprema = new Oprema(null)
                        {
                            Cena = nizNarudzbina[i].narucenaOprema.cena,
                            IdOprema = nizNarudzbina[i].narucenaOprema.id_oprema,
                            IdTipOpreme = nizNarudzbina[i].narucenaOprema.id_tip_opreme,
                            KolicinaNaLageru = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru,
                            KolicinaURezervi = nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi,
                            Lager = nizNarudzbina[i].narucenaOprema.lager,
                            Model = nizNarudzbina[i].narucenaOprema.model,
                            Name = nizNarudzbina[i].narucenaOprema.naslov,
                            Opis = nizNarudzbina[i].narucenaOprema.opis,
                            OpremaNaPopustu = nizNarudzbina[i].narucenaOprema.oprema_na_popustu,
                            Proizvodjac = nizNarudzbina[i].narucenaOprema.proizvodjac,
                            Picture = nizNarudzbina[i].narucenaOprema.slika,
                            Slika = nizNarudzbina[i].narucenaOprema.slika,
                            SlikaOriginalPutanja = nizNarudzbina[i].narucenaOprema.slikaOriginalPutanja,
                            IzabranaKolicina = 1,
                            DaliMozeJosDaseDoda = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru > 0,
                            DeletedItem = nizNarudzbina[i].narucenaOprema.DeletedField,
                            DaLiJeDugmePrihvatiNarudzbinuOmoguceno = (nizNarudzbina[i].kolicina < nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi) && (!nizNarudzbina[i].narucenaOprema.DeletedField)
                        }

                    };
                TrenutnaListaNarudzbina.Add(n);
            }

            ListaNarudzbina = TrenutnaListaNarudzbina;



            //timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 1);
            //timer.Tick += timer_Tick;
            //timer.Start();

            //foreach (var item in nizNarudzbina)
            //{
            //    Narudzbina n = new Narudzbina()
            //        {

            //            DatumNarudzbine = item.datum_narudzbine,
            //            NarucenaKolicina = item.kolicina,
            //            Prodavac = new Korisnici()
            //            {
            //                IdKorisnici = item.prodavac.id_korisnici,
            //                IdUloge = item.prodavac.id_uloge,
            //                IzabranaUloga = item.prodavac.id_uloge - 1,
            //                ImeKorisnika = item.prodavac.ime,
            //                PrezimeKorisnika = item.prodavac.prezime,
            //                MejlKorisnika = item.prodavac.mejl,
            //                BrojTelefonaKorisnika = item.prodavac.broj_telefona,
            //                BrojOstvarenihPoena = Convert.ToDouble(item.prodavac.brojOstvarenihPoena),
            //                Username = item.prodavac.username,
            //                Lozinka = item.prodavac.lozinka,
            //                NazivUloge = item.prodavac.naziv_uloge,
            //                DeletedItem = item.prodavac.deletedField
            //            },
            //            Oprema = new Oprema(null)
            //            {
            //                Cena = item.narucenaOprema.cena,
            //                IdOprema = item.narucenaOprema.id_oprema,
            //                IdTipOpreme = item.narucenaOprema.id_tip_opreme,
            //                KolicinaNaLageru = item.narucenaOprema.kolicina_na_lageru,
            //                KolicinaURezervi = item.narucenaOprema.kolicina_u_rezervi,
            //                Lager = item.narucenaOprema.lager,
            //                Model = item.narucenaOprema.model,
            //                Name = item.narucenaOprema.naslov,
            //                Opis = item.narucenaOprema.opis,
            //                OpremaNaPopustu = item.narucenaOprema.oprema_na_popustu,
            //                Proizvodjac = item.narucenaOprema.proizvodjac,
            //                Picture = item.narucenaOprema.slika,
            //                Slika = item.narucenaOprema.slika,
            //                SlikaOriginalPutanja = item.narucenaOprema.slikaOriginalPutanja,
            //                IzabranaKolicina = 1,
            //                DaliMozeJosDaseDoda = item.narucenaOprema.kolicina_na_lageru > 0,
            //                DeletedItem = item.narucenaOprema.DeletedField,

            //            }

            //        };
            //        ListaNarudzbina.Add(n);

            //}
        }

        private void popuniListuNarudzbinaOpet(DbItemNarudzbine[] nizNarudzbina, int odakleKrece, int dokleIde)
        {

            int broj = nizNarudzbina.Length;
            if (broj < dokleIde)
            {
                dokleIde = broj;
            }
            for (int i = odakleKrece; i < dokleIde; i++)
            {
                Narudzbina n = new Narudzbina()
                    {
                        RedniBrojNarudzbine = i + 1,
                        DatumNarudzbine = nizNarudzbina[i].datum_narudzbine,
                        NarucenaKolicina = nizNarudzbina[i].kolicina,
                        IdNarudzbine = nizNarudzbina[i].id_narudzbine,
                        Prodavac = new Korisnici()
                        {
                            IdKorisnici = nizNarudzbina[i].prodavac.id_korisnici,
                            IdUloge = nizNarudzbina[i].prodavac.id_uloge,
                            IzabranaUloga = nizNarudzbina[i].prodavac.id_uloge - 1,
                            ImeKorisnika = nizNarudzbina[i].prodavac.ime,
                            PrezimeKorisnika = nizNarudzbina[i].prodavac.prezime,
                            MejlKorisnika = nizNarudzbina[i].prodavac.mejl,
                            SlikaKorisnika = nizNarudzbina[i].prodavac.slikaKorisnika,
                            BrojTelefonaKorisnika = nizNarudzbina[i].prodavac.broj_telefona,
                            BrojOstvarenihPoena = Convert.ToDouble(nizNarudzbina[i].prodavac.brojOstvarenihPoena),
                            Username = nizNarudzbina[i].prodavac.username,
                            Lozinka = nizNarudzbina[i].prodavac.lozinka,
                            NazivUloge = nizNarudzbina[i].prodavac.naziv_uloge,
                            DeletedItem = nizNarudzbina[i].prodavac.deletedField
                        },
                        Oprema = new Oprema(null)
                        {
                            Cena = nizNarudzbina[i].narucenaOprema.cena,
                            IdOprema = nizNarudzbina[i].narucenaOprema.id_oprema,
                            IdTipOpreme = nizNarudzbina[i].narucenaOprema.id_tip_opreme,
                            KolicinaNaLageru = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru,
                            KolicinaURezervi = nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi,
                            Lager = nizNarudzbina[i].narucenaOprema.lager,
                            Model = nizNarudzbina[i].narucenaOprema.model,
                            Name = nizNarudzbina[i].narucenaOprema.naslov,
                            Opis = nizNarudzbina[i].narucenaOprema.opis,
                            OpremaNaPopustu = nizNarudzbina[i].narucenaOprema.oprema_na_popustu,
                            Proizvodjac = nizNarudzbina[i].narucenaOprema.proizvodjac,
                            Picture = nizNarudzbina[i].narucenaOprema.slika,
                            Slika = nizNarudzbina[i].narucenaOprema.slika,
                            SlikaOriginalPutanja = nizNarudzbina[i].narucenaOprema.slikaOriginalPutanja,
                            IzabranaKolicina = 1,
                            DaliMozeJosDaseDoda = nizNarudzbina[i].narucenaOprema.kolicina_na_lageru > 0,
                            DeletedItem = nizNarudzbina[i].narucenaOprema.DeletedField,
                            DaLiJeDugmePrihvatiNarudzbinuOmoguceno = (nizNarudzbina[i].kolicina < nizNarudzbina[i].narucenaOprema.kolicina_u_rezervi) && (!nizNarudzbina[i].narucenaOprema.DeletedField)
                        }

                    };
                ListaNarudzbina.Add(n);
            }
            if (broj != dokleIde)
            {


                //vreme = 5;
                //timer = new DispatcherTimer();
                //timer.Interval = new TimeSpan(0, 0, 1);
                //timer.Tick += timer_Tick;
                //timer.Start();
            }
            else
            {
                VidljivostDugmetaUcitajJosNarudzbina = Visibility.Hidden;
                odakleKrece = 0;
                dokleIde = 4;
            }


        }

        private void popuniListuOgranicenja(DbItemGrupeOgranicenja[] nizOgranicenja)
        {
            if (nizOgranicenja.Length > 0)
            {
                this.CurrentTipoviOpreme = new TipoviOpreme(null)
                {
                    IdTipOpreme = nizOgranicenja[0].id_tip_opreme_kolekcije
                };
            }
            else
            {
                return;
            }

            ListaOgranicenja = new ObservableCollection<GrupaOgranicenja>();
            foreach (var item in nizOgranicenja)
            {
                GrupaOgranicenja grupaOgranicenja = new GrupaOgranicenja()
                {
                    Id_grupe_ogranicenja = item.Id_grupe_ogranicenja,
                    Id_tip_opreme_kolekcije = item.id_tip_opreme_kolekcije,
                    Id_parametra1 = item.id_parametra1,
                    Id_parametra2 = item.id_parametra2,
                    Id_tip_opreme1 = item.id_tip_opreme1,
                    Id_tip_opreme2 = item.id_tip_opreme2,
                    TipProvere = item.tipProvere,
                    NazivParametra1 = item.nazivParametra1,
                    NazivParametra2 = item.nazivParametra2,
                    NazivTipaOpreme1 = item.nazivTipaOpreme1,
                    NazivTipaOpreme2 = item.nazivTipaOpreme2
                };
                ListaOgranicenja.Add(grupaOgranicenja);
            }
        }
        ObservableCollection<Oprema> ListaOpreme2 = new ObservableCollection<Oprema>();
        private void popuniListuOpremeSaParametrima(DbItemOpremaSaParametrima[] nizOpremaSaParametrima)
        {
            //ListaOpreme = null;
            ListaOpreme2 = null;
            ListaOpreme2 = new ObservableCollection<Oprema>();
            for (int i = 0; i < nizOpremaSaParametrima.Length; i++)
            {
                if (nizOpremaSaParametrima[i] != null)
                {

                }

                Oprema o = new Oprema(null)
                {
                    Cena = nizOpremaSaParametrima[i].cena,
                    IdOprema = nizOpremaSaParametrima[i].id_oprema,
                    IdTipOpreme = nizOpremaSaParametrima[i].id_tip_opreme,
                    KolicinaNaLageru = nizOpremaSaParametrima[i].kolicina_na_lageru,
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
                    DaliMozeJosDaseDoda = nizOpremaSaParametrima[i].kolicina_na_lageru > 0,
                    DeletedItem = nizOpremaSaParametrima[i].DeletedField,
                };

                if (!File.Exists(nizOpremaSaParametrima[i].slika))
                {
                    o.Picture = App.PutanjaDoSlikeNoImage;
                }

                for (int j = 0; j < ListaTipovaOpreme.Count; j++)
                {
                    if (o.IdTipOpreme == listaTipovaOpreme[j].IdTipOpreme)
                    {
                        o.IzabranTipOpreme = j;
                        o.NaziOdabranogTipaOpreme = ListaTipovaOpreme[j].Name;
                        break;
                    }
                }

                DbItemOpremaSaParametrimaStatistika opremaStatistika = (nizOpremaSaParametrima[i] as DbItemOpremaSaParametrimaStatistika);

                if (opremaStatistika != null)
                {
                    o.KolikoPutajeProdata = opremaStatistika.kolkoPutaJeProdavata;
                }


                ListaOpreme2.Add(o);

                for (int j = 0; j < nizOpremaSaParametrima[i].ListaParametara.Length; j++)
                {
                    ListaOpreme2[i].ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = nizOpremaSaParametrima[i].ListaParametara[j].default_vrednost,
                        IdParametri = nizOpremaSaParametrima[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = nizOpremaSaParametrima[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = nizOpremaSaParametrima[i].ListaParametara[j].vrednost_parametra,
                        Name = nizOpremaSaParametrima[i].ListaParametara[j].naziv_parametra,
                        TipParametra = nizOpremaSaParametrima[i].ListaParametara[j].tipParametra
                    });
                }

            }
            ListaOpreme = ListaOpreme2;
        }

        private void popuniListuIstorijeKupovine(DbItemIstorijaKupovine[] nizIstorijeKupovine)
        {
            ListaIstorijeKupovine = null;
            ListaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();

            for (int i = 0; i < nizIstorijeKupovine.Length; i++)
            {





                ListaIstorijeKupovine.Add(new IstorijaKupovine()
                {
                    Datum_prodaje = nizIstorijeKupovine[i].datum_prodaje,
                    IdIstorijaKupovine = nizIstorijeKupovine[i].id_istorija_kupovine,
                    Broj_iskoriscenih_popust_poena = nizIstorijeKupovine[i].broj_iskoriscenih_popust_poena,
                    Kupac = nizIstorijeKupovine[i].Kupac == null ? null : new Korisnici()
                    {

                        BrojTelefonaKorisnika = nizIstorijeKupovine[i].Kupac.broj_telefona,
                        IdKorisnici = nizIstorijeKupovine[i].Kupac.id_korisnici,
                        ImeKorisnika = nizIstorijeKupovine[i].Kupac.ime,
                        PrezimeKorisnika = nizIstorijeKupovine[i].Kupac.prezime,
                        MejlKorisnika = nizIstorijeKupovine[i].Kupac.mejl,
                        SlikaKorisnika = nizIstorijeKupovine[i].Kupac.slikaKorisnika,
                        ImeIPrezimeKorisnika = nizIstorijeKupovine[i].Kupac.ime + " " + nizIstorijeKupovine[i].Kupac.prezime

                    },
                    Prodavac = nizIstorijeKupovine[i].prodavac == null ? null : new Korisnici()
                    {

                        BrojTelefonaKorisnika = nizIstorijeKupovine[i].prodavac.broj_telefona,
                        IdKorisnici = nizIstorijeKupovine[i].prodavac.id_korisnici,
                        ImeKorisnika = nizIstorijeKupovine[i].prodavac.ime,
                        PrezimeKorisnika = nizIstorijeKupovine[i].prodavac.prezime,
                        MejlKorisnika = nizIstorijeKupovine[i].prodavac.mejl,
                        IdUloge = nizIstorijeKupovine[i].prodavac.id_uloge,
                        NazivUloge = nizIstorijeKupovine[i].prodavac.naziv_uloge,
                        Username = nizIstorijeKupovine[i].prodavac.username,
                        Lozinka = nizIstorijeKupovine[i].prodavac.lozinka,
                        SlikaKorisnika = nizIstorijeKupovine[i].prodavac.slikaKorisnika
                    },

                    Ukupna_cena_kupovine = nizIstorijeKupovine[i].ukupna_cena_kupovine

                });


                for (int j = 0; j < nizIstorijeKupovine[i].ListaKupljeneOpreme.Length; j++)
                {
                    //ListaIstorijeKupovine[i].ListaKupljeneOpreme = new ObservableCollection<KupljenaOprema>();

                    KupljenaOprema o = new KupljenaOprema(null)
                    {
                        Cena = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena,
                        IdOprema = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_oprema,
                        IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_tip_opreme,
                        KolicinaNaLageru = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_na_lageru + nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                        KolicinaURezervi = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_u_rezervi,
                        Lager = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].lager,
                        Model = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].model,
                        Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].naslov,
                        Opis = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].opis,
                        OpremaNaPopustu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].oprema_na_popustu,
                        Proizvodjac = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].proizvodjac,
                        Slika = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slika,
                        SlikaOriginalPutanja = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slikaOriginalPutanja,
                        IzabranaKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                        ProdataKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].prodataKolicina,
                        Popust_na_cenu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].popust_na_cenu,
                        Cena_opreme_kad_je_prodata = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena_opreme_kad_je_prodata

                    };

                    for (int k = 0; k < nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara.Length; k++)
                    {
                        o.ListaParametara.Add(new Parametri(null)

                        {
                            DefaultVrednost = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].default_vrednost,
                            IdParametri = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_parametri,
                            IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_tip_opreme,
                            VrednostParametra = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].vrednost_parametra,
                            Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].naziv_parametra
                        });

                    }

                    ListaIstorijeKupovine[i].ListaKupljeneOpreme.Add(o);
                }
            }
        }



        private void popuniListuIstorijeKupovineZaListuKupaca(DbItemIstorijaKupovine[] nizIstorijeKupovine)
        {
            ListaIstorijeKupovineZaListuKupaca = null;
            ListaIstorijeKupovineZaListuKupaca = new ObservableCollection<IstorijaKupovine>();

            for (int i = 0; i < nizIstorijeKupovine.Length; i++)
            {


                if (nizIstorijeKupovine[i].idKupac != null)
                {


                    IstorijaKupovine ik = new IstorijaKupovine()
                    {
                        Datum_prodaje = nizIstorijeKupovine[i].datum_prodaje,
                        IdIstorijaKupovine = nizIstorijeKupovine[i].id_istorija_kupovine,
                        Broj_iskoriscenih_popust_poena = nizIstorijeKupovine[i].broj_iskoriscenih_popust_poena,
                        Kupac = nizIstorijeKupovine[i].Kupac == null ? null : new Korisnici()
                        {

                            BrojTelefonaKorisnika = nizIstorijeKupovine[i].Kupac.broj_telefona,
                            IdKorisnici = nizIstorijeKupovine[i].Kupac.id_korisnici,
                            ImeKorisnika = nizIstorijeKupovine[i].Kupac.ime,
                            PrezimeKorisnika = nizIstorijeKupovine[i].Kupac.prezime,
                            MejlKorisnika = nizIstorijeKupovine[i].Kupac.mejl,
                            SlikaKorisnika = nizIstorijeKupovine[i].Kupac.slikaKorisnika, 
                            BrojKupovina = nizIstorijeKupovine[i].Kupac.brojKupovina,
                            ImeIPrezimeKorisnika = nizIstorijeKupovine[i].Kupac.ime + " " + nizIstorijeKupovine[i].Kupac.prezime

                        },
                        Prodavac = nizIstorijeKupovine[i].prodavac == null ? null : new Korisnici()
                        {

                            BrojTelefonaKorisnika = nizIstorijeKupovine[i].prodavac.broj_telefona,
                            IdKorisnici = nizIstorijeKupovine[i].prodavac.id_korisnici,
                            ImeKorisnika = nizIstorijeKupovine[i].prodavac.ime,
                            PrezimeKorisnika = nizIstorijeKupovine[i].prodavac.prezime,
                            MejlKorisnika = nizIstorijeKupovine[i].prodavac.mejl,
                            IdUloge = nizIstorijeKupovine[i].prodavac.id_uloge,
                            NazivUloge = nizIstorijeKupovine[i].prodavac.naziv_uloge,
                            Username = nizIstorijeKupovine[i].prodavac.username,
                            Lozinka = nizIstorijeKupovine[i].prodavac.lozinka,
                            SlikaKorisnika = nizIstorijeKupovine[i].prodavac.slikaKorisnika
                        },

                        Ukupna_cena_kupovine = nizIstorijeKupovine[i].ukupna_cena_kupovine

                    };
                    if (!File.Exists(ik.Kupac.SlikaKorisnika))
                        ik.Kupac.SlikaKorisnika = App.PutanjaDoSlikeProdavacKorisnici;
                    ListaIstorijeKupovineZaListuKupaca.Add(ik);
                }

                //for (int j = 0; j < nizIstorijeKupovine[i].ListaKupljeneOpreme.Length; j++)
                //{
                //    //ListaIstorijeKupovine[i].ListaKupljeneOpreme = new ObservableCollection<KupljenaOprema>();

                //    KupljenaOprema o = new KupljenaOprema(null)
                //    {
                //        Cena = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena,
                //        IdOprema = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_oprema,
                //        IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].id_tip_opreme,
                //        KolicinaNaLageru = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_na_lageru + nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                //        KolicinaURezervi = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicina_u_rezervi,
                //        Lager = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].lager,
                //        Model = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].model,
                //        Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].naslov,
                //        Opis = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].opis,
                //        OpremaNaPopustu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].oprema_na_popustu,
                //        Proizvodjac = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].proizvodjac,
                //        Slika = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slika,
                //        SlikaOriginalPutanja = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].slikaOriginalPutanja,
                //        IzabranaKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].kolicinaUKorpi,
                //        ProdataKolicina = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].prodataKolicina,
                //        Popust_na_cenu = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].popust_na_cenu,
                //        Cena_opreme_kad_je_prodata = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].cena_opreme_kad_je_prodata

                //    };

                //    for (int k = 0; k < nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara.Length; k++)
                //    {
                //        o.ListaParametara.Add(new Parametri(null)

                //        {
                //            DefaultVrednost = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].default_vrednost,
                //            IdParametri = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_parametri,
                //            IdTipOpreme = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].id_tip_opreme,
                //            VrednostParametra = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].vrednost_parametra,
                //            Name = nizIstorijeKupovine[i].ListaKupljeneOpreme[j].ListaParametara[k].naziv_parametra
                //        });

                //    }

                //    ListaIstorijeKupovineZaListuKupaca[i].ListaKupljeneOpreme.Add(o);
                //}
            }
        }

        private void popuniListuKupaca(DbItemKupci[] nizKupaca)
        {

            ListaKupaca = new ObservableCollection<Kupci>();
            Kupci k;
            foreach (var kupac in nizKupaca)
            {
                k = new Kupci()
                {
                    ImeKorisnika = kupac.ime,
                    PrezimeKorisnika = kupac.prezime,
                    NajvisePotrosio = kupac.ukupnoPotrosio,
                    BrojKupovina = kupac.brojKupovina,
                    BrojKupljeneOpreme = kupac.brojKupovina,
                    ImeIPrezimeKorisnika = kupac.ime + " " + kupac.prezime,
                    IdKorisnici = kupac.id_korisnici,
                    SlikaKorisnika = kupac.slikaKorisnika
                };
                if (kupac.ListaKupovina != null && kupac.ListaKupovina.Length > 0)
                {
                    foreach (var item in kupac.ListaKupovina)
                    {
                        KupljenaOprema ku;
                        IstorijaKupovine ik = new IstorijaKupovine()
                        {
                            Broj_iskoriscenih_popust_poena = item.broj_iskoriscenih_popust_poena,
                            IdIstorijaKupovine = item.id_istorija_kupovine,
                            Datum_prodaje = item.datum_prodaje,
                            Ukupna_cena_kupovine = item.ukupna_cena_kupovine,
                            Prodavac = new Korisnici()
                            {

                                BrojTelefonaKorisnika = item.prodavac.broj_telefona,
                                IdKorisnici = item.prodavac.id_korisnici,
                                ImeKorisnika = item.prodavac.ime,
                                PrezimeKorisnika = item.prodavac.prezime,
                                MejlKorisnika = item.prodavac.mejl,
                                IdUloge = item.prodavac.id_uloge,
                                NazivUloge = item.prodavac.naziv_uloge,
                                Username = item.prodavac.username,
                                Lozinka = item.prodavac.lozinka,
                                SlikaKorisnika = item.prodavac.slikaKorisnika
                            }
                        };
                        foreach (var jednaKupljenaOprema in item.ListaKupljeneOpreme)
                        {
                            Parametri p;

                            ku = new KupljenaOprema(null)
                            {
                                Cena = jednaKupljenaOprema.cena,
                                Cena_opreme_kad_je_prodata = jednaKupljenaOprema.cena_opreme_kad_je_prodata,
                                DeletedItem = jednaKupljenaOprema.DeletedField,
                                Picture = jednaKupljenaOprema.slika,
                                Slika = jednaKupljenaOprema.slika,
                                Model = jednaKupljenaOprema.model,
                                Opis = jednaKupljenaOprema.opis,
                                Name = jednaKupljenaOprema.naslov,
                                Popust_na_cenu = jednaKupljenaOprema.popust_na_cenu,
                                Proizvodjac = jednaKupljenaOprema.proizvodjac,
                                IdOprema = jednaKupljenaOprema.id_oprema,
                                Id_istorija_kupovine = jednaKupljenaOprema.id_istorija_kupovine,
                                IdTipOpreme = jednaKupljenaOprema.id_tip_opreme,
                                IzabranaKolicina = Convert.ToInt32(jednaKupljenaOprema.prodataKolicina)
                            };

                            foreach (var parametar in jednaKupljenaOprema.ListaParametara)
                            {
                                p = new Parametri(null)
                                {


                                    DefaultVrednost = parametar.default_vrednost,
                                    DeletedItem = parametar.deletedField,
                                    IdParametri = parametar.id_parametri,
                                    IdTipOpreme = parametar.id_tip_opreme,
                                    IzabranTipOpreme = parametar.id_tip_opreme,
                                    Name = parametar.naziv_parametra,
                                    ParametarJeIFilter = parametar.za_filter,
                                    TipParametra = parametar.tipParametra,
                                    VrednostParametra = parametar.vrednost_parametra

                                };
                                ku.ListaParametara.Add(p);
                            }



                            ik.ListaKupljeneOpreme.Add(ku);
                        }

                        k.ListaIstorijeKupovine.Add(ik);


                    }
                }
                ListaKupaca.Add(k);

            }

        }

        private void popuniListuTipovaOpremeZaKonfiguracijuOgranicenja(DbItemTipoviZaKonfiguraciju[] nizTipovaOpremeZaKonfiguraciju)
        {
            ListaTipovaZaKonfiguraciju = new ObservableCollection<TipoviKofiguracije>();

            int duzinaNiza = nizTipovaOpremeZaKonfiguraciju.Length;
            ObservableCollection<string> ListaBrojevaZaRedosled = new ObservableCollection<string>();
            for (int i = 0; i < duzinaNiza; i++)
            {
                ListaBrojevaZaRedosled.Add((i + 1).ToString());
            }


            for (int i = 0; i < nizTipovaOpremeZaKonfiguraciju.Length; i++)
            {
                TipoviKofiguracije tk = new TipoviKofiguracije(null)
                {
                    IdTipOpremeKolekcije = nizTipovaOpremeZaKonfiguraciju[i].idTipOpremeKolekcije,
                    Name = nizTipovaOpremeZaKonfiguraciju[i].naziv_tipa,
                    IzabranZaKonfiguraciju = nizTipovaOpremeZaKonfiguraciju[i].izabranZaKonfiguraciju,
                    IdTipOpreme = nizTipovaOpremeZaKonfiguraciju[i].id_tip_opreme,
                    RedosledPrikazivanja = nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja,
                    IzabranRedosled = nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja == null || nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja == 0 ?
                    0 : nizTipovaOpremeZaKonfiguraciju[i].redosledPrikazivanja - 1,
                    ListaBrojevaZaRedosled = ListaBrojevaZaRedosled,
                    MogucaKolicinaUnosa = nizTipovaOpremeZaKonfiguraciju[i].moguca_kolicina_unosa
                };
                ListaTipovaZaKonfiguraciju.Add(tk);
            }

        }

        private void resetujSadrzajOblastiOpreme()
        {
            this.CurrentOblastiOpreme = null;
            this.staraOblastOpreme = null;

            if (this.TrenutniGridTriVjuOblastiOpreme != null)
            {
                Border brdNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                TextBlock txtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as TextBlock;
                brdNazivOblastiOpreme.Background = Brushes.Transparent;
                //txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
                this.TrenutniGridTriVjuOblastiOpreme = null;
            }


            //if (brdUnosOblastiOpreme.Opacity == 1)
            //{
            //    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosOblastiOpreme, 0);
            //}
            if (brdPrikazDetaljaOblastiOpreme.Opacity == 1)
            {
                brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                //brdPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            }



            brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 1);

            LejautDokumentTitlePrikazDetaljaOblastiOpreme.Title = "PRIKAZ DETALJA";
        }

        private void resetujSadrzajTipoviOpreme()
        {
            this.CurrentTipoviOpreme = null;
            this.stariTipOpreme = null;

            if (this.TrenutniGridTriVjuTipoviOpreme != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                trenutniTxtBoxNazivOblastiOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                this.TrenutniGridTriVjuTipoviOpreme = null;
            }


            //if (brdUnosTipaOpreme.Opacity == 1)
            //{
            //    brdUnosTipaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosTipaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosTipaOpreme, 0);
            //}
            if (brdPrikazDetaljaTipoviOpreme.Opacity == 1)
            {
                brdPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 0);
            }



            brdPrazanPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaTipoviOpreme, 1);

            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "PRIKAZ DETALJA";
        }

        private void resetujSadrzajKorisnici()
        {
            otkaziIzmeneKorisnici_Click();
        }

        private void RenderingDone()
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:

                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:

                            resetujSadrzajOblastiOpreme();

                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();

                            break;
                        case 2:

                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:

                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOpremaCeoSadrzaj, 0);
                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;

                    }
                    break;
                case 2:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:
                            resetujSadrzajOblastiOpreme();
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);


                            if (timerSpustiCeluOblast != null && timerSpustiCeluOblast.IsEnabled)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:

                            resetujSadrzajTipoviOpreme();

                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);


                            if (timerSpustiCeluOblast != null && timerSpustiCeluOblast.IsEnabled)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            break;
                        case 3:

                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOpremaCeoSadrzaj, 0);
                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;
                case 3:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:
                            resetujSadrzajOblastiOpreme();
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);


                            if (timerSpustiCeluOblast != null && timerSpustiCeluOblast.IsEnabled)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:
                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:
                            otkaziIzmeneOpreme_Click();
                            //brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();

                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;
                case 4:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:
                            resetujSadrzajOblastiOpreme();
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);


                            if (timerSpustiCeluOblast != null && timerSpustiCeluOblast.IsEnabled)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:
                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:
                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();

                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            //brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;
                case 5:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:
                            resetujSadrzajOblastiOpreme();
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);


                            if (timerSpustiCeluOblast != null && timerSpustiCeluOblast.IsEnabled)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:
                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:
                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();

                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            //brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;
                case 6:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:

                            resetujSadrzajOblastiOpreme();

                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:

                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:

                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOpremaCeoSadrzaj, 0);
                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;
                case 7:
                    switch (kojeDugmeJePoReduKliknutoRanije)
                    {
                        case 1:

                            resetujSadrzajOblastiOpreme();

                            brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOblastiOpremeCeoSadrzaj, 0);
                            break;
                        case 2:

                            resetujSadrzajTipoviOpreme();



                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 0);
                            break;
                        case 3:

                            otkaziIzmeneOpreme_Click();
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdOpremaCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdOpremaCeoSadrzaj, 0);
                            break;
                        case 4:
                            otkaziIzmeneParametri_Click();
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdParametriIFilteriCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdParametriIFilteriCeoSadrzaj, 0);
                            break;
                        case 5:

                            otkaziIzmeneKorisnici_Click();
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriNaDolePolaSekunde);
                            brdKorisniciCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            Panel.SetZIndex(brdKorisniciCeoSadrzaj, 0);
                            break;
                        case 6:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdIstorijaKupovine.Visibility = Visibility.Hidden;
                            grdIstorijaKupovine.IsEnabled = false;
                            Panel.SetZIndex(grdIstorijaKupovine, -1);
                            brdPregledIstorijeKupovine.Visibility = Visibility.Hidden;
                            break;
                        case 7:
                            if (timerSpustiCeluOblast != null)
                            {
                                timerSpustiCeluOblast.Stop();
                            }
                            timerSpustiCeluOblast = new DispatcherTimer();
                            timerSpustiCeluOblast.Interval = new TimeSpan(0, 0, 0, 0, 500);
                            timerSpustiCeluOblast.Tick += timerSpustiCeluOblast_Tick;
                            timerSpustiCeluOblast.Start();
                            grdNarudzbine.Visibility = Visibility.Hidden;
                            grdNarudzbine.IsEnabled = false;
                            Panel.SetZIndex(grdNarudzbine, -1);
                            break;
                    }
                    break;

            }
        }

       

        bool kliknutoNaDugmiceUpravljanja = false;

        #region dugmici upraljvanja

        private void btnOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {
            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 1;

                VratiTextZaDugmeSearch = "Kliknite ovde kako biste pretražili oblasti opreme";
                VratiTextZaDugmeInsert = "Kliknite ovde kako biste dodali novu oblast opreme";

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnOblastiOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
                this.popuniListuOblasti(nizOblasti);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);

            }

            //double sirinaPrethodnogSadrzaja = 1;
            //double rezultatZaMarginu = 1;
            //Thickness marginaZaPomeranje = new Thickness(0);
            //Thickness marginaZaPomeranjePrethodnogBordera = new Thickness(0);
            //Border prethodniBorder = null;

            //if (prethodniBorder != null)
            //{


            //    this.popuniListuOblasti(nizOblasti);


            //    ThicknessAnimation taPomeranje = new ThicknessAnimation(marginaZaPomeranje, new Thickness(0), new TimeSpan(0, 0, 2));
            //    brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeranje);

            //    ThicknessAnimation taPomeranjePrethodnog = new ThicknessAnimation(marginaZaPomeranjePrethodnogBordera, new TimeSpan(0, 0, 2));
            //    prethodniBorder.BeginAnimation(Border.MarginProperty, taPomeranjePrethodnog);

            //    prethodniBorder.BeginAnimation(Border.OpacityProperty, daSakrijDveSekunde);
            //    Panel.SetZIndex(prethodniBorder, -1);

            //    brdOblastiOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //    Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 1);

            //}

            //foreach (var item in grdDugmici.Children)
            //{
            //    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            //}
            //btnUpravljanjeTipovimaOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");











            //ListaOblastiOpreme = null;
            //ListaOblastiOpreme = new ObservableCollection<OblastiOpreme>();




            //   Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOblastimaOpreme");
            //   SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //   SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
            //   this.popuniListuOblasti(nizOblasti);


            //   Sadrzaj.Visibility = Visibility.Visible;
            //   grdIstorijaKupovine.Visibility = Visibility.Hidden;
            //kojeDugmeJePoReduKliknuto = 1;

        }

        private void btnUpravljanjeTipovimaOpreme_Click(object sender, RoutedEventArgs e)
        {
            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 2;

                VratiTextZaDugmeSearch = "Kliknite ovde kako biste pretražili tipove opreme";
                VratiTextZaDugmeInsert = "Kliknite ovde kako biste dodali novi tip opreme";

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljanjeTipovimaOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
                this.popuniListuTipovaOpreme(nizTipovaOblasti);
                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
                this.popuniListuOblastiZaTipoveOpreme(nizOblasti);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                this.pravilnoRasporediListuKorisnika();
            }
            //double sirinaPrethodnogSadrzaja = 1;
            //double rezultatZaMarginu = 1;
            //Thickness marginaZaPomeranje = new Thickness(0);
            //Thickness marginaZaPomeranjePrethodnogBordera = new Thickness(0);
            //Border prethodniBorder = null;



            //if (prethodniBorder != null)
            //{

            //    this.popuniListuTipovaOpreme(nizTipovaOblasti);



            //    ThicknessAnimation taPomeranje = new ThicknessAnimation(marginaZaPomeranje, new Thickness(0), new TimeSpan(0, 0, 2));
            //    brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.MarginProperty, taPomeranje);

            //    ThicknessAnimation taPomeranjePrethodnog = new ThicknessAnimation(marginaZaPomeranjePrethodnogBordera, new TimeSpan(0, 0, 2));
            //    prethodniBorder.BeginAnimation(Border.MarginProperty, taPomeranjePrethodnog);

            //    prethodniBorder.BeginAnimation(Border.OpacityProperty, daSakrijDveSekunde);
            //    Panel.SetZIndex(prethodniBorder, -1);

            //    brdTipoviOpremeCeoSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //    Panel.SetZIndex(brdTipoviOpremeCeoSadrzaj, 1);

            //}





            //Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeTipovimaOpreme");

            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            //this.popuniListuTipovaOpreme(nizTipovaOblasti);
            //Sadrzaj.Visibility = Visibility.Visible;
            //grdIstorijaKupovine.Visibility = Visibility.Hidden;





        }

        private void btnUpravljanjeOpremom_Click(object sender, RoutedEventArgs e)
        {

            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 3;



                VratiTextZaDugmeSearch = "Kliknite ovde kako biste pretražili opremu";
                VratiTextZaDugmeInsert = "Kliknite ovde kako biste dodali novu opremu";


                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljanjeOpremom.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
                this.popuniListuTipovaOpreme(nizTipovaOblasti);

                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanel();
                this.popuniListuOpremeSaParametrima(nizOpreme);



                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                pravilnoRasporediListuKorisnika();
            }











            //Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOpremom");
            //Sadrzaj.Visibility = Visibility.Visible;
            //grdIstorijaKupovine.Visibility = Visibility.Hidden;

        }

        private void btnUpravljanjeFilterima_Click(object sender, RoutedEventArgs e)
        {

            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 4;

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljanjeFilterima.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

                Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeParametrima");


                VratiTextZaDugmeSearch = "Kliknite ovde kako biste pretražili parametre";
                VratiTextZaDugmeInsert = "Kliknite ovde kako biste dodali novi parametar";

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
                this.popuniListuTipovaOpreme(nizTipovaOblasti);

                SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.SelectAdminPanelParametri();
                this.popuniListuParametara(nizParametara);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                this.pravilnoRasporediListuKorisnika();
            }



            //Sadrzaj.Visibility = Visibility.Visible;
            //grdIstorijaKupovine.Visibility = Visibility.Hidden;

        }

        private void btnUpravljanjeKorisnici_Click(object sender, RoutedEventArgs e)
        {

            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 5;

                VratiTextZaDugmeSearch = "Kliknite ovde kako biste pretražili korisnike";
                VratiTextZaDugmeInsert = "Kliknite ovde kako biste dodali novog korisnika";

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljanjeKorisnici.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnici = service.PrikaziZaposleneKorisnike(null);
                this.popuniListuKorisnici(nizKorisnici);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                this.pravilnoRasporediListuKorisnika();
            }






            //Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeKorisnicima");
            //ControlTemplate template = Sadrzaj.Template;


            //Sadrzaj.Visibility = Visibility.Visible;
            //grdIstorijaKupovine.Visibility = Visibility.Hidden;
        }


        private void btnUpravljenjeIstorijomKupovine_Click(object sender, RoutedEventArgs e)
        {

            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 6;

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljenjeIstorijomKupovine.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");




                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



                SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovine = service.IstorijaKupovineZaListuKupacaDatumSelect();
                this.popuniListuIstorijeKupovineZaListuKupaca(nizIstorijeKupovine);


                this.popuniListuKupaca(service.IstorijaKupovineInicijalniPrikazKupaca());

                //SmartSoftwareServiceReference.DbItemOpremaSaParametrimaStatistika[] nizOpremeZaIstorijuKupovine = service.IstorijaKupovineNajprodavanijaOprema(false);
                //this.popuniListuOpremeSaParametrima(nizOpremeZaIstorijuKupovine);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
            }



        }

        private void btnUpravljanjeNarudzbinama_Click(object sender, RoutedEventArgs e)
        {

            if (!kliknutoNaDugmiceUpravljanja)
            {
                kliknutoNaDugmiceUpravljanja = true;
                kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                kojeDugmeJePoReduKliknuto = 7;

                foreach (var item in grdDugmici.Children)
                {
                    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                }
                btnUpravljanjeNarudzbinama.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbineSelect();
                this.popuniListuNarudzbina(nizNarudzbina);

                Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);

                if (timerProveraVisineListeNarudzbina == null)
                    timerProveraVisineListeNarudzbina = new DispatcherTimer();
                if (!timerProveraVisineListeNarudzbina.IsEnabled)
                {
                    timerProveraVisineListeNarudzbina.Interval = trajanje200;
                    timerProveraVisineListeNarudzbina.Tick += timerProveraVisineListeNarudzbina_Tick;
                    timerProveraVisineListeNarudzbina.Start();
                }


            }



        }

        private void pravilnoRasporediListuNarudzbina()
        {
            ControlTemplate ctp = cclUpravljanjeNarudzbinama.Template as ControlTemplate;
            ItemsControl iclNarudzbineProizvod = ctp.FindName("iclNarudzbineProizvod", cclUpravljanjeNarudzbinama) as ItemsControl;
            ItemsControl itcNarucilac = ctp.FindName("itcNarucilac", cclUpravljanjeNarudzbinama) as ItemsControl;


            if (iclNarudzbineProizvod != null && iclNarudzbineProizvod.Items != null)
            {
                int brojRedova = iclNarudzbineProizvod.Items.Count;
                if (brojRedova > 0)
                {
                    for (int i = 0; i < brojRedova; i++)
                    {
                        Grid grd = FindChild<Grid>(iclNarudzbineProizvod.ItemContainerGenerator.ContainerFromIndex(i));
                        TextBlock tblckNaslovProizvodaNarudzbine = grd.FindName("tblckNaslovProizvodaNarudzbine") as TextBlock;
                        TextBlock tblckNaslovProizvodaNarudzbineDetaljnije = grd.FindName("tblckNaslovProizvodaNarudzbineDetaljnije") as TextBlock;
                        if (tblckNaslovProizvodaNarudzbine != null)
                        {
                            if (tblckNaslovProizvodaNarudzbine.ActualHeight > (double)45)
                            {
                                if (ListaNarudzbina != null && ListaNarudzbina[i].Oprema != null)
                                    ListaNarudzbina[i].Oprema.DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova = true;

                                //tblckNaslovProizvodaNarudzbine.HorizontalAlignment = HorizontalAlignment.Left;

                                tblckNaslovProizvodaNarudzbineDetaljnije.Visibility = Visibility.Visible;
                                tblckNaslovProizvodaNarudzbineDetaljnije.Height = 15;
                                //tblckNaslovProizvodaNarudzbineDetaljnije.Width = tblckNaslovProizvodaNarudzbine.ActualWidth;
                            }
                            else
                            {
                                if (ListaNarudzbina != null && ListaNarudzbina[i].Oprema != null)
                                    ListaNarudzbina[i].Oprema.DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova = false;

                                //tblckNaslovProizvodaNarudzbine.HorizontalAlignment = HorizontalAlignment.Center;

                                tblckNaslovProizvodaNarudzbineDetaljnije.Visibility = Visibility.Hidden;
                                tblckNaslovProizvodaNarudzbineDetaljnije.Height = 0;
                            }
                        }




                    }
                }
                brojRedova = 0;
            }

            if (itcNarucilac != null && itcNarucilac.Items != null)
            {
                int brojRedova = itcNarucilac.Items.Count;
                if (brojRedova > 0)
                {
                    for (int i = 0; i < brojRedova; i++)
                    {
                        Grid grd = FindChild<Grid>(itcNarucilac.ItemContainerGenerator.ContainerFromIndex(i));

                        TextBlock tblckTekstImenaNarucioca = grd.FindName("tblckTekstImenaNarucioca") as TextBlock;
                        TextBlock tblckTekstPrezimenaNarucioca = grd.FindName("tblckTekstPrezimenaNarucioca") as TextBlock;
                        TextBlock tblckTekstMejlaNarucioca = grd.FindName("tblckTekstMejlaNarucioca") as TextBlock;
                        TextBlock tblckTekstImenaNaruciocaDetaljnije = grd.FindName("tblckTekstImenaNaruciocaDetaljnije") as TextBlock;
                        TextBlock tblckTekstPrezimenaNaruciocaDetaljnije = grd.FindName("tblckTekstPrezimenaNaruciocaDetaljnije") as TextBlock;
                        TextBlock tblckTekstMejlaNaruciocaDetaljnije = grd.FindName("tblckTekstMejlaNaruciocaDetaljnije") as TextBlock;

                        Grid grdTekstImenaNarucioca = grd.FindName("grdTekstImenaNarucioca") as Grid;
                        Grid grdTekstPrezimenaNarucioca = grd.FindName("grdTekstPrezimenaNarucioca") as Grid;
                        Grid grdTekstMejlaNarucioca = grd.FindName("grdTekstMejlaNarucioca") as Grid;


                        Grid grdTekstImenaNaruciocaDetaljnije = grd.FindName("grdTekstImenaNaruciocaDetaljnije") as Grid;
                        Grid grdTekstPrezimenaNaruciocaDetaljnije = grd.FindName("grdTekstPrezimenaNaruciocaDetaljnije") as Grid;
                        Border grdTekstMejlaNaruciocaDetaljnije = grd.FindName("grdTekstMejlaNaruciocaDetaljnije") as Border;





                        if (tblckTekstImenaNarucioca != null)
                        {
                            if (tblckTekstImenaNarucioca.ActualWidth > 95)
                            {
                                if (!ListaNarudzbina[i].Prodavac.DaLiTekstImenaNaruciocaZauzimaViseRedova)
                                {
                                    tblckTekstImenaNarucioca.Width = 95;
                                    ListaNarudzbina[i].Prodavac.DaLiTekstImenaNaruciocaZauzimaViseRedova = true;
                                }
                                tblckTekstImenaNarucioca.HorizontalAlignment = HorizontalAlignment.Left;

                                tblckTekstImenaNaruciocaDetaljnije.Visibility = Visibility.Visible;
                                tblckTekstImenaNaruciocaDetaljnije.Width = 15;
                                tblckTekstImenaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Right;
                                tblckTekstImenaNaruciocaDetaljnije.Margin = new Thickness(-15, 0, 0, 0);
                            }
                            else
                            {
                                if (ListaNarudzbina[i].Prodavac.DaLiTekstImenaNaruciocaZauzimaViseRedova)
                                {
                                    tblckTekstImenaNarucioca.Width = 110;
                                    ListaNarudzbina[i].Prodavac.DaLiTekstImenaNaruciocaZauzimaViseRedova = false;
                                }
                                tblckTekstImenaNarucioca.HorizontalAlignment = HorizontalAlignment.Center;


                                tblckTekstImenaNaruciocaDetaljnije.Visibility = Visibility.Hidden;
                                tblckTekstImenaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Center;
                                tblckTekstImenaNaruciocaDetaljnije.Width = 0;
                                tblckTekstImenaNaruciocaDetaljnije.Margin = new Thickness(0);
                            }
                        }
                        if (tblckTekstPrezimenaNarucioca != null)
                        {
                            if (tblckTekstPrezimenaNarucioca.ActualWidth > 95)
                            {
                                if (ListaNarudzbina != null && ListaNarudzbina[i].Prodavac != null)
                                {
                                    if (!ListaNarudzbina[i].Prodavac.DaLiTekstPrezimenaNaruciocaZauzimaViseRedova)
                                    {
                                        tblckTekstPrezimenaNarucioca.Width = 95;
                                        ListaNarudzbina[i].Prodavac.DaLiTekstPrezimenaNaruciocaZauzimaViseRedova = true;
                                    }
                                    tblckTekstPrezimenaNarucioca.HorizontalAlignment = HorizontalAlignment.Left;

                                    tblckTekstPrezimenaNaruciocaDetaljnije.Visibility = Visibility.Visible;
                                    tblckTekstPrezimenaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Right;
                                    tblckTekstPrezimenaNaruciocaDetaljnije.Width = 15;
                                    tblckTekstPrezimenaNaruciocaDetaljnije.Margin = new Thickness(-15, 0, 0, 0);
                                }
                            }
                            else
                            {
                                if (ListaNarudzbina[i].Prodavac.DaLiTekstPrezimenaNaruciocaZauzimaViseRedova)
                                {
                                    tblckTekstPrezimenaNarucioca.Width = 110;
                                    ListaNarudzbina[i].Prodavac.DaLiTekstPrezimenaNaruciocaZauzimaViseRedova = false;
                                }
                                tblckTekstPrezimenaNarucioca.HorizontalAlignment = HorizontalAlignment.Center;

                                tblckTekstPrezimenaNaruciocaDetaljnije.Visibility = Visibility.Hidden;
                                tblckTekstPrezimenaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Center;
                                tblckTekstPrezimenaNaruciocaDetaljnije.Width = 0;
                                tblckTekstPrezimenaNaruciocaDetaljnije.Margin = new Thickness(0);
                            }
                        }
                        if (tblckTekstMejlaNarucioca != null)
                        {
                            if (tblckTekstMejlaNarucioca.ActualWidth > 95)
                            {
                                if (ListaNarudzbina != null && ListaNarudzbina[i].Prodavac != null)
                                {
                                    if (!ListaNarudzbina[i].Prodavac.DaLiTekstMejlaNaruciocaZauzimaViseRedova)
                                    {
                                        tblckTekstMejlaNarucioca.Width = 95;
                                        ListaNarudzbina[i].Prodavac.DaLiTekstMejlaNaruciocaZauzimaViseRedova = true;
                                    }
                                    tblckTekstMejlaNarucioca.HorizontalAlignment = HorizontalAlignment.Left;

                                    tblckTekstMejlaNaruciocaDetaljnije.Visibility = Visibility.Visible;
                                    tblckTekstMejlaNaruciocaDetaljnije.Width = 15;
                                    tblckTekstMejlaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Right;
                                    tblckTekstMejlaNaruciocaDetaljnije.Margin = new Thickness(-15, 0, 0, 0);
                                }
                                else
                                {
                                    if (ListaNarudzbina[i].Prodavac.DaLiTekstMejlaNaruciocaZauzimaViseRedova)
                                    {
                                        tblckTekstMejlaNarucioca.Width = 110;
                                        ListaNarudzbina[i].Prodavac.DaLiTekstMejlaNaruciocaZauzimaViseRedova = false;
                                    }
                                    tblckTekstMejlaNarucioca.HorizontalAlignment = HorizontalAlignment.Center;

                                    tblckTekstMejlaNaruciocaDetaljnije.Visibility = Visibility.Hidden;
                                    tblckTekstMejlaNaruciocaDetaljnije.HorizontalAlignment = HorizontalAlignment.Center;
                                    tblckTekstMejlaNaruciocaDetaljnije.Width = 0;
                                    tblckTekstMejlaNaruciocaDetaljnije.Margin = new Thickness(0);
                                }
                            }



                        }
                    }
                }
            }
        }

        private void btnUnosOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosOblastiOpreme.Visibility = Visibility.Visible;


            this.CurrentOblastiOpreme = new OblastiOpreme(null);


        }



        #endregion







        private void btnDodajSliku_Click(object sender, RoutedEventArgs e)
        {

            // Korisnik dijalogom zadaje fajl iz koga se čita dokument
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            if (ofd.ShowDialog() ?? false == true)
            {
                this.currentOblastiOpreme.Picture = ofd.FileName;

            }
        }


        private void btnUnesiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOblastiOpreme = template.FindName("ctmUnosOblastiOpreme", Sadrzaj) as ContentControl;



            int pozicija = 0;
            string nazivSlike = null;
            string putanjaZaBazu = null;

            if (this.CurrentOblastiOpreme != null && this.CurrentOblastiOpreme.Name != null && this.CurrentOblastiOpreme.Name != "")
            {
                if (this.CurrentOblastiOpreme.Picture != null)
                {
                    pozicija = this.CurrentOblastiOpreme.Picture.LastIndexOf("\\");
                    nazivSlike = this.CurrentOblastiOpreme.Picture.Substring(pozicija + 1);

                    string putanjaPrefix = "\\slike\\oblasti_opreme\\";
                    System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentOblastiOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOblastiOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    putanjaZaBazu = putanjaPrefix + nazivSlike;
                }

                DbItemOblastiOpreme oblast = new DbItemOblastiOpreme()
                {
                    naziv_oblasti_opreme = currentOblastiOpreme.Name,
                    picture = putanjaZaBazu == null ? null : putanjaZaBazu
                };

                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeInsert(oblast);
                ctmUnosOblastiOpreme.Visibility = Visibility.Hidden;
                this.popuniListuOblasti(nizOblasti);
            }
            else
            {
                MessageBox.Show("Unesite naziv oblasti");
            }
        }


        private void btnAzurirajSlikuOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {

            switch(kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOblastiOpreme.Picture = ofd.FileName;
                this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = true;
                //ContentControl ctmPrikazDetaljaSadrzaj = Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                //this.daLiJeSlikaPromenjena = true;
            }
                    break;
                case 2:
                    OpenFileDialog ofdTipoviOpreme = new OpenFileDialog();
                    ofdTipoviOpreme.Filter = "Image file (*.jpg)|*.jpg";
                    if (ofdTipoviOpreme.ShowDialog() ?? false)
                    {
                        this.CurrentTipoviOpreme.Picture = ofdTipoviOpreme.FileName;
                    }
                    break;
                case 3:
                    OpenFileDialog ofdOprema = new OpenFileDialog();
                    ofdOprema.Filter = "Image file (*.jpg)|*.jpg";
                    if (ofdOprema.ShowDialog() ?? false)
                    {
                        this.CurrentOprema.Picture = ofdOprema.FileName;
                    }
                    break;
                case 4:
                    OpenFileDialog ofdParametri = new OpenFileDialog();
                    ofdParametri.Filter = "Image file (*.jpg)|*.jpg";
                    if (ofdParametri.ShowDialog() ?? false)
                    {
                        this.CurrentParametri.Picture = ofdParametri.FileName;
                    }
                    break;
                case 5:
                    OpenFileDialog ofdKorisnici = new OpenFileDialog();
                    ofdKorisnici.Filter = "Image file (*.jpg)|*.jpg";
                    if (ofdKorisnici.ShowDialog() ?? false)
                    {
                        this.CurrentKorisnici.SlikaKorisnika = ofdKorisnici.FileName;
                
                    }
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }

            
        }

        private void btnAzurirajOblastOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            string celaPutanjaDoslike = this.CurrentOblastiOpreme.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slika = celaPutanjaDoslike.Substring(pozicija + 1);
            string bla = this.CurrentOblastiOpreme.SlikaOriginalPutanja;
            int pozicijaZaPrefix = bla.LastIndexOf("/");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);


            System.IO.FileInfo fileInfo =
                      new System.IO.FileInfo(celaPutanjaDoslike);
            SmartSoftwareServiceInterfaceClient clientUpload =
                    new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.RemoteFileInfo
                   uploadRequestInfo = new RemoteFileInfo();

            using (System.IO.FileStream stream =
                   new System.IO.FileStream(celaPutanjaDoslike,
                   System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                uploadRequestInfo.FileName = slika;
                uploadRequestInfo.Length = fileInfo.Length;
                uploadRequestInfo.FileByteStream = stream;
                slika = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
            }

            DbItemOblastiOpreme o = new DbItemOblastiOpreme()
            {
                id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme,
                naziv_oblasti_opreme = this.CurrentOblastiOpreme.Name,
                picture = slikaPrefix + slika
            };
            if (o != null || o.id_oblasti_opreme != 0)
            {
                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeUpdate(o);
                ctmPrikazDetalja.Visibility = Visibility.Hidden;
                this.popuniListuOblasti(nizOblasti);

            }
        }

        private void btnObrisiOblastOpreme_Click(object sender, RoutedEventArgs e)
        {

            TextBlock tblckVracenTextZaDugme = FindChild<TextBlock>(sender as Button);
            string pera = tblckVracenTextZaDugme.Text;
            if (pera == "Vrati iz arhive ovu oblast opreme")
            {

                string poruka = "Prodavci će videti ovu oblast opreme. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        DbItemOblastiOpreme o = new DbItemOblastiOpreme()
                        {
                            id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme
                        };

                        if (o != null || o.id_oblasti_opreme != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OpOblastiOpremeRestore(o);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOblasti(nizOblasti);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneOblastiOpreme_Click();
                        }

                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da arhivirate ovu oblast i njene tipove?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        DbItemOblastiOpreme o = new DbItemOblastiOpreme()
                        {
                            id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme
                        };

                        if (o != null || o.id_oblasti_opreme != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OpOblastiOpremeDelete(o);
                           // ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOblasti(nizOblasti);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneOblastiOpreme_Click();
                        }
                        this.otkaziIzmeneOblastiOpreme_Click();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            
            this.pravilnoRasporediListuKorisnika();
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

        private void tbImeOblastiOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            if ((this.CurrentOblastiOpreme.DaliMozeDaSeAzurira == false) || (this.daLiJeSlikaPromenjena == false))
            {
                ContentControl ctmPrikazDetaljaSadrzaj = Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                TextBox tekstBoks = sender as TextBox;
                string tekst = tekstBoks.Text;
                string tekstTrimovan = tekst.Trim();

                if (tekst == this.CurrentOblastiOpreme.Name || tekstTrimovan == "")
                {
                    this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = false;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.White;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat2", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.White;
                }
                else
                {
                    this.CurrentOblastiOpreme.DaliMozeDaSeAzurira = true;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.LightGreen;
                    //(ctmPrikazDetaljaSadrzaj.Template.FindName("Kvadrat2", ctmPrikazDetaljaSadrzaj) as Rectangle).Fill = Brushes.LightGreen;
                }
            }
        }

        private void btnUnosTipoviOpreme_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentTipoviOpreme = new TipoviOpreme(null);
            (Sadrzaj.Template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Visible;
            this.IzabranaOblastOpreme = 0;
        }

        private void GridPrikazRezultataTipovaOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpreme();
            //this.popuniListuTipovaOpreme(nizTipovaOblasti);


            if (this.CurrentTipoviOpreme != null)
            {
                ContentControl ctmPrikazDetaljaSadrzaj = (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl);
                string tekstIme = (ctmPrikazDetaljaSadrzaj.Template.FindName("tbImeTipoviOpreme", ctmPrikazDetaljaSadrzaj) as TextBox).Text;
                this.CurrentTipoviOpreme.Name = this.CurrentTipoviOpreme.StaroImeTipa;

                if (!this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena && tekstIme != this.CurrentTipoviOpreme.StaroImeTipa)
                {
                    this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = false;
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = false;
                    this.CurrentTipoviOpreme.DaLiJeOblastPromenjena = false;

                }
            }
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosTipaOpreme = template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosTipaOpreme.Visibility = Visibility.Hidden;

            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
            this.popuniListuOblasti(nizOblasti);





            oblasti = CollectionViewSource.GetDefaultView(this.ListaTipovaOpreme);
            t = oblasti.CurrentItem as TipoviOpreme;
            this.CurrentTipoviOpreme = t;

            for (int i = 0; i < this.ListaOblastiOpremeZaTipoveOpreme.Count; i++)
            {
                if (this.ListaOblastiOpremeZaTipoveOpreme[i].IdOblastiOpreme == this.CurrentTipoviOpreme.IdOblastiOpreme)
                {
                    IzabranaOblastOpreme = i;
                }
            }

            template = null;
        }

        private void GridPrikazRezultataKorisnika_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            Grid grdDodatniGrid = template.FindName("grdDodatniGrid", Sadrzaj) as Grid;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosKorisnici.Visibility = Visibility.Hidden;
            grdDodatniGrid.Visibility = Visibility.Visible;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaKorisnika);
            k = oblasti.CurrentItem as Korisnici;
            this.CurrentKorisnici = k;
        }

        private void GridPrikazRezultataOpreme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosOpreme.Visibility = Visibility.Hidden;
            slucajnoUProlazu = false;

            ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            ControlTemplate opremaTemplate = ctmPrikazDetalja.Template;
            ItemsControl listaVrednostiParametara = opremaTemplate.FindName("listaVrednostiParametara", ctmPrikazDetalja) as ItemsControl;
            ItemsControl icListaParametaraLevo = opremaTemplate.FindName("icListaParametaraLevo", ctmPrikazDetalja) as ItemsControl;



            oblasti = CollectionViewSource.GetDefaultView(this.ListaOpreme);
            o = oblasti.CurrentItem as Oprema;
            this.CurrentOprema = o;

            for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            {
                if (this.CurrentOprema.IdTipOpreme == listaTipovaOpreme[i].IdTipOpreme)
                {
                    this.CurrentOprema.IzabranTipOpreme = i;
                }
            }
            icListaParametaraLevo.Items.Clear();
            listaVrednostiParametara.Items.Clear();

            if (this.CurrentOprema.KolekcijaOpreme != null || this.CurrentOprema.KolekcijaOpreme.Count > 0)
            {
                DbItemTipOpreme[] t = service.TipOpremeAdminPanel();

                foreach (var item in this.CurrentOprema.KolekcijaOpreme)
                {


                }
            }



            foreach (var item in this.CurrentOprema.ListaParametara)
            {


                StackPanel stpDesno = new StackPanel();
                Separator separatorDesno = new Separator();
                Style stilDesno = separatorDesno.Style;
                ToolBar tulbarDesno = new ToolBar();
                stilDesno = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorDesno.Background = Brushes.LightGray;
                stpDesno.Children.Add(separatorDesno);

                StackPanel stpLevo = new StackPanel();
                Separator separatorLevo = new Separator();
                Style stilLevo = separatorLevo.Style;
                ToolBar tulbarLevo = new ToolBar();
                stilLevo = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorLevo.Background = Brushes.LightGray;
                stpLevo.Children.Add(separatorLevo);






                if (item.TipParametra == "BIT")
                {

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;




                    CheckBox chb = new CheckBox();
                    //if (parametar.VrednostParametra == "0")
                    //    chb.IsChecked = true;
                    //else chb.IsChecked = false;
                    chb.HorizontalAlignment = HorizontalAlignment.Center;

                    BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(chb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    icListaParametaraLevo.Items.Add(grdZaListuLevo);
                    listaVrednostiParametara.Items.Add(grdZaListu);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(chb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);

                }
                else if (item.TipParametra == "TEXT")
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;

                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(120);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(120);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);



                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
                else
                {
                    Binding bindingg = new Binding();
                    bindingg.Path = new PropertyPath("VrednostParametra");
                    bindingg.Source = item;
                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();


                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
            }



            slucajnoUProlazu = false;

        }

        private void btnUnesiTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentTipoviOpreme != null && this.CurrentTipoviOpreme.IdOblastiOpreme != -1 && this.CurrentTipoviOpreme.Name != null && this.CurrentTipoviOpreme.Name != "")
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                int pozicija = 0;
                string nazivSlike = null;
                string putanjaZaBazu = null;


                if (this.CurrentTipoviOpreme.Picture != null)
                {
                    pozicija = this.CurrentTipoviOpreme.Picture.LastIndexOf("\\");
                    nazivSlike = this.CurrentTipoviOpreme.Picture.Substring(pozicija + 1);

                    string putanjaPrefix = "\\slike\\tipovi_opreme\\";
                    System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentTipoviOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentTipoviOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    putanjaZaBazu = putanjaPrefix + nazivSlike;
                }

                DbItemTipOpreme tip = new DbItemTipOpreme()
                {
                    naziv_tipa = CurrentTipoviOpreme.Name,
                    slika_tipa = putanjaZaBazu == null ? null : putanjaZaBazu,
                    id_oblasti_opreme = IzabranaOblastOpreme + 1
                };

                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeInsert(tip);
                (Sadrzaj.Template.FindName("ctmUnosTipaOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                this.popuniListuTipovaOpreme(nizTipova);
            }
            else
            {
                MessageBox.Show("Unesite naziv tipa");
            }

        }

        private void btnDodajSlikuTipaOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpg)|*.jpg"; // Ekstenzija ppp je ekstenzija dokumenta aplikacije Presentation
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentTipoviOpreme.Picture = ofd.FileName;
                this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena = true;
            }
        }

        private void btnObrisiTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tblckVracenTextZaDugme = FindChild<TextBlock>(sender as Button);
            string pera = tblckVracenTextZaDugme.Text;
            if (pera == "Vrati iz arhive ovaj tip opreme")
            {

                string poruka = "Prodavci će videti ovaj tip opreme. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        DbItemTipOpreme t = new DbItemTipOpreme()
                        {
                            id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
                        };

                        if (t != null || t.id_tip_opreme != 0)
                        {
                            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeRestore(t);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuTipovaOpreme(nizTipova);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneTipoviOpreme_Click();
                        }

                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da uklonite ovaj tip opreme i njenu opremu?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        DbItemTipOpreme t = new DbItemTipOpreme()
                        {
                            id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
                        };

                        if (t != null || t.id_tip_opreme != 0)
                        {
                            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeDelete(t);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuTipovaOpreme(nizTipova);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneTipoviOpreme_Click();
                        }

                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }

            this.pravilnoRasporediListuKorisnika();
        }

        private void btnAzurirajTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            string celaPutanjaDoslike = this.CurrentTipoviOpreme.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slika = celaPutanjaDoslike.Substring(pozicija + 1);
            string bla = this.CurrentTipoviOpreme.SlikaOriginalPutanja;

            //ovo je ispravka za sliku ako nije upisana kad se radio insert
            if (bla == null)
            {
                //string putanjaPrefix = 
                bla = "\\slike\\tipovi_opreme\\";
            }
            int pozicijaZaPrefix = bla.LastIndexOf("\\");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);
            string promenjanaSlika = slikaPrefix + slika;


            //ovdje je ovo dje smo 

            if (this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == true)
            {
                System.IO.FileInfo fileInfo =
                            new System.IO.FileInfo(celaPutanjaDoslike);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                        uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                        new System.IO.FileStream(celaPutanjaDoslike,
                        System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = slika;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    slika = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
                }
            }

            DbItemTipOpreme t = new DbItemTipOpreme()
            {
                id_oblasti_opreme = this.CurrentTipoviOpreme.IdOblastiOpreme,
                naziv_tipa = this.CurrentTipoviOpreme.Name,
                slika_tipa = promenjanaSlika,
                id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme
            };
            if (t != null || t.id_tip_opreme != 0)
            {
                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeUpdate(t);
                ctmPrikazDetalja.Visibility = Visibility.Hidden;
                this.popuniListuTipovaOpreme(nizTipova);

            }
        }

        private void tbImeTipoviOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            if ((this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira == false) || (this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == false) || (this.CurrentTipoviOpreme.DaLiJeOblastPromenjena == false))
            {
                TextBox tekstBoks = sender as TextBox;
                string tekst = tekstBoks.Text;
                string tekstTrimovan = tekst.Trim();

                if (tekst == this.CurrentTipoviOpreme.StaroImeTipa || tekstTrimovan == "")
                {
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = false;
                    if (this.CurrentTipoviOpreme.DaLiJeOblastPromenjena == false && this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena == false)
                        this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = false;
                }
                else
                {
                    this.CurrentTipoviOpreme.DaLiJeTekstTipaOpremePromenjen = true;
                    this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
                }
            }
        }

        private void btnAzurirajSlikuTipaOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentTipoviOpreme.Picture = ofd.FileName;
                this.CurrentTipoviOpreme.DaliMozeTipDaSeAzurira = true;
                this.CurrentTipoviOpreme.DaLiJeSlikaTipaPromenjena = true;
            }
        }

        private void cmbListaOblastiOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IzabranaOblastOpreme >= 0)
            {

                if(this.CurrentTipoviOpreme != null)
                    this.CurrentTipoviOpreme.NazivOblastiOpreme = this.ListaOblastiOpremeZaTipoveOpreme[Convert.ToInt32(IzabranaOblastOpreme.Value)].Name;
                
                
            }
        }

        private void tbPretragaOblastiOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] oblastiOpreme = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                oblastiOpreme = service.OblastiOpremeAdminPanel();
            }
            else
            {
                oblastiOpreme = service.PretragaOblastiOpreme(zaPretragu);

            }
            this.popuniListuOblasti(oblastiOpreme);
            this.pravilnoRasporediListuKorisnika();
        }

        private void tbPretragaTipovaOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] tipoviOpreme = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                tipoviOpreme = service.TipOpremeAdminPanel();
            }
            else
            {
                tipoviOpreme = service.PretragaTipovaOpreme(zaPretragu);

            }
            this.popuniListuTipovaOpreme(tipoviOpreme);
            this.pravilnoRasporediListuKorisnika();
        }

        private void btnAzurirajKorisnika_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


            DbItemKorisnici korisniciAzuriranje = new DbItemKorisnici()
            {
                id_korisnici = this.CurrentKorisnici.IdKorisnici,
                id_uloge = this.CurrentKorisnici.IzabranaUloga + 1,
                ime = this.CurrentKorisnici.ImeKorisnika,
                prezime = this.CurrentKorisnici.PrezimeKorisnika,
                mejl = this.CurrentKorisnici.MejlKorisnika,
                broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                brojOstvarenihPoena = Convert.ToInt32(this.CurrentKorisnici.BrojOstvarenihPoena),
                username = this.CurrentKorisnici.Username,
                lozinka = this.CurrentKorisnici.Lozinka,
                //naziv_uloge = this.CurrentKorisnici.NazivUloge,
                //deletedField = this.CurrentKorisnici.DeletedItem
            };

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciUpdate(korisniciAzuriranje);
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            this.popuniListuKorisnici(nizKorisnika);
        }





        private void btnUnosKorisnici_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosKorisnici.Visibility = Visibility.Visible;


            this.CurrentKorisnici = new Korisnici();
        }

        private void btnUnesiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentKorisnici != null)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemKorisnici korisnik = new DbItemKorisnici()
                {
                    ime = this.CurrentKorisnici.ImeKorisnika,
                    prezime = this.CurrentKorisnici.PrezimeKorisnika,
                    id_uloge = this.CurrentKorisnici.IzabranaUloga + 1,
                    mejl = this.CurrentKorisnici.MejlKorisnika,
                    broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                    brojOstvarenihPoena = (int)(this.CurrentKorisnici.BrojOstvarenihPoena),
                    username = this.CurrentKorisnici.Username,
                    lozinka = this.CurrentKorisnici.Lozinka
                    //naziv_uloge = this.CurrentKorisnici.NazivUloge
                };

                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciInsert(korisnik);
                (Sadrzaj.Template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                (Sadrzaj.Template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl).Visibility = Visibility.Hidden;
                this.popuniListuKorisnici(nizKorisnika);
            }
            else
            {
                MessageBox.Show("Popunite sva polja");
            }
        }

        private void btnAzurirajOpremu_Click(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;



            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            string celaPutanjaDoslike = this.CurrentOprema.Picture;
            int pozicija = celaPutanjaDoslike.LastIndexOf("\\");
            string slikaa = celaPutanjaDoslike.Substring(pozicija + 1);
            string bla = this.CurrentOprema.SlikaOriginalPutanja;

            if (bla == null)
            {
                //string putanjaPrefix = 
                bla = "\\slike\\oprema\\";
            }

            int pozicijaZaPrefix = bla.LastIndexOf("\\");
            string slikaPrefix = bla.Substring(0, pozicijaZaPrefix + 1);



            if (this.CurrentOprema.DaLiJeSlikaOpremePromenjena == true)
            {

                System.IO.FileInfo fileInfo =
                          new System.IO.FileInfo(celaPutanjaDoslike);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                       uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                       new System.IO.FileStream(celaPutanjaDoslike,
                       System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = slikaa;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    slikaa = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, slikaPrefix, uploadRequestInfo.FileByteStream);
                }

            }


            DbItemOpremaSaParametrima opremaZaAzuriranje = new DbItemOpremaSaParametrima()
            {
                cena = this.CurrentOprema.Cena,
                id_oprema = this.CurrentOprema.IdOprema,
                id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                kolicinaUKorpi = (int)this.CurrentOprema.TmpIzabranaKolicina,
                kolicinaURezervacijama = this.CurrentOprema.IzabranaKolicina,
                lager = this.CurrentOprema.Lager,
                model = this.CurrentOprema.Model,
                naslov = this.CurrentOprema.Name,
                opis = this.CurrentOprema.Opis,
                oprema_na_popustu = this.CurrentOprema.OpremaNaPopustu,
                proizvodjac = this.CurrentOprema.Proizvodjac,
                slika = slikaPrefix + slikaa
            };

            List<DbItemParametri> parametri = new List<DbItemParametri>();

            foreach (var item in this.CurrentOprema.ListaParametara)
            {
                parametri.Add(new DbItemParametri()
                {
                    default_vrednost = item.DefaultVrednost,
                    id_parametri = item.IdParametri,
                    id_tip_opreme = item.IdTipOpreme,
                    naziv_parametra = item.Name,
                    tipParametra = item.TipParametra,
                    vrednost_parametra = item.VrednostParametra
                });
            }

            opremaZaAzuriranje.ListaParametara = parametri.ToArray();




            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelUpdate(opremaZaAzuriranje);
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            this.popuniListuOpremeSaParametrima(nizOpreme);
        }

        private void btnAzurirajSlikuOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOprema.Picture = ofd.FileName;
                this.CurrentOprema.DaLiJeSlikaOpremePromenjena = true;
            }
        }

        private void btnUnosOpreme_Click(object sender, RoutedEventArgs e)
        {
            ucitaloSE = false;
            idiNaCMB = false;

            this.CurrentOprema = new Oprema(null);
            (Sadrzaj.Template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl).Visibility = Visibility.Visible;
            this.CurrentOprema.IzabranTipOpreme = 0;


            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;

            ListaTipovaOpreme = null;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            ControlTemplate opremaTemplate = ctmUnosOpreme.Template;
            ItemsControl listaVrednostiParametara = opremaTemplate.FindName("listaVrednostiParametara", ctmUnosOpreme) as ItemsControl;
            ItemsControl icListaParametaraLevo = opremaTemplate.FindName("icListaParametaraLevo", ctmUnosOpreme) as ItemsControl;


            icListaParametaraLevo.Items.Clear();
            listaVrednostiParametara.Items.Clear();



            //for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            //{
            //    if (this.CurrentOprema.IdTipOpreme == listaTipovaOpreme[i].IdTipOpreme)
            //    {
            //        this.CurrentOprema.IzabranTipOpreme = i;
            //    }
            //}



            //for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            //{
            //    if (this.CurrentOprema.IzabranTipOpreme == i)
            //    {
            //        this.CurrentOprema.IdTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
            //    }
            //}

            this.CurrentOprema.IzabranTipOpreme = 1;

            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(1);

            this.CurrentOprema = new Oprema(null);



            //ovdje treba veb servis


            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.CurrentOprema.ListaParametara.Add(new Parametri(null)
                {
                    DefaultVrednost = nizParametara[i].default_vrednost,
                    IdParametri = nizParametara[i].id_parametri,
                    IdTipOpreme = nizParametara[i].id_tip_opreme,
                    VrednostParametra = nizParametara[i].vrednost_parametra,
                    Name = nizParametara[i].naziv_parametra,
                    TipParametra = nizParametara[i].tipParametra
                });
            }



            foreach (var item in this.CurrentOprema.ListaParametara)
            {


                StackPanel stpDesno = new StackPanel();
                Separator separatorDesno = new Separator();
                Style stilDesno = separatorDesno.Style;
                ToolBar tulbarDesno = new ToolBar();
                stilDesno = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorDesno.Background = Brushes.LightGray;
                stpDesno.Children.Add(separatorDesno);

                StackPanel stpLevo = new StackPanel();
                Separator separatorLevo = new Separator();
                Style stilLevo = separatorLevo.Style;
                ToolBar tulbarLevo = new ToolBar();
                stilLevo = this.FindResource(ToolBar.SeparatorStyleKey) as Style;
                separatorLevo.Background = Brushes.LightGray;
                stpLevo.Children.Add(separatorLevo);






                if (item.TipParametra == "bit")
                {

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;




                    CheckBox chb = new CheckBox();
                    //if (parametar.VrednostParametra == "0")
                    //    chb.IsChecked = true;
                    //else chb.IsChecked = false;
                    chb.HorizontalAlignment = HorizontalAlignment.Center;

                    BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(chb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    icListaParametaraLevo.Items.Add(grdZaListuLevo);
                    listaVrednostiParametara.Items.Add(grdZaListu);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(chb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);

                }
                else if (item.TipParametra == "text")
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = item;

                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();

                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(120);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(120);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);



                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
                else
                {
                    Binding bindingg = new Binding();
                    bindingg.Path = new PropertyPath("VrednostParametra");
                    bindingg.Source = item;
                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                    Label lbl = new Label();
                    lbl.Content = item.Name;

                    Grid grdZaListu = new Grid();
                    Grid grdZaListuLevo = new Grid();


                    RowDefinition redDesno1 = new RowDefinition();
                    redDesno1.Height = new GridLength(5);
                    RowDefinition redDesno2 = new RowDefinition();
                    redDesno2.Height = new GridLength(30);
                    grdZaListu.RowDefinitions.Add(redDesno1);
                    grdZaListu.RowDefinitions.Add(redDesno2);

                    Grid.SetRow(stpDesno, 0);
                    Grid.SetRow(tb, 1);

                    RowDefinition redLevo1 = new RowDefinition();
                    redLevo1.Height = new GridLength(5);
                    RowDefinition redLevo2 = new RowDefinition();
                    redLevo2.Height = new GridLength(30);

                    grdZaListuLevo.RowDefinitions.Add(redLevo1);
                    grdZaListuLevo.RowDefinitions.Add(redLevo2);

                    Grid.SetRow(stpLevo, 0);
                    Grid.SetRow(lbl, 1);

                    listaVrednostiParametara.Items.Add(grdZaListu);
                    icListaParametaraLevo.Items.Add(grdZaListuLevo);

                    grdZaListu.Children.Add(stpDesno);
                    grdZaListu.Children.Add(tb);

                    grdZaListuLevo.Children.Add(stpLevo);
                    grdZaListuLevo.Children.Add(lbl);
                }
            }



            ucitaloSE = true;
            idiNaCMB = true;
            slucajnoUProlazu = true;
        }

        private bool idiNaCMB = false;
        private bool ucitaloSE = false;
        private bool slucajnoUProlazu = false;

        private void cmbListaTipovaOpremeZaOpremu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dalijeUnosUToku)
            {
                if (this.CurrentOprema != null && this.CurrentOprema.IzabranTipOpreme == -1)
                    this.CurrentOprema.IzabranTipOpreme = 0;

                if (this.CurrentOprema != null && this.CurrentOprema.IzabranTipOpreme == 0)
                {
                    listaVrednostiParametara.ItemsSource = null;
                }



                ComboBox cmb = sender as ComboBox;
                if (cmb.IsLoaded)
                {

                }
                if (tblckOdabirTipaOpremeZaOpremu.Foreground != Brushes.Black && this.CurrentOprema.IdOprema == 0 && slucajnoUProlazu && this.CurrentOprema.IzabranTipOpreme >= 1)
                {

                  


                    if (ListaTipovaOpremeZaUnosOpreme == null)
                    {
                        this.CurrentOprema.IdTipOpreme = 1;

                    }
                    else
                    {


                        for (int i = 1; i < ListaTipovaOpremeZaUnosOpreme.Count; i++)
                        {
                            if (this.CurrentOprema.IzabranTipOpreme == i)
                            {
                                this.CurrentOprema.IdTipOpreme = ListaTipovaOpremeZaUnosOpreme[i].IdTipOpreme;
                                break;
                            }
                        }
                    }
                    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                    SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(this.CurrentOprema.IdTipOpreme);

                   

                    this.CurrentOprema.ListaParametara.Clear();
                    for (int i = 0; i < nizParametara.Length; i++)
                    {
                        this.CurrentOprema.ListaParametara.Add(new Parametri(null)
                        {
                            DefaultVrednost = nizParametara[i].default_vrednost,
                            IdParametri = nizParametara[i].id_parametri,
                            IdTipOpreme = nizParametara[i].id_tip_opreme,
                            VrednostParametra = nizParametara[i].vrednost_parametra,
                            Name = nizParametara[i].naziv_parametra,
                            TipParametra = nizParametara[i].tipParametra
                        });
                    }
                    this.popuniListuZaPrikazDetaljaOpreme();


                   

                }
            }

        }

        private void btnUnesiOpremu_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOpreme = template.FindName("ctmUnosOpreme", Sadrzaj) as ContentControl;

            int pozicija = 0;
            string nazivSlike = null;
            string putanjaZaBazu = null;

            if (this.CurrentOprema != null && this.CurrentOprema.Name != null && this.CurrentOprema.Name != "")
            {
                if (this.CurrentOprema.Picture != null)
                {
                    pozicija = this.CurrentOprema.Picture.LastIndexOf("\\");
                    nazivSlike = this.CurrentOprema.Picture.Substring(pozicija + 1);





                    //ovde treba da se radi ona zajebancija sa putanjama



                    //string putanjaPrefix = "\\slike\\oblasti_opreme\\";


                    //treba valjda dinamicki da se ubacuje u foldere

                    string putanjaPrefix = "\\slike\\oprema\\probaFolder\\";




                    System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentOprema.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOprema.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    putanjaZaBazu = putanjaPrefix + nazivSlike;
                }


                DbItemOpremaSaParametrima opremaZaInsert = new DbItemOpremaSaParametrima()
                {
                    cena = this.CurrentOprema.Cena,
                    id_oprema = this.CurrentOprema.IdOprema,
                    id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                    kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                    kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                    kolicinaUKorpi = (int)this.CurrentOprema.TmpIzabranaKolicina,
                    kolicinaURezervacijama = this.CurrentOprema.IzabranaKolicina,
                    lager = this.CurrentOprema.Lager,
                    model = this.CurrentOprema.Model,
                    naslov = this.CurrentOprema.Name,
                    opis = this.CurrentOprema.Opis,
                    oprema_na_popustu = this.CurrentOprema.OpremaNaPopustu,
                    proizvodjac = this.CurrentOprema.Proizvodjac,
                    slika = putanjaZaBazu
                };

                List<DbItemParametri> parametri = new List<DbItemParametri>();

                foreach (var item in this.CurrentOprema.ListaParametara)
                {
                    parametri.Add(new DbItemParametri()
                    {
                        default_vrednost = item.DefaultVrednost,
                        id_parametri = item.IdParametri,
                        id_tip_opreme = item.IdTipOpreme,
                        naziv_parametra = item.Name,
                        tipParametra = item.TipParametra,
                        vrednost_parametra = item.VrednostParametra
                    });
                }

                opremaZaInsert.ListaParametara = parametri.ToArray();


                //ovde dole trba druga opreracija tj za insert opreme

                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelInsert(opremaZaInsert);
                ctmUnosOpreme.Visibility = Visibility.Hidden;
                this.popuniListuOpremeSaParametrima(nizOpreme);


            }
            else
            {
                MessageBox.Show("Unesite naziv oblasti");
            }
        }

        private void btnDodajSlikuOpreme_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.jpg)|*.jpg";
            if (ofd.ShowDialog() ?? false == true)
            {
                this.CurrentOprema.Picture = ofd.FileName;
                this.CurrentOprema.DaLiJeSlikaOpremePromenjena = true;
            }
        }

        private void btnObrisiOpremu_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tblckVracenTextZaDugme = FindChild<TextBlock>(sender as Button);
            string pera = tblckVracenTextZaDugme.Text;
            if (pera == "Vrati iz arhive ovu opremu")
            {

                string poruka = "Prodavci će videti ovu opremu. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentOprema.IdOprema;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaRestore(id);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOpremeSaParametrima(nizOpreme);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneOpreme_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Ovime ćete ovu opremu samo sakriti za prodavca. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentOprema.IdOprema;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaDelete(id);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuOpremeSaParametrima(nizOpreme);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneOpreme_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }

            this.pravilnoRasporediListuKorisnika();

        }



        private void GridPrikazRezultataParametara_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Visible;
            ctmUnosKorisnici.Visibility = Visibility.Hidden;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            oblasti = CollectionViewSource.GetDefaultView(this.ListaParametara);
            Parametri p = oblasti.CurrentItem as Parametri;
            this.CurrentParametri = p;

            ControlTemplate template2 = ctmPrikazDetalja.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmPrikazDetalja) as ComboBox;
            cmbUnosParametara.ItemsSource = Enum.GetNames(typeof(TipParametara));
            string[] listaTipovaParametara;
            listaTipovaParametara = Enum.GetNames(typeof(TipParametara)) as string[];
            cmbUnosParametara.SelectedIndex = 0;
            if (cmbUnosParametara != null)
            {
                if (cmbUnosParametara.Items.Count > 0)
                {
                    for (int i = 0; i < listaTipovaParametara.Length; i++)
                    {
                        if ((listaTipovaParametara[i] as string) == this.CurrentParametri.TipParametra)
                        {
                            cmbUnosParametara.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }



            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (this.ListaTipovaOpreme[i].IdTipOpreme == this.CurrentParametri.IdTipOpreme)
                {
                    this.CurrentParametri.IzabranTipOpreme = i;
                    break;
                }
            }



        }

        //private TipParametara VratiTipParametaraZaParametar(Parametri p)
        //{
        //    switch(p.TipParametra)
        //    {
        //        case ""if (val.Equals("astringvalue", StringComparison.InvariantCultureIgnoreCase))
        //    } //e izvini zvao me brat nismo se culi 100 godina evo sad cu da prekinem samo ti polako
        //}


        private void btnUnosParametara_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ContentControl ctmlevaStranaSadrzaj = template.FindName("ctmlevaStranaSadrzaj", Sadrzaj) as ContentControl;
            ctmPrikazDetalja.Visibility = Visibility.Hidden;
            ctmUnosKorisnici.Visibility = Visibility.Visible;


            ControlTemplate template2 = ctmUnosKorisnici.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmUnosKorisnici) as ComboBox;
            cmbUnosParametara.ItemsSource = Enum.GetValues(typeof(TipParametara));
            string[] listaTipovaParametara;
            listaTipovaParametara = Enum.GetNames(typeof(TipParametara)) as string[];
            cmbUnosParametara.SelectedIndex = 0;
            if (cmbUnosParametara != null && cmbUnosParametara.Items.Count > 0 && listaTipovaParametara != null && listaTipovaParametara.Length > 0)
            {
                for (int i = 0; i < listaTipovaParametara.Length; i++)
                {
                    if ((listaTipovaParametara[i] as string) == this.CurrentParametri.TipParametra)
                    {
                        cmbUnosParametara.SelectedIndex = i;
                        break;
                    }
                }

            }


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipovaOblasti);

            this.CurrentParametri = new Parametri(null);




        }



        private void popuniListuParametara(DbItemParametri[] nizParametara)
        {

            ObservableCollection<Parametri> ListaParametaraTemp = new ObservableCollection<Parametri>();

            for (int i = 0; i < nizParametara.Length; i++)
            {

                Parametri p = new Parametri(null)
                 {
                     DefaultVrednost = nizParametara[i].default_vrednost,
                     IdParametri = nizParametara[i].id_parametri,
                     IdTipOpreme = nizParametara[i].id_tip_opreme,
                     VrednostParametra = nizParametara[i].vrednost_parametra,
                     Name = nizParametara[i].naziv_parametra,
                     TipParametra = nizParametara[i].tipParametra,
                     ParametarJeIFilter = nizParametara[i].za_filter,
                     DeletedItem = nizParametara[i].deletedField
                 };
                if (this.ListaTipovaOpreme != null && this.ListaTipovaOpreme.Count > 0)
                {
                    for (int j = 0; j < this.ListaTipovaOpreme.Count; j++)
                    {
                        if (p.IdTipOpreme == this.ListaTipovaOpreme[j].IdTipOpreme)
                        {
                            p.NazivTipa = this.ListaTipovaOpreme[j].Name;
                            break;
                        }
                    }
                }

                ListaParametaraTemp.Add(p);
            }
            ListaParametara = ListaParametaraTemp;
        }

        private void popuniListuParametaraPomocno(DbItemParametri[] nizParametara)
        {

            this.ListaParametaraPomocno = new ObservableCollection<Parametri>();
            for (int i = 0; i < nizParametara.Length; i++)
            {
                this.ListaParametaraPomocno.Add(new Parametri(null)
                {
                    DefaultVrednost = nizParametara[i].default_vrednost,
                    IdParametri = nizParametara[i].id_parametri,
                    IdTipOpreme = nizParametara[i].id_tip_opreme,
                    VrednostParametra = nizParametara[i].vrednost_parametra,
                    Name = nizParametara[i].naziv_parametra,
                    TipParametra = nizParametara[i].tipParametra,
                    ParametarJeIFilter = nizParametara[i].za_filter,
                    DeletedItem = nizParametara[i].deletedField
                });

                
            }
        }


        private void btnUnesiParametar_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentParametri == null)
            {
                return;
            }

            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (i == this.CurrentParametri.IzabranTipOpreme)
                {
                    this.CurrentParametri.IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme;
                    break;
                }
            }

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosKorisnici = template.FindName("ctmUnosKorisnici", Sadrzaj) as ContentControl;

            ControlTemplate template2 = ctmUnosKorisnici.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmUnosKorisnici) as ComboBox;
            //MessageBox.Show(cmbUnosParametara.SelectedValue.ToString());
            this.CurrentParametri.TipParametra = cmbTipoviParametra.SelectedValue.ToString();


            DbItemParametri p = new DbItemParametri()
            {
                default_vrednost = this.currentParametri.DefaultVrednost,
                //id_parametri = this.currentParametri.IdParametri,
                id_tip_opreme = this.currentParametri.IdTipOpreme,
                naziv_parametra = this.currentParametri.Name,
                vrednost_parametra = this.currentParametri.VrednostParametra,
                za_filter = this.currentParametri.ParametarJeIFilter,
                tipParametra = this.currentParametri.TipParametra
            };

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriInsert(p);
            this.popuniListuParametara(nizParametara);
            otkaziIzmeneParametri_Click();
            this.pravilnoRasporediListuKorisnika();
        }

        private void btnAzurirajParametar_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentParametri == null)
            {
                return;
            }

            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (i == this.currentParametri.IzabranTipOpreme)
                {
                    this.currentParametri.IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme;
                    break;
                }
            }

            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            ControlTemplate template2 = ctmPrikazDetalja.Template;
            ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmPrikazDetalja) as ComboBox;
            //MessageBox.Show(cmbUnosParametara.SelectedValue.ToString());
            this.currentParametri.TipParametra = cmbUnosParametara.SelectedValue.ToString();




            DbItemParametri p = new DbItemParametri()
            {
                default_vrednost = this.currentParametri.DefaultVrednost,
                id_parametri = this.currentParametri.IdParametri,
                id_tip_opreme = this.currentParametri.IdTipOpreme,
                naziv_parametra = this.currentParametri.Name,
                vrednost_parametra = this.currentParametri.VrednostParametra,
                za_filter = this.currentParametri.ParametarJeIFilter,
                tipParametra = this.currentParametri.TipParametra
            };

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriUpdate(p);
            this.popuniListuParametara(nizParametara);

            ctmPrikazDetalja.Visibility = Visibility.Hidden;

        }

        private void btnObrisiParametar_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tblckVracenTextZaDugme = FindChild<TextBlock>(sender as Button);
            string pera = tblckVracenTextZaDugme.Text;
            if (pera == "Vrati iz arhive ovaj parametar")
            {

                string poruka = "Prodavci će videti ovaj parametar. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {

                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentParametri.IdParametri;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriRestore(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuParametara(nizParametara);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneParametri_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Ovime ćete ovaj parametar samo sakriti za prodavca. Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        ControlTemplate template = Sadrzaj.Template;
                        ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentParametri.IdParametri;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriDelete(id);
                            ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuParametara(nizParametara);
                            this.pravilnoRasporediListuKorisnika();
                            this.otkaziIzmeneParametri_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void dugmeMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else this.WindowState = System.Windows.WindowState.Normal;
        }

        private void dugmeZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dugmeMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void tbPretragaParametara_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] parametri = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                parametri = service.SelectAdminPanelParametri();
            }
            else
            {
                parametri = service.ParametriPretraga(zaPretragu);
            }
            this.popuniListuParametara(parametri);
            this.pravilnoRasporediListuKorisnika();
        }

        private void cmbListaUloga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            for (int i = 0; i < ListaUloga.Count; i++)
            {
                if (this.CurrentKorisnici != null)
                {
                    if (this.CurrentKorisnici.IzabranaUloga == i)
                    {
                        this.IzabraniNazivUloge = ListaUloga[i];
                    }
                }
            }
            if (dalijeUnosUToku)
            {
                if (this.CurrentKorisnici != null)
                {
                    
                        if (this.CurrentKorisnici.IzabranaUloga == 0)
                            this.CurrentKorisnici.SlikaKorisnika = App.PutanjaDoSlikeAdministratorKorisnici;
                        else if (this.CurrentKorisnici.IzabranaUloga == 1)
                            this.CurrentKorisnici.SlikaKorisnika = App.PutanjaDoSlikeProdavacKorisnici;
                }
            }
            //this.CurrentKorisnici.IdUloge = (sender as ComboBox).SelectedIndex + 1;
        }

        private void tbPretragaKorisnika_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                korisnici = service.PrikaziZaposleneKorisnike(null);
            }
            else
            {
                korisnici = service.PretragaKorisnika(zaPretragu);
            }
            this.popuniListuKorisnici(korisnici);
            this.pravilnoRasporediListuKorisnika();
        }

        private void tbPretragaOpreme_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                oprema = service.OpremaSaParametrimaAdminPanel();
            }
            else
            {
                oprema = service.PretragaOpreme(zaPretragu, true);
            }
            this.popuniListuOpremeSaParametrima(oprema);
            this.pravilnoRasporediListuKorisnika();
        }

        private void grdPrikazKupacaIstorijeKupovine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //oblasti = CollectionViewSource.GetDefaultView(this.ListaKupaca);
            //ku = oblasti.CurrentItem as Kupci;
            //this.CurrentKupci = ku;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovine = service.IstorijaKupovineSelect();
            this.popuniListuIstorijeKupovine(nizIstorijeKupovine);

            Grid grdListaKorisnici = (Grid)sender;
            //ControlTemplate ctp = cclListaKupacaIstorijeKupovine.Template as ControlTemplate;
            TextBlock tbclkSkrivenId = grdListaKorisnici.FindName("skrivenId") as TextBlock;
            if (this.ListaKupaca != null && this.ListaIstorijeKupovineZaListuKupaca != null)
            {
                int brojKupaca = this.ListaKupaca.Count;
                int brojIstorijeKupovine = this.ListaIstorijeKupovineZaListuKupaca.Count;
                if (brojKupaca > 0 && brojIstorijeKupovine > 0 && brojKupaca == brojIstorijeKupovine)
                {
                    for (int i = 0; i < brojKupaca; i++)
                    {
                        if (Convert.ToInt32(tbclkSkrivenId.Text) == ListaKupaca[i].IdKorisnici)
                        {
                            this.CurrentKupci = ListaKupaca[i];
                            brdPregledIstorijeKupovine.Visibility = Visibility.Visible;
                            break;
                        }
                    }
                }
            }



        }

        private void cmbListaUpitaZaKupce_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //this.popuniListuKupaca(service.IstorijaKupovineInicijalniPrikazKupaca());
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovine = service.IstorijaKupovineZaListuKupacaDatumSelect();
                    this.popuniListuIstorijeKupovineZaListuKupaca(nizIstorijeKupovine);

                    (grdIstorijaKupovine.FindName("cclListaKupacaIstorijeKupovine") as ContentControl).SetResourceReference(TemplateProperty, "listaKupacaDatum");
                    break;
                case 1:
                    SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovineIme = service.IstorijaKupovineZaListuKupacaImeSelect();
                    this.popuniListuIstorijeKupovineZaListuKupaca(nizIstorijeKupovineIme);

                    (grdIstorijaKupovine.FindName("cclListaKupacaIstorijeKupovine") as ContentControl).SetResourceReference(TemplateProperty, "listaKupacaIme");
                    break;
                case 2:
                    SmartSoftwareServiceReference.DbItemIstorijaKupovine[] nizIstorijeKupovineBrojKupovina = service.IstorijaKupovineZaListuKupacaBrojKupovinaSelect(false);
                    this.popuniListuIstorijeKupovineZaListuKupaca(nizIstorijeKupovineBrojKupovina);

                    (grdIstorijaKupovine.FindName("cclListaKupacaIstorijeKupovine") as ContentControl).SetResourceReference(TemplateProperty, "listaKupacaBrojKupovina");
                    break;
                case 3:
                    this.popuniListuKupaca(service.IstorijaKupovineKupciKojiNajviseTrose(false));
                    (grdIstorijaKupovine.FindName("cclListaKupacaIstorijeKupovine") as ContentControl).SetResourceReference(TemplateProperty, "kupciNajvisePotroseno");

                    break;
            }


        }



        private void element_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ContentControl ctl = grdIstorijaKupovine.FindName("listaOpremeGrupisanihPo") as ContentControl;
            ControlTemplate template = ctl.Template;
            ContentControl pera2 = template.FindName("pera2", ctl) as ContentControl;

            oblasti = CollectionViewSource.GetDefaultView(this.ListaOpreme);
            o = oblasti.CurrentItem as Oprema;
            this.CurrentOprema = o;
            pera2.Visibility = Visibility.Visible;


            //ContentControl ctl = grdIstorijaKupovine.FindName("prikazIstorijeKupovineGrupisanePoOpremi") as ContentControl;
            //ControlTemplate template = ctl.Template;
            //ContentControl pera2 = template.FindName("pera2", ctl) as ContentControl;

            ////pera2.Visibility = Visibility.Visible;
            //Grid g = sender as Grid;

            //TextBlock t = g.FindName("skrivenId") as TextBlock;





            //pera2.Content = null;



            //    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //    SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.OpremeSaParametrima((this.tmpEditObj as Oprema).IdTipOpreme);
            //    popuniListuOprema(rez);
            //    this.dalisepozivabaza = false;



            //pera2.Content = ;


        }



        private void btnOdbijNarudzbinu_Click(object sender, RoutedEventArgs e)
        {
            bool daLiJeNadjenaNarudzbina = false;
            int nadjenaNarudzbina = -1;
            TextBlock skrivenId = ((sender as Button).Parent as Grid).FindName("skrivenId") as TextBlock;
            int idNarudzbine = Convert.ToInt32(skrivenId.Text);

            string poruka = "Da li zaista želite da otkažete narudžbinu?";
            MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
            MessageBoxImage slikaBoxa = MessageBoxImage.Question;
            MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Otkazivanje narudžbine", dugmiciZaBrisanje, slikaBoxa);

            switch (rezultatBoxa)
            {
                case MessageBoxResult.Yes:
                    if (ListaNarudzbina != null && ListaNarudzbina.Count > 0)
                    {
                        for (int i = 0; i < ListaNarudzbina.Count; i++)
                        {
                            if (idNarudzbine == listaNarudzbina[i].IdNarudzbine)
                            {
                                nadjenaNarudzbina = i;
                                daLiJeNadjenaNarudzbina = true;
                                break;
                            }
                        }
                    }
                    if (daLiJeNadjenaNarudzbina && nadjenaNarudzbina >= 0)
                    {
                        DbItemNarudzbine narudzbinaOdbij = new DbItemNarudzbine()
                        {
                            id_narudzbine = ListaNarudzbina[nadjenaNarudzbina].IdNarudzbine,
                            datum_narudzbine = ListaNarudzbina[nadjenaNarudzbina].DatumNarudzbine,
                            id_oprema = ListaNarudzbina[nadjenaNarudzbina].Oprema.IdOprema,
                            id_prodavca = ListaNarudzbina[nadjenaNarudzbina].Prodavac.IdKorisnici,
                            kolicina = ListaNarudzbina[nadjenaNarudzbina].NarucenaKolicina
                        };

                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                        SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbineOdbijNarudzinu(narudzbinaOdbij);
                        this.popuniListuNarudzbina(nizNarudzbina);

                    }
                    break;
                case MessageBoxResult.No:
                    return;
            }


           
        }

        private void btnPrihvatiNarudzbinu_Click(object sender, RoutedEventArgs e)
        {
            bool daLiJeNadjenaNarudzbina = false;
            int nadjenaNarudzbina = -1;
            TextBlock skrivenId = ((sender as Button).Parent as Grid).FindName("skrivenId") as TextBlock;
            int idNarudzbine = Convert.ToInt32(skrivenId.Text);


            string poruka = "Da li zaista želite da prihvatite narudžbinu?";
            MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
            MessageBoxImage slikaBoxa = MessageBoxImage.Question;
            MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Prihvatanje narudžbine", dugmiciZaBrisanje, slikaBoxa);

            switch (rezultatBoxa)
            {
                case MessageBoxResult.Yes:
                    if (ListaNarudzbina != null && ListaNarudzbina.Count > 0)
                    {
                        for (int i = 0; i < ListaNarudzbina.Count; i++)
                        {
                            if (idNarudzbine == listaNarudzbina[i].IdNarudzbine)
                            {
                                nadjenaNarudzbina = i;
                                daLiJeNadjenaNarudzbina = true;
                                break;
                            }
                        }
                    }
                    if (daLiJeNadjenaNarudzbina && nadjenaNarudzbina >= 0)
                    {
                        DbItemNarudzbine narudzbinaPrihvati = new DbItemNarudzbine()
                        {
                            id_narudzbine = ListaNarudzbina[nadjenaNarudzbina].IdNarudzbine,
                            datum_narudzbine = ListaNarudzbina[nadjenaNarudzbina].DatumNarudzbine,
                            id_oprema = ListaNarudzbina[nadjenaNarudzbina].Oprema.IdOprema,
                            id_prodavca = ListaNarudzbina[nadjenaNarudzbina].Prodavac.IdKorisnici,
                            kolicina = ListaNarudzbina[nadjenaNarudzbina].NarucenaKolicina
                        };

                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

                        SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbinePrihvatiNarudzbinu(narudzbinaPrihvati);
                        this.popuniListuNarudzbina(nizNarudzbina);

                    }
                    break;
                case MessageBoxResult.No:
                    return;
            }



            
        }

        private void btnUpravljanjeOgranicenjima_Click(object sender, RoutedEventArgs e)
        {
            //foreach (var item in grdDugmici.Children)
            //{
            //    (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
            //}
            //btnUpravljanjeOgranicenjima.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

            //Sadrzaj.SetResourceReference(ContentControl.TemplateProperty, "UpravljanjeOgranicenjima");

            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            //SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOpreme = service.TipOpremeAdminPanel();
            //this.popuniListuTipovaOpreme(nizTipovaOpreme);





        }



        private void cmbListaTipovaOpremeOgranicenjaGlavno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;
            string nazivIzabranogTipaOpreme = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        nazivIzabranogTipaOpreme = ListaTipovaOpreme[i].Name;
                        break;
                    }
                }
                this.CurrentOgranicenjeKolekcije.Id_tip_opreme1 = izabranTipOpreme;
                this.CurrentOgranicenjeKolekcije.NazivTipaOpreme1 = nazivIzabranogTipaOpreme;

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(izabranTipOpreme);
                this.popuniListuParametara(nizParametara);
            }
        }

        private void cmbListaTipovaOpremeOgranicenjaPomocno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;
            string nazivIzabranogTipaOpreme = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        nazivIzabranogTipaOpreme = ListaTipovaOpreme[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_tip_opreme2 = izabranTipOpreme;
                this.CurrentOgranicenjeKolekcije.NazivTipaOpreme2 = nazivIzabranogTipaOpreme;


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriZaIdTipaOpreme(izabranTipOpreme);
                this.popuniListuParametaraPomocno(nizParametara);
            }
        }

        private void cmbListaParametaraOgranicenja1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranIdParametra = -1;
            string nazivIzabranogParametra = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaParametara.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranIdParametra = ListaParametara[i].IdParametri;
                        nazivIzabranogParametra = ListaParametara[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_parametra1 = izabranIdParametra;
                this.CurrentOgranicenjeKolekcije.NazivParametra1 = nazivIzabranogParametra;

            }
        }

        private void cmbListaParametaraOgranicenja2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranIdParametra = -1;
            string nazivIzabranogParametra = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaParametaraPomocno.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranIdParametra = ListaParametaraPomocno[i].IdParametri;
                        nazivIzabranogParametra = ListaParametaraPomocno[i].Name;
                        break;
                    }
                }

                this.CurrentOgranicenjeKolekcije.Id_parametra2 = izabranIdParametra;
                this.CurrentOgranicenjeKolekcije.NazivParametra2 = nazivIzabranogParametra;

            }
        }

        private void cmbListaTipovaOpremeKonfiguracije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            int izabranTipOpreme = -1;

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        izabranTipOpreme = ListaTipovaOpreme[i].IdTipOpreme;
                        break;
                    }
                }

                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                SmartSoftwareServiceReference.DbItemTipoviZaKonfiguraciju[] nizTipovaOpremeZaKonfiguraciju = service.TipoviZaNovuKonfiguraciju(izabranTipOpreme);

                this.popuniListuTipovaOpremeZaKonfiguracijuOgranicenja(nizTipovaOpremeZaKonfiguraciju);

                int selektovaniIndex = cmb.SelectedIndex;

                for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
                {
                    if (selektovaniIndex == i)
                    {
                        this.currentTipoviOpreme = new TipoviOpreme(null)
                        {
                            IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme

                        };
                        break;
                    }
                }
            }
        }


        int IzabranIdTipaOpremeZaKonfiguraciju = 0;
        private void btnIdiNaDruguStranu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ContentControl ctlDugmiciUnosOgranicenja = ctmUnosOgranicenjaTemplate.FindName("ctmDugmiciUnosOgranicenja", ctmUnosOgranicenja) as ContentControl;



            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();



            DbItemTipoviZaKonfiguraciju[] kolekcijaKonfiguracije = new DbItemTipoviZaKonfiguraciju[this.ListaTipovaZaKonfiguraciju.Count];
            for (int i = 0; i < this.ListaTipovaZaKonfiguraciju.Count; i++)
            {
                kolekcijaKonfiguracije[i] = new DbItemTipoviZaKonfiguraciju()
                {
                    naziv_tipa = ListaTipovaZaKonfiguraciju[i].Name,
                    id_tip_opreme = ListaTipovaZaKonfiguraciju[i].IdTipOpreme,
                    idTipOpremeKolekcije = ListaTipovaZaKonfiguraciju[i].IdTipOpremeKolekcije,
                    idTipOpremeDeoKolekcije = ListaTipovaZaKonfiguraciju[i].IdTipOpreme,
                    redosledPrikazivanja = ListaTipovaZaKonfiguraciju[i].IzabranRedosled + 1,
                    izabranZaKonfiguraciju = ListaTipovaZaKonfiguraciju[i].IzabranZaKonfiguraciju
                };
            }



            IzabranIdTipaOpremeZaKonfiguraciju = this.CurrentTipoviOpreme.IdTipOpreme;


            SmartSoftwareServiceReference.DbItemTipoviZaKonfiguraciju[] nizTipovaZaKonfiguraciju = service.TipoviZaNovuKonfiguracijuInsertUpdate(this.CurrentTipoviOpreme.IdTipOpreme, kolekcijaKonfiguracije);

            this.popuniListuTipovaOpreme(nizTipovaZaKonfiguraciju);

            ctlUnosOgranicenjaSredina.SetResourceReference(TemplateProperty, "ctpUnosOgranicenjaDrugaStrana");
            ctlDugmiciUnosOgranicenja.SetResourceReference(TemplateProperty, "ctpDugmiciUnosOgranicenjaDrugaStrana");


            DbItemGrupeOgranicenja[] nizOgranicenja = service.OgranicenjaSelect(IzabranIdTipaOpremeZaKonfiguraciju);
            this.popuniListuOgranicenja(nizOgranicenja);

        }

        private void btnUnesiOgranicenja_Click(object sender, RoutedEventArgs e)
        {
            int idTipOpremeKolekcije = this.CurrentTipoviOpreme.IdTipOpreme;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            DbItemGrupeOgranicenja ogranicenje = new DbItemGrupeOgranicenja()
            {
                id_tip_opreme_kolekcije = this.CurrentTipoviOpreme.IdTipOpreme,
                Id_grupe_ogranicenja = this.CurrentOgranicenjeKolekcije.Id_grupe_ogranicenja,
                id_parametra1 = this.CurrentOgranicenjeKolekcije.Id_parametra1,
                id_parametra2 = this.CurrentOgranicenjeKolekcije.Id_parametra2,
                id_tip_opreme1 = this.CurrentOgranicenjeKolekcije.Id_tip_opreme1,
                id_tip_opreme2 = this.CurrentOgranicenjeKolekcije.Id_tip_opreme2,
                tipProvere = this.CurrentOgranicenjeKolekcije.TipProvere
            };
            this.CurrentOgranicenjeKolekcije = new GrupaOgranicenja();
            this.CurrentTipoviOpreme = new TipoviOpreme(null);
            DbItemGrupeOgranicenja[] nizOgranicenja = service.OgranicenjaInsert(ogranicenje);
            this.popuniListuOgranicenja(nizOgranicenja);
        }

        private void btnVratiSeNaPrethodnuStranu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;
            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ContentControl ctlDugmiciUnosOgranicenja = ctmUnosOgranicenjaTemplate.FindName("ctmDugmiciUnosOgranicenja", ctmUnosOgranicenja) as ContentControl;






            ctlUnosOgranicenjaSredina.SetResourceReference(TemplateProperty, "ctpUnosOgranicenjaPrvaStrana");
            ctlDugmiciUnosOgranicenja.SetResourceReference(TemplateProperty, "ctpDugmiciUnosOgranicenjaPrvaStrana");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOpreme = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipovaOpreme);

            for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            {
                if (ListaTipovaOpreme[i].IdTipOpreme == IzabranIdTipaOpremeZaKonfiguraciju)
                {
                    this.CurrentTipoviOpreme.IzabranTipOpreme = i;
                    break;
                }
            }
        }

        private void btnUcitajJosJednuGrupu_Click(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;

            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ControlTemplate ctlUnosOgranicenjaSredinaTemplate = ctlUnosOgranicenjaSredina.Template;

            ContentControl ctlGrupaOgranicenja = ctlUnosOgranicenjaSredinaTemplate.FindName("ctlGrupaOgranicenja", ctlUnosOgranicenjaSredina) as ContentControl;
            ctlGrupaOgranicenja.Visibility = Visibility.Visible;

            Button jos = sender as Button;
            jos.Visibility = Visibility.Hidden;



        }



        private void btnUnesiOgranicenja_Click_1(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = Sadrzaj.Template;

            ContentControl ctmUnosOgranicenja = template.FindName("ctmUnosOgranicenja", Sadrzaj) as ContentControl;
            ControlTemplate ctmUnosOgranicenjaTemplate = ctmUnosOgranicenja.Template;

            ContentControl ctlUnosOgranicenjaSredina = ctmUnosOgranicenjaTemplate.FindName("ctlUnosOgranicenjaSredina", ctmUnosOgranicenja) as ContentControl;
            ControlTemplate ctlUnosOgranicenjaSredinaTemplate = ctlUnosOgranicenjaSredina.Template;

            ContentControl ctlGrupaOgranicenja = ctlUnosOgranicenjaSredinaTemplate.FindName("ctlGrupaOgranicenja", ctlUnosOgranicenjaSredina) as ContentControl;


        }

        private void cmbListaTipaProvere_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            int izabranIndex = cmb.SelectedIndex;
            string nazivIzabranogTipaProvere = "";

            if (izabranIndex >= 0)
            {
                for (int i = 0; i < ListaTipovaProvere.Count; i++)
                {
                    if (izabranIndex == i)
                    {
                        nazivIzabranogTipaProvere = ListaTipovaProvere[i];
                        break;
                    }
                }


                this.CurrentOgranicenjeKolekcije.TipProvere = nazivIzabranogTipaProvere;
            }
        }

        DispatcherTimer timerProveraVisineListeNarudzbina = new DispatcherTimer();

        private void btnUcitajJosNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            odakleKrece = dokleIde;
            dokleIde += 5;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemNarudzbine[] nizNarudzbina = service.OpNarudzbineSelect();
            this.popuniListuNarudzbinaOpet(nizNarudzbina, odakleKrece, dokleIde);

            if (timerProveraVisineListeNarudzbina == null)
                timerProveraVisineListeNarudzbina = new DispatcherTimer();
            if (!timerProveraVisineListeNarudzbina.IsEnabled)
            {
                timerProveraVisineListeNarudzbina.Interval = trajanje200;
                timerProveraVisineListeNarudzbina.Tick += timerProveraVisineListeNarudzbina_Tick;
                timerProveraVisineListeNarudzbina.Start();
            }


        }

        void timerProveraVisineListeNarudzbina_Tick(object sender, EventArgs e)
        {
            if (timerProveraVisineListeNarudzbina.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();

                ControlTemplate ctp = cclUpravljanjeNarudzbinama.Template as ControlTemplate;
                ScrollViewer scrolVjuverRezervacije = ctp.FindName("scrolVjuverRezervacije", cclUpravljanjeNarudzbinama) as ScrollViewer;
                Border brdHederNarudzbine = ctp.FindName("brdHederNarudzbine", cclUpravljanjeNarudzbinama) as Border;



                if (scrolVjuverRezervacije.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Collapsed)
                    brdHederNarudzbine.Margin = new Thickness(0);
                else if (scrolVjuverRezervacije.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible)
                    brdHederNarudzbine.Margin = new Thickness(-20, 0, 0, 0);

                this.pravilnoRasporediListuNarudzbina();
            }

            if (timerProveraVisineListeNarudzbina != null)
                timerProveraVisineListeNarudzbina.Stop();
            timerProveraVisineListeNarudzbina = null;

        }

        OblastiOpreme staraOblastOpreme = null;
        TipoviOpreme stariTipOpreme = null;
        Oprema staraOprema = null;
        Parametri stariParametar = null;
        Korisnici stariKorisnik = null;
        Grid TrenutniGridTriVjuOblastiOpreme = null;
        Grid TrenutniGridTriVjuTipoviOpreme = null;
        Grid TrenutniGridTriVjuOprema = null;
        Grid TrenutniGridTriVjuParametri = null;
        Grid TrenutniGridTriVjuKorisnici = null;


        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        int idOblastiOpreme = 0;
        private void grdListaOblastiOpreme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Grid grdListaOblastiOpreme = (Grid)sender;
            TextBlock tbIdOblastiOpreme = grdListaOblastiOpreme.FindName("skrivenId") as TextBlock;
            idOblastiOpreme = Convert.ToInt32(tbIdOblastiOpreme.Text);
            int idIzTrenutnogGrida = 0;
            ControlTemplate ctp = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;
            Grid grd = ctp.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            ContentControl cclPrikazDetaljaDugmiciOblastiOpreme = ctp.FindName("cclPrikazDetaljaDugmiciOblastiOpreme", cclPrikazDetaljaOblastiOpreme) as ContentControl;
            if (cclPrikazDetaljaDugmiciOblastiOpreme != null)
                cclPrikazDetaljaDugmiciOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciOblastiOpremeAzuriranje");
            stariScrollbarVisibility = Visibility.Hidden;
            Border brdPrikazDetaljaKorisniciSadrzaj = ctp.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaOblastiOpreme) as Border;
            ContentControl cclPrikazHederaOblastiOpreme = ctp.FindName("cclPrikazHederaOblastiOpreme", cclPrikazDetaljaOblastiOpreme) as ContentControl;
            ControlTemplate ctp2 = cclPrikazHederaOblastiOpreme.Template as ControlTemplate;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = null;
            if (ctp2 != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp2.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclPrikazHederaOblastiOpreme) as Grid;




            if (this.TrenutniGridTriVjuOblastiOpreme != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock;
                idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                if (idIzTrenutnogGrida != idKorisnici && idIzTrenutnogGrida != 0)
                {
                    //Border brdNazivIUlogaKorisnika = TrenutniGridTriVjuOblastiOpreme.FindName("brdNazivIUlogaKorisnika") as Border;
                    Border brdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                    TextBlock tbNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                    tbNazivOblastiOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                    TrenutniGridTriVjuOblastiOpreme = grdListaOblastiOpreme;
                }
            }
            else
            {
                TrenutniGridTriVjuOblastiOpreme = grdListaOblastiOpreme;
            }


            if (this.CurrentOblastiOpreme != null)
            {
                staraOblastOpreme = CurrentOblastiOpreme;
            }
            for (int i = 0; i < ListaOblastiOpreme.Count; i++)
            {
                if (idOblastiOpreme == ListaOblastiOpreme[i].IdOblastiOpreme)
                {
                    this.CurrentOblastiOpreme = ListaOblastiOpreme[i];
                    break;
                }
            }
            if (this.CurrentOblastiOpreme != null)
            {

                if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                {
                    if (this.CurrentOblastiOpreme.DeletedItem)
                    {


                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    }
                }




                if (staraOblastOpreme != null)
                {
                    if (staraOblastOpreme != this.CurrentOblastiOpreme)
                    {
                        DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        if (dalijeUnosUToku)
                            brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                        brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        dalijeUnosUToku = false;
                    }
                }


            }

            if (brdPrazanPrikazDetaljaOblastiOpreme.Opacity > 0)
            {
                brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 0);

                brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);
                brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = false;

            }


            brdPrikazDetaljaOblastiOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaKorisnici.Title = "PRIKAZ DETALJA";
            Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);
            staraOblastOpreme = CurrentOblastiOpreme;








            if (daLiSePrekinuoTajmerKorisnici)
            {
                daLiSePrekinuoTajmerKorisnici = false;
                if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                }
                if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                    timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                }
            }























            //Grid grdListaOblastiOpreme = (Grid)sender;
            //int id = Convert.ToInt32((grdListaOblastiOpreme.FindName("skrivenId") as TextBlock).Text);
            //int idIzTrenutnogGrida = 0;


            //if (this.CurrentOblastiOpreme != null)
            //{
            //    staraOblastOpreme = CurrentOblastiOpreme;
            //}

            //for (int i = 0; i < ListaZaSve.Count; i++)
            //{
            //    if (id == ListaZaSve[i].IdOblastiOpreme)
            //    {
            //        this.CurrentOblastiOpreme = ListaZaSve[i];
            //        break;
            //    }
            //}

            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //    Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //    Trenutnitblck.Foreground = Brushes.Black;
            //}
            //trenutniGridKliknutoNaRBKorisnici = null;


            //if (this.CurrentOblastiOpreme != null)
            //{
            //    if (staraOblastOpreme != null)
            //    {


            //        if (brdPrazanPrikazDetaljaOblastiOpreme.Opacity == 1)
            //        {
            //            brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //            brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //            Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 0);
            //        }
            //        //if (brdUnosOblastiOpreme.Opacity == 1)
            //        //{
            //        //    //brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //        //    //brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //        //    Panel.SetZIndex(brdUnosOblastiOpreme, 0);
            //        //}

            //        if (staraOblastOpreme != CurrentOblastiOpreme)
            //        {
            //            DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
            //            //brdPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
            //            Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);
            //            brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = false;
            //            //brdUnosOblastiOpreme.IsEnabled = false;
            //            brdPrikazDetaljaOblastiOpreme.IsEnabled = true;

            //        }


            //        //brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //        //brdPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            //        //Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);

            //    }
            //    else
            //    {
            //        if (brdPrazanPrikazDetaljaOblastiOpreme.Opacity == 1)
            //        {
            //            brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //            brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

            //            Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 0);
            //        }
            //        //if (brdUnosOblastiOpreme.Opacity == 1)
            //        //{
            //        //    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //        //    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //        //    Panel.SetZIndex(brdUnosOblastiOpreme, 0);
            //        //}


            //        brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //        //brdPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            //        Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);

            //        brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = false;
            //        //brdUnosOblastiOpreme.IsEnabled = false;
            //        brdPrikazDetaljaOblastiOpreme.IsEnabled = true;

            //    }
            //    if (this.TrenutniGridTriVjuOblastiOpreme != null)
            //    {
            //        idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock).Text);

            //        if (idIzTrenutnogGrida != id && idIzTrenutnogGrida != 0)
            //        {
            //            Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
            //            //TextBox trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as TextBox;
            //            trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
            //            TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as TextBlock;
            //            trenutniTxtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            //            TrenutniGridTriVjuOblastiOpreme = grdListaOblastiOpreme;
            //        }
            //    }
            //    else
            //    {
            //        TrenutniGridTriVjuOblastiOpreme = grdListaOblastiOpreme;
            //    }
            //}
            ////else
            ////{

            ////    brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            ////    brdPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

            ////    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            ////    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);


            ////    brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            ////    brdPrazanPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            ////    Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            ////    Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 1);

            ////}
            //staraOblastOpreme = CurrentOblastiOpreme;
            //LejautDokumentTitlePrikazDetaljaOblastiOpreme.Title = "PRIKAZ DETALJA";
            //brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
        }

        void timerOblastiOpremeRasporediPrikazDetaljaPravilno_Tick(object sender, EventArgs e)
        {
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

            Grid grdPrikazDetaljaOblastiOpreme = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaOblastiOpreme.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;

            if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
            {
                DispatcherTimer timerSender = sender as DispatcherTimer;
                timerSender.Stop();

                ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaOblastiOpreme);
                Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;
                //
                ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;

                Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclPrikazHederaOblastiOpreme) as Grid;
                Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;
                if (brdNaslovDetalji != null)
                {
                    if (scrollbarVisibility == Visibility.Visible)
                    {
                        //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                        //    brdDugmiciDole.Width = 480;
                        //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                        //
                        if (brdNaslovDetalji.Margin != thicknessProba)
                            brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                    }
                    else if (scrollbarVisibility == Visibility.Collapsed)
                    {
                        //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                        //    brdDugmiciDole.Width = 460;
                        //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                        if (brdNaslovDetalji.Margin != marginaCentar)
                            brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                        //brdImeIUlogaKorisnikaDetalji.Width = 460;

                    }
                    stariScrollbarVisibility = scrollbarVisibility;
                }
                int id = 0;
                Label lblSkrivenId = ctp.FindName("skrivenId", cclPrikazHederaOblastiOpreme) as Label;
                if (lblSkrivenId != null)
                    id = Convert.ToInt32(lblSkrivenId.Content);

                TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclPrikazHederaOblastiOpreme) as TextBlock;
                TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclPrikazHederaOblastiOpreme) as TextBlock;

                if (tblckNaslov != null && id != 0 && id == 2)
                {
                    if (tblckNaslov.ActualHeight > 30)
                    {
                        if (this.CurrentOblastiOpreme != null)
                            this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = true;
                        if (tblckNaslovDetaljnijeHeder != null)
                        {
                            tblckNaslovDetaljnijeHeder.Width = 15;
                            tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                        }
                    }

                    else
                    {
                        if (this.CurrentOblastiOpreme != null)
                            this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;
                        if (tblckNaslovDetaljnijeHeder != null)
                        {
                            tblckNaslovDetaljnijeHeder.Width = 0;
                            tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                        }
                    }


                }




                if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                {
                    if (slikaStrelicaHederNaziv != null)
                    {
                        slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                    }
                }
                if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                {
                    if (slikaStrelicaHederNaziv != null)
                    {
                        slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                    }
                }

                if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                {
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                    {

                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                    }
                    if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                    {
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                    }
                }




            }

            daLiSePrekinuoTajmerKorisnici = true;
            if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

            timerKorisniciRasporediPrikazDetaljaPravilno = null;
        }

        private void grdListaOblastiOpreme_MouseEnter(object sender, MouseEventArgs e)
        {

            Grid grdListaOblastiOpreme = (Grid)sender;
            TrenutnaVrednostSirine = grdListaOblastiOpreme.ActualWidth - 35;
            idOblastiOpreme = Convert.ToInt32((grdListaOblastiOpreme.FindName("skrivenId") as TextBlock).Text);


            Border brdTriVjuHierarchicalNaziv = grdListaOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
            //Border brdUnosKorisnici = grdListaOblastiOpreme.FindName("brdUnosKorisnici") as Border;

            TextBlock tbNazivOblastiOpreme = grdListaOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
            DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(opasiti0, TrenutnaVrednostSirine, trajanje200);

            if (this.TrenutniGridTriVjuOblastiOpreme != null)
            {


                int idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock).Text);

                if (idIzTrenutnogGrida != idOblastiOpreme && idIzTrenutnogGrida != 0)
                {
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
                    brdTriVjuHierarchicalNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);

                    //DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(0, grdListaKorisnici.ActualWidth, trajanje200);
                    brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                    tbNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else
            {
                brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
                brdTriVjuHierarchicalNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                tbNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }

            daBrdTriVjuHierarchicalImeIPrezimeSirina = null;














            //Grid grdHierarchical = (Grid)sender;
            //int id = Convert.ToInt32((grdHierarchical.FindName("skrivenId") as TextBlock).Text);
            //int idIzTrenutnogGrida = 0;

            //Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;


            //TextBlock txtBoxNazivOblastiOpreme = grdHierarchical.FindName("txtBoxNazivOblastiOpreme") as TextBlock;
            ////int idOblastiOpreme = Convert.ToInt32((grdHierarchical.FindName("skrivenId") as TextBlock).Text);

            //if (this.TrenutniGridTriVjuOblastiOpreme != null)
            //{
            //    idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock).Text);

            //    if (idIzTrenutnogGrida != id && idIzTrenutnogGrida != 0)
            //    {

            //        brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
            //        brdTriVjuHierarchicalNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
            //        brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalNazivSirina);
            //        txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //    }
            //}
            //else
            //{
            //    brdTriVjuHierarchicalNaziv.Background = Brushes.Gainsboro;
            //    brdTriVjuHierarchicalNaziv.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
            //    brdTriVjuHierarchicalNaziv.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalNazivSirina);
            //    txtBoxNazivOblastiOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}


        }


        private void grdListaOblastiOpreme_MouseLeave(object sender, MouseEventArgs e)
        {


            var treeViewItemOblastiOpreme = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItemOblastiOpreme != null)
            {
                Grid grdListaOblastiOpreme = (Grid)sender;
                TextBlock tbIdOblastiOpreme = grdListaOblastiOpreme.FindName("skrivenId") as TextBlock;
                idOblastiOpreme = Convert.ToInt32(tbIdOblastiOpreme.Text);

                Border brdTriVjuHierarchicalNaziv = grdListaOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;

                TextBlock tbNazivOblastiOpreme = grdListaOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
                int idIzTrenutnogGrida = 0;


                //Border brdHierarchical = grdHierarchicalTipoviOpreme.FindName("brdHierarchical") as Border;
                //Border brdTriVjuHierarchicalNaziv = grdHierarchicalTipoviOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                //Border brdAktivno = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;
                //Border brdProbaBorderi = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;
                //Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;

                if (this.TrenutniGridTriVjuOblastiOpreme != null)
                {
                    idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock).Text);

                    if (idIzTrenutnogGrida != idOblastiOpreme && idIzTrenutnogGrida != 0)
                    {
                        brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;

                        tbNazivOblastiOpreme.Foreground = tbIdOblastiOpreme.Foreground;
                    }
                }
                else
                {
                    brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                    tbNazivOblastiOpreme.Foreground = tbIdOblastiOpreme.Foreground;
                }

            }


            //var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            //if (treeViewItem != null)
            //{
            //    Grid grdHierarchical = (Grid)sender;
            //    int id = Convert.ToInt32((grdHierarchical.FindName("skrivenId") as TextBlock).Text);
            //    int idIzTrenutnogGrida = 0;


            //    Border brdHierarchical = grdHierarchical.FindName("brdHierarchical") as Border;
            //    Border brdTriVjuHierarchicalNaziv = grdHierarchical.FindName("brdTriVjuHierarchicalNaziv") as Border;

            //    TextBlock txtBoxNazivOblastiOpreme = grdHierarchical.FindName("txtBoxNazivOblastiOpreme") as TextBlock;

            //    if (this.TrenutniGridTriVjuOblastiOpreme != null)
            //    {
            //        idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOblastiOpreme.FindName("skrivenId") as TextBlock).Text);

            //        if (idIzTrenutnogGrida != id && idIzTrenutnogGrida != 0)
            //        {
            //            brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
            //            txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            //        }
            //    }
            //    else
            //    {
            //        brdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
            //        txtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            //    }

            //}


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

        DispatcherTimer timerPrikaziSakrijPretragu;



        private void imgOblastOpremeSearch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (timerPrikaziSakrijPretragu != null)
            {
                if (timerPrikaziSakrijPretragu.IsEnabled)
                    timerPrikaziSakrijPretragu.Stop();
                timerPrikaziSakrijPretragu = new DispatcherTimer();
                timerPrikaziSakrijPretragu.Interval = new TimeSpan(0, 0, 0, 0, 100);
                timerPrikaziSakrijPretragu.Tick += timerPrikaziSakrijPretragu_Tick;
                timerPrikaziSakrijPretragu.Start();
            }
            else
            {
                timerPrikaziSakrijPretragu = new DispatcherTimer();
                timerPrikaziSakrijPretragu.Interval = new TimeSpan(0, 0, 0, 0, 100);
                timerPrikaziSakrijPretragu.Tick += timerPrikaziSakrijPretragu_Tick;
                timerPrikaziSakrijPretragu.Start();
            }


        }

        void timerPrikaziSakrijPretragu_Tick(object sender, EventArgs e)
        {

            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaOblastiOpreme = brdPretragaIUnos.FindName("tbPretragaOblastiOpreme") as TextBox;
                        if (brdPretragaIUnos.Height == 45)
                        {
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaOblastiOpreme.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaOblastiOpreme.IsReadOnly = false;
                        }
                        brdPretragaIUnos.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }


                    break;
                case 2:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaTipoviOpreme = brdPretragaIUnosTipoviOpreme.FindName("tbPretragaTipoviOpreme") as TextBox;
                        if (brdPretragaIUnosTipoviOpreme.Height == 45)
                        {
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaTipoviOpreme.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaTipoviOpreme.IsReadOnly = false;
                        }
                        brdPretragaIUnosTipoviOpreme.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
                    break;
                case 3:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaOprema = brdPretragaIUnosTipoviOpreme.FindName("tbPretragaOprema") as TextBox;
                        if (brdPretragaIUnosOpreme.Height == 45)
                        { 
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaOprema.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaOprema.IsReadOnly = false;
                        }
                        brdPretragaIUnosOpreme.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
                    break;
                case 4:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaParametri = brdPretragaIUnosTipoviOpreme.FindName("tbPretragaParametri") as TextBox;
                        if (brdPretragaIUnosParametri.Height == 45)
                        { 
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaParametri.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaParametri.IsReadOnly = false;
                        }
                        brdPretragaIUnosParametri.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
                    break;
                case 5:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaKorisnici = brdPretragaIUnosTipoviOpreme.FindName("tbPretragaKorisnici") as TextBox;
                        if (brdPretragaIUnosKorisnici.Height == 45)
                        { 
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaKorisnici.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaKorisnici.IsReadOnly = false;
                        }
                        brdPretragaIUnosKorisnici.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
                    break;
                case 6:
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        TextBox tbPretragaKupci = brdPretragaIUnosTipoviOpreme.FindName("tbPretragaKupci") as TextBox;
                        if (brdPretragaIUnosKupci.Height == 45) 
                        { 
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                            tbPretragaKupci.IsReadOnly = true;
                        }
                        else
                        {
                            tbPretragaKupci.IsReadOnly = false;
                        }
                        brdPretragaIUnosKupci.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
                    break;
                case 7:
                    break;
            }


            timerPrikaziSakrijPretragu.Stop();
        }

        private void imgOblastOpremeDodaj_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    novaOblastOpreme_Click();
                    break;
                case 2:
                    noviTipOpreme_Click();
                    break;
                case 3:
                    novaOprema_Click();
                    break;
                case 4:
                    noviParametar_Click();
                    break;
                case 5:
                    noviKorisnik_Click();
                    break;
                case 6:
                    break;
                case 7:
                    break;

            }


            //this.CurrentOblastiOpreme = null;
            //this.staraOblastOpreme = null;

            //if (this.TrenutniGridTriVjuOblastiOpreme != null)
            //{
            //    Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
            //    Label trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as Label;
            //    trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
            //    trenutniTxtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            //    this.TrenutniGridTriVjuOblastiOpreme = null;
            //}


            //if (brdPrazanPrikazDetaljaOpreme.Opacity == 1)
            //{
            //    brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrazanPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 0);
            //}
            //if (brdPrikazDetaljaOblastiOpreme.Opacity == 1)
            //{
            //    brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            //}

            //brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            //Panel.SetZIndex(brdUnosOblastiOpreme, 1);
        }

        private void otkaziIzmeneOblastiOpreme_Click()
        {

            this.CurrentOblastiOpreme = null;
            this.staraOblastOpreme = null;
            this.NovaOblastOpreme = null;

            if (this.TrenutniGridTriVjuOblastiOpreme != null)
            {
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                TextBlock tbNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                tbNazivOblastiOpreme.Foreground = Brushes.Black;
                this.TrenutniGridTriVjuOblastiOpreme = null;
            }

            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                Trenutnitblck.Foreground = Brushes.Black;
            }
            trenutniGridKliknutoNaRBKorisnici = null;


            //if (brdUnosOblastiOpreme.Opacity == 1)
            //{
            //    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosOblastiOpreme, 0);
            //}
            if (brdPrikazDetaljaOblastiOpreme.Opacity == 1)
            {
                brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                //brdPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            }



            brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 1);
            brdPrikazDetaljaOblastiOpreme.IsEnabled = false;
            //brdUnosOblastiOpreme.IsEnabled = false;
            brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaOblastiOpreme.Title = "PRIKAZ DETALJA";
        }

        private void otkaziIzmeneTipoviOpreme_Click()
        {
            this.CurrentTipoviOpreme = null;
            this.stariTipOpreme = null;
            this.NoviTipOpreme = null;
            this.IzabranaOblastOpreme = 0;
            this.IzabranaOblastOpremeZaUnos = 0;
            if (this.TrenutniGridTriVjuTipoviOpreme != null)
            {
                TextBlock trenutniId = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                trenutniTxtBoxNazivOblastiOpreme.Foreground = trenutniId.Foreground;
                this.TrenutniGridTriVjuTipoviOpreme = null;
            }

            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                Trenutnitblck.Foreground = Brushes.Black;
            }
            trenutniGridKliknutoNaRBKorisnici = null;

            //if (brdUnosTipaOpreme.Opacity == 1)
            //{
            //    brdUnosTipaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosTipaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosTipaOpreme, 0);
            //}
            if (brdPrikazDetaljaTipoviOpreme.Opacity == 1)
            {
                brdPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 0);
            }



            brdPrazanPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaTipoviOpreme, 1);

            brdPrikazDetaljaTipoviOpreme.IsEnabled = false;
            //brdUnosTipaOpreme.IsEnabled = false;
            brdPrazanPrikazDetaljaTipoviOpreme.IsEnabled = true;

            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "PRIKAZ DETALJA";
        }

        private void otkaziIzmeneOpreme_Click()
        {
            IdOpremaZaSakrivanjePrikazaDetaljaOpreme = 0;
            dalijeUnosUToku = false;
            this.CurrentOprema = null;
            this.staraOprema = null;
            if (this.TrenutniGridTriVjuOprema != null)
            {
                TextBlock trenutniId = TrenutniGridTriVjuOprema.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOprema.FindName("brdNazivITipOpremeOpreme") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOprema.FindName("tbNaslovOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                trenutniTxtBoxNazivOblastiOpreme.Foreground = trenutniId.Foreground;
                this.TrenutniGridTriVjuOprema = null;
            }


            if (brdPrikazDetaljaOpreme.Opacity > 0)
            {
                brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaOpreme, 0);
            }



            brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 1);

            brdPrikazDetaljaOpreme.IsEnabled = false;
            //brdUnosTipaOpreme.IsEnabled = false;
            brdPrazanPrikazDetaljaOpreme.IsEnabled = true;

            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "PRIKAZ DETALJA";


        }


        private void otkaziIzmeneParametri_Click()
        {
            this.CurrentParametri = null;
            this.stariParametar = null;

            if (this.TrenutniGridTriVjuParametri != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuParametri.FindName("skrivenId") as TextBlock;
                int idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);


                Border brdNazivITipParametra = TrenutniGridTriVjuParametri.FindName("brdNazivITipParametra") as Border;
                Border brdTriVjuHierarchicalNazivParametriIFilteri = TrenutniGridTriVjuParametri.FindName("brdTriVjuHierarchicalNazivParametriIFilteri") as Border;
                TextBlock txtBoxNazivParametra = TrenutniGridTriVjuParametri.FindName("txtBoxNazivParametra") as TextBlock;
                brdNazivITipParametra.Background = Brushes.Transparent;
                txtBoxNazivParametra.Foreground = tbTrenutniGridTrivjuID.Foreground;
                TrenutniGridTriVjuParametri = null;
            }


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //    Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //    Trenutnitblck.Foreground = Brushes.Black;
            //}
            //trenutniGridKliknutoNaRBKorisnici = null;

            //if (brdUnosTipaOpreme.Opacity == 1)
            //{
            //    brdUnosTipaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosTipaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosTipaOpreme, 0);
            //}
            if (brdPrikazDetaljaParametriIFilteri.Opacity == 1)
            {
                brdPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaParametriIFilteri, 0);
            }



            brdPrazanPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaParametriIFilteri, 1);

            brdPrikazDetaljaParametriIFilteri.IsEnabled = false;
            //brdUnosTipaOpreme.IsEnabled = false;
            brdPrazanPrikazDetaljaParametriIFilteri.IsEnabled = true;

            LejautDokumentTitlePrikazDetaljaParametriIFilteri.Title = "PRIKAZ DETALJA";
        }

        private void otkaziIzmeneKorisnici_Click()
        {

            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            Border brdPrikazDetaljaKorisniciSadrzaj = grdPrikazDetaljaKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj") as Border;

            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            this.CurrentKorisnici = null;
            this.stariKorisnik = null;
            this.NoviKorisnik = null;
            ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclPrikazHederaImePrezimeKorisnici) as Grid;

            if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
            if (this.TrenutniGridTriVjuKorisnici != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuKorisnici.FindName("skrivenId") as TextBlock;
                Border brdNazivIUlogaKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdNazivIUlogaKorisnika") as Border;
                Border brdTriVjuHierarchicalImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdTriVjuHierarchicalImeIPrezimeKorisnika") as Border;
                TextBlock tbImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                brdNazivIUlogaKorisnika.Background = Brushes.Transparent;
                tbImeIPrezimeKorisnika.Foreground = tbTrenutniGridTrivjuID.Foreground;
                TrenutniGridTriVjuKorisnici = null;
            }

            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                Trenutnitblck.Foreground = Brushes.Black;
            }
            trenutniGridKliknutoNaRBKorisnici = null;


            //if (brdUnosKorisnici.Opacity == 1)
            //{
            //    brdUnosKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdUnosKorisnici, 0);
            //}

            if (brdPrikazDetaljaKorisnici.Opacity == 1)
            {
                brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                //if (brdPrikazDetaljaKorisniciSadrzaj != null)
                //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrikazDetaljaKorisnici, 0);
            }



            brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 1);
            brdPrikazDetaljaKorisnici.IsEnabled = false;
            //brdUnosKorisnici.IsEnabled = false;
            brdPrazanPrikazDetaljaKorisnici.IsEnabled = true;

            LejautDokumentTitlePrikazDetaljaKorisnici.Title = "PRIKAZ DETALJA";
        }

        private void btnOtkaziIzmene_Click(object sender, RoutedEventArgs e)
        {

            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    otkaziIzmeneOblastiOpreme_Click();
                    break;
                case 2:
                    otkaziIzmeneTipoviOpreme_Click();
                    break;
                case 3:
                    otkaziIzmeneOpreme_Click();
                    break;
                case 4:
                    otkaziIzmeneParametri_Click();
                    break;
                case 5:
                    otkaziIzmeneKorisnici_Click();
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }





            //brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);

            //brdPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //brdPrazanPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            //Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            //Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 1);
        }
        int brojac = 0;
        DispatcherTimer timerOsveziListuOblastiOpreme;
        private void imgOblastOpremeOsveziListu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    if (!kliknutoNaDugmiceUpravljanja)
                    {
                        kliknutoNaDugmiceUpravljanja = true;
                        kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                        kojeDugmeJePoReduKliknuto = 1;

                        foreach (var item in grdDugmici.Children)
                        {
                            (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                        }
                        btnOblastiOpreme.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");


                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
                        this.popuniListuOblasti(nizOblasti);

                        Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);

                    }
                    break;
                case 2:
                    if (!kliknutoNaDugmiceUpravljanja)
                    {
                        kliknutoNaDugmiceUpravljanja = true;
                        kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                        kojeDugmeJePoReduKliknuto = 2;


                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
                        this.popuniListuTipovaOpreme(nizTipovaOblasti);

                        Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);

                        this.pravilnoRasporediListuKorisnika();
                    }
                    break;
                case 3:
                    if (!kliknutoNaDugmiceUpravljanja)
                    {
                        kliknutoNaDugmiceUpravljanja = true;
                        kojeDugmeJePoReduKliknutoRanije = kojeDugmeJePoReduKliknuto;
                        kojeDugmeJePoReduKliknuto = 3;



                        foreach (var item in grdDugmici.Children)
                        {
                            (item as Button).SetResourceReference(Button.StyleProperty, "stilDugmici");
                        }
                        btnUpravljanjeOpremom.SetResourceReference(Button.StyleProperty, "stilDugmiciKliknuto");

                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanel();
                        this.popuniListuOpremeSaParametrima(nizOpreme);

                        SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
                        this.popuniListuTipovaOpreme(nizTipovaOblasti);

                        Dispatcher.BeginInvoke(new Action(RenderingDone), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                    }
                    break;

            }
            //this.CurrentOblastiOpreme = null;
            //this.staraOblastOpreme = null;

            //if (this.TrenutniGridTriVjuOblastiOpreme != null)
            //{
            //    Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
            //    Label trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as Label;
            //    trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
            //    trenutniTxtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
            //    this.TrenutniGridTriVjuOblastiOpreme = null;
            //}

            //daLiJeListaOblastiOpremeOsvezena = false;
            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
            //this.popuniListuOblasti(nizOblasti);

            //if (timerOsveziListuOblastiOpreme != null)
            //{
            //    if (timerOsveziListuOblastiOpreme.IsEnabled)
            //        timerOsveziListuOblastiOpreme.Stop();
            //}
            //timerOsveziListuOblastiOpreme = new DispatcherTimer();
            //timerOsveziListuOblastiOpreme.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //timerOsveziListuOblastiOpreme.Tick += timerOsveziListuOblastiOpreme_Tick;
            //timerOsveziListuOblastiOpreme.Start();


        }

        void timerOsveziListuOblastiOpreme_Tick(object sender, EventArgs e)
        {
            if (timerOsveziListuOblastiOpreme.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();


                //if (brdUnosOblastiOpreme.Opacity == 1)
                //{
                //    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                //    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                //    Panel.SetZIndex(brdUnosOblastiOpreme, 0);
                //}
                if (brdPrikazDetaljaOblastiOpreme.Opacity == 1)
                {
                    brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //brdPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                    Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
                }



                brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 1);
            }

            timerOsveziListuOblastiOpreme.Stop();
        }

        private void grdListaTipovaOpreme_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grdHierarchicalTipoviOpreme = (Grid)sender;
            TrenutnaVrednostSirine = grdHierarchicalTipoviOpreme.ActualWidth - 35;
            TextBlock tbIdTipoviOpreme = grdHierarchicalTipoviOpreme.FindName("skrivenId") as TextBlock;
            int idTipoviOpreme = Convert.ToInt32(tbIdTipoviOpreme.Text);


            Border brdHierarchicalTipoviOpreme = grdHierarchicalTipoviOpreme.FindName("brdHierarchical") as Border;
            Border brdNazivTipaOpremeIOdabranaOblastOpreme = grdHierarchicalTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
            DoubleAnimation daBrdTriVjuHierarchicalTipoviOpremeSirina = new DoubleAnimation(opasiti0, TrenutnaVrednostSirine, trajanje200);
            //Border brdAktivnoTipoviOpreme = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;

            //Border brdProbaBorderiTipoviOpreme = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;

            TextBlock tbTriVjuHierarchicalNazivTipaOpreme = grdHierarchicalTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
            int idOblastiOpremeTipoviOpreme = Convert.ToInt32((grdHierarchicalTipoviOpreme.FindName("skrivenId") as TextBlock).Text);

            if (this.TrenutniGridTriVjuTipoviOpreme != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;


                int idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                if (idIzTrenutnogGrida != idTipoviOpreme && idIzTrenutnogGrida != 0)
                {
                    brdNazivTipaOpremeIOdabranaOblastOpreme.Background = Brushes.Gainsboro;
                    brdNazivTipaOpremeIOdabranaOblastOpreme.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                    brdNazivTipaOpremeIOdabranaOblastOpreme.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalTipoviOpremeSirina);
                    tbTriVjuHierarchicalNazivTipaOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else
            {
                brdNazivTipaOpremeIOdabranaOblastOpreme.Background = Brushes.Gainsboro;
                brdNazivTipaOpremeIOdabranaOblastOpreme.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                brdNazivTipaOpremeIOdabranaOblastOpreme.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalTipoviOpremeSirina);
                tbTriVjuHierarchicalNazivTipaOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }
            daBrdTriVjuHierarchicalTipoviOpremeSirina = null;
        }

        private void grdListaTipovaOpreme_MouseLeave(object sender, MouseEventArgs e)
        {
            var treeViewItemTipoviOpreme = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItemTipoviOpreme != null)
            {
                Grid grdHierarchicalTipoviOpreme = (Grid)sender;
                TextBlock tbIdTipoviOpreme = grdHierarchicalTipoviOpreme.FindName("skrivenId") as TextBlock;
                int idTipoviOpreme = Convert.ToInt32(tbIdTipoviOpreme.Text);
                int idIzTrenutnogGridaTipoviOpreme = 0;


                Border brdNazivTipaOpremeIOdabranaOblastOpreme = grdHierarchicalTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                Border brdTriVjuHierarchicalNaziv = grdHierarchicalTipoviOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                //Border brdAktivno = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;
                //Border brdProbaBorderi = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;
                TextBlock tbTriVjuHierarchicalNazivTipaOpreme = grdHierarchicalTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;

                if (this.TrenutniGridTriVjuTipoviOpreme != null)
                {
                    TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;
                    idIzTrenutnogGridaTipoviOpreme = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                    if (idIzTrenutnogGridaTipoviOpreme != idTipoviOpreme && idIzTrenutnogGridaTipoviOpreme != 0)
                    {
                        brdNazivTipaOpremeIOdabranaOblastOpreme.Background = Brushes.Transparent;
                        tbTriVjuHierarchicalNazivTipaOpreme.Foreground = tbIdTipoviOpreme.Foreground;
                    }
                }
                else
                {
                    brdNazivTipaOpremeIOdabranaOblastOpreme.Background = Brushes.Transparent;
                    tbTriVjuHierarchicalNazivTipaOpreme.Foreground = tbIdTipoviOpreme.Foreground;
                }

            }
        }

        private void grdListaTipovaOpreme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ContentControl cclcclTipoviOpremePrikazDetaljaHeder = ctp.FindName("cclTipoviOpremePrikazDetaljaHeder", cclPrikazDetaljaKorisnici) as ContentControl;
            cclPrikazDetaljaDugmiciTipoviOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciTipoviOpremeAzuriranje");
            ControlTemplate ctp2 = cclTipoviOpremePrikazDetaljaHeder.Template as ControlTemplate;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = null;
            if (ctp2 != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp2.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclTipoviOpremePrikazDetaljaHeder) as Grid;

            Grid grdListaTipovaOpreme = (Grid)sender;
            int id = Convert.ToInt32((grdListaTipovaOpreme.FindName("skrivenId") as TextBlock).Text);
            int idIzTrenutnogGrida = 0;


            if (this.CurrentTipoviOpreme != null)
            {
                stariTipOpreme = CurrentTipoviOpreme;
            }

            for (int i = 0; i < ListaTipovaOpreme.Count; i++)
            {
                if (id == ListaTipovaOpreme[i].IdTipOpreme)
                {
                    this.CurrentTipoviOpreme = ListaTipovaOpreme[i];
                    break;
                }
            }

            for (int i = 0; i < this.ListaOblastiOpreme.Count; i++)
            {
                if (this.ListaOblastiOpreme[i].IdOblastiOpreme == this.CurrentTipoviOpreme.IdOblastiOpreme)
                {
                    IzabranaOblastOpreme = i;
                    break;
                }
            }

            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                Trenutnitblck.Foreground = Brushes.Black;
            }
            trenutniGridKliknutoNaRBKorisnici = null;


            if (this.CurrentTipoviOpreme != null)
            {

                if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                {
                    if (this.CurrentTipoviOpreme.DeletedItem)
                    {


                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    }
                }

                if (stariTipOpreme != null)
                {


                    if (brdPrazanPrikazDetaljaTipoviOpreme.Opacity == 1)
                    {
                        brdPrazanPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                        brdPrazanPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                        Panel.SetZIndex(brdPrazanPrikazDetaljaTipoviOpreme, 0);
                    }
                    //if (brdUnosTipaOpreme.Opacity == 1)
                    //{
                    //    brdUnosTipaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //    brdUnosTipaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                    //    Panel.SetZIndex(brdUnosTipaOpreme, 0);
                    //}


                    if (stariTipOpreme != CurrentTipoviOpreme)
                    {
                        DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        brdPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 1);
                        brdPrazanPrikazDetaljaTipoviOpreme.IsEnabled = false;
                        //brdUnosTipaOpreme.IsEnabled = false;
                        brdPrikazDetaljaTipoviOpreme.IsEnabled = true;
                    }

                    //brdPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                    //brdPrikazDetaljaSadrzajTipoviOpreme.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                    //Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 1);

                }
                else
                {
                    if (brdPrazanPrikazDetaljaTipoviOpreme.Opacity == 1)
                    {
                        brdPrazanPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                        brdPrazanPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                        Panel.SetZIndex(brdPrazanPrikazDetaljaTipoviOpreme, 0);
                    }
                    //if (brdUnosTipaOpreme.Opacity == 1)
                    //{
                    //    brdUnosTipaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //    brdUnosTipaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                    //    Panel.SetZIndex(brdUnosTipaOpreme, 0);
                    //}


                    brdPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                    brdPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                    Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 1);
                    brdPrazanPrikazDetaljaTipoviOpreme.IsEnabled = false;
                    //brdUnosTipaOpreme.IsEnabled = false;
                    brdPrikazDetaljaTipoviOpreme.IsEnabled = true;
                }
                if (this.TrenutniGridTriVjuTipoviOpreme != null)
                {
                    TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;
                    idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);
                    //idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock).Text);

                    if (idIzTrenutnogGrida != id && idIzTrenutnogGrida != 0)
                    {

                        Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                        //TextBox trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("txtBoxNazivOblastiOpreme") as TextBox;
                        trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                        TextBlock tbTriVjuHierarchicalNazivTipaOpreme = TrenutniGridTriVjuTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
                        tbTriVjuHierarchicalNazivTipaOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                        TrenutniGridTriVjuTipoviOpreme = grdListaTipovaOpreme;
                    }
                }
                else
                {
                    TrenutniGridTriVjuTipoviOpreme = grdListaTipovaOpreme;
                }
            }

            stariTipOpreme = CurrentTipoviOpreme;
            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "PRIKAZ DETALJA";


            //else
            //{

            //    brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

            //    brdUnosOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdUnosOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);


            //    brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //    brdPrazanPrikazDetaljaSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            //    Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 0);
            //    Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 1);

            //}



            if (daLiSePrekinuoTajmerKorisnici)
            {
                daLiSePrekinuoTajmerKorisnici = false;
                if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                }
                if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                    timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                }
            }

        }

        private void btnUnesiNovuOblastOpreme_Click(object sender, RoutedEventArgs e)
        {
            novaOblastOpreme_Click();
        }



        private void novaOblastOpreme_Click()
        {
            dalijeUnosUToku = true;
            //ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
            //cclPrikazDetaljaKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosKorisnici");
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;
            Grid grdPrikazDetaljaKorisnici = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            ContentControl cclPrikazDetaljaDugmiciOblastiOpreme = ctpOblastiOpreme.FindName("cclPrikazDetaljaDugmiciOblastiOpreme", cclPrikazDetaljaOblastiOpreme) as ContentControl;
            if (cclPrikazDetaljaDugmiciOblastiOpreme != null)
                cclPrikazDetaljaDugmiciOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciOblastiOpremeUnos");
            Border brdPrikazDetaljaKorisniciSadrzaj = ctpOblastiOpreme.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaOblastiOpreme) as Border;


            this.CurrentOblastiOpreme = null;
            this.staraOblastOpreme = null;
            this.CurrentOblastiOpreme = new OblastiOpreme(null)
            {
                Picture = App.PutanjaDoSlikeNoImage
            };
            if (this.TrenutniGridTriVjuOblastiOpreme != null)
            {
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                trenutniTxtBoxNazivOblastiOpreme.Foreground = Brushes.Black;
                this.TrenutniGridTriVjuOblastiOpreme = null;
            }


            if (brdPrazanPrikazDetaljaOblastiOpreme.Opacity > 0)
            {
                brdPrazanPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaOblastiOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaOblastiOpreme, 0);
            }

            brdPrikazDetaljaOblastiOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrikazDetaljaOblastiOpreme, 1);
            brdPrazanPrikazDetaljaOblastiOpreme.IsEnabled = false;
            brdPrikazDetaljaOblastiOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaOblastiOpreme.Title = "UNOS NOVOG TIPA OPREME";

            
        }

        private void noviTipOpreme_Click()
        {
            this.CurrentTipoviOpreme = null;
            this.stariTipOpreme = null;
            this.CurrentTipoviOpreme = new TipoviOpreme(null)
            {
                Picture = App.PutanjaDoSlikeNoImage
            };
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeAdminPanel();
            this.popuniListuOblastiZaTipoveOpreme(nizOblasti);
            

            this.IzabranaOblastOpreme = 0;

            cclPrikazDetaljaDugmiciTipoviOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciTipoviOpremeUnos");



            
            //this.IzabranaOblastOpremeZaUnos = 0;
            if (this.TrenutniGridTriVjuTipoviOpreme != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuTipoviOpreme.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuTipoviOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                Label trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as Label;
                if (trenutniBrdTriVjuHierarchicalNaziv != null)
                    trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                if (trenutniTxtBoxNazivOblastiOpreme != null)
                    trenutniTxtBoxNazivOblastiOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                this.TrenutniGridTriVjuTipoviOpreme = null;
            }


            if (brdPrazanPrikazDetaljaTipoviOpreme.Opacity == 1)
            {
                brdPrazanPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaTipoviOpreme, 0);
            }

            brdPrikazDetaljaTipoviOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrikazDetaljaTipoviOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrikazDetaljaTipoviOpreme, 1);
            brdPrazanPrikazDetaljaTipoviOpreme.IsEnabled = false;
            brdPrikazDetaljaTipoviOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "UNOS NOVOG TIPA OPREME";

            
        }

        private void novaOprema_Click()
        {
            cclPrikazDetaljaDugmiciOprema.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciOpremaUnos");

            if (this.listaVrednostiParametara != null)
                listaVrednostiParametara.ItemsSource = null;
            this.CurrentOprema = null;
            this.staraOprema = null;
            slucajnoUProlazu = true;
            IdOpremaZaSakrivanjePrikazaDetaljaOpreme = 0;
            //this.CurrentOprema.ListaParametara.Clear();
            grdOpremaOdabirTipaOpreme.Visibility = Visibility.Visible;
            Panel.SetZIndex(grdOpremaOdabirTipaOpreme, 1);
            grdOpremaOdabirTipaOpreme.IsEnabled = true;
            grdOpremaOdabraniTipOpreme.Visibility = Visibility.Hidden;
            Panel.SetZIndex(grdOpremaOdabraniTipOpreme, -1);
            grdOpremaOdabraniTipOpreme.IsEnabled = false;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpremeZaUnosOpreme(nizTipovaOblasti);

            dalijeUnosUToku = true;
            this.CurrentOprema = new Oprema(null)
            {
                IzabranTipOpreme = 0,
                Picture = App.PutanjaDoSlikeNoImage,
            };
            //this.IzabranaOblastOpremeZaUnos = 0;
            if (this.TrenutniGridTriVjuOprema != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuOprema.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOprema.FindName("brdNazivITipOpremeOpreme") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuOprema.FindName("tbNaslovOpreme") as TextBlock;
                trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                trenutniTxtBoxNazivOblastiOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                this.TrenutniGridTriVjuOprema = null;
            }


            if (brdPrazanPrikazDetaljaOpreme.Opacity > 0)
            {
                brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 0);
            }


            brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrikazDetaljaOpreme, 1);
            brdPrazanPrikazDetaljaOpreme.IsEnabled = false;
            brdPrikazDetaljaOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaOpreme.Title = "UNOS NOVE OPREME";
        }

        private void noviParametar_Click()
        {
            this.CurrentParametri = null;
            this.stariParametar = null;
            

            cclPrikazDetaljaDugmiciParametriIFilteri.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciParametriUnos");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpreme(nizTipova);

            cmbTipoviParametra.ItemsSource = Enum.GetNames(typeof(TipParametara));
            string[] listaTipovaParametara;
            listaTipovaParametara = Enum.GetNames(typeof(TipParametara)) as string[];
            cmbTipoviParametra.SelectedIndex = 0;
            this.CurrentParametri = new Parametri(null)
                {
                    IzabranTipOpreme = 0,
                    
                    
                };

            //this.IzabranaOblastOpremeZaUnos = 0;
            if (this.TrenutniGridTriVjuParametri != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuParametri.FindName("skrivenId") as TextBlock;
                Border trenutniBrdTriVjuHierarchicalNaziv = TrenutniGridTriVjuParametri.FindName("brdNazivITipParametra") as Border;
                TextBlock trenutniTxtBoxNazivOblastiOpreme = TrenutniGridTriVjuParametri.FindName("txtBoxNazivParametra") as TextBlock;
                if (trenutniBrdTriVjuHierarchicalNaziv != null)
                    trenutniBrdTriVjuHierarchicalNaziv.Background = Brushes.Transparent;
                if (trenutniTxtBoxNazivOblastiOpreme != null)
                    trenutniTxtBoxNazivOblastiOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                this.TrenutniGridTriVjuParametri = null;
            }


            if (brdPrazanPrikazDetaljaParametriIFilteri.Opacity > 0)
            {
                brdPrazanPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaParametriIFilteri, 0);
            }

            brdPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrikazDetaljaParametriIFilteri, 1);
            brdPrazanPrikazDetaljaParametriIFilteri.IsEnabled = false;
            brdPrikazDetaljaParametriIFilteri.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaParametriIFilteri.Title = "UNOS NOVOG PARAMETRA";

            
        }

        private bool dalijeUnosUToku = false;

        private void noviKorisnik_Click()
        {
            dalijeUnosUToku = true;
            //ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
            //cclPrikazDetaljaKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosKorisnici");
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Border brdPrikazDetaljaKorisniciSadrzaj = ctpKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaKorisnici) as Border;
            ContentControl cclPrikazDetaljaDugmiciKorisnici = ctpKorisnici.FindName("cclPrikazDetaljaDugmiciKorisnici", cclPrikazDetaljaKorisnici) as ContentControl;
            if (cclPrikazDetaljaDugmiciKorisnici != null)
                cclPrikazDetaljaDugmiciKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciKorisniciUnos");
            if (grdPrikazDetaljaKorisnici != null)
            {
                Grid grdDatumKreiranjaKorisnici = grdPrikazDetaljaKorisnici.FindName("grdDatumKreiranjaKorisnici") as Grid;
                Grid grdDatumAzuriranjaKorisnici = grdPrikazDetaljaKorisnici.FindName("grdDatumAzuriranjaKorisnici") as Grid;
                grdDatumAzuriranjaKorisnici.Visibility = grdDatumKreiranjaKorisnici.Visibility = Visibility.Hidden;
                grdDatumKreiranjaKorisnici.Height = grdDatumAzuriranjaKorisnici.Height = 0;
            }



            //Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            //Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            //Border brdPrikazDetaljaKorisniciSadrzaj = grdPrikazDetaljaKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj") as Border;

            //ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            this.CurrentKorisnici = null;
            this.stariKorisnik = null;
            this.CurrentKorisnici = new Korisnici()
                {
                    SlikaKorisnika =  App.PutanjaDoSlikeAdministratorKorisnici,
                    IzabranaUloga = 0
                };

            if (this.TrenutniGridTriVjuKorisnici != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuKorisnici.FindName("skrivenId") as TextBlock;
                Border brdNazivIUlogaKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdNazivIUlogaKorisnika") as Border;
                Border brdTriVjuHierarchicalImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdTriVjuHierarchicalImeIPrezimeKorisnika") as Border;
                TextBlock tbImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                brdNazivIUlogaKorisnika.Background = Brushes.Transparent;
                tbImeIPrezimeKorisnika.Foreground = tbTrenutniGridTrivjuID.Foreground;
                TrenutniGridTriVjuKorisnici = null;
            }


            if (brdPrazanPrikazDetaljaKorisnici.Opacity == 1)
            {
                brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);
            }
            //if (brdPrikazDetaljaKorisnici.Opacity == 1)
            //{
            //    brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    Panel.SetZIndex(brdPrikazDetaljaKorisnici, 0);
            //}

            brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            //brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
            Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
            //brdPrikazDetaljaKorisnici.IsEnabled = false;
            brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
            brdPrikazDetaljaKorisnici.IsEnabled = true;

            


            LejautDokumentTitlePrikazDetaljaKorisnici.Title = "UNOS NOVOG KORISNIKA";
        }

        private void btnUnesiNoviTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            noviTipOpreme_Click();
        }

        private void popuniListuZaPrikazDetaljaOpreme()
        {
            slucajnoUProlazu = false;
            int izabranTipOpreme = 0;
            if (this.CurrentOprema != null && this.CurrentOprema.IzabranTipOpreme > 0)
                izabranTipOpreme = this.CurrentOprema.IzabranTipOpreme;

            ListaTipovaOpremeZaUnosOpreme = null;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipovaOblasti = service.TipOpremeAdminPanel();
            this.popuniListuTipovaOpremeZaUnosOpreme(nizTipovaOblasti);

            ListaVrednostiParametaraTemp = null;
            ListaVrednostiParametaraTemp = new ObservableCollection<Grid>();

            for (int i = 0; i < this.CurrentOprema.ListaParametara.Count; i++)
            {
                if (this.CurrentOprema.ListaParametara[i].TipParametra == "BIT")
                {

                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = this.CurrentOprema.ListaParametara[i];

                    Grid grdZaListu = new Grid();
                    RowDefinition redRazmaka = new RowDefinition();
                    RowDefinition redSadrzaj = new RowDefinition();
                    ColumnDefinition kolonaLevo = new ColumnDefinition();
                    ColumnDefinition kolonaCentar = new ColumnDefinition();
                    ColumnDefinition kolonaDesno = new ColumnDefinition();

                    kolonaLevo.Width = new GridLength(190);
                    kolonaCentar.Width = new GridLength(20);
                    kolonaDesno.Width = new GridLength(250);
                    redRazmaka.Height = new GridLength(5);

                    CheckBox chb = new CheckBox();
                    //if (parametar.VrednostParametra == "0")
                    //    chb.IsChecked = true;
                    //else chb.IsChecked = false;
                    chb.HorizontalAlignment = HorizontalAlignment.Left;
                    chb.Tag = i;
                    BindingOperations.SetBinding(chb, CheckBox.IsCheckedProperty, binding);

                    TextBlock lbl = new TextBlock();
                    lbl.Text = this.CurrentOprema.ListaParametara[i].Name.ToUpper();
                    lbl.Tag = i;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    grdZaListu.RowDefinitions.Add(redSadrzaj);
                    grdZaListu.RowDefinitions.Add(redRazmaka);
                    grdZaListu.ColumnDefinitions.Add(kolonaLevo);
                    grdZaListu.ColumnDefinitions.Add(kolonaCentar);
                    grdZaListu.ColumnDefinitions.Add(kolonaDesno);

                    Grid.SetRow(lbl, 0);
                    Grid.SetColumn(lbl, 0);
                    Grid.SetRow(chb, 0);
                    Grid.SetColumn(chb, 2);

                    grdZaListu.Children.Add(lbl);
                    grdZaListu.Children.Add(chb);

                    ListaVrednostiParametaraTemp.Add(grdZaListu);



                }
                else if (this.CurrentOprema.ListaParametara[i].TipParametra == "TEXT")
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("VrednostParametra");
                    binding.Source = this.CurrentOprema.ListaParametara[i];

                    TextBox tb = new TextBox();
                    tb.Tag = i;

                    BindingOperations.SetBinding(tb, TextBox.TextProperty, binding);

                    TextBlock lbl = new TextBlock();
                    lbl.Text = this.CurrentOprema.ListaParametara[i].Name.ToUpper();
                    lbl.Tag = i;
                    lbl.VerticalAlignment = VerticalAlignment.Center;

                    Grid grdZaListu = new Grid();
                    RowDefinition redRazmaka = new RowDefinition();
                    RowDefinition redSadrzaj = new RowDefinition();
                    ColumnDefinition kolonaLevo = new ColumnDefinition();
                    ColumnDefinition kolonaCentar = new ColumnDefinition();
                    ColumnDefinition kolonaDesno = new ColumnDefinition();

                    kolonaLevo.Width = new GridLength(190);
                    kolonaCentar.Width = new GridLength(20);
                    kolonaDesno.Width = new GridLength(250);
                    redRazmaka.Height = new GridLength(5);





                    grdZaListu.RowDefinitions.Add(redSadrzaj);
                    grdZaListu.RowDefinitions.Add(redRazmaka);
                    grdZaListu.ColumnDefinitions.Add(kolonaLevo);
                    grdZaListu.ColumnDefinitions.Add(kolonaCentar);
                    grdZaListu.ColumnDefinitions.Add(kolonaDesno);

                    Grid.SetRow(lbl, 0);
                    Grid.SetColumn(lbl, 0);
                    Grid.SetRow(tb, 0);
                    Grid.SetColumn(tb, 2);

                    grdZaListu.Children.Add(lbl);
                    grdZaListu.Children.Add(tb);

                    ListaVrednostiParametaraTemp.Add(grdZaListu);

                }
                else
                {
                    Binding bindingg = new Binding();
                    bindingg.Path = new PropertyPath("VrednostParametra");
                    bindingg.Source = this.CurrentOprema.ListaParametara[i];
                    TextBox tb = new TextBox();
                    //tb.Text = item.VrednostParametra;
                    tb.Tag = i;
                    BindingOperations.SetBinding(tb, TextBox.TextProperty, bindingg);

                    TextBlock lbl = new TextBlock();
                    lbl.Text = this.CurrentOprema.ListaParametara[i].Name.ToUpper();
                    lbl.Tag = i;
                    lbl.VerticalAlignment = VerticalAlignment.Center;

                    Grid grdZaListu = new Grid();
                    RowDefinition redRazmaka = new RowDefinition();
                    RowDefinition redSadrzaj = new RowDefinition();
                    ColumnDefinition kolonaLevo = new ColumnDefinition();
                    ColumnDefinition kolonaCentar = new ColumnDefinition();
                    ColumnDefinition kolonaDesno = new ColumnDefinition();

                    kolonaLevo.Width = new GridLength(190);
                    kolonaCentar.Width = new GridLength(20);
                    kolonaDesno.Width = new GridLength(250);
                    redRazmaka.Height = new GridLength(5);



                    grdZaListu.RowDefinitions.Add(redSadrzaj);
                    grdZaListu.RowDefinitions.Add(redRazmaka);
                    grdZaListu.ColumnDefinitions.Add(kolonaLevo);
                    grdZaListu.ColumnDefinitions.Add(kolonaCentar);
                    grdZaListu.ColumnDefinitions.Add(kolonaDesno);

                    Grid.SetRow(lbl, 0);
                    Grid.SetColumn(lbl, 0);
                    Grid.SetRow(tb, 0);
                    Grid.SetColumn(tb, 2);

                    grdZaListu.Children.Add(lbl);
                    grdZaListu.Children.Add(tb);

                    ListaVrednostiParametaraTemp.Add(grdZaListu);
                }
            }
            listaVrednostiParametara.ItemsSource = null;
            listaVrednostiParametara.ItemsSource = ListaVrednostiParametaraTemp;

            if (this.CurrentOprema != null && izabranTipOpreme > 0)
                this.CurrentOprema.IzabranTipOpreme = izabranTipOpreme;
            else if (this.CurrentOprema != null)
                this.CurrentOprema.IzabranTipOpreme = 0;
            slucajnoUProlazu = true;
        }

        private void prikaziPrikazDetaljaOpreme()
        {

            ControlTemplate ctp2 = cclOpremaPrikazDetaljaHeder.Template as ControlTemplate;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = null;
            if (ctp2 != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp2.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclOpremaPrikazDetaljaHeder) as Grid;


            if (this.CurrentOprema != null)
            {

                if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                {
                    if (this.CurrentOprema.DeletedItem)
                    {


                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    }
                }




                if (staraOprema != null)
                {

                    if (brdPrazanPrikazDetaljaOpreme.Opacity > 0)
                    {
                        brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                        brdPrazanPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                        Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 0);
                        brdPrazanPrikazDetaljaOpreme.IsEnabled = false;
                    }


                    if (staraOprema != this.CurrentOprema)
                    {
                        //DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

                        if (dalijeUnosUToku)
                            brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                        else
                            brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, null);
                        brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        dalijeUnosUToku = false;
                    }
                }
                else
                {
                    brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                    brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                }




            }



            Panel.SetZIndex(brdPrikazDetaljaOpreme, 1);
            brdPrikazDetaljaOpreme.IsEnabled = true;






            brdPrikazDetaljaOpreme.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaOpreme.Title = "PRIKAZ DETALJA";

            stariParametar = CurrentParametri;



            if (daLiSePrekinuoTajmerKorisnici)
            {
                daLiSePrekinuoTajmerKorisnici = false;
                if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                }
                if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                    timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                }
            }
        }

        DispatcherTimer timerZaSakrivanjePrikazaDetaljaOpreme = new DispatcherTimer();
        int IdOpremaZaSakrivanjePrikazaDetaljaOpreme = 0;

        private void grdListaOpreme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (timerZaSakrivanjePrikazaDetaljaOpreme == null)
                timerZaSakrivanjePrikazaDetaljaOpreme = new DispatcherTimer();
            timerZaSakrivanjePrikazaDetaljaOpreme.Interval = trajanje200;
            Grid grdListaOpreme = (Grid)sender;
            TextBlock tbIdOprema = grdListaOpreme.FindName("skrivenId") as TextBlock;
            if (IdOpremaZaSakrivanjePrikazaDetaljaOpreme != Convert.ToInt32(tbIdOprema.Text))
                IdOpremaZaSakrivanjePrikazaDetaljaOpreme = Convert.ToInt32(tbIdOprema.Text);
            else return;

            cclPrikazDetaljaDugmiciOprema.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciOpremaAzuriranje");

            grdOpremaOdabirTipaOpreme.Visibility = Visibility.Hidden;
            grdOpremaOdabirTipaOpreme.IsEnabled = false;
            Panel.SetZIndex(grdOpremaOdabirTipaOpreme, -1);

            grdOpremaOdabraniTipOpreme.Visibility = Visibility.Visible;
            grdOpremaOdabraniTipOpreme.IsEnabled = true;
            Panel.SetZIndex(grdOpremaOdabraniTipOpreme, 1);

            int idIzTrenutnogGrida = 0;

            if (this.CurrentOprema != null)
            {
                staraOprema = CurrentOprema;
                if (brdPrikazDetaljaOpreme.Opacity > 0)
                {
                    brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                    Panel.SetZIndex(brdPrikazDetaljaOpreme, 0);
                    brdPrikazDetaljaOpreme.IsEnabled = false;
                }

            }
            else
            {
                timerZaSakrivanjePrikazaDetaljaOpreme.Interval = new TimeSpan(0, 0, 0, 0, 40);
                if (brdPrazanPrikazDetaljaOpreme.Opacity > 0)
                {
                    brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    brdPrazanPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                    Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 0);
                    brdPrazanPrikazDetaljaOpreme.IsEnabled = false;

                }
            }




            if (this.TrenutniGridTriVjuOprema != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuOprema.FindName("skrivenId") as TextBlock;
                idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                if (idIzTrenutnogGrida != IdOpremaZaSakrivanjePrikazaDetaljaOpreme && idIzTrenutnogGrida != 0)
                {
                    Border brdNazivITipOpremeOpreme = TrenutniGridTriVjuOprema.FindName("brdNazivITipOpremeOpreme") as Border;
                    Border brdTriVjuHierarchicalNaziv = TrenutniGridTriVjuOprema.FindName("brdTriVjuHierarchicalNaziv") as Border;
                    TextBlock tbNaslovOpreme = TrenutniGridTriVjuOprema.FindName("tbNaslovOpreme") as TextBlock;
                    brdNazivITipOpremeOpreme.Background = Brushes.Transparent;
                    tbNaslovOpreme.Foreground = tbTrenutniGridTrivjuID.Foreground;
                    TrenutniGridTriVjuOprema = grdListaOpreme;
                }
            }
            else
            {
                TrenutniGridTriVjuOprema = grdListaOpreme;
            }



            if (!timerZaSakrivanjePrikazaDetaljaOpreme.IsEnabled)
            {

                timerZaSakrivanjePrikazaDetaljaOpreme.Tick += timerZaSakrivanjePrikazaDetaljaOpreme_Tick;
                timerZaSakrivanjePrikazaDetaljaOpreme.Start();
            }







        }

        void timerZaSakrivanjePrikazaDetaljaOpreme_Tick(object sender, EventArgs e)
        {
            if (timerZaSakrivanjePrikazaDetaljaOpreme.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();




                for (int i = 0; i < ListaOpreme.Count; i++)
                {
                    if (IdOpremaZaSakrivanjePrikazaDetaljaOpreme == ListaOpreme[i].IdOprema)
                    {
                        this.CurrentOprema = ListaOpreme[i];
                        break;
                    }
                }





                this.popuniListuZaPrikazDetaljaOpreme();
                Dispatcher.BeginInvoke(new Action(prikaziPrikazDetaljaOpreme), System.Windows.Threading.DispatcherPriority.ContextIdle, null);

            }
            if (timerZaSakrivanjePrikazaDetaljaOpreme != null)
                timerZaSakrivanjePrikazaDetaljaOpreme.Stop();
            timerZaSakrivanjePrikazaDetaljaOpreme = null;
        }

        private void tb_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(tb.Tag))
                {

                    if (!tb.IsFocused)
                        LabelId.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    break;
                }
            }
        }
        private void tb_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(tb.Tag))
                {

                    if (!tb.IsFocused)
                        LabelId.Foreground = Brushes.Black;
                    break;
                }
            }
        }

        void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(tb.Tag))
                {

                    LabelId.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                    LabelId.Foreground = Brushes.Black;
                    break;
                }



            }

        }

        void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(tb.Tag))
                {
                    if (tb.IsFocused)
                    {
                        LabelId.BeginAnimation(Label.MarginProperty, taPomeniDesnoZa15);
                        LabelId.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                        break;
                    }

                }
            }




        }
        private void chb_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(cb.Tag))
                {
                    if (cb.IsFocused)
                    {
                        LabelId.BeginAnimation(Label.MarginProperty, taPomeniDesnoZa15);
                        LabelId.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                        break;
                    }

                }
            }
        }

        private void chb_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(cb.Tag))
                {

                    LabelId.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                    LabelId.Foreground = Brushes.Black;
                    break;


                }
            }
        }

        private void chb_MouseEnter(object sender, MouseEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(cb.Tag))
                {

                    if (!cb.IsFocused)
                        LabelId.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    break;
                }
            }
        }
        private void chb_MouseLeave(object sender, MouseEventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            //Grid grd = tb.Parent as Grid;
            //ItemsControl icdesno = grd.Parent as ItemsControl;
            //Border brd = icdesno.Parent as Border;
            //Grid grd2 = brd.Parent as Grid;
            for (int i = 0; i < listaVrednostiParametara.Items.Count; i++)
            {
                TextBlock LabelId = FindChild<TextBlock>(listaVrednostiParametara.ItemContainerGenerator.ContainerFromIndex(i));
                if (Convert.ToInt32(LabelId.Tag) == Convert.ToInt32(chb.Tag))
                {

                    if (!chb.IsFocused)
                        LabelId.Foreground = Brushes.Black;
                    break;
                }
            }
        }

        private void grdListaOpreme_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grdListaOpreme = (Grid)sender;
            TrenutnaVrednostSirine = grdListaOpreme.ActualWidth - 30;
            int idOpreme = Convert.ToInt32((grdListaOpreme.FindName("skrivenId") as TextBlock).Text);


            Border brdNazivITipOpremeOpreme = grdListaOpreme.FindName("brdNazivITipOpremeOpreme") as Border;
            //Border brdUnosKorisnici = grdListaParametriIFilteri.FindName("brdUnosKorisnici") as Border;

            TextBlock tbNaslovOpreme = grdListaOpreme.FindName("tbNaslovOpreme") as TextBlock;
            DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(opasiti0, TrenutnaVrednostSirine, trajanje200);

            if (this.TrenutniGridTriVjuOprema != null)
            {


                int idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOprema.FindName("skrivenId") as TextBlock).Text);

                if (idIzTrenutnogGrida != idOpreme && idIzTrenutnogGrida != 0)
                {
                    brdNazivITipOpremeOpreme.Background = Brushes.Gainsboro;
                    brdNazivITipOpremeOpreme.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);

                    //DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(0, grdListaKorisnici.ActualWidth, trajanje200);
                    brdNazivITipOpremeOpreme.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                    tbNaslovOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else
            {
                brdNazivITipOpremeOpreme.Background = Brushes.Gainsboro;
                brdNazivITipOpremeOpreme.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                brdNazivITipOpremeOpreme.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                tbNaslovOpreme.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }

            daBrdTriVjuHierarchicalImeIPrezimeSirina = null;




        }

        private void grdListaOpreme_MouseLeave(object sender, MouseEventArgs e)
        {
            var treeViewItemOprema = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItemOprema != null)
            {
                Grid grdListaOpreme = (Grid)sender;
                TextBlock tbidOprema = grdListaOpreme.FindName("skrivenId") as TextBlock;
                int idOprema = Convert.ToInt32(tbidOprema.Text);

                Border brdNazivITipOpremeOpreme = grdListaOpreme.FindName("brdNazivITipOpremeOpreme") as Border;
                TextBlock tbNaslovOpreme = grdListaOpreme.FindName("tbNaslovOpreme") as TextBlock;
                int idIzTrenutnogGrida = 0;


                //Border brdHierarchical = grdHierarchicalTipoviOpreme.FindName("brdHierarchical") as Border;
                //Border brdTriVjuHierarchicalNaziv = grdHierarchicalTipoviOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                //Border brdAktivno = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;
                //Border brdProbaBorderi = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;
                //Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;

                if (this.TrenutniGridTriVjuOprema != null)
                {
                    idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuOprema.FindName("skrivenId") as TextBlock).Text);

                    if (idIzTrenutnogGrida != idOprema && idIzTrenutnogGrida != 0)
                    {
                        brdNazivITipOpremeOpreme.Background = Brushes.Transparent;

                        tbNaslovOpreme.Foreground = tbidOprema.Foreground;
                    }
                }
                else
                {
                    brdNazivITipOpremeOpreme.Background = Brushes.Transparent;
                    tbNaslovOpreme.Foreground = tbidOprema.Foreground;
                }

            }
        }

        private void btnUnesiNovuOpremu_Click(object sender, RoutedEventArgs e)
        {
            novaOprema_Click();
        }

        private void grdListaParametriIFilteri_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cclPrikazDetaljaDugmiciParametriIFilteri.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciParametriIFilteriAzuriranje");
            Grid grdListaParametriIFilteri = (Grid)sender;
            TextBlock tbIdParametri = grdListaParametriIFilteri.FindName("skrivenId") as TextBlock;
            int IdParametri = Convert.ToInt32(tbIdParametri.Text);
            int idIzTrenutnogGrida = 0;
            //ControlTemplate ctp = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
            //Grid grd = ctp.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            //stariScrollbarVisibility = Visibility.Hidden;
            //Border brdPrikazDetaljaKorisniciSadrzaj = ctp.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaKorisnici) as Border;
            //ContentControl cclPrikazHederaImePrezimeKorisnici = ctp.FindName("cclPrikazHederaImePrezimeKorisnici", cclPrikazDetaljaKorisnici) as ContentControl;
            ControlTemplate ctp2 = cclParametriIFilteriPrikazDetaljaHeder.Template as ControlTemplate;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = null;
            if (ctp2 != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp2.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclParametriIFilteriPrikazDetaljaHeder) as Grid;




            if (this.TrenutniGridTriVjuParametri != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuParametri.FindName("skrivenId") as TextBlock;
                idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                if (idIzTrenutnogGrida != IdParametri && idIzTrenutnogGrida != 0)
                {
                    Border brdNazivITipParametra = TrenutniGridTriVjuParametri.FindName("brdNazivITipParametra") as Border;
                    Border brdTriVjuHierarchicalNazivParametriIFilteri = TrenutniGridTriVjuParametri.FindName("brdTriVjuHierarchicalNazivParametriIFilteri") as Border;
                    TextBlock txtBoxNazivParametra = TrenutniGridTriVjuParametri.FindName("txtBoxNazivParametra") as TextBlock;
                    brdNazivITipParametra.Background = Brushes.Transparent;
                    txtBoxNazivParametra.Foreground = tbTrenutniGridTrivjuID.Foreground;
                    TrenutniGridTriVjuParametri = grdListaParametriIFilteri;
                }
            }
            else
            {
                TrenutniGridTriVjuParametri = grdListaParametriIFilteri;
            }


            if (this.CurrentParametri != null)
            {
                stariParametar = CurrentParametri;
            }
            for (int i = 0; i < ListaParametara.Count; i++)
            {
                if (IdParametri == ListaParametara[i].IdParametri)
                {
                    this.CurrentParametri = ListaParametara[i];
                    break;
                }
            }

            
            

            if (this.CurrentParametri != null)
            {

                if (ListaTipovaOpreme != null && listaTipovaOpreme.Count > 0)
                {
                    for (int i = 0; i < ListaTipovaOpreme.Count; i++)
                    {
                        if (ListaTipovaOpreme[i].IdTipOpreme == this.CurrentParametri.IdTipOpreme)
                        {
                            this.CurrentParametri.IzabranTipOpreme = i;
                        }
                    }
                }

                if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                {
                    if (this.CurrentParametri.DeletedItem)
                    {


                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    }
                }




                if (stariParametar != null)
                {
                    if (stariParametar != this.CurrentParametri)
                    {
                        DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

                        if (dalijeUnosUToku)
                            brdPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                        brdPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        dalijeUnosUToku = false;
                    }
                }


            }

            if (brdPrazanPrikazDetaljaParametriIFilteri.Opacity > 0)
            {
                brdPrazanPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                Panel.SetZIndex(brdPrazanPrikazDetaljaParametriIFilteri, 0);

                brdPrikazDetaljaParametriIFilteri.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                brdPrikazDetaljaParametriIFilteriSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                Panel.SetZIndex(brdPrikazDetaljaParametriIFilteri, 1);
                brdPrazanPrikazDetaljaParametriIFilteri.IsEnabled = false;


            }


            brdPrikazDetaljaParametriIFilteri.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaParametriIFilteri.Title = "PRIKAZ DETALJA";

            stariParametar = CurrentParametri;



            if (daLiSePrekinuoTajmerKorisnici)
            {
                daLiSePrekinuoTajmerKorisnici = false;
                if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                }
                if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                    timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                }
            }
            cmbTipoviParametra.ItemsSource = Enum.GetNames(typeof(TipParametara));
            string[] listaTipovaParametara;
            listaTipovaParametara = Enum.GetNames(typeof(TipParametara)) as string[];
            cmbTipoviParametra.SelectedIndex = 0;
            if (cmbTipoviParametra != null)
            {
                if (cmbTipoviParametra.Items.Count > 0)
                {
                    for (int i = 0; i < listaTipovaParametara.Length; i++)
                    {
                        if ((listaTipovaParametara[i] as string).ToLower() == this.CurrentParametri.TipParametra.ToLower())
                        {
                            cmbTipoviParametra.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }



        }

        private void grdListaParametriIFilteri_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grdListaParametriIFilteri = (Grid)sender;
            TrenutnaVrednostSirine = grdListaParametriIFilteri.ActualWidth;
            int idParametriIFilteri = Convert.ToInt32((grdListaParametriIFilteri.FindName("skrivenId") as TextBlock).Text);


            Border brdNazivITipParametra = grdListaParametriIFilteri.FindName("brdNazivITipParametra") as Border;
            //Border brdUnosKorisnici = grdListaParametriIFilteri.FindName("brdUnosKorisnici") as Border;

            TextBlock txtBoxNazivParametra = grdListaParametriIFilteri.FindName("txtBoxNazivParametra") as TextBlock;
            DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(opasiti0, TrenutnaVrednostSirine, trajanje200);

            if (this.TrenutniGridTriVjuParametri != null)
            {


                int idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuParametri.FindName("skrivenId") as TextBlock).Text);

                if (idIzTrenutnogGrida != idParametriIFilteri && idIzTrenutnogGrida != 0)
                {
                    brdNazivITipParametra.Background = Brushes.Gainsboro;
                    brdNazivITipParametra.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);

                    //DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(0, grdListaKorisnici.ActualWidth, trajanje200);
                    brdNazivITipParametra.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                    txtBoxNazivParametra.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else
            {
                brdNazivITipParametra.Background = Brushes.Gainsboro;
                brdNazivITipParametra.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                brdNazivITipParametra.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                txtBoxNazivParametra.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }

            daBrdTriVjuHierarchicalImeIPrezimeSirina = null;
        }

        private void grdListaParametriIFilteri_MouseLeave(object sender, MouseEventArgs e)
        {
            var treeViewItemKorisnici = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItemKorisnici != null)
            {
                Grid grdListaParametriIFilteri = (Grid)sender;
                TextBlock tbidParametriIFilteri = grdListaParametriIFilteri.FindName("skrivenId") as TextBlock;
                int idParametriIFilteri = Convert.ToInt32(tbidParametriIFilteri.Text);

                Border brdNazivITipParametra = grdListaParametriIFilteri.FindName("brdNazivITipParametra") as Border;

                TextBlock txtBoxNazivParametra = grdListaParametriIFilteri.FindName("txtBoxNazivParametra") as TextBlock;
                int idIzTrenutnogGrida = 0;


                //Border brdHierarchical = grdHierarchicalTipoviOpreme.FindName("brdHierarchical") as Border;
                //Border brdTriVjuHierarchicalNaziv = grdHierarchicalTipoviOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                //Border brdAktivno = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;
                //Border brdProbaBorderi = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;
                //Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;

                if (this.TrenutniGridTriVjuParametri != null)
                {
                    idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuParametri.FindName("skrivenId") as TextBlock).Text);

                    if (idIzTrenutnogGrida != idParametriIFilteri && idIzTrenutnogGrida != 0)
                    {
                        brdNazivITipParametra.Background = Brushes.Transparent;

                        txtBoxNazivParametra.Foreground = tbidParametriIFilteri.Foreground;
                    }
                }
                else
                {
                    brdNazivITipParametra.Background = Brushes.Transparent;
                    txtBoxNazivParametra.Foreground = tbidParametriIFilteri.Foreground;
                }

            }
        }

        private void btnObrisiParametriIFilteri_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnosParametriIFilteri_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grdListaKorisnici_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grdListaKorisnici = (Grid)sender;
            TrenutnaVrednostSirine = grdListaKorisnici.ActualWidth - 35;
            int idKorisnici = Convert.ToInt32((grdListaKorisnici.FindName("skrivenId") as TextBlock).Text);


            Border brdNazivIUlogaKorisnika = grdListaKorisnici.FindName("brdNazivIUlogaKorisnika") as Border;
            Border brdUnosKorisnici = grdListaKorisnici.FindName("brdUnosKorisnici") as Border;

            TextBlock tbImeIPrezimeKorisnika = grdListaKorisnici.FindName("tbImeIPrezimeKorisnika") as TextBlock;
            DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(opasiti0, TrenutnaVrednostSirine, trajanje200);

            if (this.TrenutniGridTriVjuKorisnici != null)
            {


                int idIzTrenutnogGrida = Convert.ToInt32((TrenutniGridTriVjuKorisnici.FindName("skrivenId") as TextBlock).Text);

                if (idIzTrenutnogGrida != idKorisnici && idIzTrenutnogGrida != 0)
                {
                    brdNazivIUlogaKorisnika.Background = Brushes.Gainsboro;
                    brdNazivIUlogaKorisnika.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);

                    //DoubleAnimation daBrdTriVjuHierarchicalImeIPrezimeSirina = new DoubleAnimation(0, grdListaKorisnici.ActualWidth, trajanje200);
                    brdNazivIUlogaKorisnika.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                    tbImeIPrezimeKorisnika.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else
            {
                brdNazivIUlogaKorisnika.Background = Brushes.Gainsboro;
                brdNazivIUlogaKorisnika.BeginAnimation(Border.MarginProperty, taNazivKategorijeMargina);
                brdNazivIUlogaKorisnika.BeginAnimation(Border.WidthProperty, daBrdTriVjuHierarchicalImeIPrezimeSirina);
                tbImeIPrezimeKorisnika.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }

            daBrdTriVjuHierarchicalImeIPrezimeSirina = null;
        }

        private void grdListaKorisnici_MouseLeave(object sender, MouseEventArgs e)
        {
            var treeViewItemKorisnici = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItemKorisnici != null)
            {
                Grid grdListaKorisnici = (Grid)sender;
                TextBlock tbIdKorisnici = grdListaKorisnici.FindName("skrivenId") as TextBlock;
                int idKorisnici = Convert.ToInt32(tbIdKorisnici.Text);

                Border brdNazivIUlogaKorisnika = grdListaKorisnici.FindName("brdNazivIUlogaKorisnika") as Border;

                TextBlock tbImeIPrezimeKorisnika = grdListaKorisnici.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                int idIzTrenutnogGridaKorisnici = 0;


                //Border brdHierarchical = grdHierarchicalTipoviOpreme.FindName("brdHierarchical") as Border;
                //Border brdTriVjuHierarchicalNaziv = grdHierarchicalTipoviOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                //Border brdAktivno = grdHierarchicalTipoviOpreme.FindName("brdAktivno") as Border;
                //Border brdProbaBorderi = grdHierarchicalTipoviOpreme.FindName("brdProbaBorderi") as Border;
                //Label txtBoxNazivOblastiOpreme = brdTriVjuHierarchicalNaziv.Child as Label;

                if (this.TrenutniGridTriVjuKorisnici != null)
                {
                    idIzTrenutnogGridaKorisnici = Convert.ToInt32((TrenutniGridTriVjuKorisnici.FindName("skrivenId") as TextBlock).Text);

                    if (idIzTrenutnogGridaKorisnici != idKorisnici && idIzTrenutnogGridaKorisnici != 0)
                    {
                        brdNazivIUlogaKorisnika.Background = Brushes.Transparent;

                        tbImeIPrezimeKorisnika.Foreground = tbIdKorisnici.Foreground;
                    }
                }
                else
                {
                    brdNazivIUlogaKorisnika.Background = Brushes.Transparent;
                    tbImeIPrezimeKorisnika.Foreground = tbIdKorisnici.Foreground;
                }

            }
        }
        private DispatcherTimer timerOblastiOpremeRasporediPrikazDetaljaPravilno;
        private DispatcherTimer timerKorisniciRasporediPrikazDetaljaPravilno;
        private DispatcherTimer timerPrikaziDetaljeKorisnika;
        private bool daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = true;
        int idKorisnici = 0;
        private void grdListaKorisnici_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grdListaKorisnika = (Grid)sender;
            TextBlock tbIdKorisnici = grdListaKorisnika.FindName("skrivenId") as TextBlock;
            idKorisnici = Convert.ToInt32(tbIdKorisnici.Text);
            int idIzTrenutnogGrida = 0;
            ControlTemplate ctp = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
            Grid grd = ctp.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            ContentControl cclPrikazDetaljaDugmiciKorisnici = ctp.FindName("cclPrikazDetaljaDugmiciKorisnici", cclPrikazDetaljaKorisnici) as ContentControl;
            if (cclPrikazDetaljaDugmiciKorisnici != null)
                cclPrikazDetaljaDugmiciKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaDugmiciKorisniciAzuriranje");
            stariScrollbarVisibility = Visibility.Hidden;
            Border brdPrikazDetaljaKorisniciSadrzaj = ctp.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaKorisnici) as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = ctp.FindName("cclPrikazHederaImePrezimeKorisnici", cclPrikazDetaljaKorisnici) as ContentControl;
            ControlTemplate ctp2 = cclPrikazHederaImePrezimeKorisnici.Template as ControlTemplate;
            Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = null;
            if (ctp2 != null)
                grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp2.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclPrikazHederaImePrezimeKorisnici) as Grid;

            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                Trenutnitblck.Foreground = Brushes.Black;
            }
            trenutniGridKliknutoNaRBKorisnici = null;


            if (this.TrenutniGridTriVjuKorisnici != null)
            {
                TextBlock tbTrenutniGridTrivjuID = TrenutniGridTriVjuKorisnici.FindName("skrivenId") as TextBlock;
                idIzTrenutnogGrida = Convert.ToInt32(tbTrenutniGridTrivjuID.Text);

                if (idIzTrenutnogGrida != idKorisnici && idIzTrenutnogGrida != 0)
                {
                    Border brdNazivIUlogaKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdNazivIUlogaKorisnika") as Border;
                    Border brdTriVjuHierarchicalImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("brdTriVjuHierarchicalImeIPrezimeKorisnika") as Border;
                    TextBlock tbImeIPrezimeKorisnika = TrenutniGridTriVjuKorisnici.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                    brdNazivIUlogaKorisnika.Background = Brushes.Transparent;
                    tbImeIPrezimeKorisnika.Foreground = tbTrenutniGridTrivjuID.Foreground;
                    TrenutniGridTriVjuKorisnici = grdListaKorisnika;
                }
            }
            else
            {
                TrenutniGridTriVjuKorisnici = grdListaKorisnika;
            }

            //if (this.NoviKorisnik != null)
            //{

            //    brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);


            //    if (Convert.ToInt32(grd.Tag.ToString()) == 2)
            //    {


            //        //brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //        cclPrikazDetaljaKorisnici.BeginAnimation(ContentControl.OpacityProperty, daSakrijPolaSekunde);
            //        //cclPrikazDetaljaKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaKorisnici");
            //        return;
            //        if (timerPrikaziDetaljeKorisnika == null)
            //        {

            //            timerPrikaziDetaljeKorisnika = new DispatcherTimer();
            //            timerPrikaziDetaljeKorisnika.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //        }
            //        if (daLiSePrekinuoTajmerPrikaziDetaljeKorisnika)
            //        {
            //            daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = false;
            //            if (timerPrikaziDetaljeKorisnika == null)
            //            {

            //                timerPrikaziDetaljeKorisnika = new DispatcherTimer();

            //            }

            //            if (!timerPrikaziDetaljeKorisnika.IsEnabled)
            //            {

            //                timerPrikaziDetaljeKorisnika.Tick += timerPrikaziDetaljeKorisnika_Tick;
            //                timerPrikaziDetaljeKorisnika.Start();
            //            }

            //        }

            //    }
            //    this.NoviKorisnik = null;
            //}

            //else
            //{
            if (this.CurrentKorisnici != null)
            {
                stariKorisnik = CurrentKorisnici;
            }
            for (int i = 0; i < ListaKorisnika.Count; i++)
            {
                if (idKorisnici == ListaKorisnika[i].IdKorisnici)
                {
                    this.CurrentKorisnici = ListaKorisnika[i];
                    this.CurrentKorisnici.DaLiJeMuskiPolCekiran = ListaKorisnika[i].PolKorisnika == true;
                    this.CurrentKorisnici.DaLiJeZenskiPolCekiran = ListaKorisnika[i].PolKorisnika == false;
                    break;
                }
            }
            if (this.CurrentKorisnici != null)
            {

                if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                {
                    if (this.CurrentKorisnici.DeletedItem)
                    {


                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    }
                }




                if (stariKorisnik != null)
                {
                    if (stariKorisnik != this.CurrentKorisnici)
                    {
                        DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

                        if (grd != null)
                        {
                            Grid grdDatumKreiranjaKorisnici = grd.FindName("grdDatumKreiranjaKorisnici") as Grid;
                            Grid grdDatumAzuriranjaKorisnici = grd.FindName("grdDatumAzuriranjaKorisnici") as Grid;
                            grdDatumAzuriranjaKorisnici.Visibility = grdDatumKreiranjaKorisnici.Visibility = Visibility.Visible;
                            grdDatumKreiranjaKorisnici.Height = grdDatumAzuriranjaKorisnici.Height = 20;
                        }
                        if (dalijeUnosUToku)
                            brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);
                        brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        dalijeUnosUToku = false;
                    }
                }


            }

            if (brdPrazanPrikazDetaljaKorisnici.Opacity > 0)
            {
                brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);

                brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                //brdUnosKorisnici.IsEnabled = false;

            }


            brdPrikazDetaljaKorisnici.IsEnabled = true;
            LejautDokumentTitlePrikazDetaljaKorisnici.Title = "PRIKAZ DETALJA";

            stariKorisnik = CurrentKorisnici;







            //if (Convert.ToInt32(grd.Tag.ToString()) != 1)
            //{


            //    //brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
            //    cclPrikazDetaljaKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpPrikazDetaljaKorisnici");
            //    if (timerPrikaziDetaljeKorisnika == null)
            //    {

            //        timerPrikaziDetaljeKorisnika = new DispatcherTimer();
            //        timerPrikaziDetaljeKorisnika.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //    }

            //}
            //else if (brdPrazanPrikazDetaljaKorisnici.Opacity == 1)
            //{
            //    brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
            //    brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

            //    Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);
            //    brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
            //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

            //    Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
            //    brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
            //    //brdUnosKorisnici.IsEnabled = false;
            //    brdPrikazDetaljaKorisnici.IsEnabled = true;

            //}

            //if (daLiSePrekinuoTajmerPrikaziDetaljeKorisnika)
            //{
            //        daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = false;
            //        if (timerPrikaziDetaljeKorisnika == null)
            //        {

            //            timerPrikaziDetaljeKorisnika = new DispatcherTimer();

            //        }

            //            if (!timerPrikaziDetaljeKorisnika.IsEnabled)
            //            {

            //                timerPrikaziDetaljeKorisnika.Tick += timerPrikaziDetaljeKorisnika_Tick;
            //                timerPrikaziDetaljeKorisnika.Start();
            //            }

            //}






            //daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = false;


            //ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            //Grid grdKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            //Border brdKorisniciImeIPrezimeNaslov = grdKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            //ContentControl cclPrikazHederaImePrezimeKorisnici = grdKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;




            //grdPrikazObavestenjaZbogArhiviranogKorisnika.Background = Brushes.White;

            //grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;



            if (daLiSePrekinuoTajmerKorisnici)
            {
                daLiSePrekinuoTajmerKorisnici = false;
                if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                }
                if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                {
                    timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                    timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                    timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                }
            }






        }

        void timerPrikaziDetaljeKorisnika_Tick(object sender, EventArgs e)
        {
            if (timerPrikaziDetaljeKorisnika != null && timerPrikaziDetaljeKorisnika.IsEnabled)
            {
                DispatcherTimer timerSender = sender as DispatcherTimer;
                timerSender.Stop();

                ControlTemplate ctp = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
                Grid grd = ctp.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                Border brdPrikazDetaljaKorisniciSadrzaj = ctp.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaKorisnici) as Border;
                Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclPrikazDetaljaKorisnici) as Grid;

                for (int i = 0; i < ListaKorisnika.Count; i++)
                {
                    if (idKorisnici == ListaKorisnika[i].IdKorisnici)
                    {
                        this.CurrentKorisnici = ListaKorisnika[i];
                        this.CurrentKorisnici.DaLiJeMuskiPolCekiran = ListaKorisnika[i].PolKorisnika == true;
                        this.CurrentKorisnici.DaLiJeZenskiPolCekiran = ListaKorisnika[i].PolKorisnika == false;
                        break;
                    }
                }


                if (this.CurrentKorisnici != null)
                {

                    if (stariKorisnik != null)
                    {




                        //brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);

                        //Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                        //brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                        ////brdUnosKorisnici.IsEnabled = false;
                        //brdPrikazDetaljaKorisnici.IsEnabled = true;


                    }
                    else
                    {




                        brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                        brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                        Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                        brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                        //brdUnosKorisnici.IsEnabled = false;
                        brdPrikazDetaljaKorisnici.IsEnabled = true;

                        //if (brdUnosKorisnici.Opacity == 1)
                        //{
                        //    brdUnosKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                        //    brdUnosKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                        //    Panel.SetZIndex(brdUnosKorisnici, 0);
                        //}


                        //if (stariKorisnik != CurrentKorisnici)
                        //{
                        //    DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                        //    brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                        //    Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                        //    brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                        //    //brdUnosKorisnici.IsEnabled = false;
                        //    brdPrikazDetaljaKorisnici.IsEnabled = true;


                        //}
                    }

                }
                else
                {

                    //if (this.CurrentKorisnici != null)
                    //{


                    //    if (stariKorisnik != null)
                    //    {
                    //        if (brdPrazanPrikazDetaljaKorisnici.Opacity == 1)
                    //        {
                    //            brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //            brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                    //            Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);


                    //        }

                    //        //brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                    //        Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                    //        brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                    //        //brdUnosKorisnici.IsEnabled = false;
                    //        brdPrikazDetaljaKorisnici.IsEnabled = true;
                    //        if (daLiSePrekinuoTajmerPrikaziDetaljeKorisnika)
                    //        {
                    //            daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = false;
                    //            if (timerPrikaziDetaljeKorisnika == null)
                    //            {

                    //                timerPrikaziDetaljeKorisnika = new DispatcherTimer();
                    //            }
                    //            if (!timerPrikaziDetaljeKorisnika.IsEnabled)
                    //            {
                    //                timerPrikaziDetaljeKorisnika.Interval = new TimeSpan(0, 0, 0, 0, 500);
                    //                timerPrikaziDetaljeKorisnika.Tick += timerPrikaziDetaljeKorisnika_Tick;
                    //                timerPrikaziDetaljeKorisnika.Start();
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {



                    //        if (brdPrazanPrikazDetaljaKorisnici.Opacity == 1)
                    //        {
                    //            brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //            brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                    //            Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);
                    //        }
                    //        brdPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                    //        brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(ContentControl.MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                    //        Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                    //        brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                    //        //brdUnosKorisnici.IsEnabled = false;
                    //        brdPrikazDetaljaKorisnici.IsEnabled = true;

                    //        //if (brdUnosKorisnici.Opacity == 1)
                    //        //{
                    //        //    brdUnosKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //        //    brdUnosKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                    //        //    Panel.SetZIndex(brdUnosKorisnici, 0);
                    //        //}


                    //        if (stariKorisnik != CurrentKorisnici)
                    //        {
                    //            DoubleAnimation daPrikaziPonovoPolaSekunde = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
                    //            brdPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.OpacityProperty, daPrikaziPonovoPolaSekunde);
                    //            Panel.SetZIndex(brdPrikazDetaljaKorisnici, 1);
                    //            brdPrazanPrikazDetaljaKorisnici.IsEnabled = false;
                    //            //brdUnosKorisnici.IsEnabled = false;
                    //            brdPrikazDetaljaKorisnici.IsEnabled = true;

                    //            if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                    //            {
                    //                if (this.CurrentKorisnici.DeletedItem)
                    //                {


                    //                    grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    //                }
                    //                else
                    //                {
                    //                    grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    //                }
                    //            }
                    //        }




                    //        //if (brdPrazanPrikazDetaljaKorisnici.Opacity == 1)
                    //        //{
                    //        //    brdPrazanPrikazDetaljaKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //        //    brdPrazanPrikazDetaljaKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);

                    //        //    Panel.SetZIndex(brdPrazanPrikazDetaljaKorisnici, 0);
                    //        //}
                    //        //    if (brdUnosKorisnici.Opacity == 1)
                    //        //    {
                    //        //        brdUnosKorisnici.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                    //        //        brdUnosKorisniciSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                    //        //        Panel.SetZIndex(brdUnosKorisnici, 0);
                    //        //    }




                    //        //if (grdPrikazObavestenjaZbogArhiviranogKorisnika != null)
                    //        //{
                    //        //    if (this.CurrentKorisnici.DeletedItem)
                    //        //    {
                    //        //        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Visible;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        grdPrikazObavestenjaZbogArhiviranogKorisnika.Visibility = Visibility.Hidden;
                    //        //    }
                    //        //}

                    //    }
                    //}


                    //if (sacuvanaStaraVisina)
                    //{

                    //    sacuvanaStaraVisina = false;


                    //    brdKorisniciImeIPrezimeNaslov.BeginAnimation(Border.HeightProperty, null);
                    //    cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpHederImePrezimeKorisnici");
                    //}
                    //stariScrollbarVisibility = new Visibility();
                    //ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
                    //Grid grdPrikazObavestenjaZbogArhiviranogKorisnika = ctp.FindName("grdPrikazObavestenjaZbogArhiviranogKorisnika", cclPrikazHederaImePrezimeKorisnici) as Grid;
                    //Border brdPrikazDetaljaKorisniciSadrzaj = ctpKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj", cclPrikazDetaljaKorisnici) as Border;








                }
            }
            if (timerPrikaziDetaljeKorisnika != null)
                timerPrikaziDetaljeKorisnika.Stop();
            timerPrikaziDetaljeKorisnika = null;
            daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = true;
            stariKorisnik = CurrentKorisnici;
            LejautDokumentTitlePrikazDetaljaTipoviOpreme.Title = "PRIKAZ DETALJA";
        }

        void timerKorisniciRasporediPrikazDetaljaPravilno_Tick(object sender, EventArgs e)
        {


            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

                    Grid grdPrikazDetaljaOblastiOpreme = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
                    Grid grdPromenaVisineOblastiOpreme = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
                    Border brdNazivHeder = grdPrikazDetaljaOblastiOpreme.FindName("brdNazivHeder") as Border;
                    ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
                    //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdNazivHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaOblastiOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclPrikazHederaOblastiOpreme) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclPrikazHederaOblastiOpreme) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclPrikazHederaOblastiOpreme) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclPrikazHederaOblastiOpreme) as TextBlock;
                        if (brdNaslovDetalji != null)
                        {
                            if (scrollbarVisibility == Visibility.Visible)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;

                            }
                            stariScrollbarVisibility = scrollbarVisibility;
                        }
                        if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }
                        else if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 130)
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }

                        }


                        if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }

                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisnici = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilno = null;


                    break;

                case 2:



                    //Grid grdPromenaVisineTipoviOpreme = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
                    //Border brdNazivHeder = grdPrikazDetaljaOblastiOpreme.FindName("brdNazivHeder") as Border;
                    //ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
                    //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaTipoviOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclTipoviOpremePrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclTipoviOpremePrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclTipoviOpremePrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclTipoviOpremePrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclTipoviOpremePrikazDetaljaHeder) as TextBlock;
                        if (brdNaslovDetalji != null)
                        {
                            if (scrollbarVisibility == Visibility.Visible)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;

                            }
                            stariScrollbarVisibility = scrollbarVisibility;
                        }
                        if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }
                        else if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 130)
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }

                        }


                        if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }

                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisnici = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    break;


                case 3:
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdOpremaPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclOpremaPrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclOpremaPrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclOpremaPrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclOpremaPrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclOpremaPrikazDetaljaHeder) as TextBlock;
                        if (brdNaslovDetalji != null)
                        {
                            if (scrollbarVisibility == Visibility.Visible)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;

                            }
                            stariScrollbarVisibility = scrollbarVisibility;
                        }
                        if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }
                        else if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 130)
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }

                        }


                        if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }

                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisnici = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    break;
                case 4:



                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdParametriIFilteriPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaParametriIFilteri);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclParametriIFilteriPrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclParametriIFilteriPrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclParametriIFilteriPrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclParametriIFilteriPrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclParametriIFilteriPrikazDetaljaHeder) as TextBlock;
                        if (brdNaslovDetalji != null)
                        {
                            if (scrollbarVisibility == Visibility.Visible)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;

                            }
                            stariScrollbarVisibility = scrollbarVisibility;
                        }
                        if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }
                        else if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 130)
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }

                        }


                        if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }

                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisnici = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    break;

                case 5:
                    ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

                    Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                    Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                    ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;

                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null && timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaKorisnici);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;
                        //
                        ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;

                        Grid brdImeIUlogaKorisnikaDetalji = ctp.FindName("brdImeIUlogaKorisnikaDetalji", cclPrikazHederaImePrezimeKorisnici) as Grid;
                        Image slikaStrelicaHederImePrezimeKorisnika = ctp.FindName("slikaStrelicaHederImePrezimeKorisnika", cclPrikazHederaImePrezimeKorisnici) as Image;
                        if (brdImeIUlogaKorisnikaDetalji != null)
                        {
                            if (scrollbarVisibility == Visibility.Visible)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //
                                if (brdImeIUlogaKorisnikaDetalji.Margin != thicknessProba)
                                    brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);
                                if (brdImeIUlogaKorisnikaDetalji.Margin != marginaCentar)
                                    brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;

                            }
                            stariScrollbarVisibility = scrollbarVisibility;
                        }
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclPrikazHederaImePrezimeKorisnici) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckImePrezime = ctp.FindName("tblckImePrezime", cclPrikazHederaImePrezimeKorisnici) as TextBlock;
                        TextBlock tblckImeIPrezimeKorisnikaDetaljnijeHeder = ctp.FindName("tblckImeIPrezimeKorisnikaDetaljnijeHeder", cclPrikazHederaImePrezimeKorisnici) as TextBlock;

                        if (tblckImePrezime != null && id != 0 && id == 2)
                        {
                            if (tblckImePrezime.ActualHeight > 30)
                            {
                                if (this.CurrentKorisnici != null)
                                    this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = true;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Width = 15;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentKorisnici != null)
                                    this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Width = 0;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }
                        else if (tblckImePrezime != null && id != 0 && id == 1)
                        {
                            if (tblckImePrezime.ActualHeight > 130)
                            {
                                if (this.CurrentKorisnici != null)
                                    this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = true;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 30;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentKorisnici != null)
                                    this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 0;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }

                        }



                        //if (tblckImePrezime != null && id != 0 && id == 2)
                        //{
                        //    if (tblckImePrezime.ActualHeight > 30)
                        //        this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = true;
                        //    else
                        //        this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;

                        //}
                        //else if (tblckImePrezime != null && id != 0 && id == 1)
                        //{
                        //    if (tblckImePrezime.ActualHeight > 130)
                        //    {
                        //        this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = true;
                        //        if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                        //        {
                        //            tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 30;
                        //            tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Visible;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;
                        //        if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                        //        {
                        //            tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 0;
                        //            tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Hidden;
                        //        }
                        //    }
                        //} 

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederImePrezimeKorisnika != null)
                            {
                                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Visible;
                            }
                        }
                        if (!daLiJeMouseEnterKorisniciHederImePrezime && !daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederImePrezimeKorisnika != null)
                            {
                                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Hidden;
                            }
                        }

                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }




                    }

                    daLiSePrekinuoTajmerKorisnici = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilno = null;

                    break;
            }
        }

        void timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick(object sender, EventArgs e)
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:

                    ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;
                    Grid grdPrikazDetaljaOblastiOpreme = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
                    Border brdbrdNazivHeder = grdPrikazDetaljaOblastiOpreme.FindName("brdNazivHeder") as Border;
                    ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null && timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled && daLiJeMouseLeaveKorisniciHederImePrezime)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;



                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;

                        if (daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                            daLiJeMouseLeaveKorisniciHederImePrezime = false;
                        }




                    }




                    kliknutoJednom = false;
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null)
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Stop();
                    timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = null;

                    break;

                case 2:


                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null && timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled && daLiJeMouseLeaveKorisniciHederImePrezime)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;



                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclTipoviOpremePrikazDetaljaHeder) as Image;

                        if (daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                            daLiJeMouseLeaveKorisniciHederImePrezime = false;
                        }




                    }




                    kliknutoJednom = false;
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null)
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Stop();
                    timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = null;

                    break;

                case 3:


                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null && timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled && daLiJeMouseLeaveKorisniciHederImePrezime)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;



                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclOpremaPrikazDetaljaHeder) as Image;

                        if (daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                            daLiJeMouseLeaveKorisniciHederImePrezime = false;
                        }




                    }




                    kliknutoJednom = false;
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null)
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Stop();
                    timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = null;

                    break;
                case 4:


                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null && timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled && daLiJeMouseLeaveKorisniciHederImePrezime)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;



                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclParametriIFilteriPrikazDetaljaHeder) as Image;

                        if (daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                            daLiJeMouseLeaveKorisniciHederImePrezime = false;
                        }




                    }




                    kliknutoJednom = false;
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null)
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Stop();
                    timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = null;

                    break;
                case 5:

                    ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
                    Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                    Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                    ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null && timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled && daLiJeMouseLeaveKorisniciHederImePrezime)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();

                        ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;



                        Image slikaStrelicaHederImePrezimeKorisnika = ctp.FindName("slikaStrelicaHederImePrezimeKorisnika", cclPrikazHederaImePrezimeKorisnici) as Image;

                        if (daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederImePrezimeKorisnika != null)
                            {
                                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Hidden;
                            }
                            daLiJeMouseLeaveKorisniciHederImePrezime = false;
                        }




                    }




                    kliknutoJednom = false;
                    daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
                    if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja != null)
                        timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Stop();
                    timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = null;

                    break;
            }


        }
        private bool daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
        DispatcherTimer timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja;


        private void btnObrisiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            //ControlTemplate peraTMP = (sender as Button).Template as ControlTemplate;
            TextBlock tblckVracenTextZaDugme = FindChild<TextBlock>(sender as Button);
            string pera = tblckVracenTextZaDugme.Text;
            //string pera = peraTMP.FindName("")
            if (pera == "Vrati iz arhive ovog korisnika")
            {

                string poruka = "Da li zaista želite da nastavite?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
                        int id = this.CurrentKorisnici.IdKorisnici;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.KorisniciRestore(id);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuKorisnici(nizKorisnika);
                            this.otkaziIzmeneKorisnici_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            else
            {
                string poruka = "Da li zaista želite da arhivirate ovog korisnika?";
                MessageBoxButton dugmiciZaBrisanje = MessageBoxButton.YesNo;
                MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Upozorenje", dugmiciZaBrisanje, slikaBoxa);

                switch (rezultatBoxa)
                {
                    case MessageBoxResult.Yes:
                        SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                        //ControlTemplate template = Sadrzaj.Template;
                        //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

                        int id = this.CurrentKorisnici.IdKorisnici;

                        if (id != 0)
                        {
                            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.KorisniciDelete(id);
                            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
                            this.popuniListuKorisnici(nizKorisnika);
                            this.otkaziIzmeneKorisnici_Click();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;



                }

            }

            this.pravilnoRasporediListuKorisnika();
        }





        private void btnUnesiNovogKorisnika_Click(object sender, RoutedEventArgs e)
        {
            noviKorisnik_Click();
        }


        private void pravilnoRasporediListuTipovaOpreme()
        {
            if (kojeDugmeJePoReduKliknuto == 2)
            {
                if (trivjuTipoviOpreme != null)
                {
                    //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                    ScrollViewer sv = FindChild<ScrollViewer>(trivjuTipoviOpreme);
                    Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                    int brojTipovaOpreme = trivjuTipoviOpreme.Items.Count;
                    if (trivjuTipoviOpreme.Items != null && brojTipovaOpreme > 0)
                    {
                        for (int i = 0; i < brojTipovaOpreme; i++)
                        {
                            TreeViewItem tvi = trivjuTipoviOpreme.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                            //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                            Grid gridPrvi = FindChild<Grid>(tvi);
                            if (gridPrvi != null)
                            {
                                Border brdProba = gridPrvi.Children[0] as Border;
                                ContentPresenter cp = brdProba.Child as ContentPresenter;
                                Grid gridListaTipovaOpreme = FindChild<Grid>(cp);
                                if (scrollbarVisibility == Visibility.Visible) gridListaTipovaOpreme.Width = trivjuTipoviOpreme.ActualWidth - 20;
                                else gridListaTipovaOpreme.Width = trivjuTipoviOpreme.ActualWidth;
                                Border brdNazivTipaOpremeIOdabranaOblastOpreme = gridListaTipovaOpreme.FindName("brdNazivTipaOpremeIOdabranaOblastOpreme") as Border;
                                brdNazivTipaOpremeIOdabranaOblastOpreme.BeginAnimation(WidthProperty, null);



                                //



                                //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                brdNazivTipaOpremeIOdabranaOblastOpreme.Width = gridListaTipovaOpreme.Width - 30;
                                TextBlock tbImeIPrezimeKorisnika = gridListaTipovaOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
                                TextBlock tbIzabranaOblastOpreme = gridListaTipovaOpreme.FindName("tbIzabranaOblastOpreme") as TextBlock;
                                if (tbImeIPrezimeKorisnika != null)
                                {
                                    TextBlock tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije = gridListaTipovaOpreme.FindName("tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije") as TextBlock;
                                    int t = 60;
                                    //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                    if ((tbImeIPrezimeKorisnika.ActualWidth + t) > gridListaTipovaOpreme.Width)
                                    {

                                        this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova = true;
                                        tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije.Visibility = Visibility.Visible;
                                        tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                    }
                                    else
                                    {
                                        if (this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova == true)
                                        {
                                            this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova = false;
                                            tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije.Visibility = Visibility.Hidden;
                                            tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                            tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije.Width = 0;
                                        }
                                    }
                                }
                                if (tbIzabranaOblastOpreme != null)
                                {
                                    TextBlock tblckIzabranaOblastOpremeDetaljnije = gridListaTipovaOpreme.FindName("tblckIzabranaOblastOpremeDetaljnije") as TextBlock;
                                    int t = 65;
                                    //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                    if ((tbIzabranaOblastOpreme.ActualWidth + t) > gridListaTipovaOpreme.Width)
                                    {

                                        this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova = true;
                                        tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Visible;
                                        tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                    }
                                    else
                                    {
                                        if (this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova == true)
                                        {
                                            this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova = false;
                                            tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Hidden;
                                            tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                            tblckIzabranaOblastOpremeDetaljnije.Width = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void pravilnoRasporediListuKorisnika()
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:

                    if (trivjuOblastiOpreme != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuOblastiOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojOblastiOpreme = trivjuOblastiOpreme.Items.Count;
                        if (trivjuOblastiOpreme.Items != null && brojOblastiOpreme > 0)
                        {
                            for (int i = 0; i < brojOblastiOpreme; i++)
                            {
                                TreeViewItem tviOblastiOpreme = trivjuOblastiOpreme.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviOblastiOpreme);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaOblastiOpreme = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaOblastiOpreme.Width = trivjuOblastiOpreme.ActualWidth - 20;
                                    else gridListaOblastiOpreme.Width = trivjuOblastiOpreme.ActualWidth;
                                    Border brdTriVjuHierarchicalNaziv = gridListaOblastiOpreme.FindName("brdTriVjuHierarchicalNaziv") as Border;
                                    brdTriVjuHierarchicalNaziv.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdTriVjuHierarchicalNaziv.Width = gridListaOblastiOpreme.Width - 30;
                                    TextBlock tbNazivOblastiOpreme = gridListaOblastiOpreme.FindName("tbNazivOblastiOpreme") as TextBlock;
                                    //TextBlock tbIzabranaUlogaKorisnika = gridListaKorisnika.FindName("tbIzabranaUlogaKorisnika") as TextBlock;
                                    if (tbNazivOblastiOpreme != null)
                                    {
                                        TextBlock tblckNazivDetaljnije = gridListaOblastiOpreme.FindName("tblckNazivDetaljnije") as TextBlock;
                                        int t = 60;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbNazivOblastiOpreme.ActualWidth + t) > gridListaOblastiOpreme.Width)
                                        {

                                            this.ListaOblastiOpreme[i].DaLiTekstListeOblastiOpremeZauzimaViseRedova = true;
                                            tblckNazivDetaljnije.Visibility = Visibility.Visible;
                                            tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaOblastiOpreme[i].DaLiTekstListeOblastiOpremeZauzimaViseRedova == true)
                                            {
                                                this.ListaOblastiOpreme[i].DaLiTekstListeOblastiOpremeZauzimaViseRedova = false;
                                                tblckNazivDetaljnije.Visibility = Visibility.Hidden;
                                                tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckNazivDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                    //if (tbIzabranaUlogaKorisnika != null)
                                    //{
                                    //    TextBlock tblckIzabranaUlogaKorisnikaDetaljnije = gridListaKorisnika.FindName("tblckIzabranaUlogaKorisnikaDetaljnije") as TextBlock;
                                    //    int t = 65;
                                    //    //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                    //    if ((tbIzabranaUlogaKorisnika.ActualWidth + t) > gridListaKorisnika.Width)
                                    //    {

                                    //        this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = true;
                                    //        tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Visible;
                                    //        tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                    //    }
                                    //    else
                                    //    {
                                    //        if (this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova == true)
                                    //        {
                                    //            this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = false;
                                    //            tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Hidden;
                                    //            tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                    //            tblckIzabranaUlogaKorisnikaDetaljnije.Width = 0;
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }
                    }

                    break;
                case 2:

                    if (trivjuTipoviOpreme != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuTipoviOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojTipoviOpreme = trivjuTipoviOpreme.Items.Count;
                        if (trivjuTipoviOpreme.Items != null && brojTipoviOpreme > 0)
                        {
                            for (int i = 0; i < brojTipoviOpreme; i++)
                            {
                                TreeViewItem tviTipoviOpreme = trivjuTipoviOpreme.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviTipoviOpreme);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaTipoviOpreme = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaTipoviOpreme.Width = trivjuTipoviOpreme.ActualWidth - 20;
                                    else gridListaTipoviOpreme.Width = trivjuTipoviOpreme.ActualWidth;
                                    Border brdTriVjuHierarchicalNaziv = gridListaTipoviOpreme.FindName("brdTriVjuHierarchicalNazivTipaOpreme") as Border;
                                    brdTriVjuHierarchicalNaziv.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdTriVjuHierarchicalNaziv.Width = gridListaTipoviOpreme.Width - 30;
                                    TextBlock tbNaziv = gridListaTipoviOpreme.FindName("tbTriVjuHierarchicalNazivTipaOpreme") as TextBlock;
                                    TextBlock tbIzabranaOblastOpreme = gridListaTipoviOpreme.FindName("tbIzabranaOblastOpreme") as TextBlock;
                                    if (tbNaziv != null)
                                    {
                                        TextBlock tblckNazivDetaljnije = gridListaTipoviOpreme.FindName("tblckTriVjuHierarchicalNazivTipaOpremeDetaljnije") as TextBlock;
                                        int t = 60;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbNaziv.ActualWidth + t) > gridListaTipoviOpreme.Width)
                                        {

                                            this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova = true;
                                            tblckNazivDetaljnije.Visibility = Visibility.Visible;
                                            tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova == true)
                                            {
                                                this.ListaTipovaOpreme[i].DaLiTekstTipaOpremeZauzimaViseRedova = false;
                                                tblckNazivDetaljnije.Visibility = Visibility.Hidden;
                                                tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckNazivDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                    if (tbIzabranaOblastOpreme != null)
                                    {
                                        TextBlock tblckIzabranaOblastOpremeDetaljnije = gridListaTipoviOpreme.FindName("tblckIzabranaOblastOpremeDetaljnije") as TextBlock;
                                        int t = 65;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbIzabranaOblastOpreme.ActualWidth + t) > gridListaTipoviOpreme.Width)
                                        {

                                            this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova = true;
                                            tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Visible;
                                            tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova == true)
                                            {
                                                this.ListaTipovaOpreme[i].DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova = false;
                                                tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Hidden;
                                                tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckIzabranaOblastOpremeDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                case 3:
                    if (trivjuOprema != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuOprema);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojOpreme = trivjuOprema.Items.Count;
                        if (trivjuOprema.Items != null && brojOpreme > 0)
                        {
                            for (int i = 0; i < brojOpreme; i++)
                            {
                                TreeViewItem tviOprema = trivjuOprema.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviOprema);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaOpreme = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaOpreme.Width = trivjuOprema.ActualWidth - 20;
                                    else gridListaOpreme.Width = trivjuOprema.ActualWidth;
                                    Border brdTriVjuHierarchicalNaziv = gridListaOpreme.FindName("brdNazivITipOpremeOpreme") as Border;
                                    brdTriVjuHierarchicalNaziv.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdTriVjuHierarchicalNaziv.Width = gridListaOpreme.Width - 30;
                                    TextBlock tbNaziv = gridListaOpreme.FindName("tbNaslovOpreme") as TextBlock;
                                    TextBlock tbIzabranTipOpremeOpreme = gridListaOpreme.FindName("tbIzabranTipOpremeOpreme") as TextBlock;
                                    if (tbNaziv != null)
                                    {
                                        TextBlock tblckNazivDetaljnije = gridListaOpreme.FindName("tbNaslovOpremeDetaljnije") as TextBlock;
                                        int t = 60;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbNaziv.ActualWidth + t) > gridListaOpreme.Width)
                                        {

                                            this.ListaOpreme[i].DaLiTekstNaslovaZauzimaViseRedova = true;
                                            tblckNazivDetaljnije.Visibility = Visibility.Visible;
                                            tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaOpreme[i].DaLiTekstNaslovaZauzimaViseRedova == true)
                                            {
                                                this.ListaOpreme[i].DaLiTekstNaslovaZauzimaViseRedova = false;
                                                tblckNazivDetaljnije.Visibility = Visibility.Hidden;
                                                tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckNazivDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                    if (tbIzabranTipOpremeOpreme != null)
                                    {
                                        TextBlock tblckIzabranTipOpremeOpremeDetaljnije = gridListaOpreme.FindName("tblckIzabranTipOpremeOpremeDetaljnije") as TextBlock;
                                        int t = 65;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbIzabranTipOpremeOpreme.ActualWidth + t) > gridListaOpreme.Width)
                                        {

                                            this.ListaOpreme[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova = true;
                                            tblckIzabranTipOpremeOpremeDetaljnije.Visibility = Visibility.Visible;
                                            tblckIzabranTipOpremeOpremeDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaOpreme[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova == true)
                                            {
                                                this.ListaOpreme[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova = false;
                                                tblckIzabranTipOpremeOpremeDetaljnije.Visibility = Visibility.Hidden;
                                                tblckIzabranTipOpremeOpremeDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckIzabranTipOpremeOpremeDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case 4:

                    if (trivjuParametriIFilteri != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuParametriIFilteri);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojParametara = trivjuParametriIFilteri.Items.Count;
                        if (trivjuParametriIFilteri.Items != null && brojParametara > 0)
                        {
                            for (int i = 0; i < brojParametara; i++)
                            {
                                TreeViewItem tviParametri = trivjuParametriIFilteri.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviParametri);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaParametara = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaParametara.Width = trivjuParametriIFilteri.ActualWidth - 20;
                                    else gridListaParametara.Width = trivjuParametriIFilteri.ActualWidth;
                                    Border brdTriVjuHierarchicalNazivParametriIFilteri = gridListaParametara.FindName("brdNazivITipParametra") as Border;
                                    brdTriVjuHierarchicalNazivParametriIFilteri.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdTriVjuHierarchicalNazivParametriIFilteri.Width = gridListaParametara.Width;
                                    TextBlock tbNaziv = gridListaParametara.FindName("txtBoxNazivParametra") as TextBlock;
                                    TextBlock tbIzabranaOblastOpreme = gridListaParametara.FindName("tbIzabranTipOpremeZaParametar") as TextBlock;
                                    if (tbNaziv != null)
                                    {
                                        TextBlock tblckNazivDetaljnije = gridListaParametara.FindName("tblckImeIPrezimeKorisnikaDetaljnije") as TextBlock;
                                        int t = 25;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbNaziv.ActualWidth + t) > gridListaParametara.Width)
                                        {

                                            this.ListaParametara[i].DaLiTekstNazivaParametraZauzimaViseRedova = true;
                                            tblckNazivDetaljnije.Visibility = Visibility.Visible;
                                            tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaParametara[i].DaLiTekstNazivaParametraZauzimaViseRedova == true)
                                            {
                                                this.ListaParametara[i].DaLiTekstHederaNaslovaParametraZauzimaViseRedova = false;
                                                tblckNazivDetaljnije.Visibility = Visibility.Hidden;
                                                tblckNazivDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckNazivDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                    if (tbIzabranaOblastOpreme != null)
                                    {
                                        TextBlock tblckIzabranaOblastOpremeDetaljnije = gridListaParametara.FindName("tblckIzabranaUlogaKorisnikaDetaljnije") as TextBlock;
                                        int t = 35;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbIzabranaOblastOpreme.ActualWidth + t) > gridListaParametara.Width)
                                        {

                                            this.ListaParametara[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova = true;
                                            tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Visible;
                                            tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaParametara[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova == true)
                                            {
                                                this.ListaParametara[i].DaLiTekstIzabranogTipaOpremeZauzimaViseRedova = false;
                                                tblckIzabranaOblastOpremeDetaljnije.Visibility = Visibility.Hidden;
                                                tblckIzabranaOblastOpremeDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckIzabranaOblastOpremeDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case 5:

                    if (trivjuKorisnici != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuKorisnici);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojKorisnika = trivjuKorisnici.Items.Count;
                        if (trivjuKorisnici.Items != null && brojKorisnika > 0)
                        {
                            for (int i = 0; i < brojKorisnika; i++)
                            {
                                TreeViewItem tviKorisnici = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviKorisnici);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaKorisnika = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaKorisnika.Width = trivjuKorisnici.ActualWidth - 20;
                                    else gridListaKorisnika.Width = trivjuKorisnici.ActualWidth;
                                    Border brdNazivIUlogaKorisnika = gridListaKorisnika.FindName("brdNazivIUlogaKorisnika") as Border;
                                    brdNazivIUlogaKorisnika.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 30;
                                    TextBlock tbImeIPrezimeKorisnika = gridListaKorisnika.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                                    TextBlock tbIzabranaUlogaKorisnika = gridListaKorisnika.FindName("tbIzabranaUlogaKorisnika") as TextBlock;
                                    if (tbImeIPrezimeKorisnika != null)
                                    {
                                        TextBlock tblckImeIPrezimeKorisnikaDetaljnije = gridListaKorisnika.FindName("tblckImeIPrezimeKorisnikaDetaljnije") as TextBlock;
                                        int t = 60;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbImeIPrezimeKorisnika.ActualWidth + t) > gridListaKorisnika.Width)
                                        {

                                            this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova = true;
                                            tblckImeIPrezimeKorisnikaDetaljnije.Visibility = Visibility.Visible;
                                            tblckImeIPrezimeKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova == true)
                                            {
                                                this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova = false;
                                                tblckImeIPrezimeKorisnikaDetaljnije.Visibility = Visibility.Hidden;
                                                tblckImeIPrezimeKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckImeIPrezimeKorisnikaDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                    if (tbIzabranaUlogaKorisnika != null)
                                    {
                                        TextBlock tblckIzabranaUlogaKorisnikaDetaljnije = gridListaKorisnika.FindName("tblckIzabranaUlogaKorisnikaDetaljnije") as TextBlock;
                                        int t = 65;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbIzabranaUlogaKorisnika.ActualWidth + t) > gridListaKorisnika.Width)
                                        {

                                            this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = true;
                                            tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Visible;
                                            tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova == true)
                                            {
                                                this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = false;
                                                tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Hidden;
                                                tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                tblckIzabranaUlogaKorisnikaDetaljnije.Width = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case 6:
                    ControlTemplate ctp = cclListaKupacaIstorijeKupovine.Template as ControlTemplate;
                    TreeView trivjuKupci = ctp.FindName("trivjuKupci", cclListaKupacaIstorijeKupovine) as TreeView;
                    if (trivjuKupci != null)
                    {
                        //ScrollBarVisibility visibile = (ScrollBarVisibility)trivjuKorisnici.GetValue(ScrollViewer.VerticalScrollBarVisibilityProperty);
                        ScrollViewer sv = FindChild<ScrollViewer>(trivjuKupci);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        int brojKorisnika = trivjuKupci.Items.Count;
                        if (trivjuKupci.Items != null && brojKorisnika > 0)
                        {
                            for (int i = 0; i < brojKorisnika; i++)
                            {
                                TreeViewItem tviKorisnici = trivjuKupci.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                                //var sd = trivjuKorisnici.ItemContainerGenerator.ContainerFromIndex(i);
                                Grid gridPrvi = FindChild<Grid>(tviKorisnici);
                                if (gridPrvi != null)
                                {
                                    Border brdProba = gridPrvi.Children[0] as Border;
                                    ContentPresenter cp = brdProba.Child as ContentPresenter;
                                    Grid gridListaKorisnika = FindChild<Grid>(cp);
                                    if (scrollbarVisibility == Visibility.Visible) gridListaKorisnika.Width = trivjuKupci.ActualWidth - 20;
                                    else gridListaKorisnika.Width = trivjuKupci.ActualWidth;
                                    Border brdNazivIUlogaKorisnika = gridListaKorisnika.FindName("brdNazivIUlogaKorisnika") as Border;
                                    brdNazivIUlogaKorisnika.BeginAnimation(WidthProperty, null);



                                    //



                                    //if (visibile == ScrollBarVisibility.Visible) { brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 55; }
                                    brdNazivIUlogaKorisnika.Width = gridListaKorisnika.Width - 30;
                                    TextBlock tbImeIPrezimeKorisnika = gridListaKorisnika.FindName("tbImeIPrezimeKorisnika") as TextBlock;
                                    TextBlock tbIzabranaUlogaKorisnika = gridListaKorisnika.FindName("tbIzabranaUlogaKorisnika") as TextBlock;
                                    if (tbImeIPrezimeKorisnika != null)
                                    {
                                        TextBlock tblckImeIPrezimeKorisnikaDetaljnije = gridListaKorisnika.FindName("tblckImeIPrezimeKorisnikaDetaljnije") as TextBlock;
                                        int t = 60;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbImeIPrezimeKorisnika.ActualWidth + t) > gridListaKorisnika.Width)
                                        {

                                            this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova = true;
                                            tblckImeIPrezimeKorisnikaDetaljnije.Visibility = Visibility.Visible;
                                            tblckImeIPrezimeKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                        }
                                        else
                                        {
                                            if (this.ListaKorisnika != null && this.ListaKorisnika.Count > 0)
                                            {
                                                if (this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova == true)
                                                {
                                                    this.ListaKorisnika[i].DaLiTekstImenaIPrezimenaZauzimaViseRedova = false;
                                                    tblckImeIPrezimeKorisnikaDetaljnije.Visibility = Visibility.Hidden;
                                                    tblckImeIPrezimeKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                    tblckImeIPrezimeKorisnikaDetaljnije.Width = 0;
                                                }
                                            }
                                        }
                                    }
                                    if (tbIzabranaUlogaKorisnika != null)
                                    {
                                        TextBlock tblckIzabranaUlogaKorisnikaDetaljnije = gridListaKorisnika.FindName("tblckIzabranaUlogaKorisnikaDetaljnije") as TextBlock;
                                        int t = 65;
                                        //if (scrollbarVisibility == Visibility.Visible) { t = 85; }
                                        if ((tbIzabranaUlogaKorisnika.ActualWidth + t) > gridListaKorisnika.Width)
                                        {
                                            if (this.ListaKorisnika != null && this.ListaKorisnika.Count > 0)
                                            {
                                                this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = true;
                                                tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Visible;
                                                tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, daTriTackeZaDetalje);
                                            }
                                        }
                                        else
                                        {
                                            if (this.ListaKorisnika != null && this.ListaKorisnika.Count > 0)
                                            {
                                                if (this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova == true)
                                                {
                                                    this.ListaKorisnika[i].DaLiTekstIzabraneUlogeZauzimaViseRedova = false;
                                                    tblckIzabranaUlogaKorisnikaDetaljnije.Visibility = Visibility.Hidden;
                                                    tblckIzabranaUlogaKorisnikaDetaljnije.BeginAnimation(Label.WidthProperty, null);
                                                    tblckIzabranaUlogaKorisnikaDetaljnije.Width = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
            }

            if (kojeDugmeJePoReduKliknuto == 5)
            {

            }
        }

        private void trivjuKorisnici_SizeChanged(object sender, SizeChangedEventArgs e)
        {



            this.pravilnoRasporediListuKorisnika();



        }


        private void rbZenskiPol_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentKorisnici != null)
            {
                if (this.CurrentKorisnici.DaLiJeZenskiPolCekiran)
                {
                    this.CurrentKorisnici.DaLiJeZenskiPolCekiran = true;
                    this.CurrentKorisnici.DaLiJeMuskiPolCekiran = false;
                }
                else
                {
                    this.CurrentKorisnici.DaLiJeZenskiPolCekiran = false;
                    this.CurrentKorisnici.DaLiJeMuskiPolCekiran = true;
                }
            }
            else if (this.NoviKorisnik != null)
            {
                if (this.NoviKorisnik.DaLiJeMuskiPolCekiran)
                {
                    this.NoviKorisnik.DaLiJeZenskiPolCekiran = false;
                    this.NoviKorisnik.DaLiJeMuskiPolCekiran = true;
                }
                else
                {
                    this.NoviKorisnik.DaLiJeZenskiPolCekiran = true;
                    this.NoviKorisnik.DaLiJeMuskiPolCekiran = false;
                }
            }
            // Border brd1 = rb.Parent as Border;
            //RadioButton rb = (RadioButton)sender;

            //Grid grdParent1 = rb.Parent as Grid;
            //Border brd2 = grdParent1.Parent as Border;
            //Grid grdParent = brd2.Parent as Grid;
            //Label LabelId = FindChild<Label>(grdParent);
            //TextBlock tblck = FindChild<TextBlock>(grdParent);
            //int Id = Convert.ToInt32(LabelId.Content);


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;

        }

        private void rbMuskiPol_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentKorisnici != null)
            {
                if (this.CurrentKorisnici.DaLiJeMuskiPolCekiran)
                {
                    this.CurrentKorisnici.DaLiJeZenskiPolCekiran = false;
                    this.CurrentKorisnici.DaLiJeMuskiPolCekiran = true;
                }
                else
                {
                    this.CurrentKorisnici.DaLiJeZenskiPolCekiran = true;
                    this.CurrentKorisnici.DaLiJeMuskiPolCekiran = false;
                }
            }
            else if (this.NoviKorisnik != null)
            {
                if (this.NoviKorisnik.DaLiJeMuskiPolCekiran)
                {
                    this.NoviKorisnik.DaLiJeZenskiPolCekiran = false;
                    this.NoviKorisnik.DaLiJeMuskiPolCekiran = true;
                }
                else
                {
                    this.NoviKorisnik.DaLiJeZenskiPolCekiran = true;
                    this.NoviKorisnik.DaLiJeMuskiPolCekiran = false;
                }
            }

            //RadioButton rb = (RadioButton)sender;

            //Grid grdParent1 = rb.Parent as Grid;
            //Border brd2 = grdParent1.Parent as Border;
            //Grid grdParent = brd2.Parent as Grid;
            //Label LabelId = FindChild<Label>(grdParent);
            //TextBlock tblck = FindChild<TextBlock>(grdParent);
            //int Id = Convert.ToInt32(LabelId.Content);


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;


        }



        private void brdDesno_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brd = (Border)sender;
            Grid grdParent = brd.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);
            TextBox tb = FindChild<TextBox>(grdParent);
            ComboBox cmb = FindChild<ComboBox>(grdParent);
            Button btn = FindChild<Button>(grdParent);
            RadioButton rb = FindChild<RadioButton>(grdParent);






            if (tb != null)
            {
                string tempString = "";
                string trebaDaBude = "OpremaPripadaTipuOpreme";
                if (LabelId != null)
                    tempString = LabelId.Content.ToString();
                if (!tb.IsFocused || (tempString.CompareTo(trebaDaBude) == 0))
                    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }
            else if (rb != null)
            {
                if (!rb.IsFocused)
                {
                    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else if (cmb != null)
            {
                if (!cmb.IsFocused)
                {
                    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
            else if (btn != null)
            {
                if (!btn.IsFocused)
                {
                    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }

            ////Label LabelId = FindChild<Label>(grdParent);

            ////int Id = Convert.ToInt32(LabelId.Content);
            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {

            //    }
            //}
            //else
            //{
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}

        }

        private void brdDesno_MouseLeave(object sender, MouseEventArgs e)
        {

            Border brd = (Border)sender;
            Grid grdParent = brd.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);
            TextBox tb = FindChild<TextBox>(grdParent);
            Button btn = FindChild<Button>(grdParent);
            RadioButton rb = FindChild<RadioButton>(grdParent);
            ComboBox cmb = FindChild<ComboBox>(grdParent);
            if (tb != null)
            {
                string tempString = "";
                string trebaDaBude = "OpremaPripadaTipuOpreme";
                if (LabelId != null)
                    tempString = LabelId.Content.ToString();
                if (!tb.IsFocused || (tempString.CompareTo(trebaDaBude) == 0))
                    tblck.Foreground = Brushes.Black;

            }
            else if (rb != null)
            {
                int Id = Convert.ToInt32(LabelId.Content);
                if (trenutniGridKliknutoNaRBKorisnici != null)
                {
                    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
                    int trenutniId = Convert.ToInt32(trenutniLabel.Content);

                }
                else
                {
                    tblck.Foreground = Brushes.Black;
                }
            }
            else if (cmb != null)
            {
                if (!cmb.IsFocused)
                {
                    tblck.Foreground = Brushes.Black;
                }
            }
            else if (btn != null)
            {
                if (!btn.IsFocused)
                {
                    tblck.Foreground = Brushes.Black;
                }
            }

        }


        private int kojeTBjeKliknutoKorisnici = 0;
        private int kojeTBjeKliknutoRanijeKorisnici = 0;
        Grid trenutniGridKliknutoNaRBKorisnici = null;

        private void brdKorisnickoImeTxtBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TextBox tb = (TextBox)sender;

            //TextBlock tblckImeKorisnika = FindChild<TextBlock>(tb);

            //tblckImeKorisnika.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
        }

        private void TextBoxKorisniciDesno_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TextBox tb = (TextBox)sender;
            //Border brd = tb.Parent as Border;
            //Grid grdParent = brd.Parent as Grid;
            //Label LabelId = FindChild<Label>(grdParent);
            //TextBlock tblck = FindChild<TextBlock>(grdParent);
            //int Id = Convert.ToInt32(LabelId.Content);


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;
        }



        private void TextBoxAdminPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;



            Border brd = tb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);


            if (tb.IsFocused)
            {
                tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
                tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }
            //int Id = Convert.ToInt32(LabelId.Content);


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;
        }

        private void rbAdminPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            Grid grdParent1 = rb.Parent as Grid;
            Border brd2 = grdParent1.Parent as Border;
            Grid grdParent = brd2.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));


            int Id = Convert.ToInt32(LabelId.Content);


            if (trenutniGridKliknutoNaRBKorisnici != null)
            {
                Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
                int trenutniId = Convert.ToInt32(trenutniLabel.Content);
                if (Id != trenutniId)
                {
                    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
                    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

                    TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
                    Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                    Trenutnitblck.Foreground = Brushes.Black;
                }
            }
            else
            {
                tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
                tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            }
            trenutniGridKliknutoNaRBKorisnici = grdParent;
        }

        private void rbAdminPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            Grid grdParent1 = rb.Parent as Grid;
            Border brd2 = grdParent1.Parent as Border;
            Grid grdParent = brd2.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            tblck.Foreground = Brushes.Black;
            trenutniGridKliknutoNaRBKorisnici = null;
        }

        private void cmbBoxAdminPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Border brd = cb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            TextBlock tblck = FindChild<TextBlock>(grdParent);



            tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));



            //int Id = Convert.ToInt32(LabelId.Content);




            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;
        }

        private void cmbBoxAdminPanel_LostFocus(object sender, RoutedEventArgs e)
        {

            ComboBox cb = (ComboBox)sender;
            Border brd = cb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            tblck.Foreground = Brushes.Black;

        }

        private void chckBoxAdminPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Border brd = cb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            TextBlock tblck = FindChild<TextBlock>(grdParent);



            tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
        }

        private void chckBoxAdminPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Border brd = cb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            tblck.Foreground = Brushes.Black;
        }

        private void btnAdminPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Border brd1 = btn.Parent as Border;
            Grid grdParent1 = brd1.Parent as Grid;
            Border brd2 = grdParent1.Parent as Border;
            Grid grdParent = brd2.Parent as Grid;
            //Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //int Id = Convert.ToInt32(LabelId.Content);


            //if (trenutniGridKliknutoNaRBKorisnici != null)
            //{
            //    Label trenutniLabel = FindChild<Label>(trenutniGridKliknutoNaRBKorisnici);
            //    int trenutniId = Convert.ToInt32(trenutniLabel.Content);
            //    if (Id != trenutniId)
            //    {
            //        tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //        tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));

            //        TextBlock Trenutnitblck = FindChild<TextBlock>(trenutniGridKliknutoNaRBKorisnici);
            //        Trenutnitblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            //        Trenutnitblck.Foreground = Brushes.Black;
            //    }
            //}
            //else
            //{
            //    tblck.BeginAnimation(TextBlock.MarginProperty, taPomeniDesnoZa15);
            //    tblck.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
            //}
            //trenutniGridKliknutoNaRBKorisnici = grdParent;
        }

        private void btnAdminPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Border brd1 = btn.Parent as Border;
            Grid grdParent1 = brd1.Parent as Grid;
            Border brd2 = grdParent1.Parent as Border;
            Grid grdParent = brd2.Parent as Grid;
            TextBlock tblck = FindChild<TextBlock>(grdParent);

            tblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
            tblck.Foreground = Brushes.Black;
        }



        private void trivjuTipoviOpreme_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (kojeDugmeJePoReduKliknuto == 2)
            {
                pravilnoRasporediListuTipovaOpreme();
            }
        }





        private void TextBoxImeKorisnika_KeyUp(object sender, KeyEventArgs e)
        {
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("tboxPrezimeKorisnika") as TextBox;
            if (this.CurrentKorisnici != null)
                this.CurrentKorisnici.ImeIPrezimeKorisnika = (sender as TextBox).Text + " " + tboxPrezimeKorisnika.Text;
            pravilnoRasporediListuKorisnika();

            ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
            Label skrivenId = ctp.FindName("skrivenId", cclPrikazHederaImePrezimeKorisnici) as Label;

            if (skrivenId != null)
            {
                if (Convert.ToInt32(skrivenId.Content) == 1)
                {
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    daLiSePrekinuoTajmerKorisnici = true;

                    grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;


                    if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                    {
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                        }
                    }
                }
                else if (Convert.ToInt32(skrivenId.Content) == 2)
                {

                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;


                    grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 80;


                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }










                }
            }



        }

        private void TextBoxPrezimeKorisnika_KeyUp(object sender, KeyEventArgs e)
        {
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("tboxPrezimeKorisnika") as TextBox;
            TextBox tboxImeKorisnika = grdPrikazDetaljaKorisnici.FindName("tboxImeKorisnika") as TextBox;

            if (this.CurrentKorisnici != null)
                this.CurrentKorisnici.ImeIPrezimeKorisnika = tboxImeKorisnika.Text + " " + (sender as TextBox).Text;
            //else if(this.NoviKorisnik != null)
            //    this.NoviKorisnik.ImeIPrezimeKorisnika = tboxImeKorisnika.Text + " " + (sender as TextBox).Text;
            pravilnoRasporediListuKorisnika();

            ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
            Label skrivenId = ctp.FindName("skrivenId", cclPrikazHederaImePrezimeKorisnici) as Label;


            if (skrivenId != null)
            {
                if (Convert.ToInt32(skrivenId.Content) == 1)
                {
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    daLiSePrekinuoTajmerKorisnici = true;

                    grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;


                    if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                    {
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                        }
                    }
                }
                else if (Convert.ToInt32(skrivenId.Content) == 2)
                {

                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;


                    grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 80;


                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }










                }
            }
        }

        private void TextBoxAdminPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Border brd = tb.Parent as Border;
            Grid grdParent = brd.Parent as Grid;
            Label LabelId = FindChild<Label>(grdParent);
            TextBlock tblck = FindChild<TextBlock>(grdParent);
            if (!tb.IsFocused)
            {
                tblck.BeginAnimation(TextBlock.MarginProperty, taMarginaVratiUNormalu);
                tblck.Foreground = Brushes.Black;
            }

        }
        private bool sirirnaJeManja = true;
        private bool sirinaJeVeca = true;
        private double staraSirinaDesneStraneIstorijeKupovine = 0;
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double i = itcSveRezervacije.ActualWidth;
        }
        private bool daLiSePrekinuoTajmerKorisnici = true;
        private bool daLiSePrekinuoTajmerOblastiOpreme = true;

        private Visibility stariScrollbarVisibility;
        private void brdPrikazDetaljaKorisnici_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Grid grdPrikazDetaljaKorisnici;
            Grid grdPromenaVisine;
            Border brdKorisniciImeIPrezimeNaslov;
            ContentControl cclPrikazHederaImePrezimeKorisnici;
            TextBox tboxPrezimeKorisnika;

            switch (kojeDugmeJePoReduKliknuto)
            {

                case 1:
                    ctpKorisnici = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

                    grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
                    grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
                    brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                    cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
                    tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;
                    grdPromenaVisine.Height = brdOblastiOpremeCeoSadrzaj.ActualHeight / 20;



                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }



                    break;
                case 3:

                    grdPromenaVisineOprema.Height = brdOpremaCeoSadrzaj.ActualHeight / 20;



                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }



                    break;
                case 2:

                    grdPromenaVisineTipoviOpreme.Height = brdTipoviOpremeCeoSadrzaj.ActualHeight / 20;



                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }



                    break;
                case 4:

                    grdPromenaVisineParametriIFilteri.Height = brdParametriIFilteriCeoSadrzaj.ActualHeight / 20;



                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }



                    break;
                case 5:
                    ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;
                    grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                    grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
                    brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                    cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
                    tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;
                    grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;



                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }
                    break;
            }














            //Border brd = sender as Border;
            //ScrollViewer sv = FindChild<ScrollViewer>(brd);
            //Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

            //if (stariScrollbarVisibility != null)
            //{
            //    if (stariScrollbarVisibility != scrollbarVisibility)
            //    {
            //        if (scrollbarVisibility == Visibility.Visible)
            //        {
            //            brdImeIUlogaKorisnikaDetalji.Width = 480;
            //        }
            //        else
            //        {
            //            brdImeIUlogaKorisnikaDetalji.Width = 460;
            //        }
            //    }
            //}
            //else
            //{
            //    if (scrollbarVisibility == Visibility.Visible)
            //    {
            //        brdImeIUlogaKorisnikaDetalji.Width = 480;
            //    }
            //    else
            //    {
            //        brdImeIUlogaKorisnikaDetalji.Width = 460;
            //    }
            //}

            //stariScrollbarVisibility = scrollbarVisibility;



            //grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;
        }

        private void brdKorisniciImeIPrezimeNaslov_MouseEnter(object sender, MouseEventArgs e)
        {
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            brdKorisniciImeIPrezimeNaslov.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19000000"));
            ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
            Image slikaStrelicaHederImePrezimeKorisnika = ctp.FindName("slikaStrelicaHederImePrezimeKorisnika", cclPrikazHederaImePrezimeKorisnici) as Image;

            if (slikaStrelicaHederImePrezimeKorisnika != null)
            {
                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Visible;
                daLiJeMouseLeaveKorisniciHederImePrezime = false;
                daLiJeMouseEnterKorisniciHederImePrezime = true;
            }
        }

        private bool daLiJeMouseLeaveKorisniciHederImePrezime = false;
        private bool daLiJeMouseEnterKorisniciHederImePrezime = false;
        private void brdKorisniciImeIPrezimeNaslov_MouseLeave(object sender, MouseEventArgs e)
        {
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            brdKorisniciImeIPrezimeNaslov.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0C000000"));
            ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
            Image slikaStrelicaHederImePrezimeKorisnika = ctp.FindName("slikaStrelicaHederImePrezimeKorisnika", cclPrikazHederaImePrezimeKorisnici) as Image;

            if (slikaStrelicaHederImePrezimeKorisnika != null)
            {
                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Hidden;
                daLiJeMouseLeaveKorisniciHederImePrezime = true;
                daLiJeMouseEnterKorisniciHederImePrezime = false;
            }
        }

        private bool sacuvanaStaraVisina = false;
        private bool daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
        private DispatcherTimer timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu;
        private bool kliknutoJednom = false;

        private void brdKorisniciImeIPrezimeNaslov_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
            Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            if (!kliknutoJednom)
            {
                kliknutoJednom = true;
                //brdKorisniciImeIPrezimeNaslov.BeginAnimation(OpacityProperty, daSakrijPolaSekunde);
                DoubleAnimation daVisinaProba = new DoubleAnimation(brdKorisniciImeIPrezimeNaslov.ActualHeight, 40, new TimeSpan(0, 0, 1));

                ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
                Label skrivenId = ctp.FindName("skrivenId", cclPrikazHederaImePrezimeKorisnici) as Label;

                if (skrivenId != null)
                {
                    if (Convert.ToInt32(skrivenId.Content) == 1)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                        if (this.CurrentKorisnici != null)
                            this.CurrentKorisnici.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;
                        else if (this.NoviKorisnik != null)
                            this.NoviKorisnik.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;
                        daVisinaProba = new DoubleAnimation(brdKorisniciImeIPrezimeNaslov.ActualHeight, 45, new TimeSpan(0, 0, 0, 0, 500));


                        grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 80;
                        if (this.CurrentKorisnici != null)
                            cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpHederImePrezimeKorisniciKliknuto");
                        //else if (this.NoviKorisnik != null)
                        //    cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosHederImePrezimeKorisniciKliknuto");
                        brdKorisniciImeIPrezimeNaslov.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        if (daLiSePrekinuoTajmerKorisnici)
                        {
                            daLiSePrekinuoTajmerKorisnici = false;
                            if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                            }
                            if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                                timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                            }
                        }
                    }
                    else if (Convert.ToInt32(skrivenId.Content) == 2)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                            timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilno = null;
                        daLiSePrekinuoTajmerKorisnici = true;

                        grdPromenaVisine.Height = brdKorisniciCeoSadrzaj.ActualHeight / 20;
                        if (this.CurrentKorisnici != null)
                        {
                            if (this.CurrentKorisnici.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdKorisniciImeIPrezimeNaslov.ActualHeight, this.CurrentKorisnici.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdKorisniciImeIPrezimeNaslov.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpHederImePrezimeKorisnici");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpHederImePrezimeKorisnici");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }
                        else if (this.NoviKorisnik != null)
                        {
                            if (this.NoviKorisnik.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdKorisniciImeIPrezimeNaslov.ActualHeight, this.NoviKorisnik.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdKorisniciImeIPrezimeNaslov.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosHederImePrezimeKorisnici");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclPrikazHederaImePrezimeKorisnici.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosHederImePrezimeKorisnici");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }

                    }
                }









            }

        }

        void timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick(object sender, EventArgs e)
        {
            switch (kojeDugmeJePoReduKliknuto)
            {
                case 1:
                    ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

                    Grid grdPrikazDetaljaOblastiOpreme = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
                    Grid grdPromenaVisineOblastiOpreme = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
                    Border brdNazivHeder = grdPrikazDetaljaOblastiOpreme.FindName("brdNazivHeder") as Border;
                    ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
                    //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null && timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdNazivHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaOblastiOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclPrikazHederaOblastiOpreme) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclPrikazHederaOblastiOpreme) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclPrikazHederaOblastiOpreme) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclPrikazHederaOblastiOpreme) as TextBlock;

                        if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 135)
                            {
                                this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;

                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        else if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentOblastiOpreme != null)
                                    this.CurrentOblastiOpreme.DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }

                        if (brdNaslovDetalji != null)
                        {

                            if (scrollbarVisibility == Visibility.Visible)
                            {


                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);

                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;
                            }
                            stariScrollbarVisibility = scrollbarVisibility;

                        }

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime && daLiJeMouseEnterKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        else if (!daLiJeMouseEnterKorisniciHederImePrezime && daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }



                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;


                    break;

                case 2:


                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null && timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaTipoviOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclTipoviOpremePrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclTipoviOpremePrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclTipoviOpremePrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclTipoviOpremePrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclTipoviOpremePrikazDetaljaHeder) as TextBlock;

                        if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 135)
                            {
                                this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;

                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        else if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentTipoviOpreme != null)
                                    this.CurrentTipoviOpreme.DaLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }


                        if (brdNaslovDetalji != null)
                        {

                            if (scrollbarVisibility == Visibility.Visible)
                            {


                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);

                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;
                            }
                            stariScrollbarVisibility = scrollbarVisibility;

                        }

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime && daLiJeMouseEnterKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        else if (!daLiJeMouseEnterKorisniciHederImePrezime && daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }



                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;


                    break;
                case 3:


                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null && timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdOpremaPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaOpreme);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclOpremaPrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclOpremaPrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclOpremaPrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclOpremaPrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclOpremaPrikazDetaljaHeder) as TextBlock;

                        if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 135)
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = false;

                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        else if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentOprema != null)
                                    this.CurrentOprema.DaLiTekstNaslovaZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }


                        if (brdNaslovDetalji != null)
                        {

                            if (scrollbarVisibility == Visibility.Visible)
                            {


                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);

                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;
                            }
                            stariScrollbarVisibility = scrollbarVisibility;

                        }

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime && daLiJeMouseEnterKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        else if (!daLiJeMouseEnterKorisniciHederImePrezime && daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }



                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;


                    break;
                case 4:


                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null && timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdParametriIFilteriPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaParametriIFilteri);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;
                        Grid brdNaslovDetalji = ctp.FindName("brdNaslovDetalji", cclParametriIFilteriPrikazDetaljaHeder) as Grid;
                        Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclParametriIFilteriPrikazDetaljaHeder) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclParametriIFilteriPrikazDetaljaHeder) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckNaslov = ctp.FindName("tblckNaslov", cclParametriIFilteriPrikazDetaljaHeder) as TextBlock;
                        TextBlock tblckNaslovDetaljnijeHeder = ctp.FindName("tblckNaslovDetaljnijeHeder", cclParametriIFilteriPrikazDetaljaHeder) as TextBlock;

                        if (tblckNaslov != null && id != 0 && id == 1)
                        {
                            if (tblckNaslov.ActualHeight > 135)
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 30;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = false;

                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Height = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        else if (tblckNaslov != null && id != 0 && id == 2)
                        {
                            if (tblckNaslov.ActualHeight > 30)
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = true;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 15;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }

                            else
                            {
                                if (this.CurrentParametri != null)
                                    this.CurrentParametri.DaLiTekstHederaNaslovaParametraZauzimaViseRedova = false;
                                if (tblckNaslovDetaljnijeHeder != null)
                                {
                                    tblckNaslovDetaljnijeHeder.Width = 0;
                                    tblckNaslovDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }


                        }


                        if (brdNaslovDetalji != null)
                        {

                            if (scrollbarVisibility == Visibility.Visible)
                            {


                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                                if (brdNaslovDetalji.Margin != thicknessProba)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);

                                if (brdNaslovDetalji.Margin != marginaCentar)
                                    brdNaslovDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;
                            }
                            stariScrollbarVisibility = scrollbarVisibility;

                        }

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime && daLiJeMouseEnterKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                            }
                        }
                        else if (!daLiJeMouseEnterKorisniciHederImePrezime && daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederNaziv != null)
                            {
                                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                            }
                        }



                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;


                    break;
                case 5:



                    ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

                    Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                    Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
                    Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                    ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
                    TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null && timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                    {
                        DispatcherTimer timerSender = sender as DispatcherTimer;
                        timerSender.Stop();



                        brdKorisniciImeIPrezimeNaslov.BeginAnimation(Border.HeightProperty, null);

                        ScrollViewer sv = FindChild<ScrollViewer>(grdPrikazDetaljaKorisnici);
                        Visibility scrollbarVisibility = sv.ComputedVerticalScrollBarVisibility;

                        ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;
                        Grid brdImeIUlogaKorisnikaDetalji = ctp.FindName("brdImeIUlogaKorisnikaDetalji", cclPrikazHederaImePrezimeKorisnici) as Grid;
                        Image slikaStrelicaHederImePrezimeKorisnika = ctp.FindName("slikaStrelicaHederImePrezimeKorisnika", cclPrikazHederaImePrezimeKorisnici) as Image;
                        int id = 0;
                        Label lblSkrivenId = ctp.FindName("skrivenId", cclPrikazHederaImePrezimeKorisnici) as Label;
                        if (lblSkrivenId != null)
                            id = Convert.ToInt32(lblSkrivenId.Content);

                        TextBlock tblckImePrezime = ctp.FindName("tblckImePrezime", cclPrikazHederaImePrezimeKorisnici) as TextBlock;
                        TextBlock tblckImeIPrezimeKorisnikaDetaljnijeHeder = ctp.FindName("tblckImeIPrezimeKorisnikaDetaljnijeHeder", cclPrikazHederaImePrezimeKorisnici) as TextBlock;

                        if (tblckImePrezime != null && id != 0 && id == 1)
                        {
                            if (tblckImePrezime.ActualHeight > 135)
                            {
                                this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = true;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 30;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                if (this.CurrentKorisnici != null)
                                    this.CurrentKorisnici.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;
                                else if (this.NoviKorisnik != null)
                                    this.NoviKorisnik.DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;
                                if (tblckImeIPrezimeKorisnikaDetaljnijeHeder != null)
                                {
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Height = 0;
                                    tblckImeIPrezimeKorisnikaDetaljnijeHeder.Visibility = Visibility.Hidden;
                                }
                            }
                        }


                        if (brdImeIUlogaKorisnikaDetalji != null)
                        {

                            if (scrollbarVisibility == Visibility.Visible)
                            {


                                //if (brdImeIUlogaKorisnikaDetalji.Width != 480)
                                //    brdDugmiciDole.Width = 480;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 480, trajanjePolaSekunde);
                                //brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                                if (brdImeIUlogaKorisnikaDetalji.Margin != thicknessProba)
                                    brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba);
                            }
                            else if (scrollbarVisibility == Visibility.Collapsed)
                            {
                                //if (brdImeIUlogaKorisnikaDetalji.Width != 460)
                                //    brdDugmiciDole.Width = 460;
                                //DoubleAnimation daProba1 = new DoubleAnimation(brdImeIUlogaKorisnikaDetalji.Width, 460, trajanjePolaSekunde);

                                if (brdImeIUlogaKorisnikaDetalji.Margin != marginaCentar)
                                    brdImeIUlogaKorisnikaDetalji.BeginAnimation(MarginProperty, taProba2);
                                //brdImeIUlogaKorisnikaDetalji.Width = 460;
                            }
                            stariScrollbarVisibility = scrollbarVisibility;

                        }

                        if (!daLiJeMouseLeaveKorisniciHederImePrezime && daLiJeMouseEnterKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederImePrezimeKorisnika != null)
                            {
                                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Visible;
                            }
                        }
                        else if (!daLiJeMouseEnterKorisniciHederImePrezime && daLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            if (slikaStrelicaHederImePrezimeKorisnika != null)
                            {
                                slikaStrelicaHederImePrezimeKorisnika.Visibility = Visibility.Hidden;
                            }
                        }



                        if (daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime)
                        {
                            daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = false;
                            if (timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja == null)
                            {

                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja = new DispatcherTimer();
                            }
                            if (!timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.IsEnabled)
                            {
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Tick += timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick;
                                timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja.Start();
                            }
                        }


                    }
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();

                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;

                    break;
            }
        }

        private void brdNazivHeder_MouseEnter(object sender, MouseEventArgs e)
        {
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            Grid grdPromenaVisine = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
            Border brdNazivHeder = grdPrikazDetaljaKorisnici.FindName("brdNazivHeder") as Border;
            ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as TextBox;

            brdNazivHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19000000"));
            ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                daLiJeMouseLeaveKorisniciHederImePrezime = false;
                daLiJeMouseEnterKorisniciHederImePrezime = true;
            }
        }

        private void brdNazivHeder_MouseLeave(object sender, MouseEventArgs e)
        {
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            Grid grdPromenaVisine = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
            Border brdNazivHeder = grdPrikazDetaljaKorisnici.FindName("brdNazivHeder") as Border;
            ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as TextBox;

            brdNazivHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0C000000"));
            ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclPrikazHederaOblastiOpreme) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                daLiJeMouseLeaveKorisniciHederImePrezime = true;
                daLiJeMouseEnterKorisniciHederImePrezime = false;
            }
        }

        private void brdNazivHeder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

            Grid grdPrikazDetaljaKorisnici = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            Grid grdPromenaVisine = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
            Border brdNazivHeder = grdPrikazDetaljaKorisnici.FindName("brdNazivHeder") as Border;
            ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
            ControlTemplate cclPrikazHederaOblastiOpremeTemplate = cclPrikazHederaOblastiOpreme.Template as ControlTemplate;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaOblastiOpreme") as TextBox;

            if (!kliknutoJednom)
            {
                kliknutoJednom = true;
                //brdKorisniciImeIPrezimeNaslov.BeginAnimation(OpacityProperty, daSakrijPolaSekunde);
                DoubleAnimation daVisinaProba = new DoubleAnimation(brdNazivHeder.ActualHeight, 45, new TimeSpan(0, 0, 1));

                //ControlTemplate ctp = cclPrikazHederaImePrezimeKorisnici.Template;

                Label skrivenId = cclPrikazHederaOblastiOpremeTemplate.FindName("skrivenId", cclPrikazHederaOblastiOpreme) as Label;

                if (skrivenId != null)
                {
                    if (Convert.ToInt32(skrivenId.Content) == 1)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                        if (this.CurrentOblastiOpreme != null)
                            this.CurrentOblastiOpreme.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;

                        daVisinaProba = new DoubleAnimation(brdNazivHeder.ActualHeight, 40, new TimeSpan(0, 0, 0, 0, 500));


                        grdPromenaVisine.Height = brdOblastiOpremeCeoSadrzaj.ActualHeight / 80;
                        if (this.CurrentOblastiOpreme != null)
                            cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpremeKliknuto");
                        //else if (this.NoviKorisnik != null)
                        //    cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");
                        brdNazivHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        if (daLiSePrekinuoTajmerKorisnici)
                        {
                            daLiSePrekinuoTajmerKorisnici = false;
                            if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                            }
                            if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                                timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                            }
                        }
                    }
                    else if (Convert.ToInt32(skrivenId.Content) == 2)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                            timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilno = null;
                        daLiSePrekinuoTajmerKorisnici = true;

                        grdPromenaVisine.Height = brdOblastiOpremeCeoSadrzaj.ActualHeight / 20;
                        if (this.CurrentOblastiOpreme != null)
                        {
                            if (this.CurrentOblastiOpreme.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdNazivHeder.ActualHeight, this.CurrentOblastiOpreme.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdNazivHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }
                        //else if (this.NoviKorisnik != null)
                        //{
                        //    if (this.NoviKorisnik.VisinaHederaImeIPrezimeKorisnici != 0)
                        //    {
                        //        daVisinaProba = new DoubleAnimation(brdNazivHeder.ActualHeight, this.NoviKorisnik.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                        //        brdNazivHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        //        cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosHederImePrezimeKorisnici");



                        //        if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                        //        {
                        //            daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                        //            if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                        //            {
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                        //            }
                        //            if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                        //            {
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                        //            }
                        //        }


                        //    }
                        //    else
                        //    {
                        //        cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpUnosHederImePrezimeKorisnici");
                        //        if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                        //        {
                        //            daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                        //            if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                        //            {
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                        //            }
                        //            if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                        //            {
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                        //                timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                        //            }
                        //        }
                        //    }
                        //}

                    }
                }









            }
        }

        private void tboxNaziv_KeyUp(object sender, KeyEventArgs e)
        {
            ControlTemplate ctpOblastiOpreme = cclPrikazDetaljaOblastiOpreme.Template as ControlTemplate;

            Grid grdPrikazDetaljaOblastiOpreme = ctpOblastiOpreme.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaOblastiOpreme) as Grid;
            Grid grdPromenaVisine = ctpOblastiOpreme.FindName("grdPromenaVisine", cclPrikazDetaljaOblastiOpreme) as Grid;
            Border brdNazivHeder = grdPrikazDetaljaOblastiOpreme.FindName("brdNazivHeder") as Border;
            ContentControl cclPrikazHederaOblastiOpreme = grdPrikazDetaljaOblastiOpreme.FindName("cclPrikazHederaOblastiOpreme") as ContentControl;
            TextBox tboxPrezimeKorisnika = grdPrikazDetaljaOblastiOpreme.FindName("tboxPrezimeKorisnika") as TextBox;
            if (this.CurrentOblastiOpreme != null)
                this.CurrentOblastiOpreme.Name = (sender as TextBox).Text;
            pravilnoRasporediListuKorisnika();

            ControlTemplate ctp = cclPrikazHederaOblastiOpreme.Template;
            Label skrivenId = ctp.FindName("skrivenId", cclPrikazHederaOblastiOpreme) as Label;

            if (skrivenId != null)
            {
                if (Convert.ToInt32(skrivenId.Content) == 1)
                {
                    if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                        timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilno = null;
                    daLiSePrekinuoTajmerKorisnici = true;

                    grdPromenaVisine.Height = brdOblastiOpremeCeoSadrzaj.ActualHeight / 20;


                    if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                    {
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                        }
                    }
                }
                else if (Convert.ToInt32(skrivenId.Content) == 2)
                {

                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                    timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;


                    grdPromenaVisine.Height = brdOblastiOpremeCeoSadrzaj.ActualHeight / 80;


                    if (daLiSePrekinuoTajmerKorisnici)
                    {
                        daLiSePrekinuoTajmerKorisnici = false;
                        if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                        }
                        if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                        {
                            timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 40);
                            timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                            timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                        }
                    }










                }
            }
        }

        private void brdTipoviOpremePrikazDetaljaHeder_MouseEnter(object sender, MouseEventArgs e)
        {




            //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaTipoviOpreme.FindName("cclPrikazHederaOblastiOpreme") as TextBox;

            brdTipoviOpremePrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19000000"));
            ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclTipoviOpremePrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                daLiJeMouseLeaveKorisniciHederImePrezime = false;
                daLiJeMouseEnterKorisniciHederImePrezime = true;
            }



        }

        private void brdTipoviOpremePrikazDetaljaHeder_MouseLeave(object sender, MouseEventArgs e)
        {
            brdTipoviOpremePrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0C000000"));
            ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclTipoviOpremePrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                daLiJeMouseLeaveKorisniciHederImePrezime = true;
                daLiJeMouseEnterKorisniciHederImePrezime = false;
            }
        }

        private void brdTipoviOpremePrikazDetaljaHeder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            if (!kliknutoJednom)
            {
                kliknutoJednom = true;
                //brdKorisniciImeIPrezimeNaslov.BeginAnimation(OpacityProperty, daSakrijPolaSekunde);
                DoubleAnimation daVisinaProba = new DoubleAnimation(brdTipoviOpremePrikazDetaljaHeder.ActualHeight, 40, new TimeSpan(0, 0, 1));

                ControlTemplate ctp = cclTipoviOpremePrikazDetaljaHeder.Template;

                Label skrivenId = ctp.FindName("skrivenId", cclTipoviOpremePrikazDetaljaHeder) as Label;

                if (skrivenId != null)
                {
                    if (Convert.ToInt32(skrivenId.Content) == 1)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                        if (this.CurrentTipoviOpreme != null)
                            this.CurrentTipoviOpreme.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;

                        daVisinaProba = new DoubleAnimation(brdTipoviOpremePrikazDetaljaHeder.ActualHeight, 45, new TimeSpan(0, 0, 0, 0, 500));
                        brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        grdPromenaVisineTipoviOpreme.Height = brdTipoviOpremeCeoSadrzaj.ActualHeight / 80;
                        if (this.CurrentTipoviOpreme != null)
                            cclTipoviOpremePrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpTipoviOpremePrikazDetaljaHederKliknuto");
                        //else if (this.NoviKorisnik != null)
                        //    cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");
                        brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        if (daLiSePrekinuoTajmerKorisnici)
                        {
                            daLiSePrekinuoTajmerKorisnici = false;
                            if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                            }
                            if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                                timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                            }
                        }
                    }
                    else if (Convert.ToInt32(skrivenId.Content) == 2)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                            timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilno = null;
                        daLiSePrekinuoTajmerKorisnici = true;

                        grdPromenaVisineTipoviOpreme.Height = brdTipoviOpremeCeoSadrzaj.ActualHeight / 20;
                        if (this.CurrentTipoviOpreme != null)
                        {
                            if (this.CurrentTipoviOpreme.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdTipoviOpremePrikazDetaljaHeder.ActualHeight, this.CurrentTipoviOpreme.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclTipoviOpremePrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpTipoviOpremePrikazDetaljaHeder");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclTipoviOpremePrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpTipoviOpremePrikazDetaljaHeder");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }


                    }
                }









            }
        }

        private void brdParametriIFilteriPrikazDetaljaHeder_MouseEnter(object sender, MouseEventArgs e)
        {
            brdParametriIFilteriPrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19000000"));
            ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclParametriIFilteriPrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                daLiJeMouseLeaveKorisniciHederImePrezime = false;
                daLiJeMouseEnterKorisniciHederImePrezime = true;
            }
        }

        private void brdParametriIFilteriPrikazDetaljaHeder_MouseLeave(object sender, MouseEventArgs e)
        {
            brdParametriIFilteriPrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0C000000"));
            ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclParametriIFilteriPrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                daLiJeMouseLeaveKorisniciHederImePrezime = true;
                daLiJeMouseEnterKorisniciHederImePrezime = false;
            }
        }

        private void brdParametriIFilteriPrikazDetaljaHeder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!kliknutoJednom)
            {
                kliknutoJednom = true;
                //brdKorisniciImeIPrezimeNaslov.BeginAnimation(OpacityProperty, daSakrijPolaSekunde);
                DoubleAnimation daVisinaProba = new DoubleAnimation(brdParametriIFilteriPrikazDetaljaHeder.ActualHeight, 40, new TimeSpan(0, 0, 1));

                ControlTemplate ctp = cclParametriIFilteriPrikazDetaljaHeder.Template;

                Label skrivenId = ctp.FindName("skrivenId", cclParametriIFilteriPrikazDetaljaHeder) as Label;

                if (skrivenId != null)
                {
                    if (Convert.ToInt32(skrivenId.Content) == 1)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                        if (this.CurrentParametri != null)
                            this.CurrentParametri.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;

                        daVisinaProba = new DoubleAnimation(brdParametriIFilteriPrikazDetaljaHeder.ActualHeight, 45, new TimeSpan(0, 0, 0, 0, 500));
                        brdTipoviOpremePrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        grdPromenaVisineParametriIFilteri.Height = brdParametriIFilteriCeoSadrzaj.ActualHeight / 80;
                        if (this.CurrentParametri != null)
                            cclParametriIFilteriPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpParametriIFilteriPrikazDetaljaHederKliknuto");
                        //else if (this.NoviKorisnik != null)
                        //    cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");
                        brdParametriIFilteriPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        if (daLiSePrekinuoTajmerKorisnici)
                        {
                            daLiSePrekinuoTajmerKorisnici = false;
                            if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                            }
                            if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                                timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                            }
                        }
                    }
                    else if (Convert.ToInt32(skrivenId.Content) == 2)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                            timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilno = null;
                        daLiSePrekinuoTajmerKorisnici = true;

                        grdPromenaVisineParametriIFilteri.Height = brdParametriIFilteriCeoSadrzaj.ActualHeight / 20;
                        if (this.CurrentParametri != null)
                        {
                            if (this.CurrentParametri.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdParametriIFilteriPrikazDetaljaHeder.ActualHeight, this.CurrentParametri.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdParametriIFilteriPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclParametriIFilteriPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpParametriIFilteriPrikazDetaljaHeder");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclParametriIFilteriPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpParametriIFilteriPrikazDetaljaHeder");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }


                    }
                }









            }
        }

        private void brdOpremaPrikazDetaljaHeder_MouseEnter(object sender, MouseEventArgs e)
        {
            brdOpremaPrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19000000"));
            ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclOpremaPrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Visible;
                daLiJeMouseLeaveKorisniciHederImePrezime = false;
                daLiJeMouseEnterKorisniciHederImePrezime = true;
            }







        }

        private void brdOpremaPrikazDetaljaHeder_MouseLeave(object sender, MouseEventArgs e)
        {

            brdOpremaPrikazDetaljaHeder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0C000000"));
            ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;
            Image slikaStrelicaHederNaziv = ctp.FindName("slikaStrelicaHederNaziv", cclOpremaPrikazDetaljaHeder) as Image;

            if (slikaStrelicaHederNaziv != null)
            {
                slikaStrelicaHederNaziv.Visibility = Visibility.Hidden;
                daLiJeMouseLeaveKorisniciHederImePrezime = true;
                daLiJeMouseEnterKorisniciHederImePrezime = false;
            }

        }

        private void brdOpremaPrikazDetaljaHeder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!kliknutoJednom)
            {
                kliknutoJednom = true;
                //brdKorisniciImeIPrezimeNaslov.BeginAnimation(OpacityProperty, daSakrijPolaSekunde);
                DoubleAnimation daVisinaProba = new DoubleAnimation(brdOpremaPrikazDetaljaHeder.ActualHeight, 40, new TimeSpan(0, 0, 1));

                ControlTemplate ctp = cclOpremaPrikazDetaljaHeder.Template;

                Label skrivenId = ctp.FindName("skrivenId", cclOpremaPrikazDetaljaHeder) as Label;

                if (skrivenId != null)
                {
                    if (Convert.ToInt32(skrivenId.Content) == 1)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu != null)
                            timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = null;
                        daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = true;
                        if (this.CurrentOprema != null)
                            this.CurrentOprema.VisinaHederaImeIPrezimeKorisnici = (sender as Border).ActualHeight;

                        daVisinaProba = new DoubleAnimation(brdOpremaPrikazDetaljaHeder.ActualHeight, 45, new TimeSpan(0, 0, 0, 0, 500));
                        brdOpremaPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        grdPromenaVisineOprema.Height = brdOpremaCeoSadrzaj.ActualHeight / 80;
                        if (this.CurrentOprema != null)
                            cclOpremaPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpOpremaPrikazDetaljaHederKliknuto");
                        //else if (this.NoviKorisnik != null)
                        //    cclPrikazHederaOblastiOpreme.SetResourceReference(ContentControl.TemplateProperty, "ctpHederOblastiOpreme");
                        brdOpremaPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                        if (daLiSePrekinuoTajmerKorisnici)
                        {
                            daLiSePrekinuoTajmerKorisnici = false;
                            if (timerKorisniciRasporediPrikazDetaljaPravilno == null)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno = new DispatcherTimer();
                            }
                            if (!timerKorisniciRasporediPrikazDetaljaPravilno.IsEnabled)
                            {
                                timerKorisniciRasporediPrikazDetaljaPravilno.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                timerKorisniciRasporediPrikazDetaljaPravilno.Tick += timerKorisniciRasporediPrikazDetaljaPravilno_Tick;
                                timerKorisniciRasporediPrikazDetaljaPravilno.Start();
                            }
                        }
                    }
                    else if (Convert.ToInt32(skrivenId.Content) == 2)
                    {
                        if (timerKorisniciRasporediPrikazDetaljaPravilno != null)
                            timerKorisniciRasporediPrikazDetaljaPravilno.Stop();
                        timerKorisniciRasporediPrikazDetaljaPravilno = null;
                        daLiSePrekinuoTajmerKorisnici = true;

                        grdPromenaVisineOprema.Height = brdOpremaCeoSadrzaj.ActualHeight / 20;
                        if (this.CurrentOprema != null)
                        {
                            if (this.CurrentOprema.VisinaHederaImeIPrezimeKorisnici != 0)
                            {
                                daVisinaProba = new DoubleAnimation(brdOpremaPrikazDetaljaHeder.ActualHeight, this.CurrentOprema.VisinaHederaImeIPrezimeKorisnici, new TimeSpan(0, 0, 0, 0, 500));
                                brdOpremaPrikazDetaljaHeder.BeginAnimation(Border.HeightProperty, daVisinaProba);

                                cclOpremaPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpOpremaPrikazDetaljaHeder");



                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 500);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }


                            }
                            else
                            {
                                cclOpremaPrikazDetaljaHeder.SetResourceReference(ContentControl.TemplateProperty, "ctpOpremaPrikazDetaljaHeder");
                                if (daLiSePrekinuoTajmerKorisniciHederVratiUNormalu)
                                {
                                    daLiSePrekinuoTajmerKorisniciHederVratiUNormalu = false;
                                    if (timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu == null)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu = new DispatcherTimer();
                                    }
                                    if (!timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.IsEnabled)
                                    {
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Interval = new TimeSpan(0, 0, 0, 0, 40);
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Tick += timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick;
                                        timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu.Start();
                                    }
                                }
                            }
                        }


                    }
                }









            }
        }

        private void btnUnesiNoviParametar_Click(object sender, RoutedEventArgs e)
        {
            noviParametar_Click();
        }

        private void scrolVjuverRezervacije_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            ControlTemplate ctp = cclUpravljanjeNarudzbinama.Template as ControlTemplate;
            Border brdHederNarudzbine = ctp.FindName("brdHederNarudzbine", cclUpravljanjeNarudzbinama) as Border;
            if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Collapsed)
                brdHederNarudzbine.Margin = new Thickness(0);
            else if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible)
                brdHederNarudzbine.Margin = new Thickness(-20, 0, 0, 0);
        }
        Grid tempGridIstorijaKupovine;
        private void grdKlasicanPrikazIstorijeKupovine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TempIstorijaKupovine = new IstorijaKupovine();
            if (tempGridIstorijaKupovine != null)
            {
                TextBlock tbTempIstorijaKupovineSkrivenId = tempGridIstorijaKupovine.FindName("tbIstorijaKupovineSkrivenId") as TextBlock;
                int idIstorijaKupovineTempGrida = Convert.ToInt32(tbTempIstorijaKupovineSkrivenId.Text);

                for (int i = 0; i < ListaIstorijeKupovine.Count; i++)
                {

                    if (ListaIstorijeKupovine[i].IdIstorijaKupovine == idIstorijaKupovineTempGrida)
                    {
                        TempIstorijaKupovine = ListaIstorijeKupovine[i];
                        CurrentKupljenaOprema = ListaIstorijeKupovine[i].ListaKupljeneOpreme;
                        break;
                    }
                }


                //Border border5 = tempGridIstorijaKupovine.FindName("probaBorder") as Border;
                Border border6 = tempGridIstorijaKupovine.FindName("probaBorder1") as Border;
                Border border7 = tempGridIstorijaKupovine.FindName("probaBorder2") as Border;
                Border border8 = tempGridIstorijaKupovine.FindName("probaBorder3") as Border;
                TextBlock tblImeTemp = tempGridIstorijaKupovine.FindName("tblIme") as TextBlock;
                TextBlock tblDatumProdajeTemp = tempGridIstorijaKupovine.FindName("tblDatumProdaje") as TextBlock;
                TextBlock tblProdavacTemp = tempGridIstorijaKupovine.FindName("tblProdavac") as TextBlock;
                TextBlock tblUkupnaCenaKupovineTemp = tempGridIstorijaKupovine.FindName("tblUkupnaCenaKupovine") as TextBlock;

                //border5.Background = Brushes.Transparent;
                border6.Background = Brushes.Transparent;
                border7.Background = Brushes.Transparent;
                border8.Background = Brushes.Transparent;

                //tblImeTemp.Foreground = Brushes.Black;
                tblDatumProdajeTemp.Foreground = Brushes.Black;
                tblProdavacTemp.Foreground = Brushes.Black;
                tblUkupnaCenaKupovineTemp.Foreground = Brushes.Black;

                CurrentIstorijaKupovineKlasicanPrikaz.KliknutoNaGrid = false;
                TempIstorijaKupovine.KliknutoNaGrid = false;
            }



            Grid gridPrikazRezervacija = sender as Grid;

            TextBlock tbIstorijaKupovineSkrivenId = gridPrikazRezervacija.FindName("tbIstorijaKupovineSkrivenId") as TextBlock;
            int idIstorijaKupovine = Convert.ToInt32(tbIstorijaKupovineSkrivenId.Text);

            //Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
            Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
            Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
            Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border;
            TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
            TextBlock tblDatumProdaje = gridPrikazRezervacija.FindName("tblDatumProdaje") as TextBlock;
            TextBlock tblProdavac = gridPrikazRezervacija.FindName("tblProdavac") as TextBlock;
            TextBlock tblUkupnaCenaKupovine = gridPrikazRezervacija.FindName("tblUkupnaCenaKupovine") as TextBlock;



            for (int i = 0; i < ListaIstorijeKupovine.Count; i++)
            {

                if (ListaIstorijeKupovine[i].IdIstorijaKupovine == idIstorijaKupovine)
                {
                    TempIstorijaKupovine = ListaIstorijeKupovine[i];
                    CurrentKupljenaOprema = ListaIstorijeKupovine[i].ListaKupljeneOpreme;
                    break;
                }
            }



            if (CurrentIstorijaKupovineKlasicanPrikaz != null)
            {
                if (CurrentIstorijaKupovineKlasicanPrikaz.KliknutoNaGrid == false)
                {

                    //border1.Background = Brushes.White;
                    border2.Background = Brushes.White;
                    border3.Background = Brushes.White;
                    border4.Background = Brushes.White;

                    //tblIme.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumProdaje.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblProdavac.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblUkupnaCenaKupovine.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));


                    CurrentIstorijaKupovineKlasicanPrikaz.KliknutoNaGrid = true;
                    TempIstorijaKupovine.KliknutoNaGrid = true;
                }

            }

            

            tempGridIstorijaKupovine = gridPrikazRezervacija;
            

            //skrolVjuverSveRezervacije.Visibility = Visibility.Visible;

            if (timerPrikazOpremePravilnoRasporedjeno == null)
                timerPrikazOpremePravilnoRasporedjeno = new DispatcherTimer();
            if (!timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
            {
                timerPrikazOpremePravilnoRasporedjeno.Interval = new TimeSpan(0, 0, 0, 0, 200);
                timerPrikazOpremePravilnoRasporedjeno.Tick += timerPrikazOpremePravilnoRasporedjeno_Tick;
                timerPrikazOpremePravilnoRasporedjeno.Start();
            }
        }


        private void grdPrikazRezervacija_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid gridPrikazRezervacija = sender as Grid;
            TextBlock tbIstorijaKupovineSkrivenId = gridPrikazRezervacija.FindName("tbIstorijaKupovineSkrivenId") as TextBlock;
            int idIstorijaKupovine = Convert.ToInt32(tbIstorijaKupovineSkrivenId.Text);

            for (int i = 0; i < ListaIstorijeKupovine.Count; i++)
            {

                if (ListaIstorijeKupovine[i].IdIstorijaKupovine == idIstorijaKupovine)
                {
                    CurrentIstorijaKupovineKlasicanPrikaz = ListaIstorijeKupovine[i];

                    break;
                }
            }


            if (CurrentIstorijaKupovineKlasicanPrikaz != null)
            {
                if (CurrentIstorijaKupovineKlasicanPrikaz.KliknutoNaGrid == false)
                {

                    //Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
                    Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
                    Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
                    Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border;
                    TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
                    TextBlock tblDatumProdaje = gridPrikazRezervacija.FindName("tblDatumProdaje") as TextBlock;
                    TextBlock tblProdavac = gridPrikazRezervacija.FindName("tblProdavac") as TextBlock;
                    TextBlock tblUkupnaCenaKupovine = gridPrikazRezervacija.FindName("tblUkupnaCenaKupovine") as TextBlock;

                    //border1.Background = Brushes.White;
                    border2.Background = Brushes.White;
                    border3.Background = Brushes.White;
                    border4.Background = Brushes.White;

                    //tblIme.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumProdaje.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblProdavac.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblUkupnaCenaKupovine.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                }

            }

        }

        private void grdPrikazRezervacija_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid gridPrikazRezervacija = sender as Grid;
            TextBlock tbIstorijaKupovineSkrivenId = gridPrikazRezervacija.FindName("tbIstorijaKupovineSkrivenId") as TextBlock;
            int idIstorijaKupovine = Convert.ToInt32(tbIstorijaKupovineSkrivenId.Text);

            for (int i = 0; i < ListaIstorijeKupovine.Count; i++)
            {

                if (ListaIstorijeKupovine[i].IdIstorijaKupovine == idIstorijaKupovine)
                {
                    CurrentIstorijaKupovineKlasicanPrikaz = ListaIstorijeKupovine[i];
                    break;
                }
            }

            if (CurrentIstorijaKupovineKlasicanPrikaz != null)
            {
                if (CurrentIstorijaKupovineKlasicanPrikaz.KliknutoNaGrid == false)
                {

                    //Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
                    Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
                    Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
                    Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border;
                    TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
                    TextBlock tblDatumProdaje = gridPrikazRezervacija.FindName("tblDatumProdaje") as TextBlock;
                    TextBlock tblProdavac = gridPrikazRezervacija.FindName("tblProdavac") as TextBlock;
                    TextBlock tblUkupnaCenaKupovine = gridPrikazRezervacija.FindName("tblUkupnaCenaKupovine") as TextBlock;

                    //border1.Background = Brushes.Transparent;
                    border2.Background = Brushes.Transparent;
                    border3.Background = Brushes.Transparent;
                    border4.Background = Brushes.Transparent;

                    //tblIme.Foreground = Brushes.Black;
                    tblDatumProdaje.Foreground = Brushes.Black;
                    tblProdavac.Foreground = Brushes.Black;
                    tblUkupnaCenaKupovine.Foreground = Brushes.Black;
                }
            }
        }

        DispatcherTimer timerPrikazOpremePravilnoRasporedjeno = new DispatcherTimer();

        void timerPrikazOpremePravilnoRasporedjeno_Tick(object sender, EventArgs e)
        {
            if (timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();

                if (this.CurrentKupljenaOprema != null && this.CurrentKupljenaOprema.Count > 0)
                {
                    for (int i = 0; i < this.CurrentKupljenaOprema.Count; i++)
                    {
                        if (itcSveRezervacije.Items.Count > 0)
                        {
                            ContentPresenter cpa = itcSveRezervacije.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                            Border brdOprema = FindChild<Border>(cpa);
                            if (brdOprema != null)
                            {
                                TextBlock tblckOpremaNaziv = brdOprema.FindName("tblckOpremaNaziv") as TextBlock;
                                TextBlock tblckOpremaProizvodjac = brdOprema.FindName("tblckOpremaProizvodjac") as TextBlock;
                                TextBlock tblckOpremaModel = brdOprema.FindName("tblckOpremaModel") as TextBlock;
                                TextBlock tblckOpremaOpis = brdOprema.FindName("tblckOpremaOpis") as TextBlock;
                                TextBlock tblckOpremaCena = brdOprema.FindName("tblckOpremaCena") as TextBlock;
                                TextBlock tblckOpremaKolicina = brdOprema.FindName("tblckOpremaKolicina") as TextBlock;

                                TextBlock tblckOpremaUkupnaCena = brdOprema.FindName("tblckOpremaUkupnaCena") as TextBlock;

                                int id = Convert.ToInt32((brdOprema.FindName("skrivenId") as TextBlock).Text);

                                if (tblckOpremaNaziv.ActualWidth > 240)
                                {

                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstNaslovaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;

                                        tblckOpremaNazivDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaNazivDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }



                                }
                                if (tblckOpremaProizvodjac.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstProizvodjacaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaProizvodjacDetaljnije = brdOprema.FindName("tblckOpremaProizvodjacDetaljnije") as TextBlock;

                                        tblckOpremaProizvodjacDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaProizvodjacDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }
                                if (tblckOpremaModel.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstModelaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaModelDetaljnije = brdOprema.FindName("tblckOpremaModelDetaljnije") as TextBlock;

                                        tblckOpremaModelDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaModelDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }
                                if (tblckOpremaOpis.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstOpisaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaOpisDetaljnije = brdOprema.FindName("tblckOpremaOpisDetaljnije") as TextBlock;

                                        tblckOpremaOpisDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaOpisDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }

                                if (tblckOpremaCena.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstCeneZauzimaViseRedova = true;
                                        TextBlock tblckOpremaCenaDetaljnije = brdOprema.FindName("tblckOpremaCenaDetaljnije") as TextBlock;

                                        tblckOpremaCenaDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaCenaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }

                                }
                                if (tblckOpremaKolicina.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstKolicineZauzimaViseRedova = true;
                                        TextBlock tblckOpremaKolicinaDetaljnije = brdOprema.FindName("tblckOpremaKolicinaDetaljnije") as TextBlock;

                                        tblckOpremaKolicinaDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaKolicinaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }

                                }
                                if (tblckOpremaUkupnaCena.ActualWidth > 100)
                                {


                                    if (id == this.CurrentKupljenaOprema[i].IdOprema)
                                    {
                                        this.CurrentKupljenaOprema[i].DaLiTekstUkupneCeneZauzimaViseRedova = true;
                                        TextBlock tblckOpremaUkupnaCenaDetaljnije = brdOprema.FindName("tblckOpremaUkupnaCenaDetaljnije") as TextBlock;
                                        //DoubleAnimation daTblckOpremaCenaDetaljnijeSirina = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1))
                                        tblckOpremaUkupnaCenaDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaUkupnaCenaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }

                                }

                            }
                        }
                    }
                }

            }
            if (timerPrikazOpremePravilnoRasporedjeno != null)
                timerPrikazOpremePravilnoRasporedjeno.Stop();
            timerPrikazOpremePravilnoRasporedjeno = null;
        }

        private void DockingManagerIstorijaKupovine_Loaded(object sender, RoutedEventArgs e)
        {
            LevaStranaDockingManagera.ToggleAutoHide();
            //DesnaStranaDockingManagera.ToggleAutoHide();

            // You might want to do this to get a reasonable height
            var root = (LayoutAnchorablePane)LevaStranaDockingManagera.Parent;
            root.DockWidth = new GridLength(275);
            //var root2 = (LayoutAnchorablePane)DesnaStranaDockingManagera.Parent;
            //root2.DockWidth = new GridLength(300);
        }

        private void chbListaUpitaZaKupce_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chbListaUpitaZaKupce_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void brdOprema_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double i = itcSveRezervacije.ActualWidth;
        }

        private void btnIzmeniISacuvajKorisnici_Click(object sender, RoutedEventArgs e)
        {
            //ControlTemplate template = Sadrzaj.Template;
            //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;
            int pozicija, pozicijaNaWebServeruAdmin, pozicijaNaWebServeruProdavac = 1;
            string nazivSlike, nazivSlikeNaWebServeruAdmin, nazivSlikeNaWebServeruProdavac = "";

            if (this.CurrentKorisnici.DaLiJeMuskiPolCekiran && !this.CurrentKorisnici.DaLiJeZenskiPolCekiran)
                this.CurrentKorisnici.PolKorisnika = true;
            else if (!this.CurrentKorisnici.DaLiJeMuskiPolCekiran && this.CurrentKorisnici.DaLiJeZenskiPolCekiran)
                this.CurrentKorisnici.PolKorisnika = false;


            pozicija = this.CurrentKorisnici.SlikaKorisnika.LastIndexOf("\\");
            nazivSlike = this.CurrentKorisnici.SlikaKorisnika.Substring(pozicija + 1);
            pozicijaNaWebServeruAdmin = App.PutanjaDoSlikeAdministratorKorisnici.LastIndexOf("\\");
            pozicijaNaWebServeruProdavac = App.PutanjaDoSlikeProdavacKorisnici.LastIndexOf("\\");
            nazivSlikeNaWebServeruAdmin = App.PutanjaDoSlikeAdministratorKorisnici.Substring(pozicijaNaWebServeruAdmin + 1);
            nazivSlikeNaWebServeruProdavac = App.PutanjaDoSlikeProdavacKorisnici.Substring(pozicijaNaWebServeruProdavac + 1);


            string putanjaPrefix = "\\slike\\korisnici\\";

            if (nazivSlike == nazivSlikeNaWebServeruAdmin || nazivSlike == nazivSlikeNaWebServeruProdavac)
            {
                this.CurrentKorisnici.SlikaKorisnika = null;
            }
            else
            {
                

                System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentKorisnici.SlikaKorisnika);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                       uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                       new System.IO.FileStream(this.CurrentKorisnici.SlikaKorisnika,
                       System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = nazivSlike;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                }

                this.CurrentKorisnici.SlikaKorisnika = putanjaPrefix + nazivSlike;
            }


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


            DbItemKorisnici korisniciAzuriranje = new DbItemKorisnici()
            {
                id_korisnici = this.CurrentKorisnici.IdKorisnici,
                
                ime = this.CurrentKorisnici.ImeKorisnika,
                prezime = this.CurrentKorisnici.PrezimeKorisnika,
                mejl = this.CurrentKorisnici.MejlKorisnika,
                broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                brojOstvarenihPoena = Convert.ToInt32(this.CurrentKorisnici.BrojOstvarenihPoena),
                username = this.CurrentKorisnici.Username,
                lozinka = this.CurrentKorisnici.Lozinka,
                polKorisnika = this.CurrentKorisnici.PolKorisnika,
                datumAzuriranja = this.CurrentKorisnici.DatumAzuriranjaKorisnika,
                slikaKorisnika = this.CurrentKorisnici.SlikaKorisnika,
                //naziv_uloge = this.CurrentKorisnici.NazivUloge,
                //deletedField = this.CurrentKorisnici.DeletedItem
            };

            if(this.CurrentKorisnici.IdUloge == 0)
                korisniciAzuriranje.id_uloge = 1;
            else
                korisniciAzuriranje.id_uloge = 2;

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciUpdate(korisniciAzuriranje);

            this.resetujSadrzajKorisnici();

            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
            this.popuniListuKorisnici(nizKorisnika);
            this.pravilnoRasporediListuKorisnika();
        }

        private void btnKorisniciSacuvajUnos_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruAdmin, pozicijaNaWebServeruProdavac = 1;
            string nazivSlike, nazivSlikeNaWebServeruAdmin, nazivSlikeNaWebServeruProdavac = "";

            if (this.CurrentKorisnici.DaLiJeMuskiPolCekiran && !this.CurrentKorisnici.DaLiJeZenskiPolCekiran)
                this.CurrentKorisnici.PolKorisnika = true;
            else if (!this.CurrentKorisnici.DaLiJeMuskiPolCekiran && this.CurrentKorisnici.DaLiJeZenskiPolCekiran)
                this.CurrentKorisnici.PolKorisnika = false;


            pozicija = this.CurrentKorisnici.SlikaKorisnika.LastIndexOf("\\");
            nazivSlike = this.CurrentKorisnici.SlikaKorisnika.Substring(pozicija + 1);
            pozicijaNaWebServeruAdmin = App.PutanjaDoSlikeAdministratorKorisnici.LastIndexOf("\\");
            pozicijaNaWebServeruProdavac = App.PutanjaDoSlikeProdavacKorisnici.LastIndexOf("\\");
            nazivSlikeNaWebServeruAdmin = App.PutanjaDoSlikeAdministratorKorisnici.Substring(pozicijaNaWebServeruAdmin + 1);
            nazivSlikeNaWebServeruProdavac = App.PutanjaDoSlikeProdavacKorisnici.Substring(pozicijaNaWebServeruProdavac + 1);


            string putanjaPrefix = "\\slike\\korisnici\\";

            if (nazivSlike == nazivSlikeNaWebServeruAdmin || nazivSlike == nazivSlikeNaWebServeruProdavac)
            {
                this.CurrentKorisnici.SlikaKorisnika = null;
            }
            else
            {


                System.IO.FileInfo fileInfo =
                           new System.IO.FileInfo(this.CurrentKorisnici.SlikaKorisnika);
                SmartSoftwareServiceInterfaceClient clientUpload =
                        new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.RemoteFileInfo
                       uploadRequestInfo = new RemoteFileInfo();

                using (System.IO.FileStream stream =
                       new System.IO.FileStream(this.CurrentKorisnici.SlikaKorisnika,
                       System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    uploadRequestInfo.FileName = nazivSlike;
                    uploadRequestInfo.Length = fileInfo.Length;
                    uploadRequestInfo.FileByteStream = stream;
                    nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                }

                this.CurrentKorisnici.SlikaKorisnika = putanjaPrefix + nazivSlike;
            }


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


            DbItemKorisnici korisnik = new DbItemKorisnici()
            {
                id_korisnici = this.CurrentKorisnici.IdKorisnici,

                ime = this.CurrentKorisnici.ImeKorisnika,
                prezime = this.CurrentKorisnici.PrezimeKorisnika,
                mejl = this.CurrentKorisnici.MejlKorisnika,
                broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                brojOstvarenihPoena = Convert.ToInt32(this.CurrentKorisnici.BrojOstvarenihPoena),
                username = this.CurrentKorisnici.Username,
                lozinka = this.CurrentKorisnici.Lozinka,
                polKorisnika = this.CurrentKorisnici.PolKorisnika,
                datumAzuriranja = this.CurrentKorisnici.DatumAzuriranjaKorisnika,
                slikaKorisnika = this.CurrentKorisnici.SlikaKorisnika,
                //naziv_uloge = this.CurrentKorisnici.NazivUloge,
                //deletedField = this.CurrentKorisnici.DeletedItem
            };

            if (this.CurrentKorisnici.IdUloge == 0)
                korisnik.id_uloge = 1;
            else
                korisnik.id_uloge = 2;

                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.ZaposleniKorisniciInsert(korisnik);
                this.popuniListuKorisnici(nizKorisnika);
                otkaziIzmeneKorisnici_Click();
                this.pravilnoRasporediListuKorisnika();
            
        }

        private void btnIzmeniISacuvajOblastiOpreme_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentOblastiOpreme != null && this.CurrentOblastiOpreme.Picture != null)
            {
                pozicija = this.CurrentOblastiOpreme.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentOblastiOpreme.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\oblasti_opreme\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentOblastiOpreme.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentOblastiOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOblastiOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentOblastiOpreme.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemOblastiOpreme oblastiAzuriranje = new DbItemOblastiOpreme()
                {
                    id_oblasti_opreme = this.CurrentOblastiOpreme.IdOblastiOpreme,
                    naziv_oblasti_opreme = this.CurrentOblastiOpreme.Name,
                    picture = this.CurrentOblastiOpreme.Picture
                };



                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeUpdate(oblastiAzuriranje);


                this.popuniListuOblasti(nizOblasti);

                otkaziIzmeneOblastiOpreme_Click();

                this.pravilnoRasporediListuKorisnika();

            }
        }

        private void btnOblastiOpremeSacuvajUnos_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentOblastiOpreme != null && this.CurrentOblastiOpreme.Picture != null)
            {
                pozicija = this.CurrentOblastiOpreme.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentOblastiOpreme.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\oblasti_opreme\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentOblastiOpreme.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentOblastiOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOblastiOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentOblastiOpreme.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemOblastiOpreme oblastiInsert = new DbItemOblastiOpreme()
                {
                    naziv_oblasti_opreme = this.CurrentOblastiOpreme.Name,
                    picture = this.CurrentOblastiOpreme.Picture
                };



                SmartSoftwareServiceReference.DbItemOblastiOpreme[] nizOblasti = service.OblastiOpremeInsert(oblastiInsert);

                this.popuniListuOblasti(nizOblasti);

                otkaziIzmeneOblastiOpreme_Click();
                this.pravilnoRasporediListuKorisnika();
            }
        }

        private void btnIzmeniISacuvajTipOpreme_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentTipoviOpreme != null && this.CurrentTipoviOpreme.Picture != null)
            {
                pozicija = this.CurrentTipoviOpreme.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentTipoviOpreme.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\tipovi_opreme\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentTipoviOpreme.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentTipoviOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentTipoviOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentTipoviOpreme.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemTipOpreme tipAzuriranje = new DbItemTipOpreme()
                {
                    id_tip_opreme = this.CurrentTipoviOpreme.IdTipOpreme,
                    id_oblasti_opreme = this.ListaOblastiOpremeZaTipoveOpreme[Convert.ToInt32(this.IzabranaOblastOpreme.Value)].IdOblastiOpreme,
                    naziv_tipa = this.CurrentTipoviOpreme.Name,
                    slika_tipa = this.CurrentTipoviOpreme.Picture
                };



                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeUpdate(tipAzuriranje);
                this.popuniListuTipovaOpreme(nizTipova);
                otkaziIzmeneTipoviOpreme_Click();
                this.pravilnoRasporediListuKorisnika();
            }
        }

        private void btnTipoviOpremeSacuvajUnos_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentTipoviOpreme != null && this.CurrentTipoviOpreme.Picture != null)
            {
                pozicija = this.CurrentTipoviOpreme.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentTipoviOpreme.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\tipovi_opreme\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentTipoviOpreme.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentTipoviOpreme.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentTipoviOpreme.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentTipoviOpreme.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemTipOpreme tipInsert = new DbItemTipOpreme()
                {

                    id_oblasti_opreme = this.ListaOblastiOpremeZaTipoveOpreme[Convert.ToInt32(this.IzabranaOblastOpreme.Value)].IdOblastiOpreme,
                    naziv_tipa = this.CurrentTipoviOpreme.Name,
                    slika_tipa = this.CurrentTipoviOpreme.Picture
                };



                SmartSoftwareServiceReference.DbItemTipOpreme[] nizTipova = service.TipoviOpremeInsert(tipInsert);
                this.popuniListuTipovaOpreme(nizTipova);
                otkaziIzmeneTipoviOpreme_Click();
                this.pravilnoRasporediListuKorisnika();
            }
        }

        private void btnIzmeniISacuvajOpremu_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentOprema != null && this.CurrentOprema.Picture != null)
            {
                pozicija = this.CurrentOprema.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentOprema.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\oprema\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentOprema.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentOprema.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOprema.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentOprema.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemOpremaSaParametrima opremaAzuriranje = new DbItemOpremaSaParametrima()
                {
                    id_oprema = this.CurrentOprema.IdOprema,
                    id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                    naslov = this.CurrentOprema.Name,
                    slika = this.CurrentOprema.Picture,
                    proizvodjac = this.CurrentOprema.Proizvodjac,
                    cena = this.CurrentOprema.Cena,
                    model = this.CurrentOprema.Model,
                    kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                    kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                    opis = this.CurrentOprema.Opis,
                };


                List<DbItemParametri> parametri = new List<DbItemParametri>();

                foreach (var item in this.CurrentOprema.ListaParametara)
                {
                    parametri.Add(new DbItemParametri()
                    {
                        default_vrednost = item.DefaultVrednost,
                        id_parametri = item.IdParametri,
                        id_tip_opreme = item.IdTipOpreme,
                        naziv_parametra = item.Name,
                        tipParametra = item.TipParametra,
                        vrednost_parametra = item.VrednostParametra
                    });
                }

                opremaAzuriranje.ListaParametara = parametri.ToArray();


                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelUpdate(opremaAzuriranje);
                this.popuniListuOpremeSaParametrima(nizOpreme);
                this.otkaziIzmeneOpreme_Click();
                this.pravilnoRasporediListuKorisnika();
            }
        }

        private void btnOpremaSacuvajUnos_Click(object sender, RoutedEventArgs e)
        {
            int pozicija, pozicijaNaWebServeruNoImage = 1;
            string nazivSlike, nazivSlikeNaWebServeruNoImage = "";

            if (this.CurrentOprema != null && this.CurrentOprema.Picture != null)
            {
                pozicija = this.CurrentOprema.Picture.LastIndexOf("\\");
                nazivSlike = this.CurrentOprema.Picture.Substring(pozicija + 1);
                pozicijaNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.LastIndexOf("\\");

                nazivSlikeNaWebServeruNoImage = App.PutanjaDoSlikeNoImage.Substring(pozicijaNaWebServeruNoImage + 1);


                string putanjaPrefix = "\\slike\\oprema\\";

                if (nazivSlike == nazivSlikeNaWebServeruNoImage)
                {
                    this.CurrentOprema.Picture = null;
                }

                else
                {


                    System.IO.FileInfo fileInfo =
                               new System.IO.FileInfo(this.CurrentOprema.Picture);
                    SmartSoftwareServiceInterfaceClient clientUpload =
                            new SmartSoftwareServiceInterfaceClient();
                    SmartSoftwareServiceReference.RemoteFileInfo
                           uploadRequestInfo = new RemoteFileInfo();

                    using (System.IO.FileStream stream =
                           new System.IO.FileStream(this.CurrentOprema.Picture,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        uploadRequestInfo.FileName = nazivSlike;
                        uploadRequestInfo.Length = fileInfo.Length;
                        uploadRequestInfo.FileByteStream = stream;
                        nazivSlike = clientUpload.UploadFile(uploadRequestInfo.FileName, uploadRequestInfo.Length, putanjaPrefix, uploadRequestInfo.FileByteStream);
                    }

                    this.CurrentOprema.Picture = putanjaPrefix + nazivSlike;
                }


                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemOpremaSaParametrima opremaInsert = new DbItemOpremaSaParametrima()
                {
                    //id_oprema = this.CurrentOprema.IdOprema,
                    id_tip_opreme = this.CurrentOprema.IdTipOpreme,
                    naslov = this.CurrentOprema.Name,
                    slika = this.CurrentOprema.Picture,
                    proizvodjac = this.CurrentOprema.Proizvodjac,
                    cena = this.CurrentOprema.Cena,
                    model = this.CurrentOprema.Model,
                    kolicina_u_rezervi = this.CurrentOprema.KolicinaURezervi,
                    kolicina_na_lageru = this.CurrentOprema.KolicinaNaLageru,
                    opis = this.CurrentOprema.Opis,
                };


                List<DbItemParametri> parametri = new List<DbItemParametri>();

                foreach (var item in this.CurrentOprema.ListaParametara)
                {
                    parametri.Add(new DbItemParametri()
                    {
                        default_vrednost = item.DefaultVrednost,
                        id_parametri = item.IdParametri,
                        id_tip_opreme = item.IdTipOpreme,
                        naziv_parametra = item.Name,
                        tipParametra = item.TipParametra,
                        vrednost_parametra = item.VrednostParametra
                    });
                }

                opremaInsert.ListaParametara = parametri.ToArray();


                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanelInsert(opremaInsert);
                this.popuniListuOpremeSaParametrima(nizOpreme);
                dalijeUnosUToku = false;
                brdPrikazDetaljaOpremeSadrzaj.BeginAnimation(Border.MarginProperty, taPomeriUlevoPolaSekunde);
                brdPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daSakrijPolaSekunde);
                brdPrazanPrikazDetaljaOpreme.BeginAnimation(Border.OpacityProperty, daPrikaziPolaSekunde);
                brdPrazanPrikazDetaljaOpremeSadrzaj.BeginAnimation(MarginProperty, taPomeriSDesnaUNormaluPolaSekunde);

                Panel.SetZIndex(brdPrazanPrikazDetaljaOpreme, 1);
                brdPrazanPrikazDetaljaOpreme.IsEnabled = true;
                otkaziIzmeneOpreme_Click();
                this.pravilnoRasporediListuKorisnika();
            }
        }

        private void btnIzmeniISacuvajParametar_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentParametri == null)
            {
                return;
            }

            for (int i = 0; i < this.ListaTipovaOpreme.Count; i++)
            {
                if (i == this.CurrentParametri.IzabranTipOpreme)
                {
                    this.currentParametri.IdTipOpreme = this.ListaTipovaOpreme[i].IdTipOpreme;
                    break;
                }
            }

            //ControlTemplate template = Sadrzaj.Template;
            //ContentControl ctmPrikazDetalja = template.FindName("ctmPrikazDetaljaSadrzaj", Sadrzaj) as ContentControl;

            //ControlTemplate template2 = ctmPrikazDetalja.Template;
            //ComboBox cmbUnosParametara = template2.FindName("cmbTipoviParametra", ctmPrikazDetalja) as ComboBox;
            //MessageBox.Show(cmbUnosParametara.SelectedValue.ToString());
            this.CurrentParametri.TipParametra = cmbTipoviParametra.SelectedValue.ToString();




            DbItemParametri p = new DbItemParametri()
            {
                default_vrednost = this.CurrentParametri.DefaultVrednost,
                id_parametri = this.CurrentParametri.IdParametri,
                id_tip_opreme = this.CurrentParametri.IdTipOpreme,
                naziv_parametra = this.CurrentParametri.Name,
                vrednost_parametra = this.CurrentParametri.VrednostParametra,
                za_filter = this.CurrentParametri.ParametarJeIFilter,
                tipParametra = this.CurrentParametri.TipParametra
            };

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemParametri[] nizParametara = service.ParametriUpdate(p);
            this.popuniListuParametara(nizParametara);
            this.pravilnoRasporediListuKorisnika();

            
            otkaziIzmeneParametri_Click();
            //ctmPrikazDetalja.Visibility = Visibility.Hidden;
        }

        private void tbPretragaKupci_KeyUp(object sender, KeyEventArgs e)
        {
            string zaPretragu = (sender as TextBox).Text;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemIstorijaKupovine[] istorijaKupovine = null;
            if (zaPretragu == null || zaPretragu == "")
            {
                istorijaKupovine = service.IstorijaKupovineSelect();
            }
            else
            {
                istorijaKupovine = service.IstorijaKupovineZaListuKupacaDatumPretraga(zaPretragu);

            }
            this.popuniListuIstorijeKupovineZaListuKupaca(istorijaKupovine);
            this.pravilnoRasporediListuKorisnika();
        }



       

        

        


        

    }

        

       
        

    
}
