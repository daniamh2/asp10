using System.ComponentModel.DataAnnotations;

namespace project.DTOs.Requests
{
    public class CategoryRequest
    {
        [Required(ErrorMessage ="Name is required")]
        [MinLength(2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }

    }
}
