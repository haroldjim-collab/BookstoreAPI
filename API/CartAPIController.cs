using AutoMapper;
using BookstoreAPI.Model;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //https://stackoverflow.com/questions/40646312/asp-net-core-authorize-attribute-not-working-with-jwt
    public class CartAPIController : ControllerBase
    {
        readonly ICartServices cartServices;
        readonly IBookServices bookServices;
        public readonly IMapper _mapper; // define the mapper

        public CartAPIController(ICartServices cartServices, IBookServices bookServices, IMapper mapper)
        {
            this.cartServices = cartServices;
            this.bookServices = bookServices;
            _mapper = mapper;
        }

        // GET: api/<CartAPIController>
        [HttpGet]
        public async Task<IEnumerable<CartViewModel>> Get()
        {
            string customersId = ((ClaimsIdentity)User.Identity)?.FindFirst("CustomersId").Value;

            if (!String.IsNullOrEmpty(customersId))
                return _mapper.Map<List<CartViewModel>>(await cartServices.GetCartByCustomerAsync(Convert.ToInt32(customersId)));

            return Array.Empty<CartViewModel>();
        }

        // GET api/<CartAPIController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {

            //console.log('feature commit');

            return $"value={id}";
        }

        // POST api/<CartAPIController>
        [HttpPost]
        public async Task<int> Post([FromForm] int booksId)
        {
            string customersId = ((ClaimsIdentity)User.Identity)?.FindFirst("CustomersId").Value;

            if (!String.IsNullOrEmpty(customersId))
            {
                //check if the book already exists at cart
                var customerCarts = await cartServices.GetCartByCustomerAsync(Convert.ToInt32(customersId));

                if (customerCarts != null)
                {
                    var resultCarts = customerCarts.Where(s => s.BooksId == booksId).FirstOrDefault();

                    if (resultCarts != null)
                    {
                        //update the cart quantity
                        resultCarts.Quantity++;

                        //save to db
                        await cartServices.UpdateCartAsync(_mapper.Map<Carts>(resultCarts));

                        return 0;
                    }
                    else
                    {
                        //insert to cart
                        var price = await bookServices.GetBooksAsync(Convert.ToInt32(booksId));

                        var cart = new Carts()
                        {
                            BooksId = booksId,
                            Quantity = 1,
                            CustomersId = Convert.ToInt32(customersId),
                            Price = price.Price ?? 0
                        };

                        //save to db
                        await cartServices.CreateCartAsync(_mapper.Map<Carts>(cart));
                    }
                }
            }

            return 1;
        }

        // PUT api/<CartAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartAPIController>/5
        [HttpPost("{id:int}")]
        public async Task Delete(int id)
        {
            await Task.Delay(1000);
        }
    }
}
