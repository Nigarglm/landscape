using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Landscape.Areas.ViewModels.Service
{
    public class UpdateServiceVM
    {
        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(50, ErrorMessage = "En cox 50 element istifade oluna biler")]
        [MinLength(4, ErrorMessage = "En az 4 element istifade oluna biler")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [MaxLength(150, ErrorMessage = "En cox 150 element istifade oluna biler")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bu xana bos ola bilmez")]
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
