using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCoreV2.Services
{
    public class LanguageService:ILanguageService
    {
        public LocalizedString GetResource(string name, params object[] arguments)
        {
            return new LocalizedString(name, "Deneme", true);
        }
        public LocalizedString GetResource(string name)
        {
            return new LocalizedString(name, "Deneme", true);
        }
    }
    public interface ILanguageService
    {
        LocalizedString GetResource(string name, params object[] arguments);
        LocalizedString GetResource(string name);

    }
}
