using Assessment.Core.Models;
using Assessment.Data;
using Assessment.Reporting.Dtos;
using Assessment.Reporting.Services;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting
{
    [DisallowConcurrentExecution]
    public class WeeklyReport : IJob
    {
        private readonly ILogger<WeeklyReport> _logger;
        private readonly CustomerService _customerService;
        private readonly ExcelService _excelService;
        private readonly UserFileService _userFileService;
        private readonly UserService _userService;
        private readonly EmailSenderService _emailSenderService;

        public WeeklyReport(ILogger<WeeklyReport> logger,
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

        public async Task Execute(IJobExecutionContext context)
        {
            //Haftalık olarak en fazla tiraci faliyete sahip ilk 5  müşteri email yoluyla raporlanacak. 
            //Bu rapor admin rolune sahip olanlara gönderilecektir.

            var customersMostTransactions = _customerService.GetCustomers();
            var userFile = _excelService.CreateWeeklyExcel(customersMostTransactions);
            _userFileService.Add(userFile);

            var admins = _userService.GetAdmins();
            _emailSenderService.Send(userFile, admins, true);

            _logger.LogInformation("Weekly Report --- Emails sended succesfully... " + DateTime.Now);
        }
    }
}
