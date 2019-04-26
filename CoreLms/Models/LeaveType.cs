
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreLms.Models
{
    public class LeaveType
    {
        [Key]
        [Display(Name = "Leave Id")]
        public int Id { get; set; }

        [Display(Name = "Leave Type")]
        public string Name {get; set;}
        
        // ADD PROPERTIES HERE
    }
}
            