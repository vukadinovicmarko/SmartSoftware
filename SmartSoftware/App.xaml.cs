using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SmartSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string proba = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string rootPath = Directory.GetParent(proba).Parent.FullName;


        private static string putanjaDoSlikeProdavacKorisnici = App.rootPath + "\\slike\\guest-128.png";
        public static string PutanjaDoSlikeProdavacKorisnici
        {
            get { return putanjaDoSlikeProdavacKorisnici; }

        }

        private static string putanjaDoSlikeAdministratorKorisnici = App.rootPath + "\\slike\\administrator-128.png";
        public static string PutanjaDoSlikeAdministratorKorisnici
        {
            get { return putanjaDoSlikeAdministratorKorisnici; }

        }

        private static string putanjaDoSlikeNoImage = App.rootPath + "\\slike\\noImage.png";
        public static string PutanjaDoSlikeNoImage
        {
            get { return putanjaDoSlikeNoImage; }

        }
        
        
        


        






        

       
    }

    public class ProbaZaSlike : INotifyPropertyChanged
    {

        private string putanjaDoSlikeLogin = App.rootPath + "\\slike\\account-login-128-Blue.png";

        public string PutanjaDoSlikeLogin
        {
            get { return putanjaDoSlikeLogin; }
            set { SetAndNotify(ref putanjaDoSlikeLogin, value); }
        }

        private string putanjaDoSlikeLoginHoverovano = App.rootPath + "\\slike\\account-login-128-White.png";

        public string PutanjaDoSlikeLoginHoverovano
        {
            get { return putanjaDoSlikeLoginHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeLoginHoverovano, value); }
        }



        private string putanjaDoSlikeLoginOnemoguceno = App.rootPath + "\\slike\\account-login-128-Gray.png";

        public string PutanjaDoSlikeLoginOnemoguceno
        {
            get { return putanjaDoSlikeLoginOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeLoginOnemoguceno, value); }
        }




        private string putanjaDoSlikeLogo = App.rootPath + "\\slike\\software-128-Blue.png";

        public string PutanjaDoSlikeLogo
        {
            get { return putanjaDoSlikeLogo; }
            set { SetAndNotify(ref putanjaDoSlikeLogo, value); }
        }

        private string putanjaDoSlikeLogoNormalno = App.rootPath + "\\slike\\software-128-White.png";

        public string PutanjaDoSlikeLogoNormalno
        {
            get { return putanjaDoSlikeLogoNormalno; }
            set { SetAndNotify(ref putanjaDoSlikeLogoNormalno, value); }
        }

        private string putanjaDoSlikeDugmeClose = App.rootPath + "\\slike\\close-128.png";

        public string PutanjaDoSlikeDugmeClose
        {
            get { return putanjaDoSlikeDugmeClose; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeClose, value); }
        }

        private string putanjaDoSlikeDugmeCloseHoverovano = App.rootPath + "\\slike\\close-128-Red.png";

        public string PutanjaDoSlikeDugmeCloseHoverovano
        {
            get { return putanjaDoSlikeDugmeCloseHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeCloseHoverovano, value); }
        }

        private string putanjaDoSlikeDugmeMinimize = App.rootPath + "\\slike\\minimize-128.png";

        public string PutanjaDoSlikeDugmeMinimize
        {
            get { return putanjaDoSlikeDugmeMinimize; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeMinimize, value); }
        }

        private string putanjaDoSlikeDugmeMaximize = App.rootPath + "\\slike\\maximize-128.png";

        public string PutanjaDoSlikeDugmeMaximize
        {
            get { return putanjaDoSlikeDugmeMaximize; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeMaximize, value); }
        }

        private string putanjaDoSlikeDugmeMaximizeDodatni = App.rootPath + "\\slike\\maximizeDodatni-128.png";

        public string PutanjaDoSlikeDugmeMaximizeDodatni
        {
            get { return putanjaDoSlikeDugmeMaximizeDodatni; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeMaximizeDodatni, value); }
        }

        


        private string putanjaDoSlikeExpand = App.rootPath + "\\slike\\arrowDole-128-White.png";

        public string PutanjaDoSlikeExpand
        {
            get { return putanjaDoSlikeExpand; }
            set { SetAndNotify(ref putanjaDoSlikeExpand, value); }
        }




        private string putanjaDoSlikeExpandHoverovano = App.rootPath + "\\slike\\arrowDole-128-Blue.png";

        public string PutanjaDoSlikeExpandHoverovano
        {
            get { return putanjaDoSlikeExpandHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeExpandHoverovano, value); }
        }


        private string putanjaDoSlikeCollapse = App.rootPath + "\\slike\\arrowGore-128-White.png";

        public string PutanjaDoSlikeCollapse
        {
            get { return putanjaDoSlikeCollapse; }
            set { SetAndNotify(ref putanjaDoSlikeCollapse, value); }
        }

        private string putanjaDoSlikeCollapseHoverovano = App.rootPath + "\\slike\\arrowGore-128-Blue.png";

        public string PutanjaDoSlikeCollapseHoverovano
        {
            get { return putanjaDoSlikeCollapseHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeCollapseHoverovano, value); }
        }

        private string putanjaDoSlikeDodajUKorpu = App.rootPath + "\\slike\\dodajUKorpu-128-Green.png";

        public string PutanjaDoSlikeDodajUKorpu
        {
            get { return putanjaDoSlikeDodajUKorpu; }
            set { SetAndNotify(ref putanjaDoSlikeDodajUKorpu, value); }
        }

        private string putanjaDoSlikeDodajUKorpuHoverovano = App.rootPath + "\\slike\\dodajUKorpu-128-White.png";

        public string PutanjaDoSlikeDodajUKorpuHoverovano
        {
            get { return putanjaDoSlikeDodajUKorpuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeDodajUKorpuHoverovano, value); }
        }

        private string putanjaDoSlikeDodajUKorpuOnemoguceno = App.rootPath + "\\slike\\dodajUKorpu-128-Gray.png";

        public string PutanjaDoSlikeDodajUKorpuOnemoguceno
        {
            get { return putanjaDoSlikeDodajUKorpuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeDodajUKorpuOnemoguceno, value); }
        }


        private string putanjaDoSlikeDodajRezervaciju = App.rootPath + "\\slike\\dodajRezervaciju-128-Green.png";

        public string PutanjaDoSlikeDodajRezervaciju
        {
            get { return putanjaDoSlikeDodajRezervaciju; }
            set { SetAndNotify(ref putanjaDoSlikeDodajRezervaciju, value); }
        }

        private string putanjaDoSlikeDodajRezervacijuHoverovano = App.rootPath + "\\slike\\dodajRezervaciju-128-White.png";

        public string PutanjaDoSlikeDodajRezervacijuHoverovano
        {
            get { return putanjaDoSlikeDodajRezervacijuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeDodajRezervacijuHoverovano, value); }
        }

        private string putanjaDoSlikeDodajRezervacijuOnemoguceno = App.rootPath + "\\slike\\dodajRezervaciju-128-Gray.png";

        public string PutanjaDoSlikeDodajRezervacijuOnemoguceno
        {
            get { return putanjaDoSlikeDodajRezervacijuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeDodajRezervacijuOnemoguceno, value); }
        }


        private string putanjaDoSlikeIzmeni = App.rootPath + "\\slike\\edit-128-Blue.png";

        public string PutanjaDoSlikeIzmeni
        {
            get { return putanjaDoSlikeIzmeni; }
            set { SetAndNotify(ref putanjaDoSlikeIzmeni, value); }
        }

        private string putanjaDoSlikeIzmeniHoverovano = App.rootPath + "\\slike\\edit-128-White.png";

        public string PutanjaDoSlikeIzmeniHoverovano
        {
            get { return putanjaDoSlikeIzmeniHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeIzmeniHoverovano, value); }
        }


        private string putanjaDoSlikeNaruciNaLager = App.rootPath + "\\slike\\naruciNaLager-128-Blue.png";

        public string PutanjaDoSlikeNaruciNaLager
        {
            get { return putanjaDoSlikeNaruciNaLager; }
            set { SetAndNotify(ref putanjaDoSlikeNaruciNaLager, value); }
        }

        private string putanjaDoSlikeNaruciNaLagerHoverovano = App.rootPath + "\\slike\\naruciNaLager-128-White.png";

        public string PutanjaDoSlikeNaruciNaLagerHoverovano
        {
            get { return putanjaDoSlikeNaruciNaLagerHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeNaruciNaLagerHoverovano, value); }
        }

        private string putanjaDoSlikeNaruciNaLagerOnemoguceno = App.rootPath + "\\slike\\naruciNaLager-128-Gray.png";

        public string PutanjaDoSlikeNaruciNaLagerOnemoguceno
        {
            get { return putanjaDoSlikeNaruciNaLagerOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeNaruciNaLagerOnemoguceno, value); }
        }

        private string putanjaDoSlikeSkiniSaLagera = App.rootPath + "\\slike\\skiniSaLagera-128-Red.png";

        public string PutanjaDoSlikeSkiniSaLagera
        {
            get { return putanjaDoSlikeSkiniSaLagera; }
            set { SetAndNotify(ref putanjaDoSlikeSkiniSaLagera, value); }
        }

        private string putanjaDoSlikeSkiniSaLageraHoverovano = App.rootPath + "\\slike\\skiniSaLagera-128-White.png";

        public string PutanjaDoSlikeSkiniSaLageraHoverovano
        {
            get { return putanjaDoSlikeSkiniSaLageraHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeSkiniSaLageraHoverovano, value); }
        }

        private string putanjaDoSlikeSkiniSaLageraOnemoguceno = App.rootPath + "\\slike\\skiniSaLagera-128-Gray.png";

        public string PutanjaDoSlikeSkiniSaLageraOnemoguceno
        {
            get { return putanjaDoSlikeSkiniSaLageraOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeSkiniSaLageraOnemoguceno, value); }
        }

        private string putanjaDoSlikeAdminPanel = App.rootPath + "\\slike\\administrator-128-White.png";

        public string PutanjaDoSlikeAdminPanel
        {
            get { return putanjaDoSlikeAdminPanel; }
            set { SetAndNotify(ref putanjaDoSlikeAdminPanel, value); }
        }

        private string putanjaDoSlikeAdminPanelHoverovano = App.rootPath + "\\slike\\administrator-128-Blue.png";

        public string PutanjaDoSlikeAdminPanelHoverovano
        {
            get { return putanjaDoSlikeAdminPanelHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeAdminPanelHoverovano, value); }
        }

        private string putanjaDoSlikeRezervacije = App.rootPath + "\\slike\\rezervacije-128-White.png";

        public string PutanjaDoSlikeRezervacije
        {
            get { return putanjaDoSlikeRezervacije; }
            set { SetAndNotify(ref putanjaDoSlikeRezervacije, value); }
        }

        private string putanjaDoSlikeRezervacijeHoverovano = App.rootPath + "\\slike\\rezervacije-128-Blue.png";

        public string PutanjaDoSlikeRezervacijeHoverovano
        {
            get { return putanjaDoSlikeRezervacijeHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeRezervacijeHoverovano, value); }
        }

        private string putanjaDoSlikeKorpa = App.rootPath + "\\slike\\korpa-128-White.png";

        public string PutanjaDoSlikeKorpa
        {
            get { return putanjaDoSlikeKorpa; }
            set { SetAndNotify(ref putanjaDoSlikeKorpa, value); }
        }

        private string putanjaDoSlikeKorpaHoverovano = App.rootPath + "\\slike\\korpa-128-Blue.png";

        public string PutanjaDoSlikeKorpaHoverovano
        {
            get { return putanjaDoSlikeKorpaHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeKorpaHoverovano, value); }
        }

        private string putanjaDoSlikeOsvezi = App.rootPath + "\\slike\\refresh-128-White.png";

        public string PutanjaDoSlikeOsvezi
        {
            get { return putanjaDoSlikeOsvezi; }
            set { SetAndNotify(ref putanjaDoSlikeOsvezi, value); }
        }

        private string putanjaDoSlikeOsveziHoverovano = App.rootPath + "\\slike\\refresh-128-Blue.png";

        public string PutanjaDoSlikeOsveziHoverovano
        {
            get { return putanjaDoSlikeOsveziHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeOsveziHoverovano, value); }
        }
        private string putanjaDoSlikeIzmeniSliku = App.rootPath + "\\slike\\edit-image-128-Blue.png";

        public string PutanjaDoSlikeIzmeniSliku
        {
            get { return putanjaDoSlikeIzmeniSliku; }
            set { SetAndNotify(ref putanjaDoSlikeIzmeniSliku, value); }
        }

        private string putanjaDoSlikeIzmeniSlikuHoverovano = App.rootPath + "\\slike\\edit-image-128-White.png";

        public string PutanjaDoSlikeIzmeniSlikuHoverovano
        {
            get { return putanjaDoSlikeIzmeniSlikuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeIzmeniSlikuHoverovano, value); }
        }

        private string putanjaDoSlikeSacuvajIzmene = App.rootPath + "\\slike\\save-128-Green.png";

        public string PutanjaDoSlikeSacuvajIzmene
        {
            get { return putanjaDoSlikeSacuvajIzmene; }
            set { SetAndNotify(ref putanjaDoSlikeSacuvajIzmene, value); }
        }

        private string putanjaDoSlikeSacuvajIzmeneHoverovano = App.rootPath + "\\slike\\save-128-White.png";

        public string PutanjaDoSlikeSacuvajIzmeneHoverovano
        {
            get { return putanjaDoSlikeSacuvajIzmeneHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeSacuvajIzmeneHoverovano, value); }
        }


        private string putanjaDoSlikeDelete = App.rootPath + "\\slike\\delete-128-Red.png";
        private string putanjaDoSlikeDeleteHoverovano = App.rootPath + "\\slike\\delete-128-White.png";


        private string putanjaDoSlikeKupi = App.rootPath + "\\slike\\checkout-128-Green.png";
        private string putanjaDoSlikeKupiHoverovano = App.rootPath + "\\slike\\checkout-128-White.png";
        private string putanjaDoSlikeKupiOnemoguceno = App.rootPath + "\\slike\\checkout-128-Gray.png";

        public string PutanjaDoSlikeKupiOnemoguceno
        {
            get { return putanjaDoSlikeKupiOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeKupiOnemoguceno, value); }
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

        private string putanjaDoSlikeIsprazniKorpu = App.rootPath + "\\slike\\return-128-Red.png";
        private string putanjaDoSlikeIsprazniKorpuHoverovano = App.rootPath + "\\slike\\return-128-White.png";
        private string putanjaDoSlikeIsprazniKorpuOnemoguceno = App.rootPath + "\\slike\\return-128-Gray.png";



        public string PutanjaDoSlikeIsprazniKorpuOnemoguceno
        {
            get { return putanjaDoSlikeIsprazniKorpuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeIsprazniKorpuOnemoguceno, value); }
        }




        public string PutanjaDoSlikeIsprazniKorpu
        {
            get { return putanjaDoSlikeIsprazniKorpu; }
            set { SetAndNotify(ref putanjaDoSlikeIsprazniKorpu, value); }
        }


        public string PutanjaDoSlikeIsprazniKorpuHoverovano
        {
            get { return putanjaDoSlikeIsprazniKorpuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeIsprazniKorpuHoverovano, value); }
        }




        public string PutanjaDoSlikeDeleteHoverovano
        {
            get { return putanjaDoSlikeDeleteHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeDeleteHoverovano, value); }
        }


        


        public string PutanjaDoSlikeDelete
        {
            get { return putanjaDoSlikeDelete; }
            set { SetAndNotify(ref putanjaDoSlikeDelete, value); }
        }


        

        private string putanjaDoSlikeDugmeSearch = App.rootPath + "\\slike\\Search-Blue.png";

        public string PutanjaDoSlikeDugmeSearch
        {
            get { return putanjaDoSlikeDugmeSearch; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeSearch, value); }
        }

        private string putanjaDoSlikeDugmeSearchBelo = App.rootPath + "\\slike\\Search-Blue.png";

        public string PutanjaDoSlikeDugmeSearchBelo
        {
            get { return putanjaDoSlikeDugmeSearchBelo; }
            set { SetAndNotify(ref putanjaDoSlikeDugmeSearchBelo, value); }
        }

        private string putanjaDoSlikeInfo = App.rootPath + "\\slike\\info-128-Blue.png";
        public string PutanjaDoSlikeInfo
        {
            get { return putanjaDoSlikeInfo; }
            set { SetAndNotify(ref putanjaDoSlikeInfo, value); }
        }


        

        private string putanjaDoSlikeAlert = App.rootPath + "\\slike\\alert-128.png";
        public string PutanjaDoSlikeAlert
        {
            get { return putanjaDoSlikeAlert; }
            set { SetAndNotify(ref putanjaDoSlikeAlert, value); }
        }

        private string putanjaDoSlikeInfoHoverovano = App.rootPath + "\\slike\\info-128-White.png";
        public string PutanjaDoSlikeInfoHoverovano
        {
            get { return putanjaDoSlikeInfoHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeInfoHoverovano, value); }
        }

        private string putanjaDoSlikeSakrijHederNaDole = App.rootPath + "\\slike\\SakrijHederNaDole-128.png";
        public string PutanjaDoSlikeSakrijHederNaDole
        {
            get { return putanjaDoSlikeSakrijHederNaDole; }
            set { SetAndNotify(ref putanjaDoSlikeSakrijHederNaDole, value); }
        }

        private string putanjaDoSlikeSakrijHederNaGore = App.rootPath + "\\slike\\SakrijHederNaGore-128.png";
        public string PutanjaDoSlikeSakrijHederNaGore
        {
            get { return putanjaDoSlikeSakrijHederNaGore; }
            set { SetAndNotify(ref putanjaDoSlikeSakrijHederNaGore, value); }
        }

        private string putanjaDoSlikePrihvatiNarudzbinu = App.rootPath + "\\slike\\prihvatiNarudzbinu-128-Green.png";
        public string PutanjaDoSlikePrihvatiNarudzbinu
        {
            get { return putanjaDoSlikePrihvatiNarudzbinu; }
            set { SetAndNotify(ref putanjaDoSlikePrihvatiNarudzbinu, value); }
        }
        private string putanjaDoSlikePrihvatiNarudzbinuHoverovano = App.rootPath + "\\slike\\prihvatiNarudzbinu-128-White.png";
        public string PutanjaDoSlikePrihvatiNarudzbinuHoverovano
        {
            get { return putanjaDoSlikePrihvatiNarudzbinuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikePrihvatiNarudzbinuHoverovano, value); }
        }
        private string putanjaDoSlikePrihvatiNarudzbinuOnemoguceno = App.rootPath + "\\slike\\prihvatiNarudzbinu-128-Gray.png";
        public string PutanjaDoSlikePrihvatiNarudzbinuOnemoguceno
        {
            get { return putanjaDoSlikePrihvatiNarudzbinuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikePrihvatiNarudzbinuOnemoguceno, value); }
        }

        private string putanjaDoSlikePrebaciRezervacijuUKorpu = App.rootPath + "\\slike\\cart-128-Green.png";
        public string PutanjaDoSlikePrebaciRezervacijuUKorpu
        {
            get { return putanjaDoSlikePrebaciRezervacijuUKorpu; }
            set { SetAndNotify(ref putanjaDoSlikePrebaciRezervacijuUKorpu, value); }
        }

        private string putanjaDoSlikePrebaciRezervacijuUKorpuOnemoguceno = App.rootPath + "\\slike\\cart-128-Gray.png";
        public string PutanjaDoSlikePrebaciRezervacijuUKorpuOnemoguceno
        {
            get { return putanjaDoSlikePrebaciRezervacijuUKorpuOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikePrebaciRezervacijuUKorpuOnemoguceno, value); }
        }

        private string putanjaDoSlikePrebaciRezervacijuUKorpuHoverovano = App.rootPath + "\\slike\\cart-128-White.png";
        public string PutanjaDoSlikePrebaciRezervacijuUKorpuHoverovano
        {
            get { return putanjaDoSlikePrebaciRezervacijuUKorpuHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikePrebaciRezervacijuUKorpuHoverovano, value); }
        }


        private string putanjaDoSlikeCoin = App.rootPath + "\\slike\\coins-128-Green.png";
        private string putanjaDoSlikeCoinHoverovano = App.rootPath + "\\slike\\coins-128-White.png";
        private string putanjaDoSlikeCoinOnemoguceno = App.rootPath + "\\slike\\coins-128-Gray.png";

        public string PutanjaDoSlikeCoinOnemoguceno
        {
            get { return putanjaDoSlikeCoinOnemoguceno; }
            set { SetAndNotify(ref putanjaDoSlikeCoinOnemoguceno, value); }
        }


        private string putanjaDoSlikeOdabirKupca = App.rootPath + "\\slike\\odabirKupca-128-White.png";

        public string PutanjaDoSlikeOdabirKupca
        {
            get { return putanjaDoSlikeOdabirKupca; }
            set { SetAndNotify(ref putanjaDoSlikeOdabirKupca, value); }
        }







        public string PutanjaDoSlikeCoin
        {
            get { return putanjaDoSlikeCoin; }
            set { SetAndNotify(ref putanjaDoSlikeCoin, value); }
        }

        public string PutanjaDoSlikeCoinHoverovano
        {
            get { return putanjaDoSlikeCoinHoverovano; }
            set { SetAndNotify(ref putanjaDoSlikeCoinHoverovano, value); }
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
