using DotNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Domain
{
    public sealed class Email : ValueObject
    {
        public Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEquals()
        {
            yield return Value;
        }
    }
}
