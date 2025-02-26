namespace ApiCredit.Domain.Entities
{
public class Cashier : _Entity
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }


}
}