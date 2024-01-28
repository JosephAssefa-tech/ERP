using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Models
{
  public  class PasswordResetToken: BaseAuditModel
    {
       public Guid UserId { get; set; }
       public string Token { get; set; }
       public DateTime TokenExpiry { get; set; }
        
        [ForeignKey(nameof(UserId))]
       public virtual User User { get; set; }
       public PasswordResetToken()
        { 
        }
    }
   
}
