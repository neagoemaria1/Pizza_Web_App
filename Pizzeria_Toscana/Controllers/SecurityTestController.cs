using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
            // Validate the ip parameter to avoid command injection
            if (string.IsNullOrWhiteSpace(ip) || !(System.Net.IPAddress.TryParse(ip, out _) || IsValidHostName(ip)))
                throw new ArgumentException("Invalid IP address or hostname.", nameof(ip));

            var commandToExecute = $"ping -c {ip}";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("ping", $"-c {ip}");
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
        // Hostname validation: allows letters, digits, dash, and dot, max length 253, label length 63, basic check
        private static bool IsValidHostName(string host)
        {
            if (string.IsNullOrWhiteSpace(host) || host.Length > 253)
                return false;
            var labels = host.Split('.');
            foreach (var label in labels)
            {
                if (label.Length == 0 || label.Length > 63)
                    return false;
                if (!System.Text.RegularExpressions.Regex.IsMatch(label, @"^[a-zA-Z0-9-]+$"))
                    return false;
                if (label.StartsWith("-") || label.EndsWith("-"))
                    return false;
            }
            return true;
        }
    }
}
