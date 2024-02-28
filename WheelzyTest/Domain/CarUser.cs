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
}