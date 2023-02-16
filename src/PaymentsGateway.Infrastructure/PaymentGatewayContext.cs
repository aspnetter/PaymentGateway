using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Infrastructure;

public class PaymentGatewayContext : DbContext
{
    public DbSet<Payment?> Payments { get; set; }

    public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
    {
    }

    public static void Init(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PaymentGatewayContext>(options =>
        {
            options.UseNpgsql(connectionString,
                postgresOptions =>
                    postgresOptions.MigrationsAssembly(typeof(PaymentGatewayContext).Assembly.FullName));
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var payment = builder.Entity<Payment>();

        payment.ToTable("Payment");
        payment.Property(o => o.Id).ValueGeneratedNever();
    }
}