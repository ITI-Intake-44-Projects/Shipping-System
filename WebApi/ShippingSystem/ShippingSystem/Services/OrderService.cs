using AutoMapper;
using ShippingSystem.DTOs.Order;
using ShippingSystem.Models;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork _unit , IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }


        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            var orders = await unit.OrderRepository.GetOrdersAsync(pageNumber, pageSize);

            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await unit.OrderRepository.GetById(id);
            return mapper.Map<OrderDto>(order);

        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerNameAsync(string name)
        {
            var orders = await unit.OrderRepository.GetOrdersByCustomerNameAsync(name);
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }


        public async Task<IEnumerable<OrderDto>> GetMerchantOrdersAsync (string merchantId)
        {
            var orders = await unit.OrderRepository.GetOrdersByCustomerNameAsync(merchantId);
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }


        public async Task<IEnumerable<OrderDto>> GetRepresentativeOrdersAsync(string Representativeid)
        {
            var orders = await unit.OrderRepository.GetOrdersByCustomerNameAsync(Representativeid);
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }


        public async Task PostOrderAsync(OrderDto OrderDto)
        {
            var Order = mapper.Map<Order>(OrderDto);

            await unit.OrderRepository.AddOrder(Order);
            

        }

        public async Task PutGovernateAsync(OrderDto OrderDto)
        {
            var governate = mapper.Map<Governate>(OrderDto);

            await unit.GovernateRepository.Update(governate);

            await unit.Save();
        }

        public async Task<bool> RemoveGovernateAsync(int id )
        {
            var governate = await unit.GovernateRepository.GetById(id);

            if (governate == null)
            {
                return false;
               
            }
            await unit.GovernateRepository.Delete(governate);
            await unit.Save();

            return true;

        }

        public Double CalcaulateCost(OrderDto orderDto)
        {
           
            return 1.1;
        }
    }
}
