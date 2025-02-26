using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashBalance.Domain.ObjectValue;

namespace CashBalance.Domain.Entities;
public class Cash : Entity
{
    public Money Amount { get; private set; }
    public Guid CashierId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? AtLastUpdate { get; private set; }
    
    // EF Relation
    public ICollection<Cashier> Cashiers { get; set; }
    public ICollection<Extract> Extracts { get; set; }
    public Cashier Cashier { get; set; }

    public Cash() { }

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