using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public DateTime Birthday { get; set; }

        public int Age { get; set; }
    }
}
