using System.ComponentModel.DataAnnotations;

namespace ISS_TEST.Models
{
    public class MasterModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
    }
}
