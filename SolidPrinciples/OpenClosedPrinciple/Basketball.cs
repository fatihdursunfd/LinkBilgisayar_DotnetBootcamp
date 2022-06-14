using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple
{
    public class Basketball : Sport
    {
        public int Win { get; set; }
        public int Lose { get; set; }

        public override int CalculatePoint()
        {
            return Win * 2 + Lose;
        }

    }
}
