using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple
{
    public interface ITelephone
    {
        // Aşağıdaki fonksiyonlara bakıldığında üçünün kamera ile ilgili olduğu görülmektedir. Interface Segregation
        // prensipine uymak için kamera ile ilgili olan sorumlulukları başka bir interface'e taşıyarak bu presibe uyulmuş olur.
        //public void TakePhoto();

        //public void TakeVideo();

        //public void TakePortrait();

        public void MakeCall();

        public void ConnectToInternet();
    }
}
