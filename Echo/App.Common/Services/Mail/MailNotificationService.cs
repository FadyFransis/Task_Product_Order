using App.Common.Models;
using App.Common.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Services.Mail
{
    public class MailNotificationService : IMailNotification
    {
        static MailRepository mailRepository = new MailRepository();
        public bool SendMail(EmailDTO emailParameters)
        {
            return mailRepository.SendMail(emailParameters);
        }

        public bool SendMailWithAttachment(EmailDTO emailParameters, MemoryStream attachedFile, string attachmentName)
        {
            return mailRepository.SendMail(emailParameters);
        }
       
       
    }
}
