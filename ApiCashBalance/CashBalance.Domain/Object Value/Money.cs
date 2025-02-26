namespace CashBalance.Domain.ObjectValue;

    public record class Money
    {
        public double Value { get; }

        public Money(double value)
        {
            if (value < 0)
                throw new ArgumentException("The value isn't valid.");

            Value = value;
        }

        public Money Add(Money other) 
            => new(Value + other.Value);

        public Money Subtract(Money other)
            => Subtract(other.Value);

        public Money Subtract(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("This is values is invalid.");

            double newValue = Value - amount;

            if (newValue < 0)
                throw new InvalidOperationException("The Amount don't negative.");

            return new(newValue);
        }
    }