namespace CashBalance.Application.Dto
{
    public record class RemoveCashier
    {
        public Guid Id { get; }

        public RemoveCashier(Guid id)
        {
            Id = id;
        }
    }
}