using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReturnSuspense
{
    public class SearchViewModel
    {
        [Display(Name = "เลขที่สัญญา")]
        public string ContractCode { get; set; }
        [Display(Name = "ชื่อลูกค้า")]
        public string CustomerName { get; set; }
        [Display(Name = "จากวันที่รับ")]
        public DateTime? FromStmDate { get; set; }
        [Display(Name = "ถึงวันที่รับ")]
        public DateTime? ToStmDate { get; set; }
        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanType { get; set; }
        [Display(Name = "จากวันที่ทำรายการ")]
        public DateTime? FromReconcileDate { get; set; }
        [Display(Name = "ถึงวันที่ทำรายการ")]
        public DateTime? ToReconcileDate { get; set; }
        [Display(Name = "ประเภทงานรับชำระ")]
        public string ServiceCode { get; set; }

    }
}