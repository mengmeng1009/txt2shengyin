using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


public static class HttpHelper
{
    /// <summary>
    /// 发起post请求，并将请求结果存储在制定的文件内
    /// </summary>
    /// <param name="url"></param>
    /// <param name="jsonData"></param>
    /// <param name="fileName"></param>
    /// <returns>成功</returns>
    public static ZmJieGuo post2Save(string url,string jsonData ,string filepath)
    {
        ZmJieGuo jg = new ZmJieGuo();
        try
        {
            //请求
            var myWebRequest = WebRequest.Create(url);
            myWebRequest.Method = "POST";
            myWebRequest.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            myWebRequest.ContentLength = data.Length;
            Stream newStream = myWebRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close(); 
            //接收
            var myWebResponse = myWebRequest.GetResponse();
            var ReceiveStream = myWebResponse.GetResponseStream(); 
            if (ReceiveStream != null)
            {
                //创建本地文件写入流
                Stream stream = new FileStream(filepath, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = ReceiveStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = ReceiveStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
            } 
            myWebResponse.Close();
        }
        catch (Exception ex)
        {
           Console.WriteLine("post2Save异常。。。“+" + ex.Message + "+”。。\r\n");
            return jg.Error(ex.Message);
        }
        return jg;
    }
}