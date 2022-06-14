using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionPrinciple
{
    public class Twitter : SocialMedia
    {

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{message} send");
        }

        //public override void LiveBroadCast()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
