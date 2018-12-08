using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReportOutputVat
{
    public class tbDetailViewModel
    {
        public string CompBranch { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ContractCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTaxNo { get; set; }
        public string CustomerBranchCode { get; set; }
        public int? Period { get; set; }
        public string TransCode { get; set; }
        public string TransDesc { get; set; }   
        public decimal AmountExVat { get; set; }
        public decimal VatAmount { get; set; }
    }
}