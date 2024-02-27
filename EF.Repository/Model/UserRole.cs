using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Repository.Model
{
    [Table("UserRoleMapping")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [Column("Role_Id")]
        public int RoleId { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
