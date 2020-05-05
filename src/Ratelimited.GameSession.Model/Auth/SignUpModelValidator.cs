using DotNetCore.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public sealed class SignUpModelValidator: Validator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }   
    }
}
