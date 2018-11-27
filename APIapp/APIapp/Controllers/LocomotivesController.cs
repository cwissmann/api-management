using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocomotivesController : ControllerBase
    {
        // GET api/locomotives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locomotive>>> Get()
       {
            var result = await CosmosDBRepository<Locomotive>.GetItemsAsync();
            return new ActionResult<IEnumerable<Locomotive>>(result);
        }

        // GET api/locomotives/V200
        [HttpGet("{id}")]
        public async Task<ActionResult<Locomotive>> Get(string id)
        {
            var result = await CosmosDBRepository<Locomotive>.GetItemsAsync();
            return result.First(l => l.Baureihe == id);
        }

        // POST api/locomotives
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Locomotive locomotive)
        {
            await CosmosDBRepository<Locomotive>.CreateItemAsync(locomotive);
            return Ok();
        }

        // PUT api/locomotives/V200
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Locomotive locomotive)
        {
            await CosmosDBRepository<Locomotive>.UpdateItemAsync(id, locomotive);
            return Ok();
        }

        // DELETE api/locomotives/V200
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await CosmosDBRepository<Locomotive>.DeleteItemAsync(id);
            return Ok();
        }
    }
}