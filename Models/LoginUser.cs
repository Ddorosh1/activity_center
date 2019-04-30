using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace activity_center
{
public class LoginUser
{
    [Required(ErrorMessage = "Invalid Email")]
    [Display(Name = "Email")]
    public string LoginEmail {get; set;}

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Invalid Password")]
    public string LoginPassword { get; set; }
}
}