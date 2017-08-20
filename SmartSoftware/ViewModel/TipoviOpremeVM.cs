using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public class TipoviOpremeVM : SmartSoftwareGlavnaOblastVM
    {

        //private ObservableCollection<Oprema> opremaKolekcija = new ObservableCollection<Oprema>();

        //public ObservableCollection<Oprema> OpremaKolekcija
        //{
        //    get { return opremaKolekcija; }
        //}

        public TipoviOpremeVM(TipoviOpreme tipoviOpreme)
            : base (tipoviOpreme)
        {

        }
    }
}
