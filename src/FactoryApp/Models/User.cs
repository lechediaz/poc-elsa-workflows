using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactoryApp.Models
{
    public class User
    {
        #region Fields
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [MaxLength(60)]
        [Required]
        public string Role { get; set; }

        public int? SupervisorId { get; set; }
        #endregion

        #region Relations
        public virtual User Supervisor { get; set; }
        public virtual ICollection<User> Subordinates { get; set; }
        public virtual ICollection<Request> RequestsCreated { get; set; }
        public virtual ICollection<Request> RequestsAsApprover { get; set; }
        #endregion
    }
}
