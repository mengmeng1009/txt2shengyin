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
        public ZmJieGuo GetShengYin(string txt)
        {
            AliTtsOption ao = new AliTtsOption();
            ao.text = txt;
            return AliTTSHelper.txt2ShengYin(ao);
        }
        #region 添加声音
        private ZmJieGuo AddShengYin(string txt,string uid,string fenlei)
        {
            AliTtsOption ao = new AliTtsOption();
            ao.text =txt;
            ZmJieGuo jg = AliTTSHelper.txt2ShengYin(ao);
            if (jg.isOk)
            {
                ZmParameterList parameters = new ZmParameterList();
                parameters.AddParam("uid", uid);
                parameters.AddParam("sid", jg.data.ToString());
                parameters.AddParam("txt", ao.text);
                parameters.AddParam("fenlei", fenlei);
                int i_jg = MySqlHelper.InsertTab("fanyijilu", parameters.ParamList);
                if (i_jg != 1)
                {
                    return jg.Error("翻译成功，插入数据库失败");
                }
            }
            return jg;
        }
        #endregion
        #region 添加预置声音
        [HttpPost("AddShengYinByAdmin")]
        public ZmJieGuo AddShengYinByAdmin(JsonElement json)
        {
            string txt = json.Value<string>("txt");
            string fenlei= json.Value<string>("fenlei");
            return AddShengYin(txt, "admin", fenlei);
        }
        #endregion
        #region 添加自定义声音
        [HttpPost("AddShengYinByZdy")]
        public ZmJieGuo AddShengYinByZdy(JsonElement json)
        {
            #region 防止添加过多
            string jg = MySqlHelper.GetOneString("select count(*) from fanyijilu");
            if (int.Parse(jg)>1000)
            {
                return new ZmJieGuo(false, "<div style=\"height: 50vh;width:100%;padding: 5px;\"><div>服务器空间已满，赞助猛哥买个大个儿的存储空间吧</div><img src=\"./img/dashang.jpg\" style=\"width: 100%;\"></div>");
            }
            #endregion
            string fenlei = "zdy";
            string uid=json.Value<string>("uid");
            string txt = json.Value<string>("txt");
            return AddShengYin(txt, uid, fenlei);
        }
        #endregion
        #region 获取声音列表
        [HttpGet("GetShengYinList/{uid}")]
        public object GetShengYinList(string uid)
        {
            string sql_s = "select sid,txt,fenlei from fanyijilu where uid=$_uid or uid='admin' ";
            ZmParameterList parameters = new ZmParameterList();
            parameters.AddParam("uid", uid);
            return MySqlHelper.ExecuteDataTable(sql_s, parameters.ParamList).ToObj();
        }
        #endregion
    }
}
