using Advanced.Net6.WebApi.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region  swagger


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

    #region Swagger使用鉴权组件
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
    #endregion
});
#endregion

#region 跨域问题
builder.Services.AddCors(
    c => {
   c.AddPolicy("CorsPolicy", opt => opt
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()
  .WithExposedHeaders("X-Pagination"));
    });

#endregion

#region  JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5020",
        ValidateAudience = true,
        ValidAudience = "http://localhost:5029",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(60)
    };
});

#endregion

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

//app.UseHttpsRedirection();
#region  鉴权授权
app.UseAuthentication();
app.UseAuthorization();
#endregion

/// <summary>
/// 跨域解决
/// </summary>
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
