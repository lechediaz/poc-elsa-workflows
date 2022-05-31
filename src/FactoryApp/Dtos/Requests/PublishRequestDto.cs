using System.ComponentModel.DataAnnotations;

namespace FactoryApp.Dtos
{
    public class PublishRequestDto
    {
        public int RequestId { get; set; }

        [MaxLength(300)]
        [Required]
        public string ApproveLink { get; set; }

        [MaxLength(300)]
        [Required]
        public string RejectLink { get; set; }
    }
}