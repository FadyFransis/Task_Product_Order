using App.Common.Services.Logger;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Common.Repositories
{
    internal class MobileNotificationRepository
    {
        static Ilogger logger = new LoggerService();

        private string notificationHubConnection;
        private string notificationHubName;


        public MobileNotificationRepository(string NotificationHubConnection, string NotificationHubName)
        {
            this.notificationHubConnection = NotificationHubConnection;
            this.notificationHubName = NotificationHubConnection;
        }
        public bool PushNotification(string description)
        {
            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = description;

            try
            {
                hub.SendTemplateNotificationAsync(templateParams);
                //logger.Info(String.Format("PushNotification({0},{1}) result: {2}", clientId.ToString(), description, result.ToString()));
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("PushNotification({0}) Fail with error: {1}", description, ex.ToString()));
                return false;
            }
            return true;
        }

        public bool PushNotification(List<string> devicesIDs, string description)
        {
            if (devicesIDs == null || devicesIDs.Count() == 0)
                return true;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = description;

            try
            {
                foreach (string deviceID in devicesIDs)
                {
                    hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + deviceID + "}");
                }
                //logger.Info(String.Format("PushNotification({0},{1}) result: {2}", clientId.ToString(), description, result.ToString()));
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("PushNotification({0},{1}) Fail with error: {2}", devicesIDs.ToString(), description, ex.ToString()));
                return false;
            }
            return true;
        }
    }

}
