using SmartSoftware.Model;
using SmartSoftware.SmartSoftwareServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for RezervacijeProzor.xaml
    /// </summary>
    public partial class TrenutnaListaRezervacija : Window, INotifyPropertyChanged
    {
        public TrenutnaListaRezervacija()
        {
            InitializeComponent();
            this.DataContext = this;
            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();
            //SmartSoftwareServiceReference.DbItemRezervacijaSaOpremom[] oprema = service.RezervacijeSelect();
            //this.PopuniRezervacije(oprema);
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> rezervacijeProvera = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> RezervacijeProvera
        {
            get { return rezervacijeProvera; }
            set { rezervacijeProvera = value; }
        }


        //private void PopuniRezervacije(DbItemOpremaSaParametrima[] oprema)
        //{


        //    this.Rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        //    for (int i = 0; i < oprema.Length; i++)
        //    {
        //        this.Rezervacije.Add(new Oprema(null)
        //        {
        //            Cena = oprema[i].cena,
        //            IdOprema = oprema[i].id_oprema,
        //            IdTipOpreme = oprema[i].id_tip_opreme,
        //            KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
        //            KolicinaURezervi = oprema[i].kolicina_u_rezervi,
        //            Lager = oprema[i].lager,
        //            Model = oprema[i].model,
        //            Name = oprema[i].naslov,
        //            Opis = oprema[i].opis,
        //            OpremaNaPopustu = oprema[i].oprema_na_popustu,
        //            Proizvodjac = oprema[i].proizvodjac,
        //            Slika = oprema[i].slika,
        //            SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
        //            IzabranaKolicina = oprema[i].kolicinaUKorpi,
        //            SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena

        //        });

        //        this.RezervacijeProvera.Add(new Oprema(null)
        //        {
        //            Cena = oprema[i].cena,
        //            IdOprema = oprema[i].id_oprema,
        //            IdTipOpreme = oprema[i].id_tip_opreme,
        //            KolicinaNaLageru = oprema[i].kolicina_na_lageru + oprema[i].kolicinaUKorpi,
        //            KolicinaURezervi = oprema[i].kolicina_u_rezervi,
        //            Lager = oprema[i].lager,
        //            Model = oprema[i].model,
        //            Name = oprema[i].naslov,
        //            Opis = oprema[i].opis,
        //            OpremaNaPopustu = oprema[i].oprema_na_popustu,
        //            Proizvodjac = oprema[i].proizvodjac,
        //            Slika = oprema[i].slika,
        //            SlikaOriginalPutanja = oprema[i].slikaOriginalPutanja,
        //            IzabranaKolicina = oprema[i].kolicinaUKorpi,
        //            SumCena = oprema[i].kolicinaUKorpi * oprema[i].cena
        //        });




        //        for (int j = 0; j < oprema[i].ListaParametara.Length; j++)
        //        {
        //            (this.Rezervacije[i] as Oprema).ListaParametara.Add(new Parametri(null)

        //            {
        //                DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
        //                IdParametri = oprema[i].ListaParametara[j].id_parametri,
        //                IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
        //                VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
        //                Name = oprema[i].ListaParametara[j].naziv_parametra
        //            });

        //            (this.RezervacijeProvera[i] as Oprema).ListaParametara.Add(new Parametri(null)

        //            {
        //                DefaultVrednost = oprema[i].ListaParametara[j].default_vrednost,
        //                IdParametri = oprema[i].ListaParametara[j].id_parametri,
        //                IdTipOpreme = oprema[i].ListaParametara[j].id_tip_opreme,
        //                VrednostParametra = oprema[i].ListaParametara[j].vrednost_parametra,
        //                Name = oprema[i].ListaParametara[j].naziv_parametra
        //            });

        //        }

        //    }


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

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    MessageBoxResult rez = MessageBox.Show("Da li ste sigurni da zelite da otkazetu rezervaciju", "Rezervacija", MessageBoxButton.OKCancel);

        //    if (rez == MessageBoxResult.Cancel)
        //    {
                
        //    }
        //}

        private void btnRezervisi_Click(object sender, RoutedEventArgs e)
        {
            //SmartSoftwareServiceInterfaceClient service = new SmartSoftwareServiceInterfaceClient();

            
            //List<DbItemOpremaSaParametrimaRezervacija> rezervacije = new List<DbItemOpremaSaParametrimaRezervacija>();
            //DateTime sada = DateTime.Now;
            //DateTime datumIsteka = sada.AddDays(Convert.ToDouble(2));
           

            //string ime = "Kortana"; //OVDJE DA IZVUCEMO IZ NEKOG TEXTBOXA 
            //foreach (var oprema in this.Rezervacije)
            //{
            //    rezervacije.Add(new DbItemOpremaSaParametrimaRezervacija()

            //        {
            //            datum_rezervacije = sada,
            //            datum_isteka_rezervacije = datumIsteka,
            //            kolicina_rezervisane_opreme = (int)(oprema as Oprema).TmpIzabranaKolicina,
            //            imeNaRezervacija = ime,
            //            id_oprema = (oprema as Oprema).IdOprema
            //        });
            //}

            //SmartSoftwareServiceReference.OperationObject rez = service.RezervacijeInsert(rezervacije.ToArray());

        }
    }
}
