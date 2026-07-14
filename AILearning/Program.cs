using AILearning.Extentions;
using Pgvector.Dapper;

Dapper.SqlMapper.AddTypeHandler(new VectorTypeHandler());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceExtenstions(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();