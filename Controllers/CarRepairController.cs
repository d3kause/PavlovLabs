using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PavlovLabs.Models;

namespace PavlovLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRepairController : ControllerBase
    {
        private static List<CarRepairData> _data = new List<CarRepairData>();
        // GET api/CarRepair
        [HttpGet]
        public ActionResult<IEnumerable<CarRepairData>> Get()
        {
            return Ok(_data);
        }

        // GET api/CarRepair/5
        [HttpGet("{id}")]
        public ActionResult<CarRepairData> Get(int id)
        {
            if (id >= _data.Count) return NotFound("No such");
            return Ok(_data[id]);
        }

        // POST api/CarRepair
        [HttpPost]
        public IActionResult Post([FromBody] CarRepairData value)
        {
             var validationResult = value.Validate();
             if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

             _data.Add(value);
            return Ok($"{value.ToString()} has been added");
        }

        // PUT api/CarRepair/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CarRepairData value)
        {
            if (_data.Count <= id) return NotFound("No such");
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _data[id];

            _data[id] = value;
            return Ok($"{previousValue.ToString()}\n" +
                $"has been updated to\n" +
                $"{value.ToString()}");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_data.Count <= id) return NotFound("No such");
            var valueToRemove = _data[id];

            _data.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}