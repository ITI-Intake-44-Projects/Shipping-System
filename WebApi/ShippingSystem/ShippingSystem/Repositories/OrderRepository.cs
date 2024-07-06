using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Order;
using ShippingSystem.Enumerations;
using ShippingSystem.Models;

namespace ShippingSystem.Repositories
{
    public class OrderRepository : GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(ShippingContext _db ): base(_db) { }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            return await db.Orders
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerNameAsync(string name)
        {
            return await db.Orders.Where(o=>o.CustomerName == name).ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetMerchantOrdersAsync(string id )
        {
            return await db.Orders.Where(o => o.Merchant_Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetRepresentativeOrdersAsync(string id)
        {
            return await db.Orders.Where(r=> r.Representative_Id == id).ToListAsync();
        }

        public async Task<bool> AddOrder(Order order)
        {
            var shippingType = db.ShippingTypes.FirstOrDefault(sh => order.Shipping_Id == sh.Id);

            var totalCost = order.OrderCost + shippingType.AdditionalShippingValue; 

            var weightOptions = db.WeightOptions.FirstOrDefault();

            var merchant = db.Merchants.FirstOrDefault(m => m.Id == order.Merchant_Id);

            var city = db.Cities.FirstOrDefault(c=> c.Id == order.City_Id);

            if(order == null || order.ProductOrders == null)
            {
                return false ;
            }

            if (order.VillageDeliver == true)
            {
                totalCost += db.VillageCosts.FirstOrDefault().Price ?? 0;
            }

            if (order.TotalWeight > weightOptions.MaximumWeight)
            {
                totalCost += (order.TotalWeight - weightOptions.MaximumWeight) * weightOptions.AdditionalKgPrice;
            }

            if (order.Merchant.SpecialPrices != null)
            {
                var hasCity = order.Merchant.SpecialPrices.Any(sp=> sp.City_Id == order.City_Id);
                if(hasCity != false)
                {
                    totalCost = merchant.SpecialPrices.FirstOrDefault(sp => sp.City_Id == order.City_Id).TransportCost ?? 0;
                    order.TotalCost = totalCost;
                    await db.Orders.AddAsync(order);
                    await db.SaveChangesAsync();
                    return true;

                }
            }

            if (order.orderType == OrderTypeEnum.PickUp)
            {
                if (merchant.SpecialPickupCost != null || merchant.SpecialPickupCost != 0)
                {
                    totalCost += merchant.SpecialPickupCost ?? 0;
                    order.TotalCost = totalCost;

                    await db.Orders.AddAsync(order);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    totalCost += city.PickUpCost;
                    order.TotalCost = totalCost;
                    await db.Orders.AddAsync(order);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                totalCost += city.NormalCost;
                order.TotalCost = totalCost;
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return true;

            }
            return true;
        }


        public async Task<IEnumerable<Order>> FilterByStatusAndDate(OrderStatus status ,DateTime startDate ,DateTime endDate)
        {
            return await db.Orders.Where(o=> o.OrderStatus == status && o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
        }

        public async Task<IEnumerable<Order>> FilterByStatus(OrderStatus status)
        {
            return await db.Orders.Where(o => o.OrderStatus == status).ToListAsync();
        }
    }
}
