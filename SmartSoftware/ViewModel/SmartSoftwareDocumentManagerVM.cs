using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartSoftware.ViewModel
{
    public class SmartSoftwareDocumentManagerVM : INotifyPropertyChanged
    {

        private List<Oprema> lista = new List<Oprema>();

        public List<Oprema> Lista
        {
            get { return lista; }
            set { SetAndNotify(ref lista, value); }
        }


        private bool daLiJeTekstUViseRedova = false;

        public bool DaLiJeTekstUViseRedova
        {
            get { return daLiJeTekstUViseRedova; }
            set { SetAndNotify(ref daLiJeTekstUViseRedova, value); }
        }

        private GridLength posebnaKolona;

        public GridLength PosebnaKolona
        {
            get { return posebnaKolona; }
            set { SetAndNotify(ref posebnaKolona, value); }
        }

        

        private List<NovaListaZaListuSvihLista> listaSvihLista = new List<NovaListaZaListuSvihLista>();

        public List<NovaListaZaListuSvihLista> ListaSvihLista
        {
            get { return listaSvihLista; }
            set { SetAndNotify(ref listaSvihLista, value); }
        }

        private string tekstPretrage;

        public string TekstPretrage
        {
            get { return tekstPretrage; }
            set { SetAndNotify(ref tekstPretrage, value); }
        }

        

        private SmartSoftwareDocumentVM currentDocumentVM;

        public SmartSoftwareDocumentVM CurrentDocumentVM
        {
            get { return currentDocumentVM; }
            set { SetAndNotify(ref currentDocumentVM, value); }
        }

        private Oprema trenutnaOpremaZaMenjanje;

        public Oprema TrenutnaOpremaZaMenjanje
        {
            get { return trenutnaOpremaZaMenjanje; }
            set { SetAndNotify(ref trenutnaOpremaZaMenjanje, value); }
        }


        private Korisnici trenutniProdavac = new Korisnici();

        public Korisnici TrenutniProdavac
        {
            get { return trenutniProdavac; }
            set { SetAndNotify(ref trenutniProdavac, value); }
        }

       

        public SmartSoftwareDocumentManagerVM()
        {
            //PutanjaDoSlikeExpand = MainWindow.rootPath + "\\slike\\arrowDole-128-Blue.png";
            this.CurrentDocumentVM = new SmartSoftwareDocumentVM(new SmartSoftwareDocument());

             

            
        }




        private ObservableCollection<SmartSoftwareGlavnaOblast> korpa = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Korpa
        {
            get { return korpa; }
            set { SetAndNotify(ref korpa, value); }
        }

        private ObservableCollection<SmartSoftwareGlavnaOblast> rezervacije = new ObservableCollection<SmartSoftwareGlavnaOblast>();

        public ObservableCollection<SmartSoftwareGlavnaOblast> Rezervacije
        {
            get { return rezervacije; }
            set { SetAndNotify(ref rezervacije, value); }
        }

        private int brojOpremeZaRezervisanje = GlavniProzor.listaTrenutnihRezervacija.Count;

        public int BrojOpremeZaRezervisanje
        {
            get { return brojOpremeZaRezervisanje; }
            set { SetAndNotify(ref brojOpremeZaRezervisanje, value); }
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

    public class NovaListaZaListuSvihLista : INotifyPropertyChanged
    {
        private List<Oprema> novaLista = new List<Oprema>();

        public List<Oprema> NovaLista
        {
            get { return novaLista; }
            set { SetAndNotify(ref novaLista, value); }
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
