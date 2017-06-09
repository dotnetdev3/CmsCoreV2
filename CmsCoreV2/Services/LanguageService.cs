using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Services
{
    public class LanguageService:ILanguageService
    {
        public readonly IList<Language> Languages;
        public readonly IList<Resource> Resources;
        public LanguageService(ApplicationDbContext context)
        {
            Languages = context.Languages.ToList();
            Resources = context.Resources.ToList();
        }
        public LocalizedString GetResource(string name, string currentCulture, params object[] arguments)
        {
            var langId = Languages.SingleOrDefault(l => l.Culture == currentCulture).Id;            
            var resource = Resources.SingleOrDefault(r => r.Name == name && r.LanguageId == langId);
            var value = name;
            if (resource != null)
            {
                value = resource.Value;
            }
            return new LocalizedString(name, value, resource == null);
        }
        public LocalizedString GetResource(string name, string currentCulture)
        {
            var resource = Resources.SingleOrDefault(r => r.Name == name);
            var value = name;
            if (resource != null)
            {
                value = resource.Value;
            }
            return new LocalizedString(name, value, resource == null);
        }
    }
    public interface ILanguageService
    {
        LocalizedString GetResource(string name, string currentCulture, params object[] arguments);
        LocalizedString GetResource(string name, string currentCulture);

    }
}
