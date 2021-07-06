using App.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Services.Mail
{
    public interface IMailNotification
    {
        bool SendMail(EmailDTO emailParameters);
        bool SendMailWithAttachment(EmailDTO emailParameters, MemoryStream attachedFile, string attachmentName);
    }
}
