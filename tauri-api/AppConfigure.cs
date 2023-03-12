namespace tauri_api;

public class AppConfigure
{
    public static void Configure(WebApplication app)
    {
        app.UseWebSockets();
    }
}