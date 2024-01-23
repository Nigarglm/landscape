using System.ComponentModel.DataAnnotations;

namespace Landscape.Areas.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Bu xana bos ola bilmez")]
        [MaxLength(25,ErrorMessage ="En cox 25 element istifade oluna biler")]
        [MinLength(4,ErrorMessage ="En az 4 element istifade olunmalidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(25, ErrorMessage = "En cox 25 element istifade oluna biler")]
        [MinLength(4, ErrorMessage = "En az 4 element istifade olunmalidir")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(25, ErrorMessage = "En cox 25 element istifade oluna biler")]
        [MinLength(4, ErrorMessage = "En az 4 element istifade olunmalidir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
