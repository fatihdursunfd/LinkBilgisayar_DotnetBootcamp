using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{

    public class UserManager
    {

        // Bu class single responsibility prensibine aykırıdır. Çünkü 3 farklı sorumluluk bu sınıfa yüklenmiştir.
        // Bu classda hem propertyler tanımlanmış, hem şifreyi hashlemek için metod yazılmış
        // hem de kullanıcı eklemek için bir fonksiyon yazılmıştır.
        // Bu sorunu ortadan kaldırmak için her bir sorumluluk için farklı bir class oluşturulmuş. Böylelikle sorumluklar ayrılmş olur
        // ve her sınıf yalnızca bir sorumluluk sahibi olmuş olur.


        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Hash(string password)
        {
            byte[] salt = new byte[128 / 8];

            salt = Encoding.ASCII.GetBytes("Fd61ZgNVluChtzseyq9uMQ==");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        List<UserManager> _users = new List<UserManager>();
        public void UserAdd(UserManager user)
        {
            _users.Add(user);
        }
    }
}
