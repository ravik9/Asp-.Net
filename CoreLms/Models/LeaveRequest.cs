
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLms.Models
{
    public class LeaveRequest
    {
        [Key]
        [Display(Name = "Leave Request")]
        public int Id { get; set; }
        
        // ADD PROPERTIES HERE
        [Required(ErrorMessage = "Please select Leave Type")]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId{get; set;}
        public LeaveType LeaveTypes { get; set; }
        
        [Required(ErrorMessage = "Please enter Reason for Leave Application")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Please select the employee")]
        [Display(Name = "Requestor")]
        public int RequestorId { get; set; }

        public Employee Requestor { get; set; }        

        [Required(ErrorMessage = "Please select Start Date")]
        [CustomValidation(typeof(LeaveRequest), "DateValidation")]
        [DataType(DataType.Date), Display(Name = "Leave Start Date")]
        public DateTime? StartDate {get; set;}
        
        [CustomValidation(typeof(LeaveRequest), "DateValidation2")]
        [Required(ErrorMessage = "Please select End Date")]
        [DataType(DataType.Date), Display(Name = "Leave End Date")]
        public DateTime? EndDate {get; set;}
        
        //ReadOnly
        [DataType(DataType.Date), Display(Name = "Request Date")]
        public DateTime RequestDate {get; set;}

        [DataType(DataType.Date), Display(Name = "Decision Date")]
        public DateTime? ApprRejDate {get; set;}

        [Display(Name = "Status")]
        public string RequestStatus {get; set;} 

        // [CustomValidation(typeof(LeaveRequest), "DaysValidation")]
        [Display(Name = "No of Days")]
        public int NoOfDays { get; set; }
        
        [NotMapped]
        public string PendingSince  {
            get { 
                    if(ApprRejDate==null) {
                        return $"{RequestDate.Date - DateTime.Today}";
                    }
                    return "0";
                }
        }
        public static ValidationResult DateValidation(DateTime StartDate, ValidationContext context) {
            var instance = context.ObjectInstance as LeaveRequest;
            if (instance == null) {
                return ValidationResult.Success;
            }
            
            var dbContext = context.GetService(typeof(AppDbContext)) as AppDbContext;
            Employee Emp = dbContext.Employee.Where(x => x.Id == instance.RequestorId).FirstOrDefault();
            int duplicateCount = dbContext.LeaveRequest.Where(x => x.RequestorId ==  instance.RequestorId)
                                                        .Where(x => instance.StartDate >= x.StartDate && instance.StartDate <= x.EndDate)
                                                        .Count();
            if (duplicateCount > 0) {
                return new ValidationResult("Leave record fot the dates given already exists");
            }
            if(Emp!=null && instance.StartDate <= Emp.DateOfJoining){
                return new ValidationResult("Start Date of Leave can be only after the employee Joining Date");

            }

            return ValidationResult.Success;
        }
        public static ValidationResult DateValidation2(DateTime EndDate, ValidationContext context) {
            var instance = context.ObjectInstance as LeaveRequest;
            if (instance == null) {
                return ValidationResult.Success;
            }
            var dbContext = context.GetService(typeof(AppDbContext)) as AppDbContext;
            int Present = dbContext.Leave.Where(x => x.EmployeeId == instance.RequestorId && x.LeaveTypeId == instance.LeaveTypeId).Select(x => x.Balance).FirstOrDefault();
            DateTime sd;
            DateTime ed;

            if(instance.StartDate != null){
                sd = instance.StartDate.Value;
            }
            if(instance.EndDate != null){
                ed = instance.EndDate.Value;
            }
            int requested = (instance.EndDate - instance.StartDate).Value.Days;
            if (Present < requested) {
                return new ValidationResult("Max Leaves Exceeded");
            }
            int duplicateCount = dbContext.LeaveRequest.Where(x => x.RequestorId ==  instance.RequestorId)
                                                        .Where(x => instance.EndDate >= x.StartDate && instance.EndDate <= x.EndDate)
                                                        .Count();
            if(instance.StartDate > instance.EndDate){
                return new ValidationResult("Please select End Date after the Start Date");
            }
            if (duplicateCount > 0) {
                return new ValidationResult("Leave record for the dates given already exists");
            }

            return ValidationResult.Success;
        }
        // public static ValidationResult DaysValidation(int NoOfDays, ValidationContext context) {
        //     var instance = context.ObjectInstance as LeaveRequest;
        //     if (instance == null) {
        //         return ValidationResult.Success;
        //     }
            
        //     var dbContext = context.GetService(typeof(AppDbContext)) as AppDbContext;
            
        //     return ValidationResult.Success;
        // }
    }
}
            