using AutoMapper;
using BookstoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedBooksAPIController : ControllerBase
    {
        readonly IBookServices bookServices;
        public readonly IMapper _mapper; // define the mapper

        public RelatedBooksAPIController(IBookServices bookServices, IMapper mapper)
        {
            this.bookServices = bookServices;
            _mapper = mapper;
        }

        // GET: api/<RelatedBooksAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RelatedBooksAPIController>/5
        [HttpGet("{classTypesId}")]
        public async Task<IEnumerable<BooksViewModel>> Get(int classTypesId)
        {
            var randomResult = _mapper.Map<List<BooksViewModel>>(await bookServices.GetRelatedBooksAsync(classTypesId));

            return randomResult;
        }

        // POST api/<RelatedBooksAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RelatedBooksAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RelatedBooksAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
