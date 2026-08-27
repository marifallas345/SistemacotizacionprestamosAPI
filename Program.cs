using SistemacotizacionprestamosAPI.Data;
using SistemacotizacionprestamosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<GeneroRepository>();
builder.Services.AddScoped<HistorialCrediticioRepository>();
builder.Services.AddScoped<CategoriaPreguntaRepository>();
builder.Services.AddScoped<PreguntaRepository>();
builder.Services.AddScoped<RespuestaRepository>();
builder.Services.AddScoped<ReporteRepository>();
builder.Services.AddScoped<UsuarioRolRepository>();
builder.Services.AddScoped<OcupacionRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<EncuestaRepository>();
builder.Services.AddScoped<AuditoriaRepository>();
builder.Services.AddScoped<NivelEducativoRepository>();
builder.Services.AddScoped<RangoIngresoRepository>();
builder.Services.AddScoped<EstadoCotizacionRepository>();
builder.Services.AddScoped<CotizacionRepository>();
builder.Services.AddScoped<DetalleAmortizacionRepository>();
builder.Services.AddScoped<RangoEdadRepository>();
builder.Services.AddScoped<TipoPrestamoRepository>();
builder.Services.AddScoped<PlazoRepository>();
builder.Services.AddScoped<MontoRangoRepository>();
builder.Services.AddScoped<TasaInteresRangoRepository>();
builder.Services.AddScoped<CapacidadPagoRepository>();
builder.Services.AddScoped<MedioContratacionRepository>();
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
