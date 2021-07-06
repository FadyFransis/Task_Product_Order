using App.Common.Services.Logger;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using App.Common.Models;
using RestSharp;
using Newtonsoft.Json;
namespace App.Common.Repositories
{
    internal class MailRepository
    {
        static Ilogger logger = new LoggerService();

        public bool SendMail(EmailDTO sendToApi)
        {
            try
            {


                // read appsetting js
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                var mailConfiguration = root.GetSection("EmailSettings");
                var client = new RestClient(mailConfiguration.GetSection("MailApi").Value);
                var request = new RestRequest("", Method.POST, DataFormat.Json);
                request.AddHeader("content-type", "application/json");
                EmailConfiguration EmailConfig = new EmailConfiguration()
                {
                    EnableSsl = true,
                    MailPort = mailConfiguration.GetSection("MailPort").Value,
                    MailServer = mailConfiguration.GetSection("MailServer").Value,
                    UserName = mailConfiguration.GetSection("UserName").Value,
                    Password = mailConfiguration.GetSection("Password").Value,
                    SupportEmail = mailConfiguration.GetSection("SupportEmail").Value,
                    AdminEmail = mailConfiguration.GetSection("AdminEmail").Value,
                };
                sendToApi.Configuration = EmailConfig;
                request.AddJsonBody(sendToApi);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                request.RequestFormat = DataFormat.Json;
                var response = client.Post(request);
                var result = JsonConvert.DeserializeObject<ServiceResponse>(response.Content);
                
                return result.Result;
            }
            catch (Exception ex)
            {
                logger.Error("Failed to send Email " + ex.Message);
                return false;
            }
        }
    }

}
