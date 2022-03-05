﻿using BasketApi.Entities;
using BasketApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet("{userName}",Name = "GetBasket")]
        public async Task<ActionResult> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket?? new ShoppingCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {

            return Ok(await _repository.UpdateBasket(basket));
        
        }

        [HttpDelete("{userName}",Name ="DeleteBasket")]
        public async Task<ActionResult> DeleteBasket(String userName)
        {

            await _repository.DeleteBasket(userName);
            return Ok();

        }



    }
}
