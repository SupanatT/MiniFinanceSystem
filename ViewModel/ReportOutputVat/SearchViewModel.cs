using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReportOutputVat
{
    public class SearchViewModel
    {
        [Display(Name = "สาขา")]
        public string BranchCode { get; set; }
        [Display(Name = "วันที่ใบกำกับภาษี จาก")]
        public DateTime? FromInvoiceDate { get; set; }
        [Display(Name = "วันที่ใบกำกับภาษี ถึง")]
        public DateTime? ToInvoiceDate { get; set; }
    }
}