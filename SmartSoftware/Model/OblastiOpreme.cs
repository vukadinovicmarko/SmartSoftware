using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SmartSoftware.Model
{
    //[DataContract]
    //[Serializable]
    public class OblastiOpreme : SmartSoftwareGlavnaOblast
    {
        //[DataMember]
        private ObservableCollection<SmartSoftwareGlavnaOblast> items = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Items
        {
            get { return items; }
            set { SetAndNotify(ref items, value); }
        }

        public OblastiOpreme(OblastiOpreme parent)
            : base(parent)
        {

        }

        private string slikaOriginalPutanja;

        public string SlikaOriginalPutanja
        {
            get { return slikaOriginalPutanja; }
            set { SetAndNotify(ref slikaOriginalPutanja, value); }
        }


        private bool daliMozeDaSeAzurira = false;

        public bool DaliMozeDaSeAzurira
        {
            get { return daliMozeDaSeAzurira; }
            set
            {
                SetAndNotify(ref daliMozeDaSeAzurira, value);
                
            }
        }

        public string VratiTextZaDugme
        {
            get
            {
                if (this.DeletedItem)
                {
                    return "Vrati iz arhive ovu oblast opreme";
                }
                else
                {
                    return "Arhiviraj ovu oblast opreme";
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

        private bool daLiTekstNaslovaOblastiOpremeZauzimaViseRedova = false;

        public bool DaLiTekstNaslovaOblastiOpremeZauzimaViseRedova
        {
            get { return daLiTekstNaslovaOblastiOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstNaslovaOblastiOpremeZauzimaViseRedova, value); }
        }

        private bool daLiTekstListeOblastiOpremeZauzimaViseRedova = false;

        public bool DaLiTekstListeOblastiOpremeZauzimaViseRedova
        {
            get { return daLiTekstListeOblastiOpremeZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstListeOblastiOpremeZauzimaViseRedova, value); }
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

        private bool daLiTekstImenaIPrezimenaZauzimaViseRedova = false;

        public bool DaLiTekstImenaIPrezimenaZauzimaViseRedova
        {
            get { return daLiTekstImenaIPrezimenaZauzimaViseRedova; }
            set { SetAndNotify(ref daLiTekstImenaIPrezimenaZauzimaViseRedova, value); }
        }
        
        private string nazivOblastiOpreme;
        //[DataMember(Name = "NazivOblastiOpreme", IsRequired = false)]
        public string NazivOblastiOpreme
        {
            get { return nazivOblastiOpreme; }
            set { SetAndNotify(ref nazivOblastiOpreme, value); }
        }

        private int idOblastiOpreme;
        //[DataMember(Name = "IdOblastiOpreme", IsRequired = false)]
        public int IdOblastiOpreme
        {
            get { return idOblastiOpreme; }
            set { SetAndNotify(ref idOblastiOpreme, value); }
        }

       

    }
}
