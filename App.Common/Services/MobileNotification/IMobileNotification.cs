using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.MobileNotification
{
    public interface IMobileNotification
    {
        bool PushNotification(string description);
        bool PushNotification(List<string> devicesIDs, string description);
    }
}
