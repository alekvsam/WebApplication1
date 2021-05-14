using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IRabbitManager _manager;

        public TestController(IRabbitManager manager)
        {
            _manager = manager;
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // other opreation  

            // if above operation succeed, publish a message to RabbitMQ  

            var num = new System.Random().Next(9000);

            // publish message  
            _manager.Publish(new
            {
                field1 = $"Hello-{num}",
                field2 = $"rabbit-{num}"
            }, "demo.exchange.topic.dotnetcore", "topic", "*.queue.durable.dotnetcore.#");

            return new string[] { "value1", "value2" };
        }
    }
}
