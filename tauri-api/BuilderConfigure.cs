using tauri_api.Service;

namespace tauri_api;

public class BuilderConfigure
{
    public static void Configure(WebApplicationBuilder builder)
    {
        AddBean(builder);
    }

    /// <summary>
    /// 自动注入
    /// </summary>
    /// <param name="builder"></param>
    private static void AddBean(WebApplicationBuilder builder)
    {
        builder.Services
               .AddScoped<PipelineService>()
               .AddScoped<CommandService>()
               .AddScoped<ProjectService>()
               .AddScoped<SocketMessageService>()
            ;
    }
}