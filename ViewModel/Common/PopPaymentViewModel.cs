using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.Common
{
    public class PopPaymentViewModel
    {
        [Display(Name = "ช่องทางการจ่าย")]
        public string PaymentChannel { get; set; }
        [Display(Name = "วันที่ทำการจ่าย")]
        public DateTime? PaymentDate { get; set; }
        [Display(Name = "ธนาคาร")]
        public string BankCode { get; set; }
        [Display(Name = "สาขา")]
        public string BankBranchCode { get; set; }
        [Display(Name = "เลขที่บัญชี")]
        public string BankAccountNo { get; set; }
        [Display(Name = "ชื่อบัญชี")]
        public string BankAccountName { get; set; }
        public string SystemCode { get; set; }
        public string CompCode { get; set; }
        public string BranchCode { get; set; }
        public string Username { get; set; }
    }
}