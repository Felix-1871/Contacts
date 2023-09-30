using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contact
{
    [Key]
    public int ContactId { get; set; }

    [Required(ErrorMessage = "Pole 'Imię' jest wymagane.")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane.")]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Pole 'Email' jest wymagane.")]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Pole 'Hasło' jest wymagane.")]
    [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Hasło powinno spełniać podstawowe standardy złożoności.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public ContactCategory ContactCategory { get; set; }

    [ForeignKey("ContactCategory")]

    public int CategoryId { get; set; }
    public ContactSubCategory ContactSubCategory { get; set; }
    [ForeignKey("ContactSubCategory")]

    public int SubCategoryId { get; set; }

    [Required(ErrorMessage = "Pole 'Telefon' jest wymagane.")]
    [MaxLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Data urodzenia' jest wymagane.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}

public class ContactCategory
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane.")]
    [MaxLength(50)]
    public string Category { get; set; }
}

public class ContactSubCategory
{
    [Key]
    public int SubCategoryId { get; set; }

    [MaxLength(255)]
    public string SubCategory { get; set; }



}