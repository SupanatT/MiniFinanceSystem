using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MiniFinance.ViewModel.Common
{
    public class PopAdditionalSearchViewModel
    {
        public string search { get; set; }
        public List<ListItem> RadioSearch { get; set; }
        public string ControllerName { get; set; }
    }
}