using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple
{
    public class IPhone : ITelephone, ICamera
    {
        public void ConnectToInternet()
        {
            Console.WriteLine("ConnectToInternet");
        }

        public void MakeCall()
        {
            Console.WriteLine("MakeCall");
        }

        public void TakePhoto()
        {
            Console.WriteLine("TakePhoto");
        }

        public void TakePortrait()
        {
            Console.WriteLine("TakePortrait");
        }

        public void TakeVideo()
        {
            Console.WriteLine("TakeVideo");
        }
    }
}
