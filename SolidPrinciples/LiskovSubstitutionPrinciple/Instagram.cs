using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionPrinciple
{
    public class Instagram : SocialMedia , Live
    {
        public void LiveBroadCast()
        {
            Console.WriteLine("Live BroadCast");
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{message} send");
        }
    }
}
