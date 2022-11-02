using AutoMapper;
using BookstoreAPI.Model;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreAPI.API;

[Route("api/[controller]")]
[ApiController]
public class AuthorsAPIController : ControllerBase
{
    readonly IAuthorServices authorsServices;
    public readonly IMapper _mapper; // define the mapper

    public AuthorsAPIController(IAuthorServices authorsServices, IMapper mapper)
    {
        this.authorsServices = authorsServices;
        _mapper = mapper;
    }

    // GET: api/<AuthorsAPIController>
    [HttpGet]
    public async Task<IEnumerable<AuthorsViewModel>> Get()
    {
        var result = _mapper.Map<List<AuthorsViewModel>>(await authorsServices.GetAuthorsAsync());

        return result;
    }

    // GET api/<AuthorsAPIController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<AuthorsAPIController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<AuthorsAPIController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AuthorsAPIController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
