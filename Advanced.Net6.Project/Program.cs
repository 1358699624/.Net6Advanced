using Advanced.Net6.Interface;
using Advanced.Net6.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Advanced.Net6.Service.IOC;
using Advanced.Net6.Interface.IOC;

var builder = WebApplication.CreateBuilder(args);

{  
    //nuget

    builder.Logging.AddLog4Net("CfgFile/log4net.Config");
}
// Add services to the container.
builder.Services.AddControllersWithViews();

{
    builder.Services.AddSession();
}
#region ��Ȩ
    { 
    //ѡ��ʹ����Ȩ����
     builder.Services.AddAuthentication(option => {
         option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
     {
         option.LoginPath = "/Fourth/Login";//���û���ҵ��û���Ϣ---��Ȩʧ��--��ȨҲʧ����---����ת��ָ����Action
         option.AccessDeniedPath = "/Home/NoAuthorize";//ָ���ض�����ҳ
     });
    }
#endregion

#region ������Ȩ
{
    builder.Services.AddAuthorization(option =>
    {
        option.AddPolicy("rolePolicy", policyBuilder
         =>
        {
            //policyBuilder.RequireRole("Teacher"); 
            //policyBuilder.RequireClaim("Account");//�������ĳһ��Claim

            policyBuilder.RequireAssertion(context =>
            {
                bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                   && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin"
                   && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);

                //UserService userService = new UserService();
                ////userService.Validata(); 
                return bResult;
            });
        });
    });
}
#endregion

#region ע�����
    builder.Services.AddTransient<IMircPhone, MircPhone>();
#endregion

#region Autofac ���� ASP .NET  6
//�滻Mircsoft��Service.AddTransient
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerbuilder => {
    containerbuilder.RegisterType<MircPhone>().As<IMircPhone>();
    containerbuilder.RegisterType<Power>().As<IPower>();
    containerbuilder.RegisterType<Headphone>().As<IHeadphone>();

    containerbuilder.RegisterType<Phone>().As<IPhone>();
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//��Ȩ
app.UseAuthorization();//��Ȩ

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Fourth}/{action=Index}/{id?}");

app.Run();
