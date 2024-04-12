using Assembly.RecipeApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.RecipeApp.Domain.Interfaces
{
    public interface IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class AuditableEntity : IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        private string _updatedBy { get; set; }
        public string UpdatedBy
        {
            get { return _updatedBy; }
            set
            {
                _updatedBy ??= value;
            }
        }
        private DateTime _updatedDate { get; set; }
        public DateTime UpdatedDate 
        {
            get { return _updatedDate; }
            set
            {
                if (_updatedDate == default(DateTime))
                {
                    _updatedDate = CreatedDate;
                }
            }
        }

        public void SetUpdatedDate()
        { 
            _updatedDate = DateTime.Now; 
        }

        public void SetUpdatedBy(User user)
        {
            UpdatedBy = user.Username;
        }

    }

}
