
using EmployeeApp.Data;
using EmployeeApp.Endpoints;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

static void OpenBrowser(string url)
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        Process.Start("xdg-open", url);
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        Process.Start("open", url);
    }
    else
    {
        throw new PlatformNotSupportedException("Operating system not supported for opening the browser.");
    }
}

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5001);
});

var connString = builder.Configuration.GetConnectionString("EmployeesStore");
builder.Services.AddSqlite<EmployeesDataContext>(connString);
var app = builder.Build();
await app.MigrateDbAsync();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapEmployeesEndpoints();
app.MapGet("/", (IWebHostEnvironment env) =>
{
    var webRoot = env.WebRootPath;
    var path = Path.Combine(webRoot, "index.html");
    if (File.Exists(path))
    {
        return Results.File(path, "text/html");
    }
    else
    {
        return Results.NotFound("index.html not found.");
    }
});

OpenBrowser("http://localhost:5001/");
app.Run();
