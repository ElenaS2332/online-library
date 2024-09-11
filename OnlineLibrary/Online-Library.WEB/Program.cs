using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain;
using Online_Library.Domain.Entities;
using Online_Library.Repository;
using Online_Library.Repository.Implementations;
using Online_Library.Repository.Interfaces;
using Online_Library.Service.Implementations;
using Online_Library.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));


var connectionString = builder.Configuration.GetConnectionString("OnlineLibraryDBConnectionString") 
                       ?? throw new InvalidOperationException("Connection string 'OnlineLibraryDBConnectionString' not found.");

var partnerConnectionString = builder.Configuration.GetConnectionString("PartnerDBConnectionString") ?? 
                          throw new InvalidOperationException("Connection string 'PartnerDatabase' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
        ));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Add other repositories and services
builder.Services.AddScoped<IBooksInReadingListRepository, BooksInReadingListRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IReadingListRepository, ReadingListRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IAuthorsService, AuthorsService>();
builder.Services.AddTransient<IBooksService, BooksService>();
builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IReadingListService, ReadingListService>();
builder.Services.AddTransient<ISubscriptionsService, SubscriptionsService>();
builder.Services.AddTransient<ITeamsService, TeamsService>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
