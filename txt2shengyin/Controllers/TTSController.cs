using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using txt2shengyin.Model;
namespace txt2shengyin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTSController : ControllerBase
    {
        // GET: api/test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/test/5
        [HttpGet("GetToken")]
        public string GetToken()
        {
            return AliMain.getToken();
        }
        [HttpGet("GetShengYin/{txt}")]
        public string GetShengYin(string txt)
        {
            AliTtsOption ao = new AliTtsOption();
            ao.text = txt;
            return AliTTSHelper.txt2ShengYin(ao);
        }
        // POST: api/test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
