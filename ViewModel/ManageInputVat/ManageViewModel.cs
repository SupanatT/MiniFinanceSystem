using MiniFinance.Common;
using MiniFinance.Models;
using MiniFinance.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ManageInputVat
{
    public class ManageViewModel
    {
        public string mode { get; set; }

        public int rowIndex { get; set; }
        public int PayHeaderID { get; set; }
        public string ContractCode { get; set; }
        public string TransCode { get; set; }
        [Display(Name = "ประเภทภาษีซื้อ")]
        public string TransDesc { get; set; }
        [Display(Name = "รายละเอียดสินค้า")]
        public string AssetDescription { get; set; }

        [Display(Name = "วันที่รับเอกสาร")]
        public DateTime? ReceiveDocDate { get; set; }
        [Display(Name = "วันที่บนใบกำกับ")]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "วันที่เคลมภาษี")]
        public DateTime? ClaimDate { get; set; }
        [Display(Name = "เลขที่ใบกำกับ")]
        public string InvoiceNo { get; set; }
        [Display(Name = "ชื่อสินค้า")]
        public string ProductName { get; set; }
        [Display(Name = "จำนวนเงิน")]
        public decimal ExVatAmount { get; set; }
        [Display(Name = "ภาษีมูลค่าเพิ่ม")]
        public decimal VatAmount { get; set; }
        [Display(Name = "รวม")]
        public decimal TotalAmount { get; set; }
        public string PayeeCode { get; set; }
        [Display(Name = "ชื่อผู้รับเงิน")]
        public string PayeeName { get; set; }
        
        public string SellerCode { get; set; }
        [Display(Name = "จ่ายผู้ขายสินค้า")]
        public string SellerName { get; set; }
        [Display(Name = "หมายเหตุ")]
        public string Note { get; set; }
        [Display(Name = "รับใบกำกับภาษี")]
        public bool FlagReceiveInvoice { get; set; }
        [Display(Name = "รับใบเสร็จรับเงิน")]
        public bool FlagReceiveReceipt { get; set; }
        public int? AssetID { get; set; }
        public string AssetType { get; set; }

        public List<ManageViewModel> lsDetail { get; set; } = new List<ManageViewModel>();

        public void AddOrEditRowObj(ref ManageViewModel TargetObj)
        {
            try
            {
                TargetObj.PayHeaderID = PayHeaderID;
                TargetObj.TransCode = TransCode;
                TargetObj.TransDesc = TransDesc;
                TargetObj.AssetDescription = CommonMethods.getAssetDescription(AssetID, AssetType);
                TargetObj.ReceiveDocDate = ReceiveDocDate;
                TargetObj.InvoiceDate = InvoiceDate;
                TargetObj.ClaimDate = ClaimDate;
                TargetObj.InvoiceNo = InvoiceNo;
                TargetObj.ExVatAmount = ExVatAmount;
                TargetObj.VatAmount = VatAmount;
                TargetObj.TotalAmount = TotalAmount;
                TargetObj.FlagReceiveInvoice = FlagReceiveInvoice;
                TargetObj.FlagReceiveReceipt = FlagReceiveReceipt;

                TargetObj.PayeeCode = PayeeCode;
                TargetObj.PayeeName = CommonMethods.GetCustomerFullName(PayeeCode);
                TargetObj.SellerCode = SellerCode;
                TargetObj.SellerName = CommonMethods.GetCustomerFullName(SellerCode);
                TargetObj.AssetID = AssetID;
                TargetObj.AssetType = AssetType;
            }
            catch(Exception)
            {
                throw;
            }
        }

       

        public void ClearModel()
        {
            TransCode = Constants.TransCode.ProductHirePurchase;
            TransDesc = Constants.InputVatType.Product;
            ReceiveDocDate = null;
            InvoiceDate = null;
            ClaimDate = null;
            InvoiceNo = null;
            ExVatAmount = 0.00M;
            VatAmount = 0.00M;
            TotalAmount = 0.00M;
            FlagReceiveInvoice = false;
            FlagReceiveReceipt = false;
            PayeeCode = null;
            PayeeName = null;
            SellerCode = null;
            SellerName = null;
            AssetID = null;
            AssetType = null;
        }

        public void setupAssetDescription()
        {
            foreach (var item in lsDetail) //ในAssetDescription <br> แทรกในajaxไม่ได้ จึงไม่ได้ยัดhiddenส่งมา
            {
                item.AssetDescription = CommonMethods.getAssetDescription(item.AssetID, item.AssetType);
            }
        }

    }

}