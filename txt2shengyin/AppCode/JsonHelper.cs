using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;

public static class JsonHelper
{
    /// <summary>
    /// 把对象转换为JSON字符串
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>JSON字符串</returns>
    public static string ToJSON(this object o)
    {
        if (o == null)
        {
            return null;
        }
        return JsonConvert.SerializeObject(o);
    }
    /// <summary>
    /// 对JsonElement对象的辅助扩展
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>JSON字符串</returns>
    public static T Value<T>(this JsonElement json,string key)
    {
        JsonElement j = json.GetProperty(key);
        switch (json.ValueKind)
        {
            case JsonValueKind.Undefined:
                return default(T);
            case JsonValueKind.Object:
                return j;
            case JsonValueKind.Array:
                return j;
            case JsonValueKind.String:
                return (T)(Object)j.GetString();
            case JsonValueKind.Number:
                return (T)(Object)j.GetDecimal();
            case JsonValueKind.True:
                return (T)(Object)true;
            case JsonValueKind.False:
                return (T)(Object)false;
            case JsonValueKind.Null:
                return default(T);
            default:
                return j;
        }
    }
}