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

namespace SmartSoftware.Model
{
    public class Korisnici : INotifyPropertyChanged
    {

        private int idKorisnici;

        public int IdKorisnici
        {
            get { return idKorisnici; }
            set { SetAndNotify(ref idKorisnici, value); }
        }

        private string imeKorisnika;

        public string ImeKorisnika
        {
            get { return imeKorisnika; }
            set { SetAndNotify(ref imeKorisnika, value); }
        }

        private string prezimeKorisnika;

        public string PrezimeKorisnika
        {
            get { return prezimeKorisnika; }
            set { SetAndNotify(ref prezimeKorisnika, value); }
        }

        private string imeIPrezimeKorisnika;

        public string ImeIPrezimeKorisnika
        {
            get { return imeIPrezimeKorisnika; }
            set { SetAndNotify(ref imeIPrezimeKorisnika, value); }
        }

        private int izabranaUloga;

        public int IzabranaUloga
        {
            get { return izabranaUloga; }
            set { SetAndNotify(ref izabranaUloga, value); }
        }

        private string mejlKorisnika;

        public string MejlKorisnika
        {
            get { return mejlKorisnika; }
            set { SetAndNotify(ref mejlKorisnika, value); }
        }



        private string brojTelefonaKorisnika;

        public string BrojTelefonaKorisnika
        {
            get { return brojTelefonaKorisnika; }
            set { SetAndNotify(ref brojTelefonaKorisnika, value); }
        }

        private double brojOstvarenihPoena;

        public double BrojOstvarenihPoena
        {
            get { return brojOstvarenihPoena; }
            set { SetAndNotify(ref brojOstvarenihPoena, value); }
        }

        private int? brojKupovina;

        public int? BrojKupovina
        {
            get { return brojKupovina; }
            set { SetAndNotify(ref brojKupovina, value); }
        }


        private string username;

        public string Username
        {
            get { return username; }
            set { SetAndNotify(ref username, value); }
        }

        private string lozinka;

        public string Lozinka
        {
            get { return lozinka; }
            set { SetAndNotify(ref lozinka, value); }
        }

        private int brojPoenaZaPopust;

        public int BrojPoenaZaPopust
        {
            get { return brojPoenaZaPopust; }
            set { SetAndNotify(ref brojPoenaZaPopust, value); }
        }
        private int izabranBrojPoenaZaPopust;

        public int IzabranBrojPoenaZaPopust
        {
            get { return izabranBrojPoenaZaPopust; }
            set { SetAndNotify(ref izabranBrojPoenaZaPopust, value); }
        }

        private string nazivUloge;

        public string NazivUloge
        {
            get { return nazivUloge; }
            set { SetAndNotify(ref nazivUloge, value); }
        }

        private int idUloge;

        public int IdUloge
        {
            get { return idUloge; }
            set { SetAndNotify(ref idUloge, value); }
        }

        private bool deletedItem;

        public bool DeletedItem
        {
            get { return deletedItem; }
            set { SetAndNotify(ref deletedItem, value); }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Vrati iz arhive ovog korisnika";
                }
                else
                {
                    return "Arhiviraj ovog korisnika";
                }
            }
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
        public double SirinaSakrivenogBorderaZbogPorukeZaArhiviranje
        {
            get
            {
                if (this.DeletedItem)
                {
                    return 460;
                }
                else
                {
                    return 0;
                }
            }
        }

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

        private double visinaHederaImeIPrezimeKorisnici = 0;

