using Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureDataBase(builder.Configuration);
builder.Services.ConfigureUnitOfWork();
builder.Services.ConfigureServices();
builder.Services.ConfigureIdentitySettings();
builder.Services.ConfigureJWTAuthencticationSettings(builder.Configuration);
builder.Services.ConfigureEmailConnectionSettings(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFilters();
builder.Services.ConfigureValidation();
builder.Services.ConfigureSwaggerSettings();

builder.Services.AddControllers()
	.AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "World of Books v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
