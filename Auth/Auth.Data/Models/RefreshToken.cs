using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Data.Models
{
    public class RefreshToken
    {
        public string UserId { get; set; }
        public string UserRefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
