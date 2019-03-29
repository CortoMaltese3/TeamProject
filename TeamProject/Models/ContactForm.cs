using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class ContactForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        public SubjectSelector SubjectSelector { get; set; }
        [Required]
        [DisplayName("Body")]
        public string Body { get; set; }
    }

    public enum SubjectSelector
    {        
        [Display(Name = "New Branch Application")]
        NewBranchApplication,        
        [Display(Name = "Technical Issue")]
        TechnicalIssue,        
        [Display(Name ="General Infromation")]
        GeneralInformation
    };
}