namespace ApiCredit.Domain.Entities
{
    public record class Money
    {
        public double Value { get; }
        public Money(double value)
        {
            if (value < 0)
                throw new InvalidOperationException("Money cannot be negative");
            Value = value;
        }
        
        public Money Add(Money other) => new(Value + other.Value);
    }
}