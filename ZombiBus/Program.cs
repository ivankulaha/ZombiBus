using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;
using ZombiBus.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ZombiDbContext>(o => o.UseSqlite("Data Source=Persistance/database.db"));
builder.Services.AddTransient<IDeadLetterConnectionRepository, DeadLetterConnectionRepository>();

var app = builder.Build();

var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ZombiDbContext>().Database.MigrateAsync();
scope.Dispose();

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

app.UseAuthorization();
app.MapRazorPages();

app.Run();