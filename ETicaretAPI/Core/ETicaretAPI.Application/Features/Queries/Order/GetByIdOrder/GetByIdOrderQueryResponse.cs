﻿namespace ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryResponse
    {
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public object BasketItems { get; set; }
        public bool Completed { get; set; }
    }
}