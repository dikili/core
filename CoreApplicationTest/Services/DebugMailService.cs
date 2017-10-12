using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplicationTest.Services
{
    public class DebugMailService : IMailService
    {
        public void SendMail(string to, string from, string subject, string body)
        {
           Debug.WriteLine($"Sending Mail: To:{to} from:{from} subject:{subject}");
        }
    }
}
