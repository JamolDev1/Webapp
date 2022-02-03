using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapp;

public class RegisterViewModel : IValidatableObject
{
    [Required(ErrorMessage = "To'liq ism-sharfini kiritish shart!")]
    [Display(Name = "Ism-sharf")]
    public string Fullname { get; set; }

    [Required(ErrorMessage = "Tug'ilgan kunni kiritish shart!")]
    [DisplayName("Tugilgan kun")]
    public DateTimeOffset Birthdate { get; set; }

    [Required(ErrorMessage = "Telefon raqamni kiritish shart!")]
    [RegularExpression(
        @"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$", 
        ErrorMessage = "Telefon raqam formati noto'g'ri.")]
    [DisplayName("Telefon raqam")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Email kiritish shart!")]
    [EmailAddress(ErrorMessage = "Email manzil formati noto'g'ri.")]
    [DisplayName("Email manzil")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Parol kiritish shart!")]
    [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo'lishi kerak.")]
    [DisplayName("Parol")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Parolni tasdiqlash shart!")]
    [Compare(nameof(Password), ErrorMessage = "Parol va Parolni Tasdiqlash mos kelmadi.")]
    [DisplayName("Parolni tasdiqlash")]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var now = DateTimeOffset.Now;
        var limit = new DateTime(now.Year - 18, now.Month, now.Day);
        Console.WriteLine($"{limit} {Birthdate}");
        if(Birthdate > limit)
        {
            yield return new ValidationResult($"You must be at least 13 years old!", new [] { nameof(Birthdate)});
        }
    }
}