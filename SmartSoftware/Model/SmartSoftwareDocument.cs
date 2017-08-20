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
    public class SmartSoftwareDocument : INotifyPropertyChanged
    {


        private OblastiOpreme root = new OblastiOpreme(null);

        public OblastiOpreme Root
        {
            get { return root; }
           
        }




        //private ObservableCollection<OblastiOpreme> oblasti = new ObservableCollection<OblastiOpreme>();

        //public ObservableCollection<OblastiOpreme> Oblasti
        //{
        //    get { return oblasti; }
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
    }
}
