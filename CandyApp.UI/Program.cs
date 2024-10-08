using Blazored.LocalStorage;
using CandyApp.Application;
using CandyApp.Infrastructure;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add CandyApp services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(CancellationToken), serviceProvider => {
    IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    return httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//Add Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
