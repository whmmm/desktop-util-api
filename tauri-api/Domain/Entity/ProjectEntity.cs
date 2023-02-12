using SqlSugar;
using tauri_api.Domain.Enum;

namespace tauri_api.Domain.Entity;

[SugarTable("project")]
public class ProjectEntity : BaseEntity
{
    public string Name { get; set; } = "";

    /// <summary>
    /// 本地文件路径
    /// </summary>
    public string DirPath { get; set; } = "";

    public string Intro { get; set; } = "";

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(DefaultValue = "0", IsOnlyIgnoreUpdate = true)]
    public int Sort { get; set; } = 0;

    /// <summary>
    /// 执行状态
    /// </summary>
    [SugarColumn(DefaultValue = "0", IsOnlyIgnoreUpdate = true)]
    public ProjectStateEnum State { get; set; } = ProjectStateEnum.Default;

    /// <summary>
    /// 执行时间
    /// </summary>
    [SugarColumn(DefaultValue = "0", IsOnlyIgnoreUpdate = true)]
    public long ExecTime { get; set; } = 0;
}