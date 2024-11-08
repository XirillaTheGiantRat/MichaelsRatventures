using michaelsparty;

public class Turn : Commands
{
    public string Direction { get; set; } 

    public Turn()
    {
    }

    public Turn(string direction) : this()  
    {
        Direction = direction;
    }

    public override void Execute(Character character)
    {
        Console.WriteLine($"Turning {Direction}");
        character.Turn(Direction); 
    }
}
