using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public sealed class AddUserModelValidator : UserModelValidator
    {
        public AddUserModelValidator()
        {
            RuleForName();
            RuleForSurname();
            RuleForEmail();
            RuleForAuth();
        }
    }
}
