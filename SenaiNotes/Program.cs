using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SenaiNotesContext>();
builder.Services.AddTransient<IUsuariorepository, UsuarioRepository>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options => // Faz o Swagger abrir direto
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

app.UseAuthentication(); 

app.UseAuthorization(); 

app.Run();
