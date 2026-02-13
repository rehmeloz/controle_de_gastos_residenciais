using ControleGastosResidenciais.Server.Data;
using ControleGastosResidenciais.Server.Data.Repositories;
using ControleGastosResidenciais.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Builder categorias
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// Builder Pessoas
builder.Services.AddScoped<IPessoasRepository, PessoaRepository>();
builder.Services.AddScoped<IPessoasService, PessoasService>();

// Builder Transacoes
builder.Services.AddScoped<ITransacoesRepository, TransacaoRepository>();
builder.Services.AddScoped<ITransacoesService, TransacaoService>();

// Builder Relatorios
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DatabaseInitializer.Initialize();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
