using CSharp.Advanced.BackgroundTasks;
using CSharp.Advanced.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<FactoryMiddleware>();
builder.Services.AddSingleton<SampleData>();
builder.Services.AddHostedService<BackgroundRefresh>();
builder.Services.AddInfrastructure();

var app = builder.Build();

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
app.Use(async (context, next) =>
{
    // Add code before request.
    await Console.Out.WriteLineAsync("I am creating a middleware");
    await next(context);

    // Add code after request.
});
app.UseMiddleware<FactoryMiddleware>();
app.UseMiddleware<ConventionMiddleware>();

app.MapGet("/messages", (SampleData data) =>  data.Data.Order() );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
