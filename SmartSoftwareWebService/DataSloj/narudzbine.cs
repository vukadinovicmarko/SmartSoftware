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
    
    public partial class narudzbine
    {
        public int id_narudzbine { get; set; }
        public int kolicina { get; set; }
        public int id_oprema { get; set; }
        public int id_prodavca { get; set; }
        public System.DateTime datum_narudzbine { get; set; }
    
        public virtual oprema oprema { get; set; }
    }
}
