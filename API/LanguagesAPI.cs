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
    public class LanguagesAPI : ControllerBase
    {
        readonly ILanguageServices languagesServices;
        public readonly IMapper _mapper; // define the mapper

        public LanguagesAPI(ILanguageServices languagesServices, IMapper mapper)
        {
            this.languagesServices = languagesServices;
            _mapper = mapper;
        }

        // GET: api/<LanguagesAPI>
        [HttpGet]
        public async Task<IEnumerable<LanguagesViewModel>> Get()
        {
            var result = _mapper.Map<List<LanguagesViewModel>>(await languagesServices.GetLanguagesAsync());

            return result;
        }

        // GET api/<LanguagesAPI>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LanguagesAPI>
        [HttpPost]
        public async Task<int> Post([FromForm] LanguagesViewModel languages)
        {
            //save to db
            int languagesId = await languagesServices.CreateLanguageAsync(_mapper.Map<Languages>(languages));

            return languagesId;
        }

        // PUT api/<LanguagesAPI>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LanguagesAPI>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
