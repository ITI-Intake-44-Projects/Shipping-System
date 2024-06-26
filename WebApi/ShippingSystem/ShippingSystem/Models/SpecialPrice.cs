using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class SpecialPrice
    {
        public int Id { get; set; }

        public int? TransportCost { get; set;}

        public string? Governate {  get; set;}

        public string? City { get; set; }


        [ForeignKey("Merchant")]
        public string Merchant_Id { get; set; }

        public virtual Merchant? Merchant { get; set; }

        


    }


}
