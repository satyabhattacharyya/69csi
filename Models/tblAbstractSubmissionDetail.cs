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
    
    public partial class tblAbstractSubmissionDetail
    {
        public long SubId { get; set; }
        public long catid { get; set; }
        public long MemberID { get; set; }
        public string MembershipNO { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string PresentingAuthor { get; set; }
        public string Institution { get; set; }
        public string Name_of_Corresponding_Author { get; set; }
        public string Designation { get; set; }
        public string Address_for_Correspondence { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Foldername { get; set; }
        public string Filename { get; set; }
        public string type { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual life_member life_member { get; set; }
        public virtual tblAbstractCategory tblAbstractCategory { get; set; }
    }
}
