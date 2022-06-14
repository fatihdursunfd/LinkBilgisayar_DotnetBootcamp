using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple
{
    public class Footbal : Sport
    {
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }

        public override int CalculatePoint()
        {
            return Win * 3 + Draw;
        }

    }
}
