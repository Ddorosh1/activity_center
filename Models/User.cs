using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace activity_center.Models
{
    public class User
    {
    [Key]
    public int UserId {get;set;}
    [Required(ErrorMessage = "First Name should be at least 2 characters!")]
    [MinLength(2)]
    public string FirstName {get;set;}

    [Required(ErrorMessage = "Last Name should be at least 2 characters")]
    [MinLength(2)]
    public string LastName {get;set;}

    [EmailAddress]
    [Required(ErrorMessage = "Please enter a valid email")]
    public string Email {get;set;}

    [Required(ErrorMessage = "Password should be at least 3 characters long")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase and 1 number")]
    [MinLength(3)]
    public string Password {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;

    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string Confirm {get;set;}

    //nav properties go here
    public List<Association> UserActivities { get; set; }
    }
}