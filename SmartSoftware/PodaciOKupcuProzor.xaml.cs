using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for PodaciOKupcuProzor.xaml
    /// </summary>
    public partial class PodaciOKupcuProzor : Window, INotifyPropertyChanged
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        Korisnici stariKorisnik = null;



        int idKorisnici = 0;
        ControlTemplate ctpKorisnici = new ControlTemplate();

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

        ObservableCollection<Korisnici> listaKorisnika = new ObservableCollection<Korisnici>();

        public ObservableCollection<Korisnici> ListaKorisnika
        {
            get { return listaKorisnika; }
            set { SetAndNotify(ref listaKorisnika, value); }
        }

        private Korisnici trenutniProdavac;

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }


        public PodaciOKupcuProzor(Korisnici prodavac)
        {
            InitializeComponent();
            this.DataContext = this;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = service.PrikaziKorisnike(null);
            this.popuniListuKorisnici(korisnici);

            this.TrenutniProdavac = prodavac;
        }

        private bool daLiJeOdabranKorisnik = false;

        public  bool DaLiJeOdabranKorisnik
        {
            get { return daLiJeOdabranKorisnik; }
            set { SetAndNotify(ref daLiJeOdabranKorisnik, value); }
        }

        private Korisnici currentKorisnici;

        public Korisnici CurrentKorisnici
        {
            get { return currentKorisnici; }
            set { SetAndNotify(ref currentKorisnici, value); }
        }

        private void GridPrikazRezultataKorisnika_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            Grid gIzabranKorisnik = sender as Grid;
            TextBlock t = gIzabranKorisnik.FindName("skrivenId") as TextBlock;

            
            
            for (int i = 0; i < ListaKorisnika.Count; i++)
			{
			    if(ListaKorisnika[i].IdKorisnici == Int32.Parse(t.Text))
                {
                    this.CurrentKorisnici = ListaKorisnika[i];

                    break;
                }
			}

            //grdUnosKorisnika.Visibility = Visibility.Hidden;
            //grdPrikazDetaljaKorisnika.Visibility = Visibility.Visible;
            
        }

        //private void popuniListuKorisnika(DbItemKorisnici[] korisnici)
        //{
        //    this.ListaKorisnika.Clear();
        //    foreach (var korisnik in korisnici)
        //    {
        //        this.ListaKorisnika.Add
        //            (new Korisnici()
        //            {
        //                BrojOstvarenihPoena = Convert.ToDouble(korisnik.brojOstvarenihPoena),
        //                PrezimeKorisnika = korisnik.prezime,
        //                MejlKorisnika = korisnik.mejl,
        //                IdKorisnici = korisnik.id_korisnici,
        //                BrojTelefonaKorisnika = korisnik.broj_telefona,
        //                ImeKorisnika = korisnik.ime,
        //                Lozinka = korisnik.lozinka,
        //                Username = korisnik.username
        //            });
        //    }
        //}

        //private void TextBox_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string zaPretragu = (sender as TextBox).Text;
        //    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
        //    SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = null;
        //    if(zaPretragu == null || zaPretragu == "")
        //    {
        //        korisnici = service.PrikaziKorisnike(null);
        //    }
        //    else
        //    {
        //        korisnici = service.PretragaKorisnika(zaPretragu);
            
        //    }
        //    this.popuniListuKorisnika(korisnici);
        //}

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

        //private void btnRegistracijaKorisnika_Click(object sender, RoutedEventArgs e)
        //{
        //    SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
        //    DbItemKorisnici korisnik = new DbItemKorisnici()
        //    {
        //        ime = tmpKorisnik.ImeKorisnika,
        //        prezime = tmpKorisnik.PrezimeKorisnika,
        //        mejl = tmpKorisnik.MejlKorisnika,
        //        broj_telefona = tmpKorisnik.BrojTelefonaKorisnika,
        //        id_uloge = 3
        //    };
        //    SmartSoftwareServiceReference.DbItemKorisnici[] korisnici = service.OpKorisniciInsert(korisnik);
        //    this.ListaKorisnika.Add(tmpKorisnik);
        //    this.popuniListuKorisnika(korisnici);
        //    this.TmpKorisnik = new Korisnici();
        //}

        private void btnIzaberiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            //ICollectionView documentsView = CollectionViewSource.GetDefaultView(this.ListaKorisnika);

            //Korisnici korisnik = documentsView.CurrentItem as Korisnici;
            //this.CurrentKorisnici = korisnik;
           

            daLiJeOdabranKorisnik = true;


           
            this.Close();
            
        }

        private void popuniListuKorisnici(DbItemKorisnici[] nizKorisnika)
        {
            ListaKorisnika = null;
            ListaKorisnika = new ObservableCollection<Korisnici>();
            for (int i = 0; i < nizKorisnika.Length; i++)
            {
                ListaKorisnika.Add(new Korisnici()
                {
                    IdKorisnici = nizKorisnika[i].id_korisnici,
                    IdUloge = nizKorisnika[i].id_uloge,
                    IzabranaUloga = nizKorisnika[i].id_uloge,
                    ImeKorisnika = nizKorisnika[i].ime,
                    PrezimeKorisnika = nizKorisnika[i].prezime,
                    MejlKorisnika = nizKorisnika[i].mejl,
                    ImeIPrezimeKorisnika = nizKorisnika[i].ime + " " + nizKorisnika[i].prezime,
                    BrojTelefonaKorisnika = nizKorisnika[i].broj_telefona,
                    BrojOstvarenihPoena = Convert.ToDouble(nizKorisnika[i].brojOstvarenihPoena),
                    Username = nizKorisnika[i].username,
                    Lozinka = nizKorisnika[i].lozinka,
                    SlikaKorisnika = nizKorisnika[i].slikaKorisnika == null ? nizKorisnika[i].id_uloge == 1 ? App.PutanjaDoSlikeAdministratorKorisnici : App.PutanjaDoSlikeProdavacKorisnici : nizKorisnika[i].slikaKorisnika,
                    NazivUloge = nizKorisnika[i].naziv_uloge,
                    DeletedItem = nizKorisnika[i].deletedField
                });
            }
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
        }

        
        private void btnUnesiNovogKorisnika_Click(object sender, RoutedEventArgs e)
        {
            //grdPrikazDetaljaKorisnika.Visibility = Visibility.Hidden;
            //grdUnosKorisnika.Visibility = Visibility.Visible;
            noviKorisnik_Click();
        }

        private void btnRegistracijaKorisnika_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentKorisnici != null)
            {
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemKorisnici korisnik = new DbItemKorisnici()
                {
                    ime = this.CurrentKorisnici.ImeKorisnika,
                    prezime = this.CurrentKorisnici.PrezimeKorisnika,
                    id_uloge = 3,
                    mejl = this.CurrentKorisnici.MejlKorisnika,
                    broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                    //brojOstvarenihPoena = (int)(this.CurrentKorisnici.BrojOstvarenihPoena),
                    username = this.CurrentKorisnici.Username,
                    lozinka = this.CurrentKorisnici.Lozinka
                    //naziv_uloge = this.CurrentKorisnici.NazivUloge
                };

                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.OpKorisniciInsert(korisnik);
                //grdUnosKorisnika.Visibility = Visibility.Hidden;
                //grdPrikazDetaljaKorisnika.Visibility = Visibility.Visible;
                this.popuniListuKorisnici(nizKorisnika);
            }
            else
            {
                MessageBox.Show("Popunite sva polja");
            }
        }

        private void btnOtkaziUnos_Click(object sender, RoutedEventArgs e)
        {
            //grdPrikazDetaljaKorisnika.Visibility = Visibility.Hidden;
            //grdUnosKorisnika.Visibility = Visibility.Hidden;
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

        private void Window_StateChanged(object sender, EventArgs e)
        {

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
                        

                    }
                }









            }

        }

        void timerKorisniciRasporediPrikazDetaljaPravilnoHederVratiUNormalu_Tick(object sender, EventArgs e)
        {




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



        }
        Grid TrenutniGridTriVjuKorisnici = null;
        private DispatcherTimer timerOblastiOpremeRasporediPrikazDetaljaPravilno;
        private DispatcherTimer timerKorisniciRasporediPrikazDetaljaPravilno;
        private DispatcherTimer timerPrikaziDetaljeKorisnika;
        private bool daLiSePrekinuoTajmerPrikaziDetaljeKorisnika = true;
        
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

            TextBox tboxImeKorisnika = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxImeKorisnika") as TextBox;
            TextBox tboxPrezimeKorisnika = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxPrezimeKorisnika") as TextBox;
            TextBox tboxBrojTelefona = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxBrojTelefona") as TextBox;
            TextBox tboxEmail = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxEmail") as TextBox;




            tboxImeKorisnika.IsReadOnly = true;
            tboxPrezimeKorisnika.IsReadOnly = true;
            tboxBrojTelefona.IsReadOnly = true;
            tboxEmail.IsReadOnly = true;


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
                            //grdDatumAzuriranjaKorisnici.Visibility = grdDatumKreiranjaKorisnici.Visibility = Visibility.Visible;
                            //grdDatumKreiranjaKorisnici.Height = grdDatumAzuriranjaKorisnici.Height = 20;
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

        private bool dalijeUnosUToku = false;

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
            LejautDokumentTitlePrikazDetaljaKorisnici.Title = "PRIKAZ DETALJA";
        }

        void timerKorisniciRasporediPrikazDetaljaPravilno_Tick(object sender, EventArgs e)
        {


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

        }

        private bool daLiSePrekinuoTajmerKorisnici = true;
        void timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja_Tick(object sender, EventArgs e)
        {
           

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

        
        private void brdPrikazDetaljaKorisnici_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Grid grdPrikazDetaljaKorisnici;
            Grid grdPromenaVisine;
            Border brdKorisniciImeIPrezimeNaslov;
            ContentControl cclPrikazHederaImePrezimeKorisnici;
            TextBox tboxPrezimeKorisnika;

            
                
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




        }

        private void pravilnoRasporediListuKorisnika()
        {
            
               

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

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
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

            
                    if (timerPrikaziSakrijPretragu.IsEnabled)
                    {
                        DispatcherTimer timerSender = (DispatcherTimer)sender;
                        timerSender.Stop();
                        DoubleAnimation daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, new TimeSpan(0, 0, 0, 0, 500));
                        if (brdPretragaIUnosKorisnici.Height == 45)
                            daBrdPretragaPrikaziPolaSekunde = new DoubleAnimation(45, 0, new TimeSpan(0, 0, 0, 0, 500));
                        brdPretragaIUnosKorisnici.BeginAnimation(Border.HeightProperty, daBrdPretragaPrikaziPolaSekunde);
                    }
            


            timerPrikaziSakrijPretragu.Stop();
        }

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
                //grdDatumAzuriranjaKorisnici.Visibility = grdDatumKreiranjaKorisnici.Visibility = Visibility.Hidden;
                //grdDatumKreiranjaKorisnici.Height = grdDatumAzuriranjaKorisnici.Height = 0;
            }

            TextBox tboxImeKorisnika = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxImeKorisnika") as TextBox;
            TextBox tboxPrezimeKorisnika = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxPrezimeKorisnika") as TextBox;
            TextBox tboxBrojTelefona = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxBrojTelefona") as TextBox;
            TextBox tboxEmail = brdPrikazDetaljaKorisniciSadrzaj.FindName("tboxEmail") as TextBox;




            tboxImeKorisnika.IsReadOnly = false;
            tboxPrezimeKorisnika.IsReadOnly = false;
            tboxBrojTelefona.IsReadOnly = false;
            tboxEmail.IsReadOnly = false;

            



            //Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
            //Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
            //Border brdPrikazDetaljaKorisniciSadrzaj = grdPrikazDetaljaKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj") as Border;

            //ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
            //TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

            this.CurrentKorisnici = null;
            this.stariKorisnik = null;
            this.CurrentKorisnici = new Korisnici()
            {
                SlikaKorisnika = App.PutanjaDoSlikeAdministratorKorisnici,
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


        private void imgOblastOpremeDodaj_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
                    noviKorisnik_Click();
                  

            
        }

        private void imgOblastOpremeOsveziListu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            return;
           


        }

        private void trivjuKorisnici_SizeChanged(object sender, SizeChangedEventArgs e)
        {



            this.pravilnoRasporediListuKorisnika();



        }
        private Visibility stariScrollbarVisibility;
        private bool daLiSePrekinuoTajmerDaLiJeMouseLeaveKorisniciHederImePrezime = true;
        DispatcherTimer timerProveriDaLiJeMouseLeaveNaHeaderPrikazaDetalja;

        private void btnOtkaziIzmene_Click(object sender, RoutedEventArgs e)
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

        private void btnKorisniciSacuvajUnos_Click(object sender, RoutedEventArgs e)
        {
            
                SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();


                DbItemKorisnici korisnik = new DbItemKorisnici()
                {
                    ime = this.CurrentKorisnici.ImeKorisnika,
                    prezime = this.CurrentKorisnici.PrezimeKorisnika,
                    id_uloge = 3,
                    mejl = this.CurrentKorisnici.MejlKorisnika,
                    broj_telefona = this.CurrentKorisnici.BrojTelefonaKorisnika,
                    
                };

                SmartSoftwareServiceReference.DbItemKorisnici[] nizKorisnika = service.OpKorisniciInsert(korisnik);
                
                this.popuniListuKorisnici(nizKorisnika);



                ControlTemplate ctpKorisnici = cclPrikazDetaljaKorisnici.Template as ControlTemplate;

                Grid grdPrikazDetaljaKorisnici = ctpKorisnici.FindName("grdPrikazDetaljaKorisnici", cclPrikazDetaljaKorisnici) as Grid;
                Grid grdPromenaVisine = ctpKorisnici.FindName("grdPromenaVisine", cclPrikazDetaljaKorisnici) as Grid;
                Border brdKorisniciImeIPrezimeNaslov = grdPrikazDetaljaKorisnici.FindName("brdKorisniciImeIPrezimeNaslov") as Border;
                Border brdPrikazDetaljaKorisniciSadrzaj = grdPrikazDetaljaKorisnici.FindName("brdPrikazDetaljaKorisniciSadrzaj") as Border;

                ContentControl cclPrikazHederaImePrezimeKorisnici = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as ContentControl;
                TextBox tboxPrezimeKorisnika = grdPrikazDetaljaKorisnici.FindName("cclPrikazHederaImePrezimeKorisnici") as TextBox;

                this.CurrentKorisnici = null;
                this.stariKorisnik = null;

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
    }

}
