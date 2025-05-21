using BizFlow.Application.Extensions;
using BizFlow.IoC.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddIOCConfiguration(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global exception handler (put early to catch startup errors)
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // should be before Auth and Controllers

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();