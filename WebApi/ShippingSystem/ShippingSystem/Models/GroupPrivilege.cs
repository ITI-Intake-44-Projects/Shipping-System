using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class GroupPrivilege
    {
        [ForeignKey("Group")]
        public string? Group_Id { get; set; }

        [ForeignKey("Privilege")]
        public int Privelege_Id {  get; set; }
        
        public virtual Privilege? Privilege { get; set; }

        public virtual Group? Group { get; set; }



    }
}
