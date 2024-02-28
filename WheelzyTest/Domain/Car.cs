namespace Domain;

public class Car: Entity
{
    public int Year { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Submodel { get; set; }
    public ICollection<CarHistory> History { get; set; }
}