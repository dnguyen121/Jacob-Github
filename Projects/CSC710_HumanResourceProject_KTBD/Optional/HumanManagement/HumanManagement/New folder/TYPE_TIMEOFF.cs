//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HumanManagement.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class TYPE_TIMEOFF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE_TIMEOFF()
        {
            this.TIMEOFFs = new HashSet<TIMEOFF>();
        }
    
        public int type_timeoff_id { get; set; }
        public string type_timeoff_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIMEOFF> TIMEOFFs { get; set; }
    }
}
