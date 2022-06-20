using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Model
{
    public class Response<T> where T : class
    {
        public T Data { get;  set; }
        public int StatusCode { get;  set; }
        public string Error { get;  set; }

    }
}
