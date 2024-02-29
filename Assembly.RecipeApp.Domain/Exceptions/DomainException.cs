using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Domain.Exceptions
{
    internal class DomainException : Exception
    {
        public DomainException(string msg) : base(msg)
        { 
        }
    }
}
