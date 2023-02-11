using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using tauri_api.Core;
using tauri_api.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();