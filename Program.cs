using instech_blazor_coding_task;
using instech_blazor_coding_task.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IDragService, DragService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IVesselLayoutService, VesselLayoutService>();
builder.Services.AddScoped<IRotationService, RotationService>();
builder.Services.AddScoped<IVesselTrackingService, VesselTrackingService>();

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] 
    ?? throw new InvalidOperationException("ApiSettings:BaseUrl is not configured");

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
