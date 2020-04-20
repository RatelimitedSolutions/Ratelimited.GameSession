using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public class AuthModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Roles { get; set; }
    }
}
