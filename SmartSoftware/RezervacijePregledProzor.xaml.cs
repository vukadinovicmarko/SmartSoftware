using Microsoft.Win32;
using SmartSoftware;
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
using Xceed.Wpf.Toolkit;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for RezervacijePregledProzor.xaml
    /// </summary>
    public partial class RezervacijePregledProzor : Window, INotifyPropertyChanged
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string putanjaDoSlikeDelete = App.rootPath + "\\slike\\delete-128-Red.png";
        private string putanjaDoSlikeDeleteHoverovano = App.rootPath + "\\slike\\delete-128-White.png";

        DoubleAnimation daTriTackeZaDetalje = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));


        ObservableCollection<Oprema> listaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaOpreme
        {
            get { return listaOpreme; }
            set { SetAndNotify(ref listaOpreme, value); }
        }

        private string putanjaDoSlikeRezervisi = App.rootPath + "\\slike\\rezervisi-128-Green.png";
        public string PutanjaDoSlikeRezervisi
        {
            get { return putanjaDoSlikeRezervisi; }
            set { SetAndNotify(ref putanjaDoSlikeRezervisi, value); }
        }

        private string putanjaDoSlikeRezervisiHoverovano = App.rootPath + "\\slike\\rezervisi-128-White.png";
        public string PutanjaDoSlikeRezervisiHoverovano
        {
            get { return putanjaDoSlikeRezervisiHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeRezervisiHoverovano, value); }
        }

        private string putanjaDoSlikeRezervisiOnemoguceno = App.rootPath + "\\slike\\rezervisi-128-Gray.png";
        public string PutanjaDoSlikeRezervisiOnemoguceno
        {
            get { return putanjaDoSlikeRezervisiOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeRezervisiOnemoguceno, value); }
        }


        private string putanjaDoSlikePonistiRezervaciju = App.rootPath + "\\slike\\ponistiRezervaciju-128-Red.png";
        public string PutanjaDoSlikePonistiRezervaciju
        {
            get { return putanjaDoSlikePonistiRezervaciju; }
            set { SetAndNotify(ref putanjaDoSlikePonistiRezervaciju, value); }
        }

        private string putanjaDoSlikePonistiRezervacijuHoverovano = App.rootPath + "\\slike\\ponistiRezervaciju-128-White.png";
        public string PutanjaDoSlikePonistiRezervacijuHoverovano
        {
            get { return putanjaDoSlikePonistiRezervacijuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikePonistiRezervacijuHoverovano, value); }
        }

        private string putanjaDoSlikePonistiRezervacijuOnemoguceno = App.rootPath + "\\slike\\ponistiRezervaciju-128-Gray.png";
        public string PutanjaDoSlikePonistiRezervacijuOnemoguceno
        {
            get { return putanjaDoSlikePonistiRezervacijuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikePonistiRezervacijuOnemoguceno, value); }
        }


        private bool daLiJeNestoPrebacenoUKorpu = false;

        public bool DaLiJeNestoPrebacenoUKorpu
        {
            get { return daLiJeNestoPrebacenoUKorpu; }
            set { SetAndNotify(ref daLiJeNestoPrebacenoUKorpu, value); }
        }

        

        public static bool daLiJeIzvrsenaRezervacija = false;

        private ObservableCollection<Oprema> listaTrenutnihRezervacija = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaTrenutnihRezervacija
        {
            get { return listaTrenutnihRezervacija; }
            set { SetAndNotify(ref listaTrenutnihRezervacija, value); }
        }


        private ObservableCollection<Rezervacije> rezervacije = new ObservableCollection<Rezervacije>();

        public ObservableCollection<Rezervacije> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
        }

        private Rezervacije tempRezervacija = new Rezervacije();

        public Rezervacije TempRezervacija
        {
            get { return tempRezervacija; }
            set { SetAndNotify(ref tempRezervacija, value); }
        }
        private Rezervacije rezervacija;
        //private ICollectionView documentsView;

        private ObservableCollection<Oprema> currentOprema = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> CurrentOprema
        {
            get { return currentOprema; }
            set { SetAndNotify(ref currentOprema, value); }
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { SetAndNotify(ref korpa, value); }
        }


        private Rezervacije currentRezervacije = new Rezervacije();

        public Rezervacije CurrentRezervacije
        {
            get { return currentRezervacije; }
            set { SetAndNotify(ref currentRezervacije, value); }
        }

        private bool daLiImaNestoUTrenutnojListiSaRezervacijama = false;

        public bool DaLiImaNestoUTrenutnojListiSaRezervacijama
        {
            get { return daLiImaNestoUTrenutnojListiSaRezervacijama; }
            set { SetAndNotify(ref daLiImaNestoUTrenutnojListiSaRezervacijama, value); }
        }

        private double ukupnaCenaURezervacijama = 0;

        public double UkupnaCenaURezervacijama
        {
            get
            {
                double suma = 0;

                foreach (var item in this.ListaTrenutnihRezervacija)
                {
                    suma += (item as Oprema).SumCenaZaRezervisanje;
                }


                SetAndNotify(ref ukupnaCenaURezervacijama, suma);
                return this.ukupnaCenaURezervacijama;
            }

        }

        private Korisnici trenutniProdavac = new Korisnici();

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }

        public RezervacijePregledProzor(Korisnici prodavac)
        {
            InitializeComponent();
            this.DataContext = this;
            ListaTrenutnihRezervacija = new ObservableCollection<Oprema>();
            ListaTrenutnihRezervacija = GlavniProzor.listaTrenutnihRezervacija;
            //documentsView = CollectionViewSource.GetDefaultView(this.Rezervacije);
            //rezervacija = documentsView.CurrentItem as Rezervacije;
            //this.tmpRezervacija = rezervacija;

            this.TrenutniProdavac = prodavac;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanel();
            this.popuniListuOpremeSaParametrima(nizOpreme);

            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);

            if (this.ListaTrenutnihRezervacija != null && this.ListaTrenutnihRezervacija.Count > 0)
            {
                for (int j = 0; j < ListaTrenutnihRezervacija.Count; j++)
                {
                    if (this.ListaOpreme != null && this.ListaOpreme.Count > 0)
                    {
                        for (int i = 0; i < ListaOpreme.Count; i++)
                        {

                            if (this.ListaOpreme[i].IdOprema == this.ListaTrenutnihRezervacija[j].IdOprema)
                            {
                                if (this.ListaOpreme[i].DeletedItem)
                                {

                                    string poruka = "Dalje rezervisanje pojedine opreme nije moguće.\n\nOprema :\n(" + this.ListaTrenutnihRezervacija[j].Name.ToString() + "),\n\nje u međuvremenu izbrisana.";


                                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri čitanju opreme iz trenutne liste za rezervisanje", MessageBoxButton.OK, slikaBoxa);
                                        this.ListaTrenutnihRezervacija.RemoveAt(j);
                                        break;
                                    
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
            }

            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] nizOpremePonovo = service.OpremaSaParametrimaAdminPanel();
            this.popuniListuOpremeSaParametrima(nizOpremePonovo);


            if (this.ListaTrenutnihRezervacija != null && this.ListaTrenutnihRezervacija.Count > 0)
            {
                for (int j = 0; j < ListaTrenutnihRezervacija.Count; j++)
                {
                    if (this.ListaOpreme != null && this.ListaOpreme.Count > 0)
                    {
                        for (int i = 0; i < ListaOpreme.Count; i++)
                        {

                            if (this.ListaOpreme[i].IdOprema == this.ListaTrenutnihRezervacija[j].IdOprema)
                            {
                                if (this.ListaTrenutnihRezervacija[j].IzabranaKolicinaZaRezervisanje > this.ListaOpreme[i].KolicinaNaLageru)
                                {
                                    if (this.ListaOpreme[i].KolicinaNaLageru == 0)
                                    {
                                        string poruka = "Dalje rezervisanje pojedine opreme nije moguće.\n\nOprema :\n(" + this.ListaTrenutnihRezervacija[j].Name.ToString() + "),\n\nviše nije na stanju i biće izbrisana iz liste za rezervisanje. Naručite ovu opremu ponovo, ili kontaktirajte administatora.";


                                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri čitanju opreme iz trenutne liste za rezervisanje", MessageBoxButton.OK, slikaBoxa);
                                        this.ListaTrenutnihRezervacija.RemoveAt(j);
                                        break;
                                    }
                                    else if (this.ListaOpreme[i].KolicinaNaLageru > 0)
                                    {
                                        this.ListaTrenutnihRezervacija[j].IzabranaKolicinaZaRezervisanje = Convert.ToInt32(this.ListaOpreme[i].KolicinaNaLageru);
                                        this.ListaTrenutnihRezervacija[j].KolicinaNaLageru = Convert.ToInt32(this.ListaOpreme[i].KolicinaNaLageru);
                                        break;
                                    }
                                }
                                else
                                {
                                    this.ListaTrenutnihRezervacija[j].KolicinaNaLageru = Convert.ToInt32(this.ListaOpreme[i].KolicinaNaLageru);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            this.refreshujUkupnuCenu();
            

            if (ListaTrenutnihRezervacija.Count > 0)
            {
                scrolVjuverRezervacije.Visibility = Visibility.Visible;
                grdListaSaRezervacijamaJePrazna.Visibility = Visibility.Hidden;
                DaLiImaNestoUTrenutnojListiSaRezervacijama = true;


            }
            else
            {
                scrolVjuverRezervacije.Visibility = Visibility.Hidden;
                grdListaSaRezervacijamaJePrazna.Visibility = Visibility.Visible;
                DaLiImaNestoUTrenutnojListiSaRezervacijama = false;
            }

            
            
            
            SmartSoftwareServiceReference.DbItemRezervacijeSelect[] rezervacije = service.RezervacijeSelect();
            this.PopuniRezervacije(rezervacije);


            
            

            


        }


        private void PopuniRezervacije(DbItemRezervacijeSelect[] ListaRezervacija)
        {
            this.Rezervacije = new ObservableCollection<Rezervacije>();

            for (int i = 0; i < ListaRezervacija.Length; i++)
            {
                Rezervacije r = new Rezervacije()
                {
                    DatumAzuriranjaRezervacije = ListaRezervacija[i].datum_azuriranja_rezervacije, DatumIstekaRezervacije = ListaRezervacija[i].datum_isteka_rezervacije, DatumRezervacije = ListaRezervacija[i].datum_rezervacije, IdRerezervacije = ListaRezervacija[i].id_rezervacije, Ime = ListaRezervacija[i].imeNaRezervacija
                };

                for (int j = 0; j < ListaRezervacija[i].ListaOpremeZaRezervaciju.Length; j++)
                {
                    DbItemOpremaSaParametrima o = ListaRezervacija[i].ListaOpremeZaRezervaciju[j];
                    r.Oprema.Add(new Oprema(null)
                    {
                        Cena = o.cena,
                        IdOprema = o.id_oprema,
                        IdTipOpreme = o.id_tip_opreme,
                        KolicinaNaLageru = o.kolicina_na_lageru,
                        KolicinaURezervi = o.kolicina_u_rezervi,
                        Lager = o.lager,
                        Model = o.model,
                        Name = o.naslov,
                        Opis = o.opis,
                        OpremaNaPopustu = o.oprema_na_popustu,
                        Proizvodjac = o.proizvodjac,
                        Slika = o.slika,
                        SlikaOriginalPutanja = o.slikaOriginalPutanja,
                        IzabranaKolicina = 1,
                        DaliMozeJosDaseDoda = o.kolicina_na_lageru > 0,
                        TmpIzabranaKolicina = o.kolicinaURezervacijama,
                        
                    });

                    for (int k = 0; k < o.ListaParametara.Length; k++)
                    {
                        DbItemParametri p = o.ListaParametara[k];
                        r.Oprema[j].ListaParametara.Add(new Parametri(null)
                        {
                            DefaultVrednost = p.default_vrednost,
                            IdParametri = p.id_parametri,
                            IdTipOpreme = p.id_tip_opreme,
                            VrednostParametra = p.vrednost_parametra,
                            Name = p.naziv_parametra
                        });
                    }
                }
                this.Rezervacije.Add(r);
            }
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



        private void PopuniKorpu(DbItemOpremaSaParametrima[] oprema)
        {
            this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();
            if (oprema != null && oprema.Length > 0)
            {
                

                for (int i = 0; i < oprema.Length; i++)
                {
                    this.Korpa.Add(new Oprema(null)
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
                        IzabranaKolicina = oprema[i].kolicinaUKorpi,
                        SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena

                    });

                    //this.KorpaProvera.Add(new Oprema(null)
                    //{
                    //    Cena = oprema[i].cena,
                    //    IdOprema = oprema[i].id_oprema,
                    //    IdTipOpreme = oprema[i].id_tip_opreme,
                    //    KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
                    //    KolicinaURezervi = oprema[i].kolicina_u_rezervi,
                    //    Lager = oprema[i].lager,
                    //    Model = oprema[i].model,
                    //    Name = oprema[i].naslov,
                    //    Opis = oprema[i].opis,
                    //    OpremaNaPopustu = oprema[i].oprema_na_popustu,
                    //    Proizvodjac = oprema[i].proizvodjac,
                    //    Slika = oprema[i].slika,
                    //    SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
                    //    IzabranaKolicina = oprema[i].kolicinaUKorpi,
                    //    SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena
                    //});




                    for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
                    {
                        (this.Korpa[i] as Oprema).ListaParametara.Add(new Parametri(null)

                        {
                            DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                            IdParametri = oprema[i].ListaParametara[j].id_parametri,
                            IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                            VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                            Name = oprema[i].ListaParametara[j].naziv_parametra
                        });

                        //(this.KorpaProvera[i] as Oprema).ListaParametara.Add(new Parametri(null)

                        //{
                        //    DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        //    IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        //    IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        //    VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        //    Name = oprema[i].ListaParametara[j].naziv_parametra
                        //});

                    }

                }

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

                

                


                ListaOpreme2.Add(o);

                

            }
            ListaOpreme = ListaOpreme2;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void btnRezervisi_Click(object sender, RoutedEventArgs e)
        {

        }


        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void dugmeZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dugmeMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else this.WindowState = System.Windows.WindowState.Normal;
        }

        private void dugmeMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }


        public void refreshujUkupnuCenu()
        {
            this.ukupnaCenaURezervacijama = this.UkupnaCenaURezervacijama;
        }

        private void btnObrisiIzRezervacije_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Grid g = b.Parent as Grid;
            TextBlock t = g.FindName("skrivenId") as TextBlock;
            int id = Convert.ToInt32(t.Text);

            for (int i = 0; i < this.ListaTrenutnihRezervacija.Count; i++)
            {
                if ((this.ListaTrenutnihRezervacija[i] as Oprema).IdOprema == id)
                {
                    this.ListaTrenutnihRezervacija.RemoveAt(i);
                    break;
                }
            }

            if(!(this.ListaTrenutnihRezervacija.Count > 0))
            {
                scrolVjuverRezervacije.Visibility = Visibility.Hidden;
                    grdListaSaRezervacijamaJePrazna.Visibility = Visibility.Visible;
                    DaLiImaNestoUTrenutnojListiSaRezervacijama = false;
            }
            this.refreshujUkupnuCenu();
            

        }

        private void btnIsprazniTrenutnuListuRezervacija_Click(object sender, RoutedEventArgs e)
        {
            this.ListaTrenutnihRezervacija.Clear();

            scrolVjuverRezervacije.Visibility = Visibility.Hidden;
            grdListaSaRezervacijamaJePrazna.Visibility = Visibility.Visible;
            DaLiImaNestoUTrenutnojListiSaRezervacijama = false;




            this.refreshujUkupnuCenu();
        }

        private void btnDodajURezervacije_Click(object sender, RoutedEventArgs e)
        {
            RezervacijeOdabirKorisnika rok = new RezervacijeOdabirKorisnika(this.TrenutniProdavac);
            rok.ShowDialog();
            this.ListaTrenutnihRezervacija = rok.ListaTrenutnihRezervacija;

            if(daLiJeIzvrsenaRezervacija)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                DbItemRezervacijeSelect[] rezervacije = service.RezervacijeSelect();

                if (daLiJeIzvrsenaRezervacija)
                {
                    this.PopuniRezervacije(rezervacije);

                    GlavniProzor.listaTrenutnihRezervacija.Clear();
                    this.ListaTrenutnihRezervacija.Clear();
                    

                    scrolVjuverRezervacije.Visibility = Visibility.Hidden;
                    grdListaSaRezervacijamaJePrazna.Visibility = Visibility.Visible;
                    DaLiImaNestoUTrenutnojListiSaRezervacijama = false;



                    System.Windows.MessageBox.Show("Uspešno ste rezervisali opremu!");
                    sacuvaneRezervacije.IsActive = true;
                }

            }
            this.refreshujUkupnuCenu();

            

        }

        private void tbPretraga_KeyUp(object sender, KeyEventArgs e)
        {

        }
        Grid tempGrid;
        private bool daLiJeKliknutoNaGrid = false;

        public bool DaLiJeKliknutoNaGrid
        {
            get { return daLiJeKliknutoNaGrid; }
            set { SetAndNotify(ref daLiJeKliknutoNaGrid, value); }
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

        private void grdPrikazRezervacija_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TempRezervacija = new Rezervacije();
            if (tempGrid != null)
            {
                TextBlock tbSkrivenIdRezervacijeTempGrida = tempGrid.FindName("rezervacijeSkrivenId") as TextBlock;
                int idRezervacijeTempGrida = Convert.ToInt32(tbSkrivenIdRezervacijeTempGrida.Text);

                for (int i = 0; i < Rezervacije.Count; i++)
                {

                    if (Rezervacije[i].IdRerezervacije == idRezervacijeTempGrida)
                    {
                        TempRezervacija = Rezervacije[i];
                        CurrentRezervacije = Rezervacije[i];
                        break;
                    }
                }


                Border border5 = tempGrid.FindName("probaBorder") as Border;
                Border border6 = tempGrid.FindName("probaBorder1") as Border;
                Border border7 = tempGrid.FindName("probaBorder2") as Border;
                Border border8 = tempGrid.FindName("probaBorder3") as Border;
                TextBlock tblImeTemp = tempGrid.FindName("tblIme") as TextBlock;
                TextBlock tblBrojTelefonaTemp = tempGrid.FindName("tblBrojTelefona") as TextBlock;
                TextBlock tblDatumRezervacijeTemp = tempGrid.FindName("tblDatumRezervacije") as TextBlock;
                TextBlock tblDatumIstekaRezervacijeTemp = tempGrid.FindName("tblDatumIstekaRezervacije") as TextBlock;
                TextBlock tblDatumAzuriranjaRezervacijeTemp = tempGrid.FindName("tblDatumAzuriranjaRezervacije") as TextBlock;

                border5.Background = Brushes.Transparent;
                border6.Background = Brushes.Transparent;
                border7.Background = Brushes.Transparent;
                border8.Background = Brushes.Transparent;

                tblImeTemp.Foreground = Brushes.Black;
                tblBrojTelefonaTemp.Foreground = Brushes.Black;
                tblDatumRezervacijeTemp.Foreground = Brushes.Black;
                tblDatumIstekaRezervacijeTemp.Foreground = Brushes.Black;
                tblDatumAzuriranjaRezervacijeTemp.Foreground = Brushes.Black;

                CurrentRezervacije.KliknutoNaGrid = false;
                TempRezervacija.KliknutoNaGrid = false;
            }
            


            Grid gridPrikazRezervacija = sender as Grid;
            
            TextBlock tbSkrivenIdRezervacije = gridPrikazRezervacija.FindName("rezervacijeSkrivenId") as TextBlock;
            int idRezervacije = Convert.ToInt32(tbSkrivenIdRezervacije.Text);
            
            Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
            Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
            Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
            Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border;
            TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
            TextBlock tblBrojTelefona = gridPrikazRezervacija.FindName("tblBrojTelefona") as TextBlock;
            TextBlock tblDatumRezervacije = gridPrikazRezervacija.FindName("tblDatumRezervacije") as TextBlock;
            TextBlock tblDatumIstekaRezervacije = gridPrikazRezervacija.FindName("tblDatumIstekaRezervacije") as TextBlock;
            TextBlock tblDatumAzuriranjaRezervacije = gridPrikazRezervacija.FindName("tblDatumAzuriranjaRezervacije") as TextBlock;
            
            
            
            for (int i = 0; i < Rezervacije.Count; i++)
            {
                
                if (Rezervacije[i].IdRerezervacije == idRezervacije)
                {
                    TempRezervacija = Rezervacije[i];
                    CurrentRezervacije = Rezervacije[i];
                    CurrentOprema = Rezervacije[i].Oprema;
                    break;
                }
            }

            

            if (CurrentRezervacije != null)
            {
                if (CurrentRezervacije.KliknutoNaGrid == false)
                {

                    border1.Background = Brushes.White;
                    border2.Background = Brushes.White;
                    border3.Background = Brushes.White;
                    border4.Background = Brushes.White;

                    tblIme.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblBrojTelefona.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumIstekaRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumAzuriranjaRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));

                    CurrentRezervacije.KliknutoNaGrid = true;
                    TempRezervacija.KliknutoNaGrid = true;
                }

            }



            tempGrid = gridPrikazRezervacija;
            skrolVjuverSveRezervacije.Visibility = Visibility.Visible;

            if(timerPrikazOpremePravilnoRasporedjeno == null)
                timerPrikazOpremePravilnoRasporedjeno = new DispatcherTimer();
            if(!timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
            {
                timerPrikazOpremePravilnoRasporedjeno.Interval = new TimeSpan(0, 0, 0, 0, 200);
                timerPrikazOpremePravilnoRasporedjeno.Tick += timerPrikazOpremePravilnoRasporedjeno_Tick;
                timerPrikazOpremePravilnoRasporedjeno.Start();
            }

        }

        void timerPrikazOpremePravilnoRasporedjeno_Tick(object sender, EventArgs e)
        {
            if(timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
            {
                DispatcherTimer timerSender = (DispatcherTimer)sender;
                timerSender.Stop();

                if (this.CurrentOprema != null && this.CurrentOprema.Count > 0)
                {
                    for (int i = 0; i < this.CurrentOprema.Count; i++)
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

                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstNaslovaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaNazivDetaljnije = brdOprema.FindName("tblckOpremaNazivDetaljnije") as TextBlock;

                                        tblckOpremaNazivDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaNazivDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }



                                }
                                if (tblckOpremaProizvodjac.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstProizvodjacaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaProizvodjacDetaljnije = brdOprema.FindName("tblckOpremaProizvodjacDetaljnije") as TextBlock;

                                        tblckOpremaProizvodjacDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaProizvodjacDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }
                                if (tblckOpremaModel.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstModelaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaModelDetaljnije = brdOprema.FindName("tblckOpremaModelDetaljnije") as TextBlock;

                                        tblckOpremaModelDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaModelDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }
                                if (tblckOpremaOpis.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstOpisaZauzimaViseRedova = true;
                                        TextBlock tblckOpremaOpisDetaljnije = brdOprema.FindName("tblckOpremaOpisDetaljnije") as TextBlock;

                                        tblckOpremaOpisDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaOpisDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }


                                }

                                if (tblckOpremaCena.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstCeneZauzimaViseRedova = true;
                                        TextBlock tblckOpremaCenaDetaljnije = brdOprema.FindName("tblckOpremaCenaDetaljnije") as TextBlock;

                                        tblckOpremaCenaDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaCenaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }

                                }
                                if (tblckOpremaKolicina.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstKolicineZauzimaViseRedova = true;
                                        TextBlock tblckOpremaKolicinaDetaljnije = brdOprema.FindName("tblckOpremaKolicinaDetaljnije") as TextBlock;

                                        tblckOpremaKolicinaDetaljnije.Visibility = Visibility.Visible;
                                        tblckOpremaKolicinaDetaljnije.BeginAnimation(TextBlock.WidthProperty, daTriTackeZaDetalje);

                                    }

                                }
                                if (tblckOpremaUkupnaCena.ActualWidth > 100)
                                {


                                    if (id == this.CurrentOprema[i].IdOprema)
                                    {
                                        this.CurrentOprema[i].DaLiTekstUkupneCeneZauzimaViseRedova = true;
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

        DispatcherTimer timerPrikazOpremePravilnoRasporedjeno = new DispatcherTimer();

        private void grdPrikazRezervacija_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid gridPrikazRezervacija = sender as Grid;
            TextBlock tbSkrivenIdRezervacije = gridPrikazRezervacija.FindName("rezervacijeSkrivenId") as TextBlock;
            int idRezervacije = Convert.ToInt32(tbSkrivenIdRezervacije.Text);

            for (int i = 0; i < Rezervacije.Count; i++)
            {

                if (Rezervacije[i].IdRerezervacije == idRezervacije)
                {
                    CurrentRezervacije = Rezervacije[i];
                    
                    break;
                }
            }


            if (CurrentRezervacije != null)
            {
                if (CurrentRezervacije.KliknutoNaGrid == false)
                {
                   
                    Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
                    Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
                    Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
                    Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border;
                    TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
                    TextBlock tblBrojTelefona = gridPrikazRezervacija.FindName("tblBrojTelefona") as TextBlock;
                    TextBlock tblDatumRezervacije = gridPrikazRezervacija.FindName("tblDatumRezervacije") as TextBlock;
                    TextBlock tblDatumIstekaRezervacije = gridPrikazRezervacija.FindName("tblDatumIstekaRezervacije") as TextBlock;
                    TextBlock tblDatumAzuriranjaRezervacije = gridPrikazRezervacija.FindName("tblDatumAzuriranjaRezervacije") as TextBlock;

                    border1.Background = Brushes.White;
                    border2.Background = Brushes.White;
                    border3.Background = Brushes.White;
                    border4.Background = Brushes.White;

                    tblIme.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblBrojTelefona.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumIstekaRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                    tblDatumAzuriranjaRezervacije.Foreground = new SolidColorBrush(Color.FromArgb(255, 47, 87, 120));
                }
            
            }

        }

        private void grdPrikazRezervacija_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid gridPrikazRezervacija = sender as Grid;
            TextBlock tbSkrivenIdRezervacije = gridPrikazRezervacija.FindName("rezervacijeSkrivenId") as TextBlock;
            int idRezervacije = Convert.ToInt32(tbSkrivenIdRezervacije.Text);

            for (int i = 0; i < Rezervacije.Count; i++)
            {

                if (Rezervacije[i].IdRerezervacije == idRezervacije)
                {
                    CurrentRezervacije = Rezervacije[i];
                    
                    break;
                }
            }

            if (CurrentRezervacije != null)
            {
                if (CurrentRezervacije.KliknutoNaGrid == false)
                {
                    
                    Border border1 = gridPrikazRezervacija.FindName("probaBorder") as Border;
                    Border border2 = gridPrikazRezervacija.FindName("probaBorder1") as Border;
                    Border border3 = gridPrikazRezervacija.FindName("probaBorder2") as Border;
                    Border border4 = gridPrikazRezervacija.FindName("probaBorder3") as Border; 
                    TextBlock tblIme = gridPrikazRezervacija.FindName("tblIme") as TextBlock;
                    TextBlock tblBrojTelefona = gridPrikazRezervacija.FindName("tblBrojTelefona") as TextBlock;
                    TextBlock tblDatumRezervacije = gridPrikazRezervacija.FindName("tblDatumRezervacije") as TextBlock;
                    TextBlock tblDatumIstekaRezervacije = gridPrikazRezervacija.FindName("tblDatumIstekaRezervacije") as TextBlock;
                    TextBlock tblDatumAzuriranjaRezervacije = gridPrikazRezervacija.FindName("tblDatumAzuriranjaRezervacije") as TextBlock;

                    border1.Background = Brushes.Transparent;
                    border2.Background = Brushes.Transparent;
                    border3.Background = Brushes.Transparent;
                    border4.Background = Brushes.Transparent;

                    tblIme.Foreground = Brushes.Black;
                    tblBrojTelefona.Foreground = Brushes.Black;
                    tblDatumRezervacije.Foreground = Brushes.Black;
                    tblDatumIstekaRezervacije.Foreground = Brushes.Black;
                    tblDatumAzuriranjaRezervacije.Foreground = Brushes.Black;
                }
            }
        }

        private void btnIzbrisiIzabranuRezervaciju_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("da li ste sigurni da želite da izbrišete ovu rezervaciju : " + TempRezervacija.Ime.ToString() + " ?");

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            if (this.TempRezervacija != null)
            {

                DbItemRezervacijeDeleteIUpdate rezervacijaDelete = new DbItemRezervacijeDeleteIUpdate()
                {
                    id_rezervacije = this.TempRezervacija.IdRerezervacije
                    
                };

                List<DbItemOprema> lista = new List<DbItemOprema>();
                 
                for (int i = 0; i < this.TempRezervacija.Oprema.Count; i++)
                {
                    DbItemOprema oprema = new DbItemOprema()
                        {
                            id_oprema = (int)this.TempRezervacija.Oprema[i].IdOprema,
                            kolicina_rezervisane_opreme = (int) this.TempRezervacija.Oprema[i].TmpIzabranaKolicina
                        };
                    lista.Add(oprema);
                    
                    
                };

                rezervacijaDelete.ListaOpremeZaRezervaciju = lista.ToArray();





                DbItemRezervacijeSelect[] rezervacije = service.RezervacijeDelete(rezervacijaDelete);
                this.PopuniRezervacije(rezervacije);
                skrolVjuverSveRezervacije.Visibility = Visibility.Hidden;
                this.TempRezervacija.KliknutoNaGrid = false;


                this.refreshujUkupnuCenu();
            }
            
        }

        private void kolicina_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IntegerUpDown spinner = (IntegerUpDown)sender;
            int value = Convert.ToInt32(spinner.Text);

            //int value = String.IsNullOrEmpty(txtBox.Content.ToString()) ? 0 : Convert.ToInt32(txtBox.Content);
            //if (e.Direction == SpinDirection.Increase)
            //    value++;
            //else
            //    value--;
           // txtBox.Content = value.ToString();

            Grid grid = spinner.Parent as Grid;
            //Grid grid1 = grid.Parent as Grid;
            int id = 0;
            TextBlock t = grid.FindName("skrivenId") as TextBlock;
            id = Convert.ToInt32(t.Text);
            for (int i = 0; i < this.ListaTrenutnihRezervacija.Count; i++)
            {
                if ((this.ListaTrenutnihRezervacija[i] as Oprema).IdOprema == id)
                {

                    Oprema tmp = (this.ListaTrenutnihRezervacija[i] as Oprema);
                    tmp.IzabranaKolicinaZaRezervisanje = value;
                    tmp.KolicinaNaLageruZaRezervisanje = tmp.KolicinaNaLageru - tmp.IzabranaKolicinaZaRezervisanje;
                    tmp.SumCenaZaRezervisanje = (this.ListaTrenutnihRezervacija[i] as Oprema).IzabranaKolicinaZaRezervisanje * (this.ListaTrenutnihRezervacija[i] as Oprema).Cena;
                    // this.ukupnaCenaUKorpi += tmp.SumCena; 
                    //if (value == tmp.KolicinaNaLageru)
                    //{
                    //    spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                    //}
                    //else if (value == 1)
                    //{
                    //    spinner.ValidSpinDirection = ValidSpinDirections.Increase;
                    //}
                    //else
                    //{
                    //    ButtonSpinner b = new ButtonSpinner();
                    //    spinner.ValidSpinDirection = b.ValidSpinDirection;
                    //    b = null;
                    //}
                }
            }


            //foreach (var item in this.Korpa)
            //{
            //    UkupnaCenaUKorpi += (item as Oprema).SumCena;
            //}
            this.refreshujUkupnuCenu();
        }

        private void kolicina_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown spinner = (IntegerUpDown)sender;
            int value = (int)spinner.Value;


            Grid grid = spinner.Parent as Grid;
            //Grid grid1 = grid.Parent as Grid;
            int id = 0;
            TextBlock t = grid.FindName("skrivenId") as TextBlock;
            id = Convert.ToInt32(t.Text);
            for (int i = 0; i < this.ListaTrenutnihRezervacija.Count; i++)
            {
                if ((this.ListaTrenutnihRezervacija[i] as Oprema).IdOprema == id)
                {

                    Oprema tmp = (this.ListaTrenutnihRezervacija[i] as Oprema);
                    tmp.IzabranaKolicinaZaRezervisanje = value;
                    tmp.KolicinaNaLageruZaRezervisanje = tmp.KolicinaNaLageru - tmp.IzabranaKolicinaZaRezervisanje;
                    tmp.SumCenaZaRezervisanje = (this.ListaTrenutnihRezervacija[i] as Oprema).IzabranaKolicinaZaRezervisanje * (this.ListaTrenutnihRezervacija[i] as Oprema).Cena;
                    
                }
            }


            
            this.refreshujUkupnuCenu();
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

        private void btnPrebaciRezervacijuUKorpu_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);

            this.PopuniKorpu(oprema);

            if(this.Korpa != null)
            {
                if(this.Korpa.Count > 0)
                {

                    string poruka = "Korpa nije prazna.\nDa li želite da ispraznite korpu i da nastavite?";

                    MessageBoxButton dugmiciZaPoruku = MessageBoxButton.OKCancel;
                    MessageBoxImage slikaBoxa = MessageBoxImage.Question;
                    MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri prebacivanju rezervacije u korpu", dugmiciZaPoruku, slikaBoxa);
                    switch (rezultatBoxa)
                    {

                        case MessageBoxResult.OK:
                            DbItemOpremaSaParametrima[] nizOpremeZaKorpuPosleBrisanja = service.KorpaDelete(null, TrenutniProdavac.IdKorisnici);
                            this.PopuniKorpu(nizOpremeZaKorpuPosleBrisanja);
                            this.prebaciRezervacijuUKorpu();
                            break;
                        case MessageBoxResult.Cancel:
                            return;
                    }

                }
                else if(this.Korpa.Count == 0)
                {
                    this.prebaciRezervacijuUKorpu();
                }
            }
            else if(this.Korpa == null)
            {
                this.prebaciRezervacijuUKorpu();
            }

        }

        private void prebaciRezervacijuUKorpu()
        {
            if(this.TempRezervacija != null)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
                SmartSoftwareServiceReference.DbItemRezervacijeSelect[] oprema = service.PrebaciRezerevacijuUKorpu(this.TempRezervacija.IdRerezervacije, TrenutniProdavac.IdKorisnici);
                DaLiJeNestoPrebacenoUKorpu = true;

                if (tempGrid != null)
                {
                    TextBlock tbSkrivenIdRezervacijeTempGrida = tempGrid.FindName("rezervacijeSkrivenId") as TextBlock;
                    int idRezervacijeTempGrida = Convert.ToInt32(tbSkrivenIdRezervacijeTempGrida.Text);

                    for (int i = 0; i < Rezervacije.Count; i++)
                    {

                        if (Rezervacije[i].IdRerezervacije == idRezervacijeTempGrida)
                        {
                            TempRezervacija = Rezervacije[i];
                            CurrentRezervacije = Rezervacije[i];
                            break;
                        }
                    }


                    Border border5 = tempGrid.FindName("probaBorder") as Border;
                    Border border6 = tempGrid.FindName("probaBorder1") as Border;
                    Border border7 = tempGrid.FindName("probaBorder2") as Border;
                    Border border8 = tempGrid.FindName("probaBorder3") as Border;
                    TextBlock tblImeTemp = tempGrid.FindName("tblIme") as TextBlock;
                    TextBlock tblBrojTelefonaTemp = tempGrid.FindName("tblBrojTelefona") as TextBlock;
                    TextBlock tblDatumRezervacijeTemp = tempGrid.FindName("tblDatumRezervacije") as TextBlock;
                    TextBlock tblDatumIstekaRezervacijeTemp = tempGrid.FindName("tblDatumIstekaRezervacije") as TextBlock;
                    TextBlock tblDatumAzuriranjaRezervacijeTemp = tempGrid.FindName("tblDatumAzuriranjaRezervacije") as TextBlock;

                    border5.Background = Brushes.Transparent;
                    border6.Background = Brushes.Transparent;
                    border7.Background = Brushes.Transparent;
                    border8.Background = Brushes.Transparent;

                    tblImeTemp.Foreground = Brushes.Black;
                    tblBrojTelefonaTemp.Foreground = Brushes.Black;
                    tblDatumRezervacijeTemp.Foreground = Brushes.Black;
                    tblDatumIstekaRezervacijeTemp.Foreground = Brushes.Black;
                    tblDatumAzuriranjaRezervacijeTemp.Foreground = Brushes.Black;

                    CurrentRezervacije.KliknutoNaGrid = false;
                    TempRezervacija.KliknutoNaGrid = false;
                }

                tempGrid = null;
                this.CurrentRezervacije = this.TempRezervacija = null;
                this.CurrentOprema = null;
                skrolVjuverSveRezervacije.Visibility = Visibility.Hidden;


                this.PopuniRezervacije(oprema);
            }
        }

        

    }
}
