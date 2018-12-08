using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ManageInputVat
{
    public class SearchViewModel
    {
        [Display(Name = "เลขที่สัญญา")]
        public string ContractCode { get; set; }
        [Display(Name = "เลขบัตรประชาชน")]
        public string CustIDCard { get; set; }
        [Display(Name = "ชื่อลูกค้า")]
        public string CustName { get; set;}
        [Display(Name = "เลขทะเบียนรถ")]
        public string LicenseNo { get; set; }
        [Display(Name = "เลขตัวถังรถ")]
        public string ChassisNo { get; set; }

        public ManageViewModel manageVM { get; set; } = new ManageViewModel();
    }
}