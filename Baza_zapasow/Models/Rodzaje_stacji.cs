//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Baza_zapasow.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rodzaje_stacji
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rodzaje_stacji()
        {
            this.Stacje = new HashSet<Stacje>();
        }
    
        public int Rodzaj_Id { get; set; }
        public string Rodzaj { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stacje> Stacje { get; set; }
    }
}
