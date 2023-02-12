namespace tauri_api.Domain.Vo;

public class ApiResult
{
    public int Code { get; set; } = 200;

    public string Msg { get; set; } = "";

    public static ApiResult Success(String msg = "操作成功")
    {
        return new ApiResult
        {
            Msg = msg
        };
    }

    public static ApiResult Error(int code = 400, string msg = "操作失败")
    {
        return new ApiResult
        {
            Msg = msg,
            Code = code
        };
    }
}

public class ApiResult<T> : ApiResult where T : class
{
    public T? Data { get; set; }

    public static ApiResult<T> Success(T data, String msg = "操作成功")
    {
        return new ApiResult<T>
        {
            Msg = msg,
            Data = data
        };
    }
}