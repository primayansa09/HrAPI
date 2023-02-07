using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrAPI.Model
{
    public class Employees
    {
        [Key]    
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? Salary { get; set; }
        public string? Email { get; set; }
        public Gender? Gender { get; set; }
        // Self Reference of Table Employee - One To Many
        [ForeignKey("ManagerEmployees")]
        public string? Manager_Id { get; set; }
        [JsonIgnore]
        public virtual Employees? ManagerEmployees { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employees>? EmployeesOfManager { get; set; }
        //One manager have one departement-One To One
        //[JsonIgnore]
        [InverseProperty("Manager")]
        public virtual Departements Manager { get; set; }
        // Many employees have one departement - Many To One
        [ForeignKey("Departements")]
        public int? Departement_Id { get; set; }
        public virtual Departements Departements { get; set; }
        //[JsonIgnore]
        public virtual Accounts Accounts { get; set; }


    }

    public enum Gender
    {
        Male, Famale
    }
}
