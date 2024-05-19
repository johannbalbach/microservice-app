using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Consts
{
    public class EmailConfiguration
    {
        /*        public const string From = "EnrollmentSystem505@yandex.ru";
                public const string SmtpServer = "smtp.yandex.ru";
                public const int Port = 55;
                public const string UserName = "EnrollmentSystem505";
                public const string Password = "505Enrollmentsystem";*/
        public string? DisplayName { get; set; }
        public string? From { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
        public bool UseOAuth { get; set; }
    }
}
