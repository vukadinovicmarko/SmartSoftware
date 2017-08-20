using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Microsoft.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Runtime.InteropServices;
using System.Windows.Interop;
using SmartSoftware.ViewModel;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for KorpaProzor.xaml
    /// </summary>
    public partial class KorpaProzor : Window, INotifyPropertyChanged
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private bool daLiImaNestoUKorpi = false;

        public bool DaLiImaNestoUKorpi
        {
            get { return daLiImaNestoUKorpi; }
            set { SetAndNotify(ref daLiImaNestoUKorpi, value); }
        }

        private Korisnici trenutniProdavac;

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }
       
        



        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { SetAndNotify(ref korpa, value); }
        }


        private ObservableCollection<SmartSoftwareGlavnaOblast> korpaProvera = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> KorpaProvera
        {
            get { return korpaProvera; }
            set { SetAndNotify(ref korpaProvera, value); }
        }

        private double ukupnaCenaUKorpi = 0;

        public double UkupnaCenaUKorpi
        {
            get {
                double suma = 0;

                foreach (var item in this.Korpa)
                {
                    suma += (item as Oprema).SumCena;
                }


                SetAndNotify(ref ukupnaCenaUKorpi, suma);
                return this.ukupnaCenaUKorpi;
            }
            set { SetAndNotify(ref ukupnaCenaUKorpi, value); }
                

            
        }

        

        private Oprema tmpEditObj;

        public Oprema TmpEditObj
        {
            get { return tmpEditObj; }
            set { tmpEditObj = value; }
        }


        public KorpaProzor(Korisnici prodavac)
        {
            InitializeComponent();
            //itcProizvodi.ItemsSource = this.Korpa;
            //itcCenaProizvoda.ItemsSource = this.Korpa;
            //itcUkupnaCenaProizvoda.ItemsSource = this.Korpa;
            //itcKolicina.ItemsSource = this.Korpa;
            //itcDugmeIzbrisi.ItemsSource = this.Korpa;
            this.DataContext = this;
            this.TrenutniProdavac = prodavac;
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);
            this.pravilnoRasporediListuKorpe();
            

        }




        private void PopuniKorpu(DbItemOpremaSaParametrima[] oprema)
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
                    IzabranaKolicina = oprema[i].kolicinaUKorpi,
                    SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena

                });

                this.KorpaProvera.Add(new Oprema(null)
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

                    (this.KorpaProvera[i] as Oprema).ListaParametara.Add(new Parametri(null)

                    {
                        DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
                        IdParametri = oprema[i].ListaParametara[j].id_parametri,
                        IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
                        VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
                        Name = oprema[i].ListaParametara[j].naziv_parametra
                    });

                }

            }

            if (this.Korpa.Count == 0)
            {
                borderSlika.Height = 0;
                borderNaslov.Height = 0;
                bordeCena.Height = 0;
                borderUkupnaCena.Height = 0;
                borderKolicina.Height = 0;
                DaLiImaNestoUKorpi = false;
                scrolVjuverKorpa.Visibility = Visibility.Hidden;
                grdKorpaJePrazna.Visibility = Visibility.Visible;
            }
            else
            {
                borderSlika.Height = 1;
                borderNaslov.Height = 1;
                bordeCena.Height = 1;
                borderUkupnaCena.Height = 1;
                borderKolicina.Height = 1;
                DaLiImaNestoUKorpi = true;
                scrolVjuverKorpa.Visibility = Visibility.Visible;
                grdKorpaJePrazna.Visibility = Visibility.Hidden;
            }

            this.pravilnoRasporediListuKorpe();

        }


       

        //private void element_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Oprema o1 = tmpEditObj as Oprema;
        //    StackPanel s = sender as StackPanel;
        //    Xceed.Wpf.Toolkit.MessageBox.Show(sender.GetType().ToString());


            


        //    //MessageBox.Show(s.Children[0].ToString());
        //    Grid g = s.Children[0] as Grid;
        //    //MessageBox.Show(g.FindName("skrivenId").GetType().ToString());
        //    TextBlock t = g.FindName("skrivenId") as TextBlock;
        //    //MessageBox.Show(t.Text);

        //    List<Oprema> o = new List<Oprema>();
        //    for (int i = 0; i < Korpa.Count; i++)
        //    {
        //        if ((Korpa[i] as Oprema).IdOprema == Int32.Parse(t.Text))
        //        {
        //          //  o.Add(lista[i] as Oprema);
        //            this.TmpEditObj = (Korpa[i] as Oprema);
        //            break;
        //        }
        //    }
        //}

        private void SacuvajIzmeneUKorpi()
        {

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                for (int i = 0; i < Korpa.Count; i++)
                {
                    if ((this.Korpa[i] as Oprema).IzabranaKolicina <= (this.Korpa[i] as Oprema).KolicinaNaLageru)
                    {
                        service.KorpaUpdate((Korpa[i] as Oprema).IdOprema, (int)(Korpa[i] as Oprema).IzabranaKolicina, TrenutniProdavac.IdKorisnici);
                        //(Korpa[i] as Oprema).Tmp2KolicinaNaLageru = (Korpa[i] as Oprema).KolicinaNaLageru - (Korpa[i] as Oprema).IzabranaKolicina;
                        (Korpa[i] as Oprema).TmpIzabranaKolicina = 1;
                        //(Korpa[i] as Oprema).DaliMozeJosDaseDoda = (Korpa[i] as Oprema).IzabranaKolicina != (Korpa[i] as Oprema).KolicinaNaLageru;
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.SacuvajIzmeneUKorpi();
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);
            
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
            for (int i = 0; i < this.Korpa.Count; i++)
            {
                if ((this.Korpa[i] as Oprema).IdOprema == id)
                {

                    Oprema tmp = (this.Korpa[i] as Oprema);
                    tmp.IzabranaKolicina = value;
                    //tmp.KolicinaNaLageru = tmp.KolicinaNaLageru - tmp.IzabranaKolicinaZaRezervisanje;
                    tmp.SumCena = (this.Korpa[i] as Oprema).IzabranaKolicina * (this.Korpa[i] as Oprema).Cena;

                }
            }



            this.refreshujUkupnuCenu();

            //Xceed.Wpf.Toolkit.IntegerUpDown updown = sender as Xceed.Wpf.Toolkit.IntegerUpDown;
            ////MessageBox.Show(updown.Parent.GetType().ToString());
            //Grid grid = updown.Parent as Grid;
            //int id = 0;
            //TextBlock t = grid.FindName("skrivenId") as TextBlock;
            //id = Convert.ToInt32(t.Text);
            //for (int i = 0; i < this.Korpa.Count; i++)
            //{
            //    if((this.Korpa[i] as Oprema).IdOprema == id)
            //    {
            //        (this.Korpa[i] as Oprema).SumCena = (this.Korpa[i] as Oprema).IzabranaKolicina * (this.Korpa[i] as Oprema).Cena;
            //    }
            //}
            //pera.ItemsSource = this.Korpa;
        }

        private void kolicina_MouseDown(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {


            ButtonSpinner spinner = (ButtonSpinner)sender;
            Label txtBox = (Label)spinner.Content;

            int value = String.IsNullOrEmpty(txtBox.Content.ToString()) ? 0 : Convert.ToInt32(txtBox.Content);
            if (e.Direction == SpinDirection.Increase )
                value++;
            else
                value--;
            //txtBox.Content = value.ToString();

            Grid grid = spinner.Parent as Grid;
            //Grid grid1 = grid.Parent as Grid;
            int id = 0;
            TextBlock t = grid.FindName("skrivenId") as TextBlock;
            id = Convert.ToInt32(t.Text);
            for (int i = 0; i < this.korpa.Count; i++)
            {
                if ((this.korpa[i] as Oprema).IdOprema == id)
                {
                    Oprema tmp = (this.korpa[i] as Oprema);
                    if (e.Direction == SpinDirection.Increase)
                    {
                        
                        if (tmp.KolicinaNaLageru >= value)
                        {
                            if (value == tmp.KolicinaNaLageru)
                            {
                                spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                            }
                            else
                            {
                                ButtonSpinner b = new ButtonSpinner();
                                spinner.ValidSpinDirection = b.ValidSpinDirection;
                                b = null;
                                
                            }
                            tmp.IzabranaKolicina = value;
                            tmp.SumCena = (this.korpa[i] as Oprema).IzabranaKolicina * (this.korpa[i] as Oprema).Cena;
                            txtBox.Content = value.ToString();
                        }
                        else
                        {
                            spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                        }
                        
                        // this.ukupnaCenaUKorpi += tmp.SumCena; 
                    }
                    else
                    {

                        if (value == 0 || value == 1)
                        {
                            if (value == 1)
                            {
                                txtBox.Content = value.ToString();
                                tmp.IzabranaKolicina = value;
                                tmp.SumCena = (this.korpa[i] as Oprema).IzabranaKolicina * (this.korpa[i] as Oprema).Cena;
                            }
                            spinner.ValidSpinDirection = ValidSpinDirections.Increase;
                        }
                        else
                        {
                            ButtonSpinner b = new ButtonSpinner();
                            spinner.ValidSpinDirection = b.ValidSpinDirection;
                            b = null;
                            txtBox.Content = value.ToString();
                        }
                        
                    }
                }
            }


            //foreach (var item in this.Korpa)
            //{
            //    UkupnaCenaUKorpi += (item as Oprema).SumCena;
            //}
            this.refreshujUkupnuCenu();
            //Xceed.Wpf.Toolkit.MessageBox.Show(this.ukupnaCenaUKorpi.ToString());


            //TextBlock textBlockUkunaCena = this.gridDugmici.FindName("txbUkupnaCena") as TextBlock;
            //textBlockUkunaCena.Text = "Ukupna cena u korpi: " + this.UkupnaCenaUKorpi.ToString();
        }

        public void refreshujUkupnuCenu()
        {
            this.ukupnaCenaUKorpi = this.UkupnaCenaUKorpi;
        }

        private void btnObrisiCeluKorpu_Click(object sender, RoutedEventArgs e)
        {
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(null, TrenutniProdavac.IdKorisnici);
            //pera.ItemsSource = null;
            this.Korpa = this.KorpaProvera = new ObservableCollection<SmartSoftwareGlavnaOblast>();
           
            refreshujUkupnuCenu();

            itcProizvodi.ItemsSource = null;
            itcCenaProizvoda.ItemsSource = null;
            itcUkupnaCenaProizvoda.ItemsSource = null;
            itcKolicina.ItemsSource = null;
            itcDugmeIzbrisi.ItemsSource = null;

            borderSlika.Height = 0;
            borderNaslov.Height = 0;
            bordeCena.Height = 0;
            borderUkupnaCena.Height = 0;
            borderKolicina.Height = 0;
            DaLiImaNestoUKorpi = false;
            scrolVjuverKorpa.Visibility = Visibility.Hidden;
            grdKorpaJePrazna.Visibility = Visibility.Visible;
            
        }

        private void btnObrisiIzKorpe_Click(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            Grid g = b.Parent as Grid;
            TextBlock t = g.FindName("skrivenId") as TextBlock;
            int id = Convert.ToInt32(t.Text);

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rez = service.KorpaDelete(id, TrenutniProdavac.IdKorisnici);
            itcProizvodi.ItemsSource = null;
            itcCenaProizvoda.ItemsSource = null;
            itcUkupnaCenaProizvoda.ItemsSource = null;
            itcKolicina.ItemsSource = null;
            itcDugmeIzbrisi.ItemsSource = null;
            this.PopuniKorpu(rez);
            itcProizvodi.ItemsSource = this.Korpa;
            itcCenaProizvoda.ItemsSource = this.Korpa;
            itcUkupnaCenaProizvoda.ItemsSource = this.Korpa;
            itcKolicina.ItemsSource = this.Korpa;
            itcDugmeIzbrisi.ItemsSource = this.Korpa;
            refreshujUkupnuCenu();

            
        }

        private void btnKupi_Click(object sender, RoutedEventArgs e)
        {
            this.SacuvajIzmeneUKorpi();
            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);
            //this.Visibility = Visibility.Hidden;
            KupovinaProzor kupovinaProzor = new KupovinaProzor(this.TrenutniProdavac);
            
            kupovinaProzor.UkupnaCena = this.UkupnaCenaUKorpi;
            kupovinaProzor.Korpa = this.Korpa;
            kupovinaProzor.ShowDialog();
            this.Korpa = kupovinaProzor.Korpa;
            if (this.Korpa.Count > 0)
            {
                
                this.UkupnaCenaUKorpi = UkupnaCenaUKorpi;

            }
            else
            {
                
                //this.Korpa = kupovinaProzor.Korpa;
                this.UkupnaCenaUKorpi = 0;
            }
            
            if (this.Korpa != null)
            {

                if (this.Korpa.Count == 0)
                {
                    borderSlika.Height = 0;
                    borderNaslov.Height = 0;
                    bordeCena.Height = 0;
                    borderUkupnaCena.Height = 0;
                    borderKolicina.Height = 0;
                    DaLiImaNestoUKorpi = false;
                    scrolVjuverKorpa.Visibility = Visibility.Hidden;
                    grdKorpaJePrazna.Visibility = Visibility.Visible;
                }
                else
                {
                    borderSlika.Height = 1;
                    borderNaslov.Height = 1;
                    bordeCena.Height = 1;
                    borderUkupnaCena.Height = 1;
                    borderKolicina.Height = 1;
                    DaLiImaNestoUKorpi = true;
                    scrolVjuverKorpa.Visibility = Visibility.Visible;
                    grdKorpaJePrazna.Visibility = Visibility.Hidden;
                }
            }

            this.pravilnoRasporediListuKorpe();
            //this.Visibility=Visibility.Visible;
            //this.Close();
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

        private void scrolVjuverKorpaSadrzaj_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            //ControlTemplate ctp = cclUpravljanjeNarudzbinama.Template as ControlTemplate;
            //Grid brdHederNarudzbine = ctp.FindName("brdHederNarudzbine", cclUpravljanjeNarudzbinama) as Border;
            if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Collapsed)
                grdHederKorpa.Margin = new Thickness(0);
            else if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible)
                grdHederKorpa.Margin = new Thickness(-17, 0, 0, 0);
            this.pravilnoRasporediListuKorpe();
        }

        private void pravilnoRasporediListuKorpe()
        {
            if (itcProizvodi != null && itcProizvodi.Items != null)
            {
                int brojRedova = itcProizvodi.Items.Count;
                if (brojRedova > 0)
                {
                    for (int i = 0; i < brojRedova; i++)
                    {
                        Grid grd = FindChild<Grid>(itcProizvodi.ItemContainerGenerator.ContainerFromIndex(i));
                        TextBlock tblckNaslovProizvodaNarudzbine = grd.FindName("tblckNaslovProizvodaNarudzbine") as TextBlock;
                        TextBlock tblckNaslovProizvodaNarudzbineDetaljnije = grd.FindName("tblckNaslovProizvodaNarudzbineDetaljnije") as TextBlock;
                        if (tblckNaslovProizvodaNarudzbine != null)
                        {
                            if (tblckNaslovProizvodaNarudzbine.ActualHeight > (double)33)
                            {
                                if (this.Korpa != null && this.Korpa.Count > 0)
                                {
                                    if ((this.Korpa[i] as Oprema) != null)
                                    {
                                        (this.Korpa[i] as Oprema).DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova = true;
                                    }
                                } 

                                //tblckNaslovProizvodaNarudzbine.HorizontalAlignment = HorizontalAlignment.Left;

                                tblckNaslovProizvodaNarudzbineDetaljnije.Visibility = Visibility.Visible;
                                tblckNaslovProizvodaNarudzbineDetaljnije.Height = 15;
                                //tblckNaslovProizvodaNarudzbineDetaljnije.Width = tblckNaslovProizvodaNarudzbine.ActualWidth;
                            }
                            else
                            {
                                if (this.Korpa != null && this.Korpa.Count > 0)
                                {
                                    if ((this.Korpa[i] as Oprema) != null)
                                    {
                                        (this.Korpa[i] as Oprema).DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova = false;
                                    }
                                } 

                                //tblckNaslovProizvodaNarudzbine.HorizontalAlignment = HorizontalAlignment.Center;

                                tblckNaslovProizvodaNarudzbineDetaljnije.Visibility = Visibility.Hidden;
                                tblckNaslovProizvodaNarudzbineDetaljnije.Height = 0;
                            }
                        }




                    }
                }
                brojRedova = 0;
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




        //private void btnObrisiIzKorpe_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    ProbaZaSlike.PutanjaDoSlikeDelete = App.rootPath + "\\slike\\delete-128-White.png";
        //}

        //private void btnObrisiIzKorpe_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    PutanjaDoSlikeDelete = App.rootPath + "\\slike\\delete-128-Red.png";
        //}
    }
}
