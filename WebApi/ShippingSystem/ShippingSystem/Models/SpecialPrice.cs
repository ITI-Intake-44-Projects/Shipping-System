using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class SpecialPrice
    {
        public int Id { get; set; }

        public int? TransportCost { get; set;}

        //public string? Governate {  get; set;}

        //public string? City { get; set; }



        [ForeignKey("Governate")]
        public int? Governate_Id { get; set; }

        public virtual Governate Governate { get; set; }

        [ForeignKey("City")]
        public int? City_Id { get; set; }


        [ForeignKey("Merchant")]
        public string Merchant_Id { get; set; }

        public virtual Merchant? Merchant { get; set; }

        


    }


}
