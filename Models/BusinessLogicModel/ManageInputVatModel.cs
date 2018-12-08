using MiniFinance.Common;
using MiniFinance.Models.DataModel;
using MiniFinance.ViewModel.ManageInputVat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.Models.BusinessLogicModel
{
    public class ManageInputVatModel : BaseBLLModel
    {
        public void GetOldData(SearchViewModel viewModel)
        {
            try
            {
                var obj = (from a in _dbContext.T_CONT_PAYMENT_HEADER
                           join b in _dbContext.T_CONT_PAYMENT_DETAIL on a.HEADER_ID equals b.HEADER_ID
                           where a.ACTIVE == true && a.CONT_CODE == viewModel.ContractCode
                           && b.TRANS_TYPE == Constants.PaymentTranType.Payment
                           && (b.TRANS_SUB_TYPE == Constants.PaymentTranSubType.Commission || b.TRANS_SUB_TYPE == Constants.PaymentTranSubType.ProductCost)
                           select new { a, b }
                           ).ToList();

                var CarObj = obj.FirstOrDefault(x => x.a.ASSET_TYPE_ID == Constants.AssetType.Vehicle);
                if (CarObj != null)
                {
                    var itemCar = _dbContext.VIEW_FRONTEND_M_ASSET_VEHICLE.FirstOrDefault(x => x.ASSET_ID == CarObj.a.ASSET_ID);
                    viewModel.LicenseNo = itemCar?.LICENSE_NO;
                    viewModel.ChassisNo = itemCar?.CHASSIS_NO;
                }

                if (obj.Count > 0)
                {
                    int index = 0;
                    bool haveIndex = false;
                    if (obj.Where(x => x.a.ASSET_TYPE_ID == Constants.AssetType.Other).Count() >= 2)
                    {
                        haveIndex = true;
                    }

                    foreach (var det in obj)
                    {
                        string transCode = det.b.TRANS_CODE;
                        var PaymentSys = (from sPay in _dbContext.VIEW_PAYMENT_S_CONT_PAYMENT_HEADER
                                          join tPay in _dbContext.VIEW_PAYMENT_T_CONT_PAYMENT_HEADER on sPay.HEADER_ID equals tPay.S_HEADER_ID
                                          where sPay.CONT_CODE == det.a.CONT_CODE && sPay.PAYMENT_CODE == det.a.PAYMENT_CODE
                                          select new { sPay, tPay }
                                          ).FirstOrDefault(); //join S_CONT_PAY ด้วยเพราะกลัวpaymentcode จากT_CONT_PAY มีโอกาสเปลี่ยน จึงยืมมาเพื่อwhere

                        string payeeCode = PaymentSys?.tPay?.PAYEE_CODE; //ผู้รับเงิน มีสิทธิ์ที่จะโดนเปลี่ยนได้ในระบบpayment จึงต้องดึงค่าจากระบบpaymentไม่ใช่front

                        string sellerCode = PaymentSys?.sPay.PAYEE_CODE; //ผู้ขาย ปกติจะลงในPAYEECODEของfrontend อยู่แล้ว

                        if (det.a.ASSET_TYPE_ID == Constants.AssetType.Other && haveIndex)
                        {
                            index++;
                        }

                        viewModel.manageVM.lsDetail.Add(new ManageViewModel
                        {
                            PayHeaderID = det.a.HEADER_ID,
                            TransCode = transCode,
                            TransDesc = (transCode == Constants.TransCode.ProductHirePurchase) ? CommonMethods.GetFormatTextProductTrans(det.a.ASSET_TYPE_ID, haveIndex, index.ToString()) : CommonMethods.GetTransCodeDescription(transCode),
                            AssetDescription = CommonMethods.getAssetDescription(det.a.ASSET_ID, det.a.ASSET_TYPE_ID),
                            ExVatAmount = det.b.TRANS_AMOUNT_EXVAT.GetValueOrDefault(),
                            VatAmount = det.b.TRANS_VAT_AMOUNT.GetValueOrDefault(),
                            TotalAmount = det.b.TRANS_AMOUNT_INVAT.GetValueOrDefault(),

                            PayeeCode = payeeCode,
                            PayeeName = CommonMethods.GetCustomerFullName(payeeCode),
                            SellerCode = sellerCode,
                            SellerName = CommonMethods.GetCustomerFullName(sellerCode),
                            AssetID = det.a.ASSET_ID,
                            AssetType = det.a.ASSET_TYPE_ID
                        });
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void GetNewData(SearchViewModel viewModel)
        {
            try
            {
                //get จาก table T_INPUT_VAT แทน
                var obj = _dbContext.T_INPUT_VAT.Where(x => x.CONT_CODE == viewModel.ContractCode && x.ACTIVE == true);

                var CarObj = obj.FirstOrDefault(x => x.ASSET_TYPE == Constants.AssetType.Vehicle);
                if (CarObj != null)
                {
                    var itemCar = _dbContext.VIEW_FRONTEND_M_ASSET_VEHICLE.FirstOrDefault(x => x.ASSET_ID == CarObj.ASSET_ID);
                    viewModel.LicenseNo = itemCar?.LICENSE_NO;
                    viewModel.ChassisNo = itemCar?.CHASSIS_NO;
                }

                if (obj.Count() > 0)
                {
                    int index = 0;
                    bool haveIndex = false;
                    if (obj.Where(x => x.ASSET_TYPE == Constants.AssetType.Other).Count() >= 2)
                    {
                        haveIndex = true;
                    }

                    foreach (var det in obj)
                    {
                        string transCode = det.TRANS_CODE;
                        var PaymentSys = (from sPay in _dbContext.VIEW_PAYMENT_S_CONT_PAYMENT_HEADER
                                          join tPay in _dbContext.VIEW_PAYMENT_T_CONT_PAYMENT_HEADER on sPay.HEADER_ID equals tPay.S_HEADER_ID
                                          where sPay.CONT_CODE == det.CONT_CODE && sPay.PAYMENT_CODE == det.T_CONT_PAYMENT_HEADER.PAYMENT_CODE
                                          select new { tPay }
                                          ).FirstOrDefault(); //join S_CONT_PAY ด้วยเพราะกลัวpaymentcode จากT_CONT_PAY มีโอกาสเปลี่ยน จึงยืมมาเพื่อwhere

                        string payeeCode = PaymentSys?.tPay?.PAYEE_CODE; //ผู้รับเงิน มีสิทธิ์ที่จะโดนเปลี่ยนได้ในระบบpayment จึงต้องดึงค่าจากระบบpaymentไม่ใช่front

                        string sellerCode = det.PAYEE_CODE; //ผู้ขาย ปกติจะลงในPAYEECODEของfrontend อยู่แล้ว

                        if (det.ASSET_TYPE == Constants.AssetType.Other && haveIndex)
                        {
                            index++;
                        }

                        viewModel.manageVM.lsDetail.Add(new ManageViewModel
                        {
                            PayHeaderID = det.PAY_HEADER_ID,
                            TransCode = transCode,
                            TransDesc = (transCode == Constants.TransCode.ProductHirePurchase) ? CommonMethods.GetFormatTextProductTrans(det.ASSET_TYPE, haveIndex, index.ToString()) : CommonMethods.GetTransCodeDescription(transCode),
                            AssetDescription = CommonMethods.getAssetDescription(det.ASSET_ID, det.ASSET_TYPE),
                            ReceiveDocDate = det.DOC_RECEIVE_DATE,
                            InvoiceDate = det.INVOICE_DATE,
                            ClaimDate = det.CLAIM_TAX_DATE,
                            InvoiceNo = det.INVOICE_NO,
                            ExVatAmount = det.EX_VAT_AMOUNT.GetValueOrDefault(),
                            VatAmount = det.VAT_AMOUNT.GetValueOrDefault(),
                            TotalAmount = det.TOTAL_AMOUNT,
                            FlagReceiveInvoice = det.FLAG_RECEIVE_INVOICE ?? false,
                            FlagReceiveReceipt = det.FLAG_RECEIVE_RECEIPT ?? false,
                            PayeeCode = payeeCode,
                            PayeeName = CommonMethods.GetCustomerFullName(payeeCode),
                            SellerCode = sellerCode,
                            SellerName = CommonMethods.GetCustomerFullName(sellerCode),
                            AssetID = det.ASSET_ID,
                            AssetType = det.ASSET_TYPE

                        });
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void Save(ACCOUNTINGEntities db, string ContractCode, ManageViewModel obj, LoginModel.Login login)
        {
            try
            {
                var removeObj = db.T_INPUT_VAT.Where(x => x.CONT_CODE == ContractCode);
                if (removeObj.Count() > 0)
                {
                    db.T_INPUT_VAT.RemoveRange(removeObj);
                }


                db.T_INPUT_VAT.Add(new T_INPUT_VAT
                {
                    PAY_HEADER_ID = obj.PayHeaderID,
                    CONT_CODE = ContractCode,
                    TRANS_CODE = obj.TransCode,
                    INVOICE_NO = obj.InvoiceNo,
                    DOC_RECEIVE_DATE = obj.ReceiveDocDate,
                    INVOICE_DATE = obj.InvoiceDate,
                    CLAIM_TAX_DATE = obj.ClaimDate,
                    EX_VAT_AMOUNT = obj.ExVatAmount,
                    VAT_AMOUNT = obj.VatAmount,
                    TOTAL_AMOUNT = obj.TotalAmount,
                    FLAG_RECEIVE_INVOICE = obj.FlagReceiveInvoice,
                    FLAG_RECEIVE_RECEIPT = obj.FlagReceiveReceipt,
                    PAYEE_CODE = obj.PayeeCode,
                    PAYEE_NAME = obj.PayeeName,
                    SELLER_CUST_CODE = obj.SellerCode,
                    SELLER_NAME = obj.SellerName,
                    ASSET_ID = obj.AssetID,
                    ASSET_TYPE = obj.AssetType,
                    ACTIVE = true,
                    CREATE_BY = login.username,
                    CREATE_DATE = DateTimeNow,
                    COMP_CODE = login.compCode,
                    BRANCH_CODE = login.branchCode
                });

                var updateFlag = db.T_CONTRACT.FirstOrDefault(x => x.CONT_CODE == ContractCode);
                if (updateFlag != null)
                {
                    updateFlag.INPUT_VAT_FLAG = true;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}