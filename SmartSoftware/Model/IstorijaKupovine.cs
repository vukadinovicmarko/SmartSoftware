using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.Model
{
    public class IstorijaKupovine : INotifyPropertyChanged
    {

        private int idIstorijaKupovine;

        public int IdIstorijaKupovine
        {
            get { return idIstorijaKupovine; }
            set { SetAndNotify(ref idIstorijaKupovine, value); }
        }

        private System.DateTime datum_prodaje;

        public System.DateTime Datum_prodaje
        {
            get { return datum_prodaje; }
            set { SetAndNotify(ref datum_prodaje, value); }
        }

        private Korisnici prodavac;

        public Korisnici Prodavac
        {
            get { return prodavac; }
            set { SetAndNotify(ref prodavac, value); }
        }

        private Korisnici kupac;

        public Korisnici Kupac
        {
            get { return kupac; }
            set { SetAndNotify(ref kupac, value); }
        }

        private bool kliknutoNaGrid = false;

        public bool KliknutoNaGrid
        {
            get { return kliknutoNaGrid; }
            set { SetAndNotify(ref kliknutoNaGrid, value); }
        }


        private double ukupna_cena_kupovine;

        public double Ukupna_cena_kupovine
        {
            get { return ukupna_cena_kupovine; }
            set { SetAndNotify(ref ukupna_cena_kupovine, value); }
        }

        private double? broj_iskoriscenih_popust_poena;

        public double? Broj_iskoriscenih_popust_poena
        {
            get { return broj_iskoriscenih_popust_poena; }
            set { SetAndNotify(ref broj_iskoriscenih_popust_poena, value); }
        }

        private ObservableCollection<KupljenaOprema> listaKupljeneOpreme = new ObservableCollection<KupljenaOprema>();

        public ObservableCollection<KupljenaOprema> ListaKupljeneOpreme
        {
            get { return listaKupljeneOpreme; }
            set { SetAndNotify(ref listaKupljeneOpreme, value); }
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
