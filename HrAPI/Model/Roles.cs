using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HrAPI.Model
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
