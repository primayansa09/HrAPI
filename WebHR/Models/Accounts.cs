using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHR.Models
{
    public class Accounts
    {
        [Key]
        [ForeignKey("Employees")]
        public string? NIK { get; set; }
        // One Account have one employee  - One To One
        public string? Password { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
