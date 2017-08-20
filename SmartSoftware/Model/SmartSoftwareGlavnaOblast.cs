using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSoftware.Model
{

    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class SmartSoftwareGlavnaOblast : INotifyPropertyChanged
    {

        
        private OblastiOpreme parent;
        
        [DataMember]
        public OblastiOpreme Parent
        {
            get { return parent; }
            set { SetAndNotify(ref parent, value); }
        }

        
        private string name;
        
        [DataMember]
        public string Name
        {
            get { return name; }
            set { SetAndNotify(ref name, value); }
        }

        private string picture;

        [DataMember]
        public string Picture
        {
            get { return picture; }
            set { SetAndNotify(ref picture, value); }
        }


        private bool deletedItem;

        public bool DeletedItem
        {
            get { return deletedItem; }
            set { SetAndNotify(ref deletedItem, value); }
        }


        public SmartSoftwareGlavnaOblast(OblastiOpreme parent)
        {
            this.parent = parent;
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
