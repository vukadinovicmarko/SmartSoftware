using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SmartSoftware.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        private string tekstZaDobrodoslicu = "Dobrodošao/la, ";

        public string TekstZaDobrodoslicu
        {
            get { return tekstZaDobrodoslicu; }
            set { SetAndNotify(ref tekstZaDobrodoslicu, value); }
        }

        //if (TrenutniProdavac != null)
        //        {
        //            if (this.TrenutniProdavac.PolKorisnika)
        //            {
        //                return "Dobrodošao, ";
        //            }
        //            else
        //            {
        //                return "Dobrodošla, ";
        //            }
        //        }
        //        else
        //        {
        //            return "Dobrodošao/la, ";
        //        }

        private bool praznoPoljeKorisnickoIme = true;

        public bool PraznoPoljeKorisnickoIme
        {
            get { return praznoPoljeKorisnickoIme; }
            set { SetAndNotify(ref praznoPoljeKorisnickoIme, value); }
        }
        private bool praznoPoljeLozinka = true;

        public bool PraznoPoljeLozinka
        {
            get { return praznoPoljeLozinka; }
            set { SetAndNotify(ref praznoPoljeLozinka, value); }
        }

        private Korisnici trenutniProdavac;

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }



        private ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }


        //Window mainWindow = this;
         private DispatcherTimer timer = new DispatcherTimer
    (   
    TimeSpan.FromSeconds(5),
    DispatcherPriority.ApplicationIdle,// Or DispatcherPriority.SystemIdle

    (s, e) => {
        //SystemSounds.Beep.Play(); Type t = s.GetType(); MessageBox.Show(e.ToString());
    }, // or something similar
    Application.Current.Dispatcher
    );

         DispatcherTimer timerOsveziPodatkeOKorisniku = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            //SmartSoftwareServiceReference.DbItemOblastiOpreme[] niz = service.OblastiOpreme();

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.PrikaziZaposleneKorisnike(null);

            this.popuniListuKorisnika(nizKorisnika);

            //timerOsveziPodatkeOKorisniku.Interval = new TimeSpan(0, 1, 0);
            //timerOsveziPodatkeOKorisniku.Tick+=timerOsveziPodatkeOKorisniku_Tick;
            //timerOsveziPodatkeOKorisniku.Start();
        }

        //private void timerOsveziPodatkeOKorisniku_Tick(object sender, EventArgs e)
        //{
        //    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

        //    SmartSoftwareServiceReference.DbItemOblastiOpreme[] niz = service.OblastiOpreme();

        //    //SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.PrikaziKorisnike(null);

        //    //this.popuniListuKorisnika(nizKorisnika);
            
        //}

        

        void timer_Tick(object sender, EventArgs e)
        {
            // Metoda timer_Tick će se izvršavati na svakih 40ms, tj. 25 puta u sekundi.
            // U ovoj metodi će biti osvežavan prikaz proteklog vremena štoperice.
            //txtDisplay.Text = stopwatch.Elapsed.ToString(txtDisplayFormat);
        }


        private void popuniListuKorisnika(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnika = new ObservableCollection<Korisnici>();
            foreach (var item in nizKorisnika)
            {
                Korisnici korisnik = new Korisnici()
                {
                    IdKorisnici = item.id_korisnici,
                    IdUloge = item.id_uloge,
                    IzabranaUloga = item.id_uloge - 1,
                    ImeKorisnika = item.ime,
                    PrezimeKorisnika = item.prezime,
                    MejlKorisnika = item.mejl,
                    BrojTelefonaKorisnika = item.broj_telefona,
                    BrojOstvarenihPoena = Convert.ToDouble(item.brojOstvarenihPoena),
                    Username = item.username,
                    Lozinka = item.lozinka,
                    NazivUloge = item.naziv_uloge,
                    PolKorisnika = item.polKorisnika,
                    DeletedItem = item.deletedField
                };
                ListaKorisnika.Add(korisnik);
            }
            
        }
        DispatcherTimer timerCestitke = new DispatcherTimer();
        private void btnUlogujSe_Click(object sender, RoutedEventArgs e)
        {


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            //SmartSoftwareServiceReference.DbItemOblastiOpreme[] niz = service.OblastiOpreme();

            SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.PrikaziZaposleneKorisnike(null);

            this.popuniListuKorisnika(nizKorisnika);

            string textBoxUser = textBoxKorisnickoIme.Text;
            string textBoxPass = passwordBoxLozinka.Password;
            int brojac = 0;





            foreach (var item in ListaKorisnika)
            {

                if (textBoxUser == item.Username && textBoxPass == item.Lozinka)
                {
                    
                    if (item.IdUloge != 1 && item.IdUloge != 2)
                    {
                        MessageBox.Show("morate se ulogovati kao administrator ili prodavac");
                    }
                    else
                    {
                        brojac++;
                        TrenutniProdavac = item;
                        if (TrenutniProdavac != null)
                        {
                            if (this.TrenutniProdavac.PolKorisnika != null && (bool)this.TrenutniProdavac.PolKorisnika)
                            {
                                TekstZaDobrodoslicu = "Dobrodošao, " + TrenutniProdavac.ImeKorisnika;
                            }
                            else
                            {
                                TekstZaDobrodoslicu = "Dobrodošla, " + TrenutniProdavac.ImeKorisnika;
                            }
                        }
                        else
                        {
                            TekstZaDobrodoslicu = "Dobrodošao/la, ";
                        }
                        if (!timerCestitke.IsEnabled)
                        {
                            timerOsveziPodatkeOKorisniku.Stop();
                            grdCestitke.Visibility = Visibility.Visible;

                            ThicknessAnimation taDugmiciZaProzorMargina = new ThicknessAnimation(new Thickness(0, -100, 0, 0), new TimeSpan(0, 0, 1));
                            grdDugmiciZaProzor.BeginAnimation(Grid.MarginProperty, taDugmiciZaProzorMargina);

                            DoubleAnimation dA2 = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                            grdDugmiciZaProzor.BeginAnimation(Grid.OpacityProperty, dA2);
                            


                            ThicknessAnimation taSadrzaj = new ThicknessAnimation(new Thickness(-1050, 0, 0, 0), new TimeSpan(0, 0, 2));
                            grdSadrzaj.BeginAnimation(Grid.MarginProperty, taSadrzaj);


                            DoubleAnimation daSadrzajOpacity = new DoubleAnimation(0, new TimeSpan(0, 0, 1));
                            grdSadrzaj.BeginAnimation(Border.OpacityProperty, daSadrzajOpacity);

                            ThicknessAnimation taCestitke = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 2));
                            grdCestitke.BeginAnimation(Grid.MarginProperty, taCestitke);

                            timerCestitke.Interval = new TimeSpan(0, 0, 5);
                            timerCestitke.Tick += timerCestitke_Tick;
                            timerCestitke.Start();
                        }
                        
                        
                    }
                    break;
                }
            }

            if (brojac == 0)
            {
                brdGreskaLogin.Visibility = Visibility.Visible;
                ThicknessAnimation taBrdGreskaLogin = new ThicknessAnimation(new Thickness(25, 0, 0, 0), new Thickness(0), new TimeSpan(0, 0, 1));
                brdGreskaLogin.BeginAnimation(Border.MarginProperty, taBrdGreskaLogin);
            }

           

                
        }

        void timerCestitke_Tick(object sender, EventArgs e)
        {
            grdDugmiciZaProzor.Visibility = Visibility.Hidden;
            GlavniProzor glavni = new GlavniProzor(TrenutniProdavac);

            glavni.Show();
            timerCestitke.Stop();
            
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
        DispatcherTimer loginTimer = new DispatcherTimer();
        DispatcherTimer loginTimer2 = new DispatcherTimer();
        private void titlebar_Loaded(object sender, RoutedEventArgs e)
        {
            //grdSlikaINazivTitla
            ControlTemplate ctpTemp = ctlTitleBar.Template;
            Grid grdSlikaINazivTitla = ctpTemp.FindName("grdSlikaINazivTitla", ctlTitleBar) as Grid;

            DoubleAnimation dALoaded = new DoubleAnimation(375, new TimeSpan(0, 0, 2));
            ctlTitleBar.BeginAnimation(ContentControl.HeightProperty, dALoaded);
            DoubleAnimation dA1 = new DoubleAnimation(1, new TimeSpan(0, 0, 2));
            grdSlikaINazivTitla.BeginAnimation(Grid.OpacityProperty, dA1);
            loginTimer.Interval = new TimeSpan(0, 0, 0, 3);
            loginTimer.Tick += loginTimer_Tick;
            loginTimer.Start();
        }

        void loginTimer_Tick(object sender, EventArgs e)
        {

            

            ctlTitleBar.SetResourceReference(TemplateProperty, "ctpAnimacijaKraj");
            
            loginTimer.Stop();
            if (!loginTimer.IsEnabled)
            {
                loginTimer2.Interval = new TimeSpan(0, 0, 0, 1);
                loginTimer2.Tick += loginTimer2_Tick;
                loginTimer2.Start();
            }

        }
        void loginTimer2_Tick(object sender, EventArgs e)
        {
           

            DoubleAnimation dA = new DoubleAnimation(75, new TimeSpan(0, 0, 1));
            ctlTitleBar.BeginAnimation(ContentControl.HeightProperty, dA);
            grdDugmiciZaProzor.Visibility = Visibility.Visible;

            DoubleAnimation dA2 = new DoubleAnimation(1, new TimeSpan(0, 0, 2));
            grdDugmiciZaProzor.BeginAnimation(Grid.OpacityProperty, dA2);

            ThicknessAnimation tA2grdDugmiciZaProzorMargina = new ThicknessAnimation(new Thickness(0), new TimeSpan(0, 0, 2));
            grdDugmiciZaProzor.BeginAnimation(Grid.MarginProperty, tA2grdDugmiciZaProzorMargina);

            rctShadow.Visibility = Visibility.Visible;
            DoubleAnimation dArctShadow = new DoubleAnimation(1, new TimeSpan(0, 0, 2));
            rctShadow.BeginAnimation(Rectangle.OpacityProperty, dArctShadow);

            loginTimer2.Stop();
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ctlTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }


        private bool logovanjeOmoguceno = false;

        public bool LogovanjeOmoguceno
        {
            get { return logovanjeOmoguceno; }
            set { SetAndNotify(ref logovanjeOmoguceno, value); }
        }


        

        private void textBoxKorisnickoIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tvKorisnickoIme = (TextBox)sender;
            if (tvKorisnickoIme.Text != "")
            {
                tblckKorisnickoIme.Visibility = Visibility.Hidden;
                PraznoPoljeKorisnickoIme = false;
            }
            else
            {
                tblckKorisnickoIme.Visibility = Visibility.Visible;
                PraznoPoljeKorisnickoIme = true;
            }

            brdGreskaLogin.Visibility = Visibility.Hidden;
           

            if (!PraznoPoljeKorisnickoIme && !PraznoPoljeLozinka)
                LogovanjeOmoguceno = true;
            else
                LogovanjeOmoguceno = false;
        }

        private void passwordBoxLozinka_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox psBox = (PasswordBox)sender;
            if (psBox.Password != "")
            {
                tblckLozinka.Visibility = Visibility.Hidden;
                PraznoPoljeLozinka = false;
            }
            else
            {
                tblckLozinka.Visibility = Visibility.Visible;
                PraznoPoljeLozinka = true;
            }

            brdGreskaLogin.Visibility = Visibility.Hidden;
            

            if (!PraznoPoljeKorisnickoIme && !PraznoPoljeLozinka)
                LogovanjeOmoguceno = true;
            else
                LogovanjeOmoguceno = false;
        }

    }
}