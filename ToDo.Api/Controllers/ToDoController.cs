using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController: ControllerBase
    {
        ToDoManager _todoManager;
        public ToDoController(ToDoManager toDoManager)
        {
            _todoManager = toDoManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _todoManager.ToDoList.Select(td => td.ToString());
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            return _todoManager.ToDoList.Where(x=>x.Id==id).
                Select(td => td.ToString()).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
