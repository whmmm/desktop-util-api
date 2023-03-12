using SqlSugar;
using tauri_api.Core;
using tauri_api.Domain.Entity;

namespace tauri_api.Service;

/// <summary>
/// 不推荐使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseService<T> where T : BaseEntity, new()
{
    protected readonly SqlSugarScope db = SqlSugarHelper.Db;


    public virtual T GetById(int key)
    {
        return db.Queryable<T>().First(it => it.Id == key);
    }

    public virtual int DeleteById(int key)
    {
        return db.Updateable<T>()
            .SetColumns("delete_mark", true)
            .ExecuteCommand();
    }
}