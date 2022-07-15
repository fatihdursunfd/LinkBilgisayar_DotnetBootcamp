using Assessment.Reporting.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting
{
    [DisallowConcurrentExecution]
    public class MonthlyReport : IJob
    {
        private readonly ILogger<WeeklyReport> _logger;
        private readonly CustomerService _customerService;
        private readonly ExcelService _excelService;
        private readonly UserFileService _userFileService;
        private readonly UserService _userService;
        private readonly EmailSenderService _emailSenderService;

        public MonthlyReport(ILogger<WeeklyReport> logger,
                            CustomerService customerService,
                            ExcelService excelService,
                            UserFileService userFileService,
                            UserService userService,
                            EmailSenderService emailSenderService)
        {
            _logger = logger;
            _customerService = customerService;
            _excelService = excelService;
            _userFileService = userFileService;
            _userService = userService;
            _emailSenderService = emailSenderService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Aylık olarak email yoluyla  hangi şehirde kaç tane müşteri olduğu raporlancak.

            var customersWithCity = _customerService.GetCustomersWithCity();
            var userFile = _excelService.CreateMonthlyExcel(customersWithCity);
            _userFileService.Add(userFile);

            var admins = _userService.GetAdmins();
            _emailSenderService.Send(userFile, admins, false);

            _logger.LogInformation("Monthly Report --- Emails sended succesfully... " + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
