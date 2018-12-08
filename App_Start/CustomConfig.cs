using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace MiniFinance.App_Start
{
    public class CustomConfig
    {
        public class CultureAwareControllerActivator : IControllerActivator
        {
            public IController Create(RequestContext requestContext, Type controllerType)
            {
                //Get the {language} parameter in the RouteData
                string language = requestContext.RouteData.Values["language"]?.ToString() ?? "th-TH";

                //Get the culture info of the language code
                CultureInfo culture = CultureInfo.GetCultureInfo(language);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                return DependencyResolver.Current.GetService(controllerType) as IController;
            }
        }

        public class DecimalModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext,
                                            ModelBindingContext bindingContext)
            {
                ValueProviderResult valueResult = bindingContext.ValueProvider
                    .GetValue(bindingContext.ModelName);
                ModelState modelState = new ModelState { Value = valueResult };
                object actualValue = null;
                try
                {
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue,
                        CultureInfo.CurrentCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }

                bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
                return actualValue;
            }
        }



    }
}