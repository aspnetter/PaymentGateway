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
        payment.OwnsOne(o => o.TotalAmount, amount =>
        {
            amount.Property(o => o.Total).HasColumnName("Amount");

            amount.OwnsOne(c => c.Currency, c =>
            {
                c.Property(o => o.Code).HasColumnName("Currency");
            });
        });

        payment.OwnsOne(o => o.Card, card =>
        {
            card.Property(o => o.OwnerName).HasColumnName("CardOwnerName");
            card.Property(o => o.Number).HasColumnName("CardNumber");
            
            card.OwnsOne(o => o.Expires, date =>
            {
                date.Property(o => o.Month).HasColumnName("CardExpiryMonth");
                date.Property(o => o.Year).HasColumnName("CardExpiryYear");
                date.Ignore(o => o.IsOverdue);
            });
            
            card.OwnsOne(o => o.CvvCode, code =>
            {
                code.Property(o => o.Code).HasColumnName("CardCvvCode");
            });

            card.Ignore(o => o.MaskedDetails);
        });

        payment.OwnsOne(o => o.Result, status =>
        {
            status.Property(o => o.Status).HasColumnName("Status");
            status.Property(o => o.StatusReason).HasColumnName("StatusCode");
            status.Ignore(o => o.IsSuccessful);
        });
    }
}