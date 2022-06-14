using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple
{
    public class CalculatePoint
    {
        public int point = 0;

        // Aşağıdaki kod open-closed yaklaşımına uygun değildir. Örneğin bir başka spor için puan hesaplamak gerektiğinde
        // Point fonksiyonu değişime ugrayacaktır. Bu yüzden Point fonksiyonunu başka bir spor eklendiğinde değişmeyecek 
        // bir hale getirmek gerekir. Çünkü open-closed yaklaşımı bir classın değişmemesi gerektiğini savunur.

        //public int Point(object [] sports)
        //{
        //    int point = 0;

        //    foreach (var sport in sports)
        //    {
        //        if (sport is Footbal)
        //        {
        //            Footbal footbal = (Footbal)sport;
        //            point += footbal.Draw + footbal.Win * 3;
        //        }
        //        if(sport is Basketball)
        //        {
        //            Basketball basketball = (Basketball)sport;
        //            point = basketball.Win * 2 + basketball.Lose;
        //        }
        //    }

        //    return point;
        //}

        public int Point(List<Sport> sports)
        {
            int point = 0;

            foreach (var sport in sports)
            {
                point += sport.CalculatePoint();
            }

            return point;
        }
    }
}
