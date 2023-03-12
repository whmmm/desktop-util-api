using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using tauri_api;
using tauri_api.Core;
using tauri_api.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Formatting = Formatting.Indented;
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "AllowOrigin",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

BuilderConfigure.Configure(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// init database table
SqlSugarHelper.Db.DbMaintenance.CreateDatabase();
SqlSugarHelper.Db.CodeFirst.InitTables(
    typeof(ServerEntity),
    typeof(ProjectEntity)
);

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
AppConfigure.Configure(app);
app.Run();