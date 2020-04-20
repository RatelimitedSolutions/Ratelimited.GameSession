using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public class TokenModel
    {
        public TokenModel(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
