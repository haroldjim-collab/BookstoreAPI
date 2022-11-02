using AutoMapper;
using BookstoreAPI.Model;
using BookstoreAPI.Utilities;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesAPIController : ControllerBase
    {
        readonly IPicturesServices picturesServices;
        public readonly IMapper _mapper; // define the mapper

        public PicturesAPIController(IPicturesServices picturesServices, IMapper mapper)
        {
            this.picturesServices = picturesServices;
            _mapper = mapper;
        }

        // GET: api/<PicturesAPIController>
        [HttpGet]
        public async Task<IEnumerable<PicturesViewModel>> Get()
        {
            var result = _mapper.Map<List<PicturesViewModel>>(await picturesServices.GetPicturesAsync());

            return result;
        }

        // GET api/<PicturesAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PicturesAPIController>
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromForm] PicturesViewModel pictures)
        {
            //save db
            int booksId = await picturesServices.CreatePicturesAsync(_mapper.Map<Pictures>(pictures));

            return StatusCode(StatusCodes.Status201Created);
        }


        // PUT api/<PicturesAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PicturesAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
