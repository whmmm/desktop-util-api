using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using SqlSugar;
using tauri_api.Domain.Entity;
using tauri_api.Domain.Enum;
using tauri_api.Domain.Vo;
using tauri_api.Service;

namespace tauri_api.Controllers;

[Controller]
[Route("[controller]")]
public class ProjectController : BaseController<ProjectEntity>
{
    private readonly PipelineService _pipelineService;

    public ProjectController(PipelineService pipelineService)
    {
        _pipelineService = pipelineService;
    }

    public override ApiResult<List<ProjectEntity>> SelectAll(ProjectEntity query)
    {
        var exp = Expressionable.Create<ProjectEntity>();
        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            exp.And(it => it.Name.Contains(query.Name));
        }

        var list = Db.Queryable<ProjectEntity>()
            .Where(exp.ToExpression())
            .OrderByDescending(it => it.Sort)
            .OrderByDescending(it => it.Id)
            .ToList();

        return ApiResult<List<ProjectEntity>>.Success(list);
    }

    public override ApiResult<ProjectEntity> Add(ProjectEntity entity)
    {
        var hasFile = _pipelineService.HasFile(entity.DirPath);
        if (hasFile)
        {
            entity.State = ProjectStateEnum.Default;
        }
        else
        {
            entity.State = ProjectStateEnum.Primary;
        }

        return base.Add(entity);
    }
}