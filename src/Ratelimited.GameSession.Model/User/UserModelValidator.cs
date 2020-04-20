using DotNetCore.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Model
{
    public abstract class UserModelValidator : Validator<UserModel>
    {
        public void RuleForAuth()
        {
            RuleFor(x => x.Auth).SetValidator(new AuthModelValidator());
        }

        public void RuleForEmail()
        {
            RuleFor(x => x.Email).NotEmpty();
        }

        public void RuleForId()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        public void RuleForName()
        {
            RuleFor(x => x.Name).NotEmpty();
        }

        public void RuleForSurname()
        {
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
