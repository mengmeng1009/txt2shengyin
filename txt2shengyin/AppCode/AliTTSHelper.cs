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
    public static string txt2ShengYin(AliTtsOption ao)
    {
        ao.token = AliMain.getToken();
        string filename =  Guid.NewGuid().ToString() + ".mp3";
        string filepath = AppHelper.AppRootDir + "//upload//" + filename;
        string url = "https://nls-gateway.cn-shanghai.aliyuncs.com/stream/v1/tts";
        string jsondata = ao.ToJSON();
        bool jg= HttpHelper.post2Save(url, jsondata, filepath);
        return filename;
    }
}
