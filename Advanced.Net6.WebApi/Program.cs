using Advanced.Net6.WebApi.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    foreach (var item in typeof(ApiVersionInfo).GetFields())
    {
        c.SwaggerDoc(item.Name, new OpenApiInfo() { 
          Title= $"{item.Name} 标题",
           Version = item.Name,
            Description = $".NET6 WebApi {item.Name} 版本"
        });
        
    }

    #region 为Swagger JSON and UI设置xml文档注释路径 
    string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
    string xmlPath = Path.Combine(basePath, "Advanced.NET6.WebApi.xml");
    c.IncludeXmlComments(xmlPath);
    #endregion
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
        {
            c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
