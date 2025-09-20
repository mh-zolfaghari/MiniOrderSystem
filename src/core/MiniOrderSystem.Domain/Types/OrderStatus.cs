namespace MiniOrderSystem.Domain.Types
{
    public enum OrderStatus : byte
    {
        PreInvoice = 0,
        Pending = 1,
        Paid = 2,
        Shipped = 3,
        Cancelled = 4
    }
}
