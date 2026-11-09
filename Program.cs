using RacedayFINAL.Services;
using RaceDayMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add MVC
builder.Services.AddControllersWithViews();

// 2. Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. Register HttpClient with a CookieContainer for API calls
builder.Services.AddSingleton<System.Net.CookieContainer>();
builder.Services.AddHttpClient<IApiService, ApiService>()
    .ConfigurePrimaryHttpMessageHandler(sp =>
    {
        var cookies = sp.GetRequiredService<System.Net.CookieContainer>();
        return new System.Net.Http.HttpClientHandler { CookieContainer = cookies, UseCookies = true };
    });
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Must be before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();