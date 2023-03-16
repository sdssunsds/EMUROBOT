using Newtonsoft.Json;
//托管调试助手 "FatalExecutionEngineError"
//Message = 托管调试助手 "FatalExecutionEngineError":“运行时遇到了错误。此错误的地址为 0x35bc9719，在线程 0x6534 上。错误代码为 0xc0000005。此错误可能是 CLR 中的 bug，或者是用户代码的不安全部分或不可验证部分中的 bug。此 bug 的常见来源包括用户对 COM-interop 或 PInvoke 的封送处理错误，这些错误可能会损坏堆栈。”

namespace EMU.Util
{
    public class JsonManager
    {
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
