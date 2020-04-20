using DotNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratelimited.GameSession.Domain
{
    public class FullName: ValueObject
    {
        public FullName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }

        protected override IEnumerable<object> GetEquals()
        {
            yield return Name;
            yield return Surname;
        }
    }
}
