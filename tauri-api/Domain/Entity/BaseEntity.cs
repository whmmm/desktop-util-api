using SqlSugar;

namespace tauri_api.Domain.Entity;

public class BaseEntity
{
    /// <summary>
    /// 主键 id
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键")]
    public int Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(DefaultValue = "now", IsOnlyIgnoreUpdate = true, ColumnDescription = "创建时间")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnDescription = "修改时间", DefaultValue = "now", IsOnlyIgnoreUpdate = true)]
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 创建人 id
    /// </summary>
    [SugarColumn(ColumnDescription = "创建人 id", DefaultValue = "0", IsOnlyIgnoreUpdate = true)]
    public int CreatorId { get; set; } = 0;


    /// <summary>
    /// 删除标识, 默认 false
    /// </summary>
    [SugarColumn(ColumnDescription = "删除标识, 默认 false",
        DefaultValue = "false",
        IsOnlyIgnoreUpdate = true)]
    public bool DeleteMark { get; set; } = false;
}