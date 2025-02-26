﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Domain.Entities
{
    public class Cash : _Entity
    {
        public Money Amount { get; private set; }
        public Guid CashierId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? AtLastUpdate { get; private set; }

        public Cashier Cashier { get; private set; }

        public Cash(double amount, Guid cashierId)
        {
            Id = Guid.NewGuid();
            Amount = new Money(amount);
            CashierId = cashierId;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateAmount(double newAmount)
        {
            Amount = new Money(newAmount);
            AtLastUpdate = DateTime.UtcNow;
        }

    }
}
