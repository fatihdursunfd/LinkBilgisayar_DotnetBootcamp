using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Dtos
{
    public class UserFileDto
    {
        public DateTime CreatedDate { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public MemoryStream FileStream { get; set; }
    }
}
