using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public class SmartSoftwareGlavnaOblastVM : IViewModel<SmartSoftwareGlavnaOblast>
    {

        private SmartSoftwareGlavnaOblast smartSoftwareGlavnaOblast;
        public SmartSoftwareGlavnaOblast Model
        {
            get { return smartSoftwareGlavnaOblast; }
        }

        public SmartSoftwareGlavnaOblastVM(SmartSoftwareGlavnaOblast smartSoftwareGlavnaOblast)
        {
            this.smartSoftwareGlavnaOblast = smartSoftwareGlavnaOblast;
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
