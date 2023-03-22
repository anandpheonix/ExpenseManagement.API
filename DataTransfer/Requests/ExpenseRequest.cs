#nullable disable

namespace DataTransfer.Requests;

public class ExpenseRequest
{
    public string Item { get; set; }
    public double Amount { get; set; }
    public int CategoryId { get; set; }
    public string Comment { get; set; }
}
