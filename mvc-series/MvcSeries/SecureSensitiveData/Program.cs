using Microsoft.Extensions.Options;
using SecureSensitiveData.Configuration;
using SecureSensitiveData.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure EncryptionSettings from environment variable
builder.Services.Configure<EncryptionSettings>(settings =>
{
    var encryptionKey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
    if (string.IsNullOrEmpty(encryptionKey))
    {
        throw new InvalidOperationException("Encryption key is not set in environment variables.");
    }
    settings.EncryptionKey = encryptionKey;
});

// Register services
builder.Services.AddSingleton<IEncryptionService>(sp =>
{
    var options = sp.GetRequiredService<IOptions<EncryptionSettings>>().Value;
    return new EncryptionService(options.EncryptionKey);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
