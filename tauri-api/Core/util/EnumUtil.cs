using Microsoft.CSharp.RuntimeBinder;

namespace tauri_api.Core.util;

public static class EnumUtil
{
    /// <summary>
    /// 解析为枚举
    /// </summary>
    /// <param name="info"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Parse<T>(object info)
    {
        var enumType = typeof(T);

        return (T)Enum.Parse(enumType, info.ToString()!);
    }
}