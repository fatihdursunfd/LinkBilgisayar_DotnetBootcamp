using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple
{
    public interface ICamera
    {
        public void TakePhoto();

        public void TakeVideo();

        public void TakePortrait();
    }
}
