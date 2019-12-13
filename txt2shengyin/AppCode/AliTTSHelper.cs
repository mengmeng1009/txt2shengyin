using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using txt2shengyin.Model;
/// <summary>
/// 调用阿里语音服务的帮助类
/// </summary>
public static class AliTTSHelper
{
    /// <summary>
    /// 文字转声音
    /// </summary>
    /// <returns>声音文件名称</returns>
    public static ZmJieGuo txt2ShengYin(AliTtsOption ao)
    {
        ZmJieGuo jg = new ZmJieGuo();
        string sid = getSid(ao.text);
        if (!string.IsNullOrEmpty(sid))
        {
            return jg.Ok(sid);
        }
        ao.token = AliMain.getToken();
        string filename = Guid.NewGuid().ToString() + ".mp3";
        string filepath = AppHelper.AppRootDir + "//upload//" + filename;
        string url = "https://nls-gateway.cn-shanghai.aliyuncs.com/stream/v1/tts";
        string jsondata = ao.ToJSON();
        ZmJieGuo jgs = HttpHelper.post2Save(url, jsondata, filepath);
        if (jgs.isOk)
        {
            return jg.Ok(filename);
        }
        else
        {
            return jgs;
        }
        
    }
    public static string getSid(string txt)
    {
        string sql_s = "select sid from fanyijilu where txt=$_txt ";
        ZmParameterList zm = new ZmParameterList();
        zm.AddParam("txt", txt);
        string sid = MySqlHelper.GetOneString(sql_s, zm.ParamList);
        return sid;
    }
}
