using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Domain.Value_Object
{
    public class Money
    {
        public double Value { get; private set; }
        public Money(double value)
        {
            if (value < 0)
                throw new InvalidOperationException("Money cannot be negative");
            Value = value;
        }

        public Money Add(Money other)
            => new(Value + other.Value);

        public void Add(double value)
            => Value += value;

        public void Subtract(double value)
        {
            Value = Value - value;
        }
    }
}
