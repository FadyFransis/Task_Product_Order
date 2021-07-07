using App.Common.Repositories;
using System.Collections.Generic;

namespace Common.Services.MobileNotification
{
    public class MobileNotificationService : IMobileNotification
    {
        MobileNotificationRepository mobileNotificationRepository;
        public MobileNotificationService(string NotificationHubConnection, string NotificationHubName)
        {
            mobileNotificationRepository = new MobileNotificationRepository(NotificationHubConnection, NotificationHubName);
        }
        public bool PushNotification(string description)
        {
            return mobileNotificationRepository.PushNotification(description);
        }

        public bool PushNotification(List<string> devicesIDs, string description)
        {
            return mobileNotificationRepository.PushNotification(devicesIDs, description);
        }
    }
}
