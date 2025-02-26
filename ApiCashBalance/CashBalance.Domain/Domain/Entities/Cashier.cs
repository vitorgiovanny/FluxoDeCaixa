
namespace CashBalance.Domain.Entities;

public class Cashier : Entity
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Extract> Extracts { get; set; }
    
}