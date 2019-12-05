using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        #region 添加预置声音
        [HttpPost("AddShengYinByAdmin")]
        public int AddShengYinByAdmin(JsonElement json)
        {
            AliTtsOption ao = new AliTtsOption();
            ao.text =json.v;
            string sid= AliTTSHelper.txt2ShengYin(ao);
            ZmParameterList parameters = new ZmParameterList();
            parameters.AddParam("uid", "admin");
            parameters.AddParam("sid", sid);
            parameters.AddParam("txt", ao.text);
            return MySqlHelper.InsertTab("fanyijilu", parameters.ParamList);
        }
        #endregion
    }
}
