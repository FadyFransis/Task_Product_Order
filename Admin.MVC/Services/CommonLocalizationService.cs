using Admin.MVC.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;


namespace Admin.MVC.Services
{
    public class CommonLocalizationService
    {
        private readonly IStringLocalizer localizer;
        public CommonLocalizationService(IStringLocalizerFactory factory)
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            localizer = factory.Create(nameof(SharedResource), assemblyName.Name);
        }

        // if we have formatted string we can provide arguments         
        // e.g.: @Localizer.Text("Hello {0}", User.Name)
        public LocalizedString Text(string key, params string[] arguments)
        {
            return arguments == null
                ? localizer[key]
                : localizer[key, arguments];
        }

        public string Get(string key)
        {
            var t = localizer[key];
            return t;
        }
    }
}
