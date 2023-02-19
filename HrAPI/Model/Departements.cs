using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrAPI.Model
{
    public class Departements
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        // One departement have one manager - One To One
        public string? Manager_Id { get; set; }
        [ForeignKey("Manager_Id"), JsonIgnore]
        public virtual Employees Manager { get; set; }
        // One departement have many employees - One To Many
        //[JsonIgnore]
        //[NotMapped]
        [InverseProperty("Departements"), JsonIgnore]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}