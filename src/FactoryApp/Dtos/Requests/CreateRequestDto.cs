using System.Collections.Generic;

namespace FactoryApp.Dtos
{
    public class CreateRequestDto
    {
        public int AuthorId { get; set; }
        public int ApproverId { get; set; }
        public IEnumerable<CreateRequestDetailDto> Details { get; set; }
    }
}