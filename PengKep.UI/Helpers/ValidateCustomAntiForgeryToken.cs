using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace PengKep.Helpers
{

        public class ValidateCustomAntiForgeryToken : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                string cookieToken = "";
                string formToken = "";

                IEnumerable<string> tokenHeaders;

                tokenHeaders = filterContext.HttpContext.Request.Headers.GetValues("RequestVerificationToken");
                if (tokenHeaders != null)
                {
                    string[] tokens = tokenHeaders.First().Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }

                System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);

                base.OnActionExecuting(filterContext);
            }

            /*public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                
                string cookieToken = "";
                string formToken = "3r3r";

                IEnumerable<string> tokenHeaders;
                if (actionContext.Request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
                {
                    string[] tokens = tokenHeaders.First().Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
                System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);

                base.OnActionExecuting(actionContext);

            }*/

        }
}