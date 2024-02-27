using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Repository.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(64)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Password { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedOn { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
    }
}
