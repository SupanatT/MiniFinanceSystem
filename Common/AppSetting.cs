using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MiniFinance.Common
{
    public class AppSetting
    {
        private string _logPath, _companyCode, _branchCode, _uiCulture, _dbCulture, _uiFormat, _systempCode, _webMode, _portalLogin, _pathCashreceiveAPI, _pathPaymentAPI;

        private int _pagecount, _pagesize, _RPSExecuteTimeOut, _AjaxTimeout;
        public string WebMode
        {
            get { return _webMode; }
            set { _webMode = value; }
        }
        public string SystempCode
        {
            get { return _systempCode; }
            set { _systempCode = value; }
        }
        public string LogPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }
        public string CompanyCode
        {
            get
            {
                if (_companyCode.IsNullOrEmpty())
                    return null;
                return _companyCode;
            }
            set { _companyCode = value; }
        }

        public string BranchCode
        {
            get
            {
                if (_branchCode.IsNullOrEmpty())
                    return null;
                return _branchCode;
            }
            set { _branchCode = value; }
        }

        private double _fileSizeUpload;




        //public string DecryptTripleDes(string text)
        //{
        //    try
        //    {
        //        Crypto.EncryptionAlgorithm = Crypto.Algorithm.TripleDES;
        //        Crypto.Encoding = Crypto.EncodingType.BASE_64;
        //        return Crypto.DecryptFast(text);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public string EncryptTripleDes(string text)
        //{
        //    try
        //    {
        //        Crypto.EncryptionAlgorithm = Crypto.Algorithm.TripleDES;
        //        Crypto.Encoding = Crypto.EncodingType.BASE_64;
        //        return Crypto.EncryptFast(text);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int Pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }

        public double FileSizeUpload
        {
            get { return _fileSizeUpload; }
            set { _fileSizeUpload = value; }
        }

        public int RPSExecuteTimeOut
        {
            get { return _RPSExecuteTimeOut; }
            set { _RPSExecuteTimeOut = value; }
        }

        public int AjaxTimeout
        {
            get { return _AjaxTimeout; }
            set { _AjaxTimeout = value; }
        }

        public string UICulture
        {
            get { return _uiCulture; }
            set { _uiCulture = value; }
        }

        public string UIFormat
        {
            get { return _uiFormat; }
            set { _uiFormat = value; }
        }

        public string DBCulture
        {
            get { return _dbCulture; }
            set { _dbCulture = value; }
        }
        public string PortalLogin
        {
            get { return _portalLogin; }
            set { _portalLogin = value; }
        }
        public string PathCashReceiveAPI
        {
            get { return _pathCashreceiveAPI; }
            set { _pathCashreceiveAPI = value; }
        }
        public string PathPaymentAPI
        {
            get { return _pathPaymentAPI; }
            set { _pathPaymentAPI = value; }
        }



        public AppSetting()
        {
            AppSettingsReader _configReader = new AppSettingsReader();
            //Crypto. objTrippleDes = new cTripleDES();
            try
            {
                _portalLogin = (string)_configReader.GetValue("PortalLogin", typeof(string));
                _systempCode = (string)_configReader.GetValue("SystemCode", typeof(string));
                _companyCode = (string)_configReader.GetValue("CompanyCode", typeof(string));
                _branchCode = (string)_configReader.GetValue("BranchCode", typeof(string));
                _webMode = (string)_configReader.GetValue("WebMode", typeof(string));
                _pathCashreceiveAPI = (string)_configReader.GetValue("PathCashReceiveAPI", typeof(string));
                _pathPaymentAPI = (string)_configReader.GetValue("PathPaymentAPI", typeof(string));
            }
            catch
            {
                throw;
            }
        }
    }
}