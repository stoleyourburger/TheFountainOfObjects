// Create a 4x4 room.
// Moving from room to room
// Most rooms are empty, except: 1) cavern entrance 2) fountain room with the fountain.
// go to fountain, activate and go back, then you win. 
FountainOfObjectsGame game = new FountainOfObjectsGame();
game.Run();

public class FountainOfObjectsGame
{
    public void Run()
    {

        Map map = new Map();
        Renderer renderer = new Renderer();
        Player player = new Player();
        Fountain fountain = new();
        while (true)
        {
            renderer.Render(map);
            Console.WriteLine($"You are in the room at ({player.CurrentLocation.Row}, {player.CurrentLocation.Column})");

            if (player.CurrentLocation.Row == 0 && player.CurrentLocation.Column == 0 && fountain.IsOn)
            {
                Console.WriteLine("You won!");
                break;
            }
            else if (player.CurrentLocation.Row == 0 && player.CurrentLocation.Column == 0)
            {
                Console.WriteLine("You see light in this room coming from outside the cavern. This is the entrance.");
            }
            else if ((player.CurrentLocation.Row == 0 && player.CurrentLocation.Column == 2) && (!fountain.IsOn))
            {
                Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
            }
            else if ((player.CurrentLocation.Row == 0 && player.CurrentLocation.Column == 2) && (fountain.IsOn))
            {
                Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
            }

            Console.Write("What do you want to do? ");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "n":
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
                    player.CurrentLocation.Row = player.CurrentLocation.Row - 1;
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
                    break;
                case "s":
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
                    player.CurrentLocation.Row = player.CurrentLocation.Row + 1;
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
                    break;
                case "w":
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
                    player.CurrentLocation.Column = player.CurrentLocation.Column - 1;
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
                    break;
                case "e":
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Empty);
                    player.CurrentLocation.Column = player.CurrentLocation.Column + 1;
                    map.Set(new Coordinate(player.CurrentLocation.Row, player.CurrentLocation.Column), RoomState.Player);
                    break;
                case "on":
                    fountain.IsOn = true;
                    break;
                case "off":
                    fountain.IsOn = false;
                    break;

            };

            renderer.Render(map);
            Console.WriteLine($"({player.CurrentLocation.Row}, {player.CurrentLocation.Column})");
        }
    }
}
//public interface IMove
//{
//    void Run
//}
public class GameObject
{
    public Coordinate CurrentLocation { get; set; }
}
public class Player : GameObject
{
    public Player()
    {
        CurrentLocation = new Coordinate(0, 0);
    }

}
public class Fountain : GameObject
{
    public bool IsOn { get; set; }

    public Fountain()
    {
        CurrentLocation = new Coordinate(0, 2);
    }
}

public class Map
{
    private RoomState[,] _room = new RoomState[4, 4];

    public RoomState Get(Coordinate coordinate)
    {
        return _room[coordinate.Row, coordinate.Column];
    }

    public void Set(Coordinate coordinate, RoomState newState)
    {
        _room[coordinate.Row, coordinate.Column] = newState;
    }
}

public class Renderer
{
    public void Render(Map map)
    {
        char[,] room = new char[4, 4];
        for (int row = 0; row < 4; row++)
            for (int column = 0; column < 4; column++)
                room[row, column] = ToChar(map.Get(new Coordinate(row, column)));

        Console.WriteLine($"┌───┬───┬───┬───┐");
        Console.WriteLine($"│ {room[0, 0]} │ {room[0, 1]} │ {room[0, 2]} │ {room[0, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[1, 0]} │ {room[1, 1]} │ {room[1, 2]} │ {room[1, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[2, 0]} │ {room[2, 1]} │ {room[2, 2]} │ {room[2, 3]} │");
        Console.WriteLine($"├───┼───┼───┼───┤");
        Console.WriteLine($"│ {room[3, 0]} │ {room[3, 1]} │ {room[3, 2]} │ {room[3, 3]} │");
        Console.WriteLine($"└───┴───┴───┴───┘");
    }

    private char ToChar(RoomState room)
    {
        return room switch
        {
            RoomState.Empty => ' ',
            RoomState.Player => 'X',
        };
    }
}

public class Coordinate
{
    public int Row;
    public int Column;

    public Coordinate(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }
}

public enum RoomState { Empty, Player }