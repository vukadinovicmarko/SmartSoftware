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
    public class Parametri : SmartSoftwareGlavnaOblast
    {

        public Parametri(OblastiOpreme oblastiOpreme)
            :base(oblastiOpreme)
        {

        }
       
        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }


        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }

        private bool parametarJeIFilter;

        public bool ParametarJeIFilter
        {
            get { return parametarJeIFilter; }
            set { SetAndNotify(ref parametarJeIFilter, value); }
        }


        private int idParametri;

        public int IdParametri
        {
            get { return idParametri; }
            set { SetAndNotify(ref idParametri, value); }
        }

        private string defaultVrednost;

        public string DefaultVrednost
        {
            get { return defaultVrednost; }
            set { SetAndNotify(ref defaultVrednost, value); }
        }

        private string vrednostParametra;

        public string VrednostParametra
        {
            get { return vrednostParametra; }
            set { SetAndNotify(ref vrednostParametra, value); }
        }

        private ObservableCollection<VrednostiFiltera> kolekcijaVrednostiZaFilter = new ObservableCollection<VrednostiFiltera>();

        public ObservableCollection<VrednostiFiltera> KolekcijaVrednostiZaFilter
        {
            get { return kolekcijaVrednostiZaFilter; }
        }

        private bool odabranParametarZaFiltere = false;

        public bool OdabranParametarZaFiltere
        {
            get { return odabranParametarZaFiltere; }
            set { odabranParametarZaFiltere = value; }
        }


        private bool izmenjenaVrednostParamertra;

        public bool IzmenjenaVrednostParamertra
        {
            get { return izmenjenaVrednostParamertra; }
            set {
                bool vrednost = this.VrednostParametra.Length > 0 ? true : false;
                SetAndNotify(ref izmenjenaVrednostParamertra, vrednost);
            }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Vrati iz arhive ovaj parametar";
                }
                else
                {
                    return "Arhiviraj ovaj parametar";
                }
            }
        }

        private string tipParametra;

        public string TipParametra
        {
            get { return tipParametra; }
            set { SetAndNotify(ref tipParametra, value); }
        }

        private string nazivTipa;

        public string NazivTipa
        {
            get { return nazivTipa; }
            set { SetAndNotify(ref nazivTipa, value); }
        }

        private SolidColorBrush prikaziBojom;

        public SolidColorBrush PrikaziBojom
        {
            get { return prikaziBojom; }
            set {
                SolidColorBrush vrednost = this.izmenjenaVrednostParamertra == true ? Brushes.White : Brushes.Gray;
                SetAndNotify(ref prikaziBojom, vrednost);
            }
        }

        private SolidColorBrush bojaTekstaZaTrivjuItemParametri;

        public SolidColorBrush BojaTekstaZaTrivjuItemParametri
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
            set { SetAndNotify(ref bojaTekstaZaTrivjuItemParametri, value); }
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


        
       

        public SolidColorBrush BojaTekstaZaTrivjuItemParametriHeder
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
        public FontStyle StilFontaZaTrivjuItemParametri
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

        public Brush BojaCentralnogPrikazaParametriIFilteri
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


        private bool daLiTekstNazivaParametraZauzimaViseRedova = false;

        public bool DaLiTekstNazivaParametraZauzimaViseRedova
        {
            get { return daLiTekstNazivaParametraZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstNazivaParametraZauzimaViseRedova, value); }
        }

        private bool daLiTekstIzabranogTipaOpremeZauzimaViseRedova = false;

        public bool DaLiTekstIzabranogTipaOpremeZauzimaViseRedova
        {
            get { return daLiTekstIzabranogTipaOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstIzabranogTipaOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstHederaNaslovaParametraZauzimaViseRedova = false;

        public bool DaLiTekstHederaNaslovaParametraZauzimaViseRedova
        {
            get { return daLiTekstHederaNaslovaParametraZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstHederaNaslovaParametraZauzimaViseRedova, value); }
        }

        private bool daLiTekstHederaTipaOpremeZauzimaViseRedova = false;

        public bool DaLiTekstHederaTipaOpremeZauzimaViseRedova
        {
            get { return daLiTekstHederaTipaOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstHederaTipaOpremeZauzimaViseRedova, value); }
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

        private double visinaHederaImeIPrezimeKorisnici = 0;

        public double VisinaHederaImeIPrezimeKorisnici
        {
            get { return visinaHederaImeIPrezimeKorisnici; }
            set { SetAndNotify(ref visinaHederaImeIPrezimeKorisnici, value); }
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

        
    }

    

    public class VrednostiFiltera
    {
        public string VrednostFiltera { get; set; }
        public int idVrednostiFiltera { get; set; }
        public int idParametra { get; set; }
        
        private bool odabranaVrednostZaFiltere = false;        
        public bool OdabranaVrednostZaFiltere
        {
            get { return odabranaVrednostZaFiltere; }
            set { odabranaVrednostZaFiltere = value; }
        }
    }
}
