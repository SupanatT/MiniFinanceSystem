using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReportInputVat
{
    public class SearchViewModel
    {
        [Display(Name = "จากเลขที่สัญญา")]
        public string FromContractCode { get; set; }
        [Display(Name = "ถึงเลขที่สัญญา")]
        public string ToContractCode { get; set; }
        [Display(Name = "สาขา")]
        public string BranchCode { get; set; }
        [Display(Name = "จาก เดือน/ปี")]
        public DateTime? FromMonthYear { get; set; }
        [Display(Name = "ถึง เดือน/ปี")]
        public DateTime? ToMonthYear { get; set; }
    }
}