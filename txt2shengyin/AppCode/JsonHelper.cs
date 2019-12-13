using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        switch (j.ValueKind)
        {
            case JsonValueKind.Undefined:
                return default(T);
            case JsonValueKind.Object:
                return (T)(Object)j;
            case JsonValueKind.Array:
                return (T)(Object)j; 
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
                return (T)(Object)j;
        }
    }
    #region 将DataTable转为可序列化的对象
    /// <summary>
    /// 不需要分页
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="flag">false</param>
    /// <returns></returns>
    public static object ToObj(this DataTable dt)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                result.Add(dc.ColumnName, dr[dc].ToString().Trim());
            }
            list.Add(result);
        }
        return list;
    }
    #endregion
}