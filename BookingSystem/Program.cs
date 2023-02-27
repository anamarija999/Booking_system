using BookingSystem.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Services;
using SimpleBookingSystem.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);
var cnn = new SqliteConnection("Filename=:memory:");
builder.Services.AddDbContext
<LocalDatabaseContext>(o => o.UseSqlite(cnn));
//builder.Services.AddDbContext<LocalDatabaseContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("SimpleBookingSystemDatabase")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookingInterface, BookingService>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Resources}/{action=Index}/{id?}");
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
});
AddResourceBookingData(app);
app.Run();
static void AddResourceBookingData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<LocalDatabaseContext>();

    var resource1 = new Resource
    {
        Id = 1,
        Name = "Computers",
        Quantity = 10
    };

    var resource2 = new Resource
    {
        Id = 2,
        Name = "Speakers",
        Quantity = 5
    };

    var resource3 = new Resource
    {
        Id = 3,
        Name = "Phones",
        Quantity = 15
    };

    var booking1 = new Booking
    {
        Id = 1,
        DateFrom =new DateTime(2023, 02, 2, 5, 10, 0, DateTimeKind.Utc),
        DateTo = new DateTime(2023, 02, 3, 6, 0, 0, DateTimeKind.Utc),
        BookedQunatity = 5,
        ResourceId = resource1.Id,
    };
  
    var booking2 = new Booking
    {
        Id = 2,
        DateFrom = new DateTime(2023, 02, 2, 5, 10, 20, DateTimeKind.Utc),
        DateTo = new DateTime(2023, 02, 3, 6, 0, 0, DateTimeKind.Utc),
        BookedQunatity = 5,
        ResourceId = resource2.Id,
    };

    var booking3 = new Booking
    {
        Id = 3,
        DateFrom = new DateTime(2023, 02, 2, 5, 10, 20, DateTimeKind.Utc),
        DateTo = new DateTime(2023, 02, 3, 6, 0,0, DateTimeKind.Utc),
        BookedQunatity = 5,
        ResourceId = resource3.Id,
    };

    db.Resources.Add(resource1);
    db.Resources.Add(resource2);
    db.Resources.Add(resource3);
    db.Bookings.Add(booking1);
    db.Bookings.Add(booking2);
    db.Bookings.Add(booking3);

    //db.SaveChanges();
}