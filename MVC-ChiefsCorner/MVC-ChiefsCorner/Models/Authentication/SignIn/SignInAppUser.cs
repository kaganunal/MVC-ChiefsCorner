using System.ComponentModel.DataAnnotations;

namespace MVC_ChiefsCorner.Models.Authentication.SignIn
{
    public class SignInAppUser
    {
        //[Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        //public string Email { get; set; }
        [Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        public string Username { get; set; }
        //[MinLength(8)]
        [Required(ErrorMessage = "Bu bilginin girilmesi zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        //public bool IsEmailConfirmed { get; set; }
    }
}
