using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniversity.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext") ?? throw new InvalidOperationException("Connection string 'SchoolContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); //Install-Package AddDatabaseDeveloperPageExceptionFilter

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//////////////////////////////////////////////
//для более вменяемых Exception в базе:
else 
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
//////////////////////////////////////////////


//////////////////////////////////////////////
// для создания базы данных
using (IServiceScope scope = app.Services.CreateScope())
{ 
    IServiceProvider services = scope.ServiceProvider;
    SchoolContext context = services.GetRequiredService<SchoolContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}
/////////////////////////////////////////////


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
