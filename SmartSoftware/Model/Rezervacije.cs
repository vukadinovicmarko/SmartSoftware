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
    public class Rezervacije : INotifyPropertyChanged
    {
         
        private string ime;
        
        public string Ime
        {
            get { return ime; }
            set { SetAndNotify(ref ime , value); }
        }


        private int idRerezervacije;

        public int IdRerezervacije
        {
            get { return idRerezervacije; }
            set {  SetAndNotify(ref idRerezervacije , value); }
        }

        private string brojTelefona;

        public string BrojTelefona
        {
            get { return brojTelefona; }
            set { SetAndNotify(ref brojTelefona, value); }
        }


        private bool kliknutoNaGrid = false;

        public bool KliknutoNaGrid
        {
            get { return kliknutoNaGrid; }
            set { SetAndNotify(ref kliknutoNaGrid, value); }
        }


        private DateTime? datumRezervacije;

        public DateTime? DatumRezervacije
        {
              get { return datumRezervacije; }
              set { datumRezervacije = value; }
        }

        private DateTime? datumIstekaRezervacije;

        public DateTime? DatumIstekaRezervacije
        {
            get { return datumIstekaRezervacije; }
            set { datumIstekaRezervacije = value; }
        }
        private DateTime? datumAzuriranjaRezervacije;

        public DateTime? DatumAzuriranjaRezervacije
        {
            get { return datumAzuriranjaRezervacije; }
            set { datumAzuriranjaRezervacije = value; }
        }

        private ObservableCollection<Oprema> oprema = new ObservableCollection<Oprema>();

        public ObservableCollection<Oprema> Oprema
        {
            get { return oprema; }
            set { SetAndNotify(ref oprema, value); }
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
