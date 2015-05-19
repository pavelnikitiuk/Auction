using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.ModelBinder
{
    public class XSSModelBinder : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                return base.BindModel(controllerContext, bindingContext);
            }
            catch (HttpRequestValidationException)
            {
                Trace.TraceWarning("Ilegal characters were found in field {0}", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName);
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, string.Format("Ilegal characters were found in field {0}", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName));
            }
            IUnvalidatedValueProvider provider = bindingContext.ValueProvider as IUnvalidatedValueProvider;
            if (provider == null) return null;
            var result = provider.GetValue(bindingContext.ModelName, skipValidation: true);
            Debug.Assert(result != null, "result is null");
            return result.AttemptedValue;
        }
    }
}