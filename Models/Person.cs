// Goes in Models
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditableListMvc.Models
{
    public class Person
    {
        public int PrimaryKey { get; set; }
        [Remote("CheckUserNameExists", "InvitationList", ErrorMessage = "Username already exists!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Cannot empty")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Datetime cannot be empty")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Cannot be null or empty")]
        [EmailAddress(ErrorMessage = "Email not in valid format")]
        public string Email { get; set; }
        public bool Remove { get; set; }
    }
}