using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public sealed class UpdateUserModelValidator : UserModelValidator
    {
        public UpdateUserModelValidator()
        {
            RuleForId();
            RuleForName();
            RuleForSurname();
            RuleForEmail();
        }
    }
}
