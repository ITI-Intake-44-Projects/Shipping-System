using ShippingSystem.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class Order
    {

        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPhone1 { get; set; }

        public string? CustomerPhone2 { get; set; }

        public string? CustomerEmail { get; set; }

        public string? Governate { get; set; }

        public string? City { get; set; }

        public string? VillageOrStreet { get; set; }

        public bool? VillageDeliver { get; set; }

        public int? OrderCost { get; set; }

        public int? TotalWeight { get; set;}

        public string? Notes {  get; set; }

        public string? MerchantMobile {  get; set; }

        public string? MerchantAddress { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public int? TotalCost {  get; set; }

        public int? ShippingCost {  get; set; }

        public DateTime? OrderDate {  get; set; }

        [ForeignKey("OrderType")]
        public int? OrderType_Id { get; set; }

        public virtual OrderType? OrderType { get; set; }

        [ForeignKey("Branch")]
        public int? Branch_Id { get; set; }

        public virtual Branch? Branch { get; set; }

        [ForeignKey("PaymentType")]
        public int? Payment_Id { get; set; }

        public virtual PaymentType? PaymentType { get; set; }


        [ForeignKey("ShippingType")]
        public int? Shipping_Id { get; set; }

        public virtual ShippingType? ShippingType { get; set; }


        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();



    }
}
