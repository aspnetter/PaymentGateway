// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentsGateway.Infrastructure;

#nullable disable

namespace PaymentsGateway.Infrastructure.Migrations
{
    [DbContext(typeof(PaymentGatewayContext))]
    partial class PaymentGatewayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaymentsGateway.Domain.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTimeUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("PaymentsGateway.Domain.Payment", b =>
                {
                    b.OwnsOne("PaymentsGateway.Domain.Amount", "TotalAmount", b1 =>
                        {
                            b1.Property<Guid>("PaymentId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Total")
                                .HasColumnType("numeric")
                                .HasColumnName("Amount");

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payment");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");

                            b1.OwnsOne("PaymentsGateway.Domain.IsoCurrency", "Currency", b2 =>
                                {
                                    b2.Property<Guid>("AmountPaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Code")
                                        .HasColumnType("text")
                                        .HasColumnName("Currency");

                                    b2.HasKey("AmountPaymentId");

                                    b2.ToTable("Payment");

                                    b2.WithOwner()
                                        .HasForeignKey("AmountPaymentId");
                                });

                            b1.Navigation("Currency")
                                .IsRequired();
                        });

                    b.OwnsOne("PaymentsGateway.Domain.CardDetails", "Card", b1 =>
                        {
                            b1.Property<Guid>("PaymentId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CardNumber");

                            b1.Property<string>("OwnerName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CardOwnerName");

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payment");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");

                            b1.OwnsOne("PaymentsGateway.Domain.Cvv", "CvvCode", b2 =>
                                {
                                    b2.Property<Guid>("CardDetailsPaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Code")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("CardCvvCode");

                                    b2.HasKey("CardDetailsPaymentId");

                                    b2.ToTable("Payment");

                                    b2.WithOwner()
                                        .HasForeignKey("CardDetailsPaymentId");
                                });

                            b1.OwnsOne("PaymentsGateway.Domain.ExpiryDate", "Expires", b2 =>
                                {
                                    b2.Property<Guid>("CardDetailsPaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Month")
                                        .HasColumnType("integer")
                                        .HasColumnName("CardExpiryMonth");

                                    b2.Property<int>("Year")
                                        .HasColumnType("integer")
                                        .HasColumnName("CardExpiryYear");

                                    b2.HasKey("CardDetailsPaymentId");

                                    b2.ToTable("Payment");

                                    b2.WithOwner()
                                        .HasForeignKey("CardDetailsPaymentId");
                                });

                            b1.Navigation("CvvCode")
                                .IsRequired();

                            b1.Navigation("Expires")
                                .IsRequired();
                        });

                    b.OwnsOne("PaymentsGateway.Domain.PaymentResult", "Result", b1 =>
                        {
                            b1.Property<Guid>("PaymentId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Status")
                                .HasColumnType("integer")
                                .HasColumnName("Status");

                            b1.Property<string>("StatusCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("StatusCode");

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payment");

                            b1.WithOwner()
                                .HasForeignKey("PaymentId");
                        });

                    b.Navigation("Card")
                        .IsRequired();

                    b.Navigation("Result")
                        .IsRequired();

                    b.Navigation("TotalAmount")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
