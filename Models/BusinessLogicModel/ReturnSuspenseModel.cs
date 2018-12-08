using MiniFinance.Common;
using MiniFinance.ViewModel.ReturnSuspense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MiniFinance.Models.BusinessLogicModel
{
    public class ReturnSuspenseModel : BaseBLLModel
    {
        public bool CallAPI(SaveViewModel viewModel, string paramURL)
        {
            try
            {
                string apiURL = Constants.AppSetting.PathPaymentAPI + paramURL;
                HttpClient client = new HttpClient();
                var dataSend = JsonConvert.SerializeObject(viewModel);
                var stringContent = new StringContent(dataSend, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(apiURL, stringContent).Result;
                var readAPI = response.Content.ReadAsStringAsync().Result;
                bool IsApiSuccess = JsonConvert.DeserializeObject<bool>(readAPI);

                return IsApiSuccess;
            }
            catch(Exception)
            {
                throw;
            }
        }
        
  

    }
}