using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<GeneroRepository>();
builder.Services.AddScoped<OcupacionRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<EncuestaRepository>();
builder.Services.AddScoped<NivelEducativoRepository>();
builder.Services.AddScoped<RangoIngresoRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
