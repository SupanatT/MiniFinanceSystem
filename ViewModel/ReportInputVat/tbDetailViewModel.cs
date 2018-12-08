using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReportInputVat
{
    public class tbDetailViewModel
    {
        public DateTime? ClaimDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ContractCode { get; set; }
        public string SellerName { get; set; }
        public decimal AmountExVat { get; set; }
        public decimal VatAmount { get; set; }
    }
}