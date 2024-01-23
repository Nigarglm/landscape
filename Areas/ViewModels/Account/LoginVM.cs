using System.ComponentModel.DataAnnotations;

namespace Landscape.Areas.ViewModels.Account
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(25, ErrorMessage = "En cox 25 element istifade oluna biler")]
        [MinLength(4, ErrorMessage = "En az 4 element istifade olunmalidir")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }  
    }
}
