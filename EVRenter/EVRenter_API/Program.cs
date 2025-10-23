using EVRenter_Data;
using EVRenter_Repository.UnitOfWork;
using EVRenter_Service.Mapping;
using EVRenter_Service.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(UserMapping));

builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddAutoMapper(typeof(StationMapping));

builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddAutoMapper(typeof(ModelMapping));

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddAutoMapper(typeof(VehicleMapping));

builder.Services.AddScoped<IRentalPriceService, RentalPriceService>();
builder.Services.AddAutoMapper(typeof(PriceMapping));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddAutoMapper(typeof(BookingMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
