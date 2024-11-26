using BlogPost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogPost.Data;
using BlogPost.Areas.Identity.Data;
using BlogPost.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogPostIdentityDbContext>(options =>           
  options.UseSqlServer(builder.Configuration.GetConnectionString("BlogPostConnection")));

builder.Services.AddDefaultIdentity<BlogPostUser>()  //options => options.SignIn.RequireConfirmedAccount = false  if true then requires email confirmation.
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlogPostIdentityDbContext>(); 

builder.Services.AddRazorPages(); 
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogPostConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
      app.UseDeveloperExceptionPage();

      app.UseDeveloperExceptionPage();
      // app.UseMigrationsEndPoint(); with app.UseMigrationsEndPoint(), I don't have to manually run dotnet ef database update.
      app.UseSwagger();
      app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<BlogPostUser>();

app.MapStaticAssets();
app.MapControllerRoute(  
    name: "default",
    pattern: "{controller=Post}/{action=Index}")
    .WithStaticAssets();

app.MapRazorPages();

//Requested Middleware to log incoming requests
app.Use(async (context, next) => {
    Console.WriteLine(string.Format("Request Timestamp: {0} Path: {1} QueryString: {2}", DateTime.Now, context.Request.Path, context.Request.QueryString));
    await next.Invoke();
});
app.Run();
