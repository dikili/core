using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApplication.API.Models;

namespace CoreApplication.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    { 
        List<CoreTest> tests=new List<CoreTest>();
        CoreTest test=new CoreTest();
        public  ValuesController()
        {    
            test=new CoreTest{Id=3,Name="hello"};
           var test2=new CoreTest{Id=4,Name="mello"};
            tests.Add(test);
             tests.Add(test2);
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetValues()
        {
            
            // return new string[] { "value1", "value2" };
            return Ok(tests);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetValue(int Id)
        {
          var value=tests.FirstOrDefault(p=>p.Id==Id);

          return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
