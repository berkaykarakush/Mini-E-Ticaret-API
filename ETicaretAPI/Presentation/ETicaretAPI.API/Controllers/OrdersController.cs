﻿using ETicaretAPI.Application.Features.Commands.Order.CompleteOrder;
using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetAllOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            GetAllOrderQueryResponse response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest) 
        {
           CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetByIdOrderQueryRequest getByIdOrderQueryRequest)
        {
            GetByIdOrderQueryResponse response = await _mediator.Send(getByIdOrderQueryRequest);
            return Ok(response);
        }
        [HttpGet("complete-order/{Id}")]
        public async Task<IActionResult> CompleteOrder([FromRoute]CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }
    }
}
