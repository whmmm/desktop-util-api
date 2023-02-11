using Microsoft.AspNetCore.Mvc;

namespace tauri_api.Controllers;

[Controller]
[Route("[controller]")]
public class ServerController : ControllerBase
{
    [HttpGet,Route("json")]
    public string Json()
    {
        return "hello world";
    }

    [HttpGet,Route("test")]
    public string Test()
    {
        return "test";
    }
}