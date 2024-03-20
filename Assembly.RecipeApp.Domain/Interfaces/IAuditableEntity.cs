using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Domain.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime createdTime { get; set; }
        DateTime modifiedTime { get; set; }
        User User { get; set; }
        
    }

}
