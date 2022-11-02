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
    public class ClassTypesAPIController : ControllerBase
    {
        readonly IClassTypesServices classTypesServices;
        public readonly IMapper _mapper; // define the mapper

        public ClassTypesAPIController(IClassTypesServices classTypesServices, IMapper mapper)
        {
            this.classTypesServices = classTypesServices;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<ClassTypesViewModel>> Get()
        {
            var result = _mapper.Map<List<ClassTypesViewModel>>(await classTypesServices.GetClassTypesAsync());

            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<int> Post([FromBody] ClassTypesViewModel classTypes)
        {
            //save to db
            int classTypesId = await classTypesServices.CreateClassTypesAsync(_mapper.Map<ClassTypes>(classTypes));

            return classTypesId;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
