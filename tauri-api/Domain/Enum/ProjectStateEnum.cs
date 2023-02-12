namespace tauri_api.Domain.Enum;

public enum ProjectStateEnum
{
    /// <summary>
    /// 默认 灰色 
    /// </summary>
    Default = 0,

    /// <summary>
    /// 成功时的状态
    /// </summary>
    Success = 1,

    /// <summary>
    /// 警告状态
    /// </summary>
    Warning = 2,

    /// <summary>
    /// 报错时的状态
    /// </summary>
    Error = 3,

    /// <summary>
    /// 无 _deploy.yml 时的状态
    /// </summary>
    Primary = 4
}