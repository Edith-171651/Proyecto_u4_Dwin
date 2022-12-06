using Microsoft.EntityFrameworkCore;
using WEB_API_ESCUELA.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<ESCUELA_PROYECTOContext>(options => options.UseSqlServer(@"Data Source=CSPSPI069589L\SQLEXPRESS; DataBase=ESCUELA_PROYECTO; user id=ESC_PROY.OWNER; password=Argentina1@;"));  //IMPORTANTE NO PONER DOBLE SLASH   //--<LENOVI
builder.Services.AddDbContext<ESCUELA_PROYECTOContext>(options => options.UseSqlServer(@"Data Source=DESKTOP-5TOLRIG\SQLEXPRESS; DataBase=ESCUELA_PROYECTO; user id=ESC_PROY.OWNER; password=Argentina1@;"));  //IMPORTANTE NO PONER DOBLE SLASH   //--< HP

builder.Services.AddCors(options => options.AddPolicy("CORSPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CORSPolicy"); //SE DEBE DE PONER ESTA POLICY, SINO MARCA ERROR 


app.UseAuthorization();

app.MapControllers();

app.Run();
