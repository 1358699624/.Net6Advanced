using Advanced.Net6.Interface;
using Advanced.Net6.Service;
using Advanced.Net6.WebApi.Controllers;
using EntityFormworkCore6.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Log4��־

builder.Logging.AddLog4Net("CfgFile/log4net.Config");
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region  swagger


builder.Services.AddSwaggerGen(c => {
    foreach (var item in typeof(ApiVersionInfo).GetFields())
    {
        c.SwaggerDoc(item.Name, new OpenApiInfo() { 
          Title= $"{item.Name} ����",
           Version = item.Name,
            Description = $".NET6 WebApi {item.Name} �汾"
        });
        
    }

    #region ΪSwagger JSON and UI����xml�ĵ�ע��·�� 
    string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
    string xmlPath = Path.Combine(basePath, "Advanced.NET6.WebApi.xml");
    c.IncludeXmlComments(xmlPath);
    #endregion

    #region Swaggerʹ�ü�Ȩ���
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
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

#region ��������
builder.Services.AddCors(
    c => {
   c.AddPolicy("CorsPolicy", opt => opt
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()
  .WithExposedHeaders("X-Pagination"));
    });

#endregion

#region  JWT����ܵ�

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),//��Կ��jwt����Ҫһ��
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5020",//jwt�ӿڶ˿�
        ValidateAudience = true,
        ValidAudience = "http://localhost:5029",//ʹ����Ŀ�˿�
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(60)//��֤�೤ʱ��
    };
});

#endregion



#region IOC
builder.Services.AddTransient<IBaseService, BaseService>();
builder.Services.AddTransient<ICommpayService, CommpayService>();
builder.Services.AddTransient<DbContext, CustomerDbContext>();
#endregion

#region Autofac

//builder.Services.AddTransient<ICommpayService, CommpayService>();
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
#region  ��Ȩ��Ȩ
app.UseAuthentication();
app.UseAuthorization();
#endregion

/// <summary>
/// ������
/// </summary>
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
