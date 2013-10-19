using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    public class User
    {
        public User()
        {

        }

        public User(Guid id, string userName)
        {
            UserId = id;
            UserName = userName;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
