using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Repository.Model
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(16)]
        [Column("Role_Name")]
        public string Name { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
