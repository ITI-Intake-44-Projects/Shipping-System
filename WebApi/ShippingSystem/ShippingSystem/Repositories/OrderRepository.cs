using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Order;
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

        public async Task AddOrder(Order order)
        {
            var totalCost = 0;
            if(order == null || order.ProductOrders == null)
            {
                return;
            }
            totalCost += order.ProductOrders.Sum(p => p.UnitPrice * p.Quantity) ?? 0;
            if (order.VillageDeliver == true)
            {
                totalCost += db.VillageCosts.FirstOrDefault().Price ?? 0;
            }



        }
    }
}
