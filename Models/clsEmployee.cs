using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSLDaman.Models
{
    public class clsEmployee
    {
    }

    public class clsEmployeeDetail
    {
        public Int64 SrNo { get; set; }
        public string Units { get; set; }
        public string LineName { get; set; }
        public Int64 Code { get; set; }
        public string EmpName { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }

    public class clsColorMaster
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string vErrorMsg { get; set; }
        public int vErrorCode { get; set; }
        public string vQueryType { get; set; }
    }
}