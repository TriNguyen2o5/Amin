using Amin;
using Amin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký cấu hình OpenAiSettings
builder.Services.Configure<OpenAiSettings>(builder.Configuration.GetSection("OpenAI"));

// Đăng ký HttpClient và ChatGptService
builder.Services.AddHttpClient<ChatGptService>();


// Cấu hình DbContext cho SQL Server
builder.Services.AddDbContext<PatientManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));

// Cấu hình xác thực Cookie
builder.Services.AddAuthentication("UserAuth")
    .AddCookie("UserAuth", options =>
    {
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// Cấu hình Session với bộ nhớ tạm và thời gian hết hạn
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Đăng ký dịch vụ MVC với Controllers và Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt CORS
app.UseCors("AllowAllOrigins");

// Kích hoạt Session
app.UseSession();

// Kích hoạt Authentication và Authorization
app.UseAuthentication();
app.UseAuthorization();

// Cấu hình route cho khu vực Admin
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=UsersManagement}/{action=Index}/{id?}");

// Cấu hình route mặc định cho toàn bộ ứng dụng
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Định tuyến API Controller
app.MapControllers();

app.Run();
