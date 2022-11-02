using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //https://stackoverflow.com/questions/40646312/asp-net-core-authorize-attribute-not-working-with-jwt

    public class CartDeleteController : ControllerBase
    {
        readonly ICartServices cartServices;
        readonly IBookServices bookServices;
        public readonly IMapper _mapper; // define the mapper

        public CartDeleteController(ICartServices cartServices, IBookServices bookServices, IMapper mapper)
        {
            this.cartServices = cartServices;
            this.bookServices = bookServices;
            _mapper = mapper;
        }

        // GET: api/<CartProcessController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CartProcessController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartProcessController>
        [HttpPost]
        public async Task Post([FromForm] int cartId)
        {
            string customersId = ((ClaimsIdentity)User.Identity)?.FindFirst("CustomersId").Value;

            if (!String.IsNullOrEmpty(customersId))
            {
                //compare cart if from it belongs customer
                var resultCart = await cartServices.GetCartByIdAsync(cartId);

                if(resultCart != null)
                {
                    if(resultCart.Id == cartId && resultCart.CustomersId == Convert.ToInt32(customersId))
                    {
                        //delete cart
                       await cartServices.DeleteCartAsync(resultCart);
                    }
                }
            }
        }

        // PUT api/<CartProcessController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartProcessController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
