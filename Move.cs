using michaelsparty;

public class Move : Commands
{
    public int Steps { get; set; } 

    public Move()
    {
    }

    public Move(int steps) : this()  
    {
        Steps = steps;
    }

    public override void Execute(Character character)
    {
        Console.WriteLine($"Moving {Steps} steps");
        for (int i = 0; i < Steps; i++)
        {
            character.Move();  
        }
    }
}