        public double VisinaHederaImeIPrezimeKorisnici
        {
            get { return visinaHederaImeIPrezimeKorisnici; }
            set { SetAndNotify(ref visinaHederaImeIPrezimeKorisnici, value); }
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

        public Brush BojaCentralnogPrikazaKorisnici
        {
            get
            {
                if (this.DeletedItem)
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#3FDCDCDC"));
                }
                else
                {
                    GradientStop gs1 = new GradientStop((Color)ColorConverter.ConvertFromString("#FF2F5778"), 0.945);
                    GradientStop gs2 = new GradientStop(Colors.White, 0.207);
                    GradientStop gs3 = new GradientStop((Color)ColorConverter.ConvertFromString("#FFEBF0F4"), 0.425);
                    GradientStop gs4 = new GradientStop((Color)ColorConverter.ConvertFromString("#FFE9EBEE"), 0.702);
                    RadialGradientBrush rg1 = new RadialGradientBrush();
                    rg1.GradientOrigin = new Point(0.5, 0.5);
                    rg1.Center = new Point(0.5, 0.5);
                    rg1.RadiusX = 1.3;
                    rg1.RadiusY = 4;
                    rg1.GradientStops.Add(gs1);
                    rg1.GradientStops.Add(gs2);
                    rg1.GradientStops.Add(gs3);
                    rg1.GradientStops.Add(gs4);
                    return rg1;
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

        private bool daLiTekstImenaIPrezimenaZauzimaViseRedova = false;

        public bool DaLiTekstImenaIPrezimenaZauzimaViseRedova
        {
            get { return daLiTekstImenaIPrezimenaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstImenaIPrezimenaZauzimaViseRedova, value); }
        }

        private bool daLiTekstIzabraneUlogeZauzimaViseRedova = false;

        public bool DaLiTekstIzabraneUlogeZauzimaViseRedova
        {
            get { return daLiTekstIzabraneUlogeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstIzabraneUlogeZauzimaViseRedova, value); }
        }


        private bool? polKorisnika = false;

        public bool? PolKorisnika
        {
            get { return polKorisnika; }
            set { SetAndNotify(ref polKorisnika, value); }
        }

        private string slikaKorisnika;

        public string SlikaKorisnika
        {
            get { return slikaKorisnika; }
            set { SetAndNotify(ref slikaKorisnika, value); }
        }

        private DateTime? datumKreiranjaKorisnika;

        public DateTime? DatumKreiranjaKorisnika
        {
            get { return datumKreiranjaKorisnika; }
            set { SetAndNotify(ref datumKreiranjaKorisnika, value); }
        }

        private DateTime? datumAzuriranjaKorisnika;

        public DateTime? DatumAzuriranjaKorisnika
        {
            get { return datumAzuriranjaKorisnika; }
            set { SetAndNotify(ref datumAzuriranjaKorisnika, value); }
        }


        //private SolidColorBrush pozadinskaBojaZaTrivjuItemKorisnici;
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
        public SolidColorBrush PozadinskaBojaZaTrivjuItemKorisniciUloga
        {
            get
            {
                if (!this.DeletedItem)
                {
                    if (this.IzabranaUloga == 1)
                    {
                        return Brushes.Gray;
                        
                    }
                    else
                    {
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    }
                }
                else
                {
                    return Brushes.DarkGray;
                }
            }
        }

        public SolidColorBrush PozadinskaBojaZaTrivjuItemKorisniciUlogaKorpa
        {
            get
            {
                if (!this.DeletedItem)
                {
                    if (this.IzabranaUloga == 1)
                    {
                        return Brushes.DarkRed;

                    }
                    else if (this.IzabranaUloga == 2)
                    {
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
                    }
                    else
                    {
                        return Brushes.Gray;
                    }

                    
                }
                else
                {
                    return Brushes.DarkGray;
                }
            }
        }



        public bool daLiJeMuskiPolCekiran;
        public bool DaLiJeMuskiPolCekiran
        {
            get { return daLiJeMuskiPolCekiran; }
            set { SetAndNotify(ref daLiJeMuskiPolCekiran, value); }
        }

        public bool daLiJeZenskiPolCekiran;
        public bool DaLiJeZenskiPolCekiran
        {
            get { return daLiJeZenskiPolCekiran; }
            set { SetAndNotify(ref daLiJeZenskiPolCekiran, value); }
        }

        private bool daLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova = false;

        public bool DaLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova
        {
            get { return daLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstImenaIPrezimenaKorisniciZauzimaViseRedova, value); }
        }

        private bool daLiTekstImenaNaruciocaZauzimaViseRedova = false;

        public bool DaLiTekstImenaNaruciocaZauzimaViseRedova
        {
            get { return daLiTekstImenaNaruciocaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstImenaNaruciocaZauzimaViseRedova, value); }
        }
        private bool daLiTekstPrezimenaNaruciocaZauzimaViseRedova = false;

        public bool DaLiTekstPrezimenaNaruciocaZauzimaViseRedova
        {
            get { return daLiTekstPrezimenaNaruciocaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstPrezimenaNaruciocaZauzimaViseRedova, value); }
        }

        private bool daLiTekstMejlaNaruciocaZauzimaViseRedova = false;

        public bool DaLiTekstMejlaNaruciocaZauzimaViseRedova
        {
            get { return daLiTekstMejlaNaruciocaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstMejlaNaruciocaZauzimaViseRedova, value); }
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


    public class Kupci : Korisnici
    {
        private ObservableCollection<IstorijaKupovine> listaIstorijeKupovine = new ObservableCollection<IstorijaKupovine>();

        public ObservableCollection<IstorijaKupovine> ListaIstorijeKupovine
        {
            get { return listaIstorijeKupovine; }
            set { SetAndNotify(ref listaIstorijeKupovine, value); }
        }

       


        

        private double? najvisePotrosio;

        public double? NajvisePotrosio
        {
            get { return najvisePotrosio; }
            set { SetAndNotify(ref najvisePotrosio, value); }
        }


        //private int? brojKupovina;

        //public int? BrojKupovina
        //{
        //    get { return brojKupovina; }
        //    set { SetAndNotify(ref brojKupovina, value); }
        //}

        private int? brojKupljeneOpreme;

        public int? BrojKupljeneOpreme
        {
            get { return brojKupljeneOpreme; }
            set { brojKupljeneOpreme = value; }
        }


    }
    
}
