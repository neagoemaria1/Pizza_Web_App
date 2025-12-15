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
            // Only allow valid IP addresses to avoid command injection
            if (string.IsNullOrWhiteSpace(ip) || !System.Net.IPAddress.TryParse(ip, out _))
                throw new ArgumentException("Invalid IP address.", nameof(ip));

            // Build command safely using ArgumentList to avoid injection
            ProcessStartInfo processStartInfo = new ProcessStartInfo("ping");
            processStartInfo.ArgumentList.Add("-c");
            processStartInfo.ArgumentList.Add("4"); // Number of pings to send; adjust if needed
            processStartInfo.ArgumentList.Add(ip);
            Process? process = Process.Start(processStartInfo);
            if (process != null)
            {
                process.WaitForExit();
            }
            return $"ping -c 4 {ip}";
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
