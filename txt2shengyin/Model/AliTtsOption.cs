using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace txt2shengyin.Model
{
    [Serializable]
    public class AliTtsOption
    {
        //        appkey String  是 应用appkey（获取方法请阅读创建项目一节）
        //text String  是 待合成的文本，需要为UTF-8编码。使用GET方法，需要再采用RFC 3986规范进行urlencode编码，比如加号 + 编码为 %2B；使用POST方法不需要urlencode编码。
        //token String  否 服务鉴权Token，获取方法请阅读获取访问令牌一节。若不设置token参数，需要在HTTP Headers中设置X-NLS-Token字段来指定Token。
        //format String  否 音频编码格式，支持的格式：pcm、wav、mp3，默认是pcm
        //sample_rate Integer 否   音频采样率，支持16000Hz、8000Hz，默认是16000Hz
        //voice   String 否   发音人，默认是xiaoyun，其他发音人名称请在简介中选择
        //volume  Integer 否   音量，范围是0 ~100，默认50
        //speech_rate Integer 否   语速，范围是-500~500，默认是0
        //pitch_rate  Integer 否   语调，范围是-500~500，可选，默认是0
        public string appkey=ZmConfig.AliAppKey;
        public string text;
        public string token;
        public string format = "mp3";
        /// <summary>
        /// 可选值16000Hz、8000Hz
        /// </summary>
        public int sample_rate = 16000;
        /// <summary>
        /// 可选值Xiaogang男声xiaoyun女声
        /// https://help.aliyun.com/document_detail/84435.html?spm=a2c4g.11174283.3.2.19da7275hVh3Ia
        /// </summary>
        public string voice = "xiaoyun";
        public int volume = 50;
        public int speech_rate = 0;
        public int pitch_rate = 0;
    }
}
