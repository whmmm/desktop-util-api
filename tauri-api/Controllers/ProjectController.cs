using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using SqlSugar;
using tauri_api.Domain.Entity;
using tauri_api.Domain.Vo;
using tauri_api.Service;

namespace tauri_api.Controllers;

[Controller]
[Route("[controller]")]
public class ProjectController : BaseController<ProjectEntity>
{
    public override ApiResult<List<ProjectEntity>> SelectAll(ProjectEntity query)
    {
        var exp = Expressionable.Create<ProjectEntity>();
        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            exp.And(it => it.Name.Contains(query.Name));
        }

        var list = Db.Queryable<ProjectEntity>()
            .Where(exp.ToExpression())
            .ToList();

        return ApiResult<List<ProjectEntity>>.Success(list);
    }
}