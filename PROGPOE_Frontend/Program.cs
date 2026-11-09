using Microsoft.AspNetCore.Authentication.Cookies;
using PROGPOE_Frontend.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<LoginApi>("RaceDay", client =>
{
    var baseUrl = builder.Configuration["RaceDayApi:BaseUrl"];

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpClient<EventApi>("RaceDay", client =>
{
    var baseUrl = builder.Configuration["RaceDayApi:BaseUrl"];

    client.BaseAddress = new Uri(baseUrl);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) //cookie based auth
    .AddCookie(options =>
    {
        options.Cookie.Name = "YourAppSessionCookie";
        options.Cookie.HttpOnly = true; // Prevents client-side script access
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requires HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // Mitigates CSRF
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session duration
        options.SlidingExpiration = true; // Renews session if user is active

        // Suppress automatic redirects to Login pages since this is an API
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(builder.Configuration["RaceDayApi:BaseUrl"])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Append("Permissions-Policy", "unload=*");
//    await next();
//});
app.UseStaticFiles();
app.UseHttpMethodOverride();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

