using Tic_Tac_Toe.Data.Interfaces;
using Tic_Tac_Toe.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddPolicy("corspolicy", policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers();
builder.Services.AddScoped<IGameRepository, GameRepository>();

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

app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
