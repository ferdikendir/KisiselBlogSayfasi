//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proje_blog.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class kategoriler
    {
        public kategoriler()
        {
            this.gonderiler = new HashSet<gonderiler>();
        }
    
        public int katid { get; set; }
        public string kategori { get; set; }
    
        public virtual ICollection<gonderiler> gonderiler { get; set; }
    }
}
