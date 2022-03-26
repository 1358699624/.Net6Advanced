# .Net6Advanced

2022-3-26
今天写了WebApi+swagger
swagger这样的帮助文档,在之前.net5中使用过,但是过去了三个月,没练练手已经忘了
.net6自带swagger,但是需要配置xml,还有版本控制,版本控制在sqlsugar中没有使用,本次使用了
控制器中添加Authorize这个是鉴权授权
[ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]//这个在action上面添加即可

Jwt
整合API和JWT服务,存在问题JWT如果返回规范的像APIResult这样的类,返回为{},但是如果返回string类型的可以,应该是返回格式需要调整一下

跨域问题
解决方案
builder.Services.AddCors(
    c => {
   c.AddPolicy("CorsPolicy", opt => opt
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()
  .WithExposedHeaders("X-Pagination"));
    });
app.UseCors("CorsPolicy");

EF6
实现简单的CURD
下面代码可实现EFcore实体类和ContextDB跟数据库
Scaffold-DbContext "Data Source=.;Initial Catalog=AdvancedCustomerDB;User ID=sa;Password=sa123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.sqlServer -OutputDir Models -ContextDir Models -Context CustomerDbContext -Force



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

#region  JWT加入管道

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),//密钥跟jwt服务要一致
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5020",//jwt接口端口
        ValidateAudience = true,
        ValidAudience = "http://localhost:5029",//使用项目端口
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(60)//验证多长时间
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
