using ISS_TEST.Repository;
using ISS_TEST.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IMasterService, MasterService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Master/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Master}/{action=Index}/{id?}");

app.Run();