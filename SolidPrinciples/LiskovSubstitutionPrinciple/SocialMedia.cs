using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionPrinciple
{
    public abstract class SocialMedia
    {
        public abstract void SendMessage(string message);

        // Her sosyal medyada canlı yayın özelliği olmadığı için bu fonksiyonu abstract yapmak hataya sebep olacaktır.
        // Bu hatayı çözmek için canlı yayın için bir interface oluşturulur ve bu özelliğe sahip olan classlar bu interface'i 
        // kalıtım alarak kullanırlar.

        //public abstract void LiveBroadCast();

    }
}
