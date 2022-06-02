using System.Collections.Generic;

namespace FactoryApp.Dtos
{
    public class EditRequestDto
    {
        public int Id { get; set; }
        public IEnumerable<EditRequestDetailDto> Details { get; set; }
    }
}