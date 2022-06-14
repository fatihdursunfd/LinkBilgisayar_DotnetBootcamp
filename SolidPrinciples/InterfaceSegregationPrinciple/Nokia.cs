using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple
{
    public class Nokia : ITelephone
    {
        public void ConnectToInternet()
        {
            Console.WriteLine("ConnectToInternet");
        }

        public void MakeCall()
        {
            Console.WriteLine("MakeCall");
        }
    }
}
