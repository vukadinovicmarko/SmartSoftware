//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartSoftwareWebService.DataSloj
{
    using System;
    using System.Collections.Generic;
    
    public partial class istorija_kupovine
    {
        public int id_istorija_kupovine { get; set; }
        public System.DateTime datum_prodaje { get; set; }
        public int id_prodavca { get; set; }
        public Nullable<int> id_kupca { get; set; }
        public double ukupna_cena_kupovine { get; set; }
        public Nullable<double> broj_iskoriscenih_popust_poena { get; set; }
    }
}
