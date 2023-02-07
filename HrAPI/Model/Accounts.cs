using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrAPI.Model
{
    public class Accounts
    {
        [Key]
        [ForeignKey("Employees")]
        public string? NIK { get; set; }
        // One Account have one employee  - One To One
        public string? Password { get; set; }
        public virtual Employees Employees { get; set; }
        // Many to many
        //public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
