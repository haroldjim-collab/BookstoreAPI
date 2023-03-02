using AutoMapper;
using BookstoreAPI.Model;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.PictureBinariesServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureBinariesController : ControllerBase
    {
        readonly IPictureBinariesServices pictureBinariesServices;
        public readonly IMapper _mapper; // define the mapper

        public PictureBinariesController(IPictureBinariesServices pictureBinariesServices, IMapper mapper)
        {
            this.pictureBinariesServices = pictureBinariesServices;
            _mapper = mapper;
        }

        // GET: api/<PictureBinariesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PictureBinariesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PictureBinariesController>
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromForm] PictureBinariesViewModel pictureBinaries)
        {
            //save db
            //int pictureBinariesId = await pictureBinariesServices.CreatePictureBinariesAsync(_mapper.Map<PictureBinaries>(pictureBinaries));

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PictureBinariesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PictureBinariesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
