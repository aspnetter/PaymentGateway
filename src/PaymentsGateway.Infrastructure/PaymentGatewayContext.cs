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
        payment.OwnsOne(o => o.TotalAmount, o =>
        {
            o.Property(o => o.Total).HasColumnName("Amount");

            o.OwnsOne(c => c.Currency, c =>
            {
                c.Property(o => o.Code).HasColumnName("Currency");
            });
        });
        
        payment.OwnsOne(o => o.Card, o =>
        {
            o.Property(o => o.OwnerName).HasColumnName("CardOwnerName");
            o.Property(o => o.Number).HasColumnName("CardNumber");
            
            o.OwnsOne(c => c.Expires, c =>
            {
                c.Property(x => x.Month).HasColumnName("CardExpiryMonth");
                c.Property(x => x.Year).HasColumnName("CardExpiryYear");
                c.Ignore(x => x.IsOverdue);
            });
            o.OwnsOne(c => c.CvvCode, c =>
            {
                c.Property(x => x.Code).HasColumnName("CardCvvCode");
            });
        });

        payment.OwnsOne(o => o.Result, o =>
        {
            o.Property(o => o.Status).HasColumnName("Status");
            o.Property(o => o.StatusCode).HasColumnName("StatusCode");
            o.Ignore(o => o.IsSuccessful);
        });
    }
}