using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace tauri_api.Core.util;

public static class JsonUtil
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented
    };

    /// <summary>
    /// 转换为 json
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static string ToJson(object o)
    {
        return JsonConvert.SerializeObject(o, Settings);
    }
}