
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLms.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        
        // ADD PROPERTIES HERE
        [Required(ErrorMessage = "Please provide a First Name")]
        [StringLength(100, MinimumLength=2, ErrorMessage = "First Name should contain minimum 2 to maximum 100 characters only")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MidName { get; set; }

        [Required(ErrorMessage = "Please provide a Last Name")]
        [StringLength(100, MinimumLength=2, ErrorMessage = "Last Name should contain minimum 2 to maximum 100 characters only")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // READONLY PROPERTIES
        [NotMapped]
        public string EmployeeName {
            // CONCATENATE THE FIRST,MIDDLE AND LAST NAME
            get { return $"{FirstName} {MidName} {LastName}"; }
        }

        [Required (ErrorMessage = "Please enter Gender of Employee(M/F/O)")]
        [CustomValidation(typeof(Employee), "GenderValidation")]
        public string Gender { get; set; }
        
        [Required(ErrorMessage = "Please provide Designation")]
        [StringLength(100, MinimumLength=2, ErrorMessage = "Please Enter Designation")]
        public string Designation { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please provide Date of Joining")]
        [Display(Name = "Date of Joining")]
        public DateTime DateOfJoining { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Last Day")]
        public DateTime? LastDay { get; set; }
        
        public bool IsSupervisor { get; set; }

        [NotMapped]
        public double Experience{
            //no.of months in the company
            get { if(LastDay == null){
                return (Math.Round(DateTime.Today.Subtract(DateOfJoining).Days / (365.0 / 12),2) + 1);
            }
            else{
               return (Math.Round(((DateTime)LastDay).Subtract(DateOfJoining).Days / (365.0 / 12),2) + 1);

            }
            }
        }   
        //Relationships
        [Display(Name = "Supervisor ID")] 
        [CustomValidation(typeof(Employee), "SupervisorValidation")]
        public int? SupervisorId { get; set; }

        public Employee Supervisor { get; set; }

        public ICollection<Employee> Supervisees { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public ICollection<Leave> Leaves{get; set;}
        

        //Validation
        public static ValidationResult SupervisorValidation(int? SupervisorId, ValidationContext context){
            if(SupervisorId==null){
                return ValidationResult.Success;
            }

            var instance = context.ObjectInstance as Employee;
            if(instance.IsSupervisor == true && instance.SupervisorId != null){
                return new ValidationResult("Employee cannot be both supervisor and Supervisee at the same time");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult GenderValidation(string Gender, ValidationContext context){
                if("M".Equals(Gender) || "F".Equals(Gender) || "O".Equals(Gender)){
                    return ValidationResult.Success;           
                }
            return new ValidationResult("Gender can be only M/F/O");         
        }
    }
}
            