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
    public class TipoviOpreme : SmartSoftwareGlavnaOblast
    {

        private string nazivTipa;

        public string NazivTipa
        {
            get { return nazivTipa; }
            set { SetAndNotify(ref nazivTipa, value); }
        }

        private int idTipOpreme;

        public int IdTipOpreme
        {
            get { return idTipOpreme; }
            set { SetAndNotify(ref idTipOpreme, value); }
        }

        private int? idOblastiOpreme;

        public int? IdOblastiOpreme
        {
            get { return idOblastiOpreme; }
            set { SetAndNotify(ref idOblastiOpreme, value); }
        }

        private int tipSlika;

        public int TipSlika
        {
            get { return tipSlika; }
            set { SetAndNotify(ref tipSlika, value); }
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

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Vrati iz arhive ovaj tip opreme";
                }
                else
                {
                    return "Arhiviraj ovaj tip opreme";
                }
            }
        }

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }

        private string staroImeTipa;

        public string StaroImeTipa
        {
            get { return staroImeTipa; }
            set { SetAndNotify(ref staroImeTipa, value); }
        }

        private bool daliMozeTipDaSeAzurira = false;

        public bool DaliMozeTipDaSeAzurira
        {
            get { return daliMozeTipDaSeAzurira; }
            set { SetAndNotify(ref daliMozeTipDaSeAzurira, value); }
        }



        private bool daLiJeSlikaTipaPromenjena = false;

        public bool DaLiJeSlikaTipaPromenjena
        {
            get { return daLiJeSlikaTipaPromenjena; }
            set { SetAndNotify(ref daLiJeSlikaTipaPromenjena, value); }
        }
        private bool daLiJeOblastPromenjena = false;

        public bool DaLiJeOblastPromenjena1
        {
            get { return daLiJeOblastPromenjena; }
            set { SetAndNotify(ref daLiJeOblastPromenjena, value); }
        }
        private bool daLiJeTekstTipaOpremePromenjen = false;

        public bool DaLiJeTekstTipaOpremePromenjen
        {
            get { return daLiJeTekstTipaOpremePromenjen; }
            set { SetAndNotify(ref daLiJeTekstTipaOpremePromenjen, value); }
        }

        public bool DaLiJeOblastPromenjena
        {
            get { return daLiJeOblastPromenjena; }
            set { SetAndNotify(ref daLiJeOblastPromenjena, value); }
        }

        private string nazivOblastiOpreme;

        public string NazivOblastiOpreme
        {
            get { return nazivOblastiOpreme; }
            set { SetAndNotify(ref nazivOblastiOpreme, value); }
        }

        private int izabranTipOpreme;

        public int IzabranTipOpreme
        {
            get { return izabranTipOpreme; }
            set { SetAndNotify(ref izabranTipOpreme, value); }
        }

        private bool daLiJeKliknutoNaGrid = false;

        public bool DaLiJeKliknutoNaGrid
        {
            get { return daLiJeKliknutoNaGrid; }
            set { SetAndNotify(ref daLiJeKliknutoNaGrid, value); }
        }

        public SolidColorBrush BojaTekstaZaTrivjuItemTipoviOpreme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Brushes.DarkGray;
                }
                else
                {
                    return Brushes.Black as SolidColorBrush;
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
        public FontStyle StilFontaZaTrivjuItemTipoviOpreme
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
        public SolidColorBrush PozadinskaBojaZaTrivjuItemTipoviOpremeOblastOpreme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return Brushes.DarkGray;
                }
                else
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2F5778"));
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

        public Brush BojaCentralnogPrikazaTipoviOpreme
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

        private bool daLiTekstNaslovaTipaOpremeZauzimaViseRedova = false;

        public bool DaLiTekstNaslovaTipaOpremeZauzimaViseRedova
        {
            get { return daLiTekstNaslovaTipaOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstNaslovaTipaOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstHederaListeOblastiOpremeZauzimaViseRedova = false;

        public bool DaLiTekstHederaListeOblastiOpremeZauzimaViseRedova
        {
            get { return daLiTekstHederaListeOblastiOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstHederaListeOblastiOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstTipaOpremeZauzimaViseRedova = false;

        public bool DaLiTekstTipaOpremeZauzimaViseRedova
        {
            get { return daLiTekstTipaOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstTipaOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstIzabraneOblastiOpremeZauzimaViseRedova = false;

        public bool DaLiTekstIzabraneOblastiOpremeZauzimaViseRedova
        {
            get { return daLiTekstIzabraneOblastiOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstIzabraneOblastiOpremeZauzimaViseRedova, value); }
        }

        public TipoviOpreme(OblastiOpreme parent)
            : base(parent)
        {

        }

        private double visinaHederaImeIPrezimeKorisnici = 0;

        public double VisinaHederaImeIPrezimeKorisnici
        {
            get { return visinaHederaImeIPrezimeKorisnici; }
            set { SetAndNotify(ref visinaHederaImeIPrezimeKorisnici, value); }
        }

        private ObservableCollection<Oprema> opremaKolekcija = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> OpremaKolekcija
        {
            get { return opremaKolekcija; }
        }

        


        
    }

    public class TipoviKofiguracije : TipoviOpreme
    {

        public TipoviKofiguracije(OblastiOpreme parent)
            : base(parent)
        {

        }

        private bool? izabranZaKonfiguraciju;

        public bool? IzabranZaKonfiguraciju
        {
            get { return izabranZaKonfiguraciju; }
            set { SetAndNotify(ref izabranZaKonfiguraciju, value); }
        }
        private int? idTipOpremeKolekcije;

        public int? IdTipOpremeKolekcije
        {
            get { return idTipOpremeKolekcije; }
            set { SetAndNotify(ref idTipOpremeKolekcije, value); }
        }
        private int? redosledPrikazivanja;

        public int? RedosledPrikazivanja
        {
            get { return redosledPrikazivanja; }
            set { SetAndNotify(ref redosledPrikazivanja, value); }
        }
        private int? izabranRedosled;

        public int? IzabranRedosled
        {
            get { return izabranRedosled; }
            set { SetAndNotify(ref izabranRedosled, value); }
        }

        private int mogucaKolicinaUnosa;

        public int MogucaKolicinaUnosa
        {
            get { return mogucaKolicinaUnosa; }
            set { SetAndNotify(ref mogucaKolicinaUnosa, value); }
        }

        private ObservableCollection<string> listaBrojevaZaRedosled = new ObservableCollection<string>();

        public ObservableCollection<string> ListaBrojevaZaRedosled
        {
            get { return listaBrojevaZaRedosled; }
            set { SetAndNotify(ref listaBrojevaZaRedosled, value); }
        }

        

    }
}
