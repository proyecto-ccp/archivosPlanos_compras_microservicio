using Archivos.Dominio.Puertos.Integraciones;
using Archivos.Dominio.Servicios.Archivos;
using Archivos.Infraestructura.Adaptadores.Integraciones;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V.1.1.1",
        Title = "Servicio Archivos Planos",
        Description = "Cargue de planos para el modulo de compras"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
            Array.Empty<string>()
            }
        });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load("Archivos.Aplicacion")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Capa Infraestructura
builder.Services.AddScoped<IArchivoPlano, ArchivoPlano>();
builder.Services.AddHttpClient<IServicioProductosApi, ServicioProductosApi>();
//Capa Dominio - Servicios
builder.Services.AddTransient<LeerArchivo>();
builder.Services.AddTransient<CrearProducto>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

await app.RunAsync();
