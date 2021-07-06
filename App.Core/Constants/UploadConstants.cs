using Microsoft.Extensions.Configuration;
using System.IO;

namespace App.Core.Constants
{
    public class UploadConstants
    {

        public  string UPLOAD_Path { get; set; }
        // Current structre
     //   public  string LISTINGS_UPLOAD_FOLDER =   "category/";
       
       public UploadConstants()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            var appSettingsConfiguration = root.GetSection("AppSettings");
            string SiteUrl = appSettingsConfiguration.GetSection("UploadUrl").Value;
            this.UPLOAD_Path = SiteUrl;
        }
    }
}
