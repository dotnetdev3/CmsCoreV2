using CmsCoreV2.Models;
using CmsCoreV2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2
{
    public class CustomLocalizer : StringLocalizer<String>
    {
        private readonly IStringLocalizer _internalLocalizer;
        private readonly ILanguageService _languageService;

        public CustomLocalizer(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor) : base(factory)
        {
            CurrentCulture = httpContextAccessor.HttpContext.GetRouteValue("culture") as string;
            if (string.IsNullOrEmpty(CurrentCulture))
            {
                CurrentCulture = "tr";
            }

            _internalLocalizer = WithCulture(new CultureInfo(CurrentCulture));
            _languageService = (ILanguageService)httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ILanguageService));
        }

        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                //var l = _internalLocalizer[name, arguments];
                //if (l.ResourceNotFound)
                //{
                   return  _languageService.GetResource(name, CurrentCulture, arguments);
                //}
                //return l;
            }
        }

        public override LocalizedString this[string name]
        {
            get
            {
                //var l = _internalLocalizer[name];
                //if (l.ResourceNotFound)
                //{
                    return _languageService.GetResource(name, CurrentCulture);
                //}
                //return l;
            }
        }

        public string CurrentCulture { get; set; }
    }
}
