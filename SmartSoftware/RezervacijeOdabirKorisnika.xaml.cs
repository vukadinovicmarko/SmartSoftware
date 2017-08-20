using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for RezervacijeOdabirKorisnika.xaml
    /// </summary>
    public partial class RezervacijeOdabirKorisnika : Window, INotifyPropertyChanged
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        DoubleAnimation daTriTackeZaDetalje = new DoubleAnimation(0, 15, new TimeSpan(0, 0, 1));

        ObservableCollection<Oprema> listaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaOpreme
        {
            get { return listaOpreme; }
            set { SetAndNotify(ref listaOpreme, value); }
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { SetAndNotify(ref korpa, value); }
        }

        private Korisnici trenutniProdavac = new Korisnici();

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }


        private string putanjaDoSlikeKupi = App.rootPath + "\\slike\\checkout-128-Green.png";
        private string putanjaDoSlikeKupiHoverovano = App.rootPath + "\\slike\\checkout-128-White.png";
        private string putanjaDoSlikeKupiOnemoguceno = App.rootPath + "\\slike\\checkout-128-Gray.png";

        public string PutanjaDoSlikeKupiOnemoguceno
        {
            get { return putanjaDoSlikeKupiOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeKupiOnemoguceno, value); }
        }
        public string PutanjaDoSlikeKupi
        {
            get { return putanjaDoSlikeKupi; }
            set { SetAndNotify(ref putanjaDoSlikeKupi, value); }
        }
        public string PutanjaDoSlikeKupiHoverovano
        {
            get { return putanjaDoSlikeKupiHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeKupiHoverovano, value); }
        }

        private string putanjaDoSlikeDelete = App.rootPath + "\\slike\\delete-128-Red.png";
        private string putanjaDoSlikeDeleteHoverovano = App.rootPath + "\\slike\\delete-128-White.png";


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

        private string putanjaDoSlikeInfo = App.rootPath + "\\slike\\info-128-Blue.png";
        public string PutanjaDoSlikeInfo
        {
            get { return putanjaDoSlikeInfo; }
            set { SetAndNotify(ref putanjaDoSlikeInfo, value); }
        }

        private string putanjaDoSlikeInfoHoverovano = App.rootPath + "\\slike\\info-128-White.png";
        public string PutanjaDoSlikeInfoHoverovano
        {
            get { return putanjaDoSlikeInfoHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeInfoHoverovano, value); }
        }

        private bool kliknutoNaInfoIme = false;
        private bool kliknutoNaInfoBrTelefona = false;


        private string putanjaDoSlikeDodajRezervaciju = App.rootPath + "\\slike\\dodajRezervaciju-128-White.png";

        public string PutanjaDoSlikeDodajRezervaciju
        {
            get { return putanjaDoSlikeDodajRezervaciju; }
            set { SetAndNotify(ref putanjaDoSlikeDodajRezervaciju, value); }
        }

        private string putanjaDoSlikeDodajRezervacijuHoverovano = App.rootPath + "\\slike\\dodajRezervaciju-128-Blue.png";

        public string PutanjaDoSlikeDodajRezervacijuHoverovano
        {
            get { return putanjaDoSlikeDodajRezervacijuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeDodajRezervacijuHoverovano, value); }
        }

        
        private ObservableCollection<Rezervacije> rezervacije = new ObservableCollection<Rezervacije>();

        public ObservableCollection<Rezervacije> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
        }




        bool validacijaBrojaTelefona = false;
        bool validacijaImena = false;
        bool validacija = false;

        public bool Validacija
        {
            get { return validacija; }
            set { SetAndNotify(ref validacija, value); }
        }

        private Rezervacije tempRezervacija = new Rezervacije();

        public Rezervacije TempRezervacija
        {
            get { return tempRezervacija; }
            set { SetAndNotify(ref tempRezervacija, value); }
        }

        private Rezervacije currentRezervacije = new Rezervacije();

        public Rezervacije CurrentRezervacije
        {
            get { return currentRezervacije; }
            set { SetAndNotify(ref currentRezervacije, value); }
        }

        private ObservableCollection<Oprema> listaTrenutnihRezervacija = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> ListaTrenutnihRezervacija
        {
            get { return listaTrenutnihRezervacija; }
            set { SetAndNotify(ref listaTrenutnihRezervacija, value); }
        }


        private ObservableCollection<Oprema> currentOprema = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> CurrentOprema
        {
            get { return currentOprema; }
            set { SetAndNotify(ref currentOprema, value); }
        }


        public RezervacijeOdabirKorisnika(Korisnici prodavac)
        {
            InitializeComponent(); 
            this.DataContext = this;
            this.TrenutniProdavac = prodavac;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemRezervacijeSelect[] rezervacije = service.RezervacijeSelect();
            this.popuniRezervacije(rezervacije);
            ListaTrenutnihRezervacija = GlavniProzor.listaTrenutnihRezervacija;
        }



        private void popuniRezervacije(DbItemRezervacijeSelect[] ListaRezervacija)
        {
            this.Rezervacije = new ObservableCollection<Rezervacije>();

            for (int i = 0; i < ListaRezervacija.Length; i++)
            {
                Rezervacije r = new Rezervacije()
                {
                    DatumAzuriranjaRezervacije = ListaRezervacija[i].datum_azuriranja_rezervacije,
                    DatumIstekaRezervacije = ListaRezervacija[i].datum_isteka_rezervacije,
                    DatumRezervacije = ListaRezervacija[i].datum_rezervacije,
                    IdRerezervacije = ListaRezervacija[i].id_rezervacije,
                    Ime = ListaRezervacija[i].imeNaRezervacija
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
                        TmpIzabranaKolicina = o.kolicinaURezervacijama
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

                

                DbItemOpremaSaParametrimaStatistika opremaStatistika = (nizOpremaSaParametrima[i] as DbItemOpremaSaParametrimaStatistika);

                if (opremaStatistika != null)
                {
                    o.KolikoPutajeProdata = opremaStatistika.kolkoPutaJeProdavata;
                }


                ListaOpreme2.Add(o);

               

            }
            ListaOpreme = ListaOpreme2;
        }


        private void btnNapraviRezervaciju_Click(object sender, RoutedEventArgs e)
        {
            grdGornjiDeo.Visibility = Visibility.Hidden;
            grdGornjiDeo.Height = 0;

            sadrzajOdabirKorisnika.Visibility = Visibility.Hidden;
            sadrzajUnosNovogKorisnika.Visibility = Visibility.Visible;
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

        private void tbPretraga_KeyUp(object sender, KeyEventArgs e)
        {

        }
        Grid tempGrid;

        private void grdPrikazRezervacija_MouseDown(object sender, MouseButtonEventArgs e)
        {
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

            //gridPrikazRezervacija.Parent
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

            if (timerPrikazOpremePravilnoRasporedjeno == null)
                timerPrikazOpremePravilnoRasporedjeno = new DispatcherTimer();
            if (!timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
            {
                timerPrikazOpremePravilnoRasporedjeno.Interval = new TimeSpan(0, 0, 0, 0, 200);
                timerPrikazOpremePravilnoRasporedjeno.Tick += timerPrikazOpremePravilnoRasporedjeno_Tick;
                timerPrikazOpremePravilnoRasporedjeno.Start();
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
            
        }

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
        


        private void textBoxBrojTelefona_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text; //1234567

            if (!Regex.IsMatch(input, @"^((\+381)|(0))((6([0-7]|9))|([1-5][1-9]))(\/|\-)?(\d{6,7}|\d{3}(\/|\-)?\d{3,4}|\d{2}(\/|\-)?\d{2}(\/|\-)?\d{2,3}|\d{2,3}(\/|\-)?\d{2}(\/|\-)?\d{2})$"))
            {
                validacijaBrojaTelefona = false;
            }
            else validacijaBrojaTelefona = true;

            if (validacijaBrojaTelefona == true && validacijaImena == true) Validacija = true;
            else Validacija = false;
                
        }

        private void textBoxIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = (sender as TextBox).Text; //1234567

            if (!Regex.IsMatch(input, @"^[a-zA-Z]{2,}$"))
            {
                validacijaImena = false;
            }
            else validacijaImena = true;

            if (validacijaBrojaTelefona == true && validacijaImena == true) Validacija = true;
            else Validacija = false;
        }

        private void btnInfoIme_Click(object sender, RoutedEventArgs e)
        {
            if (!kliknutoNaInfoIme)
            {
                //textBoxIme.BorderThickness = new Thickness(0, 0, 0, 1);
                //brdTextBoxIme.BorderThickness = new Thickness(1, 1, 1, 0);
                //brdTextBoxIme.BorderBrush = Brushes.Black;
                brdInfoIme.Visibility = Visibility.Visible;
                brdInfoIme.Height = 40;
                btnInfoIme.SetResourceReference(StyleProperty, "dugmeInfoKliknuto");
                kliknutoNaInfoIme = true;
            }
            else
            {
                //textBoxIme.BorderThickness = new Thickness(0, 1, 0, 1);
                //brdTextBoxIme.BorderThickness = new Thickness(1, 0, 1, 0); 
                //brdTextBoxIme.BorderBrush = Brushes.Black;
                brdInfoIme.Visibility = Visibility.Hidden;
                brdInfoIme.Height = 0;
                btnInfoIme.SetResourceReference(StyleProperty, "dugmeInfo");
                kliknutoNaInfoIme = false;
            }
        }

        private void btnInfoBrTelefona_Click(object sender, RoutedEventArgs e)
        {
            if (!kliknutoNaInfoBrTelefona)
            {
                //textBoxBrojTelefona.BorderThickness = new Thickness(0, 0, 0, 1);
                //brdTextBoxBrTelefona.BorderThickness = new Thickness(1, 1, 1, 0);
                //brdTextBoxBrTelefona.BorderBrush = Brushes.Black;
                brdInfoBrTelefona.Visibility = Visibility.Visible;
                brdInfoBrTelefona.Height = 135;
                btnInfoBrTelefona.SetResourceReference(StyleProperty, "dugmeInfoKliknuto");
                kliknutoNaInfoBrTelefona = true;
            }
            else
            {
                //textBoxBrojTelefona.BorderThickness = new Thickness(0, 1, 0, 1);
                //brdTextBoxBrTelefona.BorderThickness = new Thickness(1, 0, 1, 0);
                //brdTextBoxBrTelefona.BorderBrush = Brushes.Black;
                brdInfoBrTelefona.Visibility = Visibility.Hidden;
                brdInfoBrTelefona.Height = 0;
                btnInfoBrTelefona.SetResourceReference(StyleProperty, "dugmeInfo");
                kliknutoNaInfoBrTelefona = false;
            }
        }

        private void popuniKorpu(DbItemOpremaSaParametrima[] oprema)
        {
            this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

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
                    IzabranaKolicina = oprema[i].kolicinaUKorpi

                });



                
            }
        }
        private bool prekiniFor = false;
        private bool prekiniDrugiFor = false;
        private bool prekiniDrugiForNastavi = false;
        private void btnRezervisiNaIme_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            DbItemOpremaSaParametrima[] nizOpremeNultiPut = service.OpremaSaParametrimaAdminPanel();
            this.popuniListuOpremeSaParametrima(nizOpremeNultiPut);
            
             List <DbItemOprema> ListaOpremeZaRezervisanje  = new List<DbItemOprema>();

             if (ListaTrenutnihRezervacija != null && ListaTrenutnihRezervacija.Count > 0)
             {
                 for (int i = 0; i < ListaTrenutnihRezervacija.Count; i++)
                 {
                    if (ListaOpreme != null && ListaOpreme.Count > 0)
                     {
                         for (int j = 0; j < ListaOpreme.Count; j++)
                         {
                             if (ListaTrenutnihRezervacija[i].IdOprema == ListaOpreme[j].IdOprema)
                             {
                                 if (ListaOpreme[j].DeletedItem)
                                 {
                                     string poruka = "Rezervacija pojedine opreme nije moguća.\n\nOprema :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + "),\n\nje u međuvremenu izbrisana.";


                                     MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                                     MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška pri rezervisanju", MessageBoxButton.OK, slikaBoxa);
                                     this.ListaTrenutnihRezervacija.RemoveAt(i);
                                     break;
                                 }
                                 else
                                     break;
                             }
                         }
                     }
                 }
             }

             DbItemOpremaSaParametrima[] nizOpreme = service.OpremaSaParametrimaAdminPanel();
             this.popuniListuOpremeSaParametrima(nizOpreme);

             if (ListaTrenutnihRezervacija != null && ListaTrenutnihRezervacija.Count > 0)
             {
                 for (int i = 0; i < ListaTrenutnihRezervacija.Count; i++)
                 {





                     if (ListaOpreme != null && ListaOpreme.Count > 0)
                     {
                         for (int j = 0; j < ListaOpreme.Count; j++)
                         {
                             if (ListaTrenutnihRezervacija[i].IdOprema == ListaOpreme[j].IdOprema)
                             {
                                 if (ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje > ListaOpreme[j].KolicinaNaLageru)
                                 {
                                     DbItemOpremaSaParametrima[] nizOpremeZaKorpu = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
                                     this.popuniKorpu(nizOpremeZaKorpu);

                                     if (this.Korpa.Count > 0)
                                     {
                                         for (int k = 0; k < this.Korpa.Count; k++)
                                         {
                                             if ((this.Korpa[k] as Oprema).IdOprema == ListaTrenutnihRezervacija[i].IdOprema)
                                             {
                                                 if ((this.Korpa[k] as Oprema).IzabranaKolicina >= ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje)
                                                 {

                                                     string poruka = "Rezervacija pojedine opreme nije moguća.\n\nKoličina koju želite da rezervišete je veća od one koja se nalazi na lageru.\nMeđutim, u korpi već postoji oprema koju želite da rezervišete :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + ")\n\nDa li biste želeli da se automatski obriše ova oprema iz korpe kako biste mogli i nju da rezervišete?";

                                                     MessageBoxButton dugmiciZaPoruku = MessageBoxButton.YesNoCancel;
                                                     MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                                                     MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška u toku rezervisanja opreme", dugmiciZaPoruku, slikaBoxa);
                                                     switch (rezultatBoxa)
                                                     {

                                                         case MessageBoxResult.Yes:
                                                             DbItemOpremaSaParametrima[] nizOpremeZaKorpuPosleBrisanja = service.KorpaDelete((this.Korpa[k] as Oprema).IdOprema, TrenutniProdavac.IdKorisnici);
                                                             this.popuniKorpu(nizOpremeZaKorpuPosleBrisanja);
                                                             prekiniDrugiFor = true;
                                                             break;
                                                         case MessageBoxResult.No:
                                                             prekiniDrugiFor = true;
                                                             prekiniDrugiForNastavi = true;
                                                             break;
                                                         case MessageBoxResult.Cancel:
                                                             return;
                                                     }
                                                     break;
                                                 }
                                                 else
                                                     break;
                                             }
                                             else if (this.Korpa.Count == k + 1)
                                             {
                                                 if (this.ListaOpreme[j].KolicinaNaLageru > 0)
                                                 {
                                                     string poruka = "Rezervacija pojedine opreme nije moguća.\n\nZa opremu :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + "),\n\nkoličina koju želite da rezervišete (" + ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje.ToString() + ") je veća od one koja se nalazi na lageru. Da li želite da iskoristite onu količinu koja je dostupna (" + ListaOpreme[j].KolicinaNaLageru.ToString() + ")?";

                                                     MessageBoxButton dugmiciZaPoruku = MessageBoxButton.YesNo;
                                                     MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                                                     MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška u toku rezervisanja opreme", dugmiciZaPoruku, slikaBoxa);
                                                     switch (rezultatBoxa)
                                                     {

                                                         case MessageBoxResult.Yes:
                                                             ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje = Convert.ToInt32(ListaOpreme[j].KolicinaNaLageru);
                                                             prekiniDrugiFor = true;
                                                             break;
                                                         case MessageBoxResult.No:
                                                             prekiniDrugiFor = true;
                                                             prekiniDrugiForNastavi = true;
                                                             break;

                                                     }

                                                 }
                                                 else
                                                 {

                                                     string poruka = "Rezervacija pojedine opreme nije moguća.\n\nZa opremu :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + "),\n\nkoličina koju želite da rezervišete je veća od one koja se nalazi na lageru.\nNaručite još sa lagera, ili kontaktirajte administratora, kako biste ponovo mogli da rezervišete ovu opremu";


                                                     MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                                                     MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška u toku rezervisanja opreme", MessageBoxButton.OKCancel, slikaBoxa);

                                                     switch (rezultatBoxa)
                                                     {
                                                         case MessageBoxResult.OK:
                                                             this.ListaTrenutnihRezervacija.RemoveAt(i);
                                                             break;
                                                         case MessageBoxResult.Cancel:
                                                             return;


                                                     }
                                                     prekiniDrugiFor = true;
                                                     prekiniFor = true;
                                                     break;
                                                 }
                                             }
                                         }
                                     }
                                     else
                                     {
                                         if (this.ListaOpreme[j].KolicinaNaLageru > 0)
                                         {
                                             string poruka = "Rezervacija pojedine opreme nije moguća.\n\nZa opremu :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + "),\n\nkoličina koju želite da rezervišete (" + ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje.ToString() + ") je veća od one koja se nalazi na lageru. Da li želite da iskoristite onu količinu koja je dostupna (" + ListaOpreme[j].KolicinaNaLageru.ToString() + ")?";

                                             MessageBoxButton dugmiciZaPoruku = MessageBoxButton.YesNo;
                                             MessageBoxImage slikaBoxa = MessageBoxImage.Warning;
                                             MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška u toku rezervisanja opreme", dugmiciZaPoruku, slikaBoxa);
                                             switch (rezultatBoxa)
                                             {

                                                 case MessageBoxResult.Yes:
                                                     ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje = Convert.ToInt32(ListaOpreme[j].KolicinaNaLageru);
                                                     prekiniDrugiFor = true;
                                                     break;
                                                 case MessageBoxResult.No:
                                                     prekiniDrugiFor = true;
                                                     prekiniDrugiForNastavi = true;
                                                     break;

                                             }

                                         }
                                         else
                                         {

                                             string poruka = "Rezervacija pojedine opreme nije moguća.\n\nZa opremu :\n(" + ListaTrenutnihRezervacija[i].Name.ToString() + "),\n\nkoličina koju želite da rezervišete je veća od one koja se nalazi na lageru.\nNaručite još sa lagera, ili kontaktirajte administratora, kako biste ponovo mogli da rezervišete ovu opremu";


                                             MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                                             MessageBoxResult rezultatBoxa = MessageBox.Show(poruka, "Greška u toku rezervisanja opreme", MessageBoxButton.OKCancel, slikaBoxa);

                                             switch (rezultatBoxa)
                                             {
                                                 case MessageBoxResult.OK:
                                                     this.ListaTrenutnihRezervacija.RemoveAt(i);
                                                     break;
                                                 case MessageBoxResult.Cancel:
                                                     return;


                                             }
                                             prekiniDrugiFor = true;
                                             prekiniFor = true;
                                             break;
                                         }
                                     }
                                 }
                                 else
                                 {
                                     break;
                                 }
                             }
                             if (prekiniDrugiFor)
                             {
                                 if (prekiniDrugiForNastavi)
                                 {
                                     prekiniDrugiForNastavi = false;
                                     prekiniDrugiFor = false;
                                     prekiniFor = true;
                                 }
                                 if (prekiniFor)
                                 {
                                     prekiniDrugiFor = false;
                                     break;
                                 }

                                 j = -1;
                                 prekiniDrugiFor = false;
                                 DbItemOpremaSaParametrima[] nizOpremePonovo = service.OpremeSaParametrimaGlavniProzor(null);
                                 this.popuniListuOpremeSaParametrima(nizOpremePonovo);
                                 continue;

                             }
                         }
                     }
                     if (prekiniFor)
                     {
                         prekiniFor = false;
                         continue;
                     }

                     DbItemOprema oprema = new DbItemOprema()
                     {
                         id_oprema = ListaTrenutnihRezervacija[i].IdOprema,
                         kolicina_rezervisane_opreme = Convert.ToInt32(ListaTrenutnihRezervacija[i].IzabranaKolicinaZaRezervisanje)
                     };

                     ListaOpremeZaRezervisanje.Add(oprema);
                 }
             }


            if (ListaOpremeZaRezervisanje != null && ListaOpremeZaRezervisanje.Count > 0)
            {

                DbItemRezervacijeInsert nizRezervacijaSaOpremom = new DbItemRezervacijeInsert()
                {
                    datum_azuriranja_rezervacije = DateTime.Now,
                    datum_rezervacije = DateTime.Now,
                    //DatumIstekaRezervacije = DateTime.Now,
                    imeNaRezervacija = textBoxIme.Text,
                    brojTelefona = textBoxBrojTelefona.Text,


                    ListaOpremeZaRezervaciju = ListaOpremeZaRezervisanje.ToArray()
                };


                RezervacijePregledProzor.daLiJeIzvrsenaRezervacija = service.RezervacijeInsert(nizRezervacijaSaOpremom);
            }

            if (RezervacijePregledProzor.daLiJeIzvrsenaRezervacija) this.Close();


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

        

        void timerPrikazOpremePravilnoRasporedjeno_Tick(object sender, EventArgs e)
        {
            if (timerPrikazOpremePravilnoRasporedjeno.IsEnabled)
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



        private void btnOtkaziUnosNovogKorisnikaZaRezervisanje_Click(object sender, RoutedEventArgs e)
        {
            grdGornjiDeo.Visibility = Visibility.Visible;
            grdGornjiDeo.Height = 80;

            sadrzajOdabirKorisnika.Visibility = Visibility.Visible;
            sadrzajUnosNovogKorisnika.Visibility = Visibility.Hidden;

            textBoxIme.Text = "";
            textBoxBrojTelefona.Text = "";

            brdInfoIme.Visibility = Visibility.Hidden;
            brdInfoIme.Height = 0;
            btnInfoIme.SetResourceReference(StyleProperty, "dugmeInfo");
            kliknutoNaInfoIme = false;

            brdInfoBrTelefona.Visibility = Visibility.Hidden;
            brdInfoBrTelefona.Height = 0;
            btnInfoBrTelefona.SetResourceReference(StyleProperty, "dugmeInfo");
            kliknutoNaInfoBrTelefona = false;

            Validacija = false;
        }

        private void btnRezervisiUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Da li ste sigurni da želite da rezervišete?");
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

        
    }
}
