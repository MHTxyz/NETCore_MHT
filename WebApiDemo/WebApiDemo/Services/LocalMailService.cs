using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Services
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }

    public class LocalMailService :IMailService
    {
        private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];//"developer@qq.com";
        private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];//abc@qq.com";

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }

    public class CloudMailService : IMailService
    {
        private readonly ILogger<CloudMailService> _logger;
        private readonly string _mailTo = "admin@qq.com";
        private readonly string _mailFrom = "abc@qq.com";

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string msg)
        {
            //Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
