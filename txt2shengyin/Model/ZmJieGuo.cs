using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class ZmJieGuo
{

    public ZmJieGuo()
    {
        this.isOk = true;
    }
    public ZmJieGuo(bool isOKx=true,string msgx="" ,object datax=null)
    {
        this.isOk = isOKx;
        this.msg = msgx;
        this.data = datax;
    }
    public bool isOk { get; set; }
    public string msg { get; set; }
    public object data { get; set; }
    public ZmJieGuo Ok(string msg)
    {
        this.isOk = true;
        this.msg = msg;
        this.data = msg;
        return this;
    }
    public ZmJieGuo Error(string msg)
    {
        this.isOk = false;
        this.msg = msg;
        return this;
    }
}
