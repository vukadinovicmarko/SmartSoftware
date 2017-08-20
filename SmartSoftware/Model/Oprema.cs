using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace SmartSoftware.Model
{
    public class Oprema : SmartSoftwareGlavnaOblast
    {

        

        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }

        private int idOprema;

        public int IdOprema
        {
            get { return idOprema; }
            set { SetAndNotify(ref idOprema, value); }
        }

        private int izabranaKolicina = 0;

        public int IzabranaKolicina
        {
            get { return izabranaKolicina; }
            set { SetAndNotify(ref izabranaKolicina, value);}
        }

        private int izabranaKolicinaZaRezervisanje = 0;

        public int IzabranaKolicinaZaRezervisanje
        {
            get { return izabranaKolicinaZaRezervisanje; }
            set { SetAndNotify(ref izabranaKolicinaZaRezervisanje, value); }
        }

        private int izabranaKolicinaZaRezervisanjeGlavniProzor = 0;

        public int IzabranaKolicinaZaRezervisanjeGlavniProzor
        {
            get { return izabranaKolicinaZaRezervisanjeGlavniProzor; }
            set { SetAndNotify(ref izabranaKolicinaZaRezervisanjeGlavniProzor, value); }
        }

        private int? tmpIzabranaKolicina = 1;

        public int? TmpIzabranaKolicina
        {
            get { return tmpIzabranaKolicina; }
            set { SetAndNotify(ref tmpIzabranaKolicina, value); }
        }

        private bool daliMozeJosDaSeDoda = true;

        public bool DaliMozeJosDaseDoda
        {
            get { return daliMozeJosDaSeDoda; }
            set { SetAndNotify(ref daliMozeJosDaSeDoda, value); }
        }

        private bool daliMozeJosDaseDodaURezervacije = false;

        public bool DaliMozeJosDaseDodaURezervacije
        {
            get { return daliMozeJosDaseDodaURezervacije; }
            set { SetAndNotify(ref daliMozeJosDaseDodaURezervacije, value); }
        }

        

        private int? kolikoPutajeProdata;

        public int? KolikoPutajeProdata
        {
            get { return kolikoPutajeProdata; }
            set { SetAndNotify(ref kolikoPutajeProdata, value); }
        }

        private double sumCena;

        public double SumCena
        {
            get {
                SumCena = this.izabranaKolicina * this.cena;
                return sumCena; 
            }
            set {
                SetAndNotify(ref sumCena, value);
            }
        }

        private double sumCenaZaRezervisanje;

        public double SumCenaZaRezervisanje
        {
            get
            {
                sumCenaZaRezervisanje = this.izabranaKolicinaZaRezervisanje * this.cena;
                return sumCenaZaRezervisanje;
            }
            set
            {
                SetAndNotify(ref sumCenaZaRezervisanje, value);
            }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Vrati iz arhive ovu opremu";
                }
                else
                {
                    return "Arhiviraj ovu opremu";
                }
            }
        }

        private bool daLiTekstNaslovaZauzimaViseRedova = false;

        public bool DaLiTekstNaslovaZauzimaViseRedova
        {
            get { return daLiTekstNaslovaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstNaslovaZauzimaViseRedova, value); }
        }

        private bool daLiTekstIzabranogTipaOpremeZauzimaViseRedova = false;

        public bool DaLiTekstIzabranogTipaOpremeZauzimaViseRedova
        {
            get { return daLiTekstIzabranogTipaOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstIzabranogTipaOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova = false;

        public bool DaLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova
        {
            get { return daLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstNaslovaOpremeUNarudzbinamaZauzimaViseRedova, value); }
        }


        private bool daLiJeDugmePrihvatiNarudzbinuOmoguceno = false;

        public bool DaLiJeDugmePrihvatiNarudzbinuOmoguceno
        {
            get { return daLiJeDugmePrihvatiNarudzbinuOmoguceno; }
            set { SetAndNotify(ref daLiJeDugmePrihvatiNarudzbinuOmoguceno, value); }
        }



        public string VratiSlikuZaDugmeIzbrisi
        {
            get
            {
                if (this.DeletedItem)
                {


                    return App.rootPath + "\\slike\\restore-128-Green.png";
                }
                else
                {
                    return App.rootPath + "\\slike\\delete-128-Red.png";
                }
            }
        }

        public string VratiSlikuZaDugmeIzbrisiHoverovano
        {
            get
            {
                if (this.DeletedItem)
                {

                    return App.rootPath + "\\slike\\restore-128-White.png";
                }
                else
                {
                    return App.rootPath + "\\slike\\delete-128-White.png";
                }
            }
        }

        private SolidColorBrush bojaTekstaDugmetaArhiviraj;

        public SolidColorBrush BojaTekstaDugmetaArhiviraj
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Brushes.DarkGreen;
                }
                else
                {
                    return Brushes.DarkRed;
                }
            }
            set { SetAndNotify(ref bojaTekstaDugmetaArhiviraj, value); }
        }


        private bool daLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme = false;

        public bool DaLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme
        {
            get { return daLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme; }
            set { SetAndNotify(ref daLiTekstNaslovaZauzimaViseRedovaUDetaljnomPrikazuOpreme, value); }
        }

        private bool daLiTekstProizvodjacaZauzimaViseRedova = false;

        public bool DaLiTekstProizvodjacaZauzimaViseRedova
        {
            get { return daLiTekstProizvodjacaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstProizvodjacaZauzimaViseRedova, value); }
        }

        private bool daLiTekstModelaZauzimaViseRedova = false;

        public bool DaLiTekstModelaZauzimaViseRedova
        {
            get { return daLiTekstModelaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstModelaZauzimaViseRedova, value); }
        }

        private bool daLiTekstOpisaZauzimaViseRedova = false;

        public bool DaLiTekstOpisaZauzimaViseRedova
        {
            get { return daLiTekstOpisaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstOpisaZauzimaViseRedova, value); }
        }

        private bool daLiTekstCeneZauzimaViseRedova = false;

        public bool DaLiTekstCeneZauzimaViseRedova
        {
            get { return daLiTekstCeneZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstCeneZauzimaViseRedova, value); }
        }

        private bool daLiTekstKolicineZauzimaViseRedova = false;

        public bool DaLiTekstKolicineZauzimaViseRedova
        {
            get { return daLiTekstKolicineZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstKolicineZauzimaViseRedova, value); }
        }


        private bool daLiTekstUkupneCeneZauzimaViseRedova = false;

        public bool DaLiTekstUkupneCeneZauzimaViseRedova
        {
            get { return daLiTekstUkupneCeneZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstUkupneCeneZauzimaViseRedova, value); }
        }


        private string proizvodjac;

        public string Proizvodjac
        {
            get { return proizvodjac; }
            set { SetAndNotify(ref proizvodjac, value); }
        }

        private double cena;

        public double Cena
        {
            get { return cena; }
            set { SetAndNotify(ref cena, value); }
        }

        private string opis;

        public string Opis
        {
            get { return opis; }
            set { SetAndNotify(ref opis, value); }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { SetAndNotify(ref model, value); }
        }


        private bool? lager;

        public bool? Lager
        {
            get { return lager; }
            set { SetAndNotify(ref lager, value); }
        }

        private int? kolicinaURezervi;

        public int? KolicinaURezervi
        {
            get { return kolicinaURezervi; }
            set { SetAndNotify(ref kolicinaURezervi, value); }
        }


        private int? kolicinaNaLageru;

        public int? KolicinaNaLageru
        {
            get { return kolicinaNaLageru; }
            set { SetAndNotify(ref kolicinaNaLageru, value); }
        }


        private int? kolicinaNaLageruZaRezervisanje = 0;

        public int? KolicinaNaLageruZaRezervisanje
        {
            get { return kolicinaNaLageruZaRezervisanje; }
            set { SetAndNotify(ref kolicinaNaLageruZaRezervisanje, value); }
        }


        private int? tmpKolicinaNaLageru;

        public int? TmpKolicinaNaLageru
        {
            get { return tmpKolicinaNaLageru; }
            set { SetAndNotify(ref tmpKolicinaNaLageru, value); }
        }

        private int? tmp2KolicinaNaLageru;

        public int? Tmp2KolicinaNaLageru
        {
            get { return tmp2KolicinaNaLageru; }
            set { SetAndNotify(ref tmp2KolicinaNaLageru, value); }
        }


        private int? opremaNaPopustu;

        public int? OpremaNaPopustu
        {
            get { return opremaNaPopustu; }
            set { SetAndNotify(ref opremaNaPopustu, value); }
        }


        private string slika;

        public string Slika
        {
            get { return slika; }
            set { SetAndNotify(ref slika, value); }
        }

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }


        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }

        private string naziOdabranogTipaOpreme;

        public string NaziOdabranogTipaOpreme
        {
            get { return naziOdabranogTipaOpreme; }
            set { SetAndNotify(ref naziOdabranogTipaOpreme, value); }
        }

        public double VisinaSakrivenogBorderaZbogPorukeZaArhiviranje
        {
            get
            {
                if (this.DeletedItem)
                {
                    return 30;
                }
                else
                {
                    return 0;
                }
            }
        }
        //public double SirinaSakrivenogBorderaZbogPorukeZaArhiviranje
        //{
        //    get
        //    {
        //        if (this.DeletedItem)
        //        {
        //            return 460;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        public Visibility VisibilityPorukeZaArhiviranje
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        //private bool daLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;

        //public bool DaLiTekstNaslovaTipaOpremeZauzimaViseRedova
        //{
        //    get { return daLiTekstNaslovaTipaOpremeZauzimaViseRedova; }
        //    set { SetAndNotify(ref daLiTekstNaslovaTipaOpremeZauzimaViseRedova, value); }
        //}


        //private bool daLiTekstTipaOpremeZauzimaViseRedova = false;

        //public bool DaLiTekstTipaOpremeZauzimaViseRedova
        //{
        //    get { return daLiTekstTipaOpremeZauzimaViseRedova; }
        //    set { SetAndNotify(ref daLiTekstTipaOpremeZauzimaViseRedova, value); }
        //}

        private double visinaHederaImeIPrezimeKorisnici = 0;

        public double VisinaHederaImeIPrezimeKorisnici
        {
            get { return visinaHederaImeIPrezimeKorisnici; }
            set { SetAndNotify(ref visinaHederaImeIPrezimeKorisnici, value); }
        }

        private double visinaHederaNaslovaHedera = 0;

        public double VisinaHederaNaslovaHedera
        {
            get { return visinaHederaNaslovaHedera; }
            set { SetAndNotify(ref visinaHederaNaslovaHedera, value); }
        }

        private SolidColorBrush bojaTekstaZaTrivjuItemKorisnici;

        public SolidColorBrush BojaTekstaZaTrivjuItemKorisnici
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Brushes.Gray;
                }
                else
                {
                    return Brushes.Black as SolidColorBrush;
                }
            }
            set { SetAndNotify(ref bojaTekstaZaTrivjuItemKorisnici, value); }
        }

        public SolidColorBrush BojaTekstaZaTrivjuItemKorisniciHeder
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Brushes.Gray;
                }
                else
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                }
            }
        }
        public FontStyle StilFontaZaTrivjuItemKorisnici
        {

            get
            {
                if (this.DeletedItem)
                {
                    return FontStyles.Italic;
                }
                else
                {
                    return FontStyles.Normal;
                }
            }
        }
        public SolidColorBrush BojaTekstaZaTrivjuItemKorisniciTipOpreme
        {
            get
            {
                if (!this.DeletedItem)
                {
                    
                       
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    
                }
                else
                {
                    return Brushes.DarkGray;
                }
            }
        }

        private bool daLiJeKliknutoNaGrid = false;

        public bool DaLiJeKliknutoNaGrid
        {
            get { return daLiJeKliknutoNaGrid; }
            set { SetAndNotify(ref daLiJeKliknutoNaGrid, value); }
        }
      

        public Oprema(OblastiOpreme parent)
            : base(parent)
        {

        }

        private bool daLiJeSlikaOpremePromenjena = false;

        public bool DaLiJeSlikaOpremePromenjena
        {
            get { return daLiJeSlikaOpremePromenjena; }
            set { daLiJeSlikaOpremePromenjena = value; }
        }


        private ObservableCollection<Parametri> listaParametara = new ObservableCollection<Parametri>();

        public ObservableCollection<Parametri> ListaParametara
        {
            get { return listaParametara; }
            //set { SetAndNotify(ref listaParametara, value); }
        }


        private ObservableCollection<Oprema> kolekcijaOpreme = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> KolekcijaOpreme
        {
            get { return kolekcijaOpreme; }
            set { kolekcijaOpreme = value; }
        }

    }

    public class KupljenaOprema : Oprema
    {
        private int? prodataKolicina;

        public int? ProdataKolicina
        {
            get { return prodataKolicina; }
            set { SetAndNotify(ref prodataKolicina, value); }
        }

        private double? cena_opreme_kad_je_prodata;

        public double? Cena_opreme_kad_je_prodata
        {
            get { return cena_opreme_kad_je_prodata; }
            set { SetAndNotify(ref cena_opreme_kad_je_prodata, value); }
        }

        private double? popust_na_cenu;

        public double? Popust_na_cenu
        {
            get { return popust_na_cenu; }
            set { SetAndNotify(ref popust_na_cenu, value); }
        }

        private int? ukupna_cena_artikla;

        public int? Ukupna_cena_artikla
        {
            get { return ukupna_cena_artikla; }
            set { SetAndNotify(ref ukupna_cena_artikla, value); }
        }

        private int id_istorija_kupovine;

        public int Id_istorija_kupovine
        {
            get { return id_istorija_kupovine; }
            set { SetAndNotify(ref id_istorija_kupovine, value); }
        }

        public KupljenaOprema(OblastiOpreme parent) : base(parent)
        {

        }
    }




    public class Narudzbina : INotifyPropertyChanged
    {
        private Oprema oprema;

        public Oprema Oprema
        {
            get { return oprema; }
            set { SetAndNotify(ref oprema, value); }
        }

        private int idNarudzbine;

        public int IdNarudzbine
        {
            get { return idNarudzbine; }
            set { SetAndNotify(ref idNarudzbine, value); }
        }


        private DateTime? datumNarudzbine;

        public DateTime? DatumNarudzbine
        {
            get { return datumNarudzbine; }
            set { SetAndNotify(ref datumNarudzbine, value); }
        }
        private int narucenaKolicina;

        public int NarucenaKolicina
        {
            get { return narucenaKolicina; }
            set { SetAndNotify(ref narucenaKolicina, value); }
        }
        private Korisnici prodavac;

        public Korisnici Prodavac
        {
            get { return prodavac; }
            set { SetAndNotify(ref prodavac, value); }
        }

        private int redniBrojNarudzbine;

        public int RedniBrojNarudzbine
        {
            get { return redniBrojNarudzbine; }
            set { SetAndNotify(ref redniBrojNarudzbine, value); }
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
    }

    public class GrupaOgranicenja : INotifyPropertyChanged
    {

        private int id_grupe_ogranicenja;

        public int Id_grupe_ogranicenja
        {
            get { return id_grupe_ogranicenja; }
            set { SetAndNotify(ref id_grupe_ogranicenja, value); }
        }
        private int id_tip_opreme1;

        public int Id_tip_opreme1
        {
            get { return id_tip_opreme1; }
            set { SetAndNotify(ref id_tip_opreme1, value); }
        }

        private string nazivTipaOpreme1;

        public string NazivTipaOpreme1
        {
            get { return nazivTipaOpreme1; }
            set { SetAndNotify(ref nazivTipaOpreme1, value); }
        }

        private int id_parametra1;

        public int Id_parametra1
        {
            get { return id_parametra1; }
            set { SetAndNotify(ref id_parametra1, value); }
        }
        
        private string nazivParametra1;

        public string NazivParametra1
        {
            get { return nazivParametra1; }
            set { SetAndNotify(ref nazivParametra1, value); }
        }


        private int? id_tip_opreme2;

        public int? Id_tip_opreme2
        {
            get { return id_tip_opreme2; }
            set { SetAndNotify(ref id_tip_opreme2, value); }
        }

        private string nazivTipaOpreme2;

        public string NazivTipaOpreme2
        {
            get { return nazivTipaOpreme2; }
            set { SetAndNotify(ref nazivTipaOpreme2, value); }
        }

        private int? id_parametra2;

        public int? Id_parametra2
        {
            get { return id_parametra2; }
            set { SetAndNotify(ref id_parametra2, value); }
        }

        private string nazivParametra2;
                                                    
        public string NazivParametra2
        {
            get { return nazivParametra2; }
            set { SetAndNotify(ref nazivParametra2, value); }
        }
        private string tipProvere;

        public string TipProvere
        {
            get { return tipProvere; }
            set { SetAndNotify(ref tipProvere, value); }
        }


        private int id_tip_opreme_kolekcije;

        public int Id_tip_opreme_kolekcije
        {
            get { return id_tip_opreme_kolekcije; }
            set { SetAndNotify(ref id_tip_opreme_kolekcije, value); }
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
    }



}
