using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pizzeria_Toscana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityTestController : ControllerBase
    {
        // GET: api/<SecurityTestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SecurityTestController>/5
        // GET api/SecurityTest/s
        [HttpGet("{ip}")]
        public string Get(string ip)
        {
            var commandToExecute = "ping -c " + ip;
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "-c \"" + commandToExecute + "\"");
            Process? process = Process.Start(processStartInfo);
            if (process != null)
            {
                process.WaitForExit();
            }
            return commandToExecute;
        }

        // POST api/<SecurityTestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SecurityTestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SecurityTestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
