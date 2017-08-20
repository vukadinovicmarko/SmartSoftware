using SmartSoftware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.ViewModel
{
    public static class ViewModelUtility
    {

        public static SmartSoftwareGlavnaOblastVM viewModelFactory(SmartSoftwareGlavnaOblast smartSoftwareGlavnaOblast)
        {
            if (smartSoftwareGlavnaOblast is OblastiOpreme)
                return new OblastiOpremeVM(smartSoftwareGlavnaOblast as OblastiOpreme);
            if (smartSoftwareGlavnaOblast is TipoviOpreme)
                return new TipoviOpremeVM(smartSoftwareGlavnaOblast as TipoviOpreme);
            if (smartSoftwareGlavnaOblast is Oprema)
                return new OpremaVM(smartSoftwareGlavnaOblast as Oprema);
            return null;
        }


    }
}
