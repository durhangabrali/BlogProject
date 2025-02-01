using Blog.Root;

var builder = WebApplication.CreateBuilder(args);

CompositionRoot.InjectDependecies(builder.Services);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{action=Index}/{id?}"
);

app.Run();
