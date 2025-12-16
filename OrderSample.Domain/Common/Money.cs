using System;

namespace OrderSample.Domain.Common
{
    public class Money
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            return obj is Money other && Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}
