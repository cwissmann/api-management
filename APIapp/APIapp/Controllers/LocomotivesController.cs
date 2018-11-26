using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocomotivesController : ControllerBase
    {
        public ActionResult<IEnumerable<Locomotive>> Get()
        {
            var result = CosmosDBRepository<Locomotive>.GetItemsAsync();
            return new ActionResult<IEnumerable<Locomotive>>(result.Result);
        }
    }
}