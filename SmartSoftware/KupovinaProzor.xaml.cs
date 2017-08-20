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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for KupovinaProzor.xaml
    /// </summary>
    public partial class KupovinaProzor : Window, INotifyPropertyChanged
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private string putanjaDoSlikeKupi = App.rootPath + "\\slike\\coins-128-Green.png";
        private string putanjaDoSlikeKupiHoverovano = App.rootPath + "\\slike\\coins-128-White.png";
        private string putanjaDoSlikeKupiOnemoguceno = App.rootPath + "\\slike\\coins-128-Gray.png";

        public string PutanjaDoSlikeKupiOnemoguceno
        {
            get { return putanjaDoSlikeKupiOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeKupiOnemoguceno, value); }
        }


        private Korisnici trenutniProdavac;

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
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

        private string putanjaDoSlikePreskociIKupi = App.rootPath + "\\slike\\cash-receiving-128-Green.png";
        private string putanjaDoSlikePreskociIKupiHoverovano = App.rootPath + "\\slike\\cash-receiving-128-White.png";

        public string PutanjaDoSlikePreskociIKupi
        {
            get { return putanjaDoSlikePreskociIKupi; }
            set { SetAndNotify(ref putanjaDoSlikePreskociIKupi, value); }
        }
        

        public string PutanjaDoSlikePreskociIKupiHoverovano
        {
            get { return putanjaDoSlikePreskociIKupiHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikePreskociIKupiHoverovano, value); }
        }


















        public double UkupnaCena { get; set; }

        private double ukupnaCenaSaPopustom;

        public double UkupnaCenaSaPopustom 
        {
            get { return ukupnaCenaSaPopustom; }
            set { SetAndNotify(ref ukupnaCenaSaPopustom, value); }
        }

        private bool vratiSeUglavniProzor = false;


        private bool koristeSePoeni;
        public bool KoristeSePoeni {
            get
            {
                return koristeSePoeni;
            }

            set
            {
                SetAndNotify(ref koristeSePoeni, value);
            }
        }

        private Korisnici tmpKorisnik;

        public Korisnici TmpKorisnik
        {
            get { return tmpKorisnik; }
            set { SetAndNotify(ref tmpKorisnik, value); }
        }

        

        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { korpa = value; }
        }

        public KupovinaProzor(Korisnici prodavac)
        {
            InitializeComponent();
            this.DataContext = this;
            this.TrenutniProdavac = prodavac;

           
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.vratiSeUglavniProzor = false;
            this.Close();
        }


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
                        SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena,
                        DeletedItem = oprema[i].DeletedField

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



                    }

                }
            }


        }

        private void btnPreskoci_Click(object sender, RoutedEventArgs e)
        {
            

            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);
            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                for (int i = 0; i < this.Korpa.Count; i++)
                {
                    if ((this.Korpa[i] as Oprema).DeletedItem)
                    {
                        string poruka = "Prodaja pojedine opreme nije moguća.\n\nOprema :\n(" + (this.Korpa[i] as Oprema).Name.ToString() + "),\n\nje u međuvremenu obrisana. Da li želite da nastavite sa prodajom ostale opreme iz korpe?";


                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri prodaji", MessageBoxButton.OKCancel, slikaBoxa);

                        switch (rezultatBoxa)
                        {
                            case MessageBoxResult.OK:
                                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaDelete((this.Korpa[i] as Oprema).IdOprema, TrenutniProdavac.IdKorisnici);
                                this.Korpa.RemoveAt(i);

                                break;

                            case MessageBoxResult.Cancel:
                                return;

                        }

                    }
                }
            }
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaProveraPonovo = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(opremaProveraPonovo);
            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                for (int i = 0; i < this.Korpa.Count; i++)
                {
                    if ((this.Korpa[i] as Oprema).KolicinaNaLageru < (this.Korpa[i] as Oprema).IzabranaKolicina)
                    {
                        string poruka = "Prodaja pojedine opreme nije moguća.\n\nZa opremu :\n(" + (this.Korpa[i] as Oprema).Name.ToString() + "),\n\nodabrana količina za prodaju je veća od količine na lageru. Da li želite da nastavite sa prodajom ostale opreme iz korpe?";


                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri prodaji", MessageBoxButton.OKCancel, slikaBoxa);

                        switch (rezultatBoxa)
                        {
                            case MessageBoxResult.OK:
                                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaDelete(oprema[i].id_oprema, TrenutniProdavac.IdKorisnici);
                                this.Korpa.RemoveAt(i);

                                break;

                            case MessageBoxResult.Cancel:
                                return;

                        }

                    }
                }
            }
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaProveraPonovoDrugiPut = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(opremaProveraPonovoDrugiPut);

            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                DateTime datum = DateTime.Now;


                DbItemIstorijaKupovine d = new DbItemIstorijaKupovine()
                {
                    datum_prodaje = datum,
                    Kupac = null,
                    prodavac = new DbItemKorisnici() { id_korisnici = TrenutniProdavac.IdKorisnici },
                    ukupna_cena_kupovine = this.UkupnaCenaSaPopustom,
                    broj_iskoriscenih_popust_poena = 0
                };
                List<DbItemKupljenaOpremaSaParametrima> listaKupljenjeOpreme = new List<DbItemKupljenaOpremaSaParametrima>();

                foreach (var item in this.Korpa)
                {
                    Oprema o = item as Oprema;

                    listaKupljenjeOpreme.Add(new DbItemKupljenaOpremaSaParametrima()
                        {
                            cena = o.Cena,
                            cena_opreme_kad_je_prodata = o.Cena,
                            id_oprema = o.IdOprema,
                            prodataKolicina = o.IzabranaKolicina
                        });

                }
                SmartSoftwareServiceReference.DbItemKupljenaOpremaSaParametrima[] rez = service.ProdajaArtikla(d, listaKupljenjeOpreme.ToArray());

                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rezultat = service.KorpaDelete(null, TrenutniProdavac.IdKorisnici);
                this.PopuniKorpu(rezultat);
                Xceed.Wpf.Toolkit.MessageBox.Show("Uspesno ste zavrsili Prodaju !");
                this.vratiSeUglavniProzor = true;
                this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Korpa je prazna. Prodaja nije moguća.");
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaKraj = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
                this.PopuniKorpu(opremaKraj);
                this.Close();
            }

        }

        private void btnKupi_Click(object sender, RoutedEventArgs e)
        {


            SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] oprema = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(oprema);
            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                for (int i = 0; i < this.Korpa.Count; i++)
                {
                    if ((this.Korpa[i] as Oprema).DeletedItem)
                    {
                        string poruka = "Prodaja pojedine opreme nije moguća.\n\nOprema :\n(" + (this.Korpa[i] as Oprema).Name.ToString() + "),\n\nje u međuvremenu obrisana. Da li želite da nastavite sa prodajom ostale opreme iz korpe?";


                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri prodaji", MessageBoxButton.OKCancel, slikaBoxa);

                        switch (rezultatBoxa)
                        {
                            case MessageBoxResult.OK:
                                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaDelete((this.Korpa[i] as Oprema).IdOprema, TrenutniProdavac.IdKorisnici);
                                this.Korpa.RemoveAt(i);
                                break;

                            case MessageBoxResult.Cancel:
                                return;

                        }

                    }
                }
            }
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaProveraPonovo = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(opremaProveraPonovo);
            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                for (int i = 0; i < this.Korpa.Count; i++)
                {
                    if ((this.Korpa[i] as Oprema).KolicinaNaLageru < (this.Korpa[i] as Oprema).IzabranaKolicina)
                    {
                        string poruka = "Prodaja pojedine opreme nije moguća.\n\nZa opremu :\n(" + (this.Korpa[i] as Oprema).Name.ToString() + "),\n\nodabrana količina za prodaju je veća od količine na lageru. Da li želite da nastavite sa prodajom ostale opreme iz korpe?";


                        MessageBoxImage slikaBoxa = MessageBoxImage.Error;
                        MessageBoxResult rezultatBoxa = System.Windows.MessageBox.Show(poruka, "Greška pri prodaji", MessageBoxButton.OKCancel, slikaBoxa);

                        switch (rezultatBoxa)
                        {
                            case MessageBoxResult.OK:
                                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaPonovo = service.KorpaDelete(oprema[i].id_oprema, TrenutniProdavac.IdKorisnici);
                                this.Korpa.RemoveAt(i);
                                break;

                            case MessageBoxResult.Cancel:
                                return;

                        }

                    }
                }
            }
            SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaProveraPonovoDrugiPut = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
            this.PopuniKorpu(opremaProveraPonovoDrugiPut);

            if (this.Korpa != null && this.Korpa.Count > 0)
            {
                DateTime datum = DateTime.Now;
                DbItemIstorijaKupovine d = new DbItemIstorijaKupovine()
                {
                    datum_prodaje = datum,
                    Kupac = new DbItemKorisnici() { id_korisnici = TmpKorisnik.IdKorisnici },
                    prodavac = new DbItemKorisnici() { id_korisnici = this.TrenutniProdavac.IdKorisnici },
                    ukupna_cena_kupovine = this.UkupnaCenaSaPopustom,
                    broj_iskoriscenih_popust_poena = TmpKorisnik.IzabranBrojPoenaZaPopust
                };
                List<DbItemKupljenaOpremaSaParametrima> listaKupljenjeOpreme = new List<DbItemKupljenaOpremaSaParametrima>();

                foreach (var item in this.Korpa)
                {
                    Oprema o = item as Oprema;

                    listaKupljenjeOpreme.Add(new DbItemKupljenaOpremaSaParametrima()
                    {
                        cena = o.Cena,
                        cena_opreme_kad_je_prodata = o.Cena,
                        id_oprema = o.IdOprema,
                        prodataKolicina = o.IzabranaKolicina
                    });
                }

                DbItemKupljenaOpremaSaParametrima[] kupljenaOprema = service.ProdajaArtikla(d, listaKupljenjeOpreme.ToArray());

                if (TmpKorisnik != null)
                {

                    int brojPoenaDodatno = 0;
                    if (this.koristeSePoeni)
                    {
                        brojPoenaDodatno = -TmpKorisnik.IzabranBrojPoenaZaPopust;
                    }
                    else
                    {
                        brojPoenaDodatno = Convert.ToInt32(Math.Floor(UkupnaCena / 100));
                    }

                    DbItemKorisnici korisnik = new DbItemKorisnici()
                    {
                        id_korisnici = TmpKorisnik.IdKorisnici,
                        brojOstvarenihPoena = brojPoenaDodatno
                    };
                    SmartSoftwareServiceReference.DbItemKorisnici[] rez = service.AzurirajBrojPoenaKorisnika(korisnik);
                    Xceed.Wpf.Toolkit.MessageBox.Show("Uspesno ste zavrsili Prodaju za korisnika : " + TmpKorisnik.ImeKorisnika + " !");

                }
                this.vratiSeUglavniProzor = true;
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] rezultat = service.KorpaDelete(null, TrenutniProdavac.IdKorisnici);
                this.PopuniKorpu(rezultat);
                this.Korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Korpa je prazna. Prodaja nije moguća.");
                SmartSoftwareServiceReference.DbItemOpremaSaParametrima[] opremaKraj = service.KorpaSelect(TrenutniProdavac.IdKorisnici);
                this.PopuniKorpu(opremaKraj);
                this.Close();
            }
        }


        private void proveriSadrzajKorpe()
        {
            ////List<string> listaGresaka = new List<string>();
           
            //    else
            //    {

            //    }
            //}
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            //if(!this.vratiSeUglavniProzor)
            //{
            //    KorpaProzor korpa = new KorpaProzor();
            //    korpa.Show();
            //}
        }

        private void btnUcitajPodatkeOKupcu_Click(object sender, RoutedEventArgs e)
        {
            

            PodaciOKupcuProzor prozor = new PodaciOKupcuProzor(this.TrenutniProdavac);
            prozor.ShowDialog();
            
            if(prozor.CurrentKorisnici != null && prozor.DaLiJeOdabranKorisnik)
                this.TmpKorisnik = prozor.CurrentKorisnici;





            if (prozor.DaLiJeOdabranKorisnik || (this.TmpKorisnik == null && !prozor.DaLiJeOdabranKorisnik))
            {
                grdLabelKoriscenjePoena.SetResourceReference(StyleProperty, "grdLabelNijeCekirano");
                grdPrivremeniGrid.Height = 70;
                grdKoriscenjePoena.Height = 0;

                ccontrolUkupnaCena.SetResourceReference(TemplateProperty, "ctemplateBezPopusta");

                PoeniKaoPopust.IsChecked = false;
            }
            
            borderNemaPodataka.Visibility = Visibility.Hidden;




            if (this.TmpKorisnik != null && !prozor.DaLiJeOdabranKorisnik)
            {
                
                btnKupi.IsEnabled = true;
                this.borderPodaciOKupcu.Visibility = System.Windows.Visibility.Visible;

                TmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(Math.Floor(UkupnaCena / 2 / 10));

                if (TmpKorisnik.BrojPoenaZaPopust >= Math.Floor(TmpKorisnik.BrojOstvarenihPoena))
                {
                    TmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(TmpKorisnik.BrojOstvarenihPoena);
                }

                
                if (TmpKorisnik != null && KoristeSePoeni)
                {

                    UkupnaCenaSaPopustom = UkupnaCena - (10 * TmpKorisnik.IzabranBrojPoenaZaPopust);
                }
                else
                {
                    UkupnaCenaSaPopustom = UkupnaCena;
                }
            }
            //if (TmpKorisnik.IdKorisnici != 0)
            else if (TmpKorisnik != null)
            {
                tblIzabranBrojPoenaZaPopust.Text = "0";
                btnKupi.IsEnabled=true;
                //this.TmpKorisnik.IzabranBrojPoenaZaPopust = Convert.ToInt32(TmpKorisnik.BrojOstvarenihPoena);
                this.borderPodaciOKupcu.Visibility = System.Windows.Visibility.Visible;

                if (TmpKorisnik.BrojOstvarenihPoena > 0)
                {
                    PoeniKaoPopust.IsEnabled = true;
                    lblIskoristiPoeneZaPopust.Foreground = new BrushConverter().ConvertFrom("#FF17405E") as Brush;

                    TmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(Math.Floor(UkupnaCena / 2 / 10));

                    if (TmpKorisnik.BrojPoenaZaPopust >= Math.Floor(TmpKorisnik.BrojOstvarenihPoena))
                    {
                        TmpKorisnik.BrojPoenaZaPopust = Convert.ToInt32(TmpKorisnik.BrojOstvarenihPoena);
                    }

                    TmpKorisnik.IzabranBrojPoenaZaPopust = 0;
                    TmpKorisnik.IzabranBrojPoenaZaPopust = TmpKorisnik.BrojPoenaZaPopust;
                    tblIzabranBrojPoenaZaPopust.Text = TmpKorisnik.IzabranBrojPoenaZaPopust.ToString();
                    if (TmpKorisnik != null)
                    {

                        UkupnaCenaSaPopustom = UkupnaCena - (10 * TmpKorisnik.IzabranBrojPoenaZaPopust);
                    }
                    else
                    {
                        UkupnaCenaSaPopustom = UkupnaCena;
                    }
                }
                else
                {
                    PoeniKaoPopust.IsEnabled = false;
                    PoeniKaoPopust.IsChecked = false;
                    lblIskoristiPoeneZaPopust.Foreground = Brushes.Gray;
                }


                
            }
            else
            {
                
                
                //borderBezPopusta.BorderBrush = Brushes.DarkGreen;
                //grdCenaBezPopustaLabel.Background = Brushes.DarkGreen;
                //lblCenaBezPopusta.Foreground = Brushes.White;
                //txtBlockCenaBezPopusta.Foreground = Brushes.DarkGreen;

                
                //borderSaPopustom.Visibility = Visibility.Hidden;

                //lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Hidden;
                //btnVrednostPoena.Visibility = System.Windows.Visibility.Hidden;
                //lblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
                //tblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;

                this.KoristeSePoeni = false;

                btnKupi.IsEnabled = false;
                this.borderPodaciOKupcu.Visibility = System.Windows.Visibility.Hidden;
                borderNemaPodataka.Visibility = Visibility.Visible;
                //lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Hidden;
                //btnVrednostPoena.Visibility = System.Windows.Visibility.Hidden;
                //lblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
                //tblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
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

        private void btnVrednostPoena_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBlock txtBox = (TextBlock)spinner.Content;

            int value = String.IsNullOrEmpty(txtBox.Text) ? 0 : Convert.ToInt32(txtBox.Text.ToString());

            if (e.Direction == SpinDirection.Increase && value >= TmpKorisnik.BrojOstvarenihPoena && value >= this.TmpKorisnik.BrojPoenaZaPopust)
                spinner.ValidSpinDirection = ValidSpinDirections.Decrease;

            else if (e.Direction == SpinDirection.Increase && value < TmpKorisnik.BrojOstvarenihPoena && value < this.TmpKorisnik.BrojPoenaZaPopust)
                {
                    value = value + 1;
                }
                else
                {
                    if( value > 0)
                    {
                        value = value - 1;
                    }
                }
            txtBox.Text = value.ToString();

                Grid grid = spinner.Parent as Grid;

                int id = 0;
                TextBlock t = grid.FindName("skrivenId") as TextBlock;
                id = Convert.ToInt32(t.Text);

                if (value >= TmpKorisnik.BrojPoenaZaPopust || value >= TmpKorisnik.BrojOstvarenihPoena)
                {
                    spinner.ValidSpinDirection = ValidSpinDirections.Decrease;
                }
                else if (value <= 1)
                {
                    spinner.ValidSpinDirection = ValidSpinDirections.Increase;
                }
                else
                {
                    ButtonSpinner b = new ButtonSpinner();
                    spinner.ValidSpinDirection = b.ValidSpinDirection;
                    b = null;
                }
                TmpKorisnik.IzabranBrojPoenaZaPopust = value;
                if (TmpKorisnik != null)
            {

                UkupnaCenaSaPopustom = UkupnaCena - (10 * TmpKorisnik.IzabranBrojPoenaZaPopust);
            }
            else
            {
                UkupnaCenaSaPopustom = UkupnaCena;
            }
            
        }

        private void PoeniKaoPopust_Click(object sender, RoutedEventArgs e)
        {
            
            CheckBox check = sender as CheckBox;
            var bc = new BrushConverter();
            if (check.IsChecked ?? false)
            {
                grdLabelKoriscenjePoena.SetResourceReference(StyleProperty, "grdLabel");

                grdKoriscenjePoena.Visibility = Visibility.Visible;
                grdKoriscenjePoena.Height = 30;
                grdPrivremeniGrid.Height = 40;
                ccontrolUkupnaCena.SetResourceReference(TemplateProperty, "ctemplateSaPopustom");
                if (TmpKorisnik.IzabranBrojPoenaZaPopust <= 1)
                    btnVrednostPoena.IsEnabled = false;
                else
                    btnVrednostPoena.IsEnabled = true;
                //borderBezPopusta.BorderBrush = Brushes.Gainsboro;
                //grdCenaBezPopustaLabel.Background = Brushes.Gainsboro;
                //lblCenaBezPopusta.Foreground = Brushes.Gray;
                //txtBlockCenaBezPopusta.Foreground = Brushes.Gray;

                //borderSaPopustom.Visibility = Visibility.Visible;

                //lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Visible;
                //btnVrednostPoena.Visibility = System.Windows.Visibility.Visible;
                //lblCenaSaPopustom.Visibility = System.Windows.Visibility.Visible;
                //tblCenaSaPopustom.Visibility = System.Windows.Visibility.Visible;

                this.KoristeSePoeni = true;
            }
            else
            {
                grdLabelKoriscenjePoena.SetResourceReference(StyleProperty, "grdLabelNijeCekirano");
                grdPrivremeniGrid.Height = 70;
                grdKoriscenjePoena.Height = 0;

                ccontrolUkupnaCena.SetResourceReference(TemplateProperty, "ctemplateBezPopusta");  

                //borderBezPopusta.BorderBrush = Brushes.DarkGreen;
                //grdCenaBezPopustaLabel.Background = Brushes.DarkGreen;
                //lblCenaBezPopusta.Foreground = Brushes.White;
                //txtBlockCenaBezPopusta.Foreground = Brushes.DarkGreen;

                //borderSaPopustom.Visibility = Visibility.Hidden;

                //lblBrojPoenaMogucih.Visibility = System.Windows.Visibility.Hidden;
                //btnVrednostPoena.Visibility = System.Windows.Visibility.Hidden;
                //lblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;
                //tblCenaSaPopustom.Visibility = System.Windows.Visibility.Hidden;

                this.KoristeSePoeni = false;
            }
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
