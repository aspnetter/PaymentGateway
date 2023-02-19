using BankSimulator;
using BankSimulator.Interface;
using PaymentsGateway.Application;
using PaymentsGateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBankSimulator();

builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssemblies(
        typeof(Program).Assembly, 
        typeof(CreatePaymentCommand).Assembly, 
        typeof(MakePaymentCommand).Assembly
        );
});


PaymentGatewayContext.Init(builder.Services, builder.Configuration.GetConnectionString("Payments"));

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

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    serviceScope?.ServiceProvider.GetService<PaymentGatewayContext>()?.Database.EnsureCreated();
}

app.Run();

