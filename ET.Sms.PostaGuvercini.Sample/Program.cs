using ET.Sms.PostaGuvercini.Abstract;
using ET.Sms.PostaGuvercini.Concrete;
using ET.Sms.PostaGuvercini.Sample.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var section = builder.Configuration.GetSection("PostaGuverciniConfig");
builder.Services.Configure<PostaGuverciniConfig>(section);

builder.Services.AddHttpClient();
builder.Services.AddScoped<ISmsConfiguration, SmsConfiguration>();
builder.Services.AddScoped<ISmsService, PostaGuverciniSMSServiceAdapter>();

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
