using MiniFinance.Common;
using MiniFinance.Models;
using MiniFinance.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.WriteOffManual
{
    public class MainDataViewModel
    {
        [Display(Name = "เลขที่สัญญา")]
        public string ContractCode { get; set; }
        public string LoanTypeCode { get; set; }
        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanType { get; set; }
        [Display(Name = "ผลิตภัณฑ์สินเชื่อ")]
        public string LoanProductType { get; set; }
        [Display(Name = "เลขประจำตัวประชาชน")]
        public string CustomerIDCard { get; set; }
        [Display(Name = "ชื่อลูกค้า")]
        public string CustomerName { get; set; }
        [Display(Name = "ยอดหนี้คงเหลือ")]
        public decimal? OutstandingBalance { get; set; }
       
        [Display(Name = "ค่าเบี้ยปรับคงเหลือ")]
        public decimal? PenaltyAmount { get; set; }
        [Display(Name = "ภาษีมูลค่าเพิ่มคงเหลือ")]
        public decimal? OutstandingVat { get; set; }
        [Display(Name = "รายได้รอตัดคงเหลือ")]
        public decimal? UnrealizedIncome { get; set; }
        [Display(Name = "ค่างวด")]
        public decimal? Installment { get; set; }
        [Display(Name = "ภาษีมูลค่าเพิ่ม/งวด")]
        public decimal? InstallmentVat { get; set; }
        [Display(Name = "ค่างวดคงค้าง")]
        public decimal? OverdueAmount { get; set; }
        [Display(Name = "เจ้าหน้าที่ดูแล")]
        public string FollowUpBy { get; set; }
        [Display(Name = "ค่าทวงถามคงเหลือ")]
        public decimal? LetterFee { get; set; }
        [Display(Name = "ค่าติดตามคงเหลือ")]
        public decimal? FollowUpFee { get; set; }
        [Display(Name = "เงินพักคงเหลือ")]
        public decimal? SuspenseAmount { get; set; }
        [Display(Name = "วันที่รับชำระล่าสุด")]
        public DateTime? LastReceiptDate { get; set; }
        [Display(Name = "ภาษีมูลค่าเพิ่มคงค้าง")]
        public decimal? OverdueVat { get; set; }
        
        [Display(Name = "สถานะสัญญา")]
        public string ContractStatus { get; set; }
        
        [Display(Name = "ยอดตัดหนี้สูญ")]
        public decimal? WriteOffAmount { get; set; }
        public decimal? WriteOffVatAmount { get; set; }
        public decimal? WriteOffExVatAmount { get; set; }
        [Display(Name = "วันที่ทำรายการ")]
        public DateTime? TransactionDate { get; set; }
        [Display(Name = "วันที่ตัดหนี้สูญ")]
        public DateTime? WriteOffDate { get; set; }


        //======================== Loan Field =========================
        [Display(Name = "เงินต้นคงเหลือ")]
        public decimal? PrincipalOutstanding { get; set; }
        [Display(Name = "ดอกเบี้ยคงเหลือ")]
        public decimal? InterestOutstanding { get; set; }

        public List<FieldTableFee> lsFee { get; set; }
        public List<FieldTableFee> lsDeduct { get; set; }

        //==============================================================


        public string Username { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }

    }

    public class FieldTableFee
    {
        public string TransDesc { get; set; }
        public decimal Amount { get; set; }
    }

   
}