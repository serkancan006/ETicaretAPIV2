using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService(
            IOrderWriteRepository orderWriteRepository,
            IOrderReadRepository orderReadRepository
        ) : IOrderService
    {

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            await orderWriteRepository.AddAsync(new()
            {
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderStatus = Domain.Enums.OrderStatusEnum.Pending,
                
            });
            await orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = orderReadRepository.Table.Include(o => o.Basket)
                      .ThenInclude(b => b.User)
                      .Include(o => o.Basket)
                         .ThenInclude(b => b.BasketItems)
                         .ThenInclude(bi => bi.Product);



            var data = query.Skip(page * size).Take(size);
            /*.Take((page * size)..size);*/


            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Where(o => o.OrderStatus != Domain.Enums.OrderStatusEnum.Completed).Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    OrderStatus = o.OrderStatus
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await orderReadRepository.Table
                                .Where(o => o.OrderStatus != Domain.Enums.OrderStatusEnum.Completed)
                                 .Include(o => o.Basket)
                                     .ThenInclude(b => b.BasketItems)
                                         .ThenInclude(bi => bi.Product)
                                         .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode,
                OrderStatus = data.OrderStatus
            };
        }

        public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
        {
            Order? order = await orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            if (order != null)
            {
                order.OrderStatus = Domain.Enums.OrderStatusEnum.Completed;
                orderWriteRepository.Update(order);
                return (await orderWriteRepository.SaveAsync() > 0, new()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CreatedDate,
                    Username = order.Basket.User.UserName,
                    EMail = order.Basket.User.Email
                });
            }
            return (false, null);
        }

        public async Task<Order> GetRealOrderByIdAsync(string id)
        {
            var data = await orderReadRepository.Table
                                .Where(o => o.OrderStatus != Domain.Enums.OrderStatusEnum.Completed)
                                .Include(ba => ba.OrderAddressBilling)
                                .Include(sa => sa.OrderAddressShipping)
                                 .Include(o => o.Basket)
                                    .ThenInclude(u => u.User)
                                 .Include(o => o.Basket)
                                     .ThenInclude(b => b.BasketItems)
                                         .ThenInclude(bi => bi.Product)
                                         .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return data;
        }
    }
}
