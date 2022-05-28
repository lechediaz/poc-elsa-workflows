using System.Collections.Generic;

namespace FactoryApp.Dtos
{
    public class CreateRequestDto
    {
        public int CreatedById { get; set; }
        public int ReceiverId { get; set; }
        public IEnumerable<CreateRequestDetailDto> Details { get; set; }
    }
}