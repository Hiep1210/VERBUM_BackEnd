using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using verbum_service_domain.Models.Mail;

namespace verbum_service_application.Service
{
    public interface MailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
