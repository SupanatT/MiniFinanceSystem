using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.Common
{

    public static class Constants
    {
        public static AppSetting AppSetting = new AppSetting();
        public struct LanguageType
        {
            public const string Thai = "THA";
            public const string English = "EN";
        }


        public struct ProcedureName
        {
            public const string SP_GET_REPORT_DAILY_CASH_RECEIVE = "SP_GET_REPORT_DAILY_CASH_RECEIVE";
        }

        public struct FunctionInDatabase
        {
            public const string GetCustomerName = "GetCustomerName";
        }
        public struct StyleSheets
        {
            public const string colDefault = ".col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 { "
                                            + "position: relative; "
                                            + "min-height: 1px; "
                                            + "padding-left: 15px; "
                                            + "padding-right: 15px; "
                                            + "}";
        }
        public struct CompanyGroup
        {
            public const string LIM = "LIM";
        }
        public struct Mode
        {
            public const string View = "v";
            public const string Edit = "e"; 
            public const string Add = "a";
            public const string Delete = "d";
            public const string Transfer = "tnf";
            public const string Adjust = "adj";
            public const string Approve = "Apv";
        }
        public struct RunningFormat
        {
            public const string OneZero = "0";
            public const string TwoZero = "00";
            public const string ThreeZero = "000";
            public const string FourZero = "0000";
            public const string FiveZero = "00000";
            public const string SixZero = "000000";
        }
        public static class FileType
        {
            public const string Statement = "STM";
        }

        public struct ServiceCode
        {
            public const string Installment0 = "00";
            public const string Installment0Desc = "ค่างวด";
            public const string Installment1 = "01";

            public const string Registration = "02";

            public const string Insurance = "03";

            public const string Collection = "04";

            public const string PaymentDeduct = "DD";

        }
        public struct TransCode
        {
            public const string Installment = "1001";
            public const string Principle = "10000";
            public const string Interest = "10001";
            public const string Fee1 = "10002";
            public const string Fee2 = "10003";
            public const string Fee3 = "10004";
            public const string Fee4 = "10005";
            public const string Fee5 = "10006";
            public const string Fee6 = "10006";
            public const string Fee7 = "10007";
            //public const int InstallmentHP = 10010;
            public const string DiscountForEarlySettlement = "1008";
            public const string DiscountForInstallment = "1009";
            public const string DiscountOther = "1010";
            public const string Penalty = "4001"; //ค่าเบี้ยปรับ
            public const string EarlySettlementFee = "4101";
            public const string ProductHirePurchase = "7002";
        }

        public struct WriteOffStatus
        {
            public const string WritOffFinish = "Y";
            public const string WaitApprove = "W";
        }

        public struct ContractStatus
        {
            public const string Active = "ACT";
            public const string Cancel = "CAN";
            public const string Close = "CLS";
        }
        public struct InstallmentStatus
        {            
            public const string Active = "ACT";
            public const string Cancel = "CAN";
        }
        public struct StatementStatus
        {
            public const string WaitApprove = "WAI";
            public const string Approve = "APO";
            public const string Reject = "RJE";
            public const string Active = "ACT";
            public const string Cancel = "CAN";
            public const string Transfer = "TRA";
        }
        public struct TemplateMode
        {
            public const string Cashier = "CSH";
            public const string Cancel = "CAN";
            public const string TextFile = "TXT";
        }
        public struct TemplateStatus
        {
            public const string Active = "ACT";
            public const string Cancel = "CAN";
            //public const string Text = "TXT";
        }
        public struct PaymentTranType
        {
            public const string Payment = "P";
            public const string Deduct = "D";
        }
        public struct PaymentTranSubType
        {
            public const string Loan = "LOAN";
            public const string ProductCost = "COST";
            public const string Commission = "COM";
        }
        public struct CostStatus
        {
            public const string Complete = "COM";
            public const string Active = "ACT";
            public const string Cancel = "CAN";
        }
        public struct InvoiceStatus
        {
            public const string Active = "ACT";
            public const string Cancel = "CAN";
 public const string Text = "TXT";
        }
        public struct ReceiptStatus
        {
            public const string Active = "ACT";
            public const string Cancel = "CAN";
            public const string Text = "TXT";
        }
        public struct ReconcileStatus
        {
            public const string Active = "ACT";
            public const string Cancel = "CAN";
        }
        public struct PenaltyCountType
        {
            public const string BTEF = "BTEF"; //นับต้นไม่นับท้าย
            public const string BFET = "BFET"; //นับท้ายไม่นับต้น
            public const string BTET = "BTET"; //นับต้น นับท้าย
        }
        public struct PenaltyRateType
        {
            public const string FixedRate = "FR"; //ค่าปรับแบบคงที่
            public const string MinimunRate = "MR"; //ค่าปรับแบบมีขั้นต่ำ
            public const string Normal = "NR"; //ค่าปรับแบบปกติ
        }
        public struct SESSION_KEY
        {
            public const string LoginInfo = "SessionKey.LoginInfo";
            public const string Menu = "SessionKey.Menu";
            public const string SysDate = "SessionKey.SysDate";
            public const string PageSize = "SessionKey.PageSize";
            public const string LastLoginDateTime = "SessionKey.LastLoginDateTime";
            public const string Ip = "SessionKey.Ip";

        }
        public struct SuspenseActionType
        {
            public const string PayReturn = "PAY";
            public const string ToRevenue = "REV";
        }
        public struct Msg
        {
            public const string SessionExp = "Session expired";
        }
        public static class CULTURE
        {
            public const string TH_TH = "th-TH";
            public const string EN_US = "en-US";
        }

        public struct ControllerName
        {
            public const string ManageInputVat = "ManageInputVat";
            public const string WriteOffManual = "WriteOffManual";
        }

        public struct SelectOption
        {
            public static string SelectAll = "-- ทั้งหมด --";
            public static string SelectOne = "-- กรุณาเลือก --";
        }

        public static class SearchCriteria
        {
            public const string ChassisNo_Text = "เลขตัวถัง";
            public const string ChassisNo_Value = "CHN";
            public const string ContractNo_Text = "เลขที่สัญญา";
            public const string ContractNo_Value = "CTN";
            public const string CustomerName_Text = "ชื่อลูกค้า";
            public const string CustomerName_Value = "CUS";
            public const string LicensePlate_Text = "เลขทะเบียนรถยนต์";
            public const string LicensePlate_Value = "LCP";
            public const string BlueBook_Text = "Blue Book";
            public const string BlueBook_Value = "BB";
            public const string SDF_Text = "SDF";
            public const string SDF_Value = "SDF";
            public const string IdCard_Text = "รหัสประจำตัวประชาชน";
            public const string IdCard_Value = "IDC";
            public const string IdTax_Text = "เลขประจำตัวผู้เสียภาษี";
            public const string IdTax_Value = "IDT";
        }

        public struct Status
        {
            public static string Active = "ACT";
            public static string Inactive = "INA";
            public static string System = "SYS";
        }

        public struct FileStatus
        {
            public static string Active = "ACT";
            public static string Inactive = "INA";
        }
        public struct Result
        {
            public const string True = "T";
            public const string False = "F";
            public const string Warring = "W";
        }

        public struct MasterType
        {
            public static string BankCode = "MAS001";
            public static string ReasonType = "ReasonType";
            public static string PaymentChannel = "PaymentChannel";
            public static string BankAccType = "BankAccType";
            public static string ProductType = "LoanProduct";
            public static string LoanType = "LoanType";
            public static string StatementStatus = "StatementStatus";
            public static string ReceiptStatus = "ReceiptStatus";
            public static string CustomerTitle = "CustomerTitle";
            public static string ContractStatus = "ContractStatus";
            public static string ContractSubStatus = "ContractSubStatus";
            public static string AssetType = "AssetType";
        }

        public struct AssetType
        {
            public const string Vehicle = "AT01";
            public const string Other = "AT05";
        }

        public struct urlAction
        {
            public static string ReceiveFileImport = "/CashReceive/Upload";
            public static string HomeLogin = VirtualPathUtility.ToAbsolute("~/Home/Login");
        }
        public static class FilterExtFile
        {
            public const string EXCEL_XLS = ".xls";
            public const string EXCEL_XLSX = ".xlsx";
            public const string EXCEL = ".xls, .xlsx";
            public const string CSV = ".csv";
            public const string TEXT = ".txt";
        }

        public static class DateFormat
        {
            public const string DDMMYYYY = "dd/MM/yyyy";
            public const string ddMMyyyy = "ddMMyyyy";
            public const string yyyyMMddHHmm = "yyyyMMddHHmm";
            //public const string YYYYMMDD = "yyyyMMdd";
            //public const string YYYY_MM_DD = "yyyy-MM-dd";

            //public const string DDMMYYYY_TH_TEXT = "วว/ดด/ปปปป";

            //public const string ddMMyyyy_hh_mm = "dd/MM/yyyy hh:mm";
            //public const string yyyy = "yyyy";
            //public const string MM = "MM";

            //public const string ddMMyyyy_hhmm = "ddMMyyyy_hhmm";
            //public const string ddMMMMyyyy = "dd MMMM yyyy";
            //public const string yyyyMMddHHmmss = "yyyyMMddHHmmss";
            //public const string yyyyMMddHHmmssfff = "yyyyMMddHHmmssfff";
            //public const string DDMMYYYY_NoSlash = "ddMMyyyy";
            //public const string ddMMyyyyHHmmss = "dd-MM-yyyy HH:mm:ss";
            //public const string GenerateDoc = "dd/MM/yyyy HH:mm";
            //public const string dMyyyy_hmmss = "d/M/yyyy h:mm:ss";
            //public const string dMyyyy = "d/M/yyyy";
            ////Sareegritsana.a add Wed 04-03-58
            //public const string DDMMYYYY_DOT = "dd.MM.yyyy";
            //public const string DayMonthYear2 = "dd/MM/yy";
            //public const string DayMonthYear3 = "dd-MMM-yy";
            //public const string DayMonthYear4 = "dd-MMM-yyyy";
            //public const string MMMMdyyyy = "MMMM d, yyyy";
            //public const string HHMM = "hh:mm";
            //public const string HHMM_TH_TEXT = "ชั่วโมง:นาที";
            //public const string yyMM = "yyMM";
        }

        public static class Message
        {
            public const string AskConfirmSave = "ยืนยันการบันทึกข้อมูล";
            public const string SearchFoundData = "พบข้อมูล {0} รายการ";
            public const string SearchNotFoundData = "ไม่พบข้อมูล";
            public const string SaveSuccess = "บันทึกข้อมูลสำเร็จ";
            public const string SaveSuccess1prams = "บันทึกข้อมูลสำเร็จ {0}";
            public const string SaveUnsuccess = "บันทึกข้อมูลไม่สำเร็จ";
            public const string EditSuccess = "แก้ไขข้อมูลสำเร็จ";
            public const string NO_DATA_IMPORT = "ไม่พบข้อมูล";
            public const string INVALID_FORMAT_TEMPLATE = "รูปแบบในไฟล์ไม่ถูกต้อง";

            public const string Validate_Header_Record = "Header Record Type. error";
            public const string Validate_Header_Seq_No = "Header Sequence No. error";
            public const string Validate_Header_Bank_Code = "Header Bank Code. error";
            public const string Validate_Header_Company_account = "Header Company account no. error";
            public const string Validate_Header_Company_Name = "Header Company Name. error";
            public const string Validate_Header_Effective_Date = "Header Effective date. error";


            public const string Validate_Space = "รูปแบบไฟล์ไม่ถูกต้อง : จำนวนตัวอักษรในบรรทัดไม่ครบ \"{0}\" ตัวอักษร";
            //public const string Validate_Header_Company_Name = "Header Company account no. error";

            public const string Validate_PostDate_Unmatch = "วันที่รับชำระเงิน \"{0}\" ไม่ตรงกับวันที่รับชำระใน Text file";
            public const string No_Data_Import = "ไม่พบข้อมูลในไฟล์.";
            public const string Line_Of_Detail_Not_Match = "จำนวนข้อมูลรายการที่ได้รับ Detail =  \"{0}\" ไม่ตรงกับจำนวนข้อมูล Trailer = \"{1}\"";
            public const string Bank_Code_Not_Match = "รหัสธนาคารที่เลือกไม่สอดคล้องกับข้อมูลในไฟล์";
            public const string Bank_Code_Not_Have_In_System = "ไม่พบรหัสธนาคารที่เลือกในระบบ";

            public const string ChooseAtLeastOne = "กรุณาเลือกอย่างน้อย 1 รายการ";

            #region RecieptCancel
            public const string Validate_ReceiptNo_Not_Latest = "เลขที่ใบเสร็จรับเงิน \"{0}\" ไม่สามารถยกเลิกได้ เนื่องจากต้องยกเลิกเลขที่ใบเสร็จรับเงิน \"{1}\" ก่อน";
            public const string Validate_ReceiptNo_Not_Found = "ไม่พบเลขที่ใบเสร็จรับเงิน \"{0}\"";
            public const string Validate_ReceiptNo_Cancel_Already = "เลขที่ใบเสร็จรับเงิน \"{0}\" ถูกยกเลิกแล้ว ณ วันที่ {1}";
            #endregion
            #region ImportFile
            public const string Validate_BankCode_Not_Found = "รหัสธนาคาร \"{0}\" ไม่พบในระบบ";
            public const string Validate_Datetime_Incorrect = "รูปแบบวันที่ไม่ถูกต้อง";
            public const string Validate_Value_Not_Null = "ข้อมูล \"{0}\" ห้ามว่าง";
            public const string Validate_Sum_Total_Amount_Not_Match = "จำนวนเงินรวม Detail = \"{0}\" ไม่เท่ากับจำนวนเงินรวม Trailer = \"{1}\"";
            #endregion

            #region Re-Print Receipt
            public const string Validate_Print_Success = "พิมพ์ใบเสร็จรับเงิน : {0} สำเร็จ";
            public const string Validate_Print_Fail = "พิมพ์ใบเสร็จรับเงิน : {0} ผิดพลาด";
            #endregion

        }
        public static class BankCode
        {
            public const string TMB = "011"; //ธ. ทหารไทย จำกัด (มหาชน)
            public const string KBANK = "004"; //ธ. กสิกรไทย จำกัด (มหาชน)

            public const string KTB = "006"; //ธ. กรุงไทย จำกัด (มหาชน)
            public const string SCB = "014"; //ธ. ไทยพาณิชย์ จำกัด (มหาชน)
            public const string BAY = "025"; //ธ. กรุงศรีอยุธยา จำกัด (มหาชน)
            public const string TBANK = "065"; //ธ. ธนชาต จำกัด (มหาชน)
            public const string TESCO = "TES";
            public const string TRUE = "TMN";
            //public const string TESCO = "098"; //ธ. พัฒนาวิสาหกิจขนาดกลางและขนาดย่อมแห่งประเทศไทย


        }

        public struct ParaCode
        {
            public const string sysDate = "SYSDATE";
            public const string pageSize = "PAGESIZE";
            public const string timeOut = "TIMEOUT";
            public const string DaysR3 = "X08";
            public const string ParaX09 = "X09";
            public const string ParaX10 = "X10";
            public const string ParaX11 = "X11";
            public const string ParaVat = "X12";
            public const string ParaTax = "X13";
            public const string ParaX14 = "X14";
            public const string ParaX15 = "X15";
            public const string ParaX16 = "X16";
        }

        //public struct ServiceType
        //{
        //    public const string LEASING = "LI";

        //}
        public struct SESSION
        {
            public const string ReportDailyCashReceipt = "ReportDailyCashReceipt";
            public const string PaymentList = "PaymentList";
            public const string CancelReceipt = "CancelReceipt";
            public const string Contract = "Contract";
            public const string PaymentInfo = "PaymentInfo";


            public const string SearchManualReceive = "SearchManualReceive";

        }

        public struct VatCalculateType
        {
            public const string InVat = "VT01";
            public const string ExcludeVat = "VT02";
            public const string NoVat = "VT03";

        }

        public struct M_SysConfig
        {
            public const string SysConfig_Code = "SysConfig_Code";
            public const string SysConfig_Description = "SysConfig_Description";
            public const string SysConfig_Value = "SysConfig_Value";
            public const string SysConfig_Value2 = "SysConfig_Value2";

        }
        public struct M_RateType
        {
            public const string AllowanceDoubtful = "AllowanceDoubtful"; //อัตราค่าเผื่อหนี้สงสัยจะสูญ
        }
        public struct SysConfigType
        {
            public const string InterestDiscountRate = "InsDiscountRate";
            public const string EarlySettlementFeeRate = "EarlySettlementFeeRate";
            public const string SYSTEM_DATE = "SYSTEMDATE";
        }
        public struct SysConfigID
        {
            public const string InterestDiscountRate = "InsDC";
            public const string EarlySettlementFeeRate = "STTR";
            public const string SYSTEM_DATE = "SYSDATE";
        }

        public struct PropertiesType
        {
            public const string String = "string";
            public const string Decimal = "decimal";
            public const string Int = "int";
            public const string Datetime = "datetime";

        }

        //public struct InstallmentStatus
        //{
        //    public const string Paid = "PAY";
        //    public const string Active = "ACT";
        //    public const string UnPaid = "UPD";

        //}

        public struct InputVatType
        {
            public const string Product = "ค่าสินค้า";
        }


        public struct CostType
        {

            public const string Installment = "INS";
            //public const string Cancel = "CAN";

        }
        //Screen
        public struct SourceType
        {
            public const string TextFile = "TXT"; //นำเข้าไฟล์
            public const string Manual = "MAN"; //กรอกแบบไม่รู้เลขที่สัญญา
            public const string Cashier = "CSH"; //แคชเชียร์
            public const string Adjustment = "ADJ";
        }

        public struct DropdownList
        {
            public const string Text = "Text";
            public const string Value = "Value";
        }

        public struct FlagMatch
        {
            public const bool NO = false;
            public const bool YES = true;
        }

        public struct ProductType
        {
            public const string IS = "LT1";
            public const string HP = "LT2";
        }
        public struct LoanType
        {
            public const string LN = "LN";
            public const string HP = "HP";
            public const string LI = "LI";
        }
        //ช่องทางชำระเงิน
        public struct PaymentChannel
        {
            public const string CHEQUE = "CHE"; //เช็ค
            public const string DIRECT = "CRE"; //Direct Credit
            public const string CASH = "CSH"; //เงินสด
            public const string SMART = "NET"; //Smart
            public const string TRANS = "TRA"; //เงินโอน
            public const string SUSPENSE = "SUS"; //บัญชีพัก
        }
        public struct RunningType
        {
            public const string Suspense = "SUS";
            public const string Receipt = "RGC";
            public const string Invoice = "RTG";
            public const string CreditNote = "CNG";
            public const string Temporary = "TEP";
            public const string TempReceipt = "TRL";
            public const string TempInvoice = "TVL";
            public const string TempTemporary = "TLT";
            public const string Unknown = "UNK";
        }
        public struct RunningPrefix
        {
            public const string Suspense = "SP";
            public const string Receipt = "RL";
            public const string Invoice = "VL";
            public const string CreditNote = "CNG";
            public const string Temporary = "LT";
            public const string TempReceipt = "TRL";
            public const string TempInvoice = "TVL";
            public const string TempTemporary = "TLT";
            public const string Unknown = "UNK";
        }

        public struct RunningSuffix
        {
            public const string Suspense = "";
            public const string Receipt = "";
            public const string Invoice = "";
            public const string CreditNote = "";
            public const string Temporary = "";
            public const string TempReceipt = "";
            public const string TempInvoice = "";
            public const string TempTemporary = "";
            public const string Unknown = "";
        }

        public struct ReportName
        {
            public const string Receipt = "~/Reports/CrystalReportReceipt.rpt";
        }
        public struct MapPath
        {
            public const string Temp = "~/Temp/";
            public const string TempPdf = "~/Temp/Pdf/";
            public const string TempFile = "~/Temp/File/";
        }

        public struct CashReceiveAPIMethod
        {
            public const string UpdateWriteOff = "/api/WriteOff/UpdateWriteOff/";
            public const string ReturnSuspense = "/api/ReturnSuspense/UpdateStatement/";
        }
        public struct PaymentAPIMethod
        {
            public const string ReturnSuspense = "/api/ReturnSuspenseApi/InsertPayment/";
        }


        #region "Read Text File Position"

        public struct RecordType
        {
            public const string Header = "H";
            public const string Detail = "D";
            public const string Transaction = "T";
        }

        public static class StringPositionBillBANK
        {
            //TMB File Format Layout For Direct Debit/Credit (128 New format)
            //header
            public const int H_RECORD_TYPE_Start = 1;
            public const int H_RECORD_TYPE_End = 1;
            public const int H_SEQUENCE_NUMBER_Start = 2; public const int H_SEQUENCE_NUMBER_End = 7;
            public const int H_BANK_CODE_Start = 8; public const int H_BANK_CODE_End = 10;
            public const int H_COMPANY_ACCOUNT_NO_Start = 11; public const int H_COMPANY_ACCOUNT_NO_End = 20;
            public const int H_COMPANY_NAME_Start = 21; public const int H_COMPANY_NAME_End = 60;
            public const int H_EFFECTIVE_DATE_Start = 61; public const int H_EFFECTIVE_DATE_End = 68;
            public const int H_SERVICE_Start = 69; public const int H_SERVICE_End = 76;
            public const int H_SPARE_Start = 77; public const int H_SPARE_End = 256;
            //detail
            public const int D_RECORD_TYPE_Start = 1; public const int D_RECORD_TYPE_End = 1;
            public const int D_SEQUENCE_NUMBER_Start = 2; public const int D_SEQUENCE_NUMBER_End = 7;
            public const int D_BANK_CODE_Start = 8; public const int D_BANK_CODE_End = 10;
            public const int D_ACCOUNT_NUMBER_Start = 11; public const int D_ACCOUNT_NUMBER_End = 20;
            public const int D_PAYMENT_DATE_Start = 21; public const int D_PAYMENT_DATE_End = 28;
            public const int D_PAYMENT_TIME_Start = 29; public const int D_PAYMENT_TIME_End = 34;
            public const int D_CUSTOMER_NAME_Start = 35; public const int D_CUSTOMER_NAME_End = 84;
            public const int D_REF1_Start = 85; public const int D_REF1_End = 104;
            public const int D_REF2_Start = 105; public const int D_REF2_End = 124;
            public const int D_REF3_Start = 125; public const int D_REF3_End = 144;
            public const int D_BRANCH_NO_Start = 145; public const int D_BRANCH_NO_End = 148;
            public const int D_TELLER_NO_Start = 149; public const int D_TELLER_NO_End = 152;
            public const int D_KIND_OF_TRANSACTION_Start = 153; public const int D_KIND_OF_TRANSACTION_End = 153;
            public const int D_TRANSACTION_CODE_Start = 154; public const int D_TRANSACTION_CODE_End = 156;
            public const int D_CHEQUE_Start = 157; public const int D_CHEQUE_End = 163;
            public const int D_AMOUNT_Start = 164; public const int D_AMOUNT_End = 176;
            public const int D_CHEQUE_BANK_CODE_Start = 177; public const int D_CHEQUE_BANK_CODE_End = 179;
            public const int D_SPACE_Start = 180; public const int D_SPACE_End = 256;
            //total
            public const int F_RECORD_TYPE_Start = 1; public const int F_RECORD_TYPE_End = 1;
            public const int F_SEQUENCE_NUMBER_Start = 2; public const int F_SEQUENCE_NUMBER_End = 7;
            public const int F_BANK_CODE_Start = 8; public const int F_BANK_CODE_End = 10;
            public const int F_COMPANY_ACCOUNT_NO_Start = 11; public const int F_COMPANY_ACCOUNT_NO_End = 20;
            public const int F_TOTAL_DR_AMOUNT_Start = 21; public const int F_TOTAL_DR_AMOUNT_End = 33;
            public const int F_TOTAL_DR_TRANSACTION_Start = 34; public const int F_TOTAL_DR_TRANSACTION_End = 39;
            public const int F_TOTAL_CR_AMOUNT_Start = 40; public const int F_TOTAL_CR_AMOUNT_End = 52;
            public const int F_TOTAL_CR_TRANSACTION_Start = 53; public const int F_TOTAL_CR_TRANSACTION_End = 58;
            public const int F_SPARE_Start = 59; public const int F_SPARE_End = 256;

        }

        public static class StringPositionDirectSCB
        {

            //Header
            //            public const int SCB_Record_Type_Start = 1; public const int SCB_Record_Type_End = 3;
            //            public const int SCB_Company_Id_Start = 4; public const int SCB_Company_Id_End = 15;
            //            public const int SCB_Customer_Reference_Start = 16; public const int SCB_Customer_Reference_End = 47;
            //            public const int SCB_File_Date_Start = 48; public const int SCB_Message/File_Date_End = 55;
            //public const int SCB_Message/File_Time_Start = 56;public const int SCB_Message/File_Time_End = 61;
            //public const int SCB_Channel_Id_Start = 62; public const int SCB_Channel_Id_End = 64;
            //            public const int SCB_Batch_Reference_Start = 65; public const int SCB_Batch_Reference_End = 96;
            //            public const int SCB_Record_Type_Start = 1; public const int SCB_Record_Type_End = 3;
            //            public const int SCB_Product_Code_Start = 4; public const int SCB_Product_Code_End = 6;
            //            public const int SCB_Internal_Reference_Start = 7; public const int SCB_Internal_Reference_End = 14;
            //            public const int SCB_Value_Date_Start = 15; public const int SCB_Value_Date_End = 22;
            //            public const int SCB_Processing_Round_Start = 23; public const int SCB_Processing_Round_End = 24;
            //            public const int SCB_Credit_Account_No_Start = 25; public const int SCB_Credit_Account_No_End = 49;
            //            public const int SCB_Account_Type_of_Credit_Account_Start = 50; public const int SCB_Account_Type_of_Credit_Account_End = 51;
            //            public const int SCB_Credit_Bank_Code_Start = 52; public const int SCB_Credit_Bank_Code_End = 54;
            //            public const int SCB_Credit_Branch_Code_Start = 55; public const int SCB_Credit_Branch_Code_End = 58;
            //            public const int SCB_Fee_Debit_Account_Start = 59; public const int SCB_Fee_Debit_Account_End = 83;
            //            public const int SCB_Fee_Account_Type_Start = 84; public const int SCB_Fee_Account_Type_End = 85;
            //            public const int SCB_Fee_Bank_Code_Start = 86; public const int SCB_Fee_Bank_Code_End = 88;
            //            public const int SCB_Fee_Branch_Code_Start = 89; public const int SCB_Fee_Branch_Code_End = 92;
            //            public const int SCB_Credit_Currency_Start = 93; public const int SCB_Credit_Currency_End = 95;
            //            public const int SCB_Credit_Amount_Start = 96; public const int SCB_Credit_Amount_End = 111;
            //            public const int SCB_No._of_Debits_Start = 112;public const int SCB_No._of_Debits_End = 117;
            //public const int SCB_Description_Start = 118; public const int SCB_Description_End = 187;
            //            public const int SCB_Customer_Reference_Start = 188; public const int SCB_Customer_Reference_End = 257;
            //            public const int SCB_Filler_Start = 258; public const int SCB_Filler_End = 300;
            //            public const int SCB_Record_Type_Start = 1; public const int SCB_Record_Type_End = 3;
            //            public const int SCB_Internal_Reference_Start = 4; public const int SCB_Internal_Reference_End = 11;
            //            public const int SCB_Debit_Sequence_Number_Start = 12; public const int SCB_Debit_Sequence_Number_End = 17;
            //            public const int SCB_Debit_Account_No_Start = 18; public const int SCB_Debit_Account_No_End = 42;
            //            public const int SCB_Debit_Currency_Start = 43; public const int SCB_Debit_Currency_End = 45;
            //            public const int SCB_Debit_Amount_Start = 46; public const int SCB_Debit_Amount_End = 61;
            //            public const int SCB_Debit_Reference_Code_Start = 62; public const int SCB_Debit_Reference_Code_End = 81;
            //            public const int SCB_Remark_Start = 82; public const int SCB_Remark_End = 151;
            //            public const int SCB_Filler_Start = 152; public const int SCB_Filler_End = 200;
            //            public const int SCB_Record_Type_Start = 1; public const int SCB_Record_Type_End = 3;
            //            public const int SCB_Total_No_of_Debits_Start = 4;public const int SCB_Total_No_of_Debits_End = 9;
            //public const int SCB_Total_No_of_Credits_Start = 10;public const int SCB_Total_No_of_Credits_End = 15;
            //public const int SCB_Total_Amount_Start = 16; public const int SCB_Total_Amount_End = 31;

            public const string EndOfStream = "EOF";
        }

        public static class StringPositionDirectTMB
        {
            //TMB File Format Layout For Direct Debit/Credit (128 New format)
            //header
            public const int TMB_H_RECORD_TYPE_Start = 1;
            public const int TMB_H_RECORD_TYPE_End = 1;
            public const int TMB_H_SEQUENCE_NUMBER_Start = 2; public const int TMB_H_SEQUENCE_NUMBER_End = 7;
            public const int TMB_H_BANK_CODE_Start = 8; public const int TMB_H_BANK_CODE_End = 10;
            public const int TMB_H_COMPANY_ACCOUNT_NO_Start = 11; public const int TMB_H_COMPANY_ACCOUNT_NO_End = 20;
            public const int TMB_H_COMPANY_NAME_Start = 21; public const int TMB_H_COMPANY_NAME_End = 45;
            public const int TMB_H_POST_DATE_Start = 46; public const int TMB_H_POST_DATE_End = 51;
            public const int TMB_H_TAPE_NUMBER_Start = 52; public const int TMB_H_TAPE_NUMBER_End = 57;
            public const int TMB_H_SPARE_Start = 58; public const int TMB_H_SPARE_End = 128;
            //detail
            public const int TMB_D_RECORD_TYPE_Start = 1; public const int TMB_D_RECORD_TYPE_End = 1;
            public const int TMB_D_SEQUENCE_NUMBER_Start = 2; public const int TMB_D_SEQUENCE_NUMBER_End = 7;
            public const int TMB_D_BANK_CODE_Start = 8; public const int TMB_D_BANK_CODE_End = 10;
            public const int TMB_D_ACCOUNT_NUMBER_Start = 11; public const int TMB_D_ACCOUNT_NUMBER_End = 20;
            public const int TMB_D_TRANSACTION_CODE_Start = 21; public const int TMB_D_TRANSACTION_CODE_End = 21;
            public const int TMB_D_AMOUNT_Start = 22; public const int TMB_D_AMOUNT_End = 31;
            public const int TMB_D_SERVICE_TYPE_Start = 32; public const int TMB_D_SERVICE_TYPE_End = 33;
            public const int TMB_D_STATUS_Start = 34; public const int TMB_D_STATUS_End = 34;
            public const int TMB_D_REFERENCE_AREA_1_Start = 35; public const int TMB_D_REFERENCE_AREA_1_End = 44;
            public const int TMB_D_INSERVICE_DATE_Start = 45; public const int TMB_D_INSERVICE_DATE_End = 50;
            public const int TMB_D_COMPANY_CODE_Start = 51; public const int TMB_D_COMPANY_CODE_End = 54;
            public const int TMB_D_HOME_BRANCH_Start = 55; public const int TMB_D_HOME_BRANCH_End = 57;
            public const int TMB_D_REFERENCE_AREA_2_Start = 58; public const int TMB_D_REFERENCE_AREA_2_End = 77;
            public const int TMB_D_TMB_FLAG_Start = 78; public const int TMB_D_TMB_FLAG_End = 83;
            public const int TMB_D_SPARE_Start = 84; public const int TMB_D_SPARE_End = 93;
            public const int TMB_D_ACCOUNT_NAME_Start = 94; public const int TMB_D_ACCOUNT_NAME_End = 128;
            //total
            public const int TMB_F_RECORD_TYPE_Start = 1; public const int TMB_F_RECORD_TYPE_End = 1;
            public const int TMB_F_SEQUENCE_NUMBER_Start = 2; public const int TMB_F_SEQUENCE_NUMBER_End = 7;
            public const int TMB_F_BANK_CODE_Start = 8; public const int TMB_F_BANK_CODE_End = 10;
            public const int TMB_F_COMPANY_ACCOUNT_NO_Start = 11; public const int TMB_F_COMPANY_ACCOUNT_NO_End = 20;
            public const int TMB_F_NO_OF_DR_TRANSACTION_Start = 21; public const int TMB_F_NO_OF_DR_TRANSACTION_End = 27;
            public const int TMB_F_TOTAL_DR_AMOUNT_Start = 28; public const int TMB_F_TOTAL_DR_AMOUNT_End = 40;
            public const int TMB_F_NOOF_CR_TRANSACTION_Start = 41; public const int TMB_F_NOOF_CR_TRANSACTION_End = 47;
            public const int TMB_F_TOTAL_CR_AMOUNT_Start = 48; public const int TMB_F_TOTAL_CR_AMOUNT_End = 60;
            public const int TMB_F_NOOF_REJECT_DR_TRANS_Start = 61; public const int TMB_F_NOOF_REJECT_DR_TRANS_End = 67;
            public const int TMB_F_TOTAL_REJECT_DR_AMOUNT_Start = 68; public const int TMB_F_TOTAL_REJECT_DR_AMOUNT_End = 80;
            public const int TMB_F_NO_OF_REJECT_CR_TRANS_Start = 81; public const int TMB_F_NO_OF_REJECT_CR_TRANS_End = 87;
            public const int TMB_F_TOTAL_REJECT_CR_AMOUNT_Start = 88; public const int TMB_F_TOTAL_REJECT_CR_AMOUNT_End = 100;
            public const int TMB_F_SPARE_Start = 101; public const int TMB_F_SPARE_End = 128;

        }

        public struct ProgramCode
        {
            public static string Cashier = "P101";
            public static string CancelReceipt = "P102";
            public static string PrintReceipt = "P103";
            public static string Import = "P201";
            public static string ReportCashReceivePayment = "P301";

        }

        public struct ActionCode
        {
            public static string Cashier_View = "A1011";
            public static string Cashier_Save = "A1012";
            public static string Cashier_Edit = "A1013";
            public static string CancelReceipt_View = "A1021";
            public static string CancelReceipt_Save = "A1022";
            public static string PrintReceipt_View = "A1031";
            public static string PrintReceipt_Save = "A1032";
            public static string PrintReceipt_Edit = "A1033";
            public static string PrintReceipt_Print = "A1034";
            public static string Import_View = "A2011";
            public static string Import_Save = "A2012";
            public static string Import_Edit = "A2013";
            public static string ReportCashReceivePayment_View = "A3011";
        }

        public struct ExcelImportColumn
        {
            public static string CONT_CODE = "เลขที่สัญญา";
            public static string ID_CARD_NO = "เลขบัตรประจำตัว";
            public static string TITLE_NAME = "คำนำหน้า";
            public static string FIRST_NAME = "ชื่อลูกค้า";
            public static string LAST_NAME = "นามสกุล";
            public static string ADDRESS = "ADDRESS";
            public static string PHONE_NUMBER = "เบอร์โทร";
            public static string INSTALLMENT_PERMONNT = "INSTALLMENT_PERMONNT";
            public static string INSTALLMENT_PERMONNT_TH = "ค่างวดต่อเดือน";
            public static string INSTALLMENT_REMAIN = "INSTALLMENT_REMAIN";
            public static string INSTALLMENT_REMAIN_TH = "ค่างวดค้างชำระ";
            public static string RECEIVED = "RECEIVED";
            public static string RECEIVED_TH = "จำนวนเงินรับ";
            public static string REMARK = "REMARK";
            public static string REMARK_TH = "หมายเหตุ";


        }

        #endregion



    }
}