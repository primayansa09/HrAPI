using HrAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace HrAPI.ViewModel
{
    public class RegisterVm
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public int Role_Id { get; set; }
        public string AccountNIK { get; set; }
        public string Manager_Id { get; set; }
        public int Departement_Id { get; set; }
    }
}
