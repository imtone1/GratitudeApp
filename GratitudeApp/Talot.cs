//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GratitudeApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Talot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Talot()
        {
            this.Kirjaus = new HashSet<Kirjaus>();
        }
    
        public int talo_id { get; set; }
        public string linkki { get; set; }
        public string kuvaus { get; set; }
        public int tarkeys { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kirjaus> Kirjaus { get; set; }
    }
}
