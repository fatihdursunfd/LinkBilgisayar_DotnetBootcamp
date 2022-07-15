using Assessment.Core.Models;
using Assessment.Reporting.Dtos;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Services
{
    public class ExcelService
    {
        public UserFile CreateWeeklyExcel(List<CustomerDto> customers)
        {
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                var fileName = Guid.NewGuid() + ".xlsx";
                workbook.Worksheets.Add("customers").FirstCell().InsertTable<CustomerDto>(customers, false);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files/", fileName).Replace("Assessment.Report", "Assessment.API");
                workbook.SaveAs(path);

                return new UserFile() { 
                    CreatedDate = DateTime.Now, 
                    FileName = fileName, 
                    FilePath = path , 
                    Description = $"Customers that most transaction between {DateTime.Now.ToString("dd / MM / yyyy")} and {DateTime.Now.AddDays(-7).ToString("dd / MM / yyyy") }" 
                };
            }
        }

        public UserFile CreateMonthlyExcel(List<CustomerCityDto> customers)
        {
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                var fileName = Guid.NewGuid() + ".xlsx";
                workbook.Worksheets.Add("customers").FirstCell().InsertTable<CustomerCityDto>(customers, false);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files/", fileName).Replace("Assessment.Report", "Assessment.API");
                workbook.SaveAs(path);

                return new UserFile()
                {
                    CreatedDate = DateTime.Now,
                    FileName = fileName,
                    FilePath = path,
                    Description = $" Customers count with city {DateTime.Now.ToString("dd / MM / yyyy")} and {DateTime.Now.AddDays(-30).ToString("dd / MM / yyyy") }"
                };
            }
        }
    }
}
