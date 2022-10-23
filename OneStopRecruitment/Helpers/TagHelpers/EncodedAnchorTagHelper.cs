using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStopRecruitment.Helpers.TagHelpers
{
    [HtmlTargetElement("a-encoded")]
    public class EncodedAnchorTagHelper : AnchorTagHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        protected IHtmlGenerator generator;

        public EncodedAnchorTagHelper(IHtmlGenerator generator, IHttpContextAccessor contextAccessor) : base(generator)
        {
            this.generator = generator;
            this.contextAccessor = contextAccessor;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Dictionary<string, string> encodedRouteValues = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> arg in RouteValues)
            {
                string Encoded = RouteValueEncryption.Encrypt(arg.Value);
                encodedRouteValues[arg.Key] = Base16.Encode(Encoded);
            }
            RouteValues = encodedRouteValues;
            await base.ProcessAsync(context, output);
            output.TagName = "a";
        }
    }
}
