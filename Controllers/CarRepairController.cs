using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PavlovLabs.Models;
using PavlovLabs.Storage;

namespace PavlovLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRepairController : ControllerBase
    {
      //  private static List<CarRepairData> _data = new List<CarRepairData>();
        private static IStorage<CarRepairData> _memCache =  new MemCache();
        // GET api/CarRepair
        [HttpGet]
        public ActionResult<IEnumerable<CarRepairData>> Get()
        {
            return Ok(_memCache.All);
        }

        // GET api/CarRepair/5
        [HttpGet("{id}")]
        public ActionResult<CarRepairData> Get(Guid id)
        {
            if (_memCache.Has(id)) return NotFound("No such");
            return Ok(_memCache[id]);
        }

        // POST api/CarRepair
        [HttpPost]
        public IActionResult Post([FromBody] CarRepairData value)
        {
             var validationResult = value.Validate();
             if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

             _memCache.Add(value);
            return Ok($"{value.ToString()} has been added");
        }

        // PUT api/CarRepair/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CarRepairData value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];

            _memCache[id] = value;
            return Ok($"{previousValue.ToString()}\n" +
                $"has been updated to\n" +
                $"{value.ToString()}");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_memCache.Has(id)) return NotFound("No such");
            var valueToRemove = _memCache[id];

            _memCache.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}