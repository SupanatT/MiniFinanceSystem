using MiniFinance.Controllers;
using MiniFinance.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniFinance.Models.BusinessLogicModel
{
    public class BaseBLLModel
    {
        private readonly static BaseController objBaseController = new BaseController();
        protected readonly DateTime DateTimeNow = objBaseController.DateTimeNow;
        protected ACCOUNTINGEntities _dbContext = new ACCOUNTINGEntities();
    }
}