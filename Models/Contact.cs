using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Imię' jest wymagane.")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane.")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Pole 'Email' jest wymagane.")]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    [Index(IsUnique = true)] // Unikalny email
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
    [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Hasło powinno spełniać podstawowe standardy złożoności.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane.")]
    public ContactCategory Category { get; set; }

    public ContactSubCategory? SubCategory { get; set; }

    [Required(ErrorMessage = "Pole 'Telefon' jest wymagane.")]
    [MaxLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Data urodzenia' jest wymagane.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}

public enum ContactCategory
{
    Służbowy,
    Prywatny,
    Inny
}

public enum ContactSubCategory
{
    Szef,
    Klient,
    Inna
}