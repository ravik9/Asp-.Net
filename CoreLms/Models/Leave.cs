
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreLms.Models
{
    public class Leave
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select Leave Type")]
        [Display(Name = "Leave Type")]

        public int LeaveTypeId{get; set;}
        
        // ADD PROPERTIES HERE
        public LeaveType LeaveTypes { get; set; }
        [Required(ErrorMessage = "Please Enetr Leave Balance")]
        [Display(Name = "Leave Balance")]
        [Range(0, 20)]
        public int Balance { get; set; }

        [Required(ErrorMessage = "Please select Employee")]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
            