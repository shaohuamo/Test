using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignWithGoogle.DbContext;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//    options.ForwardLimit = 2;
//    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
//    options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
//});

var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 3; //Eg: AB12AB (unique characters are A,B,1,2)
})

    //使用Entity framework存储data，并且通过ApplicationDbContext进行SQL操作
    .AddEntityFrameworkStores<AppDbContext>()

    //添加Default Token Providers
    //不一定使用，但是最好加上
    .AddDefaultTokenProviders()

    //configure repository layer level
    //由于无法manager class中直接使用DbContext，因此需要配置repository layer
    //由于配置了user store and Role store，因此无需创建custom classes for the store
    //assume that the repository will be generated automatically
    //and internally based on the data types that you mentioned here
    //GUID表示ApplicationUser中的ID的数据类型
    .AddUserStore<UserStore< IdentityUser<Guid>, IdentityRole<Guid>, AppDbContext, Guid>>()

    //for example while creating the user store we have mentioned the DB context name
    //so it internally creates an object of the DB context through dependency injection
    // and invokes the appropriate methods to perform the crude operations on the
    // users table I mean the repository accesses the DB context class internally
    //GUID表示ApplicationRole中的ID的数据类型
    .AddRoleStore<RoleStore<IdentityRole<Guid>, AppDbContext, Guid>>();

//builder.Services.AddAuthentication()
//    .AddGoogle(googleOptions =>
//        {
//            //googleOptions.ClientId = Configuration["Authentication:Google:ClientId"] ?? string.Empty;
//            //googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
//        });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().
        RequireAuthenticatedUser().Build();
    //enforces authorization policy (user must be authenticated)
    //for all the action methods

    options.AddPolicy("NotAuthorized", policy =>
    {
        policy.RequireAssertion(context =>
        {
            //返回值是Bool值
            //User表示当前登录的User
            //Identity表示当前登录的User的详细信息
            //IsAuthenticated=true表示当前User已登陆
            return !context.User.Identity.IsAuthenticated;
        });
    });
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/SignInWithGoogle";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
