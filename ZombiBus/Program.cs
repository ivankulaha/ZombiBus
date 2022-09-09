using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;
using ZombiBus.Core.Azure;
using ZombiBus.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHangfire(configuration => configuration
     .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
     .UseSimpleAssemblyNameTypeSerializer()
     .UseRecommendedSerializerSettings()
     .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
     {
         CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
         SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
         QueuePollInterval = TimeSpan.Zero,
         UseRecommendedIsolationLevel = true,
         DisableGlobalLocks = true
     }));

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<ZombiDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddScoped<IDeadLetterConnectionRepository, DeadLetterConnectionRepository>();
builder.Services.AddSingleton<IDeadLetterListenerScheduler, DeadLetterListenerScheduler>();
builder.Services.AddScoped<IDeadLetterRepository, DeadLetterRepository>();
builder.Services.AddSingleton<IAzureServiceBusDeadLettersPuller, AzureServiceBusDeadLettersPuller>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapHangfireDashboard();
});
app.UseHangfireDashboard();

app.Run();