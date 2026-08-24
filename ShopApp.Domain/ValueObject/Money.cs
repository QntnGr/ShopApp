using ShopApp.Domain.Common;

namespace ShopApp.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(
        decimal amount,
        string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(
        decimal amount,
        string currency)
    {
        if (amount <= 0)
        {
            return Result<Money>.Failure(
                new Error(
                    "Money.InvalidAmount",
                    "Amount must be greater than zero."));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            return Result<Money>.Failure(
                new Error(
                    "Money.InvalidCurrency",
                    "Currency is required."));
        }

        return Result<Money>.Success(
            new Money(
                amount,
                currency.Trim().ToUpperInvariant()));
    }

    public static Money USD(decimal amount) =>
        new(amount, "USD");

    public static Money EUR(decimal amount) =>
        new(amount, "EUR");

    public Money Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException(
                "Quantity cannot be negative.",
                nameof(quantity));
        }

        return new Money(
            Amount * quantity,
            Currency);
    }

    public override string ToString() =>
        $"{Amount:0.00} {Currency}";
}