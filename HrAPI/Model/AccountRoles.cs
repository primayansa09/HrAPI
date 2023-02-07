using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrAPI.Model
{
    public class AccountRoles
    {
        //public int Id { get; set; }
        [ForeignKey("Roles")]
        public int? RoleId { get; set; }
        [JsonIgnore]
        public virtual Roles? Roles { get; set; }

        [ForeignKey("Accounts")]
        public string? AccountNIK { get; set; }
        [JsonIgnore]
        public virtual Accounts? Accounts { get; set; }
        
    }
}
