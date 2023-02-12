using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using tauri_api.Core;
using tauri_api.Core.type;
using tauri_api.Domain.Entity;
using tauri_api.Domain.Vo;

namespace tauri_api.Service;

[Produces("application/json")]
public class BaseController<T> : ControllerBase where T : BaseEntity, new()
{
    protected readonly SqlSugarScope Db = SqlSugarHelper.Db;


    [Route("add")]
    [HttpPost]
    public virtual ApiResult<T> Add([FromBody] T entity)
    {
        Db.Insertable(entity).ExecuteReturnIdentity();

        return ApiResult<T>.Success(entity);
    }

    [HttpGet, Route("all")]
    public virtual ApiResult<List<T>> SelectAll(ProjectEntity entity)
    {
        var list = Db.Queryable<T>()
            .Where(it => it.DeleteMark == false)
            .ToList();
        return ApiResult<List<T>>.Success(list);
    }


    // 下面的方法是 service 方法, 但是不知道如何多继承,
    // 只好和 Controller 代码放一起了
    [HttpGet]
    [Route("get")]
    public virtual ApiResult<T> GetById(int key)
    {
        return ApiResult<T>.Success(Db.Queryable<T>().First(it => it.Id == key));
    }

    [HttpPost]
    [Route("delete")]
    public virtual ApiResult<Integer> DeleteById(int key)
    {
        var i = new Integer(
            Db.Updateable<T>()
                .SetColumns("delete_mark", true)
                .ExecuteCommand()
        );

        return ApiResult<Integer>.Success(i);
    }
}