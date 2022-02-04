using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModel;

public class LoginViewModel 
{
    [Required(ErrorMessage = "Email kiritish shart!")]
    [EmailAddress(ErrorMessage = "Email manzil formati noto'g'ri.")]
    [DisplayName("Email manzil")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Parol kiritish shart!")]
    [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo'lishi kerak.")]
    [DisplayName("Parol")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}