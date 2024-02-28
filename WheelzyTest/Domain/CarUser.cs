namespace Domain;

public class CarHistory: Entity
{
    public Car Car { get; set; }
    public User User { get; set; }
    public ProgressStatus Status { get; set; }
    public decimal Amount { get; set; }
    public bool Current { get; set; }
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; }
    public void DecreaseAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than 0");
        }
        if (amount > Amount)
        {
            throw new InvalidOperationException("Amount must be less than or equal to the current amount");
        }
        Amount -= amount;
    }
}

