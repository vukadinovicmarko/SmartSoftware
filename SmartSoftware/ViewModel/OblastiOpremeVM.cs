using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public class OblastiOpremeVM : SmartSoftwareGlavnaOblastVM
    {
        private ObservableCollectionVM<SmartSoftwareGlavnaOblast> items;
        public ObservableCollectionVM<SmartSoftwareGlavnaOblast> Items
        {
            get { return items; }
        }
        public OblastiOpremeVM(OblastiOpreme group)
            : base(group)
        {
            this.items = new ObservableCollectionVM<SmartSoftwareGlavnaOblast>(group.Items,
                ViewModelUtility.viewModelFactory);
            
        }

        



        

    }
}
