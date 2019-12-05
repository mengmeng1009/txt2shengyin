using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// 参数辅助类
/// sql语句里面用$_标识参数如$_code,
/// name上不需要$_参数标识
/// </summary>
[Serializable]
public class ZmParameter
{
   public string name;
    public object value;
}

public class ZmParameterList
{
    public void AddParam(string name,object value)
    {
        ParamList.Add(new ZmParameter() { name = name, value = value });
    }
    public List<ZmParameter> ParamList { get; } = new List<ZmParameter>();
}
