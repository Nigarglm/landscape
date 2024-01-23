using System.ComponentModel.DataAnnotations.Schema;

namespace Landscape.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
