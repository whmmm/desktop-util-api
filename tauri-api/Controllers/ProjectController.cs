using Microsoft.AspNetCore.Mvc;
using tauri_api.Domain.Entity;
using tauri_api.Service;

namespace tauri_api.Controllers;

[Controller]
[Route("[controller]")]
public class ProjectController : BaseController<ProjectEntity>
{
    
}