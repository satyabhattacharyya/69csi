//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _69CSI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_CSI_69_Con_Transections
    {
        public long TransId { get; set; }
        public long UserId { get; set; }
        public decimal AmountToPay { get; set; }
        public decimal AmountPaid { get; set; }
        public System.DateTime TransDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Extra1 { get; set; }
        public Nullable<System.DateTime> Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }
        public string Extra5 { get; set; }
        public Nullable<long> Extra6 { get; set; }
        public Nullable<System.DateTime> Extra7 { get; set; }
        public string Extra8 { get; set; }
        public string Extra9 { get; set; }
        public string Extra10 { get; set; }
    
        public virtual tbl_CSI_69_Con_RegistrationDetails tbl_CSI_69_Con_RegistrationDetails { get; set; }
    }
}