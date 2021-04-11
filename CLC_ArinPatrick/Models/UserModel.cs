using System;
using System.ComponentModel.DataAnnotations;

namespace Minesweeper_ArinPatrick.Models
{
    public class UserModel
    {
        //ID is pk
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string First { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string Last { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please enter your gender.")]
        public string Gender { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Age")]
        [Range(0,200)]
        [Required(ErrorMessage = "Please enter your age.")]
        public int Age { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "Please enter state using shorthand. (ex. HI, AZ)")]
        [Required(ErrorMessage = "Please enter your state.")]
        public string State { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your Username.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your gender.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage="Password must be at least 5 characters.")]
        public string Password { get; set; }

        public string toString()
        {
            return "Name: " + Username + "Password: " + Password;
        }
    }
}
