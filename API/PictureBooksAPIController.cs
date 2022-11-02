using AutoMapper;
using BookstoreAPI.Model;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureBooksAPIController : ControllerBase
    {
        readonly IPictureBooksServices pictureBooksServices;
        public readonly IMapper _mapper; // define the mapper

        public PictureBooksAPIController(IPictureBooksServices pictureBooksServices, IMapper mapper)
        {
            this.pictureBooksServices = pictureBooksServices;
            _mapper = mapper;
        }

        // GET: api/<PictureBooksController>
        [HttpGet]
        public async Task<IEnumerable<PictureBooks>> Get()
        {
            var result = await pictureBooksServices.GetPictureBooksAsync();

            return result;
        }

        // GET api/<PictureBooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PictureBooksController>
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromForm] PictureBooksViewModel pictureBooks)
        {
            //save db
            int booksId = await pictureBooksServices.CreatePictureBooksAsync(_mapper.Map<PictureBooks>(pictureBooks));

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PictureBooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PictureBooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
