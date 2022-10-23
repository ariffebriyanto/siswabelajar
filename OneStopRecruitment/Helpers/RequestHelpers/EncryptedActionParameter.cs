using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using OneStopRecruitment.Helpers.TagHelpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OneStopRecruitment.Helpers.RequestHelpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Dictionary<string, object> decodedActionArguments = new Dictionary<string, object>();

            NameValueCollection queryStr = HttpUtility.ParseQueryString(context.HttpContext.Request.QueryString.Value);
            foreach (string key in queryStr)
            {
                string Decoded = Base16.Decode(queryStr[key].ToString());
                string Val = RouteValueEncryption.Decrypt(Decoded);
                decodedActionArguments[key] = Val;
            }

            IList<ParameterDescriptor> parameters = context.ActionDescriptor.Parameters;
            foreach (KeyValuePair<string, object> arg in decodedActionArguments)
            {
                ParameterDescriptor parameterDescriptor = parameters.Where(x => x.Name.ToLower().Equals(arg.Key.ToLower())).FirstOrDefault();
                string ParameterType = parameterDescriptor?.ParameterType.FullName;

                object Val = decodedActionArguments[arg.Key];

                TypeConverter conv = TypeDescriptor.GetConverter(Type.GetType(ParameterType));
                Val = conv.ConvertFrom(Val);

                context.ActionArguments[parameterDescriptor.Name] = Val;
            }
            base.OnActionExecuting(context);
        }

        public static string From(string Param) => Base16.Encode(RouteValueEncryption.Encrypt(Param));
    }
}
