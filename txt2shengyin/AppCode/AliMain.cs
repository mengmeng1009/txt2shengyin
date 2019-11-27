using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json.Linq;
using System;

/// <summary>
/// 处理一些阿里云接口的核心操作
/// </summary>
public static class AliMain
{
    public static long tokenExpireTime = DateTime.Now.Ticks/1000;//token的过期时间
    private static string tokenstr = "";
    public static string getToken()
    {
        if (DateTime.Now.Ticks/1000< tokenExpireTime)
        {
            return tokenstr;
        }
        string token = "";
        IClientProfile profile = DefaultProfile.GetProfile(
        "cn-shanghai",
        ZmConfig.AliAccessKeyID,//"<您的AccessKey Id>",
        ZmConfig.AliAccessKeySecret//"<您的AccessKey Secret>"
        );
        DefaultAcsClient client = new DefaultAcsClient(profile);
        //try
        //{
        // 构造请求
        //Aliyun.Acs.Core.RpcAcsRequest
        CommonRequest request = new CommonRequest();
        request.Domain = "nls-meta.cn-shanghai.aliyuncs.com";
        request.Version = "2019-02-28";
        // 因为是 RPC 风格接口，需指定 ApiName(Action)
        request.Action = "CreateToken";
        // 发起请求，并得到 Response
        CommonResponse response = client.GetCommonResponse(request);
        token = response.Data;
        System.Console.WriteLine(token);
        JObject jo = null;
        jo = JObject.Parse(token);
        if (jo == null || jo["Token"] == null)
        {
            return "获取token失败";
        }
        tokenExpireTime = jo["Token"].Value<long>("ExpireTime");
        tokenstr = jo["Token"].Value<string>("Id");
        #region 结果
        //{ "NlsRequestId":"31eb4e65ad1042238a7f8b8492ea1264",
        //    "ErrMsg":"",
        //    "RequestId":"CC55D80C-4D25-432F-AA31-B25C14A09BFD",
        //    "Token":{ 
        //        "ExpireTime":1574825788,
        //        "Id":"1e3011bbd3c346c8a8bb2a6fe9510bdf",
        //        "UserId":"1013107310890239"} 
        //}
        #endregion
        //}
        //catch (ServerException ex)
        //{
        //    System.Console.WriteLine(ex.ToString());
        //}
        //catch (ClientException ex)
        //{
        //    System.Console.WriteLine(ex.ToString());
        //}
        return tokenstr;
    }
}
