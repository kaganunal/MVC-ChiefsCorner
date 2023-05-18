using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models.Authentication.SignUp
{
    public class SignUpAppUser
    {
        [Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        public string Name { get; set; }
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        //[MinLength(8)]
        [Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RePassword { get; set; }

    }

    public enum Gender
    {
        MALE,
        FEMALE,
        OTHER
    }

}
