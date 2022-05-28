using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactoryApp.Models
{
    public class RawMaterial
    {
        #region Fields
        public int Id { get; set; }

        [MaxLength(120)]
        [Required]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        #endregion

        #region Relations
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
        #endregion
    }
}
