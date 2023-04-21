namespace DataTransfer.Objects;

public class TransactionDTO
{
    public int TransactionId { get; set; }
    public string Item { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}
