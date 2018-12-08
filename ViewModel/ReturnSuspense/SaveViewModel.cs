using MiniFinance.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.ViewModel.ReturnSuspense
{
    public class SaveViewModel : PopPaymentViewModel
    {
        public bool isChecked { get; set; }
        public int STM_Id { get; set; }
        public string ContractCode { get; set; }
        public string CustomerCode { get; set; }
        public string PayeeCode { get; set; }
        public decimal SuspenseAmt { get; set; }
        public DateTime? ToRevenueDate { get; set; }
        public List<SaveViewModel> lsModel { get; set; }

    }
}