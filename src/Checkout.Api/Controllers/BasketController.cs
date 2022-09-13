using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Resource;
using Checkout.Core.Models;
using Checkout.Core.Repository;
using Checkout.Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("baskets")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketArticleRepository _basketArticleRepository;
        private readonly BasketServices _basketServices;

        public BasketController(ILogger<BasketController> logger, IBasketRepository basketRepository, IBasketArticleRepository basketArticleRepository, BasketServices basketServices)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _basketArticleRepository = basketArticleRepository;
            _basketServices = basketServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Basket>> GetAll()
        {
            try
            {
                var baskets = _basketRepository.GetAll();
                _logger.LogInformation($"Returned all baskets from database.");
                return Ok(baskets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Basket.GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Basket> Get([FromRoute] int id)
        {
            try
            {
                var basket = _basketRepository.Get(id);
                if (basket == null)
                {
                    _logger.LogError($"Basket {id}, no found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned basket {id}");
                    return Ok(basket);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Basket.Get action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<BasketDTO> Post([FromBody] BasketDTO basketDTO)
        {
            try
            {
                if (basketDTO == null)
                {
                    _logger.LogError("Basket object is null.");
                    return BadRequest("Object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model object.");
                    return BadRequest(ModelState);
                }
                Basket b = new Basket
                {
                    Customer = basketDTO.Customer,
                    PaysVat = basketDTO.PaysVat,
                };
                _basketRepository.Create(b);

                basketDTO.Id = b.Id;
                return Ok(basketDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Basket.Create action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}/article-line")]
        public ActionResult<BasketArticle> Put([FromRoute] int id, [FromBody] BasketArticle basketArticle)
        {
            try
            {
                if (basketArticle == null)
                {
                    _logger.LogError("Article object is null.");
                    return BadRequest("Object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model object.");
                    return BadRequest(ModelState);
                }
                var basket = _basketRepository.Get(id);
                if (basket == null)
                {
                    _logger.LogError($"Basket {id}, not found.");
                    return NotFound();
                }

                basketArticle.BasketId = id;
                _basketArticleRepository.Update(basketArticle);

                _basketServices.UpdateColumns(basket);
                _basketRepository.Update(basket);

                return Ok(basketArticle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Basket.Put action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //not sure what exactly is { close: true, payed: true }
        [HttpPatch("{id:int}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<Basket> patchDoc)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var basket = _basketRepository.Get(id);
            //    if (basket != null)
            //    {
            //        patchDoc.ApplyTo(basket);

            //        return Ok(basket);
            //    }
            //    else
            //    {
            //        _logger.LogError($"Basket {id}, not found.");
            //        return NotFound();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Something went wrong inside Basket.Patch action: {ex.Message}");
            //    return StatusCode(500, "Internal server error");
            //}
        }
    }
}