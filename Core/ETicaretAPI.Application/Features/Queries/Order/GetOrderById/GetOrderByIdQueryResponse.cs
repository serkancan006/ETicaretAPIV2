﻿using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryResponse
    {
        public string Address { get; set; }
        public object BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}