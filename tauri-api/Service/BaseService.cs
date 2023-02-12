using SqlSugar;
using tauri_api.Core;
using tauri_api.Domain.Entity;

namespace tauri_api.Service;

public class BaseService<T> where T : BaseEntity, new()
{
    protected readonly SqlSugarScope db = SqlSugarHelper.Db;


    public T GetById(int key)
    {
        return db.Queryable<T>().First(it => it.Id == key);
    }

    public int DeleteById(int key)
    {
        return db.Updateable<T>()
            .SetColumns("delete_mark", true)
            .ExecuteCommand();
    }
}