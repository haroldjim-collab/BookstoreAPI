using AutoMapper;
using BookstoreAPI.Model;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksAPIController : ControllerBase
    {
        readonly IBookServices bookServices;
        public readonly IMapper _mapper; // define the mapper

        public BooksAPIController(IBookServices bookServices, IMapper mapper)
        {
            this.bookServices = bookServices;
            _mapper = mapper;
        }

        // GET: api/<BooksAPIController>
        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<BooksViewModel>> Get()
        {
            var result = _mapper.Map<List<BooksViewModel>>(await bookServices.GetBooksAsync());

            return result;
        }

        // GET api/<BooksAPIController>/5
        [HttpGet("{id}")]
        public async Task<BooksViewModel> Get(int id)
        {
            var result = _mapper.Map<BooksViewModel>(await bookServices.GetBooksAsync(id));

            return result;
        }

        // POST api/<BooksAPIController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //https://stackoverflow.com/questions/40646312/asp-net-core-authorize-attribute-not-working-with-jwt
        public async Task<int> Post([FromForm] BooksInsertViewModel books)
        {
            //save to db
            int booksId = 12; // await bookServices.CreateBookAsync(_mapper.Map<Books>(books));

            //this is a quick hotfix 1 - test
            //this is a quick hotfix 11 - test

            return booksId;
        }

        // PUT api/<BooksAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Console.WriteLine(value);
        }

        // DELETE api/<BooksAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine(id);
        }
    }
}
