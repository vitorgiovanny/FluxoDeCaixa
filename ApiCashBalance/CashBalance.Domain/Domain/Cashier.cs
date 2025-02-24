using CashBalance.Domain.Domain;

namespace CashBalance.Domain;

public class Cashier : Entity
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }


}